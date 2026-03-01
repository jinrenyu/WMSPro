using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Api.Controllers.Base;

/// <summary>
/// 基础资料通用 Controller 基类
/// </summary>
[ApiController]
[Authorize]
public abstract class MasterDataController<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> : ControllerBase
    where TEntity : BaseEntity, new()
{
    protected readonly ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> Service;

    protected MasterDataController(ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto> service)
    {
        Service = service;
    }

    [HttpGet]
    public virtual async Task<ActionResult<ApiResponse<PagedResult<TListDto>>>> GetList([FromQuery] PagedRequest request)
    {
        var result = await Service.GetPagedListAsync(request);
        return Ok(ApiResponse<PagedResult<TListDto>>.Ok(result));
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<ApiResponse<TDetailDto>>> GetById(string id)
    {
        var result = await Service.GetByIdAsync(id);
        if (result == null)
            return Ok(ApiResponse<TDetailDto>.Fail("记录不存在", 404));
        return Ok(ApiResponse<TDetailDto>.Ok(result));
    }

    [HttpPost]
    public virtual async Task<ActionResult<ApiResponse<TDetailDto>>> Create([FromBody] TCreateDto request)
    {
        var result = await Service.CreateAsync(request);
        return Ok(ApiResponse<TDetailDto>.Ok(result, "创建成功"));
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Update(string id, [FromBody] TUpdateDto request)
    {
        var result = await Service.UpdateAsync(id, request);
        return Ok(ApiResponse<bool>.Ok(result, "更新成功"));
    }

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult<ApiResponse<bool>>> Delete(string id)
    {
        var result = await Service.DeleteAsync(id);
        return Ok(ApiResponse<bool>.Ok(result, "删除成功"));
    }

    [HttpGet("lookup")]
    public virtual async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var result = await Service.GetLookupAsync(request);
        return Ok(ApiResponse<List<LookupDto>>.Ok(result));
    }

    /// <summary>
    /// 获取实体的字段及类型配置信息，用于高阶动态查询等前端场景
    /// </summary>
    [HttpGet("fields")]
    public virtual ActionResult<ApiResponse<List<object>>> GetFieldsMetadata()
    {
        var properties = typeof(TEntity).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy);

        var list = properties.Select(p => 
        {
            var type = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
            string csharpTypeName = type.Name.ToLower();

            if (csharpTypeName == "int32") csharpTypeName = "int";
            else if (csharpTypeName == "int64") csharpTypeName = "long";
            else if (csharpTypeName == "int16") csharpTypeName = "short";
            else if (csharpTypeName == "boolean") csharpTypeName = "bool";
            else if (csharpTypeName == "single") csharpTypeName = "float";

            return (object)new {
                Field = char.ToLower(p.Name[0]) + p.Name.Substring(1), // camelCase for frontend
                DataType = csharpTypeName
            };
        }).ToList();

        return Ok(ApiResponse<List<object>>.Ok(list));
    }
}
