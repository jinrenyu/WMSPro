using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class EmployeeService : ApprovableDisableableCrudService<TBdEmpinfo, EmployeeListDto, EmployeeDetailDto, CreateEmployeeRequest, UpdateEmployeeRequest>
{
    private readonly IRepository<TBdDepartment> _deptRepo;

    public EmployeeService(
        IRepository<TBdEmpinfo> repository,
        IRepository<TBdDepartment> deptRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null) : base(repository, db, currentUser, operationLog)
    {
        _deptRepo = deptRepo;
    }

    protected override string PrgKey => "Employee";

    protected override Expression<Func<TBdEmpinfo, bool>> BuildSearchPredicate(string keyword)
    {
        return e => e.Fnumber.Contains(keyword) || e.Fname.Contains(keyword) || e.Ftel.Contains(keyword);
    }

    protected override Expression<Func<TBdEmpinfo, bool>>? BuildLookupFilter(LookupRequest request)
    {
        // 默认排除已离职员工
        return e => !e.Fisdeparture;
    }

    protected override LookupDto MapToLookupDto(TBdEmpinfo entity)
    {
        return new LookupDto
        {
            Uid = entity.Uid,
            FNumber = entity.Fnumber,
            FName = entity.Fname
        };
    }

    public override async Task<PagedResult<EmployeeListDto>> GetPagedListAsync(PagedRequest request)
    {
        var result = await base.GetPagedListAsync(request);

        var deptIds = result.Items
            .Where(i => !string.IsNullOrEmpty(i.FSalDeptId))
            .Select(i => i.FSalDeptId)
            .Distinct()
            .ToList();

        if (deptIds.Count > 0)
        {
            var depts = await _deptRepo.GetListAsync(d => deptIds.Contains(d.Uid));
            var deptDict = depts.ToDictionary(d => d.Uid, d => d.FName);

            foreach (var item in result.Items)
            {
                if (deptDict.TryGetValue(item.FSalDeptId, out var deptName))
                    item.FSalDeptName = deptName;
            }
        }

        return result;
    }

    protected override EmployeeListDto MapToListDto(TBdEmpinfo entity) => new()
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
        FBirthDate = entity.Fbirthdate,
        FEntryDate = entity.Fentrydate,
        FDepartureDate = entity.Fdeparturedate,
        FMail = entity.Fmail,
        FNote = entity.Fnote,
        FAddress = entity.Faddress,
        FQq = entity.Fqq,
        FWechat = entity.Fwechat,
        FEmergencyTel = entity.Femergencytel,
        Emergency = entity.Emergency,
        FGroupId = entity.FGroupId
    };

    protected override EmployeeDetailDto MapToDetailDto(TBdEmpinfo entity) => new()
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
        FBirthDate = entity.Fbirthdate,
        FEntryDate = entity.Fentrydate,
        FDepartureDate = entity.Fdeparturedate,
        FMail = entity.Fmail,
        FNote = entity.Fnote,
        FAddress = entity.Faddress,
        FQq = entity.Fqq,
        FWechat = entity.Fwechat,
        FEmergencyTel = entity.Femergencytel,
        Emergency = entity.Emergency,
        FGroupId = entity.FGroupId
    };

    protected override TBdEmpinfo MapToEntity(CreateEmployeeRequest dto) => new()
    {
        Fnumber = dto.FNumber,
        Fname = dto.FName,
        Fsex = dto.FSex,
        Feducation = dto.FEducation,
        Fbirthdate = dto.FBirthDate ?? DateTime.MinValue,
        Fentrydate = dto.FEntryDate ?? DateTime.MinValue,
        Ftel = dto.FTel,
        Fsaldeptid = dto.FSalDeptId,
        Fmail = dto.FMail,
        Fnote = dto.FNote,
        Faddress = dto.FAddress,
        Fwechat = dto.FWechat,
        Femergencytel = dto.FEmergencyTel,
        Emergency = dto.Emergency,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdEmpinfo entity, UpdateEmployeeRequest dto)
    {
        entity.Fname = dto.FName;
        entity.Fsex = dto.FSex;
        entity.Feducation = dto.FEducation;
        entity.Fbirthdate = dto.FBirthDate ?? entity.Fbirthdate;
        entity.Fentrydate = dto.FEntryDate ?? entity.Fentrydate;
        entity.Fdeparturedate = dto.FDepartureDate ?? entity.Fdeparturedate;
        entity.Fisdeparture = dto.FIsDeparture;
        entity.Ftel = dto.FTel;
        entity.Fsaldeptid = dto.FSalDeptId;
        entity.Fmail = dto.FMail;
        entity.Fnote = dto.FNote;
        entity.Faddress = dto.FAddress;
        entity.Fwechat = dto.FWechat;
        entity.Femergencytel = dto.FEmergencyTel;
        entity.Emergency = dto.Emergency;
        entity.FGroupId = dto.FGroupId;
    }
}
