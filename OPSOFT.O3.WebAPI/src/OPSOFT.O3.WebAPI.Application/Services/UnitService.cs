using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class UnitService : ApprovableDisableableCrudService<TBdUnit, UnitListDto, UnitDetailDto, CreateUnitRequest, UpdateUnitRequest>
{
    public UnitService(IRepository<TBdUnit> repository, ISqlSugarClient db, ICurrentUserService currentUser, IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog) { }

    protected override string PrgKey => "Unit";

    protected override Expression<Func<TBdUnit, bool>> BuildSearchPredicate(string keyword)
    {
        return u => u.FNumber.Contains(keyword) || u.FName.Contains(keyword);
    }

    protected override UnitListDto MapToListDto(TBdUnit entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FUnitGroupId = entity.FUnitGroupId,
        FIsBaseUnit = entity.FIsBaseUnit,
        FPrecision = entity.FPrecision,
        FCoefficient = entity.FCoefficient,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FRoundType = entity.FRoundType,
        FConvertType = entity.FConvertType,
        FGroupId = entity.FGroupId
    };

    protected override UnitDetailDto MapToDetailDto(TBdUnit entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FUnitGroupId = entity.FUnitGroupId,
        FIsBaseUnit = entity.FIsBaseUnit,
        FPrecision = entity.FPrecision,
        FCoefficient = entity.FCoefficient,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FRoundType = entity.FRoundType,
        FConvertType = entity.FConvertType,
        FGroupId = entity.FGroupId
    };

    protected override TBdUnit MapToEntity(CreateUnitRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FDescription = dto.FDescription,
        FUnitGroupId = dto.FUnitGroupId,
        FIsBaseUnit = dto.FIsBaseUnit,
        FPrecision = dto.FPrecision,
        FRoundType = dto.FRoundType,
        FConvertType = dto.FConvertType,
        FCoefficient = dto.FCoefficient,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdUnit entity, UpdateUnitRequest dto)
    {
        entity.FName = dto.FName;
        entity.FDescription = dto.FDescription;
        entity.FUnitGroupId = dto.FUnitGroupId;
        entity.FIsBaseUnit = dto.FIsBaseUnit;
        entity.FPrecision = dto.FPrecision;
        entity.FRoundType = dto.FRoundType;
        entity.FConvertType = dto.FConvertType;
        entity.FCoefficient = dto.FCoefficient;
        entity.FGroupId = dto.FGroupId;
    }
}
