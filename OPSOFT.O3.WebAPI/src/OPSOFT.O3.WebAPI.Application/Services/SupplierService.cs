using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class SupplierService : ApprovableDisableableCrudService<TBdSupplier, SupplierListDto, SupplierDetailDto, CreateSupplierRequest, UpdateSupplierRequest>
{
    public SupplierService(IRepository<TBdSupplier> repository, ISqlSugarClient db, ICurrentUserService currentUser, IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog) { }

    protected override string PrgKey => "Supplier";

    protected override Expression<Func<TBdSupplier, bool>> BuildSearchPredicate(string keyword)
    {
        return s => s.FNumber.Contains(keyword) || s.FName.Contains(keyword);
    }

    protected override SupplierListDto MapToListDto(TBdSupplier entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FContact = entity.FContact,
        FPhone = entity.FPhone,
        FAddress = entity.FAddress,
        CYmd = entity.CYmd,
        FNote = entity.FNote,
        FGroupId = entity.FGroupId
    };

    protected override SupplierDetailDto MapToDetailDto(TBdSupplier entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FContact = entity.FContact,
        FPhone = entity.FPhone,
        FAddress = entity.FAddress,
        CYmd = entity.CYmd,
        FNote = entity.FNote,
        FGroupId = entity.FGroupId
    };

    protected override TBdSupplier MapToEntity(CreateSupplierRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FContact = dto.FContact,
        FPhone = dto.FPhone,
        FAddress = dto.FAddress,
        FNote = dto.FNote,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdSupplier entity, UpdateSupplierRequest dto)
    {
        entity.FName = dto.FName;
        entity.FContact = dto.FContact;
        entity.FPhone = dto.FPhone;
        entity.FAddress = dto.FAddress;
        entity.FNote = dto.FNote;
        entity.FGroupId = dto.FGroupId;
    }
}
