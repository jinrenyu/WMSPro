using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class CustomerService : ApprovableDisableableCrudService<TBdCustomer, CustomerListDto, CustomerDetailDto, CreateCustomerRequest, UpdateCustomerRequest>
{
    private readonly IRepository<SysBaseDataGroup> _groupRepo;
    private readonly IRepository<SysOrgStructure> _orgRepo;

    public CustomerService(
        IRepository<TBdCustomer> repository,
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

    protected override string PrgKey => "Customer";

    protected override Expression<Func<TBdCustomer, bool>> BuildSearchPredicate(string keyword)
    {
        return c => c.FNumber.Contains(keyword) || c.FName.Contains(keyword) || c.FShortName.Contains(keyword);
    }

    public override async Task<CustomerDetailDto?> GetByIdAsync(string uid)
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

    public override async Task<CustomerDetailDto> CreateAsync(CreateCustomerRequest request)
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

    protected override CustomerListDto MapToListDto(TBdCustomer entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FShortName = entity.FShortName,
        FContact = entity.FContact,
        FPhone = entity.FPhone,
        FAddress = entity.FAddress,
        CYmd = entity.CYmd,
        FSeller = entity.FSeller,
        FSalDeptId = entity.FSalDeptId,
        FTradingCurrId = entity.FTradingCurrId,
        FCountry = entity.FCountry,
        FProvincial = entity.FProvincial,
        FCity = entity.FCity,
        FZip = entity.FZip,
        FWebSite = entity.FWebSite,
        FTel = entity.FTel,
        FFax = entity.FFax,
        FEmail = entity.FEmail,
        FBank = entity.FBank,
        FAccount = entity.FAccount,
        FLegalPerson = entity.FLegalPerson,
        FTaxRegisterCode = entity.FTaxRegisterCode,
        FNameEn = entity.FNameEn,
        FAddressEn = entity.FAddressEn,
        FNote = entity.FNote,
        FEmpId = entity.FEmpId,
        FGroupId = entity.FGroupId
    };

    protected override CustomerDetailDto MapToDetailDto(TBdCustomer entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FShortName = entity.FShortName,
        FContact = entity.FContact,
        FPhone = entity.FPhone,
        FAddress = entity.FAddress,
        CYmd = entity.CYmd,
        FSeller = entity.FSeller,
        FSalDeptId = entity.FSalDeptId,
        FTradingCurrId = entity.FTradingCurrId,
        FCountry = entity.FCountry,
        FProvincial = entity.FProvincial,
        FCity = entity.FCity,
        FZip = entity.FZip,
        FWebSite = entity.FWebSite,
        FTel = entity.FTel,
        FFax = entity.FFax,
        FEmail = entity.FEmail,
        FBank = entity.FBank,
        FAccount = entity.FAccount,
        FLegalPerson = entity.FLegalPerson,
        FTaxRegisterCode = entity.FTaxRegisterCode,
        FNameEn = entity.FNameEn,
        FAddressEn = entity.FAddressEn,
        FNote = entity.FNote,
        FEmpId = entity.FEmpId,
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

    protected override TBdCustomer MapToEntity(CreateCustomerRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FCompanyId = dto.FCompanyId,
        FGroupId = dto.FGroupId,
        FShortName = dto.FShortName,
        FNameEn = dto.FNameEn,
        FProvincial = dto.FProvincial,
        FCity = dto.FCity,
        FZip = dto.FZip,
        FAddress = dto.FAddress,
        FAddressEn = dto.FAddressEn,
        FContact = dto.FContact,
        FPhone = dto.FPhone,
        FFax = dto.FFax,
        FEmail = dto.FEmail,
        FBank = dto.FBank,
        FAccount = dto.FAccount,
        FEmpId = dto.FEmpId,
        FLegalPerson = dto.FLegalPerson,
        FWebSite = dto.FWebSite,
        FTaxRegisterCode = dto.FTaxRegisterCode,
        FTradingCurrId = dto.FTradingCurrId
    };

    protected override void UpdateEntity(TBdCustomer entity, UpdateCustomerRequest dto)
    {
        // 仅更新设计表单管理的字段；FSeller/FCountry/FTel/FNote/FSalDeptId 等设计外字段保留原值
        entity.FName = dto.FName;
        entity.FGroupId = dto.FGroupId;
        entity.FShortName = dto.FShortName;
        entity.FNameEn = dto.FNameEn;
        entity.FProvincial = dto.FProvincial;
        entity.FCity = dto.FCity;
        entity.FZip = dto.FZip;
        entity.FAddress = dto.FAddress;
        entity.FAddressEn = dto.FAddressEn;
        entity.FContact = dto.FContact;
        entity.FPhone = dto.FPhone;
        entity.FFax = dto.FFax;
        entity.FEmail = dto.FEmail;
        entity.FBank = dto.FBank;
        entity.FAccount = dto.FAccount;
        entity.FEmpId = dto.FEmpId;
        entity.FLegalPerson = dto.FLegalPerson;
        entity.FWebSite = dto.FWebSite;
        entity.FTaxRegisterCode = dto.FTaxRegisterCode;
        entity.FTradingCurrId = dto.FTradingCurrId;
    }
}
