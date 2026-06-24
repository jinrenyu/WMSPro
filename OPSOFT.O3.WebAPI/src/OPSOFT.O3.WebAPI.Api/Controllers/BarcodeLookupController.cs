using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 条码规则（SYS_BARCODE）下拉。供生产订单明细「条码规则」选择，显示规则名称。
/// 与 BillCodeRuleController（编码规则配置维护）区分：本控制器仅提供下拉选项。
/// </summary>
[ApiController]
[Route("api/barcode")]
[Authorize]
public class BarcodeLookupController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public BarcodeLookupController(ISqlSugarClient db)
    {
        _db = db;
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var keyword = request.Keyword?.Trim();

        var query = _db.Queryable<SysBarcode>().Where(b => !b.FDeleted);
        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(b => b.Fclasstypeid.Contains(keyword) || b.Fname.Contains(keyword));

        var list = await query
            .OrderBy(b => b.Fclasstypeid)
            .Take(request.MaxCount)
            .Select(b => new LookupDto { Uid = b.Uid, FNumber = b.Fclasstypeid, FName = b.Fname })
            .ToListAsync();

        return Ok(ApiResponse<List<LookupDto>>.Ok(list));
    }
}
