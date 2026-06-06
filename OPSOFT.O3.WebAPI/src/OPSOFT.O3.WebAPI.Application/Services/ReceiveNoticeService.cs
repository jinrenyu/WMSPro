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
/// 收料通知单服务（真实表 T_PUR_RECEIVE / T_PUR_RECEIVEENTRY）
/// 列表按明细行展开并解析名称；详情解析主表全部外键名称。
/// 状态：10=草稿(未审)、40=审核(已审)、70=关闭。
/// </summary>
public class ReceiveNoticeService : DocumentService<TPurReceive, TPurReceiveEntry,
    ReceiveNoticeListDto, ReceiveNoticeDetailDto, CreateReceiveNoticeRequest, UpdateReceiveNoticeRequest>
{
    public ReceiveNoticeService(
        IRepository<TPurReceive> headerRepo,
        IRepository<TPurReceiveEntry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog) { }

    protected override string PrgKey => "ReceiveNotice";

    // ===== 审核 / 反审核 / 关闭 =====

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("单据已审核，无需重复审核");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭，不能审核");

        var result = await Db.Updateable<TPurReceive>()
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

    /// <summary>反审核（回到草稿 10）</summary>
    public override async Task<bool> RejectAsync(string uid, string? reason = null)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能反审核");

        var result = await Db.Updateable<TPurReceive>()
            .SetColumns(h => h.FStatus == 10)
            .SetColumns(h => h.Fcheckerid == string.Empty)
            .SetColumns(h => h.Fcheckdate == DateTime.MinValue)
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

        var result = await Db.Updateable<TPurReceive>()
            .SetColumns(h => h.FStatus == 70)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<TPurReceive, bool>> BuildSearchPredicate(string keyword)
        => h => h.Fbillno.Contains(keyword) || h.Fsupplyid.Contains(keyword);

    // 已审核单据禁止修改/删除（前端只读外的后端兜底，须先反审核）
    public override async Task<bool> UpdateAsync(string uid, UpdateReceiveNoticeRequest request)
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

    // 高级筛选中归属"表头"的字段（其余字段按"明细"处理）；解析出来的名称列不参与服务端筛选
    private static readonly HashSet<string> HeaderFilterFields = new(StringComparer.OrdinalIgnoreCase) { "fbillno", "fdate", "fStatus" };

    public override async Task<PagedResult<ReceiveNoticeListDto>> GetPagedListAsync(PagedRequest request)
    {
        var filters = request.DynamicFilters ?? new List<DynamicFilterInfo>();
        var headerFilters = filters.Where(f => HeaderFilterFields.Contains(f.Field)).ToList();
        var entryFilters = filters.Where(f => !HeaderFilterFields.Contains(f.Field)).ToList();

        // 1) 表头条件（关键字 + 表头动态筛选）-> 命中的主表 Uid 集合
        List<string>? headerIds = null;
        if (!string.IsNullOrWhiteSpace(request.Keyword) || headerFilters.Count > 0)
        {
            var hq = Db.Queryable<TPurReceive>().Where(h => !h.FDeleted);
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var kw = request.Keyword.Trim();
                hq = hq.Where(h => h.Fbillno.Contains(kw));
            }
            if (headerFilters.Count > 0)
                hq = hq.Where(headerFilters.ToConditionalModels<TPurReceive>());
            headerIds = await hq.Select(h => h.Uid).ToListAsync();
            if (headerIds.Count == 0)
                return new PagedResult<ReceiveNoticeListDto> { Items = new(), TotalCount = 0, PageIndex = request.PageIndex, PageSize = request.PageSize };
        }

        // 2) 分页查询明细（+ 明细动态筛选）
        RefAsync<int> totalCount = 0;
        var query = Db.Queryable<TPurReceiveEntry>().Where(e => !e.FDeleted);
        if (headerIds != null)
            query = query.Where(e => headerIds.Contains(e.FInterId));
        if (entryFilters.Count > 0)
            query = query.Where(entryFilters.ToConditionalModels<TPurReceiveEntry>());
        var entries = await query
            .OrderBy(e => e.CYmd, OrderByType.Desc)
            .OrderBy(e => e.FENTRYID)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        if (entries.Count == 0)
            return new PagedResult<ReceiveNoticeListDto> { Items = new(), TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };

        // 3) 批量加载主表 + 名称源
        var hids = entries.Select(e => e.FInterId).Distinct().ToList();
        var headers = await Db.Queryable<TPurReceive>().Where(h => hids.Contains(h.Uid)).ToListAsync();
        var headerDict = headers.GroupBy(h => h.Uid).ToDictionary(g => g.Key, g => g.First());

        var materialDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
        var unitDict = await LoadUnitDictAsync(entries.Select(e => e.Funitid).Concat(entries.Select(e => e.Fbaseunitid)));
        var stockDict = await LoadStockDictAsync(entries.Select(e => e.Fstockid));
        var stockLocDict = await LoadStockLocDictAsync(entries.Select(e => e.Fstocklocid));
        var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));
        var supplierDict = await LoadSupplierDictAsync(headers.Select(h => h.Fsupplyid));
        var purchaserDict = await LoadEmployeeNameDictAsync(headers.Select(h => h.Fpurchaserid));
        var deptDict = await LoadDepartmentDictAsync(headers.Select(h => h.Freceivedeptid));
        var orgDict = await LoadOrgDictAsync(headers.Select(h => h.Fpurorgid));
        var billTypeDict = await LoadBillTypeDictAsync(headers.Select(h => h.Fbilltypeid));
        var statusDict = await LoadStatusDictAsync();

        // 4) 映射
        var items = entries.Select(e =>
        {
            headerDict.TryGetValue(e.FInterId, out var h);
            materialDict.TryGetValue(e.Fmaterialid ?? string.Empty, out var mat);
            unitDict.TryGetValue(e.Funitid ?? string.Empty, out var unit);
            unitDict.TryGetValue(e.Fbaseunitid ?? string.Empty, out var baseUnit);
            stockDict.TryGetValue(e.Fstockid ?? string.Empty, out var stock);
            return new ReceiveNoticeListDto
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
                Fmaterialid = e.Fmaterialid,
                FmaterialNumber = mat.Number,
                FmaterialName = mat.Name,
                FSpecification = mat.Spec,
                Factreceiveqty = e.Factreceiveqty,
                Fgodqty = e.Fgodqty,
                Fscrapqty = e.Fscrapqty,
                Finstockqty = e.Finstockqty,
                FunitName = unit.Name,
                Fbaseunitqty = e.Fbaseunitqty,
                FbaseunitName = baseUnit.Name,
                FauxpropName = flexDict.GetValueOrDefault(e.Fauxpropid ?? string.Empty, string.Empty),
                FisBatchManage = mat.Batch,
                FisKfPeriod = mat.Kf,
                FKfPeriod = mat.KfPeriod,
                FKfUnit = mat.KfUnit,
                Flot = e.Flot,
                Fprice = e.Fprice,
                Fpredeliverydate = e.Fpredeliverydate,
                Fkfdate = e.Fkfdate,
                FsupplyNumber = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Number : string.Empty,
                FsupplyName = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Name : string.Empty,
                FpurchaserName = h != null ? purchaserDict.GetValueOrDefault(h.Fpurchaserid, string.Empty) : string.Empty,
                FreceivedeptNumber = h != null ? deptDict.GetValueOrDefault(h.Freceivedeptid, default).Number : string.Empty,
                FreceivedeptName = h != null ? deptDict.GetValueOrDefault(h.Freceivedeptid, default).Name : string.Empty,
                FstockNumber = stock.Number,
                FstockName = stock.Name,
                FisOpenLocation = stock.OpenLoc,
                FstocklocName = stockLocDict.GetValueOrDefault(e.Fstocklocid ?? string.Empty, string.Empty),
                Forderbillno = e.Forderbillno,
                Forderentryid = e.Forderentryid,
                FpurorgName = h != null ? orgDict.GetValueOrDefault(h.Fpurorgid, default).Name : string.Empty
            };
        }).ToList();

        return new PagedResult<ReceiveNoticeListDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    // 抽象成员（列表走上面的重写，此处为契约的简化实现）
    protected override ReceiveNoticeListDto MapToListDto(TPurReceive entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdate = entity.Fdate,
        FStatus = entity.FStatus
    };

    // ===== 详情：主表 + 名称解析 + 明细 =====

    public override async Task<ReceiveNoticeDetailDto?> GetByIdAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return null;

        var entries = await GetEntriesByHeaderIdAsync(uid);
        var dto = MapToDetailDto(header, entries);

        // 主表名称
        dto.FbilltypeName = await Db.Queryable<TBasBilltype>().Where(b => b.Uid == header.Fbilltypeid).Select(b => b.Fname).FirstAsync() ?? string.Empty;
        var supplier = await LoadSupplierDictAsync(new[] { header.Fsupplyid });
        if (supplier.TryGetValue(header.Fsupplyid, out var sp)) { dto.FsupplyNumber = sp.Number; dto.FsupplyName = sp.Name; }
        var empDict = await LoadEmployeeNameDictAsync(new[] { header.Fpurchaserid, header.Freceiverid });
        dto.FpurchaserName = empDict.GetValueOrDefault(header.Fpurchaserid, string.Empty);
        dto.FreceiverName = empDict.GetValueOrDefault(header.Freceiverid, string.Empty);
        var deptDict = await LoadDepartmentDictAsync(new[] { header.Freceivedeptid, header.Fpurdeptid });
        dto.FreceivedeptName = deptDict.GetValueOrDefault(header.Freceivedeptid, default).Name;
        dto.FpurdeptName = deptDict.GetValueOrDefault(header.Fpurdeptid, default).Name;
        var orgDict = await LoadOrgDictAsync(new[] { header.Fdemandorgid, header.Fpurorgid });
        dto.FdemandorgName = orgDict.GetValueOrDefault(header.Fdemandorgid, default).Name;
        dto.FpurorgName = orgDict.GetValueOrDefault(header.Fpurorgid, default).Name;
        var currency = await Db.Queryable<TBdCurrency>().Where(c => c.Uid == header.Fsettlecurrid).Select(c => new { c.FNumber, c.FName }).FirstAsync();
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
            var unitDict = await LoadUnitDictAsync(entries.Select(e => e.Funitid));
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

    protected override ReceiveNoticeDetailDto MapToDetailDto(TPurReceive header, List<TPurReceiveEntry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fbilltypeid = header.Fbilltypeid,
        Fdate = header.Fdate,
        FStatus = header.FStatus,
        Foastatus = header.Foastatus,
        Foaresult = header.Foaresult,
        Fbusinesstype = header.Fbusinesstype,
        Fsrcformid = header.Fsrcformid,
        Fsrcbillno = header.Fsrcbillno,
        Fdemandorgid = header.Fdemandorgid,
        Fpurorgid = header.Fpurorgid,
        Freceivedeptid = header.Freceivedeptid,
        Fpurdeptid = header.Fpurdeptid,
        Freceiverid = header.Freceiverid,
        Fpurchaserid = header.Fpurchaserid,
        Fsupplyid = header.Fsupplyid,
        Fsettlecurrid = header.Fsettlecurrid,
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
        Entries = entries.Select(e => new ReceiveNoticeEntryDto
        {
            Uid = e.Uid,
            Fentryid = e.FENTRYID,
            Fmaterialid = e.Fmaterialid,
            Fauxpropid = e.Fauxpropid,
            Fcheckincoming = e.Fcheckincoming,
            Flot = e.Flot,
            Fsupplylot = e.Fsupplylot,
            Factreceiveqty = e.Factreceiveqty,
            Finstockqty = e.Finstockqty,
            Fgodqty = e.Fgodqty,
            Fscrapqty = e.Fscrapqty,
            Funitid = e.Funitid,
            Fbaseunitid = e.Fbaseunitid,
            Fbaseunitqty = e.Fbaseunitqty,
            Fstockid = e.Fstockid,
            Fstocklocid = e.Fstocklocid,
            Fprice = e.Fprice,
            Ftaxrate = e.Ftaxrate,
            Ftaxprice = e.Ftaxprice,
            Fdiscountrate = e.Fdiscountrate,
            Ftaxamount = e.Ftaxamount,
            Famount = e.Famount,
            Fallamount = e.Fallamount,
            Fpredeliverydate = e.Fpredeliverydate,
            Fkfdate = e.Fkfdate,
            Fexpiredate = e.Fexpiredate,
            Forderbillno = e.Forderbillno,
            Forderentryid = e.Forderentryid,
            Forderinterid = e.Forderinterid,
            Forderdetailid = e.Forderdetailid,
            Frepnote = e.Frepnote
        }).ToList()
    };

    // ===== 写入映射 =====

    protected override TPurReceive MapToHeaderEntity(CreateReceiveNoticeRequest dto) => new()
    {
        Fbillno = string.IsNullOrWhiteSpace(dto.Fbillno) ? $"SLTZ{DateTime.Now:yyyyMMddHHmmss}" : dto.Fbillno,
        Fbilltypeid = dto.Fbilltypeid,
        Fdate = dto.Fdate ?? DateTime.Now,
        Fbusinesstype = dto.Fbusinesstype,
        Fsrcformid = dto.Fsrcformid,
        Fsrcbillno = dto.Fsrcbillno,
        Fdemandorgid = dto.Fdemandorgid,
        Fpurorgid = dto.Fpurorgid,
        Freceivedeptid = dto.Freceivedeptid,
        Fpurdeptid = dto.Fpurdeptid,
        Freceiverid = dto.Freceiverid,
        Fpurchaserid = dto.Fpurchaserid,
        Fsupplyid = dto.Fsupplyid,
        Fsettlecurrid = dto.Fsettlecurrid,
        Fexchangetypeid = dto.Fexchangetypeid,
        Fexchangerate = dto.Fexchangerate == 0 ? 1 : dto.Fexchangerate,
        Fnote = dto.Fnote,
        Fcheckdate = DateTime.MinValue,
        Fdisabledate = DateTime.MinValue,
        FStatus = 10
    };

    protected override void UpdateHeaderEntity(TPurReceive entity, UpdateReceiveNoticeRequest dto)
    {
        entity.Fbilltypeid = dto.Fbilltypeid;
        entity.Fdate = dto.Fdate ?? entity.Fdate;
        entity.Fbusinesstype = dto.Fbusinesstype;
        entity.Fsrcformid = dto.Fsrcformid;
        entity.Fsrcbillno = dto.Fsrcbillno;
        entity.Fdemandorgid = dto.Fdemandorgid;
        entity.Fpurorgid = dto.Fpurorgid;
        entity.Freceivedeptid = dto.Freceivedeptid;
        entity.Fpurdeptid = dto.Fpurdeptid;
        entity.Freceiverid = dto.Freceiverid;
        entity.Fpurchaserid = dto.Fpurchaserid;
        entity.Fsupplyid = dto.Fsupplyid;
        entity.Fsettlecurrid = dto.Fsettlecurrid;
        entity.Fexchangetypeid = dto.Fexchangetypeid;
        entity.Fexchangerate = dto.Fexchangerate == 0 ? 1 : dto.Fexchangerate;
        entity.Fnote = dto.Fnote;
    }

    protected override List<TPurReceiveEntry> MapToEntryEntities(CreateReceiveNoticeRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    protected override List<TPurReceiveEntry> MapToEntryEntities(UpdateReceiveNoticeRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    private static List<TPurReceiveEntry> MapEntries(List<CreateReceiveNoticeEntryRequest> entries, string headerUid)
        => entries.Select(e =>
        {
            // 金额以服务端公式为准，不信任前端传值：金额=交货数量×单价×(1-折扣率%)、税额=金额×税率%、价税合计=金额+税额、含税单价=单价×(1+税率%)
            var amount = Math.Round(e.Factreceiveqty * e.Fprice * (1 - e.Fdiscountrate / 100m), 2);
            var taxAmount = Math.Round(amount * e.Ftaxrate / 100m, 2);
            return new TPurReceiveEntry
            {
                FInterId = headerUid,
                Fmaterialid = e.Fmaterialid,
                Fauxpropid = e.Fauxpropid,
                Fcheckincoming = e.Fcheckincoming,
                Flot = e.Flot,
                Fsupplylot = e.Fsupplylot,
                Factreceiveqty = e.Factreceiveqty,
                Fgodqty = e.Fgodqty,
                Fscrapqty = e.Fscrapqty,
                Funitid = e.Funitid,
                // 基本单位/数量：当前未接入物料换算率，按 1:1 回落（已知简化，待换算率主数据接入后完善）
                Fbaseunitid = string.IsNullOrEmpty(e.Fbaseunitid) ? e.Funitid : e.Fbaseunitid,
                Fbaseunitqty = e.Fbaseunitqty == 0 ? e.Factreceiveqty : e.Fbaseunitqty,
                Fstockid = e.Fstockid,
                Fstocklocid = e.Fstocklocid,
                Fprice = e.Fprice,
                Ftaxrate = e.Ftaxrate,
                Ftaxprice = Math.Round(e.Fprice * (1 + e.Ftaxrate / 100m), 6),
                Fdiscountrate = e.Fdiscountrate,
                Ftaxamount = taxAmount,
                Famount = amount,
                Fallamount = amount + taxAmount,
                Fpredeliverydate = e.Fpredeliverydate ?? DateTime.MinValue,
                Fkfdate = e.Fkfdate ?? DateTime.MinValue,
                Fexpiredate = e.Fexpiredate ?? DateTime.MinValue,
                Forderbillno = e.Forderbillno,
                Forderentryid = e.Forderentryid,
                Forderinterid = e.Forderinterid,
                Forderdetailid = e.Forderdetailid,
                Frepnote = e.Frepnote
            };
        }).ToList();

    protected override void SetEntryIndex(TPurReceiveEntry entry, int index)
    {
        entry.FENTRYID = index;
        entry.FDETAILID = entry.Uid;
    }

    protected override async Task<List<TPurReceiveEntry>> GetEntriesByHeaderIdAsync(string headerUid)
        => await Db.Queryable<TPurReceiveEntry>()
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

    // 仓位（FSTOCKLOCID）解析自仓位主数据 TBdStockPlace（前端选择器用 module=stockplace，按仓库级联），
    // 与选择器同源以保证存取一致。
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
