using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// BOM 单（T_BD_BOM）下拉。供生产订单明细「BOM单」选择，显示单据编号。
/// </summary>
[ApiController]
[Route("api/bom")]
[Authorize]
public class BomController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public BomController(ISqlSugarClient db)
    {
        _db = db;
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var keyword = request.Keyword?.Trim();

        var query = _db.Queryable<TBdBom>().Where(b => !b.FDeleted);
        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(b => b.FBillNo.Contains(keyword));

        var list = await query
            .OrderBy(b => b.FBillNo)
            .Take(request.MaxCount)
            .Select(b => new LookupDto { Uid = b.Uid, FNumber = b.FBillNo, FName = b.FBillNo })
            .ToListAsync();

        return Ok(ApiResponse<List<LookupDto>>.Ok(list));
    }
}
