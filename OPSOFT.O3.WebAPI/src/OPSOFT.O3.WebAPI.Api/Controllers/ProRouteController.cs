using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 产品工艺流程（T_ENG_PROROUTE）下拉。关联产品物料（T_BD_MATERIAL）带出物料/工艺信息，
/// 供生产订单明细「产品代码 / 产品工艺流程」选择。Uid=工艺流程内码，FNumber=物料编码、FName=物料名称。
/// </summary>
[ApiController]
[Route("api/proroute")]
[Authorize]
public class ProRouteController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public ProRouteController(ISqlSugarClient db)
    {
        _db = db;
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<ProRouteLookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var keyword = request.Keyword?.Trim();

        // 产品物料关联兼容：开发库 proroute.FPRODUCTID 可能存物料 FInterId（K3 原始）或 Uid
        var query = _db.Queryable<TEngProroute>()
            .LeftJoin<TBdMaterial>((p, m) => p.Fproductid == m.FInterId || p.Fproductid == m.Uid)
            .Where((p, m) => !p.FDeleted);

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where((p, m) => p.Fnumber.Contains(keyword) || p.Fname.Contains(keyword)
                || m.FNumber.Contains(keyword) || m.FName.Contains(keyword));

        var list = await query
            .OrderBy((p, m) => p.Fnumber)
            .Take(request.MaxCount)
            .Select((p, m) => new ProRouteLookupDto
            {
                Uid = p.Uid,
                FNumber = m.FNumber,
                FName = m.FName,
                Fmaterialid = m.Uid,
                FmaterialNumber = m.FNumber,
                FmaterialName = m.FName,
                FchartNumber = m.FCHARTNUMBER,
                FSpecification = m.FSpecification,
                FisBatchManage = m.FIsBatchManage,
                Fprorouteid = p.Uid,
                FprorouteNumber = p.Fnumber,
                FprorouteName = p.Fname
            })
            .ToListAsync();

        return Ok(ApiResponse<List<ProRouteLookupDto>>.Ok(list));
    }
}
