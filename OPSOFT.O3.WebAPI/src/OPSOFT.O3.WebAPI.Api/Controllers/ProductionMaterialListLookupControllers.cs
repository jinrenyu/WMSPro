using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Api.Controllers;

/// <summary>
/// 生产工单（生产订单）下拉。数据源 T_PRD_MOENTRY 关联表头 T_PRD_MO + 产品物料 + 单位，
/// 一行一条生产订单产品行，供生产用料清单「生产工单编号」选择并带出行号/产品/数量/单位。
/// Uid=生产订单明细内码、FNumber=生产订单编号、FName=产品名称。
/// </summary>
[ApiController]
[Route("api/mo")]
[Authorize]
public class MoLookupController : ControllerBase
{
    private readonly ISqlSugarClient _db;
    public MoLookupController(ISqlSugarClient db) => _db = db;

    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<MoLookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var keyword = request.Keyword?.Trim();

        var query = _db.Queryable<TPrdMoentry>()
            .LeftJoin<TPrdMo>((e, h) => e.FInterId == h.Uid)
            // 产品物料关联兼容：明细 FMATERIALID 可能存物料 Uid 或 FInterId（K3 原始）
            .LeftJoin<TBdMaterial>((e, h, m) => e.Fmaterialid == m.Uid || e.Fmaterialid == m.FInterId)
            .LeftJoin<TBdUnit>((e, h, m, u) => e.Fcommonunitid == u.Uid)
            .Where((e, h, m, u) => !e.FDeleted && !h.FDeleted);

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where((e, h, m, u) => h.Fbillno.Contains(keyword) || m.FNumber.Contains(keyword) || m.FName.Contains(keyword));

        var list = await query
            .OrderBy((e, h, m, u) => h.Fbillno, OrderByType.Desc)
            .OrderBy((e, h, m, u) => e.Fentryid)
            .Take(request.MaxCount)
            .Select((e, h, m, u) => new MoLookupDto
            {
                Uid = e.Uid,
                FNumber = h.Fbillno,
                FName = m.FName,
                Fmoid = h.Uid,
                Fmoentryid = e.Uid,
                Fmoentryseq = e.Fentryid,
                Fmaterialid = e.Fmaterialid,
                FmaterialNumber = m.FNumber,
                FmaterialName = m.FName,
                FSpecification = m.FSpecification,
                Funitid = e.Fcommonunitid,
                FunitName = u.FName,
                Fqty = e.Fqty
            })
            .ToListAsync();

        return Ok(ApiResponse<List<MoLookupDto>>.Ok(list));
    }
}

/// <summary>
/// 物料扩展下拉（带规格型号 / 基本单位）。供生产用料清单「产品代码」「明细物料代码」选择，
/// 选中后前端据此带出规格型号 / 基本单位。Uid=物料 Uid、FNumber=物料编码、FName=物料名称。
/// </summary>
[ApiController]
[Route("api/materialext")]
[Authorize]
public class MaterialExtLookupController : ControllerBase
{
    private readonly ISqlSugarClient _db;
    public MaterialExtLookupController(ISqlSugarClient db) => _db = db;

    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<MaterialExtLookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var keyword = request.Keyword?.Trim();

        var query = _db.Queryable<TBdMaterial>()
            .LeftJoin<TBdUnit>((m, u) => m.FBaseUnitId == u.Uid)
            .Where((m, u) => !m.FDeleted);

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where((m, u) => m.FNumber.Contains(keyword) || m.FName.Contains(keyword));

        var list = await query
            .OrderBy((m, u) => m.FNumber)
            .Take(request.MaxCount)
            .Select((m, u) => new MaterialExtLookupDto
            {
                Uid = m.Uid,
                FNumber = m.FNumber,
                FName = m.FName,
                FSpecification = m.FSpecification,
                FchartNumber = m.FCHARTNUMBER,
                FisBatchManage = m.FIsBatchManage,
                FBaseUnitId = m.FBaseUnitId,
                FBaseUnitNumber = u.FNumber,
                FBaseUnitName = u.FName
            })
            .ToListAsync();

        return Ok(ApiResponse<List<MaterialExtLookupDto>>.Ok(list));
    }
}

/// <summary>
/// 工序下拉（T_SFC_PROCESS）。供生产用料清单明细「工序代码」选择。
/// Uid=工序内码、FNumber=工序代码、FName=工序名称。
/// </summary>
[ApiController]
[Route("api/process")]
[Authorize]
public class ProcessLookupController : ControllerBase
{
    private readonly ISqlSugarClient _db;
    public ProcessLookupController(ISqlSugarClient db) => _db = db;

    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var keyword = request.Keyword?.Trim();

        var query = _db.Queryable<TSfcProcess>().Where(p => !p.FDeleted);
        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(p => p.FNUMBER.Contains(keyword) || p.FNAME.Contains(keyword));

        var list = await query
            .OrderBy(p => p.FNUMBER)
            .Take(request.MaxCount)
            .Select(p => new LookupDto { Uid = p.Uid, FNumber = p.FNUMBER, FName = p.FNAME })
            .ToListAsync();

        return Ok(ApiResponse<List<LookupDto>>.Ok(list));
    }
}

/// <summary>
/// 生产类型下拉（辅助资料 ProduceType）。数据源 T_BAS_ASSISTANTDATAENTRY 关联类别 T_BAS_ASSISTANTDATA，
/// 过滤类别编码 ProduceType。Uid=明细内码、FNumber=代码、FName=名称（FDATAVALUE）。
/// </summary>
[ApiController]
[Route("api/producetype")]
[Authorize]
public class ProduceTypeLookupController : ControllerBase
{
    private readonly ISqlSugarClient _db;
    public ProduceTypeLookupController(ISqlSugarClient db) => _db = db;

    [HttpGet("lookup")]
    public async Task<ActionResult<ApiResponse<List<LookupDto>>>> Lookup([FromQuery] LookupRequest request)
    {
        var keyword = request.Keyword?.Trim();

        var query = _db.Queryable<TBasAssistantdataentry>()
            .LeftJoin<TBasAssistantdata>((e, d) => e.Fid == d.FInterId)
            .Where((e, d) => !e.FDeleted && d.Fnumber == "ProduceType");

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where((e, d) => e.Fnumber.Contains(keyword) || e.Fdatavalue.Contains(keyword));

        var list = await query
            .OrderBy((e, d) => e.Fseq)
            .Take(request.MaxCount)
            .Select((e, d) => new LookupDto { Uid = e.Uid, FNumber = e.Fnumber, FName = e.Fdatavalue })
            .ToListAsync();

        return Ok(ApiResponse<List<LookupDto>>.Ok(list));
    }
}
