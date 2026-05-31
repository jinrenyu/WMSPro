using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 库存状态（只读参考数据，仅提供下拉查询，无维护界面）
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class StockStatusController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public StockStatusController(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 库存状态下拉查询（默认库存状态、默认收料状态等场景使用）
    /// </summary>
    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var query = _db.Queryable<TBdStockstatus>()
            .Where(s => !s.FDeleted && !s.FDisabled);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var kw = request.Keyword.Trim();
            query = query.Where(s => s.Fnumber.Contains(kw) || s.Fname.Contains(kw));
        }

        var list = await query
            .OrderBy(s => s.FInterId)
            .Take(request.MaxCount)
            .Select(s => new LookupDto
            {
                Uid = s.Uid,
                FNumber = s.Fnumber,
                FName = s.Fname
            })
            .ToListAsync();

        return Ok(ApiResponse<List<LookupDto>>.Ok(list));
    }
}
