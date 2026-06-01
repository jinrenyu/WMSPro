using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 职员维护（T_HR_EMPINFO）
/// </summary>
public class EmployeeService : ApprovableDisableableCrudService<THrEmpinfo, EmployeeListDto, EmployeeDetailDto, CreateEmployeeRequest, UpdateEmployeeRequest>
{
    private readonly IRepository<TBdDepartment> _deptRepo;
    private readonly IRepository<TCbCostcenter> _costCenterRepo;
    private readonly IRepository<TBasAssistantdata> _auxCategoryRepo;
    private readonly IRepository<TBasAssistantdataentry> _auxEntryRepo;
    private readonly IRepository<SysOrgStructure> _orgRepo;

    private const string DutyCategoryNumber = "D"; // 职务类别 FNUMBER

    public EmployeeService(
        IRepository<THrEmpinfo> repository,
        IRepository<TBdDepartment> deptRepo,
        IRepository<TCbCostcenter> costCenterRepo,
        IRepository<TBasAssistantdata> auxCategoryRepo,
        IRepository<TBasAssistantdataentry> auxEntryRepo,
        IRepository<SysOrgStructure> orgRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null) : base(repository, db, currentUser, operationLog)
    {
        _deptRepo = deptRepo;
        _costCenterRepo = costCenterRepo;
        _auxCategoryRepo = auxCategoryRepo;
        _auxEntryRepo = auxEntryRepo;
        _orgRepo = orgRepo;
    }

    protected override string PrgKey => "Employee";

    protected override Expression<Func<THrEmpinfo, bool>> BuildSearchPredicate(string keyword)
    {
        return e => e.Fnumber.Contains(keyword) || e.Fname.Contains(keyword) || e.Ftel.Contains(keyword);
    }

    protected override Expression<Func<THrEmpinfo, bool>>? BuildLookupFilter(LookupRequest request)
    {
        // 默认排除已离职员工
        return e => !e.Fisdeparture;
    }

    protected override LookupDto MapToLookupDto(THrEmpinfo entity)
        => new() { Uid = entity.Uid, FNumber = entity.Fnumber, FName = entity.Fname };

    /// <summary>成本中心下拉（值=FInterId）</summary>
    public async Task<List<EmployeeLookupOptionDto>> GetCostCenterLookupAsync(string? keyword)
    {
        var list = await _costCenterRepo.GetListAsync(c => !c.FDeleted && !c.FDisabled);
        var query = list.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var kw = keyword.Trim();
            query = query.Where(c => c.Fnumber.Contains(kw) || c.Fname.Contains(kw));
        }
        return query.OrderBy(c => c.Fnumber).Take(200)
            .Select(c => new EmployeeLookupOptionDto { Uid = c.FInterId, FNumber = c.Fnumber, FName = c.Fname }).ToList();
    }

    /// <summary>职务下拉：辅助资料明细，所属类别 FNUMBER='D'，FDELETED=0 AND FSTATUS=40（值=FInterId）</summary>
    public async Task<List<EmployeeLookupOptionDto>> GetDutyLookupAsync(string? keyword)
    {
        var categoryIds = (await _auxCategoryRepo.GetListAsync(c => c.Fnumber == DutyCategoryNumber && !c.FDeleted))
            .Select(c => c.FInterId).ToList();
        if (categoryIds.Count == 0) return new List<EmployeeLookupOptionDto>();

        var entries = await _auxEntryRepo.GetListAsync(e => categoryIds.Contains(e.Fid) && !e.FDeleted && e.FStatus == 40);
        var query = entries.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var kw = keyword.Trim();
            query = query.Where(e => e.Fnumber.Contains(kw) || e.Fdatavalue.Contains(kw));
        }
        return query.OrderBy(e => e.Fseq).Take(500)
            .Select(e => new EmployeeLookupOptionDto { Uid = e.FInterId, FNumber = e.Fnumber, FName = e.Fdatavalue }).ToList();
    }

    public override async Task<PagedResult<EmployeeListDto>> GetPagedListAsync(PagedRequest request)
    {
        var result = await base.GetPagedListAsync(request);

        var deptIds = result.Items.Where(i => !string.IsNullOrEmpty(i.FSalDeptId)).Select(i => i.FSalDeptId).Distinct().ToList();
        if (deptIds.Count > 0)
        {
            var depts = await _deptRepo.GetListAsync(d => deptIds.Contains(d.Uid));
            var dict = depts.ToDictionary(d => d.Uid, d => d.FName);
            foreach (var item in result.Items)
                if (dict.TryGetValue(item.FSalDeptId, out var n)) item.FSalDeptName = n;
        }
        return result;
    }

    public override async Task<EmployeeDetailDto?> GetByIdAsync(string uid)
    {
        var detail = await base.GetByIdAsync(uid);
        if (detail == null) return null;

        if (!string.IsNullOrEmpty(detail.FSalDeptId))
        {
            var dept = await _deptRepo.GetByIdAsync(detail.FSalDeptId);
            if (dept != null) detail.FSalDeptName = dept.FName;
        }
        if (!string.IsNullOrEmpty(detail.FCostCenterId))
        {
            var cc = (await _costCenterRepo.GetListAsync(c => c.FInterId == detail.FCostCenterId)).FirstOrDefault();
            if (cc != null) { detail.FCostCenterNumber = cc.Fnumber; detail.FCostCenterName = cc.Fname; }
        }
        if (!string.IsNullOrEmpty(detail.FDutyId))
        {
            var duty = (await _auxEntryRepo.GetListAsync(e => e.FInterId == detail.FDutyId)).FirstOrDefault();
            if (duty != null) { detail.FDutyNumber = duty.Fnumber; detail.FDutyName = duty.Fdatavalue; }
        }
        if (!string.IsNullOrEmpty(detail.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(detail.FCompanyId);
            if (org != null) detail.FCompanyName = org.Fname;
        }
        return detail;
    }

    public override async Task<EmployeeDetailDto> CreateAsync(CreateEmployeeRequest request)
    {
        if (!string.IsNullOrEmpty(request.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(request.FCompanyId);
            if (org != null)
                request.FCompanyId = !string.IsNullOrEmpty(org.Fparaid) ? org.Fparaid : org.Uid;
        }
        return await base.CreateAsync(request);
    }

    /// <summary>Base64（可含 dataURL 前缀）→ byte[]；空或非法返回 null</summary>
    private static byte[]? DecodePicture(string? base64)
    {
        if (string.IsNullOrEmpty(base64)) return null;
        var idx = base64.IndexOf("base64,", StringComparison.Ordinal);
        if (idx >= 0) base64 = base64[(idx + 7)..];
        try { return Convert.FromBase64String(base64); }
        catch (FormatException) { return null; }
    }

    protected override EmployeeListDto MapToListDto(THrEmpinfo entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.Fnumber,
        FName = entity.Fname,
        FSex = entity.Fsex,
        FTel = entity.Ftel,
        FSalDeptId = entity.Fsaldeptid,
        FSalDeptName = string.Empty,
        FIsDeparture = entity.Fisdeparture,
        CYmd = entity.CYmd,
        FEducation = entity.Feducation,
        FBirthDate = entity.Fbirthdate ?? DateTime.MinValue,
        FEntryDate = entity.Fentrydate ?? DateTime.MinValue,
        FDepartureDate = entity.Fdeparturedate ?? DateTime.MinValue,
        FMail = entity.Femail,
        FNote = entity.Fdescription,
        FAddress = entity.Faddress,
        FQq = entity.Fqq,
        FWechat = entity.Fwechat,
        FEmergencyTel = entity.Femergencytel,
        Emergency = entity.Emergency,
        FGroupId = entity.FGroupId
    };

    protected override EmployeeDetailDto MapToDetailDto(THrEmpinfo entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.Fnumber,
        FName = entity.Fname,
        FSex = entity.Fsex,
        FTel = entity.Ftel,
        FSalDeptId = entity.Fsaldeptid,
        FSalDeptName = string.Empty,
        FIsDeparture = entity.Fisdeparture,
        CYmd = entity.CYmd,
        FEducation = entity.Feducation,
        FBirthDate = entity.Fbirthdate ?? DateTime.MinValue,
        FEntryDate = entity.Fentrydate ?? DateTime.MinValue,
        FDepartureDate = entity.Fdeparturedate ?? DateTime.MinValue,
        FMail = entity.Femail,
        FNote = entity.Fdescription,
        FAddress = entity.Faddress,
        FQq = entity.Fqq,
        FWechat = entity.Fwechat,
        FEmergencyTel = entity.Femergencytel,
        Emergency = entity.Emergency,
        FGroupId = entity.FGroupId,
        // 扩展
        FCostCenterId = entity.Fcostcenterid,
        FDutyId = entity.Fdutyid,
        FTransferDate = entity.Ftransferdate ?? DateTime.MinValue,
        FPersonnelCoefficient = entity.Fcoefficient,
        FDutyLevel = entity.Flevel,
        FDingTalkUserId = entity.Fdduserid,
        FPictureBase64 = (entity.Fpicture != null && entity.Fpicture.Length > 0) ? Convert.ToBase64String(entity.Fpicture) : string.Empty,
        // 使用组织 / 审计
        FCompanyId = entity.FCompanyId,
        CUser = entity.CUser,
        MUser = entity.MUser,
        MYmd = entity.MYmd,
        FCheckerId = entity.FCheckerId,
        FCheckDate = entity.FCheckDate,
        Fdisableid = entity.Fdisableid,
        Fdisabledate = entity.Fdisabledate
    };

    protected override THrEmpinfo MapToEntity(CreateEmployeeRequest dto) => new()
    {
        Fnumber = dto.FNumber,
        Fname = dto.FName,
        FCompanyId = dto.FCompanyId,
        Fsex = dto.FSex,
        Feducation = dto.FEducation,
        // T_HR_EMPINFO 日期列 NOT NULL，空值统一存 DateTime.MinValue
        Fbirthdate = dto.FBirthDate ?? DateTime.MinValue,
        Fentrydate = dto.FEntryDate ?? DateTime.MinValue,
        Ftransferdate = dto.FTransferDate ?? DateTime.MinValue,
        Fdeparturedate = DateTime.MinValue,
        Ftel = dto.FTel,
        Fsaldeptid = dto.FSalDeptId,
        Fcostcenterid = dto.FCostCenterId,
        Fdutyid = dto.FDutyId,
        Flevel = dto.FDutyLevel,
        Fcoefficient = dto.FPersonnelCoefficient,
        Femail = dto.FMail,
        Fdescription = dto.FNote,
        Faddress = dto.FAddress,
        Fqq = dto.FQq,
        Fwechat = dto.FWechat,
        Femergencytel = dto.FEmergencyTel,
        Emergency = dto.Emergency,
        Fdduserid = dto.FDingTalkUserId,
        // 该列 NOT NULL，空图片存空字节数组
        Fpicture = DecodePicture(dto.FPictureBase64) ?? Array.Empty<byte>(),
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(THrEmpinfo entity, UpdateEmployeeRequest dto)
    {
        entity.Fname = dto.FName;
        entity.Fsex = dto.FSex;
        entity.Feducation = dto.FEducation;
        entity.Fbirthdate = dto.FBirthDate ?? DateTime.MinValue;
        entity.Fentrydate = dto.FEntryDate ?? DateTime.MinValue;
        entity.Ftransferdate = dto.FTransferDate ?? DateTime.MinValue;
        entity.Fdeparturedate = dto.FDepartureDate ?? DateTime.MinValue;
        entity.Fisdeparture = dto.FIsDeparture;
        entity.Ftel = dto.FTel;
        entity.Fsaldeptid = dto.FSalDeptId;
        entity.Fcostcenterid = dto.FCostCenterId;
        entity.Fdutyid = dto.FDutyId;
        entity.Flevel = dto.FDutyLevel;
        entity.Fcoefficient = dto.FPersonnelCoefficient;
        entity.Femail = dto.FMail;
        entity.Fdescription = dto.FNote;
        entity.Faddress = dto.FAddress;
        entity.Fqq = dto.FQq;
        entity.Fwechat = dto.FWechat;
        entity.Femergencytel = dto.FEmergencyTel;
        entity.Emergency = dto.Emergency;
        entity.Fdduserid = dto.FDingTalkUserId;
        entity.Fpicture = DecodePicture(dto.FPictureBase64) ?? Array.Empty<byte>();
        entity.FGroupId = dto.FGroupId;
    }
}
