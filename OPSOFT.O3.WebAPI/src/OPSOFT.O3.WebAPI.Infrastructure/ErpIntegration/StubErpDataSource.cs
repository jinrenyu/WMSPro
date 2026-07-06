using Microsoft.Extensions.Logging;
using OPSOFT.O3.WebAPI.Application.Interfaces;

namespace OPSOFT.O3.WebAPI.Infrastructure.ErpIntegration;

/// <summary>
/// 模拟 ERP 数据源（Provider=Stub）。内置若干物料样例，按请求的 fieldKeys 顺序投影返回，
/// 用于在无金蝶环境时把「配置→定时/手动同步→落库」全链路先行跑通。
/// </summary>
public class StubErpDataSource : IErpDataSource
{
    private readonly ILogger<StubErpDataSource> _logger;

    public StubErpDataSource(ILogger<StubErpDataSource> logger)
    {
        _logger = logger;
    }

    public Task<ErpQueryResult> ExecuteBillQueryAsync(string formId, string fieldKeys, string filterString, string orderString)
    {
        _logger.LogInformation("[StubErp] ExecuteBillQuery formId={FormId} fields={Fields} filter={Filter}", formId, fieldKeys, filterString);

        var requested = (fieldKeys ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();

        var sample = GetSampleRows(formId);

        var result = new ErpQueryResult { Fields = requested.Select(f => f.ToUpperInvariant()).ToList() };
        foreach (var rowMap in sample)
        {
            var arr = new object?[requested.Count];
            for (int i = 0; i < requested.Count; i++)
                arr[i] = rowMap.TryGetValue(requested[i].ToUpperInvariant(), out var v) ? v : null;
            result.Rows.Add(arr);
        }
        return Task.FromResult(result);
    }

    /// <summary>按 ERP 表单返回样例数据（字段名大写）。目前提供物料(BD_MATERIAL)。</summary>
    private static List<Dictionary<string, object?>> GetSampleRows(string formId)
    {
        if (string.Equals(formId, "BD_MATERIAL", StringComparison.OrdinalIgnoreCase))
        {
            return new List<Dictionary<string, object?>>
            {
                NewMaterial("100001", "同步物料A", "规格-A", "碳钢螺栓 M8"),
                NewMaterial("100002", "同步物料B", "规格-B", "不锈钢螺母 M8"),
                NewMaterial("100003", "同步物料C", "规格-C", "垫片 Φ8"),
            };
        }
        return new List<Dictionary<string, object?>>();
    }

    private static Dictionary<string, object?> NewMaterial(string number, string name, string spec, string desc) => new(StringComparer.OrdinalIgnoreCase)
    {
        ["FMATERIALID"] = number,       // ERP 内码（样例用编码占位）
        ["FNUMBER"] = number,
        ["FNAME"] = name,
        ["FSPECIFICATION"] = spec,
        ["FDESCRIPTION"] = desc,
        ["FBARCODE"] = string.Empty,
        ["FMATERIALGROUP"] = string.Empty,
        ["FERPCLSID"] = "1",
        ["FBASEUNITID"] = string.Empty,
        ["FISBATCHMANAGE"] = "0",
        ["FMINPOQTY"] = "0",
        ["FINCREASEQTY"] = "0",
    };
}
