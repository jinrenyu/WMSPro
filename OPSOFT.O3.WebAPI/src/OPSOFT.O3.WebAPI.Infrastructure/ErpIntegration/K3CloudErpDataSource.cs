using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using YiKdWebClient;
using YiKdWebClient.Model;

namespace OPSOFT.O3.WebAPI.Infrastructure.ErpIntegration;

/// <summary>
/// 金蝶云星空 K3Cloud WebAPI 数据源（Provider=K3Cloud）。
/// 登录/取数走开源客户端 YiKdWebClient（YiK3CloudClient，纯 HTTP、支持 .NET 10、含用户名密码/AppSecret/签名模式）。
/// 用户名密码：LoginType=ValidateLogin + ValidateLoginSettingsModel；ExecuteBillQuery(json) 返回原始 JSON 字符串。
/// 响应解析（含把金蝶错误对象识别为失败而非静默当数据）由本类的 ParseRows 处理。
/// 连接参数来自 appsettings 的 ErpIntegration 节；已对真实金蝶联调通过。
/// </summary>
public class K3CloudErpDataSource : IErpDataSource
{
    private readonly ErpIntegrationOptions _options;
    private readonly ILogger<K3CloudErpDataSource> _logger;

    // 查询 JSON 序列化：用 UnsafeRelaxedJsonEscaping，使 FilterString 里的单引号输出字面 '（而非 '）、
    // 中文输出字面 UTF-8（而非 \uXXXX），发给金蝶的请求体干净可读（非 HTML 场景，安全）。
    private static readonly JsonSerializerOptions QueryJsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public K3CloudErpDataSource(IOptions<ErpIntegrationOptions> options, ILogger<K3CloudErpDataSource> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public async Task<ErpQueryResult> ExecuteBillQueryAsync(string formId, string fieldKeys, string filterString, string orderString)
    {
        if (string.IsNullOrWhiteSpace(_options.ServerUrl))
            throw new InvalidOperationException("未配置金蝶云星空站点地址（ErpIntegration:ServerUrl）");
        if (string.IsNullOrEmpty(_options.Password))
            throw new InvalidOperationException("未配置金蝶登录密码（ErpIntegration:Password）");

        var fields = (fieldKeys ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(f => f.ToUpperInvariant()).ToList();
        var result = new ErpQueryResult { Fields = fields };

        // YiK3CloudClient 为同步阻塞调用，放到线程池执行，避免占用调用线程。
        await Task.Run(() =>
        {
            var client = new YiK3CloudClient
            {
                LoginType = LoginType.ValidateLogin,
                validateLoginSettingsModel = new ValidateLoginSettingsModel
                {
                    Url = _options.ServerUrl,       // 形如 http://host:port/k3cloud/
                    DbId = _options.AcctId,
                    UserName = _options.UserName,
                    Password = _options.Password,
                    lcid = _options.LcId
                }
            };

            // ExecuteBillQuery 的 Limit 是单次分页上限，须按 StartRow 递增翻页取尽（否则 >一页 被静默截断）。
            // 页大小可配（ErpIntegration:QueryPageSize，默认 500）：含图片等大字段时单页响应会爆炸，应从字段中移除大字段。
            var pageSize = _options.QueryPageSize > 0 ? _options.QueryPageSize : 500;
            const int maxRows = 500_000; // 安全上限，避免异常无限翻页
            int startRow = 0;
            bool firstPage = true;      // 会话复用：首页登录并保持会话，后续页复用，避免每页重复登录拖慢大表同步
            while (true)
            {
                var queryJson = JsonSerializer.Serialize(new Dictionary<string, object?>
                {
                    ["FormId"] = formId,
                    ["FieldKeys"] = fieldKeys,
                    ["FilterString"] = filterString ?? string.Empty,
                    ["OrderString"] = orderString ?? string.Empty,
                    ["TopRowCount"] = 0,
                    ["StartRow"] = startRow,
                    ["Limit"] = pageSize,
                }, QueryJsonOptions);
                _logger.LogDebug("[K3Cloud] ExecuteBillQuery {Json}", queryJson);
                // 首页 AutoLogin=true 登录、AutoLogout=false 保持会话；后续页 AutoLogin=false 复用会话。
                var resp = client.ExecuteBillQuery(queryJson, firstPage, false);
                firstPage = false;
                var pageRows = ParseRows(resp);
                result.Rows.AddRange(pageRows);
                if (pageRows.Count < pageSize) break;
                startRow += pageSize;
                if (startRow >= maxRows) break;
            }
        });

        return result;
    }

    private List<object?[]> ParseRows(string resp)
    {
        if (string.IsNullOrWhiteSpace(resp))
            throw new InvalidOperationException("金蝶取数返回为空（可能登录失败或网络异常）");

        var rows = new List<object?[]>();
        JsonDocument doc;
        try { doc = JsonDocument.Parse(resp); }
        catch (JsonException) { throw new InvalidOperationException($"金蝶取数返回无法解析：{Truncate(resp)}"); }
        using (doc)
        {
            var root = doc.RootElement;
            // 顶层为对象 = 错误响应（登录失败/会话失效等）→ 提取金蝶错误信息抛出（勿静默）
            if (root.ValueKind == JsonValueKind.Object)
                throw new InvalidOperationException($"金蝶取数失败：{ExtractKingdeeError(root) ?? Truncate(resp)}");
            if (root.ValueKind != JsonValueKind.Array)
                throw new InvalidOperationException($"金蝶取数返回异常：{Truncate(resp)}");

            // 正常成功：[[值,值,...],...]（内层为标量）；出错：金蝶把错误对象包成数组返回，
            // 单层 [{Result:...}] 或双层 [[{Result:...}]]（表单不存在/字段非法/会话失效）。
            // 若把错误对象当数据行 upsert 会"静默把错误当数据"，故须识别并抛出真实金蝶错误。
            foreach (var rowEl in root.EnumerateArray())
            {
                if (rowEl.ValueKind == JsonValueKind.Object)
                {
                    var err = ExtractKingdeeError(rowEl);
                    throw new InvalidOperationException($"金蝶取数失败：{err ?? Truncate(resp)}");
                }
                if (rowEl.ValueKind != JsonValueKind.Array) continue;

                var cells = rowEl.EnumerateArray().ToArray();
                // 内层首元素为对象 = 非正常数据行（ExecuteBillQuery 数据列均为标量）→ 按错误处理，勿当数据
                if (cells.Length > 0 && cells[0].ValueKind == JsonValueKind.Object)
                {
                    var err = ExtractKingdeeError(cells[0]);
                    throw new InvalidOperationException($"金蝶取数失败：{err ?? Truncate(resp)}");
                }
                rows.Add(cells.Select(ToClr).ToArray());
            }
        }
        return rows;
    }

    /// <summary>从金蝶响应对象提取错误信息（Result.ResponseStatus.Errors[].Message）</summary>
    private static string? ExtractKingdeeError(JsonElement el)
    {
        if (el.TryGetProperty("Result", out var result) && result.ValueKind == JsonValueKind.Object
            && result.TryGetProperty("ResponseStatus", out var rs) && rs.ValueKind == JsonValueKind.Object
            && rs.TryGetProperty("Errors", out var errs) && errs.ValueKind == JsonValueKind.Array && errs.GetArrayLength() > 0)
        {
            var first = errs[0];
            if (first.ValueKind == JsonValueKind.Object && first.TryGetProperty("Message", out var m)) return m.GetString();
            if (first.ValueKind == JsonValueKind.String) return first.GetString();
        }
        // 顶层 Message（登录失败风格：{"LoginResultType":0,"Message":"..."}）
        if (el.TryGetProperty("Message", out var topMsg) && topMsg.ValueKind == JsonValueKind.String
            && !string.IsNullOrEmpty(topMsg.GetString()))
            return topMsg.GetString();
        return null;
    }

    private static object? ToClr(JsonElement el) => el.ValueKind switch
    {
        JsonValueKind.String => el.GetString(),
        JsonValueKind.Number => el.TryGetInt64(out var l) ? l : el.GetDecimal(),
        JsonValueKind.True => true,
        JsonValueKind.False => false,
        JsonValueKind.Null => null,
        _ => el.GetRawText()
    };

    private static string Truncate(string s) => s.Length > 500 ? s[..500] : s;
}
