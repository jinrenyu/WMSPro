using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class WarehouseService : ApprovableDisableableCrudService<TBdStock, WarehouseListDto, WarehouseDetailDto, CreateWarehouseRequest, UpdateWarehouseRequest>
{
    public WarehouseService(IRepository<TBdStock> repository, ISqlSugarClient db, ICurrentUserService currentUser, IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog) { }

    protected override string PrgKey => "Warehouse";

    protected override Expression<Func<TBdStock, bool>> BuildSearchPredicate(string keyword)
    {
        return w => w.FNumber.Contains(keyword) || w.FName.Contains(keyword);
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
        FGroupId = entity.FGroupId
    };

    protected override TBdStock MapToEntity(CreateWarehouseRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FDescription = dto.FDescription,
        FPrincipal = dto.FPrincipal,
        FTel = dto.FTel,
        FType = dto.FType,
        FAddress = dto.FAddress,
        FAllowMinusQty = dto.FAllowMinusQty,
        FIsOpenLocation = dto.FIsOpenLocation,
        FStockProperty = dto.FStockProperty,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdStock entity, UpdateWarehouseRequest dto)
    {
        entity.FName = dto.FName;
        entity.FDescription = dto.FDescription;
        entity.FPrincipal = dto.FPrincipal;
        entity.FTel = dto.FTel;
        entity.FType = dto.FType;
        entity.FAddress = dto.FAddress;
        entity.FAllowMinusQty = dto.FAllowMinusQty;
        entity.FIsOpenLocation = dto.FIsOpenLocation;
        entity.FStockProperty = dto.FStockProperty;
        entity.FGroupId = dto.FGroupId;
    }
}
