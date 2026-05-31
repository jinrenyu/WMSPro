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

    public WarehouseService(
        IRepository<TBdStock> repository,
        IRepository<SysBaseDataGroup> groupRepo,
        IRepository<SysOrgStructure> orgRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog)
    {
        _groupRepo = groupRepo;
        _orgRepo = orgRepo;
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
}
