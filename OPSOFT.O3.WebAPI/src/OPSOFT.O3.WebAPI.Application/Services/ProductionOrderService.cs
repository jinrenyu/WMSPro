using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using OPSOFT.O3.WebAPI.Application.Extensions;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 生产订单 / 生产任务单服务（真实表 T_PRD_MO / T_PRD_MOENTRY）。
/// 列表按明细行展开并解析名称；详情解析主表全部外键名称。
/// 状态：10=草稿(未审)、40=审核(已审)、70=关闭。沿用采购订单单据范式。
/// </summary>
public class ProductionOrderService : DocumentService<TPrdMo, TPrdMoentry,
    ProductionOrderListDto, ProductionOrderDetailDto, CreateProductionOrderRequest, UpdateProductionOrderRequest>
{
    public ProductionOrderService(
        IRepository<TPrdMo> headerRepo,
        IRepository<TPrdMoentry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IBillCodeService billCode,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog, billCode)
    {
    }

    protected override string PrgKey => "ProductionOrder";

    // 1900 哨兵：满足开发库 SQLite 的 NOT NULL 日期列，且生产 SQLServer DATETIME(下限1753)安全；前端按 <=1900 视为空
    private static readonly DateTime DateSentinel = new(1900, 1, 1);

    /// <summary>业务状态名：1计划/2计划确认/3下达/4开工/5完工/6结案/7结算</summary>
    private static string BStatusName(string s) => s switch
    {
        "1" => "计划", "2" => "计划确认", "3" => "下达", "4" => "开工",
        "5" => "完工", "6" => "结案", "7" => "结算", _ => string.Empty
    };

    // ===== 审核 / 反审核 / 关闭 =====

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("单据已审核，无需重复审核");

        bool result;
        try
        {
            Db.AsTenant().BeginTran();

            result = await Db.Updateable<TPrdMo>()
                .SetColumns(h => h.FStatus == 40)
                .SetColumns(h => h.Fcheckerid == (CurrentUser.UserId ?? string.Empty))
                .SetColumns(h => h.Fcheckdate == DateTime.Now)
                .SetColumns(h => h.MYmd == DateTime.Now)
                .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
                .Where(h => h.Uid == uid)
                .ExecuteCommandAsync() > 0;

            // 审核=计划确认：把仍为"计划(1)"的明细业务状态置为"计划确认(2)"（对照旧系统 Verify 时 FBSTATUS=2），供后续下达
            await Db.Updateable<TPrdMoentry>()
                .SetColumns(e => e.Fbstatus == "2")
                .SetColumns(e => e.MYmd == DateTime.Now)
                .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
                .Where(e => e.FInterId == uid && !e.FDeleted && (e.Fbstatus == "1" || e.Fbstatus == ""))
                .ExecuteCommandAsync();

            Db.AsTenant().CommitTran();
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Approve, uid, header.Fbillno, "审核单据", result);
        return result;
    }

    /// <summary>反审核（回到草稿 10）</summary>
    public override async Task<bool> RejectAsync(string uid, string? reason = null)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能反审核");

        // 拦截：存在已下达/已开工/已完工/已结案的明细行时不能反审核（否则反审核→编辑会重建明细 Uid，
        // 令已生成的用料清单 FMOENTRYID 孤立）。须先逐行反下达。
        var hasReleased = await Db.Queryable<TPrdMoentry>()
            .AnyAsync(e => e.FInterId == uid && !e.FDeleted
                && (e.Fbstatus == "3" || e.Fbstatus == "4" || e.Fbstatus == "5" || e.Fbstatus == "6"));
        if (hasReleased) throw new InvalidOperationException("存在已下达的明细行，请先逐行反下达后再反审核");

        // 哨兵存入局部变量：SqlSugar 解析 SetColumns 表达式树时拒绝 private 字段，
        // 局部变量经闭包捕获（字段为 public）可被正常求值为参数化常量。
        var sentinel = DateSentinel;
        bool result;
        try
        {
            Db.AsTenant().BeginTran();

            result = await Db.Updateable<TPrdMo>()
                .SetColumns(h => h.FStatus == 10)
                .SetColumns(h => h.Fcheckerid == string.Empty)
                .SetColumns(h => h.Fcheckdate == sentinel)
                .SetColumns(h => h.MYmd == DateTime.Now)
                .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
                .Where(h => h.Uid == uid)
                .ExecuteCommandAsync() > 0;

            // 反审核回到草稿：把"计划确认(2)"的明细退回"计划(1)"
            await Db.Updateable<TPrdMoentry>()
                .SetColumns(e => e.Fbstatus == "1")
                .SetColumns(e => e.MYmd == DateTime.Now)
                .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
                .Where(e => e.FInterId == uid && !e.FDeleted && e.Fbstatus == "2")
                .ExecuteCommandAsync();

            Db.AsTenant().CommitTran();
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Reject, uid, header.Fbillno, reason ?? "反审核单据", result);
        return result;
    }

    public override async Task<bool> CloseAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能关闭");

        // 拦截：存在已下达及以上的明细行时不能关闭（与反审核同口径）。否则关闭(70)后若再编辑会重建明细 Uid，
        // 令已生成的用料清单 FMOENTRYID 孤立（见 RejectAsync 同款拦截 + UpdateAsync/DeleteAsync 仅草稿可改的兜底）。
        var hasReleased = await Db.Queryable<TPrdMoentry>()
            .AnyAsync(e => e.FInterId == uid && !e.FDeleted
                && (e.Fbstatus == "3" || e.Fbstatus == "4" || e.Fbstatus == "5" || e.Fbstatus == "6"));
        if (hasReleased) throw new InvalidOperationException("存在已下达的明细行，请先逐行反下达后再关闭");

        var result = await Db.Updateable<TPrdMo>()
            .SetColumns(h => h.FStatus == 70)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<TPrdMo, bool>> BuildSearchPredicate(string keyword)
        => h => h.Fbillno.Contains(keyword);

    // 非草稿单据禁止修改/删除（前端只读外的后端兜底）。改/删会经基类删插重建明细 Uid，
    // 故已审核(40)与已关闭(70)都必须拦死，否则会令下达生成的用料清单 FMOENTRYID 孤立。
    public override async Task<bool> UpdateAsync(string uid, UpdateProductionOrderRequest request)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus != 10) throw new InvalidOperationException("只有草稿状态的单据才能修改，请先反审核");
        return await base.UpdateAsync(uid, request);
    }

    public override async Task<bool> DeleteAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus != 10) throw new InvalidOperationException("只有草稿状态的单据才能删除，请先反审核");
        return await base.DeleteAsync(uid);
    }

    // ===== 列表：按明细行展开 + 名称解析 =====

    private static readonly HashSet<string> HeaderFilterFields = new(StringComparer.OrdinalIgnoreCase) { "fbillno", "fdate", "fStatus" };

    public override async Task<PagedResult<ProductionOrderListDto>> GetPagedListAsync(PagedRequest request)
    {
        var filters = request.DynamicFilters ?? new List<DynamicFilterInfo>();
        var headerFilters = filters.Where(f => HeaderFilterFields.Contains(f.Field)).ToList();
        var entryFilters = filters.Where(f => !HeaderFilterFields.Contains(f.Field)).ToList();

        // 1) 表头条件 -> 命中的主表 Uid 集合
        List<string>? headerIds = null;
        if (!string.IsNullOrWhiteSpace(request.Keyword) || headerFilters.Count > 0 || request.OnlyApproved)
        {
            var hq = Db.Queryable<TPrdMo>().Where(h => !h.FDeleted);
            if (request.OnlyApproved)
                hq = hq.Where(h => h.FStatus == 40 && !h.FDisabled);
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var kw = request.Keyword.Trim();
                hq = hq.Where(h => h.Fbillno.Contains(kw));
            }
            if (headerFilters.Count > 0)
                hq = hq.Where(headerFilters.ToConditionalModels<TPrdMo>());
            headerIds = await hq.Select(h => h.Uid).ToListAsync();
            if (headerIds.Count == 0)
                return new PagedResult<ProductionOrderListDto> { Items = new(), TotalCount = 0, PageIndex = request.PageIndex, PageSize = request.PageSize };
        }

        // 2) 分页查询明细
        RefAsync<int> totalCount = 0;
        var query = Db.Queryable<TPrdMoentry>().Where(e => !e.FDeleted);
        if (headerIds != null)
            query = query.Where(e => headerIds.Contains(e.FInterId));
        if (entryFilters.Count > 0)
            query = query.Where(entryFilters.ToConditionalModels<TPrdMoentry>());
        var entries = await query
            .OrderBy(e => e.CYmd, OrderByType.Desc)
            .OrderBy(e => e.Fentryid)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        if (entries.Count == 0)
            return new PagedResult<ProductionOrderListDto> { Items = new(), TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };

        // 3) 批量加载主表 + 名称源
        var hids = entries.Select(e => e.FInterId).Distinct().ToList();
        var headers = await Db.Queryable<TPrdMo>().Where(h => hids.Contains(h.Uid)).ToListAsync();
        var headerDict = headers.GroupBy(h => h.Uid).ToDictionary(g => g.Key, g => g.First());

        var materialDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
        var unitDict = await LoadUnitDictAsync(entries.Select(e => e.Fbaseunitid));
        var deptDict = await LoadDeptDictAsync(entries.Select(e => e.Fworkshopid));
        var routeDict = await LoadProRouteDictAsync(entries.Select(e => e.Fprorouteid));
        var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));
        var plannerDict = await LoadEmployeeNameDictAsync(headers.Select(h => h.Fplannerid));
        var statusDict = await LoadStatusDictAsync();
        var companyDict = await LoadOrgDictAsync(headers.Select(h => h.FCompanyId));
        var userDict = await LoadUserNameDictAsync(headers.SelectMany(h => new[] { h.CUser, h.MUser, h.Fcheckerid }));
        var auxEnabled = await LoadAuxEnabledSetAsync(entries.Select(e => e.Fmaterialid));

        // 4) 映射
        var items = entries.Select(e =>
        {
            headerDict.TryGetValue(e.FInterId, out var h);
            materialDict.TryGetValue(e.Fmaterialid ?? string.Empty, out var mat);
            var dto = new ProductionOrderListDto
            {
                Uid = h?.Uid ?? e.FInterId,
                EntryUid = e.Uid,
                Fbillno = h?.Fbillno ?? string.Empty,
                Fdate = h?.Fdate,
                Fentryid = e.Fentryid,
                FStatus = h?.FStatus ?? 0,
                FstatusName = statusDict.GetValueOrDefault(h?.FStatus ?? 0, string.Empty),
                Fworkshopid = e.Fworkshopid,
                FworkshopNumber = deptDict.GetValueOrDefault(e.Fworkshopid ?? string.Empty, default).Number,
                FworkshopName = deptDict.GetValueOrDefault(e.Fworkshopid ?? string.Empty, default).Name,
                Fmaterialid = e.Fmaterialid,
                FmaterialNumber = mat.Number,
                FmaterialName = mat.Name,
                FchartNumber = mat.Chart,
                FSpecification = mat.Spec,
                Fproducttype = e.Fproducttype,
                Fprorouteid = e.Fprorouteid,
                FprorouteName = routeDict.GetValueOrDefault(e.Fprorouteid ?? string.Empty, default).Name,
                Flot = e.Flot,
                FbaseunitName = unitDict.GetValueOrDefault(e.Fbaseunitid ?? string.Empty, default).Name,
                Fqty = e.Fqty,
                Ffcqty = e.Ffcqty,
                Funfcqty = e.Fqty - e.Ffcqty,
                Fbaseunitqty = e.Fbaseunitqty,
                Fmachinemodel = e.Fmachinemodel,
                Fbstatus = e.Fbstatus,
                FbstatusName = BStatusName(e.Fbstatus ?? string.Empty),
                Fschedulestatus = e.Fschedulestatus,
                Fissuspend = e.Fissuspend,
                FisAuxEnabled = !string.IsNullOrEmpty(e.Fmaterialid) && auxEnabled.Contains(e.Fmaterialid),
                FauxpropName = flexDict.GetValueOrDefault(e.Fauxpropid ?? string.Empty, string.Empty),
                Finhighlimit = e.Finhighlimit,
                Fplanstartdate = e.Fplanstartdate,
                Fplanfinishdate = e.Fplanfinishdate,
                Factualstartdate = e.Factualstartdate,
                Factualfinishdate = e.Factualfinishdate,
                FplannerName = plannerDict.GetValueOrDefault(h?.Fplannerid ?? string.Empty, string.Empty),
                CuserName = userDict.GetValueOrDefault(h?.CUser ?? string.Empty, string.Empty),
                CYmd = h?.CYmd,
                MuserName = userDict.GetValueOrDefault(h?.MUser ?? string.Empty, string.Empty),
                MYmd = h?.MYmd,
                FcheckerName = userDict.GetValueOrDefault(h?.Fcheckerid ?? string.Empty, string.Empty),
                Fcheckdate = h?.Fcheckdate,
                FDisabled = h?.FDisabled ?? false,
                FcompanyName = companyDict.GetValueOrDefault(h?.FCompanyId ?? string.Empty, string.Empty)
            };
            return dto;
        }).ToList();

        return new PagedResult<ProductionOrderListDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    protected override ProductionOrderListDto MapToListDto(TPrdMo entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdate = entity.Fdate,
        FStatus = entity.FStatus
    };

    // ===== 详情：主表 + 名称解析 + 明细 =====

    public override async Task<ProductionOrderDetailDto?> GetByIdAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return null;

        var entries = await GetEntriesByHeaderIdAsync(uid);
        var dto = MapToDetailDto(header, entries);

        // 主表名称
        dto.FbilltypeName = await Db.Queryable<TBasBilltype>().Where(b => b.Uid == header.Fbilltype).Select(b => b.Fname).FirstAsync() ?? string.Empty;
        dto.FcompanyName = await Db.Queryable<SysOrgStructure>().Where(o => o.Uid == header.FCompanyId).Select(o => o.Fname).FirstAsync() ?? string.Empty;
        dto.FplannerName = (await LoadEmployeeNameDictAsync(new[] { header.Fplannerid })).GetValueOrDefault(header.Fplannerid, string.Empty);
        dto.FstatusName = (await LoadStatusDictAsync()).GetValueOrDefault(header.FStatus, string.Empty);

        var userIds = new[] { header.CUser, header.MUser, header.Fcheckerid, header.Fdisableid };
        var userDict = await LoadUserNameDictAsync(userIds);
        dto.CuserName = userDict.GetValueOrDefault(header.CUser, string.Empty);
        dto.MuserName = userDict.GetValueOrDefault(header.MUser, string.Empty);
        dto.FcheckerName = userDict.GetValueOrDefault(header.Fcheckerid, string.Empty);
        dto.FdisableName = userDict.GetValueOrDefault(header.Fdisableid, string.Empty);

        // 明细名称
        if (dto.Entries.Count > 0)
        {
            var matDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
            var unitDict = await LoadUnitDictAsync(entries.SelectMany(e => new[] { e.Fbaseunitid, e.Fcommonunitid }));
            var deptDict = await LoadDeptDictAsync(entries.Select(e => e.Fworkshopid));
            var routeDict = await LoadProRouteDictAsync(entries.Select(e => e.Fprorouteid));
            var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));
            var bomDict = await LoadBomDictAsync(entries.Select(e => e.Fbomid));
            var barcodeDict = await LoadBarcodeDictAsync(entries.Select(e => e.Fbarcoderuleid));
            var finalDict = await LoadUserNameDictAsync(entries.Select(e => e.Ffinalid));
            var auxEnabled = await LoadAuxEnabledSetAsync(entries.Select(e => e.Fmaterialid));

            foreach (var line in dto.Entries)
            {
                if (matDict.TryGetValue(line.Fmaterialid ?? string.Empty, out var m))
                {
                    line.FmaterialNumber = m.Number; line.FmaterialName = m.Name;
                    line.FchartNumber = m.Chart; line.FSpecification = m.Spec; line.FisBatchManage = m.Batch;
                }
                if (deptDict.TryGetValue(line.Fworkshopid ?? string.Empty, out var d))
                { line.FworkshopNumber = d.Number; line.FworkshopName = d.Name; }
                if (unitDict.TryGetValue(line.Fbaseunitid ?? string.Empty, out var bu))
                { line.FbaseunitNumber = bu.Number; line.FbaseunitName = bu.Name; }
                if (unitDict.TryGetValue(line.Fcommonunitid ?? string.Empty, out var cu))
                { line.FcommonunitNumber = cu.Number; line.FcommonunitName = cu.Name; }
                if (routeDict.TryGetValue(line.Fprorouteid ?? string.Empty, out var r))
                { line.FprorouteNumber = r.Number; line.FprorouteName = r.Name; }
                line.FbomBillno = bomDict.GetValueOrDefault(line.Fbomid ?? string.Empty, string.Empty);
                line.FbarcoderuleName = barcodeDict.GetValueOrDefault(line.Fbarcoderuleid ?? string.Empty, string.Empty);
                line.FauxpropName = flexDict.GetValueOrDefault(line.Fauxpropid ?? string.Empty, string.Empty);
                line.FfinalName = finalDict.GetValueOrDefault(line.Ffinalid ?? string.Empty, string.Empty);
                line.FbstatusName = BStatusName(line.Fbstatus ?? string.Empty);
                line.FisAuxEnabled = !string.IsNullOrEmpty(line.Fmaterialid) && auxEnabled.Contains(line.Fmaterialid);
            }
        }

        return dto;
    }

    protected override ProductionOrderDetailDto MapToDetailDto(TPrdMo header, List<TPrdMoentry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fbilltype = header.Fbilltype,
        Fdate = header.Fdate,
        FStatus = header.FStatus,
        Foastatus = header.Foastatus,
        Foaresult = header.Foaresult,
        Fcompanyid = header.FCompanyId,
        Fplannerid = header.Fplannerid,
        Fnote = header.Fnote,
        CUser = header.CUser,
        CYmd = header.CYmd,
        Fcheckerid = header.Fcheckerid,
        Fcheckdate = header.Fcheckdate,
        MUser = header.MUser,
        MYmd = header.MYmd,
        Fdisableid = header.Fdisableid,
        Fdisabledate = header.Fdisabledate,
        FDisabled = header.FDisabled,
        Entries = entries.Select(e => new ProductionOrderEntryDto
        {
            Uid = e.Uid,
            Fentryid = e.Fentryid,
            Fproducttype = string.IsNullOrEmpty(e.Fproducttype) ? "1" : e.Fproducttype,
            Fworkshopid = e.Fworkshopid,
            Fmaterialid = e.Fmaterialid,
            Fmachinemodel = e.Fmachinemodel,
            Flot = e.Flot,
            Fbaseunitid = e.Fbaseunitid,
            Fbaseunitqty = e.Fbaseunitqty,
            Fcommonunitid = e.Fcommonunitid,
            Fqty = e.Fqty,
            Fauxpropid = e.Fauxpropid,
            Fprorouteid = e.Fprorouteid,
            Fbarcoderuleid = e.Fbarcoderuleid,
            Ffcqty = e.Ffcqty,
            Ftotalfinishqty = e.Ftotalfinishqty,
            Fbstatus = string.IsNullOrEmpty(e.Fbstatus) ? "1" : e.Fbstatus,
            Fissuspend = e.Fissuspend,
            Finhighlimit = e.Finhighlimit,
            Fretime = e.Fretime,
            Fbomid = e.Fbomid,
            Fconveydate = e.Fconveydate,
            Fplanstartdate = e.Fplanstartdate,
            Fplanfinishdate = e.Fplanfinishdate,
            Factualstartdate = e.Factualstartdate,
            Factualfinishdate = e.Factualfinishdate,
            Ffinalid = e.Ffinalid,
            Ffinaldate = e.Ffinaldate,
            Fnote = e.Fnote,
            Fstockqty = e.Fstockqty,
            Fstockbaseqty = e.Fstockbaseqty
        }).ToList()
    };

    // ===== 写入映射 =====

    protected override TPrdMo MapToHeaderEntity(CreateProductionOrderRequest dto) => new()
    {
        Fbillno = dto.Fbillno?.Trim() ?? string.Empty, // 为空时由 PrepareHeaderForCreateAsync 按编码规则取号
        Fbilltype = dto.Fbilltype,
        Fdate = dto.Fdate ?? DateTime.Now,
        FCompanyId = dto.Fcompanyid, // 组织（为空时由基类回落当前登录组织）
        Fplannerid = dto.Fplannerid,
        Fnote = dto.Fnote,
        Fcheckdate = DateSentinel,
        Fdisabledate = DateSentinel,
        FStatus = 10
    };

    // ===== 编码规则取号：SCDD + yyyyMMdd + 4位日流水（纯前缀+日期+流水，无字段段）=====
    protected override string? BillCodeFormKey => BillCodeFormKeys.ProductionOrder;
    protected override string GetBillNo(TPrdMo header) => header.Fbillno;
    protected override void SetBillNo(TPrdMo header, string billNo) => header.Fbillno = billNo;
    protected override Task<bool> BillNoExistsAsync(string billNo)
        => Db.Queryable<TPrdMo>().AnyAsync(h => h.Fbillno == billNo);

    protected override Task PopulateBillCodeContextAsync(IDictionary<string, string> ctx, TPrdMo header, CreateProductionOrderRequest dto)
    {
        ctx[BillCodeFields.Date] = (header.Fdate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
        return Task.CompletedTask;
    }

    protected override void UpdateHeaderEntity(TPrdMo entity, UpdateProductionOrderRequest dto)
    {
        entity.Fbilltype = dto.Fbilltype;
        entity.Fdate = dto.Fdate ?? entity.Fdate;
        if (!string.IsNullOrEmpty(dto.Fcompanyid)) entity.FCompanyId = dto.Fcompanyid;
        entity.Fplannerid = dto.Fplannerid;
        entity.Fnote = dto.Fnote;
    }

    protected override List<TPrdMoentry> MapToEntryEntities(CreateProductionOrderRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    protected override List<TPrdMoentry> MapToEntryEntities(UpdateProductionOrderRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    private static List<TPrdMoentry> MapEntries(List<CreateProductionOrderEntryRequest> entries, string headerUid)
        => entries.Select(e => new TPrdMoentry
        {
            FInterId = headerUid,
            Fproducttype = string.IsNullOrEmpty(e.Fproducttype) ? "1" : e.Fproducttype,
            Fworkshopid = e.Fworkshopid,
            Fmaterialid = e.Fmaterialid,
            Fmachinemodel = e.Fmachinemodel,
            Flot = e.Flot,
            Fbaseunitid = string.IsNullOrEmpty(e.Fbaseunitid) ? e.Fcommonunitid : e.Fbaseunitid,
            Fbaseunitqty = e.Fbaseunitqty,
            Fcommonunitid = e.Fcommonunitid,
            Fqty = e.Fqty,
            Fauxpropid = e.Fauxpropid,
            Fprorouteid = e.Fprorouteid,
            Fbarcoderuleid = e.Fbarcoderuleid,
            Finhighlimit = e.Finhighlimit,
            Fretime = e.Fretime,
            Fbomid = e.Fbomid,
            Fbstatus = "1", // 新建明细业务状态=计划
            // 计划/实际/下达/结案日期：空值统一 1900 哨兵（dev SQLite NOT NULL）
            Fplanstartdate = e.Fplanstartdate ?? DateSentinel,
            Fplanfinishdate = e.Fplanfinishdate ?? DateSentinel,
            Factualstartdate = DateSentinel,
            Factualfinishdate = DateSentinel,
            Fconveydate = DateSentinel,
            Ffinaldate = DateSentinel,
            Fnote = e.Fnote
        }).ToList();

    protected override void SetEntryIndex(TPrdMoentry entry, int index)
    {
        entry.Fentryid = index;
        entry.Fdetailid = entry.Uid;
    }

    protected override async Task<List<TPrdMoentry>> GetEntriesByHeaderIdAsync(string headerUid)
        => await Db.Queryable<TPrdMoentry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.Fentryid)
            .ToListAsync();

    // ===== 名称字典加载 =====

    private async Task<Dictionary<string, (string Number, string Name, string Spec, string Chart, bool Batch)>> LoadMaterialDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdMaterial>().Where(m => list.Contains(m.Uid))
            .Select(m => new { m.Uid, m.FNumber, m.FName, m.FSpecification, m.FCHARTNUMBER, m.FIsBatchManage }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key,
            g => (g.First().FNumber, g.First().FName, g.First().FSpecification, g.First().FCHARTNUMBER, g.First().FIsBatchManage));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadUnitDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdUnit>().Where(u => list.Contains(u.Uid))
            .Select(u => new { u.Uid, u.FNumber, u.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadDeptDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdDepartment>().Where(d => list.Contains(d.Uid))
            .Select(d => new { d.Uid, d.FNumber, d.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadProRouteDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TEngProroute>().Where(p => list.Contains(p.Uid))
            .Select(p => new { p.Uid, p.Fnumber, p.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().Fnumber, g.First().Fname));
    }

    private async Task<Dictionary<string, string>> LoadBomDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdBom>().Where(b => list.Contains(b.Uid))
            .Select(b => new { b.Uid, b.FBillNo }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().FBillNo);
    }

    private async Task<Dictionary<string, string>> LoadBarcodeDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysBarcode>().Where(b => list.Contains(b.Uid))
            .Select(b => new { b.Uid, b.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadEmployeeNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<THrEmpinfo>().Where(e => list.Contains(e.Uid))
            .Select(e => new { e.Uid, e.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadUserNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysLoginUser>().Where(u => list.Contains(u.UserId))
            .Select(u => new { u.UserId, u.UserName }).ToListAsync();
        return rows.GroupBy(r => r.UserId).ToDictionary(g => g.Key, g => g.First().UserName);
    }

    private async Task<Dictionary<string, string>> LoadOrgDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysOrgStructure>().Where(o => list.Contains(o.Uid))
            .Select(o => new { o.Uid, o.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<int, string>> LoadStatusDictAsync()
    {
        var rows = await Db.Queryable<SysStatus>().Select(s => new { s.Fitemid, s.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Fitemid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadFlexAuxDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdFlexauxproperty>().Where(f => list.Contains(f.Uid))
            .Select(f => new { f.Uid, f.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    /// <summary>求出"启用辅助属性"的物料 Uid 集合（按物料的 Uid/FInterId 关联 T_BD_MATERIALAUXPTY）</summary>
    private async Task<HashSet<string>> LoadAuxEnabledSetAsync(IEnumerable<string> materialIds)
    {
        var result = new HashSet<string>();
        var matIdList = materialIds.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (matIdList.Count == 0) return result;

        var mats = await Db.Queryable<TBdMaterial>().Where(m => matIdList.Contains(m.Uid))
            .Select(m => new { m.Uid, m.FInterId }).ToListAsync();
        var allKeys = mats.SelectMany(m => new[] { m.Uid, m.FInterId }).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (allKeys.Count == 0) return result;

        var auxRows = await Db.Queryable<TBdMaterialAuxPty>()
            .Where(a => a.FIsEnable && !a.FDeleted && (allKeys.Contains(a.FInterId) || allKeys.Contains(a.FMasterId)))
            .Select(a => new { a.FInterId, a.FMasterId }).ToListAsync();
        var enabledKeys = new HashSet<string>();
        foreach (var ar in auxRows)
        {
            if (!string.IsNullOrEmpty(ar.FInterId)) enabledKeys.Add(ar.FInterId);
            if (!string.IsNullOrEmpty(ar.FMasterId)) enabledKeys.Add(ar.FMasterId);
        }
        // 映射回物料 Uid
        foreach (var m in mats)
        {
            if ((m.Uid != null && enabledKeys.Contains(m.Uid)) || (m.FInterId != null && enabledKeys.Contains(m.FInterId)))
                result.Add(m.Uid);
        }
        return result;
    }
}
