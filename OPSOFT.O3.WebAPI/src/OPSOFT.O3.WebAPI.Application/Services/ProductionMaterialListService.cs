using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using OPSOFT.O3.WebAPI.Application.Extensions;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 生产用料清单 / 生产投料单服务（真实表 T_PRD_PPBOM / T_PRD_PPBOMENTRY）。
/// 一主一从：主表=一张用料清单（对应一条生产订单产品行），从表=该产品用料明细。
/// 列表一行=一张主单（非按明细展开）；详情解析主表/明细全部外键名称。
/// 状态：10=草稿(未审)、40=审核(已审)、70=关闭。沿用生产订单单据范式。
/// </summary>
public class ProductionMaterialListService : DocumentService<TPrdPpbom, TPrdPpbomentry,
    ProductionMaterialListListDto, ProductionMaterialListDetailDto, CreateProductionMaterialListRequest, UpdateProductionMaterialListRequest>
{
    public ProductionMaterialListService(
        IRepository<TPrdPpbom> headerRepo,
        IRepository<TPrdPpbomentry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IBillCodeService billCode,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog, billCode)
    {
    }

    protected override string PrgKey => "ProductionMaterialList";

    // 1900 哨兵：满足开发库 SQLite 的 NOT NULL 日期列，且生产 SQLServer DATETIME(下限1753)安全；前端按 <=1900 视为空
    private static readonly DateTime DateSentinel = new(1900, 1, 1);

    // ===== 审核 / 反审核 / 关闭 =====

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("单据已审核，无需重复审核");

        var result = await Db.Updateable<TPrdPpbom>()
            .SetColumns(h => h.FStatus == 40)
            .SetColumns(h => h.Fcheckerid == (CurrentUser.UserId ?? string.Empty))
            .SetColumns(h => h.Fcheckdate == DateTime.Now)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Approve, uid, header.Fbillno, "审核单据", result);
        return result;
    }

    /// <summary>反审核（回到草稿 10）</summary>
    public override async Task<bool> RejectAsync(string uid, string? reason = null)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能反审核");

        // 哨兵存入局部变量：SqlSugar 解析 SetColumns 表达式树时拒绝 private 字段（见 [[sqlsugar-expr-private-field-trap]]）
        var sentinel = DateSentinel;
        var result = await Db.Updateable<TPrdPpbom>()
            .SetColumns(h => h.FStatus == 10)
            .SetColumns(h => h.Fcheckerid == string.Empty)
            .SetColumns(h => h.Fcheckdate == sentinel)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Reject, uid, header.Fbillno, reason ?? "反审核单据", result);
        return result;
    }

    public override async Task<bool> CloseAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能关闭");

        var result = await Db.Updateable<TPrdPpbom>()
            .SetColumns(h => h.FStatus == 70)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<TPrdPpbom, bool>> BuildSearchPredicate(string keyword)
        => h => h.Fbillno.Contains(keyword);

    // 已审核单据禁止修改/删除（前端只读外的后端兜底，须先反审核）
    public override async Task<bool> UpdateAsync(string uid, UpdateProductionMaterialListRequest request)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的单据不能修改，请先反审核");
        return await base.UpdateAsync(uid, request);
    }

    public override async Task<bool> DeleteAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的单据不能删除，请先反审核");
        return await base.DeleteAsync(uid);
    }

    // ===== 列表：一行一张主单 + 名称解析 =====

    public override async Task<PagedResult<ProductionMaterialListListDto>> GetPagedListAsync(PagedRequest request)
    {
        var filters = request.DynamicFilters ?? new List<DynamicFilterInfo>();

        var query = Db.Queryable<TPrdPpbom>().Where(h => !h.FDeleted);
        if (request.OnlyApproved)
            query = query.Where(h => h.FStatus == 40 && !h.FDisabled);
        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var kw = request.Keyword.Trim();
            query = query.Where(h => h.Fbillno.Contains(kw) || h.Fmobillno.Contains(kw));
        }
        if (filters.Count > 0)
            query = query.Where(filters.ToConditionalModels<TPrdPpbom>());

        RefAsync<int> totalCount = 0;
        var headers = await query
            .OrderBy(h => h.CYmd, OrderByType.Desc)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        if (headers.Count == 0)
            return new PagedResult<ProductionMaterialListListDto> { Items = new(), TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };

        var materialDict = await LoadMaterialDictAsync(headers.Select(h => h.Fmaterialid));
        var statusDict = await LoadStatusDictAsync();
        var companyDict = await LoadOrgDictAsync(headers.Select(h => h.FCompanyId));
        var userDict = await LoadUserNameDictAsync(headers.SelectMany(h => new[] { h.CUser, h.MUser, h.Fcheckerid, h.Fdisableid }));

        var items = headers.Select(h =>
        {
            materialDict.TryGetValue(h.Fmaterialid ?? string.Empty, out var mat);
            return new ProductionMaterialListListDto
            {
                Uid = h.Uid,
                Fbillno = h.Fbillno,
                Fmobillno = h.Fmobillno,
                Fmoentryseq = h.Fmoentryseq,
                Fdate = h.Fdate,
                Fmaterialid = h.Fmaterialid,
                FmaterialNumber = mat.Number,
                FmaterialName = mat.Name,
                FSpecification = mat.Spec,
                Fqty = h.Fqty,
                Fnote = h.Fnote,
                FStatus = h.FStatus,
                FstatusName = statusDict.GetValueOrDefault(h.FStatus, string.Empty),
                FDisabled = h.FDisabled,
                CuserName = userDict.GetValueOrDefault(h.CUser ?? string.Empty, string.Empty),
                CYmd = h.CYmd,
                MuserName = userDict.GetValueOrDefault(h.MUser ?? string.Empty, string.Empty),
                MYmd = h.MYmd,
                FcheckerName = userDict.GetValueOrDefault(h.Fcheckerid ?? string.Empty, string.Empty),
                Fcheckdate = h.Fcheckdate,
                FdisableName = userDict.GetValueOrDefault(h.Fdisableid ?? string.Empty, string.Empty),
                FcompanyName = companyDict.GetValueOrDefault(h.FCompanyId ?? string.Empty, string.Empty)
            };
        }).ToList();

        return new PagedResult<ProductionMaterialListListDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    protected override ProductionMaterialListListDto MapToListDto(TPrdPpbom entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fmobillno = entity.Fmobillno,
        Fdate = entity.Fdate,
        Fqty = entity.Fqty,
        FStatus = entity.FStatus
    };

    // ===== 详情：主表 + 名称解析 + 明细 =====

    public override async Task<ProductionMaterialListDetailDto?> GetByIdAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return null;

        var entries = await GetEntriesByHeaderIdAsync(uid);
        var dto = MapToDetailDto(header, entries);

        // 主表名称
        var headerMatDict = await LoadMaterialDictAsync(new[] { header.Fmaterialid });
        if (headerMatDict.TryGetValue(header.Fmaterialid ?? string.Empty, out var hm))
        {
            dto.FmaterialNumber = hm.Number; dto.FmaterialName = hm.Name; dto.FSpecification = hm.Spec;
        }
        var headerAuxEnabled = await LoadAuxEnabledSetAsync(new[] { header.Fmaterialid });
        dto.FisAuxEnabled = !string.IsNullOrEmpty(header.Fmaterialid) && headerAuxEnabled.Contains(header.Fmaterialid);
        dto.FauxpropName = (await LoadFlexAuxDictAsync(new[] { header.Fauxpropid })).GetValueOrDefault(header.Fauxpropid ?? string.Empty, string.Empty);

        var motypeDict = await LoadProduceTypeDictAsync(new[] { header.Fmotype });
        if (motypeDict.TryGetValue(header.Fmotype ?? string.Empty, out var mt))
        { dto.FmotypeNumber = mt.Number; dto.FmotypeName = mt.Name; }

        var unitDict = await LoadUnitDictAsync(new[] { header.Funitid });
        if (unitDict.TryGetValue(header.Funitid ?? string.Empty, out var hu))
        { dto.FunitNumber = hu.Number; dto.FunitName = hu.Name; }

        dto.FcompanyName = await Db.Queryable<SysOrgStructure>().Where(o => o.Uid == header.FCompanyId).Select(o => o.Fname).FirstAsync() ?? string.Empty;
        dto.FstatusName = (await LoadStatusDictAsync()).GetValueOrDefault(header.FStatus, string.Empty);

        var userDict = await LoadUserNameDictAsync(new[] { header.CUser, header.MUser, header.Fcheckerid, header.Fdisableid });
        dto.CuserName = userDict.GetValueOrDefault(header.CUser, string.Empty);
        dto.MuserName = userDict.GetValueOrDefault(header.MUser, string.Empty);
        dto.FcheckerName = userDict.GetValueOrDefault(header.Fcheckerid, string.Empty);
        dto.FdisableName = userDict.GetValueOrDefault(header.Fdisableid, string.Empty);

        // 明细名称
        if (dto.Entries.Count > 0)
        {
            var matDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
            var euDict = await LoadUnitDictAsync(entries.SelectMany(e => new[] { e.Fbaseunitid, e.Fcommonunitid }));
            var procDict = await LoadProcessDictAsync(entries.Select(e => e.Foperid));
            var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));
            var stockDict = await LoadStockDictAsync(entries.Select(e => e.Fstockid));
            var locDict = await LoadStockLocDictAsync(entries.Select(e => e.Fstocklocid));
            var auxEnabled = await LoadAuxEnabledSetAsync(entries.Select(e => e.Fmaterialid));

            foreach (var line in dto.Entries)
            {
                if (matDict.TryGetValue(line.Fmaterialid ?? string.Empty, out var m))
                {
                    line.FmaterialNumber = m.Number; line.FmaterialName = m.Name;
                    line.FSpecification = m.Spec; line.FisBatchManage = m.Batch;
                }
                if (euDict.TryGetValue(line.Fbaseunitid ?? string.Empty, out var bu))
                { line.FbaseunitNumber = bu.Number; line.FbaseunitName = bu.Name; }
                if (euDict.TryGetValue(line.Fcommonunitid ?? string.Empty, out var cu))
                { line.FcommonunitNumber = cu.Number; line.FcommonunitName = cu.Name; }
                if (procDict.TryGetValue(line.Foperid ?? string.Empty, out var pr))
                { line.FoperNumber = pr.Number; line.FoperName = pr.Name; }
                if (stockDict.TryGetValue(line.Fstockid ?? string.Empty, out var st))
                { line.FstockNumber = st.Number; line.FstockName = st.Name; line.FisOpenLocation = st.OpenLoc; }
                line.FstocklocName = locDict.GetValueOrDefault(line.Fstocklocid ?? string.Empty, string.Empty);
                line.FauxpropName = flexDict.GetValueOrDefault(line.Fauxpropid ?? string.Empty, string.Empty);
                line.FisAuxEnabled = !string.IsNullOrEmpty(line.Fmaterialid) && auxEnabled.Contains(line.Fmaterialid);
            }
        }

        return dto;
    }

    protected override ProductionMaterialListDetailDto MapToDetailDto(TPrdPpbom header, List<TPrdPpbomentry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fdate = header.Fdate,
        FStatus = header.FStatus,
        Fmobillno = header.Fmobillno,
        Fmoid = header.Fmoid,
        Fmoentryid = header.Fmoentryid,
        Fmoentryseq = header.Fmoentryseq,
        Fmaterialid = header.Fmaterialid,
        Fauxpropid = header.Fauxpropid,
        Fmotype = header.Fmotype,
        Fqty = header.Fqty,
        Funitid = header.Funitid,
        Fcompanyid = header.FCompanyId,
        Fnote = header.Fnote,
        CUser = header.CUser,
        CYmd = header.CYmd,
        Fcheckerid = header.Fcheckerid,
        Fcheckdate = header.Fcheckdate,
        MUser = header.MUser,
        MYmd = header.MYmd,
        Fdisableid = header.Fdisableid,
        Fdisabledate = header.Fdisabledate,
        FDisabled = header.FDisabled,
        Entries = entries.Select(e => new ProductionMaterialListEntryDto
        {
            Uid = e.Uid,
            Fentryid = e.Fentryid,
            Freplacegroup = e.Freplacegroup,
            Fmaterialtype = string.IsNullOrEmpty(e.Fmaterialtype) ? "1" : e.Fmaterialtype,
            Fmaterialid = e.Fmaterialid,
            Foperid = e.Foperid,
            Fbaseunitid = e.Fbaseunitid,
            Fbaseqty = e.Fbaseqty,
            Fcommonunitid = e.Fcommonunitid,
            Flot = e.Flot,
            Fauxpropid = e.Fauxpropid,
            Fuserate = e.Fuserate,
            Fnumerator = e.Fnumerator,
            Fdenominator = e.Fdenominator,
            Fscraprate = e.Fscraprate,
            Ffixscrapqty = e.Ffixscrapqty,
            Fmustqty = e.Fmustqty,
            Fpickedqty = e.Fpickedqty,
            Fnopickedqty = e.Fnopickedqty,
            Fsenditemdate = e.Fsenditemdate,
            Fstockid = e.Fstockid,
            Fstocklocid = e.Fstocklocid,
            Fbackflush = e.Fbackflush,
            Ffeedsn = e.Ffeedsn,
            Fothersn = e.Fothersn,
            Fnote = e.Fnote
        }).ToList()
    };

    // ===== 写入映射 =====

    protected override TPrdPpbom MapToHeaderEntity(CreateProductionMaterialListRequest dto) => new()
    {
        Fbillno = dto.Fbillno?.Trim() ?? string.Empty, // 为空时由 PrepareHeaderForCreateAsync 按编码规则取号
        Fdate = dto.Fdate ?? DateTime.Now,
        Fmobillno = dto.Fmobillno,
        Fmoid = dto.Fmoid,
        Fmoentryid = dto.Fmoentryid,
        Fmoentryseq = dto.Fmoentryseq,
        Fmaterialid = dto.Fmaterialid,
        Fauxpropid = dto.Fauxpropid,
        Fmotype = dto.Fmotype,
        Fqty = dto.Fqty,
        Funitid = dto.Funitid,
        FCompanyId = dto.Fcompanyid, // 组织（为空时由基类回落当前登录组织）
        Fnote = dto.Fnote,
        Fcheckdate = DateSentinel,
        Fdisabledate = DateSentinel,
        FStatus = 10
    };

    // ===== 编码规则取号：SCYL + yyyyMMdd + 4位日流水 =====
    protected override string? BillCodeFormKey => BillCodeFormKeys.ProductionMaterialList;
    protected override string GetBillNo(TPrdPpbom header) => header.Fbillno;
    protected override void SetBillNo(TPrdPpbom header, string billNo) => header.Fbillno = billNo;
    protected override Task<bool> BillNoExistsAsync(string billNo)
        => Db.Queryable<TPrdPpbom>().AnyAsync(h => h.Fbillno == billNo);

    protected override Task PopulateBillCodeContextAsync(IDictionary<string, string> ctx, TPrdPpbom header, CreateProductionMaterialListRequest dto)
    {
        ctx[BillCodeFields.Date] = (header.Fdate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
        return Task.CompletedTask;
    }

    protected override void UpdateHeaderEntity(TPrdPpbom entity, UpdateProductionMaterialListRequest dto)
    {
        entity.Fdate = dto.Fdate ?? entity.Fdate;
        entity.Fmobillno = dto.Fmobillno;
        entity.Fmoid = dto.Fmoid;
        entity.Fmoentryid = dto.Fmoentryid;
        entity.Fmoentryseq = dto.Fmoentryseq;
        entity.Fmaterialid = dto.Fmaterialid;
        entity.Fauxpropid = dto.Fauxpropid;
        entity.Fmotype = dto.Fmotype;
        entity.Fqty = dto.Fqty;
        entity.Funitid = dto.Funitid;
        if (!string.IsNullOrEmpty(dto.Fcompanyid)) entity.FCompanyId = dto.Fcompanyid;
        entity.Fnote = dto.Fnote;
    }

    protected override List<TPrdPpbomentry> MapToEntryEntities(CreateProductionMaterialListRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    protected override List<TPrdPpbomentry> MapToEntryEntities(UpdateProductionMaterialListRequest dto, string headerUid)
        => MapEntries(dto.Entries, headerUid);

    private static List<TPrdPpbomentry> MapEntries(List<CreateProductionMaterialListEntryRequest> entries, string headerUid)
        => entries.Select(e => new TPrdPpbomentry
        {
            FInterId = headerUid,
            Freplacegroup = e.Freplacegroup,
            Fmaterialtype = string.IsNullOrEmpty(e.Fmaterialtype) ? "1" : e.Fmaterialtype,
            Fmaterialid = e.Fmaterialid,
            Foperid = e.Foperid,
            Fbaseunitid = string.IsNullOrEmpty(e.Fbaseunitid) ? e.Fcommonunitid : e.Fbaseunitid,
            Fcommonunitid = string.IsNullOrEmpty(e.Fcommonunitid) ? e.Fbaseunitid : e.Fcommonunitid,
            Flot = e.Flot,
            Fauxpropid = e.Fauxpropid,
            Fuserate = e.Fuserate,
            Fnumerator = e.Fnumerator,
            Fdenominator = e.Fdenominator,
            Fscraprate = e.Fscraprate,
            Ffixscrapqty = e.Ffixscrapqty,
            Fmustqty = e.Fmustqty,
            Fbaseqty = e.Fmustqty, // 基本单位应发数量回落为应发数量
            Fstockid = e.Fstockid,
            Fstocklocid = e.Fstocklocid,
            Fbackflush = e.Fbackflush,
            Ffeedsn = e.Ffeedsn,
            Fothersn = e.Fothersn,
            Fnote = e.Fnote,
            // 计划发料日期：空值统一 1900 哨兵（dev SQLite NOT NULL）
            Fsenditemdate = e.Fsenditemdate ?? DateSentinel
        }).ToList();

    protected override void SetEntryIndex(TPrdPpbomentry entry, int index)
    {
        entry.Fentryid = index;
        if (entry.Freplacegroup <= 0) entry.Freplacegroup = index;
        entry.Fdetailid = entry.Uid;
    }

    protected override async Task<List<TPrdPpbomentry>> GetEntriesByHeaderIdAsync(string headerUid)
        => await Db.Queryable<TPrdPpbomentry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.Fentryid)
            .ToListAsync();

    // ===== 名称字典加载 =====

    private async Task<Dictionary<string, (string Number, string Name, string Spec, bool Batch)>> LoadMaterialDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdMaterial>().Where(m => list.Contains(m.Uid))
            .Select(m => new { m.Uid, m.FNumber, m.FName, m.FSpecification, m.FIsBatchManage }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key,
            g => (g.First().FNumber, g.First().FName, g.First().FSpecification, g.First().FIsBatchManage));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadUnitDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdUnit>().Where(u => list.Contains(u.Uid))
            .Select(u => new { u.Uid, u.FNumber, u.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadProcessDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TSfcProcess>().Where(p => list.Contains(p.Uid))
            .Select(p => new { p.Uid, p.FNUMBER, p.FNAME }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNUMBER, g.First().FNAME));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadProduceTypeDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBasAssistantdataentry>().Where(e => list.Contains(e.Uid))
            .Select(e => new { e.Uid, e.Fnumber, e.Fdatavalue }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().Fnumber, g.First().Fdatavalue));
    }

    private async Task<Dictionary<string, (string Number, string Name, bool OpenLoc)>> LoadStockDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdStock>().Where(s => list.Contains(s.Uid))
            .Select(s => new { s.Uid, s.FNumber, s.FName, s.FIsOpenLocation }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName, g.First().FIsOpenLocation));
    }

    private async Task<Dictionary<string, string>> LoadStockLocDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdStockPlace>().Where(f => list.Contains(f.Uid))
            .Select(f => new { f.Uid, f.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().FName);
    }

    private async Task<Dictionary<string, string>> LoadUserNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysLoginUser>().Where(u => list.Contains(u.UserId))
            .Select(u => new { u.UserId, u.UserName }).ToListAsync();
        return rows.GroupBy(r => r.UserId).ToDictionary(g => g.Key, g => g.First().UserName);
    }

    private async Task<Dictionary<string, string>> LoadOrgDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysOrgStructure>().Where(o => list.Contains(o.Uid))
            .Select(o => new { o.Uid, o.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<int, string>> LoadStatusDictAsync()
    {
        var rows = await Db.Queryable<SysStatus>().Select(s => new { s.Fitemid, s.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Fitemid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadFlexAuxDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdFlexauxproperty>().Where(f => list.Contains(f.Uid))
            .Select(f => new { f.Uid, f.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    /// <summary>求出"启用辅助属性"的物料 Uid 集合（按物料的 Uid/FInterId 关联 T_BD_MATERIALAUXPTY）</summary>
    private async Task<HashSet<string>> LoadAuxEnabledSetAsync(IEnumerable<string> materialIds)
    {
        var result = new HashSet<string>();
        var matIdList = materialIds.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (matIdList.Count == 0) return result;

        var mats = await Db.Queryable<TBdMaterial>().Where(m => matIdList.Contains(m.Uid))
            .Select(m => new { m.Uid, m.FInterId }).ToListAsync();
        var allKeys = mats.SelectMany(m => new[] { m.Uid, m.FInterId }).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (allKeys.Count == 0) return result;

        var auxRows = await Db.Queryable<TBdMaterialAuxPty>()
            .Where(a => a.FIsEnable && !a.FDeleted && (allKeys.Contains(a.FInterId) || allKeys.Contains(a.FMasterId)))
            .Select(a => new { a.FInterId, a.FMasterId }).ToListAsync();
        var enabledKeys = new HashSet<string>();
        foreach (var ar in auxRows)
        {
            if (!string.IsNullOrEmpty(ar.FInterId)) enabledKeys.Add(ar.FInterId);
            if (!string.IsNullOrEmpty(ar.FMasterId)) enabledKeys.Add(ar.FMasterId);
        }
        foreach (var m in mats)
        {
            if ((m.Uid != null && enabledKeys.Contains(m.Uid)) || (m.FInterId != null && enabledKeys.Contains(m.FInterId)))
                result.Add(m.Uid);
        }
        return result;
    }
}
