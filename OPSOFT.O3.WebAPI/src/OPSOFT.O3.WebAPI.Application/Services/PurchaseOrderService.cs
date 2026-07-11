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
/// 采购订单服务（真实表 T_PUR_POORDER / T_PUR_POORDERENTRY）
/// 列表按明细行展开并解析名称；详情解析主表全部外键名称。
/// 状态：10=草稿(未审)、40=审核(已审)。
/// </summary>
public class PurchaseOrderService : DocumentService<TPurPoOrder, TPurPoOrderEntry,
    PurchaseOrderListDto, PurchaseOrderDetailDto, CreatePurchaseOrderRequest, UpdatePurchaseOrderRequest>
{
    public PurchaseOrderService(
        IRepository<TPurPoOrder> headerRepo,
        IRepository<TPurPoOrderEntry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IBillCodeService billCode,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog, billCode)
    {
    }

    protected override string PrgKey => "PurchaseOrder";

    // ===== 审核 / 反审核 / 关闭 =====

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("单据已审核，无需重复审核");

        var result = await Db.Updateable<TPurPoOrder>()
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

        var result = await Db.Updateable<TPurPoOrder>()
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

        var result = await Db.Updateable<TPurPoOrder>()
            .SetColumns(h => h.FStatus == 70)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<TPurPoOrder, bool>> BuildSearchPredicate(string keyword)
        => h => h.Fbillno.Contains(keyword) || h.Fsupplyid.Contains(keyword);

    // 已审核单据禁止修改/删除（前端只读外的后端兜底，须先反审核）
    public override async Task<bool> UpdateAsync(string uid, UpdatePurchaseOrderRequest request)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的单据不能修改，请先反审核");
        return await base.UpdateAsync(uid, request);
    }

    public override async Task<bool> DeleteAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的单据不能删除，请先反审核");
        return await base.DeleteAsync(uid);
    }

    // ===== 列表：按明细行展开 + 名称解析 =====

    // 高级筛选中归属"表头"的字段（其余字段按"明细"处理）；解析出来的名称列不参与服务端筛选
    private static readonly HashSet<string> HeaderFilterFields = new(StringComparer.OrdinalIgnoreCase) { "fbillno", "fdate", "fStatus" };

    public override async Task<PagedResult<PurchaseOrderListDto>> GetPagedListAsync(PagedRequest request)
    {
        // 拆分动态筛选：表头字段(单据编号/订单日期/审核状态)走表头预筛，明细字段(采购数量/供应商批号)走明细
        var filters = request.DynamicFilters ?? new List<DynamicFilterInfo>();
        var headerFilters = filters.Where(f => HeaderFilterFields.Contains(f.Field)).ToList();
        var entryFilters = filters.Where(f => !HeaderFilterFields.Contains(f.Field)).ToList();

        // 1) 表头条件（关键字 + 表头动态筛选 + 仅已审核）-> 命中的主表 FInterId 集合
        //    明细↔主表按 FInterId 关联（entry.FInterId == header.FInterId），兼容 ERP 同步数据
        //    （主表 Uid 为新 GUID、FInterId 为 ERP 内码）与手工单据（Uid==FInterId）。
        List<string>? headerIds = null;
        if (!string.IsNullOrWhiteSpace(request.Keyword) || headerFilters.Count > 0 || request.OnlyApproved)
        {
            var hq = Db.Queryable<TPurPoOrder>().Where(h => !h.FDeleted);
            if (request.OnlyApproved)  // 作为源单选择器时，仅取已审核(40)且非禁用的采购订单
                hq = hq.Where(h => h.FStatus == 40 && !h.FDisabled);
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var kw = request.Keyword.Trim();
                hq = hq.Where(h => h.Fbillno.Contains(kw));
            }
            if (headerFilters.Count > 0)
                hq = hq.Where(headerFilters.ToConditionalModels<TPurPoOrder>());
            headerIds = await hq.Select(h => h.FInterId).ToListAsync();
            if (headerIds.Count == 0)
                return new PagedResult<PurchaseOrderListDto> { Items = new(), TotalCount = 0, PageIndex = request.PageIndex, PageSize = request.PageSize };
        }

        // 2) 分页查询明细（+ 明细动态筛选）
        RefAsync<int> totalCount = 0;
        var query = Db.Queryable<TPurPoOrderEntry>().Where(e => !e.FDeleted);
        if (headerIds != null)
            query = query.Where(e => headerIds.Contains(e.FInterId));
        if (entryFilters.Count > 0)
            query = query.Where(entryFilters.ToConditionalModels<TPurPoOrderEntry>());
        var entries = await query
            .OrderBy(e => e.CYmd, OrderByType.Desc)
            .OrderBy(e => e.FENTRYID)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        if (entries.Count == 0)
            return new PagedResult<PurchaseOrderListDto> { Items = new(), TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };

        // 3) 批量加载主表 + 名称源（明细↔主表按 FInterId 关联）
        var hids = entries.Select(e => e.FInterId).Distinct().ToList();
        var headers = await Db.Queryable<TPurPoOrder>().Where(h => hids.Contains(h.FInterId)).ToListAsync();
        var headerDict = headers.GroupBy(h => h.FInterId).ToDictionary(g => g.Key, g => g.First());

        var materialDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
        var unitDict = await LoadUnitDictAsync(entries.Select(e => e.Funitid));
        var supplierDict = await LoadSupplierDictAsync(headers.Select(h => h.Fsupplyid));
        var purchaserDict = await LoadEmployeeNameDictAsync(headers.Select(h => h.Fpurchaserid));
        var statusDict = await LoadStatusDictAsync();
        var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));

        // 4) 映射
        var items = entries.Select(e =>
        {
            headerDict.TryGetValue(e.FInterId, out var h);
            materialDict.TryGetValue(e.Fmaterialid ?? string.Empty, out var mat);
            unitDict.TryGetValue(e.Funitid ?? string.Empty, out var unit);
            var dto = new PurchaseOrderListDto
            {
                Uid = h?.Uid ?? e.FInterId,
                EntryUid = e.Uid,
                Fbillno = h?.Fbillno ?? string.Empty,
                Fdate = h?.Fdate,
                Fentryid = e.FENTRYID,
                FStatus = h?.FStatus ?? 0,
                FstatusName = statusDict.GetValueOrDefault(h?.FStatus ?? 0, string.Empty),
                Fpurchaserid = h?.Fpurchaserid ?? string.Empty,
                FpurchaserName = purchaserDict.GetValueOrDefault(h?.Fpurchaserid ?? string.Empty, string.Empty),
                Fmaterialid = e.Fmaterialid,
                FmaterialNumber = mat.Number,
                FmaterialName = mat.Name,
                FSpecification = mat.Spec,
                FauxpropName = flexDict.GetValueOrDefault(e.Fauxpropid ?? string.Empty, string.Empty),
                Fqty = e.Fqty,
                FunitName = unit.Name,
                FsupplyNumber = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Number : string.Empty,
                FsupplyName = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Name : string.Empty,
                Fsupplierlot = e.Fsupplierlot
            };
            return dto;
        }).ToList();

        return new PagedResult<PurchaseOrderListDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    // 抽象成员（列表走上面的重写，此处为契约的简化实现）
    protected override PurchaseOrderListDto MapToListDto(TPurPoOrder entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdate = entity.Fdate,
        FStatus = entity.FStatus,
        Fpurchaserid = entity.Fpurchaserid,
        Fsupplierlot = string.Empty
    };

    // ===== 详情：主表 + 名称解析 + 明细 =====

    public override async Task<PurchaseOrderDetailDto?> GetByIdAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return null;

        // 明细按 header.FInterId 关联（entry.FInterId == header.FInterId）：
        // 手工单据 Uid==FInterId 等价；ERP 同步数据主表 Uid(GUID)≠FInterId(ERP内码)时须用 FInterId 才能取到明细。
        var entries = await GetEntriesByHeaderIdAsync(header.FInterId);
        var dto = MapToDetailDto(header, entries);

        // 主表名称
        dto.FbilltypeName = await Db.Queryable<TBasBilltype>().Where(b => b.Uid == header.Fbilltypeid || b.FInterId == header.Fbilltypeid).Select(b => b.Fname).FirstAsync() ?? string.Empty;
        var supplier = await LoadSupplierDictAsync(new[] { header.Fsupplyid });
        if (supplier.TryGetValue(header.Fsupplyid, out var sp)) { dto.FsupplyNumber = sp.Number; dto.FsupplyName = sp.Name; }
        dto.FpurchaserName = (await LoadEmployeeNameDictAsync(new[] { header.Fpurchaserid })).GetValueOrDefault(header.Fpurchaserid, string.Empty);
        dto.FpurchasedeptName = await Db.Queryable<TBdDepartment>().Where(d => d.Uid == header.Fpurchasedeptid || d.FInterId == header.Fpurchasedeptid).Select(d => d.FName).FirstAsync() ?? string.Empty;
        var currency = await Db.Queryable<TBdCurrency>().Where(c => c.Uid == header.Fsettlecurrid || c.FInterId == header.Fsettlecurrid).Select(c => new { c.FNumber, c.FName }).FirstAsync();
        if (currency != null) { dto.FcurrencyNumber = currency.FNumber; dto.FcurrencyName = currency.FName; }
        dto.FcompanyName = await Db.Queryable<SysOrgStructure>().Where(o => o.Uid == header.FCompanyId || o.FInterId == header.FCompanyId).Select(o => o.Fname).FirstAsync() ?? string.Empty;
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
            var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));

            // 物料 Uid -> {Uid, FInterId}，并求出"启用辅助属性"的物料关联键集合
            var matIdList = entries.Select(e => e.Fmaterialid).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            var matKeyMap = new Dictionary<string, string[]>();
            var auxEnabledKeys = new HashSet<string>();
            if (matIdList.Count > 0)
            {
                var mats = await Db.Queryable<TBdMaterial>().Where(m => matIdList.Contains(m.Uid) || matIdList.Contains(m.FInterId))
                    .Select(m => new { m.Uid, m.FInterId }).ToListAsync();
                matKeyMap = BuildByUidOrFInterId(mats, m => m.Uid, m => m.FInterId, m => new[] { m.Uid, m.FInterId });
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
                line.FauxpropName = flexDict.GetValueOrDefault(line.Fauxpropid ?? string.Empty, string.Empty);
                if (matKeyMap.TryGetValue(line.Fmaterialid ?? string.Empty, out var ks))
                    line.FisAuxEnabled = ks.Any(k => !string.IsNullOrEmpty(k) && auxEnabledKeys.Contains(k));
            }
        }

        return dto;
    }

    protected override PurchaseOrderDetailDto MapToDetailDto(TPurPoOrder header, List<TPurPoOrderEntry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fbilltypeid = header.Fbilltypeid,
        Fdate = header.Fdate,
        FStatus = header.FStatus,
        Foastatus = header.Foastatus,
        Foaresult = header.Foaresult,
        Fbusinesstype = header.Fbusinesstype,
        Fcompanyid = header.FCompanyId,
        Fpurchasedeptid = header.Fpurchasedeptid,
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
        Entries = entries.Select(e => new PurchaseOrderEntryDto
        {
            Uid = e.Uid,
            Fentryid = e.FENTRYID,
            Fmaterialid = e.Fmaterialid,
            Flot = e.Flot,
            Fauxpropid = e.Fauxpropid,
            Fqty = e.Fqty,
            Finstockqty = e.Finstockqty,
            Funitid = e.Funitid,
            Ftaxprice = e.Ftaxprice,
            Ftaxrate = e.Ftaxrate,
            Fprice = e.Fprice,
            Fdiscountrate = e.Fdiscountrate,
            Ftaxamount = e.Ftaxamount,
            Famount = e.Famount,
            Fallamount = e.Fallamount,
            Fdeliverydate = e.Fdeliverydate,
            Fsupplierlot = e.Fsupplierlot,
            Fnote = e.Fnote
        }).ToList()
    };

    // ===== 写入映射 =====

    protected override TPurPoOrder MapToHeaderEntity(CreatePurchaseOrderRequest dto) => new()
    {
        Fbillno = dto.Fbillno?.Trim() ?? string.Empty, // 为空时由 PrepareHeaderForCreateAsync 按编码规则取号
        Fbilltypeid = dto.Fbilltypeid,
        Fdate = dto.Fdate ?? DateTime.Now,
        Fbusinesstype = dto.Fbusinesstype,
        FCompanyId = dto.Fcompanyid,  // 采购组织（为空时由基类回落当前登录组织）
        Fpurchasedeptid = dto.Fpurchasedeptid,
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

    // ===== 编码规则取号：声明 formKey + 编号读写/查重 + 来源字段，通用取号逻辑在基类 =====
    protected override string? BillCodeFormKey => BillCodeFormKeys.PurchaseOrder;
    protected override string GetBillNo(TPurPoOrder header) => header.Fbillno;
    protected override void SetBillNo(TPurPoOrder header, string billNo) => header.Fbillno = billNo;
    protected override Task<bool> BillNoExistsAsync(string billNo)
        => Db.Queryable<TPurPoOrder>().AnyAsync(h => h.Fbillno == billNo);

    /// <summary>采购订单取号来源字段：单据日期 + 单据类型编码 + 供应商编码</summary>
    protected override async Task PopulateBillCodeContextAsync(IDictionary<string, string> ctx, TPurPoOrder header, CreatePurchaseOrderRequest dto)
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

    protected override void UpdateHeaderEntity(TPurPoOrder entity, UpdatePurchaseOrderRequest dto)
    {
        entity.Fbilltypeid = dto.Fbilltypeid;
        entity.Fdate = dto.Fdate ?? entity.Fdate;
        entity.Fbusinesstype = dto.Fbusinesstype;
        if (!string.IsNullOrEmpty(dto.Fcompanyid)) entity.FCompanyId = dto.Fcompanyid;
        entity.Fpurchasedeptid = dto.Fpurchasedeptid;
        entity.Fpurchaserid = dto.Fpurchaserid;
        entity.Fsupplyid = dto.Fsupplyid;
        entity.Fsettlecurrid = dto.Fsettlecurrid;
        entity.Fexchangetypeid = dto.Fexchangetypeid;
        entity.Fexchangerate = dto.Fexchangerate == 0 ? 1 : dto.Fexchangerate;
        entity.Fnote = dto.Fnote;
    }

    protected override List<TPurPoOrderEntry> MapToEntryEntities(CreatePurchaseOrderRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    protected override List<TPurPoOrderEntry> MapToEntryEntities(UpdatePurchaseOrderRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    private static List<TPurPoOrderEntry> MapEntries(List<CreatePurchaseOrderEntryRequest> entries, string headerUid)
        => entries.Select(e => new TPurPoOrderEntry
        {
            FInterId = headerUid,
            Fmaterialid = e.Fmaterialid,
            Fauxpropid = e.Fauxpropid,
            Flot = e.Flot,
            Fqty = e.Fqty,
            Funitid = e.Funitid,
            Fbaseunitid = string.IsNullOrEmpty(e.Fbaseunitid) ? e.Funitid : e.Fbaseunitid,
            Ftaxprice = e.Ftaxprice,
            Ftaxrate = e.Ftaxrate,
            Fprice = e.Fprice,
            Fdiscountrate = e.Fdiscountrate,
            Ftaxamount = e.Ftaxamount,
            Famount = e.Famount,
            Fallamount = e.Fallamount,
            Fdeliverydate = e.Fdeliverydate ?? DateTime.MinValue,
            Fsupplierlot = e.Fsupplierlot,
            Fnote = e.Fnote
        }).ToList();

    protected override void SetEntryIndex(TPurPoOrderEntry entry, int index)
    {
        entry.FENTRYID = index;
        entry.FDETAILID = entry.Uid;
    }

    protected override async Task<List<TPurPoOrderEntry>> GetEntriesByHeaderIdAsync(string headerUid)
        => await Db.Queryable<TPurPoOrderEntry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.FENTRYID)
            .ToListAsync();

    // ===== 名称字典加载 =====

    // 明细/表头外键的取值可能是关联主档的 Uid（手工单据）或 FInterId（ERP同步数据，存的是 K3 内码），
    // 故名称解析一律按 Uid 或 FInterId 双向匹配，并把字典同时以两者为键，取值时任填其一都能命中。
    private static Dictionary<string, TVal> BuildByUidOrFInterId<TSrc, TVal>(
        IEnumerable<TSrc> rows, Func<TSrc, string> uid, Func<TSrc, string> fInterId, Func<TSrc, TVal> val)
    {
        var dict = new Dictionary<string, TVal>();
        foreach (var r in rows)
        {
            var v = val(r);
            var u = uid(r);
            var f = fInterId(r);
            if (!string.IsNullOrEmpty(u)) dict[u] = v;
            if (!string.IsNullOrEmpty(f)) dict[f] = v;
        }
        return dict;
    }

    private async Task<Dictionary<string, (string Number, string Name, string Spec, bool Batch, bool Kf, int KfPeriod, int KfUnit)>> LoadMaterialDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdMaterial>().Where(m => list.Contains(m.Uid) || list.Contains(m.FInterId))
            .Select(m => new { m.Uid, m.FInterId, m.FNumber, m.FName, m.FSpecification, m.FIsBatchManage, m.FIsKfPeriod, m.FKfPeriod, m.FKfUnit }).ToListAsync();
        return BuildByUidOrFInterId(rows, r => r.Uid, r => r.FInterId,
            r => (r.FNumber, r.FName, r.FSpecification, r.FIsBatchManage, r.FIsKfPeriod, r.FKfPeriod, r.FKfUnit));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadUnitDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdUnit>().Where(u => list.Contains(u.Uid) || list.Contains(u.FInterId))
            .Select(u => new { u.Uid, u.FInterId, u.FNumber, u.FName }).ToListAsync();
        return BuildByUidOrFInterId(rows, r => r.Uid, r => r.FInterId, r => (r.FNumber, r.FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadSupplierDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdSupplier>().Where(s => list.Contains(s.Uid) || list.Contains(s.FInterId))
            .Select(s => new { s.Uid, s.FInterId, s.FNumber, s.FName }).ToListAsync();
        return BuildByUidOrFInterId(rows, r => r.Uid, r => r.FInterId, r => (r.FNumber, r.FName));
    }

    private async Task<Dictionary<string, string>> LoadEmployeeNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<THrEmpinfo>().Where(e => list.Contains(e.Uid) || list.Contains(e.FInterId))
            .Select(e => new { e.Uid, e.FInterId, e.Fname }).ToListAsync();
        return BuildByUidOrFInterId(rows, r => r.Uid, r => r.FInterId, r => r.Fname);
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
        var rows = await Db.Queryable<TBdFlexauxproperty>().Where(f => list.Contains(f.Uid) || list.Contains(f.FInterId))
            .Select(f => new { f.Uid, f.FInterId, f.Fname }).ToListAsync();
        return BuildByUidOrFInterId(rows, r => r.Uid, r => r.FInterId, r => r.Fname);
    }
}
