using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Application.Extensions;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 采购标签打印（收料通知单）服务。
/// 列表：已审核(40)且非禁用的收料通知单按明细行展开。
/// 条码生成：按"本次打印数量/包装数量"生成 N 个条码，写入 T_BD_BARCODERS(主档) + T_BD_BARCODERS1(源单档)。
/// 源单档：写 FPUR*（收料单内码/编号/行号/明细内码），并回填该收料行追溯的源采购订单 FPO*（如有），
/// 检验人员写 FINSPECTID/FINSPECTDATE（对应条码明细网格 IQC检验员/IQC检验日期）。
/// 条码编号：走编码规则取号 IBillCodeService（表单键 PUR_ReceiveBill），种子默认 = yyyyMMdd + 6 位日流水。
/// 条码状态/库存状态/打印来源等权威枚举见类内常量（与采购订单标签同一套）。
/// </summary>
public class ReceiveNoticeLabelService : IReceiveNoticeLabelService
{
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;
    private readonly IBillCodeService _billCode;
    private readonly IOperationLogService? _operationLog;

    private const string PrgKey = "LabelReceiveNotice";
    // 打印来源 FKID（T_BD_BARCODERS.FKID 生产 VARCHAR(2)，存枚举码）：
    // 1=采购订单 2=生产订单 3=委外订单 4=库存 5=物料 6=受托加工 7=不良处理拆卸产生 8=汇报生成 9=余料标签。
    // 收料通知单标签隶属采购收料流程且条码可追溯到源采购订单（源单档同时回填 FPO*），权威枚举无独立"收料"码，故归 1=采购订单。
    private const string FkidPurchaseOrder = "1";

    // 条码状态 FBARCODESTATUS：1=初始 2=收料 3=已拆分 4=已合并 5=已使用 10=已废弃
    // （生产 DB 默认 0 无业务含义，生成时必须显式置 1=初始）
    private const int BcInit = 1, BcVoid = 10;
    private static readonly Dictionary<int, string> BarcodeStatusNames = new()
    { [1] = "初始", [2] = "收料", [3] = "已拆分", [4] = "已合并", [5] = "已使用", [10] = "已废弃" };

    // 库存状态 FSTOCKSTATUS：0=未入库 1=已入库 2=生产领料出库 3=销售出库 4=其他出库
    // 5=委外调拨 6=委外领料 7=委外出库 8=已拆分 9=已合并 10=已废弃 11=受托加工出库
    private const int StockNotIn = 0;
    private static readonly Dictionary<int, string> StockStatusNames = new()
    {
        [0] = "未入库", [1] = "已入库", [2] = "生产领料出库", [3] = "销售出库", [4] = "其他出库",
        [5] = "委外调拨", [6] = "委外领料", [7] = "委外出库", [8] = "已拆分", [9] = "已合并", [10] = "已废弃", [11] = "受托加工出库"
    };

    // 高级筛选中归属"表头"的字段（其余按明细处理）；解析出来的名称列不参与服务端筛选
    private static readonly HashSet<string> HeaderFilterFields = new(StringComparer.OrdinalIgnoreCase) { "fbillno", "fdate" };

    public ReceiveNoticeLabelService(
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

    // ===== 列表：已审非禁用收料通知单按明细行展开 =====

    public async Task<PagedResult<ReceiveNoticeLabelListDto>> GetPagedListAsync(PagedRequest request)
    {
        var filters = request.DynamicFilters ?? new List<DynamicFilterInfo>();
        var headerFilters = filters.Where(f => HeaderFilterFields.Contains(f.Field)).ToList();
        var entryFilters = filters.Where(f => !HeaderFilterFields.Contains(f.Field)).ToList();

        // 1) 表头：仅已审核(40)且非禁用 + 关键字 + 表头动态筛选 -> 命中主表 Uid
        var hq = _db.Queryable<TPurReceive>().Where(h => !h.FDeleted && h.FStatus == 40 && !h.FDisabled);
        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var kw = request.Keyword.Trim();
            hq = hq.Where(h => h.Fbillno.Contains(kw));
        }
        if (headerFilters.Count > 0)
            hq = hq.Where(headerFilters.ToConditionalModels<TPurReceive>());
        var headerIds = await hq.Select(h => h.Uid).ToListAsync();
        if (headerIds.Count == 0)
            return Empty(request);

        // 2) 分页明细（+ 明细动态筛选）
        RefAsync<int> totalCount = 0;
        var query = _db.Queryable<TPurReceiveEntry>().Where(e => !e.FDeleted && headerIds.Contains(e.FInterId));
        if (entryFilters.Count > 0)
            query = query.Where(entryFilters.ToConditionalModels<TPurReceiveEntry>());
        var entries = await query
            .OrderBy(e => e.CYmd, OrderByType.Desc)
            .OrderBy(e => e.FENTRYID)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);
        if (entries.Count == 0)
            return Empty(request);

        // 3) 批量加载主表 + 名称源
        var hids = entries.Select(e => e.FInterId).Distinct().ToList();
        var headers = await _db.Queryable<TPurReceive>().Where(h => hids.Contains(h.Uid)).ToListAsync();
        var headerDict = headers.GroupBy(h => h.Uid).ToDictionary(g => g.Key, g => g.First());

        var materialDict = await LoadMaterialDictAsync(entries.Select(e => e.Fmaterialid));
        var unitDict = await LoadUnitDictAsync(entries.Select(e => e.Funitid));
        var supplierDict = await LoadSupplierDictAsync(headers.Select(h => h.Fsupplyid));
        var orgDict = await LoadOrgDictAsync(headers.Select(h => h.Fpurorgid));
        var statusDict = await LoadStatusDictAsync();
        var flexDict = await LoadFlexAuxDictAsync(entries.Select(e => e.Fauxpropid));
        var userDict = await LoadUserNameDictAsync(headers.SelectMany(h => new[] { h.CUser, h.Fcheckerid }));

        var items = entries.Select(e =>
        {
            headerDict.TryGetValue(e.FInterId, out var h);
            materialDict.TryGetValue(e.Fmaterialid ?? string.Empty, out var mat);
            unitDict.TryGetValue(e.Funitid ?? string.Empty, out var unit);
            return new ReceiveNoticeLabelListDto
            {
                Uid = h?.Uid ?? e.FInterId,
                EntryUid = e.Uid,
                Fentryid = e.FENTRYID,
                Fdate = h?.Fdate,
                Fbillno = h?.Fbillno ?? string.Empty,
                FpurorgName = orgDict.GetValueOrDefault(h?.Fpurorgid ?? string.Empty, string.Empty),
                Fmaterialid = e.Fmaterialid,
                FmaterialNumber = mat.Number,
                FmaterialName = mat.Name,
                FSpecification = mat.Spec,
                FauxpropName = flexDict.GetValueOrDefault(e.Fauxpropid ?? string.Empty, string.Empty),
                Flot = e.Flot,
                FunitName = unit.Name,
                FsupplyNumber = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Number : string.Empty,
                FsupplyName = h != null ? supplierDict.GetValueOrDefault(h.Fsupplyid, default).Name : string.Empty,
                Factreceiveqty = e.Factreceiveqty,
                Finstockqty = e.Finstockqty,
                Fkfdate = e.Fkfdate,
                FisKfPeriod = mat.Kf,
                FKfPeriod = mat.KfPeriod,
                FKfUnit = mat.KfUnit,
                Fincreaseqty = mat.IncreaseQty,
                FStatus = h?.FStatus ?? 0,
                FstatusName = statusDict.GetValueOrDefault(h?.FStatus ?? 0, string.Empty),
                FDisabled = h?.FDisabled ?? false,
                CuserName = userDict.GetValueOrDefault(h?.CUser ?? string.Empty, string.Empty),
                CYmd = h?.CYmd,
                FcheckerName = userDict.GetValueOrDefault(h?.Fcheckerid ?? string.Empty, string.Empty),
                Fcheckdate = h?.Fcheckdate
            };
        }).ToList();

        return new PagedResult<ReceiveNoticeLabelListDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    private static PagedResult<ReceiveNoticeLabelListDto> Empty(PagedRequest request) => new()
    {
        Items = new(),
        TotalCount = 0,
        PageIndex = request.PageIndex,
        PageSize = request.PageSize
    };

    // ===== 条码生成页单据头 =====

    public async Task<ReceiveNoticeLabelHeadDto?> GetGenerateHeadAsync(string entryUid)
    {
        var entry = await _db.Queryable<TPurReceiveEntry>().Where(e => e.Uid == entryUid && !e.FDeleted).FirstAsync();
        if (entry == null) return null;
        var header = await _db.Queryable<TPurReceive>().Where(h => h.Uid == entry.FInterId && !h.FDeleted).FirstAsync();
        if (header == null) return null;

        var dto = new ReceiveNoticeLabelHeadDto
        {
            Uid = header.Uid,
            EntryUid = entry.Uid,
            Fentryid = entry.FENTRYID,
            Fbillno = header.Fbillno,
            Fdate = header.Fdate,
            Fbusinesstype = header.Fbusinesstype,
            Fsupplyid = header.Fsupplyid,
            Fmaterialid = entry.Fmaterialid,
            Fauxpropid = entry.Fauxpropid,
            Factreceiveqty = entry.Factreceiveqty,
            Funitid = entry.Funitid,
            Fbaseunitid = entry.Fbaseunitid,
            Flot = entry.Flot,
            Fkfdate = entry.Fkfdate,
            Fexpiredate = entry.Fexpiredate
        };

        dto.FpurorgName = await _db.Queryable<SysOrgStructure>().Where(o => o.Uid == header.Fpurorgid).Select(o => o.Fname).FirstAsync() ?? string.Empty;
        var supplier = await LoadSupplierDictAsync(new[] { header.Fsupplyid });
        if (supplier.TryGetValue(header.Fsupplyid, out var sp)) { dto.FsupplyNumber = sp.Number; dto.FsupplyName = sp.Name; }
        var unit = await LoadUnitDictAsync(new[] { entry.Funitid, entry.Fbaseunitid });
        dto.FunitName = unit.GetValueOrDefault(entry.Funitid ?? string.Empty, default).Name;
        dto.FbaseunitName = unit.GetValueOrDefault(entry.Fbaseunitid ?? string.Empty, default).Name;
        dto.FauxpropName = (await LoadFlexAuxDictAsync(new[] { entry.Fauxpropid })).GetValueOrDefault(entry.Fauxpropid ?? string.Empty, string.Empty);

        var mat = await _db.Queryable<TBdMaterial>().Where(m => m.Uid == entry.Fmaterialid)
            .Select(m => new { m.FNumber, m.FName, m.FSpecification, m.FIsBatchManage, m.FIsKfPeriod, m.FKfPeriod, m.FKfUnit, m.FIncreaseQty, m.FBARTYPE }).FirstAsync();
        if (mat != null)
        {
            dto.FmaterialNumber = mat.FNumber; dto.FmaterialName = mat.FName; dto.FSpecification = mat.FSpecification;
            dto.FisBatchManage = mat.FIsBatchManage; dto.FisKfPeriod = mat.FIsKfPeriod;
            dto.Fkfperiod = mat.FKfPeriod; dto.Fkfunit = mat.FKfUnit;
            dto.Fincreaseqty = mat.FIncreaseQty;
            dto.Fbartype = (mat.FBARTYPE.HasValue && mat.FBARTYPE.Value >= 1 && mat.FBARTYPE.Value <= 3) ? mat.FBARTYPE.Value : 1;
        }
        else dto.Fbartype = 1;

        // 按"收料单内码+行号"稳定键统计：明细行内码(Uid)会因单据编辑重存(物理删+重插)而变，旧条码 Fpurdetailid 指向已删旧行；兼容仍指当前行的旧 detailid
        var genHeadId = entry.FInterId; var genRowNo = entry.FENTRYID ?? 0; var genEntUid = entry.Uid;
        dto.GeneratedCount = genRowNo > 0
            ? await _db.Queryable<TBdBarcoders1>().Where(s => !s.FDeleted && (s.Fpurdetailid == genEntUid || (s.Fpurid == genHeadId && s.Fpurentryid == genRowNo))).CountAsync()
            : await _db.Queryable<TBdBarcoders1>().Where(s => !s.FDeleted && s.Fpurdetailid == genEntUid).CountAsync();
        return dto;
    }

    // ===== 条码明细查询 =====

    public async Task<List<BarcodeLineDto>> GetBarcodesAsync(string receiveEntryUid)
    {
        // 明细行内码(Uid)在收料通知单被编辑重存时会重建(物理删+重插)，旧条码的 Fpurdetailid 指向已删除的旧行内码。
        // 故按"收料单内码 + 行号"稳定键匹配(并兼容仍指向当前行的旧 Fpurdetailid)，避免编辑后条码丢失显示。
        var entry = await _db.Queryable<TPurReceiveEntry>().Where(e => e.Uid == receiveEntryUid && !e.FDeleted)
            .Select(e => new { e.FInterId, e.FENTRYID }).FirstAsync();
        var headId = entry?.FInterId ?? string.Empty;
        var rowNo = entry?.FENTRYID ?? 0;
        var links = (rowNo > 0 && !string.IsNullOrEmpty(headId))
            ? await _db.Queryable<TBdBarcoders1>().Where(s => !s.FDeleted && (s.Fpurdetailid == receiveEntryUid || (s.Fpurid == headId && s.Fpurentryid == rowNo))).ToListAsync()
            : await _db.Queryable<TBdBarcoders1>().Where(s => !s.FDeleted && s.Fpurdetailid == receiveEntryUid).ToListAsync();
        if (links.Count == 0) return new();
        var codes = links.Select(l => l.Fbarcode).Distinct().ToList();
        var masters = await _db.Queryable<TBdBarcoders>().Where(m => codes.Contains(m.Fbarcode) && !m.FDeleted).ToListAsync();
        return await BuildBarcodeLinesAsync(masters, links);
    }

    // ===== 条码生成 =====

    public async Task<List<BarcodeLineDto>> GenerateAsync(GenerateReceiveBarcodeRequest request)
    {
        var entry = await _db.Queryable<TPurReceiveEntry>().Where(e => e.Uid == request.ReceiveEntryUid && !e.FDeleted).FirstAsync()
            ?? throw new KeyNotFoundException("收料通知单明细不存在");
        var header = await _db.Queryable<TPurReceive>().Where(h => h.Uid == entry.FInterId && !h.FDeleted).FirstAsync()
            ?? throw new KeyNotFoundException("收料通知单不存在");
        if (header.FStatus != 40 || header.FDisabled)
            throw new InvalidOperationException("只有已审核且非禁用的收料通知单才能打印标签");

        if (request.PrintQty <= 0) throw new InvalidOperationException("本次打印数量必须大于 0");

        var bartype = (request.Fbartype >= 1 && request.Fbartype <= 3) ? request.Fbartype : 1;
        // 每个条码的数量 unitQty：
        //   单品条码(1)——恒为 1（忽略包装数量），共 ceil(打印数量) 个；
        //   最小包装量(2)/批次(3)——等于包装数量，末个取余数，共 ceil(打印数量/包装数量) 个。
        var unitQty = bartype == 1 ? 1m : request.PackageQty;
        if (unitQty <= 0) throw new InvalidOperationException("包装数量必须大于 0");

        // 先以 decimal 校验上下界，再转 int，避免极大商值在 (int) 强转时抛 OverflowException → 500
        var rawCount = Math.Ceiling(request.PrintQty / unitQty);
        if (rawCount <= 0 || rawCount > 5000) throw new InvalidOperationException("条码数量必须在 1..5000 之间");
        var count = (int)rawCount;

        var now = DateTime.Now;
        var userId = _currentUser.UserId ?? string.Empty;
        var companyId = string.IsNullOrEmpty(header.Fpurorgid) ? (_currentUser.CompanyId ?? string.Empty) : header.Fpurorgid;
        var batchId = Guid.NewGuid().ToString("N"); // 本次生成标识 FCURRID
        var hasInspect = !string.IsNullOrEmpty(request.Finspectid);

        var masters = new List<TBdBarcoders>(count);
        var links = new List<TBdBarcoders1>(count);

        // 编码规则动态字段上下文（规则配置了对应字段段才会用到）
        var materialNumber = await _db.Queryable<TBdMaterial>().Where(m => m.Uid == entry.Fmaterialid).Select(m => m.FNumber).FirstAsync() ?? string.Empty;
        var supplierNumber = await _db.Queryable<TBdSupplier>().Where(s => s.Uid == header.Fsupplyid).Select(s => s.FNumber).FirstAsync() ?? string.Empty;
        var ctx = new Dictionary<string, string>
        {
            [BillCodeFields.Date] = now.ToString("yyyy-MM-dd HH:mm:ss"),
            [BillCodeFields.Material] = materialNumber,
            [BillCodeFields.Lot] = request.Flot ?? string.Empty,
            [BillCodeFields.SourceBillNo] = header.Fbillno,
            [BillCodeFields.Supplier] = supplierNumber
        };

        try
        {
            _db.AsTenant().BeginTran();

            // 统一编码规则取号（事务内原子占号，回滚一并释放）
            var barcodeNos = await _billCode.NextBarcodesAsync(BillCodeFormKeys.ReceiveBill, count, ctx);

            // 生产 T_BD_BARCODERS.FBARCODE 有唯一聚簇索引而 SQLite 开发库没有：
            // 显式查重把"开发库静默重号/生产裸索引报错"变成清晰的业务错误
            var dup = await _db.Queryable<TBdBarcoders>().Where(m => barcodeNos.Contains(m.Fbarcode)).AnyAsync();
            if (dup)
                throw new InvalidOperationException("生成的条码与已有条码重号，请检查编码规则或流水计数后重试");

            var remaining = request.PrintQty;

            for (var i = 0; i < count; i++)
            {
                var qty = (i == count - 1) ? remaining : unitQty;
                remaining -= unitQty;
                var barcodeNo = barcodeNos[i];
                var masterUid = Guid.NewGuid().ToString("N");
                var linkUid = Guid.NewGuid().ToString("N");

                masters.Add(new TBdBarcoders
                {
                    Uid = masterUid, FInterId = masterUid, FGroupId = string.Empty,
                    Fbarcode = barcodeNo, Fobarcode = string.Empty,
                    Fmaterialid = entry.Fmaterialid,
                    Fbarcodestatus = BcInit, Fbartype = bartype,
                    Fstockstatus = StockNotIn, Fqualitystatus = 0,
                    Fisbox = false, Fboxcodetype = 0, Fismix = false,
                    Flotid = string.Empty, Flot = request.Flot,
                    FKID = FkidPurchaseOrder, FAUXPROPID = entry.Fauxpropid,
                    FQTY = qty, FBASEQTY = qty, Ftotalqty = request.PrintQty, FFORMQTY = unitQty,
                    FSALEQTY = 0, FINBOX = false, // 生产库 NOT NULL，显式赋默认值（显式写 NULL 不触发 DB DEFAULT）
                    FUNITID = entry.Funitid, FBASEUNITID = string.IsNullOrEmpty(entry.Fbaseunitid) ? entry.Funitid : entry.Fbaseunitid,
                    FSECUNITID = request.EnableAuxQty ? request.Fsecunitid : string.Empty,
                    FSECUNITQTY = request.EnableAuxQty ? request.Fsecqty : 0,
                    FSUPPLYID = header.Fsupplyid,
                    // 货主/保管者：从收料通知单明细带到条码主档（采购订单标签生成的条码无此来源，保持空值）
                    FKEEPERTYPEID = entry.Fkeepertypeid, FKEEPERID = entry.Fkeeperid,
                    FOWNERTYPEID = entry.Fownertypeid, FOWNERID = entry.Fownerid,
                    FKFDATE = request.Fkfdate, FUSEFULDATE = request.Fusefuldate,
                    Fmfgdate = DateTime.MinValue, FDATE = null, FINSPECTDATE = hasInspect ? now : null,
                    FCURRID = batchId,
                    FStatus = 0, FDeleted = false, FDisabled = false, FCompanyId = companyId,
                    CYmd = now, CUser = userId, MYmd = now, MUser = userId
                });

                links.Add(new TBdBarcoders1
                {
                    Uid = linkUid, FInterId = linkUid, FGroupId = string.Empty,
                    Fbarcode = barcodeNo,
                    // 源采购订单（来自该收料行的源单追溯，可能为空）
                    Fpoid = entry.Forderinterid, Fpobillno = entry.Forderbillno,
                    Fpoentreyid = entry.Forderentryid, Fpodetailid = entry.Forderdetailid,
                    // 收料通知单（本功能源单）
                    Fpurid = header.Uid, Fpurbillno = header.Fbillno,
                    Fpurentryid = entry.FENTRYID ?? 0, Fpurdetailid = entry.Uid,
                    Fsupplierid = header.Fsupplyid,
                    Finspectid = request.Finspectid ?? string.Empty,
                    Finspectdate = hasInspect ? now : DateTime.MinValue,
                    FStatus = 0, FDeleted = false, FDisabled = false, FCompanyId = companyId,
                    CYmd = now, CUser = userId, MYmd = now, MUser = userId
                });
            }

            await _db.Insertable(masters).ExecuteCommandAsync();
            await _db.Insertable(links).ExecuteCommandAsync();

            _db.AsTenant().CommitTran();
        }
        catch
        {
            _db.AsTenant().RollbackTran();
            throw;
        }

        // await 而非 fire-and-forget：Scoped SqlSugarClient 非线程安全，未等待的日志插入会与
        // 紧随其后的 BuildBarcodeLinesAsync 多条查询并发使用同一连接
        if (_operationLog != null)
            await _operationLog.LogAsync(PrgKey, OperationType.Create, entry.Uid, header.Fbillno, $"生成条码 {count} 个", true);

        return await BuildBarcodeLinesAsync(masters, links);
    }

    // ===== 作废 / 反作废 / 打印 =====

    public async Task<int> VoidAsync(List<string> barcodes)
    {
        var codes = (barcodes ?? new()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
        if (codes.Count == 0) return 0;
        var n = await _db.Updateable<TBdBarcoders>()
            .SetColumns(m => m.Fbarcodestatus == BcVoid)
            .SetColumns(m => m.MYmd == DateTime.Now)
            .SetColumns(m => m.MUser == (_currentUser.UserId ?? string.Empty))
            .Where(m => codes.Contains(m.Fbarcode) && !m.FDeleted && m.Fbarcodestatus == BcInit)
            .ExecuteCommandAsync();
        if (n > 0 && _operationLog != null) await _operationLog.LogAsync(PrgKey, OperationType.Update, string.Join(",", codes), null, $"作废条码 {n} 个", true);
        return n;
    }

    public async Task<int> UnvoidAsync(List<string> barcodes)
    {
        var codes = (barcodes ?? new()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
        if (codes.Count == 0) return 0;
        var n = await _db.Updateable<TBdBarcoders>()
            .SetColumns(m => m.Fbarcodestatus == BcInit)
            .SetColumns(m => m.MYmd == DateTime.Now)
            .SetColumns(m => m.MUser == (_currentUser.UserId ?? string.Empty))
            .Where(m => codes.Contains(m.Fbarcode) && !m.FDeleted && m.Fbarcodestatus == BcVoid)
            .ExecuteCommandAsync();
        if (n > 0 && _operationLog != null) await _operationLog.LogAsync(PrgKey, OperationType.Update, string.Join(",", codes), null, $"反作废条码 {n} 个", true);
        return n;
    }

    public async Task<int> MarkPrintedAsync(List<string> barcodes)
    {
        var codes = (barcodes ?? new()).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
        if (codes.Count == 0) return 0;
        var now = DateTime.Now;
        var n = await _db.Updateable<TBdBarcoders>()
            .SetColumns(m => m.FDATE == now)
            .SetColumns(m => m.MYmd == now)
            .SetColumns(m => m.MUser == (_currentUser.UserId ?? string.Empty))
            .Where(m => codes.Contains(m.Fbarcode) && !m.FDeleted && m.Fbarcodestatus != BcVoid)
            .ExecuteCommandAsync();
        if (n > 0 && _operationLog != null) await _operationLog.LogAsync(PrgKey, OperationType.Print, string.Join(",", codes), null, $"打印条码 {n} 个", true);
        return n;
    }

    // ===== 条码明细行装配（master + link → DTO + 名称解析）=====

    private async Task<List<BarcodeLineDto>> BuildBarcodeLinesAsync(List<TBdBarcoders> masters, List<TBdBarcoders1> links)
    {
        if (masters.Count == 0) return new();
        var linkByCode = links.GroupBy(l => l.Fbarcode).ToDictionary(g => g.Key, g => g.First());

        var matDict = await LoadMaterialDictAsync(masters.Select(m => m.Fmaterialid));
        var unitDict = await LoadUnitDictAsync(masters.SelectMany(m => new[] { m.FUNITID, m.FBASEUNITID }));
        var flexDict = await LoadFlexAuxDictAsync(masters.Select(m => m.FAUXPROPID));
        var orgDict = await LoadOrgDictAsync(masters.Select(m => m.FCompanyId));
        var inspectDict = await LoadEmployeeNameDictAsync(links.Select(l => l.Finspectid));

        return masters
            .OrderBy(m => m.Fbarcode)
            .Select(m =>
            {
                linkByCode.TryGetValue(m.Fbarcode, out var link);
                matDict.TryGetValue(m.Fmaterialid ?? string.Empty, out var mat);
                return new BarcodeLineDto
                {
                    Uid = m.Uid,
                    Fbarcode = m.Fbarcode,
                    Fbarcodestatus = m.Fbarcodestatus,
                    FbarcodestatusName = BarcodeStatusNames.GetValueOrDefault(m.Fbarcodestatus, string.Empty),
                    Fstockstatus = m.Fstockstatus,
                    FstockstatusName = StockStatusNames.GetValueOrDefault(m.Fstockstatus, string.Empty),
                    FcompanyName = orgDict.GetValueOrDefault(m.FCompanyId ?? string.Empty, string.Empty),
                    Fdate = m.FDATE,
                    FmaterialNumber = mat.Number,
                    FmaterialName = mat.Name,
                    FSpecification = mat.Spec,
                    Flot = m.Flot,
                    FauxpropName = flexDict.GetValueOrDefault(m.FAUXPROPID ?? string.Empty, string.Empty),
                    Fqty = m.FQTY ?? 0,
                    FunitName = unitDict.GetValueOrDefault(m.FUNITID ?? string.Empty, default).Name,
                    Fbaseqty = m.FBASEQTY ?? 0,
                    FbaseunitName = unitDict.GetValueOrDefault(m.FBASEUNITID ?? string.Empty, default).Name,
                    Fkfdate = m.FKFDATE,
                    Fusefuldate = m.FUSEFULDATE,
                    Fsecunitqty = m.FSECUNITQTY ?? 0,
                    Fpobillno = link?.Fpobillno ?? string.Empty,
                    Fpurbillno = link?.Fpurbillno ?? string.Empty,
                    // Fpoentreyid 语义=采购订单表体行号，与 Fpobillno(采购订单编号)同组，取源采购订单追溯行号（与蓝本一致）
                    Fpoentreyid = link?.Fpoentreyid ?? 0,
                    FinspectName = link != null ? inspectDict.GetValueOrDefault(link.Finspectid ?? string.Empty, string.Empty) : string.Empty,
                    Finspectdate = (link != null && link.Finspectdate.HasValue && link.Finspectdate.Value.Year > 1900) ? link.Finspectdate : null
                };
            }).ToList();
    }

    // ===== 名称字典加载 =====

    private async Task<Dictionary<string, (string Number, string Name, string Spec, decimal IncreaseQty, bool Kf, int KfPeriod, int KfUnit)>> LoadMaterialDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdMaterial>().Where(m => list.Contains(m.Uid))
            .Select(m => new { m.Uid, m.FNumber, m.FName, m.FSpecification, m.FIncreaseQty, m.FIsKfPeriod, m.FKfPeriod, m.FKfUnit }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key,
            g => (g.First().FNumber, g.First().FName, g.First().FSpecification, g.First().FIncreaseQty, g.First().FIsKfPeriod, g.First().FKfPeriod, g.First().FKfUnit));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadUnitDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdUnit>().Where(u => list.Contains(u.Uid))
            .Select(u => new { u.Uid, u.FNumber, u.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, (string Number, string Name)>> LoadSupplierDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdSupplier>().Where(s => list.Contains(s.Uid))
            .Select(s => new { s.Uid, s.FNumber, s.FName }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => (g.First().FNumber, g.First().FName));
    }

    private async Task<Dictionary<string, string>> LoadOrgDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<SysOrgStructure>().Where(o => list.Contains(o.Uid))
            .Select(o => new { o.Uid, o.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadEmployeeNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<THrEmpinfo>().Where(e => list.Contains(e.Uid))
            .Select(e => new { e.Uid, e.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadUserNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<SysLoginUser>().Where(u => list.Contains(u.UserId))
            .Select(u => new { u.UserId, u.UserName }).ToListAsync();
        return rows.GroupBy(r => r.UserId).ToDictionary(g => g.Key, g => g.First().UserName);
    }

    private async Task<Dictionary<int, string>> LoadStatusDictAsync()
    {
        var rows = await _db.Queryable<SysStatus>().Select(s => new { s.Fitemid, s.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Fitemid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadFlexAuxDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await _db.Queryable<TBdFlexauxproperty>().Where(f => list.Contains(f.Uid))
            .Select(f => new { f.Uid, f.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Uid).ToDictionary(g => g.Key, g => g.First().Fname);
    }
}
