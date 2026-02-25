using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class DepartmentService : CrudService<TBdDepartment, DepartmentListDto, DepartmentDetailDto, CreateDepartmentRequest, UpdateDepartmentRequest>
{
    private readonly IRepository<TBdDepartment> _repo;

    public DepartmentService(IRepository<TBdDepartment> repository, IOperationLogService? operationLog = null) : base(repository, operationLog)
    {
        _repo = repository;
    }

    protected override string PrgKey => "Department";

    /// <summary>
    /// 获取部门树形结构
    /// </summary>
    public async Task<List<DepartmentTreeDto>> GetTreeAsync()
    {
        var all = await _repo.GetListAsync(d => !d.FDeleted);
        var dtos = all.Select(d => new DepartmentTreeDto
        {
            Uid = d.Uid,
            FNumber = d.FNumber,
            FName = d.FName,
            FFullName = d.FFullName,
            FParentId = d.FParentId,
            FManager = d.FManager,
            FIsLine = d.FIsLine,
            CYmd = d.CYmd
        }).ToList();

        var lookup = dtos.ToLookup(d => d.FParentId);
        foreach (var dto in dtos)
        {
            dto.Children = lookup[dto.Uid].ToList();
        }

        // 返回顶级节点（ParentId 为空或不存在的）
        var allIds = new HashSet<string>(dtos.Select(d => d.Uid));
        return dtos.Where(d => string.IsNullOrEmpty(d.FParentId) || !allIds.Contains(d.FParentId)).ToList();
    }

    protected override Expression<Func<TBdDepartment, bool>> BuildSearchPredicate(string keyword)
    {
        return d => d.FNumber.Contains(keyword) || d.FName.Contains(keyword) || d.FFullName.Contains(keyword);
    }

    protected override Expression<Func<TBdDepartment, bool>>? BuildLookupFilter(LookupRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId))
        {
            return d => d.FParentId == request.ParentId;
        }
        return null;
    }

    protected override DepartmentListDto MapToListDto(TBdDepartment entity) => new()
    {
        Uid = entity.Uid,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FFullName = entity.FFullName,
        FParentId = entity.FParentId,
        FManager = entity.FManager,
        FIsLine = entity.FIsLine,
        CYmd = entity.CYmd
    };

    protected override DepartmentDetailDto MapToDetailDto(TBdDepartment entity) => new()
    {
        Uid = entity.Uid,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FFullName = entity.FFullName,
        FParentId = entity.FParentId,
        FManager = entity.FManager,
        FIsLine = entity.FIsLine,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FHelpCode = entity.FHelpCode,
        FCreateOrgId = entity.FCreateOrgId,
        FDeptProperty = entity.FDeptProperty,
        FCostAccountType = entity.FCostAccountType,
        FEffectDate = entity.FEffectDate,
        FLapseDate = entity.FLapseDate,
        FErpNumber = entity.FErpNumber
    };

    protected override TBdDepartment MapToEntity(CreateDepartmentRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FDescription = dto.FDescription,
        FFullName = dto.FFullName,
        FHelpCode = dto.FHelpCode,
        FParentId = dto.FParentId,
        FManager = dto.FManager,
        FIsLine = dto.FIsLine,
        FDeptProperty = dto.FDeptProperty,
        FCostAccountType = dto.FCostAccountType,
        FEffectDate = dto.FEffectDate ?? DateTime.Now,
        FLapseDate = dto.FLapseDate ?? DateTime.MaxValue
    };

    protected override void UpdateEntity(TBdDepartment entity, UpdateDepartmentRequest dto)
    {
        entity.FName = dto.FName;
        entity.FDescription = dto.FDescription;
        entity.FFullName = dto.FFullName;
        entity.FHelpCode = dto.FHelpCode;
        entity.FParentId = dto.FParentId;
        entity.FManager = dto.FManager;
        entity.FIsLine = dto.FIsLine;
        entity.FDeptProperty = dto.FDeptProperty;
        entity.FCostAccountType = dto.FCostAccountType;
        entity.FEffectDate = dto.FEffectDate ?? entity.FEffectDate;
        entity.FLapseDate = dto.FLapseDate ?? entity.FLapseDate;
    }
}
