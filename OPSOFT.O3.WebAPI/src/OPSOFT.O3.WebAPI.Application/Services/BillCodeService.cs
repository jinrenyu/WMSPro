using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 编码规则取号引擎。
/// 段类型 FTYPE：1=常量 2=文本字段 3=日期字段 4=流水号（5=数值字段，按文本处理）；
/// 勾选"编码依据"(ISSERIAL) 的段值拼成流水键（SYS_*CODENO.FBILLCODE）——日期段做依据即按日重置，
/// 字段段做依据即按字段值分组独立取号；流水占号用 DB 端原子自增（SET FSEQ=FSEQ+N），
/// 首行并发 INSERT 撞 (FCLASSTYPEID,FBILLCODE) 唯一索引时回退重试 UPDATE 路径。
/// 单据/条码两套表结构同构，投影为中性段模型后共用装配逻辑。
/// </summary>
public class BillCodeService : IBillCodeService
{
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;
    private readonly IBillCodeFormCatalog _catalog;

    public BillCodeService(ISqlSugarClient db, ICurrentUserService currentUser, IBillCodeFormCatalog catalog)
    {
        _db = db;
        _currentUser = currentUser;
        _catalog = catalog;
    }

    /// <summary>由单据表头实体类名解析 formKey（基类反射取号用）。委托数据驱动目录。</summary>
    public Task<string?> ResolveFormKeyByEntityAsync(string entityName)
        => _catalog.ResolveFormKeyByEntityAsync(entityName);

    private sealed record Segment(string Type, string FieldId, string FieldName, string Value, int Length,
        int Min, int Max, int Step, string Format, string PadChar, string PadAlign, bool IsSerial, bool IsMember);

    public async Task<string> ResolveBillNoAsync(string formKey, string? manualNo,
        IReadOnlyDictionary<string, string>? context = null, Func<string, Task<bool>>? existsAsync = null)
    {
        var manual = manualNo?.Trim() ?? string.Empty;
        if (manual.Length > 0)
        {
            if (DbByteLen(manual) > 50)
                throw new InvalidOperationException("单据编号不能超过 50 字节（FBILLNO 列长上限）");
            var rule = await _db.Queryable<SysListCode>().Where(r => r.Fclasstypeid == formKey && !r.FDeleted).FirstAsync();
            if (rule != null)
            {
                // 禁用拦截：手工号与自动号语义一致（自动号在 NextBillNosAsync 内拦截）
                if (rule.FDisabled)
                    throw new InvalidOperationException($"单据编码规则「{rule.Fname}」已禁用，无法使用");
                if (!rule.Ismodify)
                    throw new InvalidOperationException($"编码规则「{rule.Fname}」不允许手工指定单据编号");
            }
            if (existsAsync != null && await existsAsync(manual))
                throw new InvalidOperationException($"单据编号「{manual}」已存在");
            return manual;
        }

        // 自动取号撞到已占用编号（如手工抢填了规则的下一个号）时跳号重取：
        // 占号与建单同事务，提交后流水即永久越过被占用号，避免唯一索引冲突回滚→重取同号的死循环
        var no = (await NextBillNosAsync(formKey, 1, context))[0];
        if (existsAsync != null)
        {
            var guard = 0;
            while (await existsAsync(no))
            {
                if (++guard > 100)
                    throw new InvalidOperationException("自动取号连续撞到已存在的单据编号，请检查编码规则或流水计数");
                no = (await NextBillNosAsync(formKey, 1, context))[0];
            }
        }
        return no;
    }

    public async Task<List<string>> NextBillNosAsync(string formKey, int count, IReadOnlyDictionary<string, string>? context = null)
    {
        var rule = await _db.Queryable<SysListCode>().Where(r => r.Fclasstypeid == formKey && !r.FDeleted).FirstAsync();
        if (rule == null)
        {
            var name = await _catalog.GetFormNameAsync(formKey) ?? formKey;
            throw new InvalidOperationException($"未配置「{name}」的单据编码规则，请到 系统管理→编码规则 配置");
        }
        if (rule.FDisabled)
            throw new InvalidOperationException($"单据编码规则「{rule.Fname}」已禁用");

        var rows = await _db.Queryable<SysListCodeEntry>()
            .Where(e => e.Fclasstypeid == formKey && !e.FDeleted)
            .OrderBy(e => e.FENTRYID)
            .ToListAsync();
        var segments = rows.Select(e => ToSegment(e.Ftype, e.FIELDID, e.FIELDNAME, e.FVALUE, e.FLENGHT,
            e.FMIN, e.FMAX, e.FSTEP, e.FORMATSTRING, e.FCHAR, e.FCHARALIGNMENT, e.ISSERIAL, e.ISMEMBER)).ToList();
        return await BuildAsync(barcode: false, rule.Fname, formKey, segments, count, context);
    }

    public async Task<List<string>> NextBarcodesAsync(string formKey, int count, IReadOnlyDictionary<string, string>? context = null)
    {
        var rule = await _db.Queryable<SysBarcode>().Where(r => r.Fclasstypeid == formKey && !r.FDeleted).FirstAsync();
        if (rule == null)
        {
            var name = await _catalog.GetFormNameAsync(formKey) ?? formKey;
            throw new InvalidOperationException($"未配置「{name}」的条码编码规则，请到 系统管理→编码规则 配置");
        }
        if (rule.FDisabled)
            throw new InvalidOperationException($"条码编码规则「{rule.Fname}」已禁用");

        var rows = await _db.Queryable<SysBarcodeEntry>()
            .Where(e => e.Fclasstypeid == formKey && !e.FDeleted)
            .OrderBy(e => e.FENTRYID)
            .ToListAsync();
        var segments = rows.Select(e => ToSegment(e.Ftype, e.FIELDID, e.FIELDNAME, e.FVALUE, e.FLENGHT,
            e.FMIN, e.FMAX, e.FSTEP, e.FORMATSTRING, e.FCHAR, e.FCHARALIGNMENT, e.ISSERIAL, e.ISMEMBER)).ToList();
        return await BuildAsync(barcode: true, rule.Fname, formKey, segments, count, context);
    }

    private static Segment ToSegment(string type, string fieldId, string fieldName, string value, int? length,
        int? min, int? max, int? step, string format, string padChar, string padAlign, bool? isSerial, bool? isMember)
        => new(type?.Trim() ?? string.Empty, fieldId ?? string.Empty, fieldName ?? string.Empty, value ?? string.Empty,
            length ?? 0, min ?? 0, max ?? 0, step ?? 0, format ?? string.Empty, padChar ?? string.Empty,
            padAlign ?? string.Empty, isSerial ?? false, isMember ?? true);

    // ===== 编号装配 =====

    /// <summary>装配中间结果：Tokens 为各段拼接片段（流水段位置占位空串），填入流水值即成编号</summary>
    private sealed record Assembled(List<string> Tokens, int SerialIdx, Segment Serial, string BasisKey);

    /// <summary>
    /// 按规则段 + 上下文装配编号骨架与流水键（纯计算、不落库）：解析常量/日期/字段段为 token，
    /// 勾"依据"的非流水段值拼成 basisKey，定位唯一流水段。取号与预览共用此逻辑。
    /// </summary>
    private Assembled Assemble(string ruleName, List<Segment> segments, IReadOnlyDictionary<string, string> ctx)
    {
        var active = segments.Where(s => s.IsMember || s.IsSerial).ToList();
        if (active.Count == 0)
            throw new InvalidOperationException($"编码规则「{ruleName}」未配置任何分段，请到 系统管理→编码规则 配置");

        Segment? serial = null;
        var serialIdx = -1;
        var tokens = new List<string>(active.Count);
        var basisParts = new List<string>();
        foreach (var seg in active)
        {
            string token;
            switch (seg.Type)
            {
                case "4":
                    if (serial != null)
                        throw new InvalidOperationException($"编码规则「{ruleName}」配置了多个流水号段");
                    serial = seg;
                    serialIdx = tokens.Count;
                    token = string.Empty;
                    break;
                case "1":
                    token = seg.Value;
                    break;
                case "3":
                    token = ResolveDate(seg, ctx, ruleName);
                    break;
                default: // 2=文本字段 / 5=数值字段
                    token = ResolveField(seg, ctx, ruleName);
                    break;
            }
            if (seg.IsSerial && seg.Type != "4")
                basisParts.Add(token);
            // 流水段恒参与编号；其余段未勾"编码元素"时只作依据、不拼入编号
            tokens.Add(seg.Type == "4" || seg.IsMember ? token : string.Empty);
        }
        if (serial == null)
            throw new InvalidOperationException($"编码规则「{ruleName}」未配置流水号段");

        var basisKey = string.Join("|", basisParts);
        if (DbByteLen(basisKey) > 250)
            throw new InvalidOperationException($"编码规则「{ruleName}」编码依据值过长（超过 250 字节）");
        return new Assembled(tokens, serialIdx, serial, basisKey);
    }

    private async Task<List<string>> BuildAsync(bool barcode, string ruleName, string formKey,
        List<Segment> segments, int count, IReadOnlyDictionary<string, string>? context)
    {
        if (count <= 0)
            throw new InvalidOperationException("取号数量必须大于 0");
        var a = Assemble(ruleName, segments, context ?? new Dictionary<string, string>());

        var min = a.Serial.Min > 0 ? a.Serial.Min : 1;
        var step = a.Serial.Step > 0 ? a.Serial.Step : 1;
        var digits = a.Serial.Length;

        var start = await OccupyAsync(barcode, formKey, a.BasisKey, count, min, step);
        var last = start + (long)(count - 1) * step;
        if (a.Serial.Max > 0 && last > a.Serial.Max)
            throw new InvalidOperationException($"编码规则「{ruleName}」流水号将达到 {last}，超出最大值 {a.Serial.Max}");
        if (digits > 0 && last.ToString().Length > digits)
            throw new InvalidOperationException($"编码规则「{ruleName}」流水号将达到 {last}，超出 {digits} 位上限，请调整规则");

        var results = new List<string>(count);
        for (var i = 0; i < count; i++)
        {
            var val = start + (long)i * step;
            a.Tokens[a.SerialIdx] = digits > 0 ? val.ToString().PadLeft(digits, '0') : val.ToString();
            var no = string.Concat(a.Tokens);
            if (DbByteLen(no) > 50)
                throw new InvalidOperationException($"生成的编号「{no}」超过 50 字节（FBILLNO/FBARCODE 生产列为 VARCHAR(50) 按字节计），请精简编码规则");
            results.Add(no);
        }
        return results;
    }

    /// <summary>
    /// 取号预览（不占号）：按已保存规则、用字段白名单样例值 + 当前日期构造上下文，
    /// 读该流水键当前已占用流水算"下一个号"。无法生成时把原因写入 Message 返回，不抛异常。
    /// </summary>
    public async Task<BillCodePreviewDto> PreviewAsync(string formKey, bool barcode)
    {
        var dto = new BillCodePreviewDto();
        string ruleName;
        List<Segment> segments;
        if (barcode)
        {
            var rule = await _db.Queryable<SysBarcode>().Where(r => r.Fclasstypeid == formKey && !r.FDeleted).FirstAsync();
            if (rule == null) { dto.Message = "未配置条码编码规则"; return dto; }
            ruleName = rule.Fname;
            dto.Disabled = rule.FDisabled;
            var rows = await _db.Queryable<SysBarcodeEntry>().Where(e => e.Fclasstypeid == formKey && !e.FDeleted).OrderBy(e => e.FENTRYID).ToListAsync();
            segments = rows.Select(e => ToSegment(e.Ftype, e.FIELDID, e.FIELDNAME, e.FVALUE, e.FLENGHT,
                e.FMIN, e.FMAX, e.FSTEP, e.FORMATSTRING, e.FCHAR, e.FCHARALIGNMENT, e.ISSERIAL, e.ISMEMBER)).ToList();
        }
        else
        {
            var rule = await _db.Queryable<SysListCode>().Where(r => r.Fclasstypeid == formKey && !r.FDeleted).FirstAsync();
            if (rule == null) { dto.Message = "未配置单据编码规则"; return dto; }
            ruleName = rule.Fname;
            dto.Disabled = rule.FDisabled;
            var rows = await _db.Queryable<SysListCodeEntry>().Where(e => e.Fclasstypeid == formKey && !e.FDeleted).OrderBy(e => e.FENTRYID).ToListAsync();
            segments = rows.Select(e => ToSegment(e.Ftype, e.FIELDID, e.FIELDNAME, e.FVALUE, e.FLENGHT,
                e.FMIN, e.FMAX, e.FSTEP, e.FORMATSTRING, e.FCHAR, e.FCHARALIGNMENT, e.ISSERIAL, e.ISMEMBER)).ToList();
        }

        if (segments.Count == 0) { dto.Message = "未配置分段"; return dto; }
        dto.Configured = true;

        var fields = barcode ? BillCodeFieldRegistry.GetBarcodeFields(formKey) : BillCodeFieldRegistry.GetBillFields(formKey);
        var ctx = new Dictionary<string, string>();
        foreach (var f in fields)
            if (!string.IsNullOrEmpty(f.Sample)) ctx[f.FieldId] = f.Sample;

        try
        {
            var a = Assemble(ruleName, segments, ctx);
            var step = a.Serial.Step > 0 ? a.Serial.Step : 1;
            var min = a.Serial.Min > 0 ? a.Serial.Min : 1;
            var digits = a.Serial.Length;
            var cur = await ReadSeqAsync(barcode, formKey, a.BasisKey);
            var nextVal = cur.HasValue ? cur.Value + step : min;
            dto.BasisKey = a.BasisKey;
            dto.CurrentSeq = cur ?? 0;
            // 与 BuildAsync 真实取号同口径的越界校验，避免面板对"取号会失败"的规则仍显示正常号
            if (a.Serial.Max > 0 && nextVal > a.Serial.Max)
            {
                dto.Message = $"下一个流水号 {nextVal} 将超出最大值 {a.Serial.Max}，取号会失败";
                return dto;
            }
            if (digits > 0 && nextVal.ToString().Length > digits)
            {
                dto.Message = $"下一个流水号 {nextVal} 将超出 {digits} 位上限，取号会失败";
                return dto;
            }
            a.Tokens[a.SerialIdx] = digits > 0 ? nextVal.ToString().PadLeft(digits, '0') : nextVal.ToString();
            var no = string.Concat(a.Tokens);
            dto.NextNo = no;
            dto.ByteLength = DbByteLen(no);
        }
        catch (InvalidOperationException ex)
        {
            dto.Message = ex.Message; // 字段缺值/格式错误/多流水段等：原样回显，不阻断配置页
        }
        return dto;
    }

    /// <summary>读取指定流水键当前已占用到的流水值（无计数行返回 null = 尚未取过号）</summary>
    private async Task<int?> ReadSeqAsync(bool barcode, string formKey, string basisKey)
    {
        if (barcode)
        {
            var row = await _db.Queryable<SysBarcodeNo>().Where(s => s.Fclasstypeid == formKey && s.Fbillcode == basisKey).FirstAsync();
            return row?.Fseq;
        }
        var lrow = await _db.Queryable<SysListCodeNo>().Where(s => s.Fclasstypeid == formKey && s.Fbillcode == basisKey).FirstAsync();
        return lrow?.Fseq;
    }

    private static string ResolveField(Segment seg, IReadOnlyDictionary<string, string> ctx, string ruleName)
    {
        if (string.IsNullOrEmpty(seg.FieldId))
            throw new InvalidOperationException($"编码规则「{ruleName}」的字段段未选择来源字段");
        var display = string.IsNullOrEmpty(seg.FieldName) ? seg.FieldId : seg.FieldName;
        if (!ctx.TryGetValue(seg.FieldId, out var raw) || string.IsNullOrWhiteSpace(raw))
            throw new InvalidOperationException($"编码规则「{ruleName}」的字段「{display}」未取到值，无法生成编号");

        var v = raw.Trim();
        if (seg.Format == "U") v = v.ToUpperInvariant();
        else if (seg.Format == "L") v = v.ToLowerInvariant();
        if (seg.Length > 0)
        {
            if (v.Length > seg.Length) v = v[..seg.Length];
            else if (v.Length < seg.Length && seg.PadChar.Length > 0)
                v = seg.PadAlign == "L" ? v.PadLeft(seg.Length, seg.PadChar[0]) : v.PadRight(seg.Length, seg.PadChar[0]);
        }
        return v;
    }

    private static string ResolveDate(Segment seg, IReadOnlyDictionary<string, string> ctx, string ruleName)
    {
        var dt = DateTime.Now;
        // 上下文给了可解析的日期值则采用；MinValue 哨兵（落库 1900-01-01）视为未提供，回落当前时间
        if (seg.FieldId.Length > 0 && ctx.TryGetValue(seg.FieldId, out var raw)
            && DateTime.TryParse(raw, out var parsed) && parsed.Year > 1900)
            dt = parsed;
        var fmt = string.IsNullOrWhiteSpace(seg.Format) ? "yyyyMMdd" : seg.Format;
        try
        {
            return dt.ToString(fmt);
        }
        catch (FormatException)
        {
            throw new InvalidOperationException($"编码规则「{ruleName}」日期段格式「{fmt}」无效");
        }
    }

    // ===== 流水占号（SYS_LISTCODENO / SYS_BARCODENO，须在调用方事务内）=====

    /// <summary>
    /// 原子预占 count 个流水（步长 step），返回本批起始流水值。
    /// UPDATE 命中则 DB 端自增后回读倒推起点；未命中（该依据键首次取号）则 INSERT 起始行，
    /// INSERT 撞唯一索引（并发首插）时回退重试 UPDATE。
    /// </summary>
    private async Task<int> OccupyAsync(bool barcode, string formKey, string basisKey, int count, int min, int step)
    {
        var delta = count * step;
        if (barcode)
        {
            var affected = await BumpBarcodeSeqAsync(formKey, basisKey, delta);
            if (affected == 0)
            {
                if (await TryInsertSeqRowAsync(NewSeqRow<SysBarcodeNo>(formKey, basisKey, min + (count - 1) * step)))
                    return min;
                affected = await BumpBarcodeSeqAsync(formKey, basisKey, delta);
                if (affected == 0)
                    throw new InvalidOperationException("流水占号失败：并发冲突重试后仍未命中计数行");
            }
            var newSeq = await _db.Queryable<SysBarcodeNo>()
                .Where(s => s.Fclasstypeid == formKey && s.Fbillcode == basisKey)
                .Select(s => s.Fseq).FirstAsync();
            return newSeq - delta + step;
        }
        else
        {
            var affected = await BumpListSeqAsync(formKey, basisKey, delta);
            if (affected == 0)
            {
                if (await TryInsertSeqRowAsync(NewSeqRow<SysListCodeNo>(formKey, basisKey, min + (count - 1) * step)))
                    return min;
                affected = await BumpListSeqAsync(formKey, basisKey, delta);
                if (affected == 0)
                    throw new InvalidOperationException("流水占号失败：并发冲突重试后仍未命中计数行");
            }
            var newSeq = await _db.Queryable<SysListCodeNo>()
                .Where(s => s.Fclasstypeid == formKey && s.Fbillcode == basisKey)
                .Select(s => s.Fseq).FirstAsync();
            return newSeq - delta + step;
        }
    }

    private Task<int> BumpBarcodeSeqAsync(string formKey, string basisKey, int delta)
        => _db.Updateable<SysBarcodeNo>()
            .SetColumns(s => s.Fseq == s.Fseq + delta)
            .SetColumns(s => s.MYmd == DateTime.Now)
            .Where(s => s.Fclasstypeid == formKey && s.Fbillcode == basisKey)
            .ExecuteCommandAsync();

    private Task<int> BumpListSeqAsync(string formKey, string basisKey, int delta)
        => _db.Updateable<SysListCodeNo>()
            .SetColumns(s => s.Fseq == s.Fseq + delta)
            .SetColumns(s => s.MYmd == DateTime.Now)
            .Where(s => s.Fclasstypeid == formKey && s.Fbillcode == basisKey)
            .ExecuteCommandAsync();

    /// <summary>
    /// 插入计数起始行：成功返回 true，撞 (FCLASSTYPEID,FBILLCODE) 唯一索引（并发首插输家）返回 false。
    /// PostgreSQL 下语句失败会把整个事务置为 aborted（25P02），必须用 SAVEPOINT 包住 INSERT、
    /// 冲突后 ROLLBACK TO SAVEPOINT 事务才能继续重试 UPDATE（MSSQL 默认语句级回滚 / SQLite 仅中止语句，无此问题）。
    /// 非重复键异常原样抛出，避免用注定回滚的事务重试并掩盖根因（如 MSSQL 死锁 1205）。
    /// </summary>
    private async Task<bool> TryInsertSeqRowAsync<T>(T row) where T : BaseEntity, new()
    {
        var isPg = _db.CurrentConnectionConfig.DbType == DbType.PostgreSQL;
        if (isPg) await _db.Ado.ExecuteCommandAsync("SAVEPOINT sp_codeno");
        try
        {
            await _db.Insertable(row).ExecuteCommandAsync();
            if (isPg) await _db.Ado.ExecuteCommandAsync("RELEASE SAVEPOINT sp_codeno");
            return true;
        }
        catch (Exception ex) when (IsDuplicateKey(ex))
        {
            if (isPg) await _db.Ado.ExecuteCommandAsync("ROLLBACK TO SAVEPOINT sp_codeno");
            return false;
        }
    }

    private static bool IsDuplicateKey(Exception? ex)
    {
        for (var e = ex; e != null; e = e.InnerException)
        {
            var m = e.Message;
            if (m.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase)
                || m.Contains("duplicate", StringComparison.OrdinalIgnoreCase)
                || m.Contains("重复键")
                || m.Contains("23505"))
                return true;
        }
        return false;
    }

    /// <summary>按生产 DBCS collation 估算 VARCHAR 字节长度（CJK 等非 ASCII 字符按 2 字节计）</summary>
    private static int DbByteLen(string s) => s.Sum(c => c <= 0x7F ? 1 : 2);

    private T NewSeqRow<T>(string formKey, string basisKey, int seq) where T : BaseEntity, new()
    {
        var uid = Guid.NewGuid().ToString("N");
        var row = new T
        {
            Uid = uid,
            FInterId = uid,
            FGroupId = string.Empty,
            FStatus = 0,
            FDeleted = false,
            FDisabled = false,
            FCompanyId = _currentUser.CompanyId ?? string.Empty,
            CYmd = DateTime.Now,
            CUser = _currentUser.UserId ?? string.Empty,
            MYmd = DateTime.Now,
            MUser = _currentUser.UserId ?? string.Empty
        };
        switch (row)
        {
            case SysBarcodeNo b:
                b.Fclasstypeid = formKey; b.Fbillcode = basisKey; b.Fseq = seq; break;
            case SysListCodeNo l:
                l.Fclasstypeid = formKey; l.Fbillcode = basisKey; l.Fseq = seq; break;
        }
        return row;
    }
}
