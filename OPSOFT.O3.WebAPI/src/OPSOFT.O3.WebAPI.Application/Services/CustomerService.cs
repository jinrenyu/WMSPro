using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class CustomerService : ApprovableDisableableCrudService<TBdCustomer, CustomerListDto, CustomerDetailDto, CreateCustomerRequest, UpdateCustomerRequest>
{
    public CustomerService(IRepository<TBdCustomer> repository, ISqlSugarClient db, ICurrentUserService currentUser, IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog) { }

    protected override string PrgKey => "Customer";

    protected override Expression<Func<TBdCustomer, bool>> BuildSearchPredicate(string keyword)
    {
        return c => c.FNumber.Contains(keyword) || c.FName.Contains(keyword) || c.FShortName.Contains(keyword);
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
        FGroupId = entity.FGroupId
    };

    protected override TBdCustomer MapToEntity(CreateCustomerRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FShortName = dto.FShortName,
        FContact = dto.FContact,
        FPhone = dto.FPhone,
        FAddress = dto.FAddress,
        FSeller = dto.FSeller,
        FTradingCurrId = dto.FTradingCurrId,
        FCountry = dto.FCountry,
        FTel = dto.FTel,
        FEmail = dto.FEmail,
        FNote = dto.FNote,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdCustomer entity, UpdateCustomerRequest dto)
    {
        entity.FName = dto.FName;
        entity.FShortName = dto.FShortName;
        entity.FContact = dto.FContact;
        entity.FPhone = dto.FPhone;
        entity.FAddress = dto.FAddress;
        entity.FSeller = dto.FSeller;
        entity.FTradingCurrId = dto.FTradingCurrId;
        entity.FCountry = dto.FCountry;
        entity.FTel = dto.FTel;
        entity.FEmail = dto.FEmail;
        entity.FNote = dto.FNote;
        entity.FGroupId = dto.FGroupId;
    }
}
