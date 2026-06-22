using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 系统表单（SYS_BILLTEMPLATE）下拉，用于出入库流程配置的「源单类型 / 目标单据类型」选择。
/// 注意：返回的 LookupDto.Uid 故意置为 FNUMBER —— 出入库流程配置的源单/目标类型列存的是 FNUMBER（非 Uid），
/// 这样前端 LookupSelect 的选中值即为 FNUMBER，存取与名称解析一致。
/// </summary>
[ApiController]
[Route("api/billtemplate")]
[Authorize]
public class BillTemplateController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public BillTemplateController(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>系统表单下拉（按 FNUMBER/FNAME 关键字过滤）。</summary>
    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var keyword = request.Keyword?.Trim();

        var query = _db.Queryable<SysBillTemplate>()
            .Where(t => !t.FDeleted && t.Fnumber != "");

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(t => t.Fnumber.Contains(keyword) || t.Fname.Contains(keyword));

        var list = await query
            .OrderBy(t => t.Fnumber)
            .Take(request.MaxCount)
            .Select(t => new LookupDto { Uid = t.Fnumber, FNumber = t.Fnumber, FName = t.Fname })
            .ToListAsync();

        return Ok(ApiResponse<List<LookupDto>>.Ok(list));
    }
}
