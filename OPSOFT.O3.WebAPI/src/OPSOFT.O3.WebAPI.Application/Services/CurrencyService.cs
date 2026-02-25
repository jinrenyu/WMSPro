using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class CurrencyService : ApprovableDisableableCrudService<TBdCurrency, CurrencyListDto, CurrencyDetailDto, CreateCurrencyRequest, UpdateCurrencyRequest>
{
    public CurrencyService(IRepository<TBdCurrency> repository, ISqlSugarClient db, ICurrentUserService currentUser, IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog) { }

    protected override string PrgKey => "Currency";

    protected override Expression<Func<TBdCurrency, bool>> BuildSearchPredicate(string keyword)
    {
        return c => c.FNumber.Contains(keyword) || c.FCode.Contains(keyword) || c.FName.Contains(keyword);
    }

    protected override CurrencyListDto MapToListDto(TBdCurrency entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FCode = entity.FCode,
        FName = entity.FName,
        FExchangeRate = entity.FExchangeRate,
        FPriceDigits = entity.FPriceDigits,
        FAmountDigits = entity.FAmountDigits,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FFixRate = entity.FFixRate,
        FUseOrgId = entity.FUseOrgId,
        FGroupId = entity.FGroupId
    };

    protected override CurrencyDetailDto MapToDetailDto(TBdCurrency entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FCode = entity.FCode,
        FName = entity.FName,
        FExchangeRate = entity.FExchangeRate,
        FPriceDigits = entity.FPriceDigits,
        FAmountDigits = entity.FAmountDigits,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FFixRate = entity.FFixRate,
        FUseOrgId = entity.FUseOrgId,
        FGroupId = entity.FGroupId
    };

    protected override TBdCurrency MapToEntity(CreateCurrencyRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FCode = dto.FCode,
        FName = dto.FName,
        FDescription = dto.FDescription,
        FPriceDigits = dto.FPriceDigits,
        FAmountDigits = dto.FAmountDigits,
        FFixRate = dto.FFixRate,
        FExchangeRate = dto.FExchangeRate,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdCurrency entity, UpdateCurrencyRequest dto)
    {
        entity.FName = dto.FName;
        entity.FCode = dto.FCode;
        entity.FDescription = dto.FDescription;
        entity.FPriceDigits = dto.FPriceDigits;
        entity.FAmountDigits = dto.FAmountDigits;
        entity.FFixRate = dto.FFixRate;
        entity.FExchangeRate = dto.FExchangeRate;
        entity.FGroupId = dto.FGroupId;
    }
}
