using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Services;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class DepartmentController : MasterDataController<TBdDepartment, DepartmentListDto, DepartmentDetailDto, CreateDepartmentRequest, UpdateDepartmentRequest>
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(ICrudService<TBdDepartment, DepartmentListDto, DepartmentDetailDto, CreateDepartmentRequest, UpdateDepartmentRequest> service)
        : base(service)
    {
        _departmentService = (DepartmentService)service;
    }

    /// <summary>
    /// 获取部门树形结构
    /// </summary>
    [HttpGet("tree")]
    public async Task<ActionResult<ApiResponse<List<DepartmentTreeDto>>>> GetTree()
    {
        var result = await _departmentService.GetTreeAsync();
        return Ok(ApiResponse<List<DepartmentTreeDto>>.Ok(result));
    }
}
