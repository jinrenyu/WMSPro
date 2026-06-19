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
/// 退料申请单服务（真实表 T_PUR_MRAPP / T_PUR_MRAPPENTRY，一主一从）。
/// 列表按明细行展开并解析名称；详情解析主表全部外键名称。
/// 状态：10=草稿(未审)、40=审核(已审)、70=关闭。
/// 金额服务端重算（基数=申请退料数量 Fmrappqty），不信任前端传值。
/// </summary>
public class MaterialReturnApplyService : DocumentService<TPurMrApp, TPurMrAppEntry,
    MaterialReturnApplyListDto, MaterialReturnApplyDetailDto, CreateMaterialReturnApplyRequest, UpdateMaterialReturnApplyRequest>
{
    public MaterialReturnApplyService(
        IRepository<TPurMrApp> headerRepo,
        IRepository<TPurMrAppEntry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IBillCodeService billCode,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog, billCode)
    {
    }

    protected override string PrgKey => "MaterialReturnApply";

    // ===== 审核 / 反审核 / 关闭 =====

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("单据已审核，无需重复审核");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭，不能审核");

        var result = await Db.Updateable<TPurMrApp>()
            .SetColumns(h => h.FStatus == 40)
            .SetColumns(h => h.Fcheckerid == (CurrentUser.UserId ?? string.Empty))
            .SetColumns(h => h.Fcheckdate == DateTime.Now)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Approve, uid, header.Fbillno, "审核单据", result);
        return result;
    }

    /// <summary>反审核（回到草稿 10）。审核日期回退用 1900 哨兵（开发库 NOT NULL 兼容，前端按 &lt;=1900 显示为空）。</summary>
    public override async Task<bool> RejectAsync(string uid, string? reason = null)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能反审核");

        var sentinelDate = new DateTime(1900, 1, 1);
        var result = await Db.Updateable<TPurMrApp>()
            .SetColumns(h => h.FStatus == 10)
            .SetColumns(h => h.Fcheckerid == string.Empty)
            .SetColumns(h => h.Fcheckdate == sentinelDate)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Reject, uid, header.Fbillno, reason ?? "反审核单据", result);
        return result;
    }

    public override async Task<bool> CloseAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能关闭");

        var result = await Db.Updateable<TPurMrApp>()
            .SetColumns(h => h.FStatus == 70)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<TPurMrApp, bool>> BuildSearchPredicate(string keyword)
        => h => h.Fbillno.Contains(keyword) || h.Fsupplyid.Contains(keyword);

    // 已审核/已关闭单据禁止修改/删除（前端只读外的后端兜底，须先反审核）
    public override async Task<bool> UpdateAsync(string uid, UpdateMaterialReturnApplyRequest request)
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

    // ===== 列表：按明细行展开 + 名称解析 =====

    private static readonly HashSet<string> HeaderFilterFields = new(StringComparer.OrdinalIgnoreCase) { "fbillno", "fdate", "fStatus" };

    public override async Task<PagedResult<MaterialReturnApplyListDto>> GetPagedListAsync(PagedRequest request)
    {
        var filters = request.DynamicFilters ?? new List<DynamicFilterInfo>();
        var headerFilters = filters.Where(f => HeaderFilterFields.Contains(f.Field)).ToList();
        var entryFilters = filters.Where(f => !HeaderFilterFields.Contains(f.Field)).ToList();

        // 1) 表头条件（关键字 + 表头动态筛选 + 仅已审核）-> 命中的主表 Uid 集合
        List<string>? headerIds = null;
        if (!string.IsNullOrWhiteSpace(request.Keyword) || headerFilters.Count > 0 || request.OnlyApproved)
        {
            var hq = Db.Queryable<TPurMrApp>().Where(h => !h.FDeleted);
            if (request.OnlyApproved)  // 作为下游单据源单选择器时，仅取已审核(40)且非禁用的退料申请单
                hq = hq.Where(h => h.FStatus == 40 && !h.FDisabled);
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var kw = request.Keyword.Trim();
                hq = hq.Where(h => h.Fbillno.Contains(kw));
            }
            if (headerFilters.Count > 0)
                hq = hq.Where(headerFilters.ToConditionalModels<TPurMrApp>());
            headerIds = await hq.Select(h => h.Uid).ToListAsync();
            if (headerIds.Count == 0)
                return new PagedResult<MaterialReturnApplyListDto> { Items = new(), TotalCount = 0, PageIndex = request.PageIndex, PageSize = request.PageSize };
        }

        // 2) 分页查询明细（+ 明细动态筛选）
        RefAsync<int> totalCount = 0;
        var query = Db.Queryable<TPurMrAppEntry>().Where(e => !e.FDeleted);
        if (headerIds != null)
            query = query.Where(e => headerIds.Contains(e.FInterId));
        if (entryFilters.Count > 0)
            query = query.Where(entryFilters.ToConditionalModels<TPurMrAppEntry>());
        var entries = await query
            .OrderBy(e => e.CYmd, OrderByType.Desc)
            .OrderBy(e => e.FENTRYID)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        if (entries.Count == 0)
            return new PagedResult<MaterialReturnApplyListDto> { Items = new(), TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };

        // 3) 批量加载主表 + 名称源
        var hids = entries.Select(e => e.FInterId).Distinct().ToList();
        var headers = await Db.Queryable<TPurMrApp>().Where(h => hids.Contains(h.Uid)).ToListAsync();
        var headerDict = headers.GroupBy(h => h.Uid).ToDictionary(g => g.Key, g => g.First());

        var materialDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
        var unitDict = await LoadUnitDictAsync(entries.Select(e => e.Funitid).Concat(entries.Select(e => e.Fstockunitid)));
        var stockDict = await LoadStockDictAsync(entries.Select(e => e.Fstockid));
        var stockLocDict = await LoadStockLocDictAsync(entries.Select(e => e.Fstocklocid));
        var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));
        var supplierDict = await LoadSupplierDictAsync(headers.Select(h => h.Fsupplyid));
        var deptDict = await LoadDepartmentDictAsync(headers.Select(h => h.Fappdeptid));
        var orgDict = await LoadOrgDictAsync(headers.Select(h => h.Frequireorgid)
            .Concat(headers.Select(h => h.Fpurchaseorgid)).Concat(headers.Select(h => h.Fstockorgid)));
        var billTypeDict = await LoadBillTypeDictAsync(headers.Select(h => h.Fbilltypeid));
        var userDict = await LoadUserNameDictAsync(headers.Select(h => h.CUser).Concat(headers.Select(h => h.Fcheckerid)));
        var statusDict = await LoadStatusDictAsync();

        // 4) 映射
        var items = entries.Select(e =>
        {
            headerDict.TryGetValue(e.FInterId, out var h);
            materialDict.TryGetValue(e.Fmaterialid ?? string.Empty, out var mat);
            unitDict.TryGetValue(e.Funitid ?? string.Empty, out var unit);
            unitDict.TryGetValue(e.Fstockunitid ?? string.Empty, out var stockUnit);
            stockDict.TryGetValue(e.Fstockid ?? string.Empty, out var stock);
            return new MaterialReturnApplyListDto
            {
                Uid = h?.Uid ?? e.FInterId,
                EntryUid = e.Uid,
                Fbillno = h?.Fbillno ?? string.Empty,
                Fdate = h?.Fdate,
                FbilltypeName = h != null ? billTypeDict.GetValueOrDefault(h.Fbilltypeid, string.Empty) : string.Empty,
                Fentryid = e.FENTRYID,
                FStatus = h?.FStatus ?? 0,
                FstatusName = statusDict.GetValueOrDefault(h?.FStatus ?? 0, string.Empty),
                FDisabled = h?.FDisabled ?? false,
                Fbusinesstype = h?.Fbusinesstype ?? string.Empty,
                Frmtype = h?.Frmtype ?? string.Empty,
                Frmmode = h?.Frmmode ?? string.Empty,
                FrequireorgName = h != null ? orgDict.GetValueOrDefault(h.Frequireorgid, default).Name : string.Empty,
                FappdeptName = h != null ? deptDict.GetValueOrDefault(h.Fappdeptid, default).Name : string.Empty,
                FpurchaseorgName = h != null ? orgDict.GetValueOrDefault(h.Fpurchaseorgid, default).Name : string.Empty,
                FstockorgName = h != null ? orgDict.GetValueOrDefault(h.Fstockorgid, default).Name : string.Empty,
                Fmaterialid = e.Fmaterialid,
                FmaterialNumber = mat.Number,
                FmaterialName = mat.Name,
                FSpecification = mat.Spec,
                Flot = e.Flot,
                FauxpropName = flexDict.GetValueOrDefault(e.Fauxpropid ?? string.Empty, string.Empty),
                Fmrappqty = e.Fmrappqty,
                Fmrqty = e.Fmrqty,
                Fmrbqty = e.Fmrbqty,
                Freplenishqty = e.Freplenishqty,
                FunitName = unit.Name,
                FstockunitNumber = stockUnit.Number,
                FstockunitName = stockUnit.Name,
                FstockNumber = stock.Number,
                FstockName = stock.Name,
                FisOpenLocation = stock.OpenLoc,
                FstocklocName = stockLocDict.GetValueOrDefault(e.Fstocklocid ?? string.Empty, string.Empty),
                Fprice = e.Fprice,
                Famount = e.Famount,
                FsupplyNumber = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Number : string.Empty,
                FsupplyName = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Name : string.Empty,
                Fsrcbillno = e.Fsrcbillno,
                CuserName = h != null ? userDict.GetValueOrDefault(h.CUser, string.Empty) : string.Empty,
                CYmd = h?.CYmd,
                FcheckerName = h != null ? userDict.GetValueOrDefault(h.Fcheckerid, string.Empty) : string.Empty,
                Fcheckdate = h?.Fcheckdate
            };
        }).ToList();

        return new PagedResult<MaterialReturnApplyListDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    // 抽象成员（列表走上面的重写，此处为契约的简化实现）
    protected override MaterialReturnApplyListDto MapToListDto(TPurMrApp entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdate = entity.Fdate,
        FStatus = entity.FStatus
    };

    // ===== 详情：主表 + 名称解析 + 明细 =====

    public override async Task<MaterialReturnApplyDetailDto?> GetByIdAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return null;

        var entries = await GetEntriesByHeaderIdAsync(uid);
        var dto = MapToDetailDto(header, entries);

        // 主表名称
        dto.FbilltypeName = await Db.Queryable<TBasBilltype>().Where(b => b.Uid == header.Fbilltypeid).Select(b => b.Fname).FirstAsync() ?? string.Empty;
        var supplier = await LoadSupplierDictAsync(new[] { header.Fsupplyid });
        if (supplier.TryGetValue(header.Fsupplyid, out var sp)) { dto.FsupplyNumber = sp.Number; dto.FsupplyName = sp.Name; }
        var deptDict = await LoadDepartmentDictAsync(new[] { header.Fappdeptid });
        dto.FappdeptName = deptDict.GetValueOrDefault(header.Fappdeptid, default).Name;
        var orgDict = await LoadOrgDictAsync(new[] { header.Frequireorgid, header.Fpurchaseorgid, header.Fstockorgid });
        dto.FrequireorgName = orgDict.GetValueOrDefault(header.Frequireorgid, default).Name;
        dto.FpurchaseorgName = orgDict.GetValueOrDefault(header.Fpurchaseorgid, default).Name;
        dto.FstockorgName = orgDict.GetValueOrDefault(header.Fstockorgid, default).Name;
        var currency = await Db.Queryable<TBdCurrency>().Where(c => c.Uid == header.Fcurrencyid).Select(c => new { c.FNumber, c.FName }).FirstAsync();
        if (currency != null) { dto.FcurrencyNumber = currency.FNumber; dto.FcurrencyName = currency.FName; }
        dto.FstatusName = (await LoadStatusDictAsync()).GetValueOrDefault(header.FStatus, string.Empty);

        // 制单/审核/修改/禁用人名（取自登录用户）
        var userIds = new[] { header.CUser, header.MUser, header.Fcheckerid, header.Fdisableid };
        var userDict = await LoadUserNameDictAsync(userIds);
        dto.CuserName = userDict.GetValueOrDefault(header.CUser, string.Empty);
        dto.MuserName = userDict.GetValueOrDefault(header.MUser, string.Empty);
        dto.FcheckerName = userDict.GetValueOrDefault(header.Fcheckerid, string.Empty);
        dto.FdisableName = userDict.GetValueOrDefault(header.Fdisableid, string.Empty);

        // 明细名称 + 辅助属性名 + 是否启用辅助属性
        if (dto.Entries.Count > 0)
        {
            var matDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
            var unitDict = await LoadUnitDictAsync(entries.Select(e => e.Funitid).Concat(entries.Select(e => e.Fstockunitid)));
            var stockDict = await LoadStockDictAsync(entries.Select(e => e.Fstockid));
            var stockLocDict = await LoadStockLocDictAsync(entries.Select(e => e.Fstocklocid));
            var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));

            // 物料 Uid -> {Uid, FInterId}，并求出"启用辅助属性"的物料关联键集合
            var matIdList = entries.Select(e => e.Fmaterialid).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            var matKeyMap = new Dictionary<string, string[]>();
            var auxEnabledKeys = new HashSet<string>();
            if (matIdList.Count > 0)
            {
                var mats = await Db.Queryable<TBdMaterial>().Where(m => matIdList.Contains(m.Uid))
                    .Select(m => new { m.Uid, m.FInterId }).ToListAsync();
                matKeyMap = mats.GroupBy(m => m.Uid).ToDictionary(g => g.Key, g => new[] { g.First().Uid, g.First().FInterId });
                var allKeys = mats.SelectMany(m => new[] { m.Uid, m.FInterId }).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
                if (allKeys.Count > 0)
                {
                    var auxRows = await Db.Queryable<TBdMaterialAuxPty>()
                        .Where(a => a.FIsEnable && !a.FDeleted && (allKeys.Contains(a.FInterId) || allKeys.Contains(a.FMasterId)))
                        .Select(a => new { a.FInterId, a.FMasterId }).ToListAsync();
                    foreach (var ar in auxRows)
                    {
                        if (!string.IsNullOrEmpty(ar.FInterId)) auxEnabledKeys.Add(ar.FInterId);
                        if (!string.IsNullOrEmpty(ar.FMasterId)) auxEnabledKeys.Add(ar.FMasterId);
                    }
                }
            }

            foreach (var line in dto.Entries)
            {
                if (matDict.TryGetValue(line.Fmaterialid ?? string.Empty, out var m))
                {
                    line.FmaterialNumber = m.Number; line.FmaterialName = m.Name; line.FSpecification = m.Spec;
                    line.FisBatchManage = m.Batch; line.FisKfPeriod = m.Kf; line.FKfPeriod = m.KfPeriod; line.FKfUnit = m.KfUnit;
                }
                if (unitDict.TryGetValue(line.Funitid ?? string.Empty, out var u))
                {
                    line.FunitNumber = u.Number; line.FunitName = u.Name;
                }
                if (unitDict.TryGetValue(line.Fstockunitid ?? string.Empty, out var su))
                {
                    line.FstockunitName = su.Name;
                }
                if (stockDict.TryGetValue(line.Fstockid ?? string.Empty, out var st))
                {
                    line.FstockNumber = st.Number; line.FstockName = st.Name; line.FisOpenLocation = st.OpenLoc;
                }
                line.FstocklocName = stockLocDict.GetValueOrDefault(line.Fstocklocid ?? string.Empty, string.Empty);
                line.FauxpropName = flexDict.GetValueOrDefault(line.Fauxpropid ?? string.Empty, string.Empty);
                if (matKeyMap.TryGetValue(line.Fmaterialid ?? string.Empty, out var ks))
                    line.FisAuxEnabled = ks.Any(k => !string.IsNullOrEmpty(k) && auxEnabledKeys.Contains(k));
            }
        }

        return dto;
    }

    protected override MaterialReturnApplyDetailDto MapToDetailDto(TPurMrApp header, List<TPurMrAppEntry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fbilltypeid = header.Fbilltypeid,
        Fdate = header.Fdate,
        FStatus = header.FStatus,
        Foastatus = header.Foastatus,
        Foaresult = header.Foaresult,
        Fbusinesstype = header.Fbusinesstype,
        Frmtype = header.Frmtype,
        Frmmode = header.Frmmode,
        Freplenishmode = header.Freplenishmode,
        Frequireorgid = header.Frequireorgid,
        Fpurchaseorgid = header.Fpurchaseorgid,
        Fstockorgid = header.Fstockorgid,
        Fappdeptid = header.Fappdeptid,
        Fsupplyid = header.Fsupplyid,
        Fcurrencyid = header.Fcurrencyid,
        Fexchangetypeid = header.Fexchangetypeid,
        Fexchangerate = header.Fexchangerate,
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
        Entries = entries.Select(e => new MaterialReturnApplyEntryDto
        {
            Uid = e.Uid,
            Fentryid = e.FENTRYID,
            Frowtype = e.Frowtype,
            Fmaterialid = e.Fmaterialid,
            Fauxpropid = e.Fauxpropid,
            Flot = e.Flot,
            Fmrappqty = e.Fmrappqty,
            Fmrqty = e.Fmrqty,
            Fmrbqty = e.Fmrbqty,
            Freplenishqty = e.Freplenishqty,
            Fstockqty = e.Fstockqty,
            Fbaseunitqty = e.Fbaseunitqty,
            Funitid = e.Funitid,
            Fbaseunitid = e.Fbaseunitid,
            Fstockunitid = e.Fstockunitid,
            Fstockid = e.Fstockid,
            Fstocklocid = e.Fstocklocid,
            Fprice = e.Fprice,
            Ftaxrate = e.Ftaxrate,
            Ftaxprice = e.Ftaxprice,
            Fdiscountrate = e.Fdiscountrate,
            Fdiscount = e.Fdiscount,
            Ftaxamount = e.Ftaxamount,
            Famount = e.Famount,
            Fallamount = e.Fallamount,
            Fsrcformid = e.Fsrcformid,
            Fsrcbillno = e.Fsrcbillno,
            Forderbillno = e.Forderbillno,
            Forderentryid = e.Forderentryid,
            Forderinterid = e.Forderinterid,
            Forderdetailid = e.Forderdetailid,
            Fnote = e.Fnote
        }).ToList()
    };

    // ===== 写入映射 =====

    protected override TPurMrApp MapToHeaderEntity(CreateMaterialReturnApplyRequest dto) => new()
    {
        Fbillno = dto.Fbillno?.Trim() ?? string.Empty, // 为空时由 PrepareHeaderForCreateAsync 按编码规则取号
        Fbilltypeid = dto.Fbilltypeid,
        Fdate = dto.Fdate ?? DateTime.Now,
        Fbusinesstype = dto.Fbusinesstype,
        Frmtype = dto.Frmtype,
        Frmmode = dto.Frmmode,
        Freplenishmode = dto.Freplenishmode,
        Frequireorgid = dto.Frequireorgid,
        Fpurchaseorgid = dto.Fpurchaseorgid,
        Fstockorgid = dto.Fstockorgid,
        Fappdeptid = dto.Fappdeptid,
        Fsupplyid = dto.Fsupplyid,
        Fcurrencyid = dto.Fcurrencyid,
        Fexchangetypeid = dto.Fexchangetypeid,
        Fexchangerate = dto.Fexchangerate == 0 ? 1 : dto.Fexchangerate,
        Fnote = dto.Fnote,
        // 审核/禁用日期用 1900 哨兵：开发库 SQLite 这些列 NOT NULL（不能 null），1900 既满足 NOT NULL，
        // 又落在生产 SQLServer DATETIME 下限(1753)之上（不能用 DateTime.MinValue=0001，FDISABLEDATE 为 DATETIME 会溢出）；前端按 <=1900 显示为空
        Fcheckdate = new DateTime(1900, 1, 1),
        Fdisabledate = new DateTime(1900, 1, 1),
        FStatus = 10
    };

    // ===== 编码规则取号：声明 formKey + 编号读写/查重 + 来源字段，通用取号逻辑在基类 =====
    protected override string? BillCodeFormKey => BillCodeFormKeys.MrApp;
    protected override string GetBillNo(TPurMrApp header) => header.Fbillno;
    protected override void SetBillNo(TPurMrApp header, string billNo) => header.Fbillno = billNo;
    protected override Task<bool> BillNoExistsAsync(string billNo)
        => Db.Queryable<TPurMrApp>().AnyAsync(h => h.Fbillno == billNo);

    /// <summary>退料申请单取号来源字段：单据日期 + 单据类型编码 + 供应商编码（当前规则仅用日期段，其余备用）</summary>
    protected override async Task PopulateBillCodeContextAsync(IDictionary<string, string> ctx, TPurMrApp header, CreateMaterialReturnApplyRequest dto)
    {
        ctx[BillCodeFields.Date] = (header.Fdate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
        if (!string.IsNullOrEmpty(header.Fbilltypeid))
        {
            var billTypeNo = await Db.Queryable<TBasBilltype>().Where(b => b.Uid == header.Fbilltypeid).Select(b => b.Fnumber).FirstAsync();
            if (!string.IsNullOrEmpty(billTypeNo)) ctx[BillCodeFields.BillType] = billTypeNo;
        }
        if (!string.IsNullOrEmpty(header.Fsupplyid))
        {
            var supplierNo = await Db.Queryable<TBdSupplier>().Where(s => s.Uid == header.Fsupplyid).Select(s => s.FNumber).FirstAsync();
            if (!string.IsNullOrEmpty(supplierNo)) ctx[BillCodeFields.Supplier] = supplierNo;
        }
    }

    protected override void UpdateHeaderEntity(TPurMrApp entity, UpdateMaterialReturnApplyRequest dto)
    {
        entity.Fbilltypeid = dto.Fbilltypeid;
        entity.Fdate = dto.Fdate ?? entity.Fdate;
        entity.Fbusinesstype = dto.Fbusinesstype;
        entity.Frmtype = dto.Frmtype;
        entity.Frmmode = dto.Frmmode;
        entity.Freplenishmode = dto.Freplenishmode;
        entity.Frequireorgid = dto.Frequireorgid;
        entity.Fpurchaseorgid = dto.Fpurchaseorgid;
        entity.Fstockorgid = dto.Fstockorgid;
        entity.Fappdeptid = dto.Fappdeptid;
        entity.Fsupplyid = dto.Fsupplyid;
        entity.Fcurrencyid = dto.Fcurrencyid;
        entity.Fexchangetypeid = dto.Fexchangetypeid;
        entity.Fexchangerate = dto.Fexchangerate == 0 ? 1 : dto.Fexchangerate;
        entity.Fnote = dto.Fnote;
    }

    protected override List<TPurMrAppEntry> MapToEntryEntities(CreateMaterialReturnApplyRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    protected override List<TPurMrAppEntry> MapToEntryEntities(UpdateMaterialReturnApplyRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    private static List<TPurMrAppEntry> MapEntries(List<CreateMaterialReturnApplyEntryRequest> entries, string headerUid)
        => entries.Select(e =>
        {
            // 金额服务端重算（基数=申请退料数量 Fmrappqty）：金额=数量×单价×(1-折扣率%)、折扣额=数量×单价×折扣率%、
            // 税额=金额×税率%、价税合计=金额+税额、含税单价=单价×(1+税率%)
            var amount = Math.Round(e.Fmrappqty * e.Fprice * (1 - e.Fdiscountrate / 100m), 2);
            var discount = Math.Round(e.Fmrappqty * e.Fprice * (e.Fdiscountrate / 100m), 2);
            var taxAmount = Math.Round(amount * e.Ftaxrate / 100m, 2);
            return new TPurMrAppEntry
            {
                FInterId = headerUid,
                Frowtype = string.IsNullOrEmpty(e.Frowtype) ? "Standard" : e.Frowtype,
                Fmaterialid = e.Fmaterialid,
                Fauxpropid = e.Fauxpropid,
                Flot = e.Flot,
                Fmrappqty = e.Fmrappqty,
                Fmrqty = e.Fmrqty,
                Freplenishqty = e.Freplenishqty,
                Fstockqty = e.Fstockqty == 0 ? e.Fmrappqty : e.Fstockqty,
                Funitid = e.Funitid,
                // 基本单位/数量：当前未接入物料换算率，按 1:1 回落（待换算率主数据接入后完善）
                Fbaseunitid = string.IsNullOrEmpty(e.Fbaseunitid) ? e.Funitid : e.Fbaseunitid,
                Fbaseunitqty = e.Fbaseunitqty == 0 ? e.Fmrappqty : e.Fbaseunitqty,
                Fstockunitid = string.IsNullOrEmpty(e.Fstockunitid) ? e.Funitid : e.Fstockunitid,
                Fstockid = e.Fstockid,
                Fstocklocid = e.Fstocklocid,
                Fprice = e.Fprice,
                Ftaxrate = e.Ftaxrate,
                Ftaxprice = Math.Round(e.Fprice * (1 + e.Ftaxrate / 100m), 6),
                Fdiscountrate = e.Fdiscountrate,
                Fdiscount = discount,
                Ftaxamount = taxAmount,
                Famount = amount,
                Fallamount = amount + taxAmount,
                Fsrcformid = e.Fsrcformid,
                Fsrcbillno = e.Fsrcbillno,
                Forderbillno = e.Forderbillno,
                Forderentryid = e.Forderentryid,
                Forderinterid = e.Forderinterid,
                Forderdetailid = e.Forderdetailid,
                Fnote = e.Fnote
            };
        }).ToList();

    protected override void SetEntryIndex(TPurMrAppEntry entry, int index)
    {
        entry.FENTRYID = index;
        entry.FDETAILID = entry.Uid;
    }

    protected override async Task<List<TPurMrAppEntry>> GetEntriesByHeaderIdAsync(string headerUid)
        => await Db.Queryable<TPurMrAppEntry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.FENTRYID)
            .ToListAsync();

    // ===== 名称字典加载 =====

    private async Task<Dictionary<string, (string Number, string Name, string Spec, bool Batch, bool Kf, int KfPeriod, int KfUnit)>> LoadMaterialDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdMaterial>().Where(m => list.Contains(m.Uid))
            .Select(m => new { m.Uid, m.FNumber, m.FName, m.FSpecification, m.FIsBatchManage, m.FIsKfPeriod, m.FKfPeriod, m.FKfUnit }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key,
            g => (g.First().FNumber, g.First().FName, g.First().FSpecification, g.First().FIsBatchManage, g.First().FIsKfPeriod, g.First().FKfPeriod, g.First().FKfUnit));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadUnitDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdUnit>().Where(u => list.Contains(u.Uid))
            .Select(u => new { u.Uid, u.FNumber, u.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadSupplierDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdSupplier>().Where(s => list.Contains(s.Uid))
            .Select(s => new { s.Uid, s.FNumber, s.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadDepartmentDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdDepartment>().Where(d => list.Contains(d.Uid))
            .Select(d => new { d.Uid, d.FNumber, d.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name, bool OpenLoc)>> LoadStockDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdStock>().Where(s => list.Contains(s.Uid))
            .Select(s => new { s.Uid, s.FNumber, s.FName, s.FIsOpenLocation }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName, g.First().FIsOpenLocation));
    }

    // 仓位（FSTOCKLOCID）解析自仓位主数据 TBdStockPlace（前端选择器用 module=stockplace，按仓库级联），与选择器同源以保证存取一致。
    private async Task<Dictionary<string, string>> LoadStockLocDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdStockPlace>().Where(f => list.Contains(f.Uid))
            .Select(f => new { f.Uid, f.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().FName);
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadOrgDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysOrgStructure>().Where(o => list.Contains(o.Uid))
            .Select(o => new { o.Uid, o.Fnumber, o.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().Fnumber, g.First().Fname));
    }

    private async Task<Dictionary<string, string>> LoadBillTypeDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBasBilltype>().Where(b => list.Contains(b.Uid))
            .Select(b => new { b.Uid, b.Fname }).ToListAsync();
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
}
