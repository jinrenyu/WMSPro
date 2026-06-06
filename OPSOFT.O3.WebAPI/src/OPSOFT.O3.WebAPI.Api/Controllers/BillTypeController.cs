using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 单据类型（T_BAS_BILLTYPE）下拉。按业务表单标识 FBillFormid 过滤，如采购订单 = PUR_PurchaseOrder。
/// </summary>
[ApiController]
[Route("api/billtype")]
[Authorize]
public class BillTypeController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public BillTypeController(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 单据类型下拉。parentId 传业务表单标识(FBillFormid)，缺省取采购订单 PUR_PurchaseOrder。
    /// </summary>
    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var formId = string.IsNullOrWhiteSpace(request.ParentId) ? "PUR_PurchaseOrder" : request.ParentId.Trim();
        var keyword = request.Keyword?.Trim();

        var query = _db.Queryable<TBasBilltype>()
            .Where(b => !b.FDeleted && !b.FDisabled && b.Fbillformid == formId);

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(b => b.Fnumber.Contains(keyword) || b.Fname.Contains(keyword));

        var list = await query
            .OrderBy(b => b.Fnumber)
            .Take(request.MaxCount)
            .Select(b => new LookupDto { Uid = b.Uid, FNumber = b.Fnumber, FName = b.Fname })
            .ToListAsync();

        return Ok(ApiResponse<List<LookupDto>>.Ok(list));
    }
}
