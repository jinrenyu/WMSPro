using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class MaterialBarTypeService : ApprovableDisableableCrudService<TBdMaterialbartype, MaterialBarTypeListDto, MaterialBarTypeDetailDto, CreateMaterialBarTypeRequest, UpdateMaterialBarTypeRequest>
{
    public MaterialBarTypeService(IRepository<TBdMaterialbartype> repository, ISqlSugarClient db, ICurrentUserService currentUser, IOperationLogService? operationLog = null) : base(repository, db, currentUser, operationLog) { }

    protected override string PrgKey => "MaterialBarType";

    protected override Expression<Func<TBdMaterialbartype, bool>> BuildSearchPredicate(string keyword)
    {
        return e => e.Fmaterialnumber.Contains(keyword) || e.Fmaterialname.Contains(keyword);
    }

    protected override MaterialBarTypeListDto MapToListDto(TBdMaterialbartype entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        Fmaterialid = entity.Fmaterialid,
        Fbartype = entity.Fbartype,
        Fmaterialnumber = entity.Fmaterialnumber,
        Fmaterialname = entity.Fmaterialname,
        Fcheckdate = entity.FCheckDate,
        Fcheckerid = entity.FCheckerId,
        Fdisabledate = entity.Fdisabledate,
        Fdisableid = entity.Fdisableid,
        CYmd = entity.CYmd,
        FGroupId = entity.FGroupId
    };

    protected override MaterialBarTypeDetailDto MapToDetailDto(TBdMaterialbartype entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        Fmaterialid = entity.Fmaterialid,
        Fbartype = entity.Fbartype,
        Fmaterialnumber = entity.Fmaterialnumber,
        Fmaterialname = entity.Fmaterialname,
        Fcheckdate = entity.FCheckDate,
        Fcheckerid = entity.FCheckerId,
        Fdisabledate = entity.Fdisabledate,
        Fdisableid = entity.Fdisableid,
        CYmd = entity.CYmd,
        FGroupId = entity.FGroupId
    };

    protected override TBdMaterialbartype MapToEntity(CreateMaterialBarTypeRequest dto) => new()
    {
        Fmaterialid = dto.Fmaterialid,
        Fbartype = dto.Fbartype,
        Fmaterialnumber = dto.Fmaterialnumber,
        Fmaterialname = dto.Fmaterialname,
        FCheckDate = dto.Fcheckdate ?? DateTime.MinValue,
        FCheckerId = dto.Fcheckerid,
        Fdisabledate = dto.Fdisabledate ?? DateTime.MinValue,
        Fdisableid = dto.Fdisableid,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdMaterialbartype entity, UpdateMaterialBarTypeRequest dto)
    {
        entity.Fmaterialid = dto.Fmaterialid;
        entity.Fbartype = dto.Fbartype;
        entity.Fmaterialnumber = dto.Fmaterialnumber;
        entity.Fmaterialname = dto.Fmaterialname;
        entity.FCheckDate = dto.Fcheckdate ?? entity.FCheckDate;
        entity.FCheckerId = dto.Fcheckerid;
        entity.Fdisabledate = dto.Fdisabledate ?? entity.Fdisabledate;
        entity.Fdisableid = dto.Fdisableid;
        entity.FGroupId = dto.FGroupId;
    }
}
