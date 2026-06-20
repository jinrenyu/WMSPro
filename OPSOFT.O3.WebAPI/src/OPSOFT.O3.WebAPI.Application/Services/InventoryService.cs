using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Extensions;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 即时库存服务（只读）：分页查询真实表 T_STK_INVENTORY，并批量解析物料/单位/仓库/仓位/组织/
/// 辅助属性/库存状态等关联名称。FK 列均存目标资料的 Uid（与采购入库过账写入一致），故按 Uid 解析。
/// </summary>
public class InventoryService : IInventoryService
{
    private readonly ISqlSugarClient _db;

    public InventoryService(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task<PagedResult<InventoryListDto>> GetPagedListAsync(PagedRequest request)
    {
        var query = _db.Queryable<TStkInventory>().Where(i => !i.FDeleted);

        // 关键字：物料代码/名称/规格 命中的物料 Uid 集合，或 批次(FLOT) 直接 like
        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var kw = request.Keyword.Trim();
            var matUids = await _db.Queryable<TBdMaterial>()
                .Where(m => m.FNumber.Contains(kw) || m.FName.Contains(kw) || m.FSpecification.Contains(kw))
                .Select(m => m.Uid)
                .ToListAsync();
            query = query.Where(i => matUids.Contains(i.Fmaterialid) || i.FLOT.Contains(kw));
        }

        // 动态筛选：
        //  · 名称类字段（物料代码/名称、规格、仓库、仓位、辅助属性、库存状态、组织/库存组织）——库存表本身没有这些列，
        //    先按操作符在目标资料表解析出匹配的 Uid（库存状态额外兼容存 FNumber 的情形），再用 IN 约束库存表对应外键列；
        //    多个条件按 AND 组合，任一名称条件无匹配则整表为空（可实现"某仓库+某仓位+某物料+某批次"组合查）。
        //  · 其余库存表真实列（批次 FLOT、数量 FQTY 等）走 ToConditionalModels<TStkInventory>。
        var realFilters = new List<DynamicFilterInfo>();
        foreach (var filter in request.DynamicFilters ?? new List<DynamicFilterInfo>())
        {
            if (string.IsNullOrWhiteSpace(filter.Field)) continue;
            switch (filter.Field.Trim().ToLowerInvariant())
            {
                case "fmaterialnumber":
                {
                    var u = await ResolveUidsAsync<TBdMaterial>("FNumber", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fmaterialid));
                    break;
                }
                case "fmaterialname":
                {
                    var u = await ResolveUidsAsync<TBdMaterial>("FName", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fmaterialid));
                    break;
                }
                case "fspecification":
                {
                    var u = await ResolveUidsAsync<TBdMaterial>("FSpecification", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fmaterialid));
                    break;
                }
                case "fstocknumber":
                {
                    var u = await ResolveUidsAsync<TBdStock>("FNumber", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fstockid));
                    break;
                }
                case "fstockname":
                {
                    var u = await ResolveUidsAsync<TBdStock>("FName", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fstockid));
                    break;
                }
                case "fstocklocnumber":
                {
                    var u = await ResolveUidsAsync<TBdStockPlace>("FNumber", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fstocklocid));
                    break;
                }
                case "fstocklocname":
                {
                    var u = await ResolveUidsAsync<TBdStockPlace>("FName", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fstocklocid));
                    break;
                }
                case "fauxpropname":
                {
                    var u = await ResolveUidsAsync<TBdFlexauxproperty>("Fname", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fauxpropid));
                    break;
                }
                case "fstockorgname":
                {
                    var u = await ResolveUidsAsync<SysOrgStructure>("Fname", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.Fstockorgid));
                    break;
                }
                case "fcompanyname":
                {
                    var u = await ResolveUidsAsync<SysOrgStructure>("Fname", filter);
                    if (u.Count == 0) return EmptyPage(request);
                    query = query.Where(i => u.Contains(i.FCompanyId));
                    break;
                }
                case "fstockstatusname":
                {
                    var keys = await ResolveStockStatusKeysAsync(filter);
                    if (keys.Count == 0) return EmptyPage(request);
                    query = query.Where(i => keys.Contains(i.Fstockstatusid));
                    break;
                }
                default:
                    realFilters.Add(filter);
                    break;
            }
        }
        if (realFilters.Count > 0)
            query = query.Where(realFilters.ToConditionalModels<TStkInventory>());

        RefAsync<int> totalCount = 0;
        var rows = await query
            .OrderBy(i => i.CYmd, OrderByType.Desc)
            .OrderBy(i => i.Uid)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        if (rows.Count == 0)
            return new PagedResult<InventoryListDto> { Items = new(), TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };

        // 批量加载名称源
        var materialDict = await LoadMaterialDictAsync(rows.Select(r => r.Fmaterialid));
        var unitDict = await LoadUnitDictAsync(rows.SelectMany(r => new[] { r.Fstockunitid, r.Fbaseunitid, r.FSECUNITID }));
        var stockDict = await LoadStockDictAsync(rows.Select(r => r.Fstockid));
        var stockLocDict = await LoadStockPlaceDictAsync(rows.Select(r => r.Fstocklocid));
        var orgDict = await LoadOrgNameDictAsync(rows.SelectMany(r => new[] { r.FCompanyId, r.Fstockorgid }));
        var auxDict = await LoadFlexAuxDictAsync(rows.Select(r => r.Fauxpropid));
        var statusDict = await LoadStockStatusDictAsync(rows.Select(r => r.Fstockstatusid));

        var items = rows.Select(r =>
        {
            materialDict.TryGetValue(r.Fmaterialid ?? string.Empty, out var mat);
            unitDict.TryGetValue(r.Fstockunitid ?? string.Empty, out var stockUnit);
            unitDict.TryGetValue(r.Fbaseunitid ?? string.Empty, out var baseUnit);
            unitDict.TryGetValue(r.FSECUNITID ?? string.Empty, out var secUnit);
            stockDict.TryGetValue(r.Fstockid ?? string.Empty, out var stock);
            stockLocDict.TryGetValue(r.Fstocklocid ?? string.Empty, out var loc);

            return new InventoryListDto
            {
                Uid = r.Uid,
                FmaterialNumber = mat.Number,
                FmaterialName = mat.Name,
                FSpecification = mat.Spec,
                Flot = r.FLOT ?? string.Empty,
                FauxpropName = auxDict.GetValueOrDefault(r.Fauxpropid ?? string.Empty, string.Empty),
                Fqty = r.Fqty,
                FstockunitName = stockUnit.Name,
                Fbaseunitqty = r.Fbaseunitqty,
                FbaseunitName = baseUnit.Name,
                Fsecunitqty = r.FSECUNITQTY ?? 0,
                FsecunitName = secUnit.Name,
                FstockNumber = stock.Number,
                FstockName = stock.Name,
                FstocklocNumber = loc.Number,
                FstocklocName = loc.Name,
                FisKfPeriod = mat.Kf,
                Fkfdate = r.Fkfdate,
                FKfPeriod = mat.KfPeriod,
                FKfUnit = mat.KfUnit,
                Fusefuldate = r.Fusefuldate,
                FstockstatusName = statusDict.GetValueOrDefault(r.Fstockstatusid ?? string.Empty, string.Empty),
                FstockorgName = orgDict.GetValueOrDefault(r.Fstockorgid ?? string.Empty, string.Empty),
                FcompanyName = orgDict.GetValueOrDefault(r.FCompanyId ?? string.Empty, string.Empty)
            };
        }).ToList();

        return new PagedResult<InventoryListDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    private static PagedResult<InventoryListDto> EmptyPage(PagedRequest request) =>
        new() { Items = new(), TotalCount = 0, PageIndex = request.PageIndex, PageSize = request.PageSize };

    // ===== 名称类筛选：在目标资料表按操作符解析匹配键，用于约束库存表外键列 =====

    /// <summary>按操作符在目标资料表解析匹配的 Uid 集合（仅未删除）。复用 ToConditionalModels 处理 eq/like/gt… 等操作符。</summary>
    private async Task<List<string>> ResolveUidsAsync<T>(string targetProp, DynamicFilterInfo filter) where T : BaseEntity, new()
    {
        var conds = new List<DynamicFilterInfo>
        {
            new() { Field = targetProp, Operator = filter.Operator, Value = filter.Value }
        }.ToConditionalModels<T>();
        var uids = await _db.Queryable<T>().Where(t => !t.FDeleted).Where(conds).Select(t => t.Uid).ToListAsync();
        return uids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
    }

    /// <summary>库存状态名称筛选：解析匹配的状态行，返回其 Uid 与 FNumber（库存表外键可能存两者之一）。</summary>
    private async Task<List<string>> ResolveStockStatusKeysAsync(DynamicFilterInfo filter)
    {
        var conds = new List<DynamicFilterInfo>
        {
            new() { Field = "Fname", Operator = filter.Operator, Value = filter.Value }
        }.ToConditionalModels<TBdStockstatus>();
        var rows = await _db.Queryable<TBdStockstatus>().Where(s => !s.FDeleted).Where(conds)
            .Select(s => new { s.Uid, s.Fnumber }).ToListAsync();
        var keys = new List<string>();
        foreach (var r in rows)
        {
            if (!string.IsNullOrEmpty(r.Uid)) keys.Add(r.Uid);
            if (!string.IsNullOrEmpty(r.Fnumber)) keys.Add(r.Fnumber);
        }
        return keys.Distinct().ToList();
    }

    // ===== 名称字典加载（均按目标资料 Uid 解析；与 InStockService 一致）=====

    private async Task<Dictionary<string, (string Number, string Name, string Spec, bool Kf, int KfPeriod, int KfUnit)>> LoadMaterialDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdMaterial>().Where(m => list.Contains(m.Uid))
            .Select(m => new { m.Uid, m.FNumber, m.FName, m.FSpecification, m.FIsKfPeriod, m.FKfPeriod, m.FKfUnit }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key,
            g => (g.First().FNumber, g.First().FName, g.First().FSpecification, g.First().FIsKfPeriod, g.First().FKfPeriod, g.First().FKfUnit));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadUnitDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdUnit>().Where(u => list.Contains(u.Uid)).Select(u => new { u.Uid, u.FNumber, u.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadStockDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdStock>().Where(s => list.Contains(s.Uid)).Select(s => new { s.Uid, s.FNumber, s.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadStockPlaceDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdStockPlace>().Where(s => list.Contains(s.Uid)).Select(s => new { s.Uid, s.FNumber, s.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, string>> LoadOrgNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<SysOrgStructure>().Where(o => list.Contains(o.Uid)).Select(o => new { o.Uid, o.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadFlexAuxDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdFlexauxproperty>().Where(f => list.Contains(f.Uid)).Select(f => new { f.Uid, f.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    /// <summary>库存状态名称字典：同时以 Uid 和 FNumber 为键（兼容存 Uid 与条码主档存 FNUMBER 两种取值，与 InStockService 一致）</summary>
    private async Task<Dictionary<string, string>> LoadStockStatusDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdStockstatus>()
            .Where(s => list.Contains(s.Uid) || list.Contains(s.Fnumber))
            .Select(s => new { s.Uid, s.Fnumber, s.Fname }).ToListAsync();
        var dict = new Dictionary<string, string>();
        foreach (var r in rows)
        {
            if (!string.IsNullOrEmpty(r.Uid)) dict[r.Uid] = r.Fname;
            if (!string.IsNullOrEmpty(r.Fnumber)) dict[r.Fnumber] = r.Fname;
        }
        return dict;
    }
}
