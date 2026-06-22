using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Extensions;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 出入库流程配置服务（真实表 T_BOS_SELBILL / T_BOS_SELBILLENTRY，一主一从）。
/// 配置后续单据（采购入库单、采购退料单等）的「源单类型」可取值，以及单据可下推为哪些目标单据。
/// 表头无 FBILLNO：单据编号 = FNUMBER（系统生成，yyyyMMdd+4位日流水）。注意 FNUMBER 在库上无唯一约束（唯一索引在 FINTERID），见取号说明。
/// 源单类型 FSOURCETRANTYPE / 目标单据类型 FDESTTRANTYPE 存 SYS_BILLTEMPLATE.FNUMBER，按 FNUMBER 解析名称。
/// 明细 FSOURCEID / FDESTID 存 T_BAS_BILLTYPE.Uid，按 Uid 解析编号/名称。
/// 状态 FSTATUS：10=草稿(未审)、40=审核(已审)、70=关闭；禁用走 FDisabled。
/// </summary>
public class SelBillService : DocumentService<TBosSelbill, TBosSelbillentry,
    SelBillListDto, SelBillDetailDto, CreateSelBillRequest, UpdateSelBillRequest>, ISelBillService
{
    public SelBillService(
        IRepository<TBosSelbill> headerRepo,
        IRepository<TBosSelbillentry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog)
    {
    }

    protected override string PrgKey => "SelBill";

    // ===== 单据编号 FNUMBER 生成（无 FBILLNO 列；yyyyMMdd + 4位日流水）=====
    // 重要：T_BOS_SELBILL 的 FNUMBER 在生产与开发库均无唯一约束（唯一索引建在 FINTERID 上），
    // 因此不能依赖数据库拦截重号。这里按当天前缀取最大流水 +1 生成，并对已存在编号做应用层去重自检。
    // 仅作展示编号、且配置页为低频写入：两个并发事务在各自提交前都读到相同 max 的极小窗口仍可能撞号，
    // 但 FNUMBER 不参与业务关联（外键走 Uid/FInterId），重号不致数据错乱，可由用户改名或重存修正。
    protected override async Task PrepareHeaderForCreateAsync(TBosSelbill header, CreateSelBillRequest dto)
    {
        if (!string.IsNullOrWhiteSpace(header.Fnumber)) return; // 尊重手工号
        var prefix = DateTime.Now.ToString("yyyyMMdd");
        var sameDay = await Db.Queryable<TBosSelbill>()
            .Where(s => s.Fnumber.StartsWith(prefix))
            .Select(s => s.Fnumber).ToListAsync();
        var used = new HashSet<string>(sameDay, StringComparer.OrdinalIgnoreCase);
        int max = 0;
        foreach (var n in sameDay)
        {
            if (n.Length > prefix.Length && int.TryParse(n.Substring(prefix.Length), out var seq) && seq > max)
                max = seq;
        }
        // 从 max+1 起递增，跳过已占用编号（兜住历史空洞与同进程并发）
        int next = max + 1;
        string candidate;
        do { candidate = prefix + (next++).ToString("D4"); } while (used.Contains(candidate));
        header.Fnumber = candidate;
    }

    // ===== 审核 / 反审核 / 关闭 =====

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("单据已审核，无需重复审核");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭，不能审核");

        header.FStatus = 40;
        header.Fcheckerid = CurrentUser.UserId ?? string.Empty;
        header.Fcheckdate = DateTime.Now;
        header.MYmd = DateTime.Now;
        header.MUser = CurrentUser.UserId ?? string.Empty;
        var result = await Db.Updateable(header)
            .UpdateColumns(h => new { h.FStatus, h.Fcheckerid, h.Fcheckdate, h.MYmd, h.MUser })
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Approve, uid, header.Fnumber, "审核流程配置", result);
        return result;
    }

    /// <summary>反审核（回到草稿 10）。审核日期回退用 1900 哨兵（前端按 &lt;=1900 显示为空）。</summary>
    public override async Task<bool> RejectAsync(string uid, string? reason = null)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能反审核");

        header.FStatus = 10;
        header.Fcheckerid = string.Empty;
        header.Fcheckdate = new DateTime(1900, 1, 1);
        header.MYmd = DateTime.Now;
        header.MUser = CurrentUser.UserId ?? string.Empty;
        var result = await Db.Updateable(header)
            .UpdateColumns(h => new { h.FStatus, h.Fcheckerid, h.Fcheckdate, h.MYmd, h.MUser })
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Reject, uid, header.Fnumber, reason ?? "反审核流程配置", result);
        return result;
    }

    public override async Task<bool> CloseAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能关闭");

        header.FStatus = 70;
        header.MYmd = DateTime.Now;
        header.MUser = CurrentUser.UserId ?? string.Empty;
        var result = await Db.Updateable(header)
            .UpdateColumns(h => new { h.FStatus, h.MYmd, h.MUser })
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fnumber, "关闭流程配置", result);
        return result;
    }

    // ===== 禁用 / 反禁用 =====

    public async Task<bool> DisableAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FDisabled) throw new InvalidOperationException("单据已禁用");

        header.FDisabled = true;
        header.Fdisableid = CurrentUser.UserId ?? string.Empty;
        header.Fdisabledate = DateTime.Now;
        header.MYmd = DateTime.Now;
        header.MUser = CurrentUser.UserId ?? string.Empty;
        var result = await Db.Updateable(header)
            .UpdateColumns(h => new { h.FDisabled, h.Fdisableid, h.Fdisabledate, h.MYmd, h.MUser })
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Disable, uid, header.Fnumber, "禁用流程配置", result);
        return result;
    }

    public async Task<bool> EnableAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (!header.FDisabled) throw new InvalidOperationException("单据未禁用");

        header.FDisabled = false;
        header.Fdisableid = string.Empty;
        header.Fdisabledate = new DateTime(1900, 1, 1);
        header.MYmd = DateTime.Now;
        header.MUser = CurrentUser.UserId ?? string.Empty;
        var result = await Db.Updateable(header)
            .UpdateColumns(h => new { h.FDisabled, h.Fdisableid, h.Fdisabledate, h.MYmd, h.MUser })
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Enable, uid, header.Fnumber, "反禁用流程配置", result);
        return result;
    }

    // ===== 下推：按源单类型查可下推目标（出入库流程配置反向查询）=====

    /// <summary>
    /// 反向于 InStockService/PurchaseReturnService 的 GetSourceBillTypesAsync：
    /// 那两处按 Fdesttrantype==目标 取 Fsourcetrantype（目标单视角看可选源单）；
    /// 这里按 Fsourcetrantype==源单 取 Fdesttrantype（源单视角看可下推目标）。
    /// 仅取启用(Fisuse)、未禁用、未删除的流程；排除空目标；按 Fdefault 优先排序。
    /// </summary>
    public async Task<List<PushDownTargetDto>> GetPushDownTargetsAsync(string sourceTranType)
    {
        if (string.IsNullOrWhiteSpace(sourceTranType)) return new();

        var rows = await Db.Queryable<TBosSelbill>()
            .Where(s => s.Fsourcetrantype == sourceTranType && s.Fisuse && !s.FDisabled && !s.FDeleted)
            .OrderBy(s => s.Fdefault, OrderByType.Desc)
            // 次级排序键：同一源单映射到多个目标且 Fdefault 均为 true 时（如采购订单同时可下推入库单/退料单），
            // 单凭 Fdefault 排序非唯一，平级行在 SqlServer/PG 下顺序未定义；按 Fdesttrantype 兜底保证返回顺序确定，
            // 使前端 PushDownDialog 默认选中项稳定。
            .OrderBy(s => s.Fdesttrantype)
            .Select(s => s.Fdesttrantype)
            .ToListAsync();

        var dests = rows.Where(d => !string.IsNullOrEmpty(d)).Distinct().ToList();
        if (dests.Count == 0) return new();

        var tmplDict = await LoadBillTemplateDictAsync(dests);
        return dests.Select(d => new PushDownTargetDto
        {
            Value = d,
            Label = tmplDict.GetValueOrDefault(d, d)
        }).ToList();
    }

    // 已审核/已关闭单据禁止修改/删除（前端只读外的后端兜底，须先反审核）
    public override async Task<bool> UpdateAsync(string uid, UpdateSelBillRequest request)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的单据不能修改，请先反审核");
        if (header.FStatus == 70) throw new InvalidOperationException("已关闭的单据不能修改");
        return await base.UpdateAsync(uid, request);
    }

    public override async Task<bool> DeleteAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的单据不能删除，请先反审核");
        if (header.FStatus == 70) throw new InvalidOperationException("已关闭的单据不能删除");
        return await base.DeleteAsync(uid);
    }

    protected override Expression<Func<TBosSelbill, bool>> BuildSearchPredicate(string keyword)
        => h => h.Fnumber.Contains(keyword) || h.Fname.Contains(keyword);

    // ===== 列表：按主表一行 + 名称解析 =====

    public override async Task<PagedResult<SelBillListDto>> GetPagedListAsync(PagedRequest request)
    {
        var filters = request.DynamicFilters ?? new List<DynamicFilterInfo>();

        var query = Db.Queryable<TBosSelbill>().Where(h => !h.FDeleted);
        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var kw = request.Keyword.Trim();
            query = query.Where(h => h.Fnumber.Contains(kw) || h.Fname.Contains(kw));
        }
        if (filters.Count > 0)
            query = query.Where(filters.ToConditionalModels<TBosSelbill>());

        RefAsync<int> totalCount = 0;
        var headers = await query
            .OrderBy(h => h.CYmd, OrderByType.Desc)
            .OrderBy(h => h.Fnumber)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        if (headers.Count == 0)
            return new PagedResult<SelBillListDto> { Items = new(), TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };

        var tmplDict = await LoadBillTemplateDictAsync(headers.Select(h => h.Fsourcetrantype).Concat(headers.Select(h => h.Fdesttrantype)));
        var userDict = await LoadUserNameDictAsync(headers.Select(h => h.CUser).Concat(headers.Select(h => h.Fcheckerid)).Concat(headers.Select(h => h.Fdisableid)));
        var statusDict = await LoadStatusDictAsync();

        var items = headers.Select(h => new SelBillListDto
        {
            Uid = h.Uid,
            Fnumber = h.Fnumber,
            Fname = h.Fname,
            Fsourcetrantype = h.Fsourcetrantype,
            FsourcetypeName = tmplDict.GetValueOrDefault(h.Fsourcetrantype, string.Empty),
            Fdesttrantype = h.Fdesttrantype,
            FdesttranName = tmplDict.GetValueOrDefault(h.Fdesttrantype, string.Empty),
            Fiscontrolqty = h.Fiscontrolqty,
            Fisopensource = h.Fisopensource,
            Fdefault = h.Fdefault,
            Fisuse = h.Fisuse,
            Fcheck = h.Fcheck,
            Fisdefaultstock = h.Fisdefaultstock,
            FStatus = h.FStatus,
            FstatusName = statusDict.GetValueOrDefault(h.FStatus, string.Empty),
            FDisabled = h.FDisabled,
            CuserName = userDict.GetValueOrDefault(h.CUser, string.Empty),
            CYmd = h.CYmd,
            FcheckerName = userDict.GetValueOrDefault(h.Fcheckerid, string.Empty),
            Fcheckdate = h.Fcheckdate,
            FdisableName = userDict.GetValueOrDefault(h.Fdisableid, string.Empty),
            Fdisabledate = h.Fdisabledate
        }).ToList();

        return new PagedResult<SelBillListDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    // 抽象成员（列表走上面的重写，此处为契约的简化实现）
    protected override SelBillListDto MapToListDto(TBosSelbill entity) => new()
    {
        Uid = entity.Uid,
        Fnumber = entity.Fnumber,
        Fname = entity.Fname,
        FStatus = entity.FStatus
    };

    // ===== 详情：主表 + 名称解析 + 明细 =====

    public override async Task<SelBillDetailDto?> GetByIdAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return null;

        var entries = await GetEntriesByHeaderIdAsync(uid);
        var dto = MapToDetailDto(header, entries);

        // 源单类型 / 目标单据类型名（SYS_BILLTEMPLATE.FNUMBER -> FNAME）
        var tmplDict = await LoadBillTemplateDictAsync(new[] { header.Fsourcetrantype, header.Fdesttrantype });
        dto.FsourcetypeName = tmplDict.GetValueOrDefault(header.Fsourcetrantype, string.Empty);
        dto.FdesttranName = tmplDict.GetValueOrDefault(header.Fdesttrantype, string.Empty);

        // 状态名
        dto.FstatusName = (await LoadStatusDictAsync()).GetValueOrDefault(header.FStatus, string.Empty);

        // 制单/修改/审核/禁用人名（取自登录用户）
        var userDict = await LoadUserNameDictAsync(new[] { header.CUser, header.MUser, header.Fcheckerid, header.Fdisableid });
        dto.CuserName = userDict.GetValueOrDefault(header.CUser, string.Empty);
        dto.MuserName = userDict.GetValueOrDefault(header.MUser, string.Empty);
        dto.FcheckerName = userDict.GetValueOrDefault(header.Fcheckerid, string.Empty);
        dto.FdisableName = userDict.GetValueOrDefault(header.Fdisableid, string.Empty);

        // 明细：源单/目标单据类型编号+名称（T_BAS_BILLTYPE.Uid -> FNUMBER/FNAME）
        if (dto.Entries.Count > 0)
        {
            var btDict = await LoadBillTypeDictAsync(entries.Select(e => e.Fsourceid).Concat(entries.Select(e => e.Fdestid)));
            foreach (var line in dto.Entries)
            {
                if (btDict.TryGetValue(line.Fsourceid ?? string.Empty, out var src)) { line.FsourceNumber = src.Number; line.FsourceName = src.Name; }
                if (btDict.TryGetValue(line.Fdestid ?? string.Empty, out var dst)) { line.FdestNumber = dst.Number; line.FdestName = dst.Name; }
            }
        }

        return dto;
    }

    protected override SelBillDetailDto MapToDetailDto(TBosSelbill header, List<TBosSelbillentry> entries) => new()
    {
        Uid = header.Uid,
        Fnumber = header.Fnumber,
        Fname = header.Fname,
        Fsourcetrantype = header.Fsourcetrantype,
        Fdesttrantype = header.Fdesttrantype,
        Fisuse = header.Fisuse,
        Fdefault = header.Fdefault,
        Fisopensource = header.Fisopensource,
        Fcheck = header.Fcheck,
        Fiscontrolqty = header.Fiscontrolqty,
        Fisdefaultstock = header.Fisdefaultstock,
        Fcansynerp = header.Fcansynerp,
        Fcheckerpstock = header.Fcheckerpstock,
        Fischecklot = header.Fischecklot,
        Fischeckaux = header.Fischeckaux,
        Fischeckkfdate = header.Fischeckkfdate,
        Fisusecoderule = header.Fisusecoderule,
        Fcanpushdown = header.Fcanpushdown,
        Fiswholeout = header.Fiswholeout,
        Fkind = header.Fkind,
        Fproname = header.Fproname,
        FStatus = header.FStatus,
        FDisabled = header.FDisabled,
        CUser = header.CUser,
        CYmd = header.CYmd,
        MUser = header.MUser,
        MYmd = header.MYmd,
        Fcheckerid = header.Fcheckerid,
        Fcheckdate = header.Fcheckdate,
        Fdisableid = header.Fdisableid,
        Fdisabledate = header.Fdisabledate,
        Entries = entries.Select(e => new SelBillEntryDto
        {
            Uid = e.Uid,
            Fentryid = e.Fentryid,
            Fsourceid = e.Fsourceid,
            Fdestid = e.Fdestid,
            Fdefault = e.Fdefault
        }).ToList()
    };

    // ===== 写入映射 =====

    protected override TBosSelbill MapToHeaderEntity(CreateSelBillRequest dto) => new()
    {
        Fnumber = dto.Fnumber?.Trim() ?? string.Empty, // 为空时由 PrepareHeaderForCreateAsync 生成
        Fname = dto.Fname?.Trim() ?? string.Empty,
        Fsourcetrantype = dto.Fsourcetrantype,
        Fdesttrantype = dto.Fdesttrantype,
        Fisuse = dto.Fisuse,
        Fdefault = dto.Fdefault,
        Fisopensource = dto.Fisopensource,
        Fcheck = dto.Fcheck,
        Fiscontrolqty = dto.Fiscontrolqty,
        Fisdefaultstock = dto.Fisdefaultstock,
        Fcansynerp = dto.Fcansynerp,
        Fcheckerpstock = dto.Fcheckerpstock,
        Fischecklot = dto.Fischecklot,
        Fischeckaux = dto.Fischeckaux,
        Fischeckkfdate = dto.Fischeckkfdate,
        Fisusecoderule = dto.Fisusecoderule,
        Fcanpushdown = dto.Fcanpushdown,
        Fiswholeout = dto.Fiswholeout,
        Fkind = dto.Fkind ?? string.Empty,
        Fproname = dto.Fproname ?? string.Empty,
        // 审核/禁用日期用 1900 哨兵：满足开发库 NOT NULL，且落在生产 SQLServer DATETIME 下限(1753)之上；
        // 前端按 <=1900 显示为空。与现有 T_BOS_SELBILL 种子及兄弟单据一致。
        Fcheckdate = new DateTime(1900, 1, 1),
        Fdisabledate = new DateTime(1900, 1, 1),
        FStatus = 10
    };

    protected override void UpdateHeaderEntity(TBosSelbill entity, UpdateSelBillRequest dto)
    {
        entity.Fname = dto.Fname?.Trim() ?? string.Empty;
        entity.Fsourcetrantype = dto.Fsourcetrantype;
        entity.Fdesttrantype = dto.Fdesttrantype;
        entity.Fisuse = dto.Fisuse;
        entity.Fdefault = dto.Fdefault;
        entity.Fisopensource = dto.Fisopensource;
        entity.Fcheck = dto.Fcheck;
        entity.Fiscontrolqty = dto.Fiscontrolqty;
        entity.Fisdefaultstock = dto.Fisdefaultstock;
        entity.Fcansynerp = dto.Fcansynerp;
        entity.Fcheckerpstock = dto.Fcheckerpstock;
        entity.Fischecklot = dto.Fischecklot;
        entity.Fischeckaux = dto.Fischeckaux;
        entity.Fischeckkfdate = dto.Fischeckkfdate;
        entity.Fisusecoderule = dto.Fisusecoderule;
        entity.Fproname = dto.Fproname ?? string.Empty;
        // 注意：Fcanpushdown / Fiswholeout / Fkind 不由本维护页维护，此处不覆盖——
        // entity 已从库内加载，保留其原值，避免被前端缺省值（false/空）静默清空（如 ERP 同步写入的条码类型）。
    }

    protected override List<TBosSelbillentry> MapToEntryEntities(CreateSelBillRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    protected override List<TBosSelbillentry> MapToEntryEntities(UpdateSelBillRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    private static List<TBosSelbillentry> MapEntries(List<CreateSelBillEntryRequest> entries, string headerUid)
        => entries
            .Where(e => !string.IsNullOrEmpty(e.Fsourceid) || !string.IsNullOrEmpty(e.Fdestid))
            .Select(e => new TBosSelbillentry
            {
                FInterId = headerUid,
                Fsourceid = e.Fsourceid ?? string.Empty,
                Fdestid = e.Fdestid ?? string.Empty,
                Fdefault = e.Fdefault
            }).ToList();

    protected override void SetEntryIndex(TBosSelbillentry entry, int index)
    {
        entry.Fentryid = index;
        entry.Fdetailid = entry.Uid;
    }

    protected override async Task<List<TBosSelbillentry>> GetEntriesByHeaderIdAsync(string headerUid)
        => await Db.Queryable<TBosSelbillentry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.Fentryid)
            .ToListAsync();

    // ===== 名称字典加载 =====

    /// <summary>SYS_BILLTEMPLATE：FNUMBER -> FNAME（源单类型/目标单据类型按编码解析名称）</summary>
    private async Task<Dictionary<string, string>> LoadBillTemplateDictAsync(IEnumerable<string> numbers)
    {
        var list = numbers.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysBillTemplate>().Where(t => list.Contains(t.Fnumber))
            .Select(t => new { t.Fnumber, t.Fname }).ToListAsync();
        return rows.Where(r => !string.IsNullOrEmpty(r.Fnumber)).GroupBy(r => r.Fnumber).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    /// <summary>T_BAS_BILLTYPE：Uid -> (FNUMBER, FNAME)（明细源单/目标编号、名称）</summary>
    private async Task<Dictionary<string, (string Number, string Name)>> LoadBillTypeDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBasBilltype>().Where(b => list.Contains(b.Uid))
            .Select(b => new { b.Uid, b.Fnumber, b.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().Fnumber, g.First().Fname));
    }

    private async Task<Dictionary<string, string>> LoadUserNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysLoginUser>().Where(u => list.Contains(u.UserId))
            .Select(u => new { u.UserId, u.UserName }).ToListAsync();
        return rows.GroupBy(r => r.UserId).ToDictionary(g => g.Key, g => g.First().UserName);
    }

    private async Task<Dictionary<int, string>> LoadStatusDictAsync()
    {
        var rows = await Db.Queryable<SysStatus>().Select(s => new { s.Fitemid, s.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Fitemid).ToDictionary(g => g.Key, g => g.First().Fname);
    }
}
