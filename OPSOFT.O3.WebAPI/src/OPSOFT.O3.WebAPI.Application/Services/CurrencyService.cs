using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class CurrencyService : ApprovableDisableableCrudService<TBdCurrency, CurrencyListDto, CurrencyDetailDto, CreateCurrencyRequest, UpdateCurrencyRequest>
{
    private readonly IRepository<SysBaseDataGroup> _groupRepo;
    private readonly IRepository<SysOrgStructure> _orgRepo;

    public CurrencyService(
        IRepository<TBdCurrency> repository,
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

    protected override string PrgKey => "Currency";

    protected override Expression<Func<TBdCurrency, bool>> BuildSearchPredicate(string keyword)
    {
        return c => c.FNumber.Contains(keyword) || c.FCode.Contains(keyword) || c.FName.Contains(keyword);
    }

    public override async Task<CurrencyDetailDto?> GetByIdAsync(string uid)
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

    public override async Task<CurrencyDetailDto> CreateAsync(CreateCurrencyRequest request)
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
        FGroupId = entity.FGroupId,
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

    protected override TBdCurrency MapToEntity(CreateCurrencyRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FCode = dto.FCode,
        FName = dto.FName,
        FCompanyId = dto.FCompanyId,
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
