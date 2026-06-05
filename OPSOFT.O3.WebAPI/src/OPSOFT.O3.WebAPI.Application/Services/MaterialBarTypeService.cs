using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class MaterialBarTypeService : ApprovableDisableableCrudService<TBdMaterialbartype, MaterialBarTypeListDto, MaterialBarTypeDetailDto, CreateMaterialBarTypeRequest, UpdateMaterialBarTypeRequest>
{
    private readonly IRepository<SysBaseDataGroup> _groupRepo;
    private readonly IRepository<SysOrgStructure> _orgRepo;
    private readonly IRepository<TBdMaterial> _materialRepo;
    private readonly IRepository<SysLoginUser> _userRepo;
    private readonly IRepository<SysStatus> _statusRepo;

    public MaterialBarTypeService(
        IRepository<TBdMaterialbartype> repository,
        IRepository<SysBaseDataGroup> groupRepo,
        IRepository<SysOrgStructure> orgRepo,
        IRepository<TBdMaterial> materialRepo,
        IRepository<SysLoginUser> userRepo,
        IRepository<SysStatus> statusRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog)
    {
        _groupRepo = groupRepo;
        _orgRepo = orgRepo;
        _materialRepo = materialRepo;
        _userRepo = userRepo;
        _statusRepo = statusRepo;
    }

    protected override string PrgKey => "MaterialBarType";

    protected override Expression<Func<TBdMaterialbartype, bool>> BuildSearchPredicate(string keyword)
    {
        return e => e.Fmaterialnumber.Contains(keyword) || e.Fmaterialname.Contains(keyword);
    }

    public override async Task<MaterialBarTypeDetailDto?> GetByIdAsync(string uid)
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

        // 物料规格型号 / 启用序列号：FMATERIALID = T_BD_MATERIAL.FINTERID（兼容历史按 Uid 存储）
        if (!string.IsNullOrEmpty(detail.Fmaterialid))
        {
            var material = await _materialRepo.AsQueryable()
                .Where(m => m.FInterId == detail.Fmaterialid || m.Uid == detail.Fmaterialid)
                .FirstAsync();
            if (material != null)
            {
                detail.FSpecification = material.FSpecification;
                detail.FFeedSn = material.FFEEDSN ?? false;
            }
        }

        // 制单人 / 修改人 / 审核人 / 禁用人 姓名：SYS_LOGINUSER.USER_ID -> USER_NAME
        var userIds = new[] { detail.CUser, detail.MUser, detail.Fcheckerid, detail.Fdisableid }
            .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (userIds.Count > 0)
        {
            var users = await _userRepo.AsQueryable().Where(u => userIds.Contains(u.UserId)).ToListAsync();
            var nameMap = users.GroupBy(u => u.UserId).ToDictionary(g => g.Key, g => g.First().UserName);
            detail.CUserName = nameMap.TryGetValue(detail.CUser, out var cn) ? cn : string.Empty;
            detail.MUserName = nameMap.TryGetValue(detail.MUser, out var mn) ? mn : string.Empty;
            detail.FCheckerName = nameMap.TryGetValue(detail.Fcheckerid, out var ckn) ? ckn : string.Empty;
            detail.FDisableName = nameMap.TryGetValue(detail.Fdisableid, out var dn) ? dn : string.Empty;
        }

        // 单据状态名称：SYS_STATUS.FITEMID -> FNAME
        var status = await _statusRepo.AsQueryable().Where(s => s.Fitemid == detail.FStatus).FirstAsync();
        if (status != null) detail.FStatusName = status.Fname;

        return detail;
    }

    public override async Task<MaterialBarTypeDetailDto> CreateAsync(CreateMaterialBarTypeRequest request)
    {
        // 物料：前端传所选物料 Uid，按规范关系 FMATERIALID = T_BD_MATERIAL.FINTERID 存其 FInterId
        var material = await _materialRepo.AsQueryable()
            .Where(m => m.Uid == request.Fmaterialid || m.FInterId == request.Fmaterialid)
            .FirstAsync();
        // 找不到则阻止创建，避免把前端 Uid 当作 FInterId 静默落库（破坏规范关系）
        if (material == null)
            throw new KeyNotFoundException("物料不存在");
        request.Fmaterialid = material.FInterId;
        request.Fmaterialnumber = material.FNumber;
        request.Fmaterialname = material.FName;

        // 使用组织(FCompanyId)记录所选组织的上级(SysOrgStructure.FPARAID)；无上级则记录其自身
        if (!string.IsNullOrEmpty(request.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(request.FCompanyId);
            if (org != null)
                request.FCompanyId = !string.IsNullOrEmpty(org.Fparaid) ? org.Fparaid : org.Uid;
        }
        return await base.CreateAsync(request);
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
        FGroupId = entity.FGroupId,
        // ---- 使用组织 / 制单修改（只读）----
        FCompanyId = entity.FCompanyId,
        CUser = entity.CUser,
        MUser = entity.MUser,
        MYmd = entity.MYmd
    };

    protected override TBdMaterialbartype MapToEntity(CreateMaterialBarTypeRequest dto) => new()
    {
        Fmaterialid = dto.Fmaterialid,
        Fbartype = dto.Fbartype,
        Fmaterialnumber = dto.Fmaterialnumber,
        Fmaterialname = dto.Fmaterialname,
        FCompanyId = dto.FCompanyId,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdMaterialbartype entity, UpdateMaterialBarTypeRequest dto)
    {
        // 编辑时物料锁定，仅条码类型与分组可改
        entity.Fbartype = dto.Fbartype;
        entity.FGroupId = dto.FGroupId;
    }
}
