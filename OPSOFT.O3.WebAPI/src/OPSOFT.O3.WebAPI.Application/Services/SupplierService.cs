using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class SupplierService : ApprovableDisableableCrudService<TBdSupplier, SupplierListDto, SupplierDetailDto, CreateSupplierRequest, UpdateSupplierRequest>
{
    private readonly IRepository<SysBaseDataGroup> _groupRepo;
    private readonly IRepository<SysOrgStructure> _orgRepo;

    public SupplierService(
        IRepository<TBdSupplier> repository,
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

    protected override string PrgKey => "Supplier";

    protected override Expression<Func<TBdSupplier, bool>> BuildSearchPredicate(string keyword)
    {
        return s => s.FNumber.Contains(keyword) || s.FName.Contains(keyword)
            || s.FSHORTNAME.Contains(keyword) || s.FContact.Contains(keyword);
    }

    public override async Task<SupplierDetailDto?> GetByIdAsync(string uid)
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

    public override async Task<SupplierDetailDto> CreateAsync(CreateSupplierRequest request)
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

    protected override SupplierListDto MapToListDto(TBdSupplier entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FShortName = entity.FSHORTNAME,
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
        FShortName = entity.FSHORTNAME,
        FContact = entity.FContact,
        FPhone = entity.FPhone,
        FAddress = entity.FAddress,
        CYmd = entity.CYmd,
        FNote = entity.FNote,
        FGroupId = entity.FGroupId,
        // ---- 基本 ----
        FTaxRate = entity.FTAXRATE,
        FCountry = entity.FCOUNTRY,
        FProvincial = entity.FPROVINCIAL,
        FFax = entity.FFAX,
        FEmail = entity.FEMAIL,
        FBank = entity.FBANK,
        FAccount = entity.FACCOUNT,
        FEmpId = entity.FEMPID,
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

    protected override TBdSupplier MapToEntity(CreateSupplierRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FCompanyId = dto.FCompanyId,
        FGroupId = dto.FGroupId,
        FSHORTNAME = dto.FShortName,
        FTAXRATE = dto.FTaxRate,
        FCOUNTRY = dto.FCountry,
        FPROVINCIAL = dto.FProvincial,
        FAddress = dto.FAddress,
        FContact = dto.FContact,
        FPhone = dto.FPhone,
        FFAX = dto.FFax,
        FEMAIL = dto.FEmail,
        FBANK = dto.FBank,
        FACCOUNT = dto.FAccount,
        FEMPID = dto.FEmpId
    };

    protected override void UpdateEntity(TBdSupplier entity, UpdateSupplierRequest dto)
    {
        // 仅更新设计表单管理的字段；FNote/FTRADE/FPROVINCE/FZIP/FCITY/FTENDPERMIT/FEMPNAME/FEMPNUMBER/
        // FWEBSITE/FREGISTERADDRESS/FREGISTERCODE 等设计外字段保留原值
        entity.FName = dto.FName;
        entity.FGroupId = dto.FGroupId;
        entity.FSHORTNAME = dto.FShortName;
        entity.FTAXRATE = dto.FTaxRate;
        entity.FCOUNTRY = dto.FCountry;
        entity.FPROVINCIAL = dto.FProvincial;
        entity.FAddress = dto.FAddress;
        entity.FContact = dto.FContact;
        entity.FPhone = dto.FPhone;
        entity.FFAX = dto.FFax;
        entity.FEMAIL = dto.FEmail;
        entity.FBANK = dto.FBank;
        entity.FACCOUNT = dto.FAccount;
        entity.FEMPID = dto.FEmpId;
    }
}
