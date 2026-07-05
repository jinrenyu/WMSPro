using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 生产订单「下达 / 反下达」服务。对照旧系统 OPSOFT.O3.Server.Base ProductiveTask 的 CreatePPBom / ReCommit。
///
/// 下达(Release)：对选中明细行，按其 BOM(T_BD_BOM/ENTRY) 生成一张「生产用料清单」
///   (T_PRD_PPBOM 表头 + T_PRD_PPBOMENTRY 明细)，生成即审核态(FSTATUS=40)；
///   同时把生产订单明细业务状态置为「下达(3)」、记下达日期。
/// 反下达(Unrelease)：删除该明细对应的用料清单(连同明细)，把明细业务状态退回「计划确认(2)」、清下达日期。
///
/// 关联键用明细 Uid(=FDETAILID) 落到用料清单 FMOENTRYID：因为已下达明细被「反审核拦截」(见 ProductionOrderService.RejectAsync)
/// 不能再被编辑重建 Uid，故下达期间 Uid 稳定，不会出现 [[id-association-finterid]] 的孤立。
/// </summary>
public class ProductionReleaseService : IProductionReleaseService
{
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;
    private readonly IBillCodeService _billCode;
    private readonly IOperationLogService? _operationLog;

    private const string PrgKey = "ProductionOrder";

    // 1900 哨兵：满足开发库 SQLite 的 NOT NULL 日期列；前端按 <=1900 视为空
    private static readonly DateTime DateSentinel = new(1900, 1, 1);

    public ProductionReleaseService(
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IBillCodeService billCode,
        IOperationLogService? operationLog = null)
    {
        _db = db;
        _currentUser = currentUser;
        _billCode = billCode;
        _operationLog = operationLog;
    }

    // ===== 下达 =====

    public async Task<ProductionReleaseResultDto> ReleaseAsync(string moUid, List<string> entryUids)
    {
        var result = new ProductionReleaseResultDto();

        var header = await _db.Queryable<TPrdMo>().Where(h => h.Uid == moUid && !h.FDeleted).FirstAsync()
            ?? throw new KeyNotFoundException("生产订单不存在");
        if (header.FStatus != 40)
            throw new InvalidOperationException("只有已审核的生产订单才能下达");

        var uids = (entryUids ?? new List<string>()).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (uids.Count == 0) throw new InvalidOperationException("请先选择要下达的明细行");

        var entries = await _db.Queryable<TPrdMoentry>()
            .Where(e => e.FInterId == moUid && !e.FDeleted && uids.Contains(e.Uid))
            .OrderBy(e => e.Fentryid)
            .ToListAsync();
        if (entries.Count == 0) throw new InvalidOperationException("选中的明细行不存在");

        // 产品物料编号（消息显示用）
        var prodNumberDict = await LoadMaterialNumberDictAsync(entries.Select(e => e.Fmaterialid));

        foreach (var entry in entries)
        {
            var prodNo = prodNumberDict.GetValueOrDefault(entry.Fmaterialid ?? string.Empty, entry.Fmaterialid ?? string.Empty);
            var tag = $"任务单【{header.Fbillno}】行【{entry.Fentryid}】产品【{prodNo}】";

            // 业务状态校验（与旧系统 ProductiveTask_B.Commit 一致）：6=结案，3/4/5=已下达/开工/完工
            if (entry.Fbstatus == "6") { result.Messages.Add($"{tag}已结案，无法下达"); result.FailCount++; continue; }
            if (entry.Fbstatus is "3" or "4" or "5") { result.Messages.Add($"{tag}已下达"); result.FailCount++; continue; }
            if (string.IsNullOrEmpty(entry.Fbomid)) { result.Messages.Add($"{tag}未绑定 BOM，无法下达"); result.FailCount++; continue; }

            // 防重：该明细已存在用料清单
            var exists = await _db.Queryable<TPrdPpbom>().AnyAsync(p => p.Fmoentryid == entry.Uid && !p.FDeleted);
            if (exists) { result.Messages.Add($"{tag}已生成过用料清单，无法重复下达"); result.FailCount++; continue; }

            // 取 BOM 子项（兼容 FBOMID 存 BOM 的 Uid 或 FInterId）
            var bom = await _db.Queryable<TBdBom>().Where(b => b.Uid == entry.Fbomid || b.FInterId == entry.Fbomid).FirstAsync();
            if (bom == null) { result.Messages.Add($"{tag}BOM 不存在，无法下达"); result.FailCount++; continue; }
            var bomKeys = new[] { bom.FInterId, bom.Uid }.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            var bomEntries = await _db.Queryable<TBdBomentry>()
                .Where(e => !e.FDeleted && bomKeys.Contains(e.FInterId))
                .OrderBy(e => e.Fentryid)
                .ToListAsync();
            if (bomEntries.Count == 0) { result.Messages.Add($"{tag}缺少 BOM 信息，无法下达"); result.FailCount++; continue; }

            // 子项物料基本单位（兼容子项 FMATERIALID 存 Uid 或 FInterId）
            var matBaseUnit = await LoadMaterialBaseUnitDictAsync(bomEntries.Select(b => b.Fmaterialid));

            // 单行各自事务：硬失败仅降级为一条失败项并继续，不中断整批、不丢弃已累计的 result
            try
            {
                var billNo = await GenerateAndPersistAsync(header, entry, bomEntries, matBaseUnit);
                result.Messages.Add($"{tag}下达成功");
                result.BillNos.Add(billNo);
                result.SuccessCount++;
            }
            catch (Exception ex)
            {
                result.Messages.Add($"{tag}下达失败：{ex.Message}");
                result.FailCount++;
            }
        }

        if (result.SuccessCount > 0 && _operationLog != null)
            await _operationLog.LogAsync(PrgKey, OperationType.Update, moUid, header.Fbillno,
                $"下达 {result.SuccessCount} 行，生成用料清单 {string.Join("、", result.BillNos)}", true);

        return result;
    }

    /// <summary>单行下达落库（事务内：取号 + 写用料清单主从 + 回写明细状态）。返回生成的用料清单单号。</summary>
    private async Task<string> GenerateAndPersistAsync(
        TPrdMo header, TPrdMoentry entry, List<TBdBomentry> bomEntries,
        Dictionary<string, string> matBaseUnit)
    {
        try
        {
            _db.AsTenant().BeginTran();

            // 事务内重检防重：外层预检之后、并发/重复提交可能已被另一请求生成（关闭 check-then-act 竞态窗口）
            if (await _db.Queryable<TPrdPpbom>().AnyAsync(p => p.Fmoentryid == entry.Uid && !p.FDeleted))
                throw new InvalidOperationException("已生成过用料清单，无法重复下达");

            var now = DateTime.Now;
            var ppbomUid = Guid.NewGuid().ToString("N");
            var userId = _currentUser.UserId ?? string.Empty;
            var commonUnit = string.IsNullOrEmpty(entry.Fcommonunitid) ? entry.Fbaseunitid : entry.Fcommonunitid;

            // 取号：生产用料清单编号规则（SCYL），事务内原子占号、回滚一并释放
            var ctx = new Dictionary<string, string> { [BillCodeFields.Date] = now.ToString("yyyy-MM-dd HH:mm:ss") };
            var billNo = await _billCode.ResolveBillNoAsync(
                BillCodeFormKeys.ProductionMaterialList, null, ctx,
                no => _db.Queryable<TPrdPpbom>().AnyAsync(p => p.Fbillno == no));

            var ppbom = new TPrdPpbom
            {
                Uid = ppbomUid,
                FInterId = ppbomUid,
                Fbillno = billNo,
                Fdate = now,
                Fmaterialid = entry.Fmaterialid,
                Fworkshopid = entry.Fworkshopid,
                Fmoentryseq = entry.Fentryid,
                Funitid = commonUnit,
                Fdocumentstatus = "0",
                Fqty = entry.Fqty,
                Fmoid = header.Uid,
                Fmoentryid = entry.Uid,
                Fbomid = entry.Fbomid,
                Fauxpropid = entry.Fauxpropid,
                Fmobillno = header.Fbillno,
                FStatus = 40,             // 生成即审核态（对照旧系统 FSTATUS=40）
                Fcheckerid = userId,
                Fcheckdate = now,
                Fdisabledate = DateSentinel,
                FCompanyId = header.FCompanyId,
                CYmd = now,
                CUser = userId,
                MYmd = now,
                MUser = userId
            };
            await _db.Insertable(ppbom).ExecuteCommandAsync();

            var ppbomEntries = new List<TPrdPpbomentry>();
            int seq = 1;
            foreach (var b in bomEntries)
            {
                var matKey = b.Fmaterialid ?? string.Empty;
                matBaseUnit.TryGetValue(matKey, out var matBase);
                // 基本单位取子项物料基本单位（缺则回落 BOM 行单位/产品单位）；常用单位取 BOM 子项用量单位 b.Funitid，
                // 与 FNUMERATOR/FDENOMINATOR 同口径——qty 即按此单位表达，避免应发数量单位标注错位
                var baseUnit = string.IsNullOrEmpty(matBase) ? (string.IsNullOrEmpty(b.Funitid) ? commonUnit : b.Funitid) : matBase;
                var lineUnit = string.IsNullOrEmpty(b.Funitid) ? baseUnit : b.Funitid;

                var den = b.Fdenominator == 0 ? 1 : b.Fdenominator;
                var qty = Math.Round(b.Fnumerator / den * entry.Fqty, 6);     // 应发数量 = 单位用量(分子/分母) × 生产数量
                // 基本单位应发数量：与手工建单路径(ProductionMaterialListService 的 Fbaseqty=Fmustqty)口径一致，
                // 暂不做跨单位换算（项目级单位换算模型完善后两条路径统一接入）
                var baseQty = qty;

                var detailUid = Guid.NewGuid().ToString("N");
                ppbomEntries.Add(new TPrdPpbomentry
                {
                    Uid = detailUid,
                    FInterId = ppbomUid,
                    Fentryid = seq,
                    Fdetailid = detailUid,
                    Freplacegroup = b.Freplacegroup > 0 ? b.Freplacegroup : seq,
                    Fmaterialid = b.Fmaterialid,
                    Fmaterialtype = string.IsNullOrEmpty(b.Fmaterialtype) ? "1" : b.Fmaterialtype,
                    Foperid = b.Foperid,
                    Fproductid = entry.Fmaterialid,
                    Fcommonunitid = lineUnit,
                    Fbaseunitid = baseUnit,
                    Fnumerator = b.Fnumerator,
                    Fdenominator = b.Fdenominator,
                    Fscraprate = b.Fscraprate,
                    Ffixscrapqty = b.Ffixscrapqty,
                    Fuserate = 100,
                    Fmustqty = qty,
                    Fbaseqty = baseQty,
                    Fnopickedqty = qty,
                    Fnopickedbaseqty = baseQty,
                    Fauxpropid = b.Fauxpropid,
                    Fstockid = b.Fstockid,
                    Fstocklocid = b.Fstocklocid,
                    Fbackflush = b.Fbackflush ? 1 : 0,
                    Ffeedsn = b.Ffeedsn,
                    Fothersn = b.Fothersn,
                    Fisskip = b.Fisskip,
                    Fnote = b.Fnote,
                    Fsenditemdate = (entry.Fplanstartdate == null || entry.Fplanstartdate <= DateSentinel) ? DateSentinel : entry.Fplanstartdate,
                    FStatus = 40,
                    FCompanyId = header.FCompanyId,
                    CYmd = now,
                    CUser = userId,
                    MYmd = now,
                    MUser = userId
                });
                seq++;
            }
            await _db.Insertable(ppbomEntries).ExecuteCommandAsync();

            // 回写生产订单明细：业务状态=下达(3)、下达日期=now
            await _db.Updateable<TPrdMoentry>()
                .SetColumns(e => e.Fbstatus == "3")
                .SetColumns(e => e.Fconveydate == now)
                .SetColumns(e => e.MYmd == now)
                .SetColumns(e => e.MUser == userId)
                .Where(e => e.Uid == entry.Uid)
                .ExecuteCommandAsync();

            _db.AsTenant().CommitTran();
            return billNo;
        }
        catch
        {
            _db.AsTenant().RollbackTran();
            throw;
        }
    }

    // ===== 反下达 =====

    public async Task<ProductionReleaseResultDto> UnreleaseAsync(string moUid, List<string> entryUids)
    {
        var result = new ProductionReleaseResultDto();

        var header = await _db.Queryable<TPrdMo>().Where(h => h.Uid == moUid && !h.FDeleted).FirstAsync()
            ?? throw new KeyNotFoundException("生产订单不存在");
        if (header.FStatus != 40)
            throw new InvalidOperationException("只有已审核的生产订单才能反下达");

        var uids = (entryUids ?? new List<string>()).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (uids.Count == 0) throw new InvalidOperationException("请先选择要反下达的明细行");

        var entries = await _db.Queryable<TPrdMoentry>()
            .Where(e => e.FInterId == moUid && !e.FDeleted && uids.Contains(e.Uid))
            .OrderBy(e => e.Fentryid)
            .ToListAsync();
        if (entries.Count == 0) throw new InvalidOperationException("选中的明细行不存在");

        var prodNumberDict = await LoadMaterialNumberDictAsync(entries.Select(e => e.Fmaterialid));

        foreach (var entry in entries)
        {
            var prodNo = prodNumberDict.GetValueOrDefault(entry.Fmaterialid ?? string.Empty, entry.Fmaterialid ?? string.Empty);
            var tag = $"任务单【{header.Fbillno}】行【{entry.Fentryid}】产品【{prodNo}】";

            if (entry.Fbstatus != "3") { result.Messages.Add($"{tag}业务状态不是下达，无法反下达"); result.FailCount++; continue; }

            var entryUid = entry.Uid; // 闭包捕获局部变量
            var userId = _currentUser.UserId ?? string.Empty;
            var sentinel = DateSentinel;
            try
            {
                _db.AsTenant().BeginTran();

                // 取该明细对应的用料清单单号（消息回显），再级联硬删主从（对照旧系统 DELETE FROM）
                var ppboms = await _db.Queryable<TPrdPpbom>().Where(p => p.Fmoentryid == entryUid).ToListAsync();
                var ppbomIds = ppboms.Select(p => p.Uid).ToList();
                if (ppbomIds.Count > 0)
                {
                    await _db.Deleteable<TPrdPpbomentry>().Where(e => ppbomIds.Contains(e.FInterId)).ExecuteCommandAsync();
                    await _db.Deleteable<TPrdPpbom>().Where(p => ppbomIds.Contains(p.Uid)).ExecuteCommandAsync();
                }

                // 回退明细：业务状态=计划确认(2)、清下达日期
                await _db.Updateable<TPrdMoentry>()
                    .SetColumns(e => e.Fbstatus == "2")
                    .SetColumns(e => e.Fconveydate == sentinel)
                    .SetColumns(e => e.MYmd == DateTime.Now)
                    .SetColumns(e => e.MUser == userId)
                    .Where(e => e.Uid == entryUid)
                    .ExecuteCommandAsync();

                _db.AsTenant().CommitTran();

                foreach (var p in ppboms) result.BillNos.Add(p.Fbillno);
                result.Messages.Add($"{tag}反下达成功");
                result.SuccessCount++;
            }
            catch (Exception ex)
            {
                _db.AsTenant().RollbackTran();
                result.Messages.Add($"{tag}反下达失败：{ex.Message}");
                result.FailCount++;
            }
        }

        if (result.SuccessCount > 0 && _operationLog != null)
            await _operationLog.LogAsync(PrgKey, OperationType.Update, moUid, header.Fbillno,
                $"反下达 {result.SuccessCount} 行，删除用料清单 {string.Join("、", result.BillNos)}", true);

        return result;
    }

    // ===== 辅助 =====

    /// <summary>物料编号字典（按物料 Uid 或 FInterId 命中，键为传入的 id 值）。</summary>
    private async Task<Dictionary<string, string>> LoadMaterialNumberDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        var dict = new Dictionary<string, string>();
        if (list.Count == 0) return dict;
        var rows = await _db.Queryable<TBdMaterial>()
            .Where(m => list.Contains(m.Uid) || list.Contains(m.FInterId))
            .Select(m => new { m.Uid, m.FInterId, m.FNumber }).ToListAsync();
        foreach (var id in list)
        {
            var r = rows.FirstOrDefault(x => x.Uid == id) ?? rows.FirstOrDefault(x => x.FInterId == id);
            if (r != null) dict[id] = r.FNumber;
        }
        return dict;
    }

    /// <summary>子项物料基本单位字典，键为传入的 id 值（兼容物料 Uid / FInterId）。</summary>
    private async Task<Dictionary<string, string>> LoadMaterialBaseUnitDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        var dict = new Dictionary<string, string>();
        if (list.Count == 0) return dict;
        var rows = await _db.Queryable<TBdMaterial>()
            .Where(m => list.Contains(m.Uid) || list.Contains(m.FInterId))
            .Select(m => new { m.Uid, m.FInterId, m.FBaseUnitId }).ToListAsync();
        foreach (var id in list)
        {
            var r = rows.FirstOrDefault(x => x.Uid == id) ?? rows.FirstOrDefault(x => x.FInterId == id);
            if (r != null) dict[id] = r.FBaseUnitId;
        }
        return dict;
    }
}
