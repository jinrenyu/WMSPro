using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Api.Controllers.Base;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Services;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

[Route("api/[controller]")]
public class EmployeeController : ApprovableDisableableMasterDataController<THrEmpinfo, EmployeeListDto, EmployeeDetailDto, CreateEmployeeRequest, UpdateEmployeeRequest>
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(IApprovableDisableableService<THrEmpinfo, EmployeeListDto, EmployeeDetailDto, CreateEmployeeRequest, UpdateEmployeeRequest> service)
        : base(service)
    {
        _employeeService = (EmployeeService)service;
    }

    /// <summary>成本中心下拉</summary>
    [HttpGet("cost-centers")]
    public async Task<ActionResult<ApiResponse<List<EmployeeLookupOptionDto>>>> CostCenters([FromQuery] string? keyword)
        => Ok(ApiResponse<List<EmployeeLookupOptionDto>>.Ok(await _employeeService.GetCostCenterLookupAsync(keyword)));

    /// <summary>职务下拉（辅助资料类别 FNUMBER='D'）</summary>
    [HttpGet("duties")]
    public async Task<ActionResult<ApiResponse<List<EmployeeLookupOptionDto>>>> Duties([FromQuery] string? keyword)
        => Ok(ApiResponse<List<EmployeeLookupOptionDto>>.Ok(await _employeeService.GetDutyLookupAsync(keyword)));
}
