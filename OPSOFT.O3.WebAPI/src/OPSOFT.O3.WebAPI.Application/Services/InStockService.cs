using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Extensions;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 采购入库单服务（真实表 T_STK_INSTOCK 主表 + T_STK_INSTOCKENTRY[物料汇总] + ENTRY1[录入条码] + ENTRY2[底阶条码]）。
/// 一主三从：重写 Create/Update/Delete 在单一事务内级联三张明细表。
/// 录入类型 Ftypeid：1=物料（直录物料汇总）；2=条码（扫码写 ENTRY1/ENTRY2，服务端按物料汇总到 ENTRY，FBODYID 回填父阶）。
/// 状态：10=草稿(未审)、40=审核(已审)、70=关闭。
/// </summary>
public class InStockService : DocumentService<TStkInstock, TStkInstockentry,
    InStockListDto, InStockDetailDto, CreateInStockRequest, UpdateInStockRequest>, IInStockService
{
    public InStockService(
        IRepository<TStkInstock> headerRepo,
        IRepository<TStkInstockentry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IBillCodeService billCode,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog, billCode)
    {
    }

    protected override string PrgKey => "InStock";

    // ===== 审核 / 反审核 / 关闭 =====

    /// <summary>审核=入库过账（事务内）：①条码主档原子翻转(防并发占用) ②即时库存累加 ③单据置 40</summary>
    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("单据已审核，无需重复审核");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭，不能审核");

        try
        {
            Db.AsTenant().BeginTran();

            // ① 单据级乐观锁：状态前置翻转(仅草稿10可审)，受影响行=0 即被并发抢占 → 抛错回滚。
            //    这是物料路径与条码路径共用的并发闸（防重复点击/超时重试/两请求并发重复加库存）。
            var locked = await Db.Updateable<TStkInstock>()
                .SetColumns(h => h.FStatus == 40)
                .SetColumns(h => h.Fcheckerid == (CurrentUser.UserId ?? string.Empty))
                .SetColumns(h => h.Fcheckdate == DateTime.Now)
                .SetColumns(h => h.MYmd == DateTime.Now)
                .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
                .Where(h => h.Uid == uid && h.FStatus == 10)
                .ExecuteCommandAsync();
            if (locked == 0) throw new InvalidOperationException("单据状态已变更（可能已被审核），请刷新后重试");

            // ② 条码主档原子翻转：初始(1)→收料(2)、未入库(0)→已入库(1)，回写仓库/仓位/库存状态；
            //    带状态条件 WHERE Fbarcodestatus==1，受影响行=0 即被其它单据抢占 → 抛错回滚整单（防并发重复入库）
            await PostBarcodesOnApproveAsync(uid);
            // ③ 即时库存累加（按物料汇总明细，维度=物料+仓库+仓位+库存组织+辅助属性+库存状态+批次+生产日期+有效期）
            var entries = await Db.Queryable<TStkInstockentry>().Where(e => e.FInterId == uid && !e.FDeleted).ToListAsync();
            foreach (var e in entries) await ApplyInventoryAsync(e, header, +1);
            // ④ 回写源单累计入库数量：按录入明细 ENTRY1 逐条归集（每条带各自源单/订单行，避免汇总 ENTRY 多源合并误记）；
            //    无 ENTRY1(物料直录)时回落物料汇总 ENTRY。事务内。
            var srcLines = await Db.Queryable<TStkInstockentry1>().Where(x => x.FInterId == uid && !x.FDeleted).ToListAsync();
            if (srcLines.Count > 0) await UpdateSourceCumulativeFromLinesAsync(srcLines, +1);
            else await UpdateSourceCumulativeAsync(entries, +1);

            Db.AsTenant().CommitTran();
            if (OperationLog != null) await OperationLog.LogAsync(PrgKey, OperationType.Approve, uid, header.Fbillno, "审核入库过账", true);
            return true;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    /// <summary>反审核=冲回（事务内）：①条码冲回(收料→初始/已入库→未入库，仅复原仍处收料态的) ②即时库存减数量 ③单据回 10</summary>
    public override async Task<bool> RejectAsync(string uid, string? reason = null)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能反审核");

        try
        {
            Db.AsTenant().BeginTran();

            // 单据级乐观锁：状态前置回写(仅已审40可反审)，受影响行=0 即并发抢占 → 抛错回滚（防重复冲回）
            var locked = await Db.Updateable<TStkInstock>()
                .SetColumns(h => h.FStatus == 10)
                .SetColumns(h => h.Fcheckerid == string.Empty)
                .SetColumns(h => h.Fcheckdate == new DateTime(1900, 1, 1))
                .SetColumns(h => h.MYmd == DateTime.Now)
                .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
                .Where(h => h.Uid == uid && h.FStatus == 40)
                .ExecuteCommandAsync();
            if (locked == 0) throw new InvalidOperationException("单据状态已变更（可能已被反审核），请刷新后重试");

            await ReverseBarcodesOnRejectAsync(uid);
            var entries = await Db.Queryable<TStkInstockentry>().Where(e => e.FInterId == uid && !e.FDeleted).ToListAsync();
            foreach (var e in entries) await ApplyInventoryAsync(e, header, -1);
            // ④ 回写源单累计入库数量（反审核冲回，与审核对称）
            var srcLines = await Db.Queryable<TStkInstockentry1>().Where(x => x.FInterId == uid && !x.FDeleted).ToListAsync();
            if (srcLines.Count > 0) await UpdateSourceCumulativeFromLinesAsync(srcLines, -1);
            else await UpdateSourceCumulativeAsync(entries, -1);

            Db.AsTenant().CommitTran();
            if (OperationLog != null) await OperationLog.LogAsync(PrgKey, OperationType.Reject, uid, header.Fbillno, reason ?? "反审核冲回", true);
            return true;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    // ===== 入库过账：条码主档翻转 + 即时库存累加/冲回 =====

    /// <summary>回写源单累计入库数量：收料通知单(FINSTOCKQTY/BASE)与采购订单(FINSTOCKQTY/BASE)按源单溯源字段命中并增减。
    /// sign=+1 入库审核累加 / -1 反审核冲回。源单/订单按"单号+行号"稳定键定位（明细行内码会因单据编辑重建而变，见 ID 关联约定）。</summary>
    private async Task UpdateSourceCumulativeAsync(List<TStkInstockentry> entries, int sign)
    {
        foreach (var e in entries)
        {
            if (string.IsNullOrEmpty(e.Fmaterialid) || e.Frealqty == 0) continue;   // 按主数量判零（与库存过账口径一致）
            decimal rawBase = e.FBASEUNITQTY ?? e.Frealqty;
            decimal qty = e.Frealqty * sign;
            decimal baseQty = (rawBase == 0 ? e.Frealqty : rawBase) * sign;          // base 为 0/空 均回落主数量，避免主/基累计分叉
            // 收料通知单：累计入库数量增减
            if (e.Fsrcformid == BillCodeFormKeys.ReceiveBill && !string.IsNullOrEmpty(e.Fsrcbillno) && e.Fsrcentryid > 0)
                await BumpReceiveInstockAsync(e.Fsrcbillno, e.Fsrcentryid, qty, baseQty);
            // 采购订单：累计入库数量增减（直接以采购订单 或 经收料通知单回填的采购订单，均带 FORDER* 溯源）
            if (!string.IsNullOrEmpty(e.FORDERBILLNO) && (e.FORDERENTRYID ?? 0) > 0)
                await BumpPoCumulativeAsync(e.FORDERBILLNO, e.FORDERENTRYID ?? 0, qty, baseQty, 0m, 0m);
        }
    }

    /// <summary>按录入明细 ENTRY1 逐条归集累计入库（每条带各自源单/订单行，正确归属；扫码路径用，避免汇总 ENTRY 多源合并误记）。</summary>
    private async Task UpdateSourceCumulativeFromLinesAsync(List<TStkInstockentry1> lines, int sign)
    {
        foreach (var e in lines)
        {
            if (string.IsNullOrEmpty(e.Fmaterialid) || e.Fqty == 0) continue;
            decimal qty = e.Fqty * sign;
            decimal baseQty = (e.Fbaseunitqty == 0 ? e.Fqty : e.Fbaseunitqty) * sign;
            if (e.Fsrcformid == BillCodeFormKeys.ReceiveBill && !string.IsNullOrEmpty(e.Fsrcbillno) && e.Fsrcentryid > 0)
                await BumpReceiveInstockAsync(e.Fsrcbillno, e.Fsrcentryid, qty, baseQty);
            if (!string.IsNullOrEmpty(e.Forderbillno) && e.Forderentryid > 0)
                await BumpPoCumulativeAsync(e.Forderbillno, e.Forderentryid, qty, baseQty, 0m, 0m);
        }
    }

    /// <summary>收料通知单明细累计入库数量增减（按单号+行号定位源单行）。</summary>
    private async Task BumpReceiveInstockAsync(string billNo, int rowNo, decimal qtyDelta, decimal baseDelta)
    {
        var hid = await Db.Queryable<TPurReceive>().Where(h => h.Fbillno == billNo && !h.FDeleted).Select(h => h.FInterId).FirstAsync();
        if (string.IsNullOrEmpty(hid)) return;
        await Db.Updateable<TPurReceiveEntry>()
            .SetColumns(x => x.Finstockqty == x.Finstockqty + qtyDelta)
            .SetColumns(x => x.Finstockbaseqty == x.Finstockbaseqty + baseDelta)
            .Where(x => x.FInterId == hid && x.FENTRYID == rowNo && !x.FDeleted)
            .ExecuteCommandAsync();
    }

    /// <summary>采购订单明细累计入库/累计退料数量增减（按单号+行号定位源单行）。入库仅 instock 增量、mrb 传 0。</summary>
    private async Task BumpPoCumulativeAsync(string billNo, int rowNo, decimal instockDelta, decimal instockBaseDelta, decimal mrbDelta, decimal mrbBaseDelta)
    {
        var hid = await Db.Queryable<TPurPoOrder>().Where(h => h.Fbillno == billNo && !h.FDeleted).Select(h => h.FInterId).FirstAsync();
        if (string.IsNullOrEmpty(hid)) return;
        await Db.Updateable<TPurPoOrderEntry>()
            .SetColumns(x => x.Finstockqty == x.Finstockqty + instockDelta)
            .SetColumns(x => x.Finstockbaseqty == x.Finstockbaseqty + instockBaseDelta)
            .SetColumns(x => x.Fmrbqty == x.Fmrbqty + mrbDelta)
            .SetColumns(x => x.Fmrbbaseqty == x.Fmrbbaseqty + mrbBaseDelta)
            .Where(x => x.FInterId == hid && x.FENTRYID == rowNo && !x.FDeleted)
            .ExecuteCommandAsync();
    }

    /// <summary>审核时条码主档原子翻转（仅对存在于条码主档的物理单品条码；箱码容器/手工码跳过；并发抢占即抛错）</summary>
    private async Task PostBarcodesOnApproveAsync(string uid)
    {
        var bottoms = await Db.Queryable<TStkInstockentry2>().Where(e => e.FInterId == uid && !e.FDeleted).ToListAsync();
        var codes = bottoms.Select(b => b.Fbarcode).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (codes.Count == 0) return;
        var existing = (await Db.Queryable<TBdBarcoders>().Where(m => codes.Contains(m.Fbarcode) && !m.FDeleted).Select(m => m.Fbarcode).ToListAsync()).ToHashSet();
        var now = DateTime.Now; var user = CurrentUser.UserId ?? string.Empty;
        var done = new HashSet<string>();
        foreach (var b in bottoms)
        {
            var code = b.Fbarcode;
            if (string.IsNullOrEmpty(code) || !existing.Contains(code) || !done.Add(code)) continue;
            var sid = b.Fstockid; var lid = b.Fstocklocid; var ss = b.Fstockstatusid;
            var affected = await Db.Updateable<TBdBarcoders>()
                .SetColumns(m => m.Fbarcodestatus == 2)
                .SetColumns(m => m.Fstockstatus == 1)
                .SetColumns(m => m.FSTOCKID == sid)
                .SetColumns(m => m.FSTOCKLOCID == lid)
                .SetColumns(m => m.FSTOCKSTATUSID == ss)
                .SetColumns(m => m.MUser == user)
                .SetColumns(m => m.MYmd == now)
                .Where(m => m.Fbarcode == code && m.Fbarcodestatus == 1 && !m.FDeleted)
                .ExecuteCommandAsync();
            if (affected == 0)
                throw new InvalidOperationException($"条码 {code} 已被其他单据收料/出库或已入库，无法入库，请核对后重试");
        }
    }

    /// <summary>反审核时条码主档冲回（仅复原仍处"收料(2)"的条码，已被下游消费的不动）</summary>
    private async Task ReverseBarcodesOnRejectAsync(string uid)
    {
        var bottoms = await Db.Queryable<TStkInstockentry2>().Where(e => e.FInterId == uid && !e.FDeleted).ToListAsync();
        var now = DateTime.Now; var user = CurrentUser.UserId ?? string.Empty;
        var done = new HashSet<string>();
        foreach (var b in bottoms)
        {
            var code = b.Fbarcode;
            if (string.IsNullOrEmpty(code) || !done.Add(code)) continue;
            await Db.Updateable<TBdBarcoders>()
                .SetColumns(m => m.Fbarcodestatus == 1)
                .SetColumns(m => m.Fstockstatus == 0)
                .SetColumns(m => m.MUser == user)
                .SetColumns(m => m.MYmd == now)
                .Where(m => m.Fbarcode == code && m.Fbarcodestatus == 2 && !m.FDeleted)
                .ExecuteCommandAsync();
        }
    }

    /// <summary>即时库存累加(sign=+1)/冲回(sign=-1)：按 物料+仓库+仓位+库存组织+辅助属性+库存状态+批次+生产日期+有效期 原子 upsert。
    /// 先带业务键条件自增 UPDATE(数量/基本数量/余额/辅助单位数量对称加减)；冲回带下限防负；未命中且加库存则新建。</summary>
    private async Task ApplyInventoryAsync(TStkInstockentry e, TStkInstock header, int sign)
    {
        if (string.IsNullOrEmpty(e.Fmaterialid) || e.Frealqty == 0) return;
        decimal qty = e.Frealqty * sign;
        decimal baseQty = (e.FBASEUNITQTY ?? e.Frealqty) * sign;
        decimal secQty = (e.FSECUNITQTY ?? 0) * sign;
        decimal dec = -qty;                                   // 冲回时为正
        var org = header.FCompanyId ?? string.Empty;          // 库存组织=收料组织
        // 仓库/仓位/库存状态是单据表头级属性，物料汇总明细行不一定带值 → 优先取明细、回退取表头
        // （修复即时库存 FSTOCKID/FSTOCKLOCID/FSTOCKSTATUSID 写空：过账明细这三列为空，值实际在表头）
        var stockId = !string.IsNullOrEmpty(e.Fstockid) ? e.Fstockid : (header.Fstockid ?? string.Empty);
        var locId = !string.IsNullOrEmpty(e.Fstocklocid) ? e.Fstocklocid : (header.Fstocklocid ?? string.Empty);
        var status = !string.IsNullOrEmpty(e.FSTOCKSTATUSID) ? e.FSTOCKSTATUSID : (header.Fstockstatusid ?? string.Empty);
        var lot = e.Flot ?? string.Empty;
        var aux = e.Fauxpropid ?? string.Empty;
        var kf = e.Fkfdate ?? new DateTime(1900, 1, 1);       // 生产/采购日期纳入维度(FEFO)
        var uf = e.Fusefuldate ?? new DateTime(1900, 1, 1);   // 有效期至纳入维度
        var now = DateTime.Now; var user = CurrentUser.UserId ?? string.Empty;

        // 原子自增 UPDATE（按完整业务键）
        var upd = Db.Updateable<TStkInventory>()
            .SetColumns(x => x.Fqty == x.Fqty + qty)
            .SetColumns(x => x.Fbaseunitqty == x.Fbaseunitqty + baseQty)
            .SetColumns(x => x.Fbal == x.Fbal + qty)
            .SetColumns(x => x.FSECUNITQTY == (x.FSECUNITQTY ?? 0) + secQty)
            .SetColumns(x => x.FUPDATETIME == now)
            .SetColumns(x => x.MUser == user)
            .SetColumns(x => x.MYmd == now)
            .Where(x => x.Fmaterialid == e.Fmaterialid && x.Fstockid == stockId && x.Fstocklocid == locId
                && x.Fstockorgid == org && x.Fauxpropid == aux && x.Fstockstatusid == status && x.FLOT == lot
                && x.Fkfdate == kf && x.Fusefuldate == uf && !x.FDeleted);
        if (sign < 0) upd = upd.Where(x => x.Fqty >= dec && x.Fbal >= dec);   // 冲回不足则不命中
        var hit = await upd.ExecuteCommandAsync();
        if (hit > 0) return;

        if (sign > 0)
        {
            // 首次入库该维度：新建库存行。并发首插撞业务键唯一索引(生产 T_STK_INVENTORY_UK)时，自动改走原子自增 UPDATE 自愈
            var id = Guid.NewGuid().ToString("N");
            try
            {
                await Db.Insertable(new TStkInventory
                {
                    Uid = id, FInterId = id,
                    Fmaterialid = e.Fmaterialid, Fstockid = stockId, Fstocklocid = locId,
                    Fstockorgid = org, Fauxpropid = aux, Fstockstatusid = status, FLOT = lot,
                    Fkeeperid = e.FKEEPERID, Fownerid = e.FOWNERID, Fkeepertypeid = e.FKEEPERTYPEID, Fownertypeid = e.FOWNERTYPEID,
                    Fbaseunitid = e.Fbaseunitid, Fbaseunitqty = baseQty,
                    Fstockunitid = e.Funitid, Fqty = qty, Fbal = qty,
                    Fkfdate = kf, Fusefuldate = uf,
                    FSUPPLYID = header.Fsupplyid, FSECUNITID = e.FSECUNITID ?? string.Empty, FSECUNITQTY = secQty,
                    FCHECKDATE = new DateTime(1900, 1, 1), FDISABLEDATE = new DateTime(1900, 1, 1),
                    FUPDATETIME = now, FISVIRTUAL = false,
                    FStatus = 40, FCompanyId = org,
                    CYmd = now, CUser = user, MYmd = now, MUser = user
                }).ExecuteCommandAsync();
            }
            catch
            {
                // 并发另一事务已建同键行(唯一索引冲突) → 改走原子自增；仍 0 则为其它异常，抛出
                var retry = await Db.Updateable<TStkInventory>()
                    .SetColumns(x => x.Fqty == x.Fqty + qty)
                    .SetColumns(x => x.Fbaseunitqty == x.Fbaseunitqty + baseQty)
                    .SetColumns(x => x.Fbal == x.Fbal + qty)
                    .SetColumns(x => x.FSECUNITQTY == (x.FSECUNITQTY ?? 0) + secQty)
                    .SetColumns(x => x.FUPDATETIME == now)
                    .SetColumns(x => x.MUser == user)
                    .SetColumns(x => x.MYmd == now)
                    .Where(x => x.Fmaterialid == e.Fmaterialid && x.Fstockid == stockId && x.Fstocklocid == locId
                        && x.Fstockorgid == org && x.Fauxpropid == aux && x.Fstockstatusid == status && x.FLOT == lot
                        && x.Fkfdate == kf && x.Fusefuldate == uf && !x.FDeleted)
                    .ExecuteCommandAsync();
                if (retry == 0) throw;
            }
        }
        else
        {
            // 冲回但无匹配库存（不足/已被下游消费）→ 抛错回滚，保证库存非负
            throw new InvalidOperationException("物料即时库存不足，无法冲回入库（本单货物可能已被下游单据消费）");
        }
    }

    /// <summary>条码可入库性判定：仅"初始(1)+未入库(0)"可入库；返回不可入库原因，可入库返回 null</summary>
    private static string? BarcodeUnavailableReason(TBdBarcoders m)
    {
        if (m.Fbarcodestatus == 10) return "条码已废弃";
        if (m.Fstockstatus != 0) return "条码已入库";
        if (m.Fbarcodestatus == 2) return "条码已收料";
        if (m.Fbarcodestatus == 5) return "条码已使用";
        if (m.Fbarcodestatus != 1) return "条码状态不可入库";
        return null;
    }

    public override async Task<bool> CloseAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 70) throw new InvalidOperationException("单据已关闭");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的单据才能关闭");

        var result = await Db.Updateable<TStkInstock>()
            .SetColumns(h => h.FStatus == 70)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Close, uid, header.Fbillno, "关闭单据", result);
        return result;
    }

    protected override Expression<Func<TStkInstock, bool>> BuildSearchPredicate(string keyword)
        => h => h.Fbillno.Contains(keyword) || h.Fsupplyid.Contains(keyword);

    // ===== 一主三从：重写 Create/Update/Delete（单事务级联 ENTRY/ENTRY1/ENTRY2）=====

    public override async Task<InStockDetailDto> CreateAsync(CreateInStockRequest request)
    {
        try
        {
            Db.AsTenant().BeginTran();

            var header = MapToHeaderEntity(request);
            header.Uid = Guid.NewGuid().ToString("N");
            header.FInterId = header.Uid;
            header.CYmd = DateTime.Now;
            header.CUser = CurrentUser.UserId ?? string.Empty;
            header.MYmd = DateTime.Now;
            header.MUser = CurrentUser.UserId ?? string.Empty;
            // 收料组织即 FCompanyId（MapToHeaderEntity 已写入）；为空才回落当前登录组织
            if (string.IsNullOrEmpty(header.FCompanyId))
                header.FCompanyId = CurrentUser.CompanyId ?? string.Empty;

            await PrepareHeaderForCreateAsync(header, request);
            await Db.Insertable(header).ExecuteCommandAsync();

            await PersistEntriesAsync(header, request.Ftypeid, request.Entries, request.BarcodeEntries);

            Db.AsTenant().CommitTran();

            if (OperationLog != null && !string.IsNullOrEmpty(PrgKey))
                await OperationLog.LogAsync(PrgKey, OperationType.Create, header.Uid);

            return (await GetByIdAsync(header.Uid))!;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    public override async Task<bool> UpdateAsync(string uid, UpdateInStockRequest request)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的单据不能修改，请先反审核");
        if (header.FStatus == 70) throw new InvalidOperationException("已关闭的单据不能修改");

        try
        {
            Db.AsTenant().BeginTran();

            UpdateHeaderEntity(header, request);
            header.MYmd = DateTime.Now;
            header.MUser = CurrentUser.UserId ?? string.Empty;
            await Db.Updateable(header).IgnoreColumns(e => new { e.CYmd, e.CUser }).ExecuteCommandAsync();

            // 物理删旧的三套明细
            await Db.Deleteable<TStkInstockentry>().Where(e => e.FInterId == uid).ExecuteCommandAsync();
            await Db.Deleteable<TStkInstockentry1>().Where(e => e.FInterId == uid).ExecuteCommandAsync();
            await Db.Deleteable<TStkInstockentry2>().Where(e => e.FInterId == uid).ExecuteCommandAsync();

            await PersistEntriesAsync(header, request.Ftypeid, request.Entries, request.BarcodeEntries);

            Db.AsTenant().CommitTran();

            if (OperationLog != null && !string.IsNullOrEmpty(PrgKey))
                await OperationLog.LogAsync(PrgKey, OperationType.Update, uid);

            return true;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    public override async Task<bool> DeleteAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("单据不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的单据不能删除，请先反审核");
        if (header.FStatus == 70) throw new InvalidOperationException("已关闭的单据不能删除");

        try
        {
            Db.AsTenant().BeginTran();

            var result = await HeaderRepo.SoftDeleteAsync(uid);
            if (result)
            {
                // 级联软删三套明细，避免按明细行展开列表出现孤儿行
                await Db.Updateable<TStkInstockentry>()
                    .SetColumns(e => e.FDeleted == true).SetColumns(e => e.MYmd == DateTime.Now)
                    .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
                    .Where(e => e.FInterId == uid).ExecuteCommandAsync();
                await Db.Updateable<TStkInstockentry1>()
                    .SetColumns(e => e.FDeleted == true).SetColumns(e => e.MYmd == DateTime.Now)
                    .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
                    .Where(e => e.FInterId == uid).ExecuteCommandAsync();
                await Db.Updateable<TStkInstockentry2>()
                    .SetColumns(e => e.FDeleted == true).SetColumns(e => e.MYmd == DateTime.Now)
                    .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
                    .Where(e => e.FInterId == uid).ExecuteCommandAsync();
            }

            Db.AsTenant().CommitTran();

            if (result && OperationLog != null && !string.IsNullOrEmpty(PrgKey))
                await OperationLog.LogAsync(PrgKey, OperationType.Delete, uid);
            return result;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    /// <summary>持久化三套明细（事务内调用）。录入类型=条码 时按 ENTRY1 聚合出 ENTRY 并回填 FBODYID。</summary>
    private async Task PersistEntriesAsync(TStkInstock header, int ftypeid,
        List<CreateInStockMaterialEntryRequest>? materialReqs, List<CreateInStockBarcodeEntryRequest>? barcodeReqs)
    {
        var headerUid = header.Uid;
        var now = DateTime.Now;
        var user = CurrentUser.UserId ?? string.Empty;
        var company = CurrentUser.CompanyId ?? string.Empty;
        // 仓库/仓位/库存状态录在单据表头，须下沉到三张明细（ENTRY/ENTRY1/ENTRY2）——
        // 库存按物料汇总 ENTRY 过账，明细这三列空则库存也写空。明细自带值时优先保留，仅空缺时继承表头。
        var headerStockId = header.Fstockid ?? string.Empty;
        var headerLocId = header.Fstocklocid ?? string.Empty;
        var headerStatusId = header.Fstockstatusid ?? string.Empty;

        if (ftypeid == 2)
        {
            // 1) 录入明细 ENTRY1 + 底阶条码 ENTRY2（箱码重查装箱清单展开；非箱码镜像该条码）
            var entry1List = new List<TStkInstockentry1>();
            var entry2List = new List<TStkInstockentry2>();
            var seenCodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);   // 单内条码去重：防同码重复录入致库存翻倍(与条码主档翻转口径一致)
            int e1idx = 1, e2idx = 1;
            foreach (var r in barcodeReqs ?? new())
            {
                if (string.IsNullOrEmpty(r.Fbarcode) && string.IsNullOrEmpty(r.Fboxbarcode)) continue;
                var dedupKey = (r.Fisbox && !string.IsNullOrEmpty(r.Fboxbarcode)) ? r.Fboxbarcode : r.Fbarcode;
                if (!seenCodes.Add(dedupKey))
                    throw new InvalidOperationException($"条码 {dedupKey} 在本单重复录入，请勿重复扫描");
                var e1 = BuildEntry1(r);
                Stamp(e1, headerUid, now, user, company);
                // 明细未带仓库/仓位/库存状态时继承表头（先于 ENTRY2/汇总 ENTRY 派生，确保级联下沉、过账库存有值）
                if (string.IsNullOrEmpty(e1.Fstockid)) e1.Fstockid = headerStockId;
                // 仓位仅在行仓库与表头仓库一致时才继承表头仓位，避免行改成别的仓库却串入属于表头仓库的仓位
                if (string.IsNullOrEmpty(e1.Fstocklocid) && e1.Fstockid == headerStockId) e1.Fstocklocid = headerLocId;
                if (string.IsNullOrEmpty(e1.Fstockstatusid)) e1.Fstockstatusid = headerStatusId;
                e1.Fentryid = e1idx++;
                e1.Fdetailid = e1.Uid;
                entry1List.Add(e1);

                if (e1.Fisbox && !string.IsNullOrEmpty(e1.Fboxbarcode))
                {
                    var childCodes = await Db.Queryable<TBdBarcodersentry>()
                        .Where(x => x.Fboxcode == e1.Fboxbarcode && !x.FDeleted).Select(x => x.Fbarcode).ToListAsync();
                    var childMasters = childCodes.Count > 0
                        ? await Db.Queryable<TBdBarcoders>().Where(m => childCodes.Contains(m.Fbarcode) && !m.FDeleted).ToListAsync()
                        : new List<TBdBarcoders>();
                    if (childMasters.Count > 0)
                    {
                        // 混装箱（子条码物料不唯一）：按箱主档单一物料聚合会与子条码实际物料不符，暂不支持
                        var distinctMat = childMasters.Select(c => c.Fmaterialid).Where(s => !string.IsNullOrEmpty(s)).Distinct().Count();
                        if (distinctMat > 1)
                            throw new InvalidOperationException($"箱码 {e1.Fboxbarcode} 为混装箱（含多种物料），暂不支持混装箱入库，请拆分后逐条录入");
                        decimal boxSum = 0;
                        foreach (var cm in childMasters)
                        {
                            var e2 = BuildEntry2FromMaster(cm, e1);
                            Stamp(e2, headerUid, now, user, company);
                            e2.Fentryid = e2idx++;
                            e2.Fdetailid = e2.Uid;
                            entry2List.Add(e2);
                            boxSum += e2.Fqty;
                        }
                        // 箱录入行数量统一为子条码数量之和，保证『物料汇总实收 = Σ底阶条码数量』恒等（不信任前端传值）
                        e1.Fqty = boxSum;
                        e1.Fbaseunitqty = boxSum;
                        // 箱主档物料为空（箱壳）时承袭子条码物料，确保按物料聚合正确
                        if (string.IsNullOrEmpty(e1.Fmaterialid)) e1.Fmaterialid = childMasters[0].Fmaterialid;
                    }
                    else
                    {
                        // 空箱（无装箱清单）：底阶镜像箱码自身，保证 ENTRY2 非空且与物料汇总数量恒等、底阶可反查
                        var e2 = BuildEntry2FromBarcodeLine(e1);
                        e2.Fboxbarcode = e1.Fboxbarcode;
                        e2.Fbarcode = string.IsNullOrEmpty(e1.Fbarcode) ? e1.Fboxbarcode : e1.Fbarcode;
                        Stamp(e2, headerUid, now, user, company);
                        e2.Fentryid = e2idx++;
                        e2.Fdetailid = e2.Uid;
                        entry2List.Add(e2);
                    }
                }
                else
                {
                    var e2 = BuildEntry2FromBarcodeLine(e1);
                    Stamp(e2, headerUid, now, user, company);
                    e2.Fentryid = e2idx++;
                    e2.Fdetailid = e2.Uid;
                    entry2List.Add(e2);
                }
            }

            // 源单溯源：按条码档批量权威回填 ENTRY1/ENTRY2 的源单/订单字段（标签打印时写入；箱码取首个子条码）
            var srcCodes = entry1List.Select(e => string.IsNullOrEmpty(e.Fboxbarcode) ? e.Fbarcode : e.Fboxbarcode)
                .Concat(entry2List.Select(e => e.Fbarcode));
            var srcDict = await LoadBarcodeSourceDictAsync(srcCodes);
            foreach (var e1 in entry1List)
            {
                var key = string.IsNullOrEmpty(e1.Fboxbarcode) ? e1.Fbarcode : e1.Fboxbarcode;
                if (srcDict.TryGetValue(key, out var l)) ApplySourceToEntry1Entity(e1, l);
                else if (e1.Fisbox)
                {
                    var child = entry2List.FirstOrDefault(x => x.Fboxbarcode == e1.Fboxbarcode && !string.IsNullOrEmpty(x.Fbarcode));
                    if (child != null && srcDict.TryGetValue(child.Fbarcode, out var lc)) ApplySourceToEntry1Entity(e1, lc);
                }
            }
            foreach (var e2 in entry2List)
                if (srcDict.TryGetValue(e2.Fbarcode, out var l)) ApplySourceToEntry2Entity(e2, l);

            // 货主/保管者：从条码主档 T_BD_BARCODERS 按条码回填到 ENTRY1（明细未带值时），再经物料聚合带到汇总 ENTRY 与即时库存
            var koCodes = entry1List.SelectMany(e => new[] { e.Fbarcode, e.Fboxbarcode })
                .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            if (koCodes.Count > 0)
            {
                var koDict = (await Db.Queryable<TBdBarcoders>()
                        .Where(m => koCodes.Contains(m.Fbarcode) && !m.FDeleted)
                        .Select(m => new { m.Fbarcode, m.FKEEPERID, m.FOWNERID, m.FKEEPERTYPEID, m.FOWNERTYPEID }).ToListAsync())
                    .GroupBy(r => r.Fbarcode).ToDictionary(g => g.Key, g => g.First());
                foreach (var e1 in entry1List)
                {
                    var ko = (!string.IsNullOrEmpty(e1.Fbarcode) && koDict.TryGetValue(e1.Fbarcode, out var k1)) ? k1
                           : (!string.IsNullOrEmpty(e1.Fboxbarcode) && koDict.TryGetValue(e1.Fboxbarcode, out var k2)) ? k2 : null;
                    if (ko == null) continue;
                    if (string.IsNullOrEmpty(e1.Fkeeperid)) e1.Fkeeperid = ko.FKEEPERID;
                    if (string.IsNullOrEmpty(e1.Fownerid)) e1.Fownerid = ko.FOWNERID;
                    if (string.IsNullOrEmpty(e1.Fkeepertypeid)) e1.Fkeepertypeid = ko.FKEEPERTYPEID;
                    if (string.IsNullOrEmpty(e1.Fownertypeid)) e1.Fownertypeid = ko.FOWNERTYPEID;
                }
            }

            // 2) 按物料维度聚合出物料汇总 ENTRY，实收=该组 Σ条码数量，并回填录入明细的父阶表体内码
            var materialList = new List<TStkInstockentry>();
            int mIdx = 1;
            foreach (var grp in entry1List.GroupBy(e => AggKey(e.Fmaterialid, e.Flot, e.Fstockid, e.Fstocklocid, e.Fstockstatusid, e.Fauxpropid, e.Funitid)))
            {
                var first = grp.First();
                decimal qty = grp.Sum(x => x.Fqty);
                decimal secQty = grp.Sum(x => x.Fsecunitqty);
                var me = BuildMaterialFromBarcode(first, qty, secQty);
                Stamp(me, headerUid, now, user, company);
                me.FENTRYID = mIdx++;
                me.FDETAILID = me.Uid;
                foreach (var e1 in grp) e1.Fbodyid = me.FDETAILID;
                materialList.Add(me);
            }

            if (materialList.Count > 0) await Db.Insertable(materialList).ExecuteCommandAsync();
            if (entry1List.Count > 0) await Db.Insertable(entry1List).ExecuteCommandAsync();
            if (entry2List.Count > 0) await Db.Insertable(entry2List).ExecuteCommandAsync();
        }
        else
        {
            // 录入类型=物料：直接写物料汇总 ENTRY
            var materialList = new List<TStkInstockentry>();
            int mIdx = 1;
            foreach (var r in materialReqs ?? new())
            {
                if (string.IsNullOrEmpty(r.Fmaterialid)) continue;
                var me = BuildMaterialFromReq(r);
                // 物料录入行未带仓库/仓位/库存状态时继承表头
                if (string.IsNullOrEmpty(me.Fstockid)) me.Fstockid = headerStockId;
                // 仓位仅在行仓库与表头仓库一致时才继承表头仓位，避免行改成别的仓库却串入属于表头仓库的仓位
                if (string.IsNullOrEmpty(me.Fstocklocid) && me.Fstockid == headerStockId) me.Fstocklocid = headerLocId;
                if (string.IsNullOrEmpty(me.FSTOCKSTATUSID)) me.FSTOCKSTATUSID = headerStatusId;
                Stamp(me, headerUid, now, user, company);
                me.FENTRYID = mIdx++;
                me.FDETAILID = me.Uid;
                materialList.Add(me);
            }
            if (materialList.Count > 0) await Db.Insertable(materialList).ExecuteCommandAsync();
        }
    }

    private static void Stamp(BaseEntity e, string headerUid, DateTime now, string user, string company)
    {
        e.Uid = Guid.NewGuid().ToString("N");
        e.FInterId = headerUid;
        e.CYmd = now; e.CUser = user; e.MYmd = now; e.MUser = user;
        e.FCompanyId = company;
    }

    private static string AggKey(string mat, string lot, string stock, string loc, string status, string aux, string unit)
        => string.Join("|", mat ?? "", lot ?? "", stock ?? "", loc ?? "", status ?? "", aux ?? "", unit ?? "");

    // ===== 实体构造 =====

    private static TStkInstockentry1 BuildEntry1(CreateInStockBarcodeEntryRequest r) => new()
    {
        Ftypeid = 2,
        Fisbox = r.Fisbox,
        Fboxbarcode = r.Fboxbarcode,
        Fbarcode = r.Fbarcode,
        Fbartype = r.Fbartype,
        Fmaterialid = r.Fmaterialid,
        Fauxpropid = r.Fauxpropid,
        Flot = r.Flot,
        Fkfdate = r.Fkfdate ?? new DateTime(1900, 1, 1),
        Fusefuldate = r.Fusefuldate ?? new DateTime(1900, 1, 1),
        Fqty = r.Fqty,
        Fstockid = r.Fstockid,
        Fstocklocid = r.Fstocklocid,
        Fsupplyid = r.Fsupplyid,
        Funitid = r.Funitid,
        Fbaseunitid = string.IsNullOrEmpty(r.Fbaseunitid) ? r.Funitid : r.Fbaseunitid,
        Fbaseunitqty = r.Fbaseunitqty == 0 ? r.Fqty : r.Fbaseunitqty,
        Fsecunitid = r.Fsecunitid,
        Fsecunitqty = r.Fsecunitqty,
        Fstockstatusid = r.Fstockstatusid,
        Fwwintype = r.Fwwintype,
        Ftaxprice = r.Ftaxprice,
        Ftaxrate = r.Ftaxrate,
        Fdiscountrate = r.Fdiscountrate,
        Fsrcformid = r.Fsrcformid,
        Fsrcbillno = r.Fsrcbillno,
        Fsrcdetailid = r.Fsrcdetailid,
        Fsrcentryid = r.Fsrcentryid,
        Fordertypeid = r.Fordertypeid,
        Forderinterid = r.Forderinterid,
        Forderbillno = r.Forderbillno,
        Forderdetailid = r.Forderdetailid,
        Forderentryid = r.Forderentryid
    };

    /// <summary>箱内子条码：物料/批次/数量取条码主档，仓库/仓位/库存状态等承袭该箱录入行上下文</summary>
    private static TStkInstockentry2 BuildEntry2FromMaster(TBdBarcoders m, TStkInstockentry1 ctx) => new()
    {
        Fboxbarcode = ctx.Fboxbarcode,
        Fbarcode = m.Fbarcode,
        Fmaterialid = m.Fmaterialid,
        Fauxpropid = string.IsNullOrEmpty(m.FAUXPROPID) ? ctx.Fauxpropid : m.FAUXPROPID,
        Flot = m.Flot,
        Fkfdate = m.FKFDATE ?? new DateTime(1900, 1, 1),
        Fusefuldate = m.FUSEFULDATE ?? new DateTime(1900, 1, 1),
        Fqty = m.FQTY ?? (m.Fbartype == 1 ? 1m : 0m),
        Fstockid = ctx.Fstockid,
        Fstocklocid = ctx.Fstocklocid,
        Fstockstatusid = ctx.Fstockstatusid,
        Fsupplyid = string.IsNullOrEmpty(m.FSUPPLYID) ? ctx.Fsupplyid : m.FSUPPLYID,
        Funitid = string.IsNullOrEmpty(m.FUNITID) ? ctx.Funitid : m.FUNITID,
        Fbaseunitid = string.IsNullOrEmpty(m.FBASEUNITID) ? ctx.Fbaseunitid : m.FBASEUNITID,
        Fbaseunitqty = m.FQTY ?? (m.Fbartype == 1 ? 1m : 0m),   // 与 Fqty 同口径回落，1:1 无换算下二者相等
        Ftaxprice = ctx.Ftaxprice,
        Ftaxrate = ctx.Ftaxrate,
        Fdiscountrate = ctx.Fdiscountrate
    };

    /// <summary>非箱码：底阶条码即录入行本身的镜像</summary>
    private static TStkInstockentry2 BuildEntry2FromBarcodeLine(TStkInstockentry1 e) => new()
    {
        Fboxbarcode = string.Empty,
        Fbarcode = e.Fbarcode,
        Fmaterialid = e.Fmaterialid,
        Fauxpropid = e.Fauxpropid,
        Flot = e.Flot,
        Fkfdate = e.Fkfdate ?? new DateTime(1900, 1, 1),
        Fusefuldate = e.Fusefuldate ?? new DateTime(1900, 1, 1),
        Fqty = e.Fqty,
        Fstockid = e.Fstockid,
        Fstocklocid = e.Fstocklocid,
        Fstockstatusid = e.Fstockstatusid,
        Fsupplyid = e.Fsupplyid,
        Funitid = e.Funitid,
        Fbaseunitid = e.Fbaseunitid,
        Fbaseunitqty = e.Fbaseunitqty,
        Ftaxprice = e.Ftaxprice,
        Ftaxrate = e.Ftaxrate,
        Fdiscountrate = e.Fdiscountrate,
        Fsrcformid = e.Fsrcformid,
        Fsrcbillno = e.Fsrcbillno,
        Fsrcentryid = e.Fsrcentryid,
        Fsrcdetailid = e.Fsrcdetailid,
        Fordertypeid = e.Fordertypeid,
        Forderinterid = e.Forderinterid,
        Forderbillno = e.Forderbillno,
        Forderdetailid = e.Forderdetailid,
        Forderentryid = e.Forderentryid
    };

    private static TStkInstockentry BuildMaterialFromReq(CreateInStockMaterialEntryRequest r)
    {
        decimal qty = r.Frealqty;
        decimal price = r.Fprice;
        decimal taxRate = r.Ftaxrate;
        decimal disc = r.Fdiscountrate;
        decimal amount = Math.Round(qty * price * (1 - disc / 100m), 2);
        decimal taxAmount = Math.Round(amount * taxRate / 100m, 2);
        decimal taxPrice = Math.Round(price * (1 + taxRate / 100m), 6);
        decimal discount = Math.Round(qty * price * disc / 100m, 2);
        return new TStkInstockentry
        {
            Frowtype = string.Empty,
            Fwwintype = r.Fwwintype,
            Fmaterialid = r.Fmaterialid,
            Fsrcformid = r.Fsrcformid,
            Fsrcbillno = r.Fsrcbillno,
            Fsrcentryid = r.Fsrcentryid,
            Fmustqty = r.Fmustqty == 0 ? qty : r.Fmustqty,
            Frealqty = qty,
            Fprice = price,
            Fstockid = r.Fstockid,
            Fstocklocid = r.Fstocklocid,
            Fauxpropid = r.Fauxpropid,
            Flot = r.Flot,
            Fkfdate = r.Fkfdate ?? new DateTime(1900, 1, 1),
            Fusefuldate = r.Fusefuldate ?? new DateTime(1900, 1, 1),
            // 基本单位/数量：未接入换算率，1:1 回落（与收料通知单一致）
            Fbaseunitid = string.IsNullOrEmpty(r.Fbaseunitid) ? r.Funitid : r.Fbaseunitid,
            Funitid = r.Funitid,
            Fdiscountrate = disc,
            Fdiscount = discount,
            Ftaxrate = taxRate,
            Ftaxprice = taxPrice,
            Famount = amount,
            FamountLc = amount,
            // 生产 NOT NULL 但实体可空的列须显式赋值（SqlSugar 写 NULL 不触发 DB DEFAULT）
            FTAXAMOUNT = taxAmount,
            FTAXAMOUNT_LC = taxAmount,
            FALLAMOUNT = amount + taxAmount,
            FALLAMOUNT_LC = amount + taxAmount,
            FBASEUNITQTY = r.Fbaseunitqty == 0 ? qty : r.Fbaseunitqty,
            FSECUNITID = r.Fsecunitid,
            FSECUNITQTY = r.Fsecunitqty,
            FSTOCKSTATUSID = r.Fstockstatusid,
            FGIVEAWAY = false,
            FORDERBILLNO = r.Forderbillno,
            FORDERENTRYID = r.Forderentryid,
            FORDERINTERID = r.Forderinterid,
            FORDERDETAILID = r.Forderdetailid
        };
    }

    private static TStkInstockentry BuildMaterialFromBarcode(TStkInstockentry1 first, decimal qty, decimal secQty)
    {
        decimal taxPrice = first.Ftaxprice;
        decimal taxRate = first.Ftaxrate;
        decimal disc = first.Fdiscountrate;
        decimal price = taxRate != 0 ? Math.Round(taxPrice / (1 + taxRate / 100m), 6) : taxPrice;
        decimal amount = Math.Round(qty * price * (1 - disc / 100m), 2);
        decimal taxAmount = Math.Round(amount * taxRate / 100m, 2);
        decimal discount = Math.Round(qty * price * disc / 100m, 2);
        return new TStkInstockentry
        {
            Frowtype = string.Empty,
            Fwwintype = first.Fwwintype,
            Fmaterialid = first.Fmaterialid,
            Fsrcformid = first.Fsrcformid,
            Fsrcbillno = first.Fsrcbillno,
            Fsrcentryid = first.Fsrcentryid,
            Fsrcdetailid = first.Fsrcdetailid,
            Fmustqty = qty,
            Frealqty = qty,
            Fprice = price,
            Fstockid = first.Fstockid,
            Fstocklocid = first.Fstocklocid,
            FKEEPERID = first.Fkeeperid,
            FOWNERID = first.Fownerid,
            FKEEPERTYPEID = first.Fkeepertypeid,
            FOWNERTYPEID = first.Fownertypeid,
            Fauxpropid = first.Fauxpropid,
            Flot = first.Flot,
            Fkfdate = first.Fkfdate ?? new DateTime(1900, 1, 1),
            Fusefuldate = first.Fusefuldate ?? new DateTime(1900, 1, 1),
            Fsupplyid = first.Fsupplyid,
            Fbaseunitid = first.Fbaseunitid,
            Funitid = first.Funitid,
            Fdiscountrate = disc,
            Fdiscount = discount,
            Ftaxrate = taxRate,
            Ftaxprice = taxPrice,
            Famount = amount,
            FamountLc = amount,
            FTAXAMOUNT = taxAmount,
            FTAXAMOUNT_LC = taxAmount,
            FALLAMOUNT = amount + taxAmount,
            FALLAMOUNT_LC = amount + taxAmount,
            FBASEUNITQTY = qty,
            FSECUNITID = first.Fsecunitid,
            FSECUNITQTY = secQty,
            FSTOCKSTATUSID = first.Fstockstatusid,
            FGIVEAWAY = false,
            FORDERTYPEID = first.Fordertypeid,
            FORDERBILLNO = first.Forderbillno,
            FORDERENTRYID = first.Forderentryid,
            FORDERINTERID = first.Forderinterid,
            FORDERDETAILID = first.Forderdetailid
        };
    }

    // ===== 列表：按物料汇总明细 ENTRY 行展开 + 名称解析 =====

    private static readonly HashSet<string> HeaderFilterFields = new(StringComparer.OrdinalIgnoreCase) { "fbillno", "fdate", "fStatus" };

    public override async Task<PagedResult<InStockListDto>> GetPagedListAsync(PagedRequest request)
    {
        var filters = request.DynamicFilters ?? new List<DynamicFilterInfo>();
        var headerFilters = filters.Where(f => HeaderFilterFields.Contains(f.Field)).ToList();
        var entryFilters = filters.Where(f => !HeaderFilterFields.Contains(f.Field)).ToList();

        List<string>? headerIds = null;
        if (!string.IsNullOrWhiteSpace(request.Keyword) || headerFilters.Count > 0)
        {
            var hq = Db.Queryable<TStkInstock>().Where(h => !h.FDeleted);
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                var kw = request.Keyword.Trim();
                hq = hq.Where(h => h.Fbillno.Contains(kw));
            }
            if (headerFilters.Count > 0)
                hq = hq.Where(headerFilters.ToConditionalModels<TStkInstock>());
            headerIds = await hq.Select(h => h.Uid).ToListAsync();
            if (headerIds.Count == 0)
                return new PagedResult<InStockListDto> { Items = new(), TotalCount = 0, PageIndex = request.PageIndex, PageSize = request.PageSize };
        }

        RefAsync<int> totalCount = 0;
        var query = Db.Queryable<TStkInstockentry>().Where(e => !e.FDeleted);
        if (headerIds != null)
            query = query.Where(e => headerIds.Contains(e.FInterId));
        if (entryFilters.Count > 0)
            query = query.Where(entryFilters.ToConditionalModels<TStkInstockentry>());
        var entries = await query
            .OrderBy(e => e.CYmd, OrderByType.Desc)
            .OrderBy(e => e.FENTRYID)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        if (entries.Count == 0)
            return new PagedResult<InStockListDto> { Items = new(), TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };

        var hids = entries.Select(e => e.FInterId).Distinct().ToList();
        var headers = await Db.Queryable<TStkInstock>().Where(h => hids.Contains(h.Uid)).ToListAsync();
        var headerDict = headers.GroupBy(h => h.Uid).ToDictionary(g => g.Key, g => g.First());

        var materialDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
        var unitDict = await LoadUnitDictAsync(entries.Select(e => e.Funitid).Concat(entries.Select(e => e.Fbaseunitid)));
        var supplierDict = await LoadSupplierDictAsync(headers.Select(h => h.Fsupplyid));
        var deptDict = await LoadDepartmentDictAsync(headers.Select(h => h.Fmrdeptid));
        var empDict = await LoadEmployeeNameDictAsync(headers.Select(h => h.Fempid));
        var orgDict = await LoadOrgDictAsync(headers.Select(h => h.FCompanyId));
        var billTypeDict = await LoadBillTypeDictAsync(headers.Select(h => h.Fbilltypeid));
        var statusDict = await LoadStatusDictAsync();
        var userDict = await LoadUserNameDictAsync(headers.Select(h => h.CUser).Concat(headers.Select(h => h.Fcheckerid)));

        var items = entries.Select(e =>
        {
            headerDict.TryGetValue(e.FInterId, out var h);
            materialDict.TryGetValue(e.Fmaterialid ?? string.Empty, out var mat);
            unitDict.TryGetValue(e.Funitid ?? string.Empty, out var unit);
            unitDict.TryGetValue(e.Fbaseunitid ?? string.Empty, out var baseUnit);
            return new InStockListDto
            {
                Uid = h?.Uid ?? e.FInterId,
                EntryUid = e.Uid,
                Fbillno = h?.Fbillno ?? string.Empty,
                Fdate = h?.Fdate,
                FbilltypeName = h != null ? billTypeDict.GetValueOrDefault(h.Fbilltypeid, string.Empty) : string.Empty,
                Ftypeid = h?.Ftypeid ?? 0,
                FtypeName = (h?.Ftypeid ?? 0) == 2 ? "条码" : "物料",
                Fentryid = e.FENTRYID,
                FStatus = h?.FStatus ?? 0,
                FstatusName = statusDict.GetValueOrDefault(h?.FStatus ?? 0, string.Empty),
                FDisabled = h?.FDisabled ?? false,
                Fsupplyid = h?.Fsupplyid ?? string.Empty,
                FsupplyNumber = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Number : string.Empty,
                FsupplyName = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Name : string.Empty,
                FmrdeptName = h != null ? deptDict.GetValueOrDefault(h.Fmrdeptid, default).Name : string.Empty,
                FempName = h != null ? empDict.GetValueOrDefault(h.Fempid, string.Empty) : string.Empty,
                Forderbillno = e.FORDERBILLNO,
                Fmaterialid = e.Fmaterialid,
                FmaterialNumber = mat.Number,
                FmaterialName = mat.Name,
                FSpecification = mat.Spec,
                Flot = e.Flot,
                Frealqty = e.Frealqty,
                Fmustqty = e.Fmustqty,
                FunitName = unit.Name,
                FbaseunitName = baseUnit.Name,
                Fkfdate = e.Fkfdate,
                Ferpno = h?.Ferpno ?? string.Empty,
                FcompanyName = h != null ? orgDict.GetValueOrDefault(h.FCompanyId, default).Name : string.Empty,
                CuserName = h != null ? userDict.GetValueOrDefault(h.CUser, string.Empty) : string.Empty,
                CYmd = h?.CYmd,
                FcheckerName = h != null ? userDict.GetValueOrDefault(h.Fcheckerid, string.Empty) : string.Empty,
                Fcheckdate = h?.Fcheckdate,
                Fwwintype = e.Fwwintype
            };
        }).ToList();

        return new PagedResult<InStockListDto> { Items = items, TotalCount = totalCount, PageIndex = request.PageIndex, PageSize = request.PageSize };
    }

    protected override InStockListDto MapToListDto(TStkInstock entity) => new()
    {
        Uid = entity.Uid,
        Fbillno = entity.Fbillno,
        Fdate = entity.Fdate,
        Ftypeid = entity.Ftypeid,
        FStatus = entity.FStatus
    };

    // ===== 详情：主表 + 名称解析 + 三套明细 =====

    public override async Task<InStockDetailDto?> GetByIdAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return null;

        var materials = await GetEntriesByHeaderIdAsync(uid);
        var barcodes = await Db.Queryable<TStkInstockentry1>().Where(e => e.FInterId == uid && !e.FDeleted).OrderBy(e => e.Fentryid).ToListAsync();
        var bottoms = await Db.Queryable<TStkInstockentry2>().Where(e => e.FInterId == uid && !e.FDeleted).OrderBy(e => e.Fentryid).ToListAsync();

        var dto = MapToDetailDto(header, materials);
        dto.BarcodeEntries = barcodes.Select(MapBarcodeEntry).ToList();
        dto.BottomEntries = bottoms.Select(MapBottomEntry).ToList();

        // ---- 主表名称 ----
        dto.FbilltypeName = await Db.Queryable<TBasBilltype>().Where(b => b.Uid == header.Fbilltypeid).Select(b => b.Fname).FirstAsync() ?? string.Empty;
        dto.FsrcformName = string.IsNullOrEmpty(header.Fsrcformid) ? "无源单"
            : (await Db.Queryable<SysBillTemplate>().Where(t => t.Fnumber == header.Fsrcformid).Select(t => t.Fname).FirstAsync() ?? header.Fsrcformid);
        var supplier = await LoadSupplierDictAsync(new[] { header.Fsupplyid });
        if (supplier.TryGetValue(header.Fsupplyid, out var sp)) { dto.FsupplyNumber = sp.Number; dto.FsupplyName = sp.Name; }
        var empDict = await LoadEmployeeNameDictAsync(new[] { header.Fpurchaserid, header.Fstockerid, header.Fempid });
        dto.FpurchaserName = empDict.GetValueOrDefault(header.Fpurchaserid, string.Empty);
        dto.FstockerName = empDict.GetValueOrDefault(header.Fstockerid, string.Empty);
        dto.FempName = empDict.GetValueOrDefault(header.Fempid, string.Empty);
        var deptDict = await LoadDepartmentDictAsync(new[] { header.Fmrdeptid, header.Fpurchasedeptid });
        dto.FmrdeptName = deptDict.GetValueOrDefault(header.Fmrdeptid, default).Name;
        dto.FpurchasedeptName = deptDict.GetValueOrDefault(header.Fpurchasedeptid, default).Name;
        var orgDict = await LoadOrgDictAsync(new[] { header.Fdemandorgid, header.Fpurchaseorgid, header.FCompanyId });
        dto.FdemandorgName = orgDict.GetValueOrDefault(header.Fdemandorgid, default).Name;
        dto.FpurchaseorgName = orgDict.GetValueOrDefault(header.Fpurchaseorgid, default).Name;
        dto.FcompanyName = orgDict.GetValueOrDefault(header.FCompanyId, default).Name;
        var currency = await Db.Queryable<TBdCurrency>().Where(c => c.Uid == header.Fcurrencyid).Select(c => new { c.FNumber, c.FName }).FirstAsync();
        if (currency != null) { dto.FcurrencyNumber = currency.FNumber; dto.FcurrencyName = currency.FName; }
        dto.FstatusName = (await LoadStatusDictAsync()).GetValueOrDefault(header.FStatus, string.Empty);

        // 表头仓库/仓位/库存状态名称
        var stockDict = await LoadStockDictAsync(new[] { header.Fstockid });
        if (stockDict.TryGetValue(header.Fstockid, out var hs)) { dto.FstockNumber = hs.Number; dto.FstockName = hs.Name; dto.FisOpenLocation = hs.OpenLoc; }
        dto.FstocklocName = (await LoadStockLocDictAsync(new[] { header.Fstocklocid })).GetValueOrDefault(header.Fstocklocid, string.Empty);
        var stockStatusDict = await LoadStockStatusDictAsync(new[] { header.Fstockstatusid });
        dto.FstockstatusName = stockStatusDict.GetValueOrDefault(header.Fstockstatusid, string.Empty);

        // 制单/审核/修改/禁用人名
        var userDict = await LoadUserNameDictAsync(new[] { header.CUser, header.MUser, header.Fcheckerid, header.Fdisableid });
        dto.CuserName = userDict.GetValueOrDefault(header.CUser, string.Empty);
        dto.MuserName = userDict.GetValueOrDefault(header.MUser, string.Empty);
        dto.FcheckerName = userDict.GetValueOrDefault(header.Fcheckerid, string.Empty);
        dto.FdisableName = userDict.GetValueOrDefault(header.Fdisableid, string.Empty);

        // ---- 明细名称（物料汇总 + 录入明细）----
        await ResolveEntryNamesAsync(dto);

        return dto;
    }

    private async Task ResolveEntryNamesAsync(InStockDetailDto dto)
    {
        var matIds = dto.Entries.Select(e => e.Fmaterialid)
            .Concat(dto.BarcodeEntries.Select(e => e.Fmaterialid))
            .Concat(dto.BottomEntries.Select(e => e.Fmaterialid));
        var unitIds = dto.Entries.Select(e => e.Funitid).Concat(dto.BarcodeEntries.Select(e => e.Funitid)).Concat(dto.BarcodeEntries.Select(e => e.Fsecunitid));
        var stockIds = dto.Entries.Select(e => e.Fstockid).Concat(dto.BarcodeEntries.Select(e => e.Fstockid));
        var locIds = dto.Entries.Select(e => e.Fstocklocid).Concat(dto.BarcodeEntries.Select(e => e.Fstocklocid));
        var auxIds = dto.Entries.Select(e => e.Fauxpropid).Concat(dto.BarcodeEntries.Select(e => e.Fauxpropid));
        var statusIds = dto.Entries.Select(e => e.Fstockstatusid).Concat(dto.BarcodeEntries.Select(e => e.Fstockstatusid));

        var matDict = await LoadMaterialDictAsync(matIds);
        var unitDict = await LoadUnitDictAsync(unitIds);
        var stockDict = await LoadStockDictAsync(stockIds);
        var locDict = await LoadStockLocDictAsync(locIds);
        var auxDict = await LoadFlexAuxDictAsync(auxIds);
        var statusDict = await LoadStockStatusDictAsync(statusIds);

        // 物料"是否启用辅助属性"判定（按物料 Uid/FInterId 双键匹配 TBdMaterialAuxPty）
        var matIdList = matIds.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        var matKeyMap = new Dictionary<string, string[]>();
        var auxEnabledKeys = new HashSet<string>();
        if (matIdList.Count > 0)
        {
            var mats = await Db.Queryable<TBdMaterial>().Where(m => matIdList.Contains(m.Uid)).Select(m => new { m.Uid, m.FInterId }).ToListAsync();
            matKeyMap = mats.GroupBy(m => m.Uid).ToDictionary(g => g.Key, g => new[] { g.First().Uid, g.First().FInterId });
            var allKeys = mats.SelectMany(m => new[] { m.Uid, m.FInterId }).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
            if (allKeys.Count > 0)
            {
                var auxRows = await Db.Queryable<TBdMaterialAuxPty>()
                    .Where(a => a.FIsEnable && !a.FDeleted && (allKeys.Contains(a.FInterId) || allKeys.Contains(a.FMasterId)))
                    .Select(a => new { a.FInterId, a.FMasterId }).ToListAsync();
                foreach (var ar in auxRows)
                {
                    if (!string.IsNullOrEmpty(ar.FInterId)) auxEnabledKeys.Add(ar.FInterId);
                    if (!string.IsNullOrEmpty(ar.FMasterId)) auxEnabledKeys.Add(ar.FMasterId);
                }
            }
        }

        foreach (var line in dto.Entries)
        {
            if (matDict.TryGetValue(line.Fmaterialid ?? string.Empty, out var m))
            {
                line.FmaterialNumber = m.Number; line.FmaterialName = m.Name; line.FSpecification = m.Spec;
                line.FisBatchManage = m.Batch; line.FisKfPeriod = m.Kf; line.FKfPeriod = m.KfPeriod; line.FKfUnit = m.KfUnit;
            }
            if (unitDict.TryGetValue(line.Funitid ?? string.Empty, out var u)) { line.FunitNumber = u.Number; line.FunitName = u.Name; }
            if (stockDict.TryGetValue(line.Fstockid ?? string.Empty, out var st)) { line.FstockNumber = st.Number; line.FstockName = st.Name; line.FisOpenLocation = st.OpenLoc; }
            line.FstocklocName = locDict.GetValueOrDefault(line.Fstocklocid ?? string.Empty, string.Empty);
            line.FauxpropName = auxDict.GetValueOrDefault(line.Fauxpropid ?? string.Empty, string.Empty);
            line.FstockstatusName = statusDict.GetValueOrDefault(line.Fstockstatusid ?? string.Empty, string.Empty);
            if (matKeyMap.TryGetValue(line.Fmaterialid ?? string.Empty, out var ks))
                line.FisAuxEnabled = ks.Any(k => !string.IsNullOrEmpty(k) && auxEnabledKeys.Contains(k));
        }

        foreach (var line in dto.BarcodeEntries)
        {
            if (matDict.TryGetValue(line.Fmaterialid ?? string.Empty, out var m))
            {
                line.FmaterialNumber = m.Number; line.FmaterialName = m.Name; line.FSpecification = m.Spec; line.FisBatchManage = m.Batch;
                line.FisKfPeriod = m.Kf; line.FKfPeriod = m.KfPeriod; line.FKfUnit = m.KfUnit; line.FisSecUnit = m.SecUnit;
            }
            if (unitDict.TryGetValue(line.Funitid ?? string.Empty, out var u)) { line.FunitNumber = u.Number; line.FunitName = u.Name; }
            if (unitDict.TryGetValue(line.Fsecunitid ?? string.Empty, out var su)) { line.FsecunitNumber = su.Number; line.FsecunitName = su.Name; }
            if (stockDict.TryGetValue(line.Fstockid ?? string.Empty, out var st)) { line.FstockNumber = st.Number; line.FstockName = st.Name; line.FisOpenLocation = st.OpenLoc; }
            line.FstocklocName = locDict.GetValueOrDefault(line.Fstocklocid ?? string.Empty, string.Empty);
            line.FauxpropName = auxDict.GetValueOrDefault(line.Fauxpropid ?? string.Empty, string.Empty);
            line.FstockstatusName = statusDict.GetValueOrDefault(line.Fstockstatusid ?? string.Empty, string.Empty);
        }

        foreach (var line in dto.BottomEntries)
        {
            if (matDict.TryGetValue(line.Fmaterialid ?? string.Empty, out var m)) { line.FmaterialNumber = m.Number; line.FmaterialName = m.Name; }
        }
    }

    protected override InStockDetailDto MapToDetailDto(TStkInstock header, List<TStkInstockentry> entries) => new()
    {
        Uid = header.Uid,
        Fbillno = header.Fbillno,
        Fbilltypeid = header.Fbilltypeid,
        Fdate = header.Fdate,
        FStatus = header.FStatus,
        Ftypeid = header.Ftypeid,
        Fbusinesstype = header.Fbusinesstype,
        Fsrcformid = header.Fsrcformid,
        Fsrcbillno = header.Fsrcbillno,
        Fdemandorgid = header.Fdemandorgid,
        Fpurchaseorgid = header.Fpurchaseorgid,
        Fcompanyid = header.FCompanyId,
        Fpurchasedeptid = header.Fpurchasedeptid,
        Fmrdeptid = header.Fmrdeptid,
        Fpurchaserid = header.Fpurchaserid,
        Fstockerid = header.Fstockerid,
        Fempid = header.Fempid,
        Fsupplyid = header.Fsupplyid,
        Fcurrencyid = header.Fcurrencyid,
        Fexchangetypeid = header.Fexchangetypeid,
        Fexchangerate = header.Fexchangerate,
        Fstockid = header.Fstockid,
        Fstocklocid = header.Fstocklocid,
        Fstockstatusid = header.Fstockstatusid,
        CUser = header.CUser,
        CYmd = header.CYmd,
        Fcheckerid = header.Fcheckerid,
        Fcheckdate = header.Fcheckdate,
        MUser = header.MUser,
        MYmd = header.MYmd,
        Fdisableid = header.Fdisableid,
        Fdisabledate = header.Fdisabledate,
        FDisabled = header.FDisabled,
        Entries = entries.Select(e => new InStockMaterialEntryDto
        {
            Uid = e.Uid,
            Fentryid = e.FENTRYID,
            Fmaterialid = e.Fmaterialid,
            Fauxpropid = e.Fauxpropid,
            Flot = e.Flot,
            Fstockid = e.Fstockid,
            Fstocklocid = e.Fstocklocid,
            Fstockstatusid = e.FSTOCKSTATUSID,
            Frealqty = e.Frealqty,
            Fmustqty = e.Fmustqty,
            Funitid = e.Funitid,
            Fbaseunitid = e.Fbaseunitid,
            Fbaseunitqty = e.FBASEUNITQTY ?? 0,
            Fsecunitid = e.FSECUNITID,
            Fsecunitqty = e.FSECUNITQTY ?? 0,
            Fwwintype = e.Fwwintype,
            Fkfdate = e.Fkfdate,
            Fusefuldate = e.Fusefuldate,
            Fprice = e.Fprice,
            Ftaxprice = e.Ftaxprice,
            Ftaxrate = e.Ftaxrate,
            Fdiscountrate = e.Fdiscountrate,
            Fdiscount = e.Fdiscount,
            Ftaxamount = e.FTAXAMOUNT ?? 0,
            Famount = e.Famount,
            Fallamount = e.FALLAMOUNT ?? 0,
            Fsrcformid = e.Fsrcformid,
            Fsrcbillno = e.Fsrcbillno,
            Fsrcentryid = e.Fsrcentryid,
            Forderbillno = e.FORDERBILLNO,
            Forderentryid = e.FORDERENTRYID ?? 0,
            Forderinterid = e.FORDERINTERID,
            Forderdetailid = e.FORDERDETAILID
        }).ToList()
    };

    private static InStockBarcodeEntryDto MapBarcodeEntry(TStkInstockentry1 e) => new()
    {
        Uid = e.Uid,
        Fentryid = e.Fentryid,
        Ftypeid = e.Ftypeid,
        Fisbox = e.Fisbox,
        Fboxbarcode = e.Fboxbarcode,
        Fbarcode = e.Fbarcode,
        Fbartype = e.Fbartype,
        Fmaterialid = e.Fmaterialid,
        Fauxpropid = e.Fauxpropid,
        Flot = e.Flot,
        Fstockid = e.Fstockid,
        Fstocklocid = e.Fstocklocid,
        Fqty = e.Fqty,
        Funitid = e.Funitid,
        Fbaseunitid = e.Fbaseunitid,
        Fbaseunitqty = e.Fbaseunitqty,
        Fsecunitid = e.Fsecunitid,
        Fsecunitqty = e.Fsecunitqty,
        Fstockstatusid = e.Fstockstatusid,
        Fwwintype = e.Fwwintype,
        Fsupplyid = e.Fsupplyid,
        Fkfdate = e.Fkfdate,
        Fusefuldate = e.Fusefuldate,
        Ftaxprice = e.Ftaxprice,
        Ftaxrate = e.Ftaxrate,
        Fdiscountrate = e.Fdiscountrate,
        Fsrcformid = e.Fsrcformid,
        Fsrcbillno = e.Fsrcbillno,
        Fsrcentryid = e.Fsrcentryid,
        Fsrcdetailid = e.Fsrcdetailid,
        Fordertypeid = e.Fordertypeid,
        Forderinterid = e.Forderinterid,
        Forderbillno = e.Forderbillno,
        Forderdetailid = e.Forderdetailid,
        Forderentryid = e.Forderentryid
    };

    private static InStockBottomEntryDto MapBottomEntry(TStkInstockentry2 e) => new()
    {
        Uid = e.Uid,
        Fentryid = e.Fentryid,
        Fboxbarcode = e.Fboxbarcode,
        Fbarcode = e.Fbarcode,
        Fmaterialid = e.Fmaterialid,
        Fauxpropid = e.Fauxpropid,
        Flot = e.Flot,
        Fkfdate = e.Fkfdate,
        Fusefuldate = e.Fusefuldate,
        Fqty = e.Fqty,
        Fstockid = e.Fstockid,
        Fstocklocid = e.Fstocklocid,
        Fsupplyid = e.Fsupplyid,
        Funitid = e.Funitid,
        Fbaseunitid = e.Fbaseunitid,
        Fbaseunitqty = e.Fbaseunitqty,
        Ftaxprice = e.Ftaxprice,
        Ftaxrate = e.Ftaxrate,
        Fdiscountrate = e.Fdiscountrate,
        Fstockstatusid = e.Fstockstatusid,
        Fsrcformid = e.Fsrcformid,
        Fsrcbillno = e.Fsrcbillno,
        Fsrcentryid = e.Fsrcentryid,
        Fsrcdetailid = e.Fsrcdetailid,
        Fordertypeid = e.Fordertypeid,
        Forderinterid = e.Forderinterid,
        Forderbillno = e.Forderbillno,
        Forderdetailid = e.Forderdetailid,
        Forderentryid = e.Forderentryid
    };

    // ===== 写入映射（表头）=====

    protected override TStkInstock MapToHeaderEntity(CreateInStockRequest dto) => new()
    {
        Fbillno = dto.Fbillno?.Trim() ?? string.Empty,
        Fbilltypeid = dto.Fbilltypeid,
        Ftypeid = dto.Ftypeid,
        Fdate = dto.Fdate ?? DateTime.Now,
        Fbusinesstype = dto.Fbusinesstype,
        Fsrcformid = dto.Fsrcformid,
        Fsrcbillno = dto.Fsrcbillno,
        Fdemandorgid = dto.Fdemandorgid,
        Fpurchaseorgid = dto.Fpurchaseorgid,
        FCompanyId = dto.Fcompanyid,                 // 收料组织 = 单据所属组织（为空时基类回落当前组织）
        Fpurchasedeptid = dto.Fpurchasedeptid,
        Fmrdeptid = dto.Fmrdeptid,
        Fpurchaserid = dto.Fpurchaserid,
        Fstockerid = dto.Fstockerid,
        Fempid = dto.Fempid,
        Fsupplyid = dto.Fsupplyid,
        Fcurrencyid = dto.Fcurrencyid,
        Fexchangetypeid = dto.Fexchangetypeid,
        Fexchangerate = string.IsNullOrEmpty(dto.Fexchangerate) ? "1" : dto.Fexchangerate,
        Fstockid = dto.Fstockid,
        Fstocklocid = dto.Fstocklocid,
        Fstockstatusid = dto.Fstockstatusid,
        Fcheckdate = DateTime.MinValue,        // FCHECKDATE 为 DATE 列(下限 0001)，MinValue 安全
        Fdisabledate = new DateTime(1900, 1, 1), // FDISABLEDATE 为 DATETIME 列(下限 1753)：用 1900 哨兵，避 MinValue 溢出、又满足开发库可能的 NOT NULL；前端按<=1900过滤
        FStatus = 10
    };

    protected override void UpdateHeaderEntity(TStkInstock entity, UpdateInStockRequest dto)
    {
        entity.Fbilltypeid = dto.Fbilltypeid;
        entity.Ftypeid = dto.Ftypeid;
        entity.Fdate = dto.Fdate ?? entity.Fdate;
        entity.Fbusinesstype = dto.Fbusinesstype;
        entity.Fsrcformid = dto.Fsrcformid;
        entity.Fsrcbillno = dto.Fsrcbillno;
        entity.Fdemandorgid = dto.Fdemandorgid;
        entity.Fpurchaseorgid = dto.Fpurchaseorgid;
        if (!string.IsNullOrEmpty(dto.Fcompanyid)) entity.FCompanyId = dto.Fcompanyid;
        entity.Fpurchasedeptid = dto.Fpurchasedeptid;
        entity.Fmrdeptid = dto.Fmrdeptid;
        entity.Fpurchaserid = dto.Fpurchaserid;
        entity.Fstockerid = dto.Fstockerid;
        entity.Fempid = dto.Fempid;
        entity.Fsupplyid = dto.Fsupplyid;
        entity.Fcurrencyid = dto.Fcurrencyid;
        entity.Fexchangetypeid = dto.Fexchangetypeid;
        entity.Fexchangerate = string.IsNullOrEmpty(dto.Fexchangerate) ? "1" : dto.Fexchangerate;
        entity.Fstockid = dto.Fstockid;
        entity.Fstocklocid = dto.Fstocklocid;
        entity.Fstockstatusid = dto.Fstockstatusid;
    }

    // 抽象成员的契约实现（实际写入走重写的 Create/Update + PersistEntriesAsync）
    protected override List<TStkInstockentry> MapToEntryEntities(CreateInStockRequest dto, string headerUid)
        => dto.Entries.Where(r => !string.IsNullOrEmpty(r.Fmaterialid)).Select(BuildMaterialFromReq).ToList();
    protected override List<TStkInstockentry> MapToEntryEntities(UpdateInStockRequest dto, string headerUid)
        => dto.Entries.Where(r => !string.IsNullOrEmpty(r.Fmaterialid)).Select(BuildMaterialFromReq).ToList();

    protected override void SetEntryIndex(TStkInstockentry entry, int index)
    {
        entry.FENTRYID = index;
        entry.FDETAILID = entry.Uid;
    }

    protected override async Task<List<TStkInstockentry>> GetEntriesByHeaderIdAsync(string headerUid)
        => await Db.Queryable<TStkInstockentry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.FENTRYID)
            .ToListAsync();

    // ===== 编码规则取号 =====
    protected override string? BillCodeFormKey => BillCodeFormKeys.InStock;
    protected override string GetBillNo(TStkInstock header) => header.Fbillno;
    protected override void SetBillNo(TStkInstock header, string billNo) => header.Fbillno = billNo;
    protected override Task<bool> BillNoExistsAsync(string billNo)
        => Db.Queryable<TStkInstock>().AnyAsync(h => h.Fbillno == billNo);

    protected override async Task PopulateBillCodeContextAsync(IDictionary<string, string> ctx, TStkInstock header, CreateInStockRequest dto)
    {
        ctx[BillCodeFields.Date] = (header.Fdate ?? DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
        if (!string.IsNullOrEmpty(header.Fbilltypeid))
        {
            var billTypeNo = await Db.Queryable<TBasBilltype>().Where(b => b.Uid == header.Fbilltypeid).Select(b => b.Fnumber).FirstAsync();
            if (!string.IsNullOrEmpty(billTypeNo)) ctx[BillCodeFields.BillType] = billTypeNo;
        }
        if (!string.IsNullOrEmpty(header.Fsupplyid))
        {
            var supplierNo = await Db.Queryable<TBdSupplier>().Where(s => s.Uid == header.Fsupplyid).Select(s => s.FNumber).FirstAsync();
            if (!string.IsNullOrEmpty(supplierNo)) ctx[BillCodeFields.Supplier] = supplierNo;
        }
    }

    // ===== 扫码解析 =====

    public async Task<ScanBarcodeResultDto> ScanBarcodeAsync(ScanBarcodeRequest request)
    {
        var code = (request.Fbarcode ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(code))
            return new ScanBarcodeResultDto { Found = false, Message = "请输入条码" };

        var master = await Db.Queryable<TBdBarcoders>().Where(m => m.Fbarcode == code && !m.FDeleted).FirstAsync();
        if (master == null)
            return new ScanBarcodeResultDto { Found = false, Message = $"条码不存在：{code}" };
        if (master.Fbarcodestatus == 10)
            return new ScanBarcodeResultDto { Found = false, Message = $"条码已废弃：{code}" };

        // 仓库/仓位：表头优先、其次回落条码主档；二者必须同源，避免「表头仓库 + 条码主档他库仓位」跨库错配
        // 表头填了仓库则带入录入明细（仓位随表头，表头没填仓位则留空由用户手动填）；表头没填仓库才整体回落条码主档
        bool useHeaderStock = !string.IsNullOrEmpty(request.Fstockid);
        string stockId = useHeaderStock ? request.Fstockid : (master.FSTOCKID ?? string.Empty);
        string locId = useHeaderStock ? request.Fstocklocid : (master.FSTOCKLOCID ?? string.Empty);
        // 库存状态：独立维度，维持原逻辑（优先条码主档，其次表头）
        string statusId = !string.IsNullOrEmpty(master.FSTOCKSTATUSID) ? master.FSTOCKSTATUSID : request.Fstockstatusid;

        var result = new ScanBarcodeResultDto { Found = true };

        if (master.Fisbox)
        {
            // 箱码：展开装箱清单子条码
            var childCodes = await Db.Queryable<TBdBarcodersentry>()
                .Where(x => x.Fboxcode == code && !x.FDeleted).Select(x => x.Fbarcode).ToListAsync();
            var childMasters = childCodes.Count > 0
                ? await Db.Queryable<TBdBarcoders>().Where(m => childCodes.Contains(m.Fbarcode) && !m.FDeleted).ToListAsync()
                : new List<TBdBarcoders>();

            // 可入库性校验：箱内任一子条码已收料/已入库/已废弃 → 整箱拒收（防重复入库/被调走）
            var badChild = childMasters.FirstOrDefault(c => BarcodeUnavailableReason(c) != null);
            if (badChild != null)
                return new ScanBarcodeResultDto { Found = false, Message = $"箱内条码 {badChild.Fbarcode} {BarcodeUnavailableReason(badChild)}，箱码不可入库" };

            result.Entry2 = childMasters.Select(c => MasterToBottom(c, stockId, locId, statusId)).ToList();
            decimal boxQty = childMasters.Count > 0 ? childMasters.Sum(c => c.FQTY ?? (c.Fbartype == 1 ? 1m : 0m)) : (master.FQTY ?? 0);
            result.Entry1 = MasterToBarcodeEntry(master, isBox: true, qty: boxQty, stockId, locId, statusId);
            if (childMasters.Count == 0) result.Message = "箱码无装箱明细，已按箱码主档数量入库";
        }
        else
        {
            // 可入库性校验：单品条码须为"初始+未入库"，已收料/已入库/已废弃则拒收（防重复入库/被调走）
            var reason = BarcodeUnavailableReason(master);
            if (reason != null) return new ScanBarcodeResultDto { Found = false, Message = $"{reason}：{code}" };
            // 单品/包装条码：ENTRY1 与 ENTRY2 都记录该条码
            decimal qty = master.FQTY ?? (master.Fbartype == 1 ? 1m : 0m);
            result.Entry1 = MasterToBarcodeEntry(master, isBox: false, qty: qty, stockId, locId, statusId);
            result.Entry2 = new List<InStockBottomEntryDto> { MasterToBottom(master, stockId, locId, statusId) };
        }

        // 解析名称（物料/单位/仓库/仓位/库存状态/辅助属性）
        await ResolveScanNamesAsync(result);

        // 源单溯源：条码标签打印时已写入源单档，扫码即带出源单/订单信息
        if (result.Entry1 != null)
        {
            var srcCodes = new List<string> { result.Entry1.Fbarcode, result.Entry1.Fboxbarcode };
            srcCodes.AddRange(result.Entry2.Select(b => b.Fbarcode));
            var srcDict = await LoadBarcodeSourceDictAsync(srcCodes);
            var key = string.IsNullOrEmpty(result.Entry1.Fboxbarcode) ? result.Entry1.Fbarcode : result.Entry1.Fboxbarcode;
            if (srcDict.TryGetValue(key, out var l1)) ApplySourceToScanEntry1(result.Entry1, l1);
            else if (result.Entry1.Fisbox && result.Entry2.Count > 0 && srcDict.TryGetValue(result.Entry2[0].Fbarcode, out var lc))
                ApplySourceToScanEntry1(result.Entry1, lc);   // 箱码自身无源单档时取首个子条码
            foreach (var b in result.Entry2)
                if (srcDict.TryGetValue(b.Fbarcode, out var lb)) ApplySourceToScanBottom(b, lb);
        }

        // 表头已指定源单时，校验该条码确实属于此源单（条码的源单编号或订单编号需匹配），防止误扫不相关条码入库
        if (!string.IsNullOrWhiteSpace(request.Fsrcbillno) && result.Entry1 != null)
        {
            var hb = request.Fsrcbillno.Trim();
            bool belongs = string.Equals(result.Entry1.Fsrcbillno, hb, StringComparison.OrdinalIgnoreCase)
                        || string.Equals(result.Entry1.Forderbillno, hb, StringComparison.OrdinalIgnoreCase);
            if (!belongs)
                return new ScanBarcodeResultDto { Found = false, Message = $"该条码不属于源单【{hb}】，不能扫入" };
        }
        return result;
    }

    private InStockBarcodeEntryDto MasterToBarcodeEntry(TBdBarcoders m, bool isBox, decimal qty, string stockId, string locId, string statusId) => new()
    {
        Ftypeid = 2,
        Fisbox = isBox,
        Fboxbarcode = isBox ? m.Fbarcode : string.Empty,
        Fbarcode = m.Fbarcode,
        Fbartype = m.Fbartype,
        Fmaterialid = m.Fmaterialid,
        Fauxpropid = m.FAUXPROPID,
        Flot = m.Flot,
        Fkfdate = m.FKFDATE,
        Fusefuldate = m.FUSEFULDATE,
        Fqty = qty,
        Fstockid = stockId,
        Fstocklocid = locId,
        Fstockstatusid = statusId,
        Funitid = m.FUNITID,
        Fbaseunitid = string.IsNullOrEmpty(m.FBASEUNITID) ? m.FUNITID : m.FBASEUNITID,
        Fbaseunitqty = qty,
        Fsecunitid = m.FSECUNITID,
        Fsecunitqty = m.FSECUNITQTY ?? 0,
        Fsupplyid = m.FSUPPLYID
    };

    private InStockBottomEntryDto MasterToBottom(TBdBarcoders m, string stockId, string locId, string statusId) => new()
    {
        Fboxbarcode = string.Empty,
        Fbarcode = m.Fbarcode,
        Fmaterialid = m.Fmaterialid,
        Fauxpropid = m.FAUXPROPID,
        Flot = m.Flot,
        Fkfdate = m.FKFDATE,
        Fusefuldate = m.FUSEFULDATE,
        Fqty = m.FQTY ?? (m.Fbartype == 1 ? 1m : 0m),
        Fstockid = stockId,
        Fstocklocid = locId,
        Fstockstatusid = statusId,
        Fsupplyid = m.FSUPPLYID,
        Funitid = m.FUNITID,
        Fbaseunitid = string.IsNullOrEmpty(m.FBASEUNITID) ? m.FUNITID : m.FBASEUNITID,
        Fbaseunitqty = m.FQTY ?? (m.Fbartype == 1 ? 1m : 0m)
    };

    private async Task ResolveScanNamesAsync(ScanBarcodeResultDto result)
    {
        if (result.Entry1 == null) return;
        var e = result.Entry1;
        var matDict = await LoadMaterialDictAsync(new[] { e.Fmaterialid });
        if (matDict.TryGetValue(e.Fmaterialid ?? string.Empty, out var m))
        {
            e.FmaterialNumber = m.Number; e.FmaterialName = m.Name; e.FSpecification = m.Spec; e.FisBatchManage = m.Batch;
            e.FisKfPeriod = m.Kf; e.FKfPeriod = m.KfPeriod; e.FKfUnit = m.KfUnit; e.FisSecUnit = m.SecUnit;
        }
        var unitDict = await LoadUnitDictAsync(new[] { e.Funitid, e.Fsecunitid });
        if (unitDict.TryGetValue(e.Funitid ?? string.Empty, out var u)) { e.FunitNumber = u.Number; e.FunitName = u.Name; }
        if (unitDict.TryGetValue(e.Fsecunitid ?? string.Empty, out var su)) { e.FsecunitNumber = su.Number; e.FsecunitName = su.Name; }
        var stockDict = await LoadStockDictAsync(new[] { e.Fstockid });
        if (stockDict.TryGetValue(e.Fstockid ?? string.Empty, out var st)) { e.FstockNumber = st.Number; e.FstockName = st.Name; e.FisOpenLocation = st.OpenLoc; }
        e.FstocklocName = (await LoadStockLocDictAsync(new[] { e.Fstocklocid })).GetValueOrDefault(e.Fstocklocid ?? string.Empty, string.Empty);
        e.FauxpropName = (await LoadFlexAuxDictAsync(new[] { e.Fauxpropid })).GetValueOrDefault(e.Fauxpropid ?? string.Empty, string.Empty);
        e.FstockstatusName = (await LoadStockStatusDictAsync(new[] { e.Fstockstatusid })).GetValueOrDefault(e.Fstockstatusid ?? string.Empty, string.Empty);

        var bottomMatDict = await LoadMaterialDictAsync(result.Entry2.Select(b => b.Fmaterialid));
        foreach (var b in result.Entry2)
            if (bottomMatDict.TryGetValue(b.Fmaterialid ?? string.Empty, out var bm)) { b.FmaterialNumber = bm.Number; b.FmaterialName = bm.Name; }
    }

    // ===== 源单溯源（条码标签打印时写入 T_BD_BARCODERS1：采购订单标签→FPO*；收料通知单标签→FPUR*+回填FPO*）=====

    private async Task<Dictionary<string, TBdBarcoders1>> LoadBarcodeSourceDictAsync(IEnumerable<string> codes)
    {
        var list = codes.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdBarcoders1>().Where(s => list.Contains(s.Fbarcode) && !s.FDeleted).ToListAsync();
        return rows.GroupBy(r => r.Fbarcode).ToDictionary(g => g.Key, g => g.First());
    }

    /// <summary>由源单档解析 (源单=入库直接来源, 订单=原始采购订单) 两组追溯字段。
    /// FPURDETAILID/FPURBILLNO 非空=收料通知单标签所生成(源单=收料通知单、订单=回填的采购订单)；否则=采购订单标签(源单=订单=采购订单)。</summary>
    private static (string SrcForm, string SrcId, string SrcBillNo, int SrcEntryId, string SrcDetailId,
                    string OrderType, string OrderInterId, string OrderBillNo, int OrderEntryId, string OrderDetailId)
        ResolveSource(TBdBarcoders1 l)
    {
        if (!string.IsNullOrEmpty(l.Fpurdetailid) || !string.IsNullOrEmpty(l.Fpurbillno))
            return (BillCodeFormKeys.ReceiveBill, l.Fpurid, l.Fpurbillno, l.Fpurentryid, l.Fpurdetailid,
                    BillCodeFormKeys.PurchaseOrder, l.Fpoid, l.Fpobillno, l.Fpoentreyid, l.Fpodetailid);
        if (!string.IsNullOrEmpty(l.Fpobillno) || !string.IsNullOrEmpty(l.Fpodetailid))
            return (BillCodeFormKeys.PurchaseOrder, l.Fpoid, l.Fpobillno, l.Fpoentreyid, l.Fpodetailid,
                    BillCodeFormKeys.PurchaseOrder, l.Fpoid, l.Fpobillno, l.Fpoentreyid, l.Fpodetailid);
        return (string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, 0, string.Empty);
    }

    private static void ApplySourceToScanEntry1(InStockBarcodeEntryDto e, TBdBarcoders1 l)
    {
        var s = ResolveSource(l);
        if (string.IsNullOrEmpty(s.SrcForm) && string.IsNullOrEmpty(s.OrderBillNo)) return;
        e.Fsrcformid = s.SrcForm; e.Fsrcbillno = s.SrcBillNo; e.Fsrcentryid = s.SrcEntryId; e.Fsrcdetailid = s.SrcDetailId;
        e.Fordertypeid = s.OrderType; e.Forderinterid = s.OrderInterId; e.Forderbillno = s.OrderBillNo; e.Forderentryid = s.OrderEntryId; e.Forderdetailid = s.OrderDetailId;
    }

    private static void ApplySourceToScanBottom(InStockBottomEntryDto e, TBdBarcoders1 l)
    {
        var s = ResolveSource(l);
        if (string.IsNullOrEmpty(s.SrcForm) && string.IsNullOrEmpty(s.OrderBillNo)) return;
        e.Fsrcformid = s.SrcForm; e.Fsrcbillno = s.SrcBillNo; e.Fsrcentryid = s.SrcEntryId; e.Fsrcdetailid = s.SrcDetailId;
        e.Fordertypeid = s.OrderType; e.Forderinterid = s.OrderInterId; e.Forderbillno = s.OrderBillNo; e.Forderentryid = s.OrderEntryId; e.Forderdetailid = s.OrderDetailId;
    }

    private static void ApplySourceToEntry1Entity(TStkInstockentry1 e, TBdBarcoders1 l)
    {
        var s = ResolveSource(l);
        if (string.IsNullOrEmpty(s.SrcForm) && string.IsNullOrEmpty(s.OrderBillNo)) return;
        e.Fsrcformid = s.SrcForm; e.Fsrcid = s.SrcId; e.Fsrcbillno = s.SrcBillNo; e.Fsrcentryid = s.SrcEntryId; e.Fsrcdetailid = s.SrcDetailId;
        e.Fordertypeid = s.OrderType; e.Forderinterid = s.OrderInterId; e.Forderbillno = s.OrderBillNo; e.Forderentryid = s.OrderEntryId; e.Forderdetailid = s.OrderDetailId;
    }

    private static void ApplySourceToEntry2Entity(TStkInstockentry2 e, TBdBarcoders1 l)
    {
        var s = ResolveSource(l);
        if (string.IsNullOrEmpty(s.SrcForm) && string.IsNullOrEmpty(s.OrderBillNo)) return;
        e.Fsrcformid = s.SrcForm; e.Fsrcid = s.SrcId; e.Fsrcbillno = s.SrcBillNo; e.Fsrcentryid = s.SrcEntryId; e.Fsrcdetailid = s.SrcDetailId;
        e.Fordertypeid = s.OrderType; e.Forderinterid = s.OrderInterId; e.Forderbillno = s.OrderBillNo; e.Forderentryid = s.OrderEntryId; e.Forderdetailid = s.OrderDetailId;
    }

    // ===== 源单类型（数据驱动）=====

    public async Task<List<SourceBillTypeDto>> GetSourceBillTypesAsync()
    {
        var rows = await Db.Queryable<TBosSelbill>()
            .Where(s => s.Fdesttrantype == "STK_InStock" && s.Fisuse && !s.FDeleted)
            .OrderBy(s => s.Fdefault, OrderByType.Desc)
            .Select(s => s.Fsourcetrantype).ToListAsync();
        var types = rows.Distinct().ToList();

        var nameRows = await Db.Queryable<SysBillTemplate>().Select(t => new { t.Fnumber, t.Fname }).ToListAsync();
        var nameDict = nameRows.Where(t => !string.IsNullOrEmpty(t.Fnumber)).GroupBy(t => t.Fnumber).ToDictionary(g => g.Key, g => g.First().Fname);

        return types.Select(rt => new SourceBillTypeDto
        {
            Value = rt ?? string.Empty,
            Label = string.IsNullOrEmpty(rt) ? "无源单" : nameDict.GetValueOrDefault(rt, rt)
        }).ToList();
    }

    // ===== 名称字典加载 =====

    private async Task<Dictionary<string, (string Number, string Name, string Spec, bool Batch, bool Kf, int KfPeriod, int KfUnit, bool SecUnit)>> LoadMaterialDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdMaterial>().Where(m => list.Contains(m.Uid))
            .Select(m => new { m.Uid, m.FNumber, m.FName, m.FSpecification, m.FIsBatchManage, m.FIsKfPeriod, m.FKfPeriod, m.FKfUnit, m.FISSECUNIT }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key,
            g => (g.First().FNumber, g.First().FName, g.First().FSpecification, g.First().FIsBatchManage, g.First().FIsKfPeriod, g.First().FKfPeriod, g.First().FKfUnit, g.First().FISSECUNIT ?? false));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadUnitDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdUnit>().Where(u => list.Contains(u.Uid)).Select(u => new { u.Uid, u.FNumber, u.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadSupplierDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdSupplier>().Where(s => list.Contains(s.Uid)).Select(s => new { s.Uid, s.FNumber, s.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadDepartmentDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdDepartment>().Where(d => list.Contains(d.Uid)).Select(d => new { d.Uid, d.FNumber, d.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name, bool OpenLoc)>> LoadStockDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdStock>().Where(s => list.Contains(s.Uid)).Select(s => new { s.Uid, s.FNumber, s.FName, s.FIsOpenLocation }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName, g.First().FIsOpenLocation));
    }

    private async Task<Dictionary<string, string>> LoadStockLocDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdStockPlace>().Where(f => list.Contains(f.Uid)).Select(f => new { f.Uid, f.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().FName);
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadOrgDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysOrgStructure>().Where(o => list.Contains(o.Uid)).Select(o => new { o.Uid, o.Fnumber, o.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().Fnumber, g.First().Fname));
    }

    private async Task<Dictionary<string, string>> LoadBillTypeDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBasBilltype>().Where(b => list.Contains(b.Uid)).Select(b => new { b.Uid, b.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadEmployeeNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<THrEmpinfo>().Where(e => list.Contains(e.Uid)).Select(e => new { e.Uid, e.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadUserNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysLoginUser>().Where(u => list.Contains(u.UserId)).Select(u => new { u.UserId, u.UserName }).ToListAsync();
        return rows.GroupBy(r => r.UserId).ToDictionary(g => g.Key, g => g.First().UserName);
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
        var rows = await Db.Queryable<TBdFlexauxproperty>().Where(f => list.Contains(f.Uid)).Select(f => new { f.Uid, f.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    /// <summary>库存状态名称字典：同时以 Uid 和 FNumber 为键（兼容本系统存 Uid 与条码主档存 FNUMBER 两种取值）</summary>
    private async Task<Dictionary<string, string>> LoadStockStatusDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<TBdStockstatus>()
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
