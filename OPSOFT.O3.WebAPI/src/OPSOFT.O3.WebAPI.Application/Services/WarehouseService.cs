using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class WarehouseService : ApprovableDisableableCrudService<TBdStock, WarehouseListDto, WarehouseDetailDto, CreateWarehouseRequest, UpdateWarehouseRequest>
{
    private readonly IRepository<SysBaseDataGroup> _groupRepo;
    private readonly IRepository<SysOrgStructure> _orgRepo;
    private readonly IRepository<TBdStockflexitem> _flexItemRepo;
    private readonly IRepository<TBdStockflexdetail> _flexDetailRepo;
    private readonly IRepository<TBasFlexvalues> _flexValuesRepo;
    private readonly IRepository<TBasFlexvaluesentry> _flexValuesEntryRepo;
    private readonly ISqlSugarClient _db;

    public WarehouseService(
        IRepository<TBdStock> repository,
        IRepository<SysBaseDataGroup> groupRepo,
        IRepository<SysOrgStructure> orgRepo,
        IRepository<TBdStockflexitem> flexItemRepo,
        IRepository<TBdStockflexdetail> flexDetailRepo,
        IRepository<TBasFlexvalues> flexValuesRepo,
        IRepository<TBasFlexvaluesentry> flexValuesEntryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog)
    {
        _groupRepo = groupRepo;
        _orgRepo = orgRepo;
        _flexItemRepo = flexItemRepo;
        _flexDetailRepo = flexDetailRepo;
        _flexValuesRepo = flexValuesRepo;
        _flexValuesEntryRepo = flexValuesEntryRepo;
        _db = db;
    }

    protected override string PrgKey => "Warehouse";

    protected override Expression<Func<TBdStock, bool>> BuildSearchPredicate(string keyword)
    {
        return w => w.FNumber.Contains(keyword) || w.FName.Contains(keyword);
    }

    public override async Task<WarehouseDetailDto?> GetByIdAsync(string uid)
    {
        var detail = await base.GetByIdAsync(uid);
        if (detail == null) return null;

        // 分组名称
        if (!string.IsNullOrEmpty(detail.FGroupId))
        {
            var group = await _groupRepo.GetByIdAsync(detail.FGroupId);
            if (group != null) detail.FGroupName = group.Fname;
        }

        // 使用组织（公司）名称
        if (!string.IsNullOrEmpty(detail.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(detail.FCompanyId);
            if (org != null) detail.FCompanyName = org.Fname;
        }

        return detail;
    }

    public override async Task<WarehouseDetailDto> CreateAsync(CreateWarehouseRequest request)
    {
        // 使用组织(FCompanyId)记录所选组织的上级(SysOrgStructure.FPARAID)；无上级则记录其自身
        if (!string.IsNullOrEmpty(request.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(request.FCompanyId);
            if (org != null)
                request.FCompanyId = !string.IsNullOrEmpty(org.Fparaid) ? org.Fparaid : org.Uid;
        }
        return await base.CreateAsync(request);
    }

    public override async Task<bool> UpdateAsync(string uid, UpdateWarehouseRequest request)
    {
        // 取旧值判断"启用仓位管理"是否由 true→false
        var existing = await Repository.GetByIdAsync(uid);
        var wasOpenLocation = existing != null && existing.FIsOpenLocation;

        var result = await base.UpdateAsync(uid, request);

        // 关闭仓位管理时联动清理其仓位集/值，避免残留数据下次重开时"复活"
        if (result && wasOpenLocation && !request.FIsOpenLocation && existing != null)
        {
            var linkKey = ResolveLinkKey(existing);
            try
            {
                _db.Ado.BeginTran();
                await _db.Deleteable<TBdStockflexitem>().Where(x => x.FInterId == linkKey).ExecuteCommandAsync();
                await _db.Deleteable<TBdStockflexdetail>().Where(x => x.FInterId == linkKey).ExecuteCommandAsync();
                _db.Ado.CommitTran();
            }
            catch
            {
                _db.Ado.RollbackTran();
                throw;
            }
        }
        return result;
    }

    protected override WarehouseListDto MapToListDto(TBdStock entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FPrincipal = entity.FPrincipal,
        FTel = entity.FTel,
        FType = entity.FType,
        FAddress = entity.FAddress,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FStockProperty = entity.FStockProperty,
        FAllowMinusQty = entity.FAllowMinusQty,
        FIsOpenLocation = entity.FIsOpenLocation,
        FBonded = entity.FBonded,
        FAllowMrpPlan = entity.FAllowMrpPlan,
        FAllowLock = entity.FAllowLock,
        FAvailableAlert = entity.FAvailableAlert,
        FAvailablePicking = entity.FAvailablePicking,
        FSortingPriority = entity.FSortingPriority,
        FIsVirtual = entity.FIsVirtual,
        ErpNumber = entity.ErpNumber,
        FGroupId = entity.FGroupId
    };

    protected override WarehouseDetailDto MapToDetailDto(TBdStock entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FPrincipal = entity.FPrincipal,
        FTel = entity.FTel,
        FType = entity.FType,
        FAddress = entity.FAddress,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FStockProperty = entity.FStockProperty,
        FAllowMinusQty = entity.FAllowMinusQty,
        FIsOpenLocation = entity.FIsOpenLocation,
        FBonded = entity.FBonded,
        FAllowMrpPlan = entity.FAllowMrpPlan,
        FAllowLock = entity.FAllowLock,
        FAvailableAlert = entity.FAvailableAlert,
        FAvailablePicking = entity.FAvailablePicking,
        FSortingPriority = entity.FSortingPriority,
        FIsVirtual = entity.FIsVirtual,
        ErpNumber = entity.ErpNumber,
        FGroupId = entity.FGroupId,
        // ---- 库存状态 / 所在车间 ----
        FStockStatusType = entity.FStockStatusType,
        FDefStockStatusId = entity.FDefStockStatusId,
        FDefReceiveStatusId = entity.FDefReceiveStatusId,
        FWorkshopId = entity.FWorkshopId,
        // ---- 使用组织 / 审计（只读） ----
        FCompanyId = entity.FCompanyId,
        CUser = entity.CUser,
        MUser = entity.MUser,
        MYmd = entity.MYmd,
        FCheckerId = entity.FCheckerId,
        FCheckDate = entity.FCheckDate,
        Fdisableid = entity.Fdisableid,
        Fdisabledate = entity.Fdisabledate
    };

    protected override TBdStock MapToEntity(CreateWarehouseRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FCompanyId = dto.FCompanyId,
        FDescription = dto.FDescription,
        FPrincipal = dto.FPrincipal,
        FTel = dto.FTel,
        FType = dto.FType,
        FAddress = dto.FAddress,
        FStockProperty = dto.FStockProperty,
        FStockStatusType = dto.FStockStatusType,
        FDefStockStatusId = dto.FDefStockStatusId,
        FDefReceiveStatusId = dto.FDefReceiveStatusId,
        FWorkshopId = dto.FWorkshopId,
        FBonded = dto.FBonded,
        FAllowMinusQty = dto.FAllowMinusQty,
        FIsOpenLocation = dto.FIsOpenLocation,
        FAllowMrpPlan = dto.FAllowMrpPlan,
        FAllowLock = dto.FAllowLock,
        FAvailableAlert = dto.FAvailableAlert,
        FIsVirtual = dto.FIsVirtual,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdStock entity, UpdateWarehouseRequest dto)
    {
        // 仅更新设计图维护的字段；设计外字段（FAvailablePicking/FSortingPriority/ErpNumber 等）保持不变
        entity.FName = dto.FName;
        entity.FDescription = dto.FDescription;
        entity.FPrincipal = dto.FPrincipal;
        entity.FTel = dto.FTel;
        entity.FType = dto.FType;
        entity.FAddress = dto.FAddress;
        entity.FStockProperty = dto.FStockProperty;
        entity.FStockStatusType = dto.FStockStatusType;
        entity.FDefStockStatusId = dto.FDefStockStatusId;
        entity.FDefReceiveStatusId = dto.FDefReceiveStatusId;
        entity.FWorkshopId = dto.FWorkshopId;
        entity.FBonded = dto.FBonded;
        entity.FAllowMinusQty = dto.FAllowMinusQty;
        entity.FIsOpenLocation = dto.FIsOpenLocation;
        entity.FAllowMrpPlan = dto.FAllowMrpPlan;
        entity.FAllowLock = dto.FAllowLock;
        entity.FAvailableAlert = dto.FAvailableAlert;
        entity.FIsVirtual = dto.FIsVirtual;
        entity.FGroupId = dto.FGroupId;
    }

    // ============ 仓位信息（两级主从：仓位集 TBdStockflexitem → 仓位集值 TBdStockflexdetail，按 FInterId 关联仓库）============

    /// <summary>子表关联键：优先用仓库 FInterId（app 数据 FInterId==Uid），为空回退 Uid</summary>
    private static string ResolveLinkKey(TBdStock stock)
        => string.IsNullOrEmpty(stock.FInterId) ? stock.Uid : stock.FInterId;

    public async Task<List<WarehouseFlexItemDto>> GetFlexAsync(string warehouseUid)
    {
        var stock = await Repository.GetByIdAsync(warehouseUid);
        if (stock == null) return new List<WarehouseFlexItemDto>();
        var linkKey = ResolveLinkKey(stock);

        var items = await _flexItemRepo.GetListAsync(x => x.FInterId == linkKey);
        var details = await _flexDetailRepo.GetListAsync(x => x.FInterId == linkKey);

        // 仓位集 代码/名称/描述：JOIN T_BAS_FLEXVALUES（FFLEXID == FINTERID）
        var flexIds = items.Where(i => !string.IsNullOrEmpty(i.Fflexid)).Select(i => i.Fflexid).Distinct().ToList();
        var flexDict = flexIds.Count > 0
            ? (await _flexValuesRepo.GetListAsync(v => flexIds.Contains(v.FInterId))).ToDictionary(v => v.FInterId)
            : new Dictionary<string, TBasFlexvalues>();

        // 仓位集值 代码/名称/描述：JOIN T_BAS_FLEXVALUESENTRY（FFLEXENTRYID == FDETAILID）
        var entryIds = details.Where(d => !string.IsNullOrEmpty(d.Fflexentryid)).Select(d => d.Fflexentryid).Distinct().ToList();
        var entryDict = entryIds.Count > 0
            ? (await _flexValuesEntryRepo.GetListAsync(e => entryIds.Contains(e.Fdetailid)))
                .GroupBy(e => e.Fdetailid).ToDictionary(g => g.Key, g => g.First())
            : new Dictionary<string, TBasFlexvaluesentry>();

        // 按父阶表体内码(FBodyid == 仓位集 FDetailid)归组仓位集值
        var detailsByBody = details
            .OrderBy(d => d.Fentryid)
            .GroupBy(d => d.Fbodyid)
            .ToDictionary(g => g.Key, g => g.ToList());

        return items.OrderBy(i => i.Fentryid).Select(i =>
        {
            flexDict.TryGetValue(i.Fflexid, out var fv);
            return new WarehouseFlexItemDto
            {
                Uid = i.Uid,
                FEntryId = i.Fentryid,
                FFlexId = i.Fflexid,
                FNumber = fv?.Fnumber ?? string.Empty,
                FName = fv?.Fname ?? string.Empty,
                FDescription = fv?.Fdescription ?? string.Empty,
                FIsMustInput = i.Fismustinput,
                Details = detailsByBody.TryGetValue(i.Fdetailid, out var ds)
                    ? ds.Select(d =>
                    {
                        entryDict.TryGetValue(d.Fflexentryid, out var fe);
                        return new WarehouseFlexDetailDto
                        {
                            Uid = d.Uid,
                            FEntryId = d.Fentryid,
                            FFlexEntryId = d.Fflexentryid,
                            FNumber = fe?.Fnumber ?? string.Empty,
                            FName = fe?.Fname ?? string.Empty,
                            FDescription = fe?.Fdescription ?? string.Empty
                        };
                    }).ToList()
                    : new List<WarehouseFlexDetailDto>()
            };
        }).ToList();
    }

    public async Task SaveFlexAsync(string warehouseUid, List<SaveWarehouseFlexItem> items)
    {
        var stock = await Repository.GetByIdAsync(warehouseUid);
        if (stock == null) throw new KeyNotFoundException("仓库不存在");
        var linkKey = ResolveLinkKey(stock);

        try
        {
            _db.Ado.BeginTran();
            // 整组替换：硬删旧的仓位集与仓位集值
            await _db.Deleteable<TBdStockflexitem>().Where(x => x.FInterId == linkKey).ExecuteCommandAsync();
            await _db.Deleteable<TBdStockflexdetail>().Where(x => x.FInterId == linkKey).ExecuteCommandAsync();

            var itemIndex = 1;
            foreach (var it in items ?? new List<SaveWarehouseFlexItem>())
            {
                // 跳过未选择仓位集的空行
                if (string.IsNullOrWhiteSpace(it.FFlexId)) continue;

                var detailId = Guid.NewGuid().ToString("N");
                await _flexItemRepo.InsertAsync(new TBdStockflexitem
                {
                    FInterId = linkKey,
                    Fentryid = itemIndex++,
                    Fdetailid = detailId,
                    Fflexid = it.FFlexId,
                    Fismustinput = it.FIsMustInput
                });

                var detailIndex = 1;
                foreach (var d in it.Details ?? new List<SaveWarehouseFlexDetailItem>())
                {
                    if (string.IsNullOrWhiteSpace(d.FFlexEntryId)) continue;
                    await _flexDetailRepo.InsertAsync(new TBdStockflexdetail
                    {
                        FInterId = linkKey,
                        Fbodyid = detailId,
                        Fentryid = detailIndex++,
                        Fdetailid = Guid.NewGuid().ToString("N"),
                        Fflexentryid = d.FFlexEntryId
                    });
                }
            }
            _db.Ado.CommitTran();
        }
        catch
        {
            _db.Ado.RollbackTran();
            throw;
        }
    }

    // ---- 仓位集 / 仓位集值 下拉（来自主表 T_BAS_FLEXVALUES / T_BAS_FLEXVALUESENTRY）----

    /// <summary>仓位集下拉：值(Uid)用 FLEXVALUES.FINTERID（即 STOCKFLEXITEM.FFLEXID 的关联键）</summary>
    public async Task<List<LookupDto>> GetFlexSetLookupAsync(string? keyword)
    {
        var list = await _flexValuesRepo.GetListAsync(v => !v.FDeleted && !v.FDisabled);
        var query = list.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var kw = keyword.Trim();
            query = query.Where(v => v.Fnumber.Contains(kw) || v.Fname.Contains(kw));
        }
        return query.OrderBy(v => v.Fnumber).Take(200).Select(v => new LookupDto
        {
            Uid = v.FInterId,
            FNumber = v.Fnumber,
            FName = v.Fname
        }).ToList();
    }

    /// <summary>仓位集值下拉：按所选仓位集(parentFlexId == FLEXVALUESENTRY.FINTERID)过滤；值(Uid)用 FDETAILID</summary>
    public async Task<List<LookupDto>> GetFlexSetValueLookupAsync(string parentFlexId, string? keyword)
    {
        if (string.IsNullOrWhiteSpace(parentFlexId)) return new List<LookupDto>();
        var list = await _flexValuesEntryRepo.GetListAsync(e => e.FInterId == parentFlexId && !e.FDeleted && !e.FDisabled && !e.Fforbid);
        var query = list.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var kw = keyword.Trim();
            query = query.Where(e => e.Fnumber.Contains(kw) || e.Fname.Contains(kw));
        }
        return query.OrderBy(e => e.Fentryid).Take(500).Select(e => new LookupDto
        {
            Uid = e.Fdetailid,
            FNumber = e.Fnumber,
            FName = e.Fname
        }).ToList();
    }
}
