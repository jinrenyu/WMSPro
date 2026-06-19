using System.Text;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 编码规则配置维护。表单清单与动态字段白名单取自 BillCodeFieldRegistry；
/// 保存校验：恰好一个流水号段、字段段必须在白名单内、补位符单字符、按示例值估算编号总长 ≤ 50
///（FBILLNO/FBARCODE 生产列长均为 VARCHAR(50)）。分段整删整插（硬删，沿用单据明细更新范式）。
/// </summary>
public class BillCodeRuleService : IBillCodeRuleService
{
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;
    private readonly IBillCodeService _billCode;
    private readonly IBillCodeFormCatalog _catalog;
    private readonly IOperationLogService? _operationLog;

    private const string PrgKey = "BillCodeRule";

    public BillCodeRuleService(ISqlSugarClient db, ICurrentUserService currentUser, IBillCodeService billCode,
        IBillCodeFormCatalog catalog, IOperationLogService? operationLog = null)
    {
        _db = db;
        _currentUser = currentUser;
        _billCode = billCode;
        _catalog = catalog;
        _operationLog = operationLog;
    }

    // ===== 列表 =====

    public async Task<List<BillCodeRuleListDto>> GetListAsync()
    {
        var forms = await _catalog.GetFormsAsync();
        var keys = forms.Select(f => f.FormKey).ToList();
        var listHeads = await _db.Queryable<SysListCode>().Where(r => keys.Contains(r.Fclasstypeid) && !r.FDeleted).ToListAsync();
        var listEntries = await _db.Queryable<SysListCodeEntry>().Where(e => keys.Contains(e.Fclasstypeid) && !e.FDeleted).OrderBy(e => e.FENTRYID).ToListAsync();
        var bcHeads = await _db.Queryable<SysBarcode>().Where(r => keys.Contains(r.Fclasstypeid) && !r.FDeleted).ToListAsync();
        var bcEntries = await _db.Queryable<SysBarcodeEntry>().Where(e => keys.Contains(e.Fclasstypeid) && !e.FDeleted).OrderBy(e => e.FENTRYID).ToListAsync();

        return forms.Select(f =>
        {
            var lh = listHeads.FirstOrDefault(x => x.Fclasstypeid == f.FormKey);
            var ls = listEntries.Where(x => x.Fclasstypeid == f.FormKey).Select(MapEntry).ToList();
            var bh = bcHeads.FirstOrDefault(x => x.Fclasstypeid == f.FormKey);
            var bs = bcEntries.Where(x => x.Fclasstypeid == f.FormKey).Select(MapEntry).ToList();
            return new BillCodeRuleListDto
            {
                FormKey = f.FormKey,
                FormName = f.FormName,
                BillConfigured = lh != null && ls.Count > 0,
                BillRuleName = lh?.Fname ?? string.Empty,
                BillSummary = SummaryOf(ls),
                BarcodeConfigured = bh != null && bs.Count > 0,
                BarcodeRuleName = bh?.Fname ?? string.Empty,
                BarcodeSummary = SummaryOf(bs),
                BillDisabled = lh?.FDisabled ?? false,
                BarcodeDisabled = bh?.FDisabled ?? false,
                MYmd = new[] { lh?.MYmd, bh?.MYmd }.Where(d => d.HasValue).Max()
            };
        }).ToList();
    }

    // ===== 业务表单目录登记（数据驱动：新单据在界面登记一行即出现在配置页）=====

    public Task<List<BillCodeDocumentDto>> GetAvailableDocumentsAsync()
        => _catalog.GetAvailableDocumentsAsync();

    public async Task RegisterFormAsync(BillCodeFormRegisterRequest request)
    {
        await _catalog.RegisterAsync(request);
        if (_operationLog != null)
            await _operationLog.LogAsync(PrgKey, OperationType.Update, request.FormKey?.Trim() ?? string.Empty,
                request.FormName?.Trim() ?? string.Empty, "登记编码规则业务表单", true);
    }

    public async Task UnregisterFormAsync(string formKey)
    {
        var formName = await _catalog.GetFormNameAsync(formKey) ?? formKey;
        await _catalog.UnregisterAsync(formKey);
        if (_operationLog != null)
            await _operationLog.LogAsync(PrgKey, OperationType.Delete, formKey, formName, "注销编码规则业务表单", true);
    }

    // ===== 详情 =====

    public async Task<BillCodeRuleDetailDto?> GetDetailAsync(string formKey)
    {
        var formName = await _catalog.GetFormNameAsync(formKey);
        if (formName == null) return null;

        var lh = await _db.Queryable<SysListCode>().Where(r => r.Fclasstypeid == formKey && !r.FDeleted).FirstAsync();
        var ls = await _db.Queryable<SysListCodeEntry>().Where(e => e.Fclasstypeid == formKey && !e.FDeleted).OrderBy(e => e.FENTRYID).ToListAsync();
        var bh = await _db.Queryable<SysBarcode>().Where(r => r.Fclasstypeid == formKey && !r.FDeleted).FirstAsync();
        var bs = await _db.Queryable<SysBarcodeEntry>().Where(e => e.Fclasstypeid == formKey && !e.FDeleted).OrderBy(e => e.FENTRYID).ToListAsync();

        return new BillCodeRuleDetailDto
        {
            FormKey = formKey,
            FormName = formName,
            Bill = new BillCodeRuleSectionDto
            {
                Fname = lh?.Fname ?? $"{formName}编号规则",
                Ismodify = lh?.Ismodify ?? true,
                Disabled = lh?.FDisabled ?? false,
                Fnote = lh?.Fnote ?? string.Empty,
                Segments = ls.Select(MapEntry).ToList()
            },
            Barcode = new BillCodeRuleSectionDto
            {
                Fname = bh?.Fname ?? $"{formName}条码规则",
                Ismodify = bh?.Ismodify ?? false,
                Disabled = bh?.FDisabled ?? false,
                Fnote = bh?.Fnote ?? string.Empty,
                Segments = bs.Select(MapEntry).ToList()
            },
            BillFields = BillCodeFieldRegistry.GetBillFields(formKey).ToList(),
            BarcodeFields = BillCodeFieldRegistry.GetBarcodeFields(formKey).ToList()
        };
    }

    // ===== 保存 =====

    public async Task<bool> SaveAsync(string formKey, SaveBillCodeRuleRequest request)
    {
        var formName = await _catalog.GetFormNameAsync(formKey)
            ?? throw new InvalidOperationException($"未注册的业务表单「{formKey}」");

        var bill = Normalize(request.Bill, $"{formName}编号规则");
        var barcode = Normalize(request.Barcode, $"{formName}条码规则");
        ValidateSection("单据编号规则", bill, BillCodeFieldRegistry.GetBillFields(formKey));
        ValidateSection("条码编号规则", barcode, BillCodeFieldRegistry.GetBarcodeFields(formKey));

        try
        {
            _db.AsTenant().BeginTran();
            await UpsertListRuleAsync(formKey, bill);
            await UpsertBarcodeRuleAsync(formKey, barcode);
            _db.AsTenant().CommitTran();
        }
        catch
        {
            _db.AsTenant().RollbackTran();
            throw;
        }

        // await 而非 fire-and-forget：Scoped SqlSugarClient 非线程安全，且避免与请求 Scope 释放赛跑丢日志
        if (_operationLog != null)
            await _operationLog.LogAsync(PrgKey, OperationType.Update, formKey, formName, "保存编码规则", true);
        return true;
    }

    // ===== 清除 / 重置流水 / 取号预览 =====

    public async Task<bool> DeleteAsync(string formKey)
    {
        var formName = await _catalog.GetFormNameAsync(formKey)
            ?? throw new InvalidOperationException($"未注册的业务表单「{formKey}」");
        var now = DateTime.Now;
        var user = _currentUser.UserId ?? string.Empty;
        try
        {
            _db.AsTenant().BeginTran();
            // 软删两套规则头（与 GetList/GetDetail 的 !FDeleted 过滤一致；重新保存时 Upsert 不带过滤会复活该头）
            await _db.Updateable<SysListCode>()
                .SetColumns(r => r.FDeleted == true).SetColumns(r => r.MYmd == now).SetColumns(r => r.MUser == user)
                .Where(r => r.Fclasstypeid == formKey && !r.FDeleted).ExecuteCommandAsync();
            await _db.Deleteable<SysListCodeEntry>().Where(e => e.Fclasstypeid == formKey).ExecuteCommandAsync();
            await _db.Updateable<SysBarcode>()
                .SetColumns(r => r.FDeleted == true).SetColumns(r => r.MYmd == now).SetColumns(r => r.MUser == user)
                .Where(r => r.Fclasstypeid == formKey && !r.FDeleted).ExecuteCommandAsync();
            await _db.Deleteable<SysBarcodeEntry>().Where(e => e.Fclasstypeid == formKey).ExecuteCommandAsync();
            _db.AsTenant().CommitTran();
        }
        catch
        {
            _db.AsTenant().RollbackTran();
            throw;
        }

        if (_operationLog != null)
            await _operationLog.LogAsync(PrgKey, OperationType.Delete, formKey, formName, "清除编码规则", true);
        return true;
    }

    public async Task<bool> ResetSeqAsync(string formKey)
    {
        var formName = await _catalog.GetFormNameAsync(formKey)
            ?? throw new InvalidOperationException($"未注册的业务表单「{formKey}」");
        try
        {
            _db.AsTenant().BeginTran();
            // 删计数行：下次取号当作首次，从起始号重新计（含历史迁移到本表单键的旧计数行）
            await _db.Deleteable<SysListCodeNo>().Where(s => s.Fclasstypeid == formKey).ExecuteCommandAsync();
            await _db.Deleteable<SysBarcodeNo>().Where(s => s.Fclasstypeid == formKey).ExecuteCommandAsync();
            _db.AsTenant().CommitTran();
        }
        catch
        {
            _db.AsTenant().RollbackTran();
            throw;
        }

        if (_operationLog != null)
            await _operationLog.LogAsync(PrgKey, OperationType.Update, formKey, formName, "重置编码流水", true);
        return true;
    }

    public async Task<BillCodeRulePreviewDto> PreviewAsync(string formKey)
    {
        if (await _catalog.GetFormNameAsync(formKey) == null)
            throw new InvalidOperationException($"未注册的业务表单「{formKey}」");
        return new BillCodeRulePreviewDto
        {
            Bill = await _billCode.PreviewAsync(formKey, barcode: false),
            Barcode = await _billCode.PreviewAsync(formKey, barcode: true)
        };
    }

    private static BillCodeRuleSectionDto Normalize(BillCodeRuleSectionDto? section, string defaultName)
    {
        var sec = section ?? new BillCodeRuleSectionDto();
        sec.Fname = string.IsNullOrWhiteSpace(sec.Fname) ? defaultName : sec.Fname.Trim();
        sec.Fnote = sec.Fnote?.Trim() ?? string.Empty;
        sec.Segments ??= new List<BillCodeSegmentDto>();
        foreach (var s in sec.Segments)
        {
            s.Ftype = s.Ftype?.Trim() ?? string.Empty;
            if (s.Ftype == "5") s.Ftype = "2"; // 数值字段与文本字段行为完全相同，已并入文本字段统一存储
            s.Fieldid = s.Fieldid?.Trim() ?? string.Empty;
            s.FieldName = s.FieldName?.Trim() ?? string.Empty;
            s.Fvalue = s.Fvalue?.Trim() ?? string.Empty;
            s.FormatString = s.FormatString?.Trim() ?? string.Empty;
            s.Fchar = s.Fchar ?? string.Empty;
            s.FcharAlignment = s.FcharAlignment?.Trim() ?? string.Empty;
            s.Fnote = s.Fnote?.Trim() ?? string.Empty;
            if (s.Ftype == "4") s.IsMember = true; // 流水段恒参与编号
            if (s.Ftype is "1" or "4") s.FormatString = string.Empty; // 常量/流水段不使用格式串（堵 API 直调塞任意值）
        }
        return sec;
    }

    private static void ValidateSection(string kindName, BillCodeRuleSectionDto sec, IReadOnlyList<BillCodeFieldDto> fields)
    {
        if (sec.Fname.Length > 200) throw new InvalidOperationException($"{kindName}：规则名称不能超过 200 字符");
        if (sec.Fnote.Length > 255) throw new InvalidOperationException($"{kindName}：描述不能超过 255 字符");
        if (sec.Segments.Count == 0) return; // 允许清空 = 未配置（生成时提示去配置）
        if (sec.Segments.Count > 20) throw new InvalidOperationException($"{kindName}：分段不能超过 20 个");

        var serialCount = sec.Segments.Count(s => s.Ftype == "4");
        if (serialCount != 1)
            throw new InvalidOperationException($"{kindName}：必须且只能配置一个流水号段（当前 {serialCount} 个）");

        var idx = 0;
        foreach (var s in sec.Segments)
        {
            idx++;
            var pos = $"{kindName}第 {idx} 段";
            if (s.Flenght is < 0 or > 50) throw new InvalidOperationException($"{pos}：长度必须在 0..50 之间");
            // 生产 FCHAR 为 VARCHAR(1)（1 字节），全角/中文字符占 2 字节会截断报错，限定单个半角可见字符
            if (s.Fchar.Length > 1 || (s.Fchar.Length == 1 && (s.Fchar[0] < ' ' || s.Fchar[0] > '~')))
                throw new InvalidOperationException($"{pos}：补位符只能是单个半角（ASCII）字符");
            if (s.FcharAlignment is not ("" or "L" or "R")) throw new InvalidOperationException($"{pos}：补位方向只能是 L 或 R");
            if (s.Fvalue.Length > 50) throw new InvalidOperationException($"{pos}：设置值不能超过 50 字符");
            // 生产 FORMATSTRING 为 VARCHAR(50) 按字节计
            if (DbByteLen(s.FormatString) > 50) throw new InvalidOperationException($"{pos}：格式串过长（按字节估算超过 50）");
            if (s.Fnote.Length > 255) throw new InvalidOperationException($"{pos}：描述不能超过 255 字符");
            // 非流水段：既不拼入编号(ISMEMBER)也不作依据(ISSERIAL)则该段无意义
            if (s.Ftype != "4" && !s.IsMember && !s.IsSerial)
                throw new InvalidOperationException($"{pos}：既未勾「拼入编号」也未勾「依据」，该段无意义，请勾选其一或删除该段");

            switch (s.Ftype)
            {
                case "1":
                    if (s.Fvalue.Length == 0) throw new InvalidOperationException($"{pos}：常量段必须填写设置值");
                    break;
                case "2":
                    var tf = fields.FirstOrDefault(f => f.FieldId == s.Fieldid && f.Kind == "text");
                    if (tf == null) throw new InvalidOperationException($"{pos}：字段「{(s.Fieldid.Length == 0 ? "(未选择)" : s.Fieldid)}」不在该单据可用字段范围内");
                    s.FieldName = tf.FieldName;
                    if (s.FormatString is not ("" or "U" or "L")) throw new InvalidOperationException($"{pos}：字段段格式只能是 U（大写）或 L（小写）");
                    break;
                case "3":
                    if (s.Fieldid.Length > 0)
                    {
                        var df = fields.FirstOrDefault(f => f.FieldId == s.Fieldid && f.Kind == "date");
                        if (df == null) throw new InvalidOperationException($"{pos}：日期字段「{s.Fieldid}」不在该单据可用字段范围内");
                        s.FieldName = df.FieldName;
                    }
                    try { _ = DateTime.Now.ToString(string.IsNullOrWhiteSpace(s.FormatString) ? "yyyyMMdd" : s.FormatString); }
                    catch (FormatException) { throw new InvalidOperationException($"{pos}：日期格式「{s.FormatString}」无效"); }
                    break;
                case "4":
                    if (s.Flenght > 9) throw new InvalidOperationException($"{pos}：流水号位数不能超过 9 位");
                    if (s.Fmin < 0 || s.Fmax < 0 || s.Fstep < 0) throw new InvalidOperationException($"{pos}：流水号起始/最大/步长不能为负数");
                    // 上限防 OccupyAsync 的 count*step / min+(count-1)*step int 乘法溢出（条码单批最大 5000）
                    if (s.Fstep > 1000) throw new InvalidOperationException($"{pos}：流水号步长不能超过 1000");
                    if (s.Fmin > 999_999_999 || s.Fmax > 999_999_999) throw new InvalidOperationException($"{pos}：流水号起始/最大值不能超过 999999999");
                    if (s.Fmax > 0 && s.Fmax < Math.Max(s.Fmin, 1)) throw new InvalidOperationException($"{pos}：最大流水号不能小于起始流水号");
                    break;
                default:
                    throw new InvalidOperationException($"{pos}：未知段类型「{s.Ftype}」");
            }
        }

        var sample = BuildSample(sec, fields);
        if (DbByteLen(sample) > 50)
            throw new InvalidOperationException($"{kindName}：按当前配置生成的编号约 {DbByteLen(sample)} 字节（示例 {sample}），超过 50 字节上限（FBILLNO/FBARCODE 生产列为 VARCHAR(50) 按字节计）");
    }

    /// <summary>按生产 DBCS collation 估算 VARCHAR 字节长度（CJK 等非 ASCII 字符按 2 字节计）</summary>
    private static int DbByteLen(string s) => s.Sum(c => c <= 0x7F ? 1 : 2);

    /// <summary>按示例值拼一个样例编号（长度校验用，逻辑与取号引擎一致）</summary>
    private static string BuildSample(BillCodeRuleSectionDto sec, IReadOnlyList<BillCodeFieldDto> fields)
    {
        var sb = new StringBuilder();
        foreach (var s in sec.Segments.Where(x => x.IsMember))
        {
            switch (s.Ftype)
            {
                case "1":
                    sb.Append(s.Fvalue);
                    break;
                case "3":
                    sb.Append(DateTime.Now.ToString(string.IsNullOrWhiteSpace(s.FormatString) ? "yyyyMMdd" : s.FormatString));
                    break;
                case "4":
                    sb.Append('0', Math.Max(s.Flenght, 1));
                    break;
                default:
                    var sample = fields.FirstOrDefault(f => f.FieldId == s.Fieldid)?.Sample;
                    sample = string.IsNullOrEmpty(sample) ? "XXXX" : sample;
                    if (s.Flenght > 0)
                        sample = sample.Length > s.Flenght ? sample[..s.Flenght] : sample.PadRight(s.Flenght, 'X');
                    sb.Append(sample);
                    break;
            }
        }
        return sb.ToString();
    }

    private static string SummaryOf(IReadOnlyList<BillCodeSegmentDto> segs)
        => segs.Count == 0 ? string.Empty : string.Join(" + ", segs.Select(s => s.Ftype switch
        {
            "1" => $"“{s.Fvalue}”",
            "3" => $"日期({(string.IsNullOrEmpty(s.FormatString) ? "yyyyMMdd" : s.FormatString)})",
            "4" => $"流水({(s.Flenght > 0 ? $"{s.Flenght}位" : "不补零")})",
            _ => $"字段({(string.IsNullOrEmpty(s.FieldName) ? s.Fieldid : s.FieldName)})"
        }));

    // ===== 实体映射与落库 =====

    private static BillCodeSegmentDto MapEntry(SysListCodeEntry e) => MapEntryCore(e.Ftype, e.FIELDID, e.FIELDNAME, e.FVALUE,
        e.FLENGHT, e.FMIN, e.FMAX, e.FSTEP, e.FORMATSTRING, e.FCHAR, e.FCHARALIGNMENT, e.ISSERIAL, e.ISMEMBER, e.Fnote);

    private static BillCodeSegmentDto MapEntry(SysBarcodeEntry e) => MapEntryCore(e.Ftype, e.FIELDID, e.FIELDNAME, e.FVALUE,
        e.FLENGHT, e.FMIN, e.FMAX, e.FSTEP, e.FORMATSTRING, e.FCHAR, e.FCHARALIGNMENT, e.ISSERIAL, e.ISMEMBER, e.Fnote);

    private static BillCodeSegmentDto MapEntryCore(string type, string fieldId, string fieldName, string value, int? length,
        int? min, int? max, int? step, string format, string padChar, string padAlign, bool? isSerial, bool? isMember, string note)
        => new()
        {
            Ftype = type,
            Fieldid = fieldId,
            FieldName = fieldName,
            Fvalue = value,
            Flenght = length ?? 0,
            Fmin = min ?? 0,
            Fmax = max ?? 0,
            Fstep = step ?? 0,
            FormatString = format,
            Fchar = padChar,
            FcharAlignment = padAlign,
            IsSerial = isSerial ?? false,
            IsMember = isMember ?? true,
            Fnote = note
        };

    private async Task UpsertListRuleAsync(string formKey, BillCodeRuleSectionDto sec)
    {
        var now = DateTime.Now;
        var user = _currentUser.UserId ?? string.Empty;
        var header = await _db.Queryable<SysListCode>().Where(r => r.Fclasstypeid == formKey).FirstAsync();
        if (header == null)
        {
            var uid = Guid.NewGuid().ToString("N");
            header = new SysListCode
            {
                Uid = uid, FInterId = uid, Fclasstypeid = formKey,
                Fname = sec.Fname, Ismodify = sec.Ismodify, Fnote = sec.Fnote, Fhex = 10, FDisabled = sec.Disabled,
                Fcheckdate = DateTime.MinValue, // 开发库该列 NOT NULL（生产为 DATE NULL），按惯例赋 MinValue 哨兵
                FStatus = 40, FCompanyId = "DEFAULT",
                CYmd = now, CUser = user, MYmd = now, MUser = user
            };
            await _db.Insertable(header).ExecuteCommandAsync();
        }
        else
        {
            header.Fname = sec.Fname;
            header.Ismodify = sec.Ismodify;
            header.FDisabled = sec.Disabled;
            header.Fnote = sec.Fnote;
            header.FDeleted = false;
            header.MYmd = now;
            header.MUser = user;
            await _db.Updateable(header).IgnoreColumns(e => new { e.CYmd, e.CUser }).ExecuteCommandAsync();
        }

        await _db.Deleteable<SysListCodeEntry>().Where(e => e.Fclasstypeid == formKey).ExecuteCommandAsync();
        var rows = sec.Segments.Select((s, i) =>
        {
            var uid = Guid.NewGuid().ToString("N");
            return new SysListCodeEntry
            {
                Uid = uid, FInterId = header.Uid, FDETAILID = uid, Fclasstypeid = formKey, FENTRYID = i + 1,
                Ftype = s.Ftype, FIELDID = s.Fieldid, FIELDNAME = s.FieldName, FVALUE = s.Fvalue,
                FLENGHT = s.Flenght, FMIN = s.Fmin, FMAX = s.Fmax, FSTEP = s.Fstep,
                FORMATSTRING = s.FormatString, FCHAR = s.Fchar, FCHARALIGNMENT = s.FcharAlignment,
                ISSERIAL = s.IsSerial, ISMEMBER = s.IsMember, Fnote = s.Fnote, FCODECONTRAST = string.Empty,
                FStatus = 40, FCompanyId = "DEFAULT",
                CYmd = now, CUser = user, MYmd = now, MUser = user
            };
        }).ToList();
        if (rows.Count > 0)
            await _db.Insertable(rows).ExecuteCommandAsync();
    }

    private async Task UpsertBarcodeRuleAsync(string formKey, BillCodeRuleSectionDto sec)
    {
        var now = DateTime.Now;
        var user = _currentUser.UserId ?? string.Empty;
        var header = await _db.Queryable<SysBarcode>().Where(r => r.Fclasstypeid == formKey).FirstAsync();
        if (header == null)
        {
            var uid = Guid.NewGuid().ToString("N");
            header = new SysBarcode
            {
                Uid = uid, FInterId = uid, Fclasstypeid = formKey, Fprgkey = formKey,
                Fname = sec.Fname, Ismodify = sec.Ismodify, Fnote = sec.Fnote, Fhex = 10, FDisabled = sec.Disabled,
                Fcheckdate = DateTime.MinValue, // 开发库该列 NOT NULL（生产为 DATE NULL），按惯例赋 MinValue 哨兵
                FStatus = 40, FCompanyId = "DEFAULT",
                CYmd = now, CUser = user, MYmd = now, MUser = user
            };
            await _db.Insertable(header).ExecuteCommandAsync();
        }
        else
        {
            header.Fname = sec.Fname;
            header.Ismodify = sec.Ismodify;
            header.FDisabled = sec.Disabled;
            header.Fnote = sec.Fnote;
            header.FDeleted = false;
            header.MYmd = now;
            header.MUser = user;
            await _db.Updateable(header).IgnoreColumns(e => new { e.CYmd, e.CUser }).ExecuteCommandAsync();
        }

        await _db.Deleteable<SysBarcodeEntry>().Where(e => e.Fclasstypeid == formKey).ExecuteCommandAsync();
        var rows = sec.Segments.Select((s, i) =>
        {
            var uid = Guid.NewGuid().ToString("N");
            return new SysBarcodeEntry
            {
                Uid = uid, FInterId = header.Uid, FDETAILID = uid, Fclasstypeid = formKey, FENTRYID = i + 1,
                Ftype = s.Ftype, FIELDID = s.Fieldid, FIELDNAME = s.FieldName, FVALUE = s.Fvalue,
                FLENGHT = s.Flenght, FMIN = s.Fmin, FMAX = s.Fmax, FSTEP = s.Fstep,
                FORMATSTRING = s.FormatString, FCHAR = s.Fchar, FCHARALIGNMENT = s.FcharAlignment,
                ISSERIAL = s.IsSerial, ISMEMBER = s.IsMember, Fnote = s.Fnote, FCODECONTRAST = string.Empty,
                FStatus = 40, FCompanyId = "DEFAULT",
                CYmd = now, CUser = user, MYmd = now, MUser = user
            };
        }).ToList();
        if (rows.Count > 0)
            await _db.Insertable(rows).ExecuteCommandAsync();
    }
}
