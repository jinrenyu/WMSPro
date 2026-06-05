using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 演示数据种子服务 - 仅在演示模式下插入模拟数据，生产环境不启用
/// </summary>
public class DemoDataSeedService
{
    private readonly ISqlSugarClient _db;

    public DemoDataSeedService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 根据前缀和编码生成确定性 Uid（基于 MD5 哈希），确保跨实体的外键引用一致
    /// </summary>
    private static string SeedUid(string prefix, string number) =>
        new Guid(System.Security.Cryptography.MD5.HashData(
            System.Text.Encoding.UTF8.GetBytes($"{prefix}_{number}"))).ToString("N");

    private static string DeptUid(string number) => SeedUid("DEPT", number);
    private static string UnitUid(string number) => SeedUid("UNIT", number);
    private static string StockUid(string number) => SeedUid("STOCK", number);
    private static string SpUid(string number) => SeedUid("SP", number);
    private static string StockStatusUid(string number) => SeedUid("STOCKSTATUS", number);
    private static string FlexValUid(string number) => SeedUid("FLEXVAL", number);
    private static string FlexValEntryUid(string flexNumber, string valueCode) => SeedUid("FLEXVALENTRY", $"{flexNumber}_{valueCode}");

    public async Task SeedAsync()
    {
        await SeedBaseDataGroupsAsync();
        await SeedUnitGroupsAsync();
        await SeedUnitsAsync();
        await SeedStockStatusAsync();
        await SeedFlexValuesAsync();
        await SeedWarehousesAsync();
        await SeedStockPlacesAsync();
        await SeedCurrenciesAsync();
        await SeedDepartmentsAsync();
        await SeedCustomersAsync();
        await SeedSuppliersAsync();
        await SeedCostCentersAsync();
        await SeedDutiesAsync();
        await SeedEmployeesAsync();
        await SeedMaterialsAsync();
        await SeedMaterialBarTypesAsync();
        await SeedAssistantDataAsync();
    }

    #region 基础资料分组种子数据

    private async Task SeedBaseDataGroupsAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        var groups = new List<SysBaseDataGroup>
        {
            // ═══════════════════════════════════════════════════════
            // 物料分组（Fprgkey = "Material"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("MAT",       "Material", "MAT",       "物料分组",     "",                    "MAT",                now, company, user),
            CreateGroup("MAT-ELC",   "Material", "MAT-ELC",   "电子元器件",   GroupUid("MAT"),       "MAT.MAT-ELC",        now, company, user),
            CreateGroup("MAT-MTL",   "Material", "MAT-MTL",   "金属材料",     GroupUid("MAT"),       "MAT.MAT-MTL",        now, company, user),
            CreateGroup("MAT-CHM",   "Material", "MAT-CHM",   "化工辅料",     GroupUid("MAT"),       "MAT.MAT-CHM",        now, company, user),
            CreateGroup("MAT-PCB",   "Material", "MAT-PCB",   "PCB板材",      GroupUid("MAT"),       "MAT.MAT-PCB",        now, company, user),
            CreateGroup("MAT-SFP",   "Material", "MAT-SFP",   "半成品",       GroupUid("MAT"),       "MAT.MAT-SFP",        now, company, user),
            CreateGroup("MAT-FG",    "Material", "MAT-FG",    "成品",         GroupUid("MAT"),       "MAT.MAT-FG",         now, company, user),
            CreateGroup("MAT-PKG",   "Material", "MAT-PKG",   "包装材料",     GroupUid("MAT"),       "MAT.MAT-PKG",        now, company, user),

            // ═══════════════════════════════════════════════════════
            // 客户分组（Fprgkey = "Customer"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("CUS",       "Customer", "CUS",       "客户分组",     "",                    "CUS",                now, company, user),
            CreateGroup("CUS-DOM",   "Customer", "CUS-DOM",   "国内客户",     GroupUid("CUS"),       "CUS.CUS-DOM",        now, company, user),
            CreateGroup("CUS-OVS",   "Customer", "CUS-OVS",   "海外客户",     GroupUid("CUS"),       "CUS.CUS-OVS",        now, company, user),
            CreateGroup("CUS-VIP",   "Customer", "CUS-VIP",   "VIP客户",      GroupUid("CUS-DOM"),   "CUS.CUS-DOM.CUS-VIP",now, company, user),
            CreateGroup("CUS-REG",   "Customer", "CUS-REG",   "普通客户",     GroupUid("CUS-DOM"),   "CUS.CUS-DOM.CUS-REG",now, company, user),

            // ═══════════════════════════════════════════════════════
            // 供应商分组（Fprgkey = "Supplier"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("SUP",       "Supplier", "SUP",       "供应商分组",   "",                    "SUP",                now, company, user),
            CreateGroup("SUP-RAW",   "Supplier", "SUP-RAW",   "原材料供应商", GroupUid("SUP"),       "SUP.SUP-RAW",        now, company, user),
            CreateGroup("SUP-ELC",   "Supplier", "SUP-ELC",   "电子元器件供应商", GroupUid("SUP"),   "SUP.SUP-ELC",        now, company, user),
            CreateGroup("SUP-PKG",   "Supplier", "SUP-PKG",   "包装材料供应商", GroupUid("SUP"),     "SUP.SUP-PKG",        now, company, user),
            CreateGroup("SUP-EQP",   "Supplier", "SUP-EQP",   "设备工具供应商", GroupUid("SUP"),     "SUP.SUP-EQP",        now, company, user),
            CreateGroup("SUP-OVS",   "Supplier", "SUP-OVS",   "海外供应商",   GroupUid("SUP"),       "SUP.SUP-OVS",        now, company, user),

            // ═══════════════════════════════════════════════════════
            // 仓库分组（Fprgkey = "Warehouse"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("WH",        "Warehouse", "WH",       "仓库分组",     "",                    "WH",                 now, company, user),
            CreateGroup("WH-RM",     "Warehouse", "WH-RM",    "原材料仓",     GroupUid("WH"),        "WH.WH-RM",           now, company, user),
            CreateGroup("WH-WIP",    "Warehouse", "WH-WIP",   "半成品仓",     GroupUid("WH"),        "WH.WH-WIP",          now, company, user),
            CreateGroup("WH-FG",     "Warehouse", "WH-FG",    "成品仓",       GroupUid("WH"),        "WH.WH-FG",           now, company, user),
            CreateGroup("WH-PKG",    "Warehouse", "WH-PKG",   "包材仓",       GroupUid("WH"),        "WH.WH-PKG",          now, company, user),
            CreateGroup("WH-RT",     "Warehouse", "WH-RT",    "退货仓",       GroupUid("WH"),        "WH.WH-RT",           now, company, user),
            CreateGroup("WH-VT",     "Warehouse", "WH-VT",    "虚拟仓",       GroupUid("WH"),        "WH.WH-VT",           now, company, user),

            // ═══════════════════════════════════════════════════════
            // 仓位分组（Fprgkey = "StockPlace"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("SP",        "StockPlace", "SP",       "仓位分组",     "",                    "SP",                 now, company, user),
            CreateGroup("SP-STORE",  "StockPlace", "SP-STORE", "存储区",       GroupUid("SP"),        "SP.SP-STORE",        now, company, user),
            CreateGroup("SP-TEMP",   "StockPlace", "SP-TEMP",  "暂存区",       GroupUid("SP"),        "SP.SP-TEMP",         now, company, user),
            CreateGroup("SP-SHIP",   "StockPlace", "SP-SHIP",  "发货区",       GroupUid("SP"),        "SP.SP-SHIP",         now, company, user),
            CreateGroup("SP-INSP",   "StockPlace", "SP-INSP",  "待检区",       GroupUid("SP"),        "SP.SP-INSP",         now, company, user),
            CreateGroup("SP-HAZ",    "StockPlace", "SP-HAZ",   "危化品区",     GroupUid("SP"),        "SP.SP-HAZ",          now, company, user),

            // ═══════════════════════════════════════════════════════
            // 币种分组（Fprgkey = "Currency"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("CUR",       "Currency", "CUR",        "币种分组",     "",                    "CUR",                now, company, user),
            CreateGroup("CUR-BASE",  "Currency", "CUR-BASE",   "本位币",       GroupUid("CUR"),       "CUR.CUR-BASE",       now, company, user),
            CreateGroup("CUR-INTL",  "Currency", "CUR-INTL",   "国际货币",     GroupUid("CUR"),       "CUR.CUR-INTL",       now, company, user),
            CreateGroup("CUR-APAC",  "Currency", "CUR-APAC",   "亚太货币",     GroupUid("CUR"),       "CUR.CUR-APAC",       now, company, user),
            CreateGroup("CUR-OTHER", "Currency", "CUR-OTHER",  "其他货币",     GroupUid("CUR"),       "CUR.CUR-OTHER",      now, company, user),

            // ═══════════════════════════════════════════════════════
            // 单位分组（Fprgkey = "Unit"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("UNT",       "Unit", "UNT",            "单位分组",     "",                    "UNT",                now, company, user),
            CreateGroup("UNT-WGT",   "Unit", "UNT-WGT",        "重量单位",     GroupUid("UNT"),       "UNT.UNT-WGT",        now, company, user),
            CreateGroup("UNT-LEN",   "Unit", "UNT-LEN",        "长度单位",     GroupUid("UNT"),       "UNT.UNT-LEN",        now, company, user),
            CreateGroup("UNT-QTY",   "Unit", "UNT-QTY",        "数量单位",     GroupUid("UNT"),       "UNT.UNT-QTY",        now, company, user),
            CreateGroup("UNT-VOL",   "Unit", "UNT-VOL",        "体积单位",     GroupUid("UNT"),       "UNT.UNT-VOL",        now, company, user),
            CreateGroup("UNT-AREA",  "Unit", "UNT-AREA",       "面积单位",     GroupUid("UNT"),       "UNT.UNT-AREA",       now, company, user),
            CreateGroup("UNT-TIME",  "Unit", "UNT-TIME",       "时间单位",     GroupUid("UNT"),       "UNT.UNT-TIME",       now, company, user),

            // ═══════════════════════════════════════════════════════
            // 部门分组（Fprgkey = "Department"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("DEPT",      "Department", "DEPT",     "部门分组",     "",                    "DEPT",               now, company, user),
            CreateGroup("DEPT-MGT",  "Department", "DEPT-MGT", "管理部门",     GroupUid("DEPT"),      "DEPT.DEPT-MGT",      now, company, user),
            CreateGroup("DEPT-PRD",  "Department", "DEPT-PRD", "生产部门",     GroupUid("DEPT"),      "DEPT.DEPT-PRD",      now, company, user),
            CreateGroup("DEPT-RD",   "Department", "DEPT-RD",  "研发部门",     GroupUid("DEPT"),      "DEPT.DEPT-RD",       now, company, user),
            CreateGroup("DEPT-SAL",  "Department", "DEPT-SAL", "销售部门",     GroupUid("DEPT"),      "DEPT.DEPT-SAL",      now, company, user),
            CreateGroup("DEPT-FIN",  "Department", "DEPT-FIN", "财务部门",     GroupUid("DEPT"),      "DEPT.DEPT-FIN",      now, company, user),
            CreateGroup("DEPT-WH",   "Department", "DEPT-WH",  "仓储部门",     GroupUid("DEPT"),      "DEPT.DEPT-WH",       now, company, user),
            CreateGroup("DEPT-QC",   "Department", "DEPT-QC",  "质量部门",     GroupUid("DEPT"),      "DEPT.DEPT-QC",       now, company, user),
            CreateGroup("DEPT-PUR",  "Department", "DEPT-PUR", "采购部门",     GroupUid("DEPT"),      "DEPT.DEPT-PUR",      now, company, user),

            // ═══════════════════════════════════════════════════════
            // 职员分组（Fprgkey = "Employee"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("EMP",       "Employee", "EMP",        "职员分组",     "",                    "EMP",                now, company, user),
            CreateGroup("EMP-MGT",   "Employee", "EMP-MGT",    "管理人员",     GroupUid("EMP"),       "EMP.EMP-MGT",        now, company, user),
            CreateGroup("EMP-TECH",  "Employee", "EMP-TECH",   "技术人员",     GroupUid("EMP"),       "EMP.EMP-TECH",       now, company, user),
            CreateGroup("EMP-PROD",  "Employee", "EMP-PROD",   "生产人员",     GroupUid("EMP"),       "EMP.EMP-PROD",       now, company, user),
            CreateGroup("EMP-SALE",  "Employee", "EMP-SALE",   "销售人员",     GroupUid("EMP"),       "EMP.EMP-SALE",       now, company, user),
            CreateGroup("EMP-FIN",   "Employee", "EMP-FIN",    "财务人员",     GroupUid("EMP"),       "EMP.EMP-FIN",        now, company, user),
            CreateGroup("EMP-WH",    "Employee", "EMP-WH",     "仓储人员",     GroupUid("EMP"),       "EMP.EMP-WH",         now, company, user),

            // ═══════════════════════════════════════════════════════
            // 物料条码类型分组（Fprgkey = "MaterialBarType"）
            // ═══════════════════════════════════════════════════════
            CreateGroup("MBT",       "MaterialBarType", "MBT",       "条码类型分组",   "",                  "MBT",                now, company, user),
            CreateGroup("MBT-SINGLE","MaterialBarType", "MBT-SINGLE","单品条码",       GroupUid("MBT"),     "MBT.MBT-SINGLE",     now, company, user),
            CreateGroup("MBT-PKG",   "MaterialBarType", "MBT-PKG",   "最小包装量条码", GroupUid("MBT"),     "MBT.MBT-PKG",        now, company, user),
            CreateGroup("MBT-BATCH", "MaterialBarType", "MBT-BATCH", "批次条码",       GroupUid("MBT"),     "MBT.MBT-BATCH",      now, company, user),
        };

        var existingNumbers = await _db.Queryable<SysBaseDataGroup>()
            .Where(g => !g.FDeleted)
            .Select(g => new { g.Fprgkey, g.Fnumber })
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers.Select(e => $"{e.Fprgkey}_{e.Fnumber}"));
        var missing = groups.Where(g => !existingSet.Contains($"{g.Fprgkey}_{g.Fnumber}")).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static SysBaseDataGroup CreateGroup(
        string number, string prgkey, string fnumber, string name,
        string parentId, string fullNumber,
        DateTime now, string company, string user)
    {
        return new SysBaseDataGroup
        {
            Uid = GroupUid(number),
            FInterId = $"GRP_{prgkey}_{number}",
            Fprgkey = prgkey,
            Fnumber = fnumber,
            Fname = name,
            Fcname = name,
            Ftname = "",
            Fename = "",
            Fparentid = parentId,
            Ffullnumber = fullNumber,
            Fnote = "",
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    // 单位组（T_BD_UNITGROUP）：所属分组下拉来源；FInterId=代码，与单位 FUNITGROUPID 对应；含基准单位
    private async Task SeedUnitGroupsAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        var groups = new List<TBdUnitgroup>
        {
            CreateUnitGroup("UG_WEIGHT", "重量组", "KG",  "千克",  now, company, user),
            CreateUnitGroup("UG_LENGTH", "长度组", "M",   "米",    now, company, user),
            CreateUnitGroup("UG_QTY",    "数量组", "PCS", "个",    now, company, user),
            CreateUnitGroup("UG_TIME",   "时间组", "HR",  "小时",  now, company, user),
            CreateUnitGroup("UG_VOLUME", "体积组", "L",   "升",    now, company, user),
            CreateUnitGroup("UG_AREA",   "面积组", "M2",  "平方米", now, company, user),
        };

        var existing = new HashSet<string>(
            await _db.Queryable<TBdUnitgroup>().Where(g => !g.FDeleted).Select(g => g.Fnumber).ToListAsync());
        var missing = groups.Where(g => !existing.Contains(g.Fnumber)).ToList();
        if (missing.Count > 0)
            await _db.Insertable(missing).ExecuteCommandAsync();
    }

    private static TBdUnitgroup CreateUnitGroup(
        string code, string name, string baseNumber, string baseName,
        DateTime now, string company, string user)
    {
        return new TBdUnitgroup
        {
            Uid = code,
            FInterId = code,
            Fnumber = code,
            Fname = name,
            Fbaseunitnumber = baseNumber,
            Fbaseunitname = baseName,
            Fcheckdate = DateTime.MinValue,
            Fdisabledate = DateTime.MinValue,
            FStatus = 0,
            FDeleted = false,
            FDisabled = false,
            FCompanyId = company,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    private async Task SeedUnitsAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 分组 Uid 引用
        var grpWgt  = GroupUid("UNT-WGT"); var grpLen = GroupUid("UNT-LEN");
        var grpQty  = GroupUid("UNT-QTY"); var grpVol = GroupUid("UNT-VOL");
        var grpArea = GroupUid("UNT-AREA"); var grpTime = GroupUid("UNT-TIME");

        var units = new List<TBdUnit>
        {
            // 重量单位组
            CreateUnit("KG",  "千克", "重量基本单位",       "UG_WEIGHT", true,  3, "1", "固定换算", 1m,      now, company, user, grpWgt),
            CreateUnit("G",   "克",   "千分之一千克",       "UG_WEIGHT", false, 3, "1", "固定换算", 0.001m,  now, company, user, grpWgt),
            CreateUnit("T",   "吨",   "一千千克",           "UG_WEIGHT", false, 3, "1", "固定换算", 1000m,   now, company, user, grpWgt),
            CreateUnit("MG",  "毫克", "百万分之一千克",     "UG_WEIGHT", false, 6, "1", "固定换算", 0.000001m, now, company, user, grpWgt),
            CreateUnit("LB",  "磅",   "英制重量单位",       "UG_WEIGHT", false, 3, "1", "固定换算", 0.4536m, now, company, user, grpWgt),
            CreateUnit("OZ",  "盎司", "英制重量单位",       "UG_WEIGHT", false, 3, "1", "固定换算", 0.02835m, now, company, user, grpWgt),

            // 长度单位组
            CreateUnit("M",   "米",   "长度基本单位",       "UG_LENGTH", true,  3, "1", "固定换算", 1m,      now, company, user, grpLen),
            CreateUnit("CM",  "厘米", "百分之一米",         "UG_LENGTH", false, 3, "1", "固定换算", 0.01m,   now, company, user, grpLen),
            CreateUnit("MM",  "毫米", "千分之一米",         "UG_LENGTH", false, 3, "1", "固定换算", 0.001m,  now, company, user, grpLen),
            CreateUnit("KM",  "千米", "一千米",             "UG_LENGTH", false, 3, "1", "固定换算", 1000m,   now, company, user, grpLen),
            CreateUnit("IN",  "英寸", "英制长度单位",       "UG_LENGTH", false, 3, "1", "固定换算", 0.0254m, now, company, user, grpLen),
            CreateUnit("FT",  "英尺", "英制长度单位",       "UG_LENGTH", false, 3, "1", "固定换算", 0.3048m, now, company, user, grpLen),

            // 数量单位组
            CreateUnit("PCS", "个",   "数量基本单位",       "UG_QTY",    true,  0, "1", "固定换算", 1m,      now, company, user, grpQty),
            CreateUnit("SET", "套",   "成套计量",           "UG_QTY",    false, 0, "1", "固定换算", 1m,      now, company, user, grpQty),
            CreateUnit("PR",  "双",   "成对计量",           "UG_QTY",    false, 0, "1", "固定换算", 2m,      now, company, user, grpQty),
            CreateUnit("DOZ", "打",   "十二个为一打",       "UG_QTY",    false, 0, "1", "固定换算", 12m,     now, company, user, grpQty),
            CreateUnit("BOX", "箱",   "包装箱",             "UG_QTY",    false, 0, "1", "固定换算", 1m,      now, company, user, grpQty),
            CreateUnit("BAG", "袋",   "包装袋",             "UG_QTY",    false, 0, "1", "固定换算", 1m,      now, company, user, grpQty),
            CreateUnit("PKG", "包",   "包装单位",           "UG_QTY",    false, 0, "1", "固定换算", 1m,      now, company, user, grpQty),
            CreateUnit("PLT", "托盘", "托盘计量",           "UG_QTY",    false, 0, "1", "固定换算", 1m,      now, company, user, grpQty),
            CreateUnit("CTN", "纸箱", "纸箱包装",           "UG_QTY",    false, 0, "1", "固定换算", 1m,      now, company, user, grpQty),
            CreateUnit("ROL", "卷",   "卷装计量",           "UG_QTY",    false, 0, "1", "固定换算", 1m,      now, company, user, grpQty),

            // 体积单位组
            CreateUnit("L",   "升",   "体积基本单位",       "UG_VOLUME", true,  3, "1", "固定换算", 1m,      now, company, user, grpVol),
            CreateUnit("ML",  "毫升", "千分之一升",         "UG_VOLUME", false, 3, "1", "固定换算", 0.001m,  now, company, user, grpVol),
            CreateUnit("M3",  "立方米","一千升",            "UG_VOLUME", false, 3, "1", "固定换算", 1000m,   now, company, user, grpVol),
            CreateUnit("GAL", "加仑", "英制体积单位",       "UG_VOLUME", false, 3, "1", "固定换算", 3.7854m, now, company, user, grpVol),

            // 面积单位组
            CreateUnit("M2",  "平方米", "面积基本单位",     "UG_AREA",   true,  3, "1", "固定换算", 1m,      now, company, user, grpArea),
            CreateUnit("CM2", "平方厘米","万分之一平方米",   "UG_AREA",   false, 3, "1", "固定换算", 0.0001m, now, company, user, grpArea),
            CreateUnit("KM2", "平方千米","一百万平方米",     "UG_AREA",   false, 3, "1", "固定换算", 1000000m, now, company, user, grpArea),

            // 时间单位组
            CreateUnit("HR",  "小时", "时间基本单位",       "UG_TIME",   true,  2, "1", "固定换算", 1m,      now, company, user, grpTime),
            CreateUnit("MIN", "分钟", "六十分之一小时",     "UG_TIME",   false, 2, "1", "固定换算", 1m / 60m, now, company, user, grpTime),
            CreateUnit("DAY", "天",   "二十四小时",         "UG_TIME",   false, 2, "1", "固定换算", 24m,     now, company, user, grpTime),
        };

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdUnit>()
            .Where(u => !u.FDeleted)
            .Select(u => u.FNumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = units.Where(u => !existingSet.Contains(u.FNumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static TBdUnit CreateUnit(
        string number, string name, string description,
        string unitGroupId, bool isBase, int precision,
        string roundType, string convertType, decimal coefficient,
        DateTime now, string company, string user, string groupId = "")
    {
        return new TBdUnit
        {
            Uid = UnitUid(number),
            FInterId = $"UNIT_{number}",
            FNumber = number,
            FName = name,
            FDescription = description,
            FUnitGroupId = unitGroupId,
            FIsBaseUnit = isBase,
            FPrecision = precision,
            FRoundType = roundType,
            FConvertType = convertType,
            FCoefficient = coefficient,
            FGroupId = groupId,
            FUseOrgId = company,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #region 仓库种子数据

    #region 库存状态种子数据

    private async Task SeedStockStatusAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 库存状态来源：T_BD_STOCKSTATUS（FINTERID / FNUMBER / FNAME），用于默认库存状态、默认收料状态等下拉
        var statuses = new List<TBdStockstatus>
        {
            CreateStockStatus("10000",  "KCZT01_SYS", "可用",     now, company, user),
            CreateStockStatus("10001",  "KCZT02_SYS", "待检",     now, company, user),
            CreateStockStatus("10002",  "KCZT03_SYS", "冻结",     now, company, user),
            CreateStockStatus("10003",  "KCZT04_SYS", "退回冻结", now, company, user),
            CreateStockStatus("10004",  "KCZT05_SYS", "在途",     now, company, user),
            CreateStockStatus("10005",  "KCZT06_SYS", "收货冻结", now, company, user),
            CreateStockStatus("10006",  "KCZT07_SYS", "废品",     now, company, user),
            CreateStockStatus("10257",  "KCZT08_SYS", "不良",     now, company, user),
            CreateStockStatus("103401", "KCZT99_SYS", "外借",     now, company, user),
        };

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdStockstatus>()
            .Where(s => !s.FDeleted)
            .Select(s => s.Fnumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = statuses.Where(s => !existingSet.Contains(s.Fnumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static TBdStockstatus CreateStockStatus(
        string interId, string number, string name,
        DateTime now, string company, string user)
    {
        return new TBdStockstatus
        {
            Uid = StockStatusUid(number),
            FInterId = interId,
            Fnumber = number,
            Fname = name,
            Favailable = true,
            Isdefault = true,
            // NOT NULL 日期列需赋 DateTime.MinValue（SQLite 该列不可空）
            Fcheckdate = DateTime.MinValue,
            Fdisabledate = DateTime.MinValue,
            FStatus = 0,
            FDeleted = false,
            FDisabled = false,
            FCompanyId = company,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 仓位集（仓位值集）主数据种子

    private async Task SeedFlexValuesAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 仓位集（T_BAS_FLEXVALUES）头 + 仓位集值（T_BAS_FLEXVALUESENTRY）明细
        // 明细 FINTERID 关联其所属仓位集的 FINTERID；明细 FDETAILID 为仓位集值的关联键（被 STOCKFLEXDETAIL.FFLEXENTRYID 引用）
        var sets = new[]
        {
            (Number: "AQKQ", Name: "A区库区", Prefix: "A1-", Count: 20),
            (Number: "BQKQ", Name: "B区库区", Prefix: "B1-", Count: 10),
        };

        var heads = new List<TBasFlexvalues>();
        var entries = new List<TBasFlexvaluesentry>();
        foreach (var s in sets)
        {
            var headInterId = FlexValUid(s.Number);
            heads.Add(new TBasFlexvalues
            {
                Uid = headInterId,
                FInterId = headInterId,
                Fnumber = s.Number,
                Fname = s.Name,
                Fflexnumber = s.Number,
                Fflextypeid = "仓位",
                FCheckDate = DateTime.MinValue,
                Fdisabledate = DateTime.MinValue,
                FStatus = 0,
                FDeleted = false,
                FDisabled = false,
                FCompanyId = company,
                CYmd = now,
                CUser = user,
                MYmd = now,
                MUser = user
            });

            for (var n = 1; n <= s.Count; n++)
            {
                var code = $"{s.Prefix}{n}";
                entries.Add(new TBasFlexvaluesentry
                {
                    Uid = FlexValEntryUid(s.Number, code),
                    FInterId = headInterId,
                    Fdetailid = FlexValEntryUid(s.Number, code),
                    Fentryid = n,
                    Fnumber = code,
                    Fname = code,
                    Fforbid = false,
                    FStatus = 0,
                    FDeleted = false,
                    FDisabled = false,
                    FCompanyId = company,
                    CYmd = now,
                    CUser = user,
                    MYmd = now,
                    MUser = user
                });
            }
        }

        // 幂等：按 FNumber 判断仓位集头是否已存在
        var existingHeadNumbers = new HashSet<string>(
            await _db.Queryable<TBasFlexvalues>().Where(v => !v.FDeleted).Select(v => v.Fnumber).ToListAsync());
        var missingHeads = heads.Where(h => !existingHeadNumbers.Contains(h.Fnumber)).ToList();
        if (missingHeads.Count > 0)
        {
            // 头与明细原子插入：避免中途失败留下"有头无值"的孤立仓位集（幂等键只判头 FNumber，孤立头将无法自愈）
            var newHeadIds = new HashSet<string>(missingHeads.Select(h => h.FInterId));
            var missingEntries = entries.Where(e => newHeadIds.Contains(e.FInterId)).ToList();
            try
            {
                _db.Ado.BeginTran();
                await _db.Insertable(missingHeads).ExecuteCommandAsync();
                if (missingEntries.Count > 0)
                    await _db.Insertable(missingEntries).ExecuteCommandAsync();
                _db.Ado.CommitTran();
            }
            catch
            {
                _db.Ado.RollbackTran();
                throw;
            }
        }
    }

    #endregion

    private async Task SeedWarehousesAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 分组 Uid 引用
        var grpWhRm  = GroupUid("WH-RM");  var grpWhWip = GroupUid("WH-WIP");
        var grpWhFg  = GroupUid("WH-FG");  var grpWhPkg = GroupUid("WH-PKG");
        var grpWhRt  = GroupUid("WH-RT");  var grpWhVt  = GroupUid("WH-VT");

        // 仓库类型 type：1=物料类;2=TPM类  仓库属性 property：1=普通仓库;2=车间仓库;3=供应商仓库;4=客户仓库;5=第三方仓储
        var warehouses = new List<TBdStock>
        {
            // 原材料仓库
            CreateWarehouse("WH-RM01", "原材料仓库A", "存放金属、塑料等原材料",     "张伟",   "13800001001", "1", "1", true,  false, false, "深圳市宝安区工业园A栋1层", now, company, user, grpWhRm),
            CreateWarehouse("WH-RM02", "原材料仓库B", "存放化工、橡胶等原材料",     "李强",   "13800001002", "1", "1", true,  false, false, "深圳市宝安区工业园A栋2层", now, company, user, grpWhRm),

            // 半成品仓库
            CreateWarehouse("WH-WIP01", "半成品仓库",  "存放生产线在制品及半成品",   "王芳",   "13800001003", "1", "1", true,  false, false, "深圳市宝安区工业园B栋1层", now, company, user, grpWhWip),

            // 成品仓库
            CreateWarehouse("WH-FG01", "成品仓库A",   "存放已完工待发货的成品",     "赵敏",   "13800001004", "1", "1", true,  false, false, "深圳市宝安区工业园C栋1层", now, company, user, grpWhFg),
            CreateWarehouse("WH-FG02", "成品仓库B",   "大件成品及出口产品存放",     "刘洋",   "13800001005", "1", "1", true,  false, false, "深圳市宝安区工业园C栋2层", now, company, user, grpWhFg),

            // 包材仓库
            CreateWarehouse("WH-PK01", "包材仓库",    "存放纸箱、泡沫、标签等包材", "陈静",   "13800001006", "1", "1", false, false, false, "深圳市宝安区工业园D栋1层", now, company, user, grpWhPkg),

            // 退货仓库
            CreateWarehouse("WH-RT01", "退货仓库",    "存放客户退回的产品",         "孙磊",   "13800001007", "1", "1", false, true,  false, "深圳市宝安区工业园E栋1层", now, company, user, grpWhRt),

            // 不良品仓库
            CreateWarehouse("WH-NG01", "不良品仓库",  "存放质检不合格的物料和产品", "周杰",   "13800001008", "1", "1", false, true,  false, "深圳市宝安区工业园E栋2层", now, company, user, grpWhRt),

            // 外租仓库（第三方仓储）
            CreateWarehouse("WH-EX01", "外租仓库",    "第三方物流外租仓库",         "吴涛",   "13800001009", "1", "5", true,  false, false, "东莞市虎门镇物流园3号库",  now, company, user, grpWhFg),

            // 虚拟仓库
            CreateWarehouse("WH-VT01", "在途虚拟仓",  "记录在途物料的虚拟仓库",     "郑华",   "13800001010", "1", "1", false, false, true,  "",                         now, company, user, grpWhVt),
        };

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdStock>()
            .Where(w => !w.FDeleted)
            .Select(w => w.FNumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = warehouses.Where(w => !existingSet.Contains(w.FNumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static TBdStock CreateWarehouse(
        string number, string name, string description,
        string principal, string tel, string type, string property,
        bool isOpenLocation, bool allowMinusQty, bool isVirtual, string address,
        DateTime now, string company, string user, string groupId = "")
    {
        return new TBdStock
        {
            Uid = StockUid(number),
            FInterId = $"STK_{number}",
            FNumber = number,
            FName = name,
            FDescription = description,
            FPrincipal = principal,
            FTel = tel,
            FType = type,
            FStockProperty = property,
            // 默认库存状态相关：库存状态类型默认“可用(0)”，默认库存/收料状态指向库存状态表“可用(KCZT01_SYS)”
            FStockStatusType = "0",
            FDefStockStatusId = StockStatusUid("KCZT01_SYS"),
            FDefReceiveStatusId = StockStatusUid("KCZT01_SYS"),
            FIsOpenLocation = isOpenLocation,
            FAllowMinusQty = allowMinusQty,
            FIsVirtual = isVirtual,
            FAddress = address,
            FAllowMrpPlan = !isVirtual,
            FAllowLock = true,
            FAvailableAlert = !isVirtual,
            FAvailablePicking = isOpenLocation,
            FSortingPriority = 0,
            FGroupId = groupId,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 仓位种子数据

    private async Task SeedStockPlacesAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        var places = new List<TBdStockPlace>();

        // 分组 Uid 引用
        var grpSpStore = GroupUid("SP-STORE"); var grpSpTemp = GroupUid("SP-TEMP");
        var grpSpShip  = GroupUid("SP-SHIP");  var grpSpInsp = GroupUid("SP-INSP");
        var grpSpHaz   = GroupUid("SP-HAZ");

        // 原材料仓库A - 按区域/货架/层划分
        AddStockPlaces(places, "WH-RM01", "RM01", "原材料A", new[]
        {
            ("A01-01", "A区1排1层", "存储区", true,  500m),
            ("A01-02", "A区1排2层", "存储区", true,  500m),
            ("A01-03", "A区1排3层", "存储区", false, 300m),
            ("A02-01", "A区2排1层", "存储区", true,  500m),
            ("A02-02", "A区2排2层", "存储区", true,  500m),
            ("A02-03", "A区2排3层", "存储区", false, 300m),
            ("B01-01", "B区1排1层", "存储区", true,  800m),
            ("B01-02", "B区1排2层", "存储区", true,  800m),
            ("B02-01", "B区2排1层", "存储区", true,  800m),
            ("B02-02", "B区2排2层", "存储区", true,  800m),
            ("RCV-01", "收货暂存区1", "暂存区", true, 1000m),
            ("RCV-02", "收货暂存区2", "暂存区", true, 1000m),
        }, now, company, user, grpSpStore);

        // 原材料仓库B
        AddStockPlaces(places, "WH-RM02", "RM02", "原材料B", new[]
        {
            ("C01-01", "C区1排1层", "存储区", false, 400m),
            ("C01-02", "C区1排2层", "存储区", false, 400m),
            ("C02-01", "C区2排1层", "存储区", false, 400m),
            ("C02-02", "C区2排2层", "存储区", false, 400m),
            ("D01-01", "D区1排1层", "危化品区", false, 200m),
            ("D01-02", "D区1排2层", "危化品区", false, 200m),
        }, now, company, user, grpSpHaz);

        // 成品仓库A - 按通道/货架划分
        AddStockPlaces(places, "WH-FG01", "FG01", "成品A", new[]
        {
            ("P01-01", "1号通道1层", "存储区", true,  600m),
            ("P01-02", "1号通道2层", "存储区", true,  600m),
            ("P01-03", "1号通道3层", "存储区", false, 400m),
            ("P02-01", "2号通道1层", "存储区", true,  600m),
            ("P02-02", "2号通道2层", "存储区", true,  600m),
            ("P02-03", "2号通道3层", "存储区", false, 400m),
            ("P03-01", "3号通道1层", "存储区", true,  600m),
            ("P03-02", "3号通道2层", "存储区", true,  600m),
            ("SHP-01", "发货区1",   "发货区", true,  2000m),
            ("SHP-02", "发货区2",   "发货区", true,  2000m),
        }, now, company, user, grpSpStore);

        // 成品仓库B
        AddStockPlaces(places, "WH-FG02", "FG02", "成品B", new[]
        {
            ("L01-01", "大件区1排1层", "大件区", true,  1000m),
            ("L01-02", "大件区1排2层", "大件区", true,  1000m),
            ("L02-01", "大件区2排1层", "大件区", true,  1000m),
            ("L02-02", "大件区2排2层", "大件区", true,  1000m),
            ("EXP-01", "出口待检区",   "待检区", false, 1500m),
            ("EXP-02", "出口发货区",   "发货区", true,  2000m),
        }, now, company, user, grpSpStore);

        // 半成品仓库
        AddStockPlaces(places, "WH-WIP01", "WIP01", "半成品", new[]
        {
            ("W01-01", "1号线暂存区", "暂存区", true, 500m),
            ("W01-02", "2号线暂存区", "暂存区", true, 500m),
            ("W01-03", "3号线暂存区", "暂存区", true, 500m),
            ("W02-01", "半成品货架1层", "存储区", true, 400m),
            ("W02-02", "半成品货架2层", "存储区", true, 400m),
        }, now, company, user, grpSpTemp);

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdStockPlace>()
            .Where(sp => !sp.FDeleted)
            .Select(sp => sp.FNumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = places.Where(sp => !existingSet.Contains(sp.FNumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static void AddStockPlaces(
        List<TBdStockPlace> places, string stockNumber, string prefix, string stockName,
        (string code, string name, string property, bool allowMix, decimal maxCapacity)[] items,
        DateTime now, string company, string user, string groupId = "")
    {
        foreach (var (code, name, property, allowMix, maxCapacity) in items)
        {
            var number = $"{prefix}-{code}";
            places.Add(new TBdStockPlace
            {
                Uid = SpUid(number),
                FInterId = $"SP_{number}",
                FNumber = number,
                FName = $"{stockName}-{name}",
                FDescription = $"{stockName}仓库 {name}",
                FStockId = StockUid(stockNumber),
                FSpProperty = property,
                FAllowMix = allowMix,
                FMaxCapacity = maxCapacity,
                FGroupId = groupId,
                FCompanyId = company,
                FStatus = 0,
                FDeleted = false,
                CYmd = now,
                CUser = user,
                MYmd = now,
                MUser = user
            });
        }
    }

    #endregion

    #region 币种种子数据

    private async Task SeedCurrenciesAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 分组 Uid 引用
        var grpCurBase  = GroupUid("CUR-BASE"); var grpCurIntl = GroupUid("CUR-INTL");
        var grpCurApac  = GroupUid("CUR-APAC"); var grpCurOther = GroupUid("CUR-OTHER");

        var currencies = new List<TBdCurrency>
        {
            // 本位币
            CreateCurrency("CNY", "CNY", "人民币",     "中华人民共和国法定货币", 2, 2, 1, 1m,        now, company, user, grpCurBase),

            // 主要国际货币
            CreateCurrency("USD", "USD", "美元",       "美利坚合众国法定货币",   4, 2, 0, 7.2450m,   now, company, user, grpCurIntl),
            CreateCurrency("EUR", "EUR", "欧元",       "欧元区法定货币",         4, 2, 0, 7.8930m,   now, company, user, grpCurIntl),
            CreateCurrency("GBP", "GBP", "英镑",       "大不列颠及北爱尔兰联合王国法定货币", 4, 2, 0, 9.1560m, now, company, user, grpCurIntl),
            CreateCurrency("JPY", "JPY", "日元",       "日本法定货币",           4, 0, 0, 0.0483m,   now, company, user, grpCurApac),

            // 亚太货币
            CreateCurrency("HKD", "HKD", "港币",       "中国香港法定货币",       4, 2, 0, 0.9280m,   now, company, user, grpCurApac),
            CreateCurrency("TWD", "TWD", "新台币",     "中国台湾地区通用货币",   4, 2, 0, 0.2230m,   now, company, user, grpCurApac),
            CreateCurrency("KRW", "KRW", "韩元",       "大韩民国法定货币",       4, 0, 0, 0.0053m,   now, company, user, grpCurApac),
            CreateCurrency("SGD", "SGD", "新加坡元",   "新加坡共和国法定货币",   4, 2, 0, 5.4120m,   now, company, user, grpCurApac),
            CreateCurrency("MYR", "MYR", "马来西亚林吉特", "马来西亚法定货币",   4, 2, 0, 1.6350m,   now, company, user, grpCurApac),
            CreateCurrency("THB", "THB", "泰铢",       "泰王国法定货币",         4, 2, 0, 0.2080m,   now, company, user, grpCurApac),
            CreateCurrency("VND", "VND", "越南盾",     "越南社会主义共和国法定货币", 4, 0, 0, 0.000289m, now, company, user, grpCurApac),
            CreateCurrency("INR", "INR", "印度卢比",   "印度共和国法定货币",     4, 2, 0, 0.0862m,   now, company, user, grpCurApac),
            CreateCurrency("AUD", "AUD", "澳大利亚元", "澳大利亚联邦法定货币",  4, 2, 0, 4.7250m,   now, company, user, grpCurApac),

            // 其他常用货币
            CreateCurrency("CAD", "CAD", "加拿大元",   "加拿大法定货币",         4, 2, 0, 5.2780m,   now, company, user, grpCurOther),
            CreateCurrency("CHF", "CHF", "瑞士法郎",   "瑞士联邦法定货币",       4, 2, 0, 8.1640m,   now, company, user, grpCurOther),
            CreateCurrency("RUB", "RUB", "俄罗斯卢布", "俄罗斯联邦法定货币",     4, 2, 0, 0.0790m,   now, company, user, grpCurOther),
            CreateCurrency("BRL", "BRL", "巴西雷亚尔", "巴西联邦共和国法定货币", 4, 2, 0, 1.2530m,   now, company, user, grpCurOther),
            CreateCurrency("MXN", "MXN", "墨西哥比索", "墨西哥合众国法定货币",   4, 2, 0, 0.3560m,   now, company, user, grpCurOther),
            CreateCurrency("AED", "AED", "阿联酋迪拉姆", "阿拉伯联合酋长国法定货币", 4, 2, 0, 1.9720m, now, company, user, grpCurOther),
        };

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdCurrency>()
            .Where(c => !c.FDeleted)
            .Select(c => c.FNumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = currencies.Where(c => !existingSet.Contains(c.FNumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static TBdCurrency CreateCurrency(
        string number, string code, string name, string description,
        int priceDigits, int amountDigits, int fixRate, decimal exchangeRate,
        DateTime now, string company, string user, string groupId = "")
    {
        return new TBdCurrency
        {
            Uid = Guid.NewGuid().ToString("N"),
            FInterId = $"CUR_{number}",
            FNumber = number,
            FCode = code,
            FName = name,
            FDescription = description,
            FPriceDigits = priceDigits,
            FAmountDigits = amountDigits,
            FFixRate = fixRate,
            FExchangeRate = exchangeRate,
            FGroupId = groupId,
            FUseOrgId = company,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 部门种子数据

    private async Task SeedDepartmentsAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        var depts = new List<TBdDepartment>
        {
            // ═══════════════════════════════════════════════════════
            // 第一级：集团
            // ═══════════════════════════════════════════════════════
            CreateDepartment(DeptUid("GRP"),             "GRP",             "欧普智能集团",       "集团总部",                                     "",                      "OPZNJT", "欧普智能集团",                                       "集团",     "张建国", false, false, now, company, user),

            // ═══════════════════════════════════════════════════════
            // 第二级：集团职能中心 + 子公司
            // ═══════════════════════════════════════════════════════
            // 集团职能中心
            CreateDepartment(DeptUid("GRP_OFFICE"),      "GRP_OFFICE",      "集团总裁办",         "集团战略决策与日常运营管理",                   DeptUid("GRP"),              "ZCBGS",  "欧普智能集团-集团总裁办",                             "管理部门", "张建国", false, false, now, company, user),
            CreateDepartment(DeptUid("GRP_FIN"),          "GRP_FIN",         "集团财务中心",       "集团财务核算、资金管理及税务筹划",             DeptUid("GRP"),              "JTCWZX", "欧普智能集团-集团财务中心",                           "财务部门", "杨秀芳", false, true,  now, company, user),
            CreateDepartment(DeptUid("GRP_HR"),           "GRP_HR",          "集团人力资源中心",   "集团人才战略、组织发展及薪酬绩效",             DeptUid("GRP"),              "JTRLZX", "欧普智能集团-集团人力资源中心",                       "管理部门", "李明华", false, false, now, company, user),
            CreateDepartment(DeptUid("GRP_IT"),           "GRP_IT",          "集团信息技术中心",   "集团信息化建设、系统运维及数字化转型",         DeptUid("GRP"),              "JTITB",  "欧普智能集团-集团信息技术中心",                       "管理部门", "李明华", false, false, now, company, user),
            CreateDepartment(DeptUid("GRP_AUDIT"),        "GRP_AUDIT",       "集团审计法务部",     "集团内部审计、合规管理及法律事务",             DeptUid("GRP"),              "JTSJB",  "欧普智能集团-集团审计法务部",                         "管理部门", "李明华", false, false, now, company, user),

            // 子公司
            CreateDepartment(DeptUid("SZ_MFG"),           "SZ_MFG",          "深圳制造有限公司",   "负责电子产品研发与生产制造",                   DeptUid("GRP"),              "SZZZ",   "欧普智能集团-深圳制造有限公司",                       "子公司",   "吴建华", false, false, now, company, user),
            CreateDepartment(DeptUid("SH_TRADE"),         "SH_TRADE",        "上海贸易有限公司",   "负责国内外贸易及供应链管理",                   DeptUid("GRP"),              "SHMY",   "欧普智能集团-上海贸易有限公司",                       "子公司",   "张明",   false, false, now, company, user),
            CreateDepartment(DeptUid("GZ_LOGISTICS"),     "GZ_LOGISTICS",    "广州物流有限公司",   "负责仓储物流及配送服务",                       DeptUid("GRP"),              "GZWL",   "欧普智能集团-广州物流有限公司",                       "子公司",   "张伟",   false, false, now, company, user),

            // ═══════════════════════════════════════════════════════
            // 第三级：子公司下属部门
            // ═══════════════════════════════════════════════════════

            // --- 深圳制造有限公司 ---
            CreateDepartment(DeptUid("SZ_GM"),            "SZ_GM",           "总经理办公室",       "深圳制造公司日常运营管理",                     DeptUid("SZ_MFG"),           "SZZJL",  "欧普智能集团-深圳制造有限公司-总经理办公室",          "管理部门", "吴建华", false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_RD"),            "SZ_RD",           "研发部",             "新产品研发、技术创新及工艺改进",               DeptUid("SZ_MFG"),           "SZYFB",  "欧普智能集团-深圳制造有限公司-研发部",                "研发部门", "李明华", false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_PROD"),          "SZ_PROD",         "生产部",             "产品生产制造及生产计划管理",                   DeptUid("SZ_MFG"),           "SZSCB",  "欧普智能集团-深圳制造有限公司-生产部",                "生产部门", "郑国强", true,  false, now, company, user),
            CreateDepartment(DeptUid("SZ_QC"),            "SZ_QC",           "品质部",             "来料检验、制程检验及成品检验",                 DeptUid("SZ_MFG"),           "SZPZB",  "欧普智能集团-深圳制造有限公司-品质部",                "质量部门", "黄志刚", false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_PURCHASE"),      "SZ_PURCHASE",     "采购部",             "原材料及设备采购、供应商管理",                 DeptUid("SZ_MFG"),           "SZCGB",  "欧普智能集团-深圳制造有限公司-采购部",                "采购部门", "陈志远", false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_WH"),            "SZ_WH",           "仓储部",             "原材料及成品仓库管理",                         DeptUid("SZ_MFG"),           "SZCCB",  "欧普智能集团-深圳制造有限公司-仓储部",                "仓储部门", "张伟",   false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_FIN"),           "SZ_FIN",          "财务部",             "深圳制造公司财务核算及成本管理",               DeptUid("SZ_MFG"),           "SZCWB",  "欧普智能集团-深圳制造有限公司-财务部",                "财务部门", "杨秀芳", false, true,  now, company, user),
            CreateDepartment(DeptUid("SZ_HR"),            "SZ_HR",           "人事行政部",         "深圳制造公司人事管理及行政后勤",               DeptUid("SZ_MFG"),           "SZRSB",  "欧普智能集团-深圳制造有限公司-人事行政部",            "管理部门", "李明华", false, false, now, company, user),

            // --- 上海贸易有限公司 ---
            CreateDepartment(DeptUid("SH_GM"),            "SH_GM",           "总经理办公室",       "上海贸易公司日常运营管理",                     DeptUid("SH_TRADE"),         "SHZJL",  "欧普智能集团-上海贸易有限公司-总经理办公室",          "管理部门", "张明",   false, false, now, company, user),
            CreateDepartment(DeptUid("SH_SALES"),         "SH_SALES",        "销售部",             "国内外产品销售及客户关系维护",                 DeptUid("SH_TRADE"),         "SHXSB",  "欧普智能集团-上海贸易有限公司-销售部",                "销售部门", "张明",   false, false, now, company, user),
            CreateDepartment(DeptUid("SH_SCM"),           "SH_SCM",          "供应链管理部",       "供应链计划、协调及物流管理",                   DeptUid("SH_TRADE"),         "SHGYLB", "欧普智能集团-上海贸易有限公司-供应链管理部",          "采购部门", "陈志远", false, false, now, company, user),
            CreateDepartment(DeptUid("SH_FIN"),           "SH_FIN",          "财务部",             "上海贸易公司财务核算及应收应付管理",           DeptUid("SH_TRADE"),         "SHCWB",  "欧普智能集团-上海贸易有限公司-财务部",                "财务部门", "徐文斌", false, true,  now, company, user),
            CreateDepartment(DeptUid("SH_CS"),            "SH_CS",           "客户服务部",         "售后服务、客户投诉处理及技术支持",             DeptUid("SH_TRADE"),         "SHKSB",  "欧普智能集团-上海贸易有限公司-客户服务部",            "销售部门", "刘伟",   false, false, now, company, user),

            // --- 广州物流有限公司 ---
            CreateDepartment(DeptUid("GZ_GM"),            "GZ_GM",           "总经理办公室",       "广州物流公司日常运营管理",                     DeptUid("GZ_LOGISTICS"),     "GZZJL",  "欧普智能集团-广州物流有限公司-总经理办公室",          "管理部门", "张伟",   false, false, now, company, user),
            CreateDepartment(DeptUid("GZ_WH"),            "GZ_WH",           "仓储运营部",         "仓库运营管理及库存管控",                       DeptUid("GZ_LOGISTICS"),     "GZCCB",  "欧普智能集团-广州物流有限公司-仓储运营部",            "仓储部门", "王芳",   false, false, now, company, user),
            CreateDepartment(DeptUid("GZ_TRANS"),         "GZ_TRANS",        "运输配送部",         "干线运输、城市配送及车辆调度",                 DeptUid("GZ_LOGISTICS"),     "GZYSB",  "欧普智能集团-广州物流有限公司-运输配送部",            "仓储部门", "刘洋",   false, false, now, company, user),
            CreateDepartment(DeptUid("GZ_FIN"),           "GZ_FIN",          "财务部",             "广州物流公司财务核算及费用管理",               DeptUid("GZ_LOGISTICS"),     "GZCWB",  "欧普智能集团-广州物流有限公司-财务部",                "财务部门", "徐文斌", false, true,  now, company, user),

            // ═══════════════════════════════════════════════════════
            // 第四级：部门下属科室/班组
            // ═══════════════════════════════════════════════════════

            // --- 深圳制造-研发部下属 ---
            CreateDepartment(DeptUid("SZ_RD_HW"),         "SZ_RD_HW",        "硬件研发科",         "电路设计、PCB Layout及硬件测试",               DeptUid("SZ_RD"),            "YJYF",   "欧普智能集团-深圳制造有限公司-研发部-硬件研发科",     "研发部门", "李明华", false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_RD_SW"),         "SZ_RD_SW",        "软件研发科",         "嵌入式软件、上位机及测试程序开发",             DeptUid("SZ_RD"),            "RJYF",   "欧普智能集团-深圳制造有限公司-研发部-软件研发科",     "研发部门", "李明华", false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_RD_PE"),         "SZ_RD_PE",        "工艺工程科",         "生产工艺开发、工装夹具设计",                   DeptUid("SZ_RD"),            "GYGC",   "欧普智能集团-深圳制造有限公司-研发部-工艺工程科",     "研发部门", "郑国强", false, false, now, company, user),

            // --- 深圳制造-生产部下属产线 ---
            CreateDepartment(DeptUid("SZ_PROD_SMT"),      "SZ_PROD_SMT",     "SMT车间",            "SMT贴片生产线，含印刷、贴片、回流焊",         DeptUid("SZ_PROD"),          "SMTCJ",  "欧普智能集团-深圳制造有限公司-生产部-SMT车间",        "生产部门", "郑国强", true,  true,  now, company, user),
            CreateDepartment(DeptUid("SZ_PROD_DIP"),      "SZ_PROD_DIP",     "DIP车间",            "插件及波峰焊生产线",                           DeptUid("SZ_PROD"),          "DIPCJ",  "欧普智能集团-深圳制造有限公司-生产部-DIP车间",        "生产部门", "郑国强", true,  true,  now, company, user),
            CreateDepartment(DeptUid("SZ_PROD_ASSY"),     "SZ_PROD_ASSY",    "组装车间",           "产品组装、调试及老化测试",                     DeptUid("SZ_PROD"),          "ZZCJ",   "欧普智能集团-深圳制造有限公司-生产部-组装车间",       "生产部门", "林小红", true,  true,  now, company, user),
            CreateDepartment(DeptUid("SZ_PROD_PKG"),      "SZ_PROD_PKG",     "包装车间",           "产品包装、贴标及装箱",                         DeptUid("SZ_PROD"),          "BZCJ",   "欧普智能集团-深圳制造有限公司-生产部-包装车间",       "生产部门", "林小红", true,  true,  now, company, user),

            // --- 深圳制造-品质部下属 ---
            CreateDepartment(DeptUid("SZ_QC_IQC"),        "SZ_QC_IQC",       "来料检验组",         "原材料及外协件来料检验",                       DeptUid("SZ_QC"),            "IQC",    "欧普智能集团-深圳制造有限公司-品质部-来料检验组",     "质量部门", "黄志刚", false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_QC_IPQC"),       "SZ_QC_IPQC",      "制程检验组",         "生产过程巡检及首件检验",                       DeptUid("SZ_QC"),            "IPQC",   "欧普智能集团-深圳制造有限公司-品质部-制程检验组",     "质量部门", "何晓燕", false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_QC_OQC"),        "SZ_QC_OQC",       "出货检验组",         "成品出货检验及包装检查",                       DeptUid("SZ_QC"),            "OQC",    "欧普智能集团-深圳制造有限公司-品质部-出货检验组",     "质量部门", "何晓燕", false, false, now, company, user),

            // --- 深圳制造-仓储部下属 ---
            CreateDepartment(DeptUid("SZ_WH_RM"),         "SZ_WH_RM",        "原材料仓",           "原材料收发存管理",                             DeptUid("SZ_WH"),            "YLCK",   "欧普智能集团-深圳制造有限公司-仓储部-原材料仓",       "仓储部门", "王芳",   false, false, now, company, user),
            CreateDepartment(DeptUid("SZ_WH_FG"),         "SZ_WH_FG",        "成品仓",             "成品收发存及发货管理",                         DeptUid("SZ_WH"),            "CPCK",   "欧普智能集团-深圳制造有限公司-仓储部-成品仓",         "仓储部门", "刘洋",   false, false, now, company, user),

            // --- 上海贸易-销售部下属 ---
            CreateDepartment(DeptUid("SH_SALES_DOM"),     "SH_SALES_DOM",    "国内销售科",         "负责国内市场开拓及大客户维护",                 DeptUid("SH_SALES"),         "GNXS",   "欧普智能集团-上海贸易有限公司-销售部-国内销售科",     "销售部门", "张明",   false, false, now, company, user),
            CreateDepartment(DeptUid("SH_SALES_OVS"),     "SH_SALES_OVS",    "海外销售科",         "负责海外市场开拓及出口业务",                   DeptUid("SH_SALES"),         "HWXS",   "欧普智能集团-上海贸易有限公司-销售部-海外销售科",     "销售部门", "刘伟",   false, false, now, company, user),
            CreateDepartment(DeptUid("SH_SALES_EC"),      "SH_SALES_EC",     "电商运营科",         "线上渠道运营及电商平台管理",                   DeptUid("SH_SALES"),         "DSYY",   "欧普智能集团-上海贸易有限公司-销售部-电商运营科",     "销售部门", "赵阳",   false, false, now, company, user),

            // --- 广州物流-仓储运营部下属 ---
            CreateDepartment(DeptUid("GZ_WH_A"),          "GZ_WH_A",         "A库运营组",          "A库日常运营及库存管理",                         DeptUid("GZ_WH"),            "AKYYB",  "欧普智能集团-广州物流有限公司-仓储运营部-A库运营组",  "仓储部门", "王芳",   false, false, now, company, user),
            CreateDepartment(DeptUid("GZ_WH_B"),          "GZ_WH_B",         "B库运营组",          "B库日常运营及库存管理",                         DeptUid("GZ_WH"),            "BKYYB",  "欧普智能集团-广州物流有限公司-仓储运营部-B库运营组",  "仓储部门", "赵敏",   false, false, now, company, user),
            CreateDepartment(DeptUid("GZ_WH_COLD"),       "GZ_WH_COLD",      "冷链仓运营组",       "冷链仓温控管理及冷藏品存储",                   DeptUid("GZ_WH"),            "LLCK",   "欧普智能集团-广州物流有限公司-仓储运营部-冷链仓运营组","仓储部门", "刘洋",   false, false, now, company, user),
        };

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdDepartment>()
            .Where(d => !d.FDeleted)
            .Select(d => d.FNumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = depts.Where(d => !existingSet.Contains(d.FNumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static readonly Dictionary<string, string> DeptPropertyGroupMap = new()
    {
        ["管理部门"] = "DEPT-MGT", ["生产部门"] = "DEPT-PRD", ["研发部门"] = "DEPT-RD",
        ["销售部门"] = "DEPT-SAL", ["财务部门"] = "DEPT-FIN", ["仓储部门"] = "DEPT-WH",
        ["质量部门"] = "DEPT-QC",  ["采购部门"] = "DEPT-PUR",
    };

    private static TBdDepartment CreateDepartment(
        string uid, string number, string name, string description,
        string parentId, string helpCode, string fullName,
        string deptProperty, string manager,
        bool costAccountType, bool isLine,
        DateTime now, string company, string user)
    {
        var groupId = DeptPropertyGroupMap.TryGetValue(deptProperty, out var gk) ? GroupUid(gk) : "";
        return new TBdDepartment
        {
            Uid = uid,
            FInterId = $"DEPT_{number}",
            FNumber = number,
            FName = name,
            FDescription = description,
            FParentId = parentId,
            FHelpCode = helpCode,
            FFullName = fullName,
            FDeptProperty = deptProperty,
            FManager = manager,
            FCostAccountType = costAccountType,
            FIsLine = isLine,
            FGroupId = groupId,
            FCreateOrgId = company,
            FUseOrgId = company,
            FEffectDate = new DateTime(2020, 1, 1),
            FLapseDate = DateTime.MaxValue,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 客户种子数据

    private async Task SeedCustomersAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 分组 Uid 引用
        var grpCusDom = GroupUid("CUS-DOM"); var grpCusOvs = GroupUid("CUS-OVS");

        var customers = new List<TBdCustomer>
        {
            // 国内大客户
            CreateCustomer("CUS-001", "深圳华芯电子科技有限公司",   "华芯电子",   "HuaXin Electronics",  "王建国", "13900001001", "wang@huaxin.com",     "CNY", "中国", "广东省", "深圳市", "深圳市南山区科技园南路88号",       "518000", "张明",   "DEPT_SALES", now, company, user, grpCusDom),
            CreateCustomer("CUS-002", "上海智联自动化设备有限公司", "智联自动化", "ZhiLian Automation",  "李秀英", "13900001002", "li@zhilian.com",      "CNY", "中国", "上海市", "上海市", "上海市浦东新区张江高科技园区200号", "200120", "刘伟",   "DEPT_SALES", now, company, user, grpCusDom),
            CreateCustomer("CUS-003", "北京中科精密仪器有限公司",   "中科精密",   "ZhongKe Precision",   "赵丽华", "13900001003", "zhao@zhongke.com",    "CNY", "中国", "北京市", "北京市", "北京市海淀区中关村大街15号",       "100080", "张明",   "DEPT_SALES", now, company, user, grpCusDom),
            CreateCustomer("CUS-004", "广州恒达机械制造有限公司",   "恒达机械",   "HengDa Machinery",    "陈志强", "13900001004", "chen@hengda.com",     "CNY", "中国", "广东省", "广州市", "广州市黄埔区开发大道168号",        "510700", "王丽",   "DEPT_SALES", now, company, user, grpCusDom),
            CreateCustomer("CUS-005", "杭州云帆信息技术有限公司",   "云帆信息",   "YunFan IT",           "孙晓明", "13900001005", "sun@yunfan.com",      "CNY", "中国", "浙江省", "杭州市", "杭州市余杭区文一西路1218号",       "311100", "刘伟",   "DEPT_SALES", now, company, user, grpCusDom),
            CreateCustomer("CUS-006", "成都天府新能源科技有限公司", "天府新能源", "TianFu New Energy",   "周文博", "13900001006", "zhou@tianfu-ne.com",  "CNY", "中国", "四川省", "成都市", "成都市高新区天府大道南段666号",    "610041", "张明",   "DEPT_SALES", now, company, user, grpCusDom),
            CreateCustomer("CUS-007", "武汉光谷激光技术有限公司",   "光谷激光",   "GuangGu Laser",       "吴建华", "13900001007", "wu@gglaser.com",      "CNY", "中国", "湖北省", "武汉市", "武汉市东湖高新区光谷大道77号",     "430074", "王丽",   "DEPT_SALES", now, company, user, grpCusDom),
            CreateCustomer("CUS-008", "苏州纳微半导体有限公司",     "纳微半导体", "NaWei Semiconductor", "郑国强", "13900001008", "zheng@nawei.com",     "CNY", "中国", "江苏省", "苏州市", "苏州市工业园区星湖街328号",        "215021", "刘伟",   "DEPT_SALES", now, company, user, grpCusDom),

            // 海外客户
            CreateCustomer("CUS-101", "TechVision Inc.",                    "TechVision",  "TechVision Inc.",     "John Smith",    "+1-408-555-0101", "john@techvision.com",    "USD", "美国",   "加利福尼亚州", "圣何塞",   "1200 Innovation Way, San Jose, CA 95110",       "95110",  "张明", "DEPT_SALES", now, company, user, grpCusOvs),
            CreateCustomer("CUS-102", "EuroTech GmbH",                      "EuroTech",    "EuroTech GmbH",       "Hans Mueller",  "+49-89-555-0201", "hans@eurotech.de",       "EUR", "德国",   "巴伐利亚州",   "慕尼黑",   "Technologiepark Str. 45, 80331 Munich",         "80331",  "刘伟", "DEPT_SALES", now, company, user, grpCusOvs),
            CreateCustomer("CUS-103", "Sakura Manufacturing Co., Ltd.",     "Sakura Mfg",  "Sakura Manufacturing","Tanaka Yuki",   "+81-3-5555-0301", "tanaka@sakura-mfg.jp",   "JPY", "日本",   "东京都",       "东京",     "2-3-1 Marunouchi, Chiyoda-ku, Tokyo 100-0005", "100-0005","张明", "DEPT_SALES", now, company, user, grpCusOvs),
            CreateCustomer("CUS-104", "Samsung Parts Trading Co., Ltd.",    "Samsung Parts","Samsung Parts",       "Kim Min-jun",   "+82-2-555-0401",  "kim@samsungparts.kr",    "KRW", "韩国",   "首尔特别市",   "首尔",     "123 Gangnam-daero, Gangnam-gu, Seoul 06164",    "06164",  "王丽", "DEPT_SALES", now, company, user, grpCusOvs),
            CreateCustomer("CUS-105", "Global Supply Chain Pte. Ltd.",      "GSC",         "Global Supply Chain", "David Tan",     "+65-6555-0501",   "david@gsc-sg.com",       "SGD", "新加坡", "",             "新加坡",   "10 Changi Business Park Central 2, #07-01",     "486030", "刘伟", "DEPT_SALES", now, company, user, grpCusOvs),
        };

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdCustomer>()
            .Where(c => !c.FDeleted)
            .Select(c => c.FNumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = customers.Where(c => !existingSet.Contains(c.FNumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static TBdCustomer CreateCustomer(
        string number, string name, string shortName, string nameEn,
        string contact, string phone, string email,
        string currencyId, string country, string provincial, string city, string address, string zip,
        string seller, string salDeptId,
        DateTime now, string company, string user, string groupId = "")
    {
        return new TBdCustomer
        {
            Uid = Guid.NewGuid().ToString("N"),
            FInterId = $"CUS_{number}",
            FNumber = number,
            FName = name,
            FShortName = shortName,
            FNameEn = nameEn,
            FContact = contact,
            FPhone = phone,
            FEmail = email,
            FTradingCurrId = currencyId,
            FCountry = country,
            FProvincial = provincial,
            FProvice = provincial,
            FCity = city,
            FAddress = address,
            FZip = zip,
            FSeller = seller,
            FSalDeptId = salDeptId,
            FGroupId = groupId,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 供应商种子数据

    private async Task SeedSuppliersAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 分组 Uid 引用
        var grpSupRaw = GroupUid("SUP-RAW"); var grpSupElc = GroupUid("SUP-ELC");
        var grpSupPkg = GroupUid("SUP-PKG"); var grpSupEqp = GroupUid("SUP-EQP");
        var grpSupOvs = GroupUid("SUP-OVS");

        var suppliers = new List<TBdSupplier>
        {
            // 原材料供应商
            CreateSupplier("SUP-001", "东莞市鑫盛金属材料有限公司", "李国华", "13800002001", "东莞市长安镇锦厦工业区8号",           "金属原材料供应商，主营铝合金、不锈钢板材", now, company, user, grpSupRaw),
            CreateSupplier("SUP-002", "佛山市南海塑胶原料有限公司", "黄志明", "13800002002", "佛山市南海区狮山镇工业大道128号",     "工程塑料供应商，主营PA/PC/POM等原料",       now, company, user, grpSupRaw),
            CreateSupplier("SUP-003", "江苏恒力化工新材料有限公司", "张建军", "13800002003", "苏州市吴江区汾湖经济开发区创业路99号", "化工原料供应商，主营涂料、胶粘剂",         now, company, user, grpSupRaw),
            CreateSupplier("SUP-004", "浙江永固橡胶制品有限公司",   "刘明辉", "13800002004", "宁波市镇海区骆驼街道工业园区A区",     "橡胶制品供应商，主营密封件、减震垫",       now, company, user, grpSupRaw),

            // 电子元器件供应商
            CreateSupplier("SUP-005", "深圳市芯源电子有限公司",     "陈伟东", "13800002005", "深圳市福田区华强北路1002号",           "电子元器件供应商，主营IC芯片、电容电阻",   now, company, user, grpSupElc),
            CreateSupplier("SUP-006", "上海贝尔电气有限公司",       "王海燕", "13800002006", "上海市闵行区莘庄工业区申富路88号",     "电气元件供应商，主营继电器、连接器",       now, company, user, grpSupElc),

            // 包装材料供应商
            CreateSupplier("SUP-007", "广州市利达包装材料有限公司", "林志豪", "13800002007", "广州市白云区太和镇民营科技园16号",     "包装材料供应商，主营纸箱、泡沫、缠绕膜",   now, company, user, grpSupPkg),
            CreateSupplier("SUP-008", "中山市华美印刷有限公司",     "何晓峰", "13800002008", "中山市小榄镇工业大道东58号",           "印刷品供应商，主营标签、说明书、彩盒",     now, company, user, grpSupPkg),

            // 设备及工具供应商
            CreateSupplier("SUP-009", "沈阳机床工具有限公司",       "赵国栋", "13800002009", "沈阳市铁西区开发大路16号",             "机床设备供应商，主营数控车床、加工中心",   now, company, user, grpSupEqp),
            CreateSupplier("SUP-010", "无锡精密刀具有限公司",       "孙建平", "13800002010", "无锡市惠山区洛社镇工业集中区",         "刀具供应商，主营铣刀、钻头、丝锥",         now, company, user, grpSupEqp),

            // 海外供应商
            CreateSupplier("SUP-101", "Mitsubishi Materials Corp.",  "Sato Kenji",    "+81-3-5252-5200", "1-3-2 Otemachi, Chiyoda-ku, Tokyo, Japan",       "日本进口特种钢材及硬质合金供应商",         now, company, user, grpSupOvs),
            CreateSupplier("SUP-102", "Texas Instruments Inc.",      "Robert Johnson", "+1-972-995-2011", "12500 TI Blvd, Dallas, TX 75243, USA",           "美国进口半导体芯片供应商",                 now, company, user, grpSupOvs),
            CreateSupplier("SUP-103", "BASF SE",                     "Thomas Weber",  "+49-621-60-0",    "Carl-Bosch-Str. 38, 67056 Ludwigshafen, Germany","德国进口化工原料及工程塑料供应商",         now, company, user, grpSupOvs),
            CreateSupplier("SUP-104", "Samsung SDI Co., Ltd.",       "Park Ji-hoon",  "+82-31-8006-3100","150 Samsung-ro, Yeongtong-gu, Suwon, Korea",     "韩国进口锂电池及电子材料供应商",           now, company, user, grpSupOvs),
            CreateSupplier("SUP-105", "Bosch Rexroth AG",            "Klaus Fischer", "+49-9352-18-0",   "Zum Eisengiesser 1, 97816 Lohr am Main, Germany","德国进口液压及自动化设备供应商",           now, company, user, grpSupOvs),
        };

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdSupplier>()
            .Where(s => !s.FDeleted)
            .Select(s => s.FNumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = suppliers.Where(s => !existingSet.Contains(s.FNumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static TBdSupplier CreateSupplier(
        string number, string name, string contact, string phone, string address, string note,
        DateTime now, string company, string user, string groupId = "")
    {
        return new TBdSupplier
        {
            Uid = Guid.NewGuid().ToString("N"),
            FInterId = $"SUP_{number}",
            FNumber = number,
            FName = name,
            FContact = contact,
            FPhone = phone,
            FAddress = address,
            FNote = note,
            FGroupId = groupId,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 职员种子数据

    private async Task SeedEmployeesAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 分组 Uid 引用
        var grpEmpMgt  = GroupUid("EMP-MGT");  var grpEmpSale = GroupUid("EMP-SALE");
        var grpEmpProd = GroupUid("EMP-PROD"); var grpEmpWh   = GroupUid("EMP-WH");
        var grpEmpFin  = GroupUid("EMP-FIN");  var grpEmpTech = GroupUid("EMP-TECH");

        // 性别按新约定 0=男;1=女
        var employees = new List<THrEmpinfo>
        {
            // 管理层 → 集团总裁办 (GRP_OFFICE)
            CreateEmployee("EMP-001", "张建国", 0, "本科",   new DateTime(1975, 3, 15), new DateTime(2010, 1, 5),  "13700001001", "zhangjg@opsoft.com",  DeptUid("GRP_OFFICE"),  "深圳市南山区科技园1栋501",   "张秀兰", "13700009001", now, company, user, grpEmpMgt),
            CreateEmployee("EMP-002", "李明华", 0, "硕士",   new DateTime(1978, 7, 22), new DateTime(2012, 3, 12), "13700001002", "limh@opsoft.com",     DeptUid("GRP_OFFICE"),  "深圳市南山区蛇口花园3栋202", "李芳",   "13700009002", now, company, user, grpEmpMgt),

            // 销售部 → 上海贸易-销售部 (SH_SALES)
            CreateEmployee("EMP-010", "张明",   0, "本科",   new DateTime(1985, 5, 10), new DateTime(2015, 6, 1),  "13700001010", "zhangm@opsoft.com",   DeptUid("SH_SALES"),    "深圳市宝安区西乡街道108号",  "张丽",   "13700009010", now, company, user, grpEmpSale),
            CreateEmployee("EMP-011", "刘伟",   0, "本科",   new DateTime(1988, 11, 3), new DateTime(2016, 8, 15), "13700001011", "liuw@opsoft.com",     DeptUid("SH_SALES"),    "深圳市龙华区民治街道56号",   "刘芳",   "13700009011", now, company, user, grpEmpSale),
            CreateEmployee("EMP-012", "王丽",   1, "大专",   new DateTime(1990, 2, 28), new DateTime(2017, 4, 10), "13700001012", "wangl@opsoft.com",    DeptUid("SH_SALES"),    "深圳市福田区景田路88号",     "王强",   "13700009012", now, company, user, grpEmpSale),
            CreateEmployee("EMP-013", "赵阳",   0, "本科",   new DateTime(1992, 8, 16), new DateTime(2019, 7, 1),  "13700001013", "zhaoy@opsoft.com",    DeptUid("SH_SALES"),    "深圳市南山区前海路12号",     "赵敏",   "13700009013", now, company, user, grpEmpSale),

            // 采购部 → 深圳制造-采购部 (SZ_PURCHASE)
            CreateEmployee("EMP-020", "陈志远", 0, "本科",   new DateTime(1986, 4, 20), new DateTime(2014, 9, 1),  "13700001020", "chenzy@opsoft.com",   DeptUid("SZ_PURCHASE"), "深圳市宝安区沙井街道22号",   "陈芳",   "13700009020", now, company, user, grpEmpMgt),
            CreateEmployee("EMP-021", "孙丽娟", 1, "本科",   new DateTime(1989, 12, 5), new DateTime(2016, 3, 20), "13700001021", "sunlj@opsoft.com",    DeptUid("SZ_PURCHASE"), "深圳市龙岗区坂田街道66号",   "孙强",   "13700009021", now, company, user, grpEmpMgt),
            CreateEmployee("EMP-022", "周海涛", 0, "大专",   new DateTime(1991, 6, 18), new DateTime(2018, 5, 8),  "13700001022", "zhouht@opsoft.com",   DeptUid("SZ_PURCHASE"), "深圳市光明区公明街道33号",   "周敏",   "13700009022", now, company, user, grpEmpMgt),

            // 仓储部 → 深圳制造-仓储部 (SZ_WH)
            CreateEmployee("EMP-030", "张伟",   0, "大专",   new DateTime(1984, 9, 8),  new DateTime(2013, 2, 18), "13700001030", "zhangw@opsoft.com",   DeptUid("SZ_WH"),       "深圳市宝安区福永街道45号",   "张芳",   "13700009030", now, company, user, grpEmpWh),
            CreateEmployee("EMP-031", "王芳",   1, "大专",   new DateTime(1987, 1, 25), new DateTime(2015, 11, 5), "13700001031", "wangf@opsoft.com",    DeptUid("SZ_WH"),       "深圳市宝安区松岗街道78号",   "王强",   "13700009031", now, company, user, grpEmpWh),
            CreateEmployee("EMP-032", "赵敏",   1, "高中",   new DateTime(1993, 3, 12), new DateTime(2018, 8, 20), "13700001032", "zhaom@opsoft.com",    DeptUid("SZ_WH"),       "深圳市宝安区石岩街道15号",   "赵刚",   "13700009032", now, company, user, grpEmpWh),
            CreateEmployee("EMP-033", "刘洋",   0, "大专",   new DateTime(1990, 10, 7), new DateTime(2017, 6, 12), "13700001033", "liuy@opsoft.com",     DeptUid("SZ_WH"),       "深圳市宝安区西乡街道99号",   "刘芳",   "13700009033", now, company, user, grpEmpWh),

            // 生产部 → 深圳制造-生产部 (SZ_PROD)
            CreateEmployee("EMP-040", "吴建华", 0, "本科",   new DateTime(1982, 6, 30), new DateTime(2011, 4, 1),  "13700001040", "wujh@opsoft.com",     DeptUid("SZ_PROD"),     "深圳市宝安区沙井街道88号",   "吴芳",   "13700009040", now, company, user, grpEmpProd),
            CreateEmployee("EMP-041", "郑国强", 0, "大专",   new DateTime(1988, 8, 14), new DateTime(2015, 10, 8), "13700001041", "zhenggq@opsoft.com",  DeptUid("SZ_PROD"),     "深圳市宝安区松岗街道55号",   "郑丽",   "13700009041", now, company, user, grpEmpProd),
            CreateEmployee("EMP-042", "林小红", 1, "大专",   new DateTime(1991, 4, 22), new DateTime(2018, 1, 15), "13700001042", "linxh@opsoft.com",    DeptUid("SZ_PROD"),     "深圳市光明区光明街道28号",   "林强",   "13700009042", now, company, user, grpEmpProd),

            // 质检部 → 深圳制造-品质部 (SZ_QC)
            CreateEmployee("EMP-050", "黄志刚", 0, "本科",   new DateTime(1985, 11, 9), new DateTime(2014, 7, 1),  "13700001050", "huangzg@opsoft.com",  DeptUid("SZ_QC"),       "深圳市龙华区大浪街道36号",   "黄芳",   "13700009050", now, company, user, grpEmpTech),
            CreateEmployee("EMP-051", "何晓燕", 1, "本科",   new DateTime(1990, 7, 3),  new DateTime(2017, 9, 18), "13700001051", "hexy@opsoft.com",     DeptUid("SZ_QC"),       "深圳市龙华区观湖街道42号",   "何强",   "13700009051", now, company, user, grpEmpTech),

            // 财务部 → 深圳制造-财务部 (SZ_FIN)
            CreateEmployee("EMP-060", "杨秀芳", 1, "本科",   new DateTime(1983, 2, 14), new DateTime(2012, 5, 6),  "13700001060", "yangxf@opsoft.com",   DeptUid("SZ_FIN"),      "深圳市福田区梅林路18号",     "杨刚",   "13700009060", now, company, user, grpEmpFin),
            CreateEmployee("EMP-061", "徐文斌", 0, "硕士",   new DateTime(1987, 9, 27), new DateTime(2016, 2, 22), "13700001061", "xuwb@opsoft.com",     DeptUid("SZ_FIN"),      "深圳市福田区景田北路25号",   "徐芳",   "13700009061", now, company, user, grpEmpFin),
        };

        // 幂等：只插入数据库中不存在的记录（按 Fnumber 判断）
        var existingNumbers = await _db.Queryable<THrEmpinfo>()
            .Where(e => !e.FDeleted)
            .Select(e => e.Fnumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = employees.Where(e => !existingSet.Contains(e.Fnumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static THrEmpinfo CreateEmployee(
        string number, string name, int sex, string education,
        DateTime birthDate, DateTime entryDate,
        string tel, string email, string deptId, string address,
        string emergencyContact, string emergencyTel,
        DateTime now, string company, string user, string groupId = "")
    {
        return new THrEmpinfo
        {
            Uid = SeedUid("EMP", number),
            FInterId = SeedUid("EMP", number),
            Fnumber = number,
            Fname = name,
            Fsex = sex,
            Feducation = education,
            Fbirthdate = birthDate,
            Fentrydate = entryDate,
            Fdeparturedate = DateTime.MinValue,
            Ftransferdate = DateTime.MinValue,
            FCheckDate = DateTime.MinValue,
            Fdisabledate = DateTime.MinValue,
            Fisdeparture = false,
            Fpicture = Array.Empty<byte>(),
            Ftel = tel,
            Femail = email,
            Fsaldeptid = deptId,
            Faddress = address,
            Emergency = emergencyContact,
            Femergencytel = emergencyTel,
            FGroupId = groupId,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 成本中心 / 职务（辅助资料）种子数据

    // 成本中心（T_CB_COSTCENTER）：FInterId=代码
    private async Task SeedCostCentersAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        var centers = new[]
        {
            ("CC001", "综合管理中心"), ("CC002", "销售中心"), ("CC003", "生产中心"),
            ("CC004", "采购中心"), ("CC005", "财务中心"), ("CC006", "技术中心"),
        };
        var list = centers.Select(c => new TCbCostcenter
        {
            Uid = c.Item1,
            FInterId = c.Item1,
            Fnumber = c.Item1,
            Fname = c.Item2,
            Fcheckdate = DateTime.MinValue,
            Fdisabledate = DateTime.MinValue,
            FStatus = 0,
            FDeleted = false,
            FDisabled = false,
            FCompanyId = company,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        }).ToList();

        var existing = new HashSet<string>(
            await _db.Queryable<TCbCostcenter>().Where(c => !c.FDeleted).Select(c => c.Fnumber).ToListAsync());
        var missing = list.Where(c => !existing.Contains(c.Fnumber)).ToList();
        if (missing.Count > 0)
            await _db.Insertable(missing).ExecuteCommandAsync();
    }

    // 职务：辅助资料类别(FNUMBER='D') + 其下条目；职务下拉按"类别 FNUMBER='D' AND FSTATUS=40"取条目
    private async Task SeedDutiesAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";
        const string dutyCatId = "AUXCAT_DUTY";

        // 类别（FStatus=40 已审核，便于条目过滤）
        if (!await _db.Queryable<TBasAssistantdata>().AnyAsync(c => c.Fnumber == "D"))
        {
            await _db.Insertable(new TBasAssistantdata
            {
                Uid = dutyCatId,
                FInterId = dutyCatId,
                Fnumber = "D",
                Fname = "职务",
                FCheckDate = DateTime.MinValue,
                Fdisabledate = DateTime.MinValue,
                FStatus = 40,
                FDeleted = false,
                FDisabled = false,
                FCompanyId = company,
                CYmd = now,
                CUser = user,
                MYmd = now,
                MUser = user
            }).ExecuteCommandAsync();
        }

        var duties = new[]
        {
            ("DUTY-01", "总经理"), ("DUTY-02", "副总经理"), ("DUTY-03", "部门经理"),
            ("DUTY-04", "主管"), ("DUTY-05", "组长"), ("DUTY-06", "员工"),
        };
        var entries = duties.Select((d, i) => new TBasAssistantdataentry
        {
            Uid = SeedUid("DUTY", d.Item1),
            FInterId = SeedUid("DUTY", d.Item1),
            Fid = dutyCatId,
            Fnumber = d.Item1,
            Fdatavalue = d.Item2,
            Fseq = i + 1,
            FCheckDate = DateTime.MinValue,
            Fdisabledate = DateTime.MinValue,
            FStatus = 40,
            FDeleted = false,
            FDisabled = false,
            FCompanyId = company,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        }).ToList();

        var existing = new HashSet<string>(
            await _db.Queryable<TBasAssistantdataentry>().Where(e => e.Fid == dutyCatId && !e.FDeleted)
                .Select(e => e.Fnumber).ToListAsync());
        var missing = entries.Where(e => !existing.Contains(e.Fnumber)).ToList();
        if (missing.Count > 0)
            await _db.Insertable(missing).ExecuteCommandAsync();
    }

    #endregion

    #region 物料种子数据

    private async Task SeedMaterialsAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // 单位 Uid 引用（对应 SeedUnitsAsync 中的 FNumber）
        var pcs = UnitUid("PCS"); var kg = UnitUid("KG"); var m = UnitUid("M");
        var l = UnitUid("L"); var set = UnitUid("SET"); var box = UnitUid("BOX");
        var rol = UnitUid("ROL"); var g = UnitUid("G");

        // 仓库 Uid 引用
        var whRm01 = StockUid("WH-RM01"); var whRm02 = StockUid("WH-RM02");
        var whFg01 = StockUid("WH-FG01"); var whPk01 = StockUid("WH-PK01");
        var whWip01 = StockUid("WH-WIP01");

        // 仓位 Uid 引用（编码规则：{仓库前缀}-{区域编码}）
        var rm01A0101 = SpUid("RM01-A01-01"); var rm01A0102 = SpUid("RM01-A01-02");
        var rm01B0101 = SpUid("RM01-B01-01"); var rm01B0102 = SpUid("RM01-B01-02");
        var rm02C0101 = SpUid("RM02-C01-01"); var rm02D0101 = SpUid("RM02-D01-01");
        var fg01P0101 = SpUid("FG01-P01-01"); var fg01P0201 = SpUid("FG01-P02-01");

        // 分组 Uid 引用
        var grpElc = GroupUid("MAT-ELC"); var grpMtl = GroupUid("MAT-MTL");
        var grpChm = GroupUid("MAT-CHM"); var grpPcb = GroupUid("MAT-PCB");
        var grpSfp = GroupUid("MAT-SFP"); var grpFg  = GroupUid("MAT-FG");
        var grpPkg = GroupUid("MAT-PKG");

        var materials = new List<TBdMaterial>
        {
            // ═══════════════════════════════════════════════════════
            // 电子元器件（原材料）
            // ═══════════════════════════════════════════════════════
            CreateMaterial("MAT-E001", "贴片电阻 0402 10KΩ",    "0402 10KΩ ±1%",       "外购",  "A", pcs, pcs, pcs, pcs, whRm01, rm01A0101, true,  true,  true,  100000m, 50000m, 0, 0, 5000m,  5000m,  now, company, user, grpElc),
            CreateMaterial("MAT-E002", "贴片电容 0603 100nF",    "0603 100nF X7R 50V",  "外购",  "A", pcs, pcs, pcs, pcs, whRm01, rm01A0101, true,  true,  true,  80000m,  30000m, 0, 0, 4000m,  4000m,  now, company, user, grpElc),
            CreateMaterial("MAT-E003", "STM32F103C8T6 芯片",     "LQFP-48 ARM Cortex-M3","外购", "A", pcs, pcs, pcs, pcs, whRm01, rm01A0102, true,  true,  true,  5000m,   1000m,  0, 0, 100m,   100m,   now, company, user, grpElc),
            CreateMaterial("MAT-E004", "USB Type-C 连接器",      "16Pin SMD 母座",      "外购",  "B", pcs, pcs, pcs, pcs, whRm01, rm01A0102, true,  true,  false, 20000m,  5000m,  0, 0, 500m,   500m,   now, company, user, grpElc),
            CreateMaterial("MAT-E005", "3.3V 稳压芯片 AMS1117", "SOT-223 3.3V 1A",     "外购",  "B", pcs, pcs, pcs, pcs, whRm01, rm01A0102, true,  true,  false, 10000m,  3000m,  0, 0, 500m,   500m,   now, company, user, grpElc),
            CreateMaterial("MAT-E006", "LED 发光二极管 红色",     "0805 红色 20mA",      "外购",  "C", pcs, pcs, pcs, pcs, whRm01, rm01A0101, false, false, false, 50000m,  10000m, 0, 0, 5000m,  5000m,  now, company, user, grpElc),

            // ═══════════════════════════════════════════════════════
            // 金属/结构件（原材料）
            // ═══════════════════════════════════════════════════════
            CreateMaterial("MAT-M001", "铝合金板材 6061-T6",     "1.5mm×1220mm×2440mm", "外购",  "A", kg,  kg,  kg,  kg,  whRm01, rm01B0101, true,  true,  false, 5000m,   1000m,  1m, 1.02m, 100m,   50m,    now, company, user, grpMtl),
            CreateMaterial("MAT-M002", "不锈钢板 304",           "2.0mm×1000mm×2000mm", "外购",  "B", kg,  kg,  kg,  kg,  whRm01, rm01B0102, true,  true,  false, 3000m,   500m,   1m, 1.02m, 100m,   50m,    now, company, user, grpMtl),
            CreateMaterial("MAT-M003", "铜线 T2 紫铜",           "Φ0.5mm",              "外购",  "B", kg,  kg,  kg,  kg,  whRm01, rm01B0101, true,  true,  false, 2000m,   300m,   1m, 1.01m, 50m,    25m,    now, company, user, grpMtl),
            CreateMaterial("MAT-M004", "锌合金压铸件-外壳A",     "定制件 图号ZJ-A001",  "外购",  "A", pcs, pcs, pcs, pcs, whRm01, rm01B0102, true,  true,  true,  10000m,  2000m,  0.15m, 0.18m, 500m, 100m,  now, company, user, grpMtl),

            // ═══════════════════════════════════════════════════════
            // 化工/辅料（原材料）
            // ═══════════════════════════════════════════════════════
            CreateMaterial("MAT-C001", "无铅锡膏 SAC305",        "500g/罐 Sn96.5Ag3Cu0.5","外购","A", g,   g,   g,   g,   whRm02, rm02D0101, true,  true,  true,  50000m,  10000m, 0, 0, 500m,   500m,   now, company, user, grpChm),
            CreateMaterial("MAT-C002", "助焊剂 免清洗型",        "1L/瓶",                "外购", "B", l,   l,   l,   l,   whRm02, rm02D0101, true,  true,  true,  200m,    50m,    0, 0, 10m,    5m,     now, company, user, grpChm),
            CreateMaterial("MAT-C003", "导热硅脂 HY-510",        "100g/支",              "外购", "C", g,   g,   g,   g,   whRm02, rm02C0101, false, false, false, 5000m,   1000m,  0, 0, 100m,   100m,   now, company, user, grpChm),
            CreateMaterial("MAT-C004", "环氧树脂胶 AB胶",        "50ml/支",              "外购", "C", pcs, pcs, pcs, pcs, whRm02, rm02C0101, false, true,  true,  500m,    100m,   0, 0, 20m,    10m,    now, company, user, grpChm),

            // ═══════════════════════════════════════════════════════
            // PCB 板（原材料）
            // ═══════════════════════════════════════════════════════
            CreateMaterial("MAT-P001", "主控板 PCB",              "FR-4 4层 1.6mm 沉金", "外购", "A", pcs, pcs, pcs, pcs, whRm01, rm01A0101, true,  true,  true,  10000m,  2000m,  0.025m, 0.026m, 500m, 100m, now, company, user, grpPcb),
            CreateMaterial("MAT-P002", "电源板 PCB",              "FR-4 2层 1.0mm HAL",  "外购", "B", pcs, pcs, pcs, pcs, whRm01, rm01A0101, true,  true,  true,  8000m,   1500m,  0.015m, 0.016m, 500m, 100m, now, company, user, grpPcb),
            CreateMaterial("MAT-P003", "显示板 PCB",              "FR-4 2层 0.8mm 沉金", "外购", "B", pcs, pcs, pcs, pcs, whRm01, rm01A0102, true,  true,  true,  6000m,   1000m,  0.012m, 0.013m, 500m, 100m, now, company, user, grpPcb),

            // ═══════════════════════════════════════════════════════
            // 半成品
            // ═══════════════════════════════════════════════════════
            CreateMaterial("SFP-001",  "主控板 PCBA",             "SMT+DIP 完成品",      "自制", "A", pcs, pcs, pcs, pcs, whWip01, "",        false, true,  true,  5000m,   1000m,  0.035m, 0.036m, 0m,   0m,   now, company, user, grpSfp),
            CreateMaterial("SFP-002",  "电源板 PCBA",             "SMT 完成品",          "自制", "A", pcs, pcs, pcs, pcs, whWip01, "",        false, true,  true,  4000m,   800m,   0.020m, 0.021m, 0m,   0m,   now, company, user, grpSfp),
            CreateMaterial("SFP-003",  "显示模组总成",            "LCD+触摸屏+FPC",      "自制", "B", pcs, pcs, pcs, pcs, whWip01, "",        false, true,  false, 3000m,   500m,   0.045m, 0.048m, 0m,   0m,   now, company, user, grpSfp),

            // ═══════════════════════════════════════════════════════
            // 成品
            // ═══════════════════════════════════════════════════════
            CreateMaterial("FG-001",   "智能控制器 OC-200",       "标准版 100-240V",     "自制", "A", pcs, pcs, pcs, set, whFg01,  fg01P0101, false, true,  true,  3000m,   500m,   0.35m,  0.52m,  0m,   0m,   now, company, user, grpFg),
            CreateMaterial("FG-002",   "智能控制器 OC-200P",      "增强版 PoE供电",      "自制", "A", pcs, pcs, pcs, set, whFg01,  fg01P0101, false, true,  true,  2000m,   300m,   0.38m,  0.55m,  0m,   0m,   now, company, user, grpFg),
            CreateMaterial("FG-003",   "智能网关 OG-500",         "工业级 双网口",       "自制", "A", pcs, pcs, pcs, set, whFg01,  fg01P0201, false, true,  true,  1500m,   200m,   0.85m,  1.20m,  0m,   0m,   now, company, user, grpFg),
            CreateMaterial("FG-004",   "智能传感器 OS-100T",      "温湿度型 RS485",      "自制", "B", pcs, pcs, pcs, box, whFg01,  fg01P0201, false, true,  true,  5000m,   800m,   0.05m,  0.08m,  0m,   0m,   now, company, user, grpFg),

            // ═══════════════════════════════════════════════════════
            // 包装材料
            // ═══════════════════════════════════════════════════════
            CreateMaterial("PKG-001",  "OC-200 包装彩盒",         "250×180×80mm 350g铜版纸","外购","C", pcs, pcs, pcs, pcs, whPk01, "", false, false, false, 10000m, 2000m, 0, 0, 500m, 500m, now, company, user, grpPkg),
            CreateMaterial("PKG-002",  "OG-500 包装彩盒",         "350×250×120mm 350g铜版纸","外购","C", pcs, pcs, pcs, pcs, whPk01, "", false, false, false, 5000m,  1000m, 0, 0, 500m, 500m, now, company, user, grpPkg),
            CreateMaterial("PKG-003",  "EPE 珍珠棉内衬",          "定制型 20mm厚",       "外购", "C", pcs, pcs, pcs, pcs, whPk01,  "",       false, false, false, 10000m,  2000m,  0, 0, 500m,   500m,   now, company, user, grpPkg),
            CreateMaterial("PKG-004",  "产品标签 不干胶",         "60×40mm 铜版纸",      "外购", "C", rol, rol, rol, rol, whPk01,  "",       false, false, false, 200m,    50m,    0, 0, 10m,    5m,     now, company, user, grpPkg),
            CreateMaterial("PKG-005",  "外箱 五层瓦楞纸",         "600×400×350mm",       "外购", "C", pcs, pcs, pcs, pcs, whPk01,  "",       false, false, false, 3000m,   500m,   0, 0, 100m,   50m,    now, company, user, grpPkg),
        };

        // 幂等：只插入数据库中不存在的记录（按 FNumber 判断）
        var existingNumbers = await _db.Queryable<TBdMaterial>()
            .Where(m => !m.FDeleted)
            .Select(m => m.FNumber)
            .ToListAsync();

        var existingSet = new HashSet<string>(existingNumbers);
        var missing = materials.Where(m => !existingSet.Contains(m.FNumber)).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static TBdMaterial CreateMaterial(
        string number, string name, string spec, string erpClsId, string abc,
        string baseUnitId, string storeUnitId, string saleUnitId, string purchaseUnitId,
        string deStockId, string deSpId,
        bool isBatchManage, bool isKfPeriod, bool checkIncoming,
        decimal maxQty, decimal safeQty, decimal netWeight, decimal grossWeight,
        decimal minPoQty, decimal increaseQty,
        DateTime now, string company, string user, string groupId = "")
    {
        return new TBdMaterial
        {
            Uid = Guid.NewGuid().ToString("N"),
            FInterId = $"MAT_{number}",
            FNumber = number,
            FName = name,
            FSpecification = spec,
            FErpClsId = erpClsId,
            FAbc = abc,
            FBaseUnitId = baseUnitId,
            FStoreUnitId = storeUnitId,
            FSaleUnitId = saleUnitId,
            FPurchaseUnitId = purchaseUnitId,
            FDeStockId = deStockId,
            FDeSpId = deSpId,
            FGroupId = groupId,
            FIsBatchManage = isBatchManage,
            FIsKfPeriod = isKfPeriod,
            FKfUnit = isKfPeriod ? 1 : 0,
            FKfPeriod = isKfPeriod ? 365 : 0,
            FCheckIncoming = checkIncoming,
            FMaxQty = maxQty,
            FSafeQty = safeQty,
            FNetWeight = netWeight,
            FGrossWeight = grossWeight,
            FMinPoQty = minPoQty,
            FIncreaseQty = increaseQty,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 物料条码类型种子数据

    private async Task SeedMaterialBarTypesAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        var barTypes = new List<TBdMaterialbartype>
        {
            // 电子元器件 — 单品条码（逐个追溯）
            CreateMaterialBarType("MAT-E001", "贴片电阻 0402 10KΩ",    1, now, company, user),
            CreateMaterialBarType("MAT-E002", "贴片电容 0603 100nF",    1, now, company, user),
            CreateMaterialBarType("MAT-E003", "STM32F103C8T6 芯片",     1, now, company, user),
            CreateMaterialBarType("MAT-E004", "USB Type-C 连接器",      2, now, company, user),
            CreateMaterialBarType("MAT-E005", "3.3V 稳压芯片 AMS1117", 2, now, company, user),
            CreateMaterialBarType("MAT-E006", "LED 发光二极管 红色",     3, now, company, user),

            // 金属/结构件 — 批次条码
            CreateMaterialBarType("MAT-M001", "铝合金板材 6061-T6",     3, now, company, user),
            CreateMaterialBarType("MAT-M002", "不锈钢板 304",           3, now, company, user),
            CreateMaterialBarType("MAT-M003", "铜线 T2 紫铜",           3, now, company, user),
            CreateMaterialBarType("MAT-M004", "锌合金压铸件-外壳A",     2, now, company, user),

            // 化工/辅料 — 批次条码
            CreateMaterialBarType("MAT-C001", "无铅锡膏 SAC305",        3, now, company, user),
            CreateMaterialBarType("MAT-C002", "助焊剂 免清洗型",        3, now, company, user),

            // PCB 板 — 最小包装量条码
            CreateMaterialBarType("MAT-P001", "主控板 PCB",              2, now, company, user),
            CreateMaterialBarType("MAT-P002", "电源板 PCB",              2, now, company, user),
            CreateMaterialBarType("MAT-P003", "显示板 PCB",              2, now, company, user),

            // 半成品 — 单品条码（逐个追溯）
            CreateMaterialBarType("SFP-001",  "主控板 PCBA",             1, now, company, user),
            CreateMaterialBarType("SFP-002",  "电源板 PCBA",             1, now, company, user),
            CreateMaterialBarType("SFP-003",  "显示模组总成",            1, now, company, user),

            // 成品 — 单品条码
            CreateMaterialBarType("FG-001",   "智能控制器 OC-200",       1, now, company, user),
            CreateMaterialBarType("FG-002",   "智能控制器 OC-200P",      1, now, company, user),
            CreateMaterialBarType("FG-003",   "智能网关 OG-500",         1, now, company, user),
            CreateMaterialBarType("FG-004",   "智能传感器 OS-100T",      1, now, company, user),

            // 包装材料 — 批次条码
            CreateMaterialBarType("PKG-001",  "OC-200 包装彩盒",         3, now, company, user),
            CreateMaterialBarType("PKG-002",  "OG-500 包装彩盒",         3, now, company, user),
            CreateMaterialBarType("PKG-003",  "EPE 珍珠棉内衬",          3, now, company, user),
        };

        // 幂等：按物料编码+条码类型判断是否已存在
        var existing = await _db.Queryable<TBdMaterialbartype>()
            .Where(b => !b.FDeleted)
            .Select(b => new { b.Fmaterialnumber, b.Fbartype })
            .ToListAsync();

        var existingSet = new HashSet<string>(existing.Select(e => $"{e.Fmaterialnumber}_{e.Fbartype}"));
        var missing = barTypes.Where(b => !existingSet.Contains($"{b.Fmaterialnumber}_{b.Fbartype}")).ToList();

        if (missing.Count > 0)
        {
            await _db.Insertable(missing).ExecuteCommandAsync();
        }
    }

    private static TBdMaterialbartype CreateMaterialBarType(
        string materialNumber, string materialName, int barType,
        DateTime now, string company, string user)
    {
        // 条码类型 → 分组：1=单品条码 2=最小包装量条码 3=批次条码
        var groupId = barType switch
        {
            1 => GroupUid("MBT-SINGLE"),
            2 => GroupUid("MBT-PKG"),
            3 => GroupUid("MBT-BATCH"),
            _ => string.Empty
        };

        return new TBdMaterialbartype
        {
            Uid = SeedUid("MBT", $"{materialNumber}_{barType}"),
            FInterId = $"MBT_{materialNumber}_{barType}",
            // 规范关系：FMATERIALID = 物料 FInterId（CreateMaterial 用 $"MAT_{number}"），否则前端取规格/序列号关联不到
            Fmaterialid = $"MAT_{materialNumber}",
            Fbartype = barType,
            Fmaterialnumber = materialNumber,
            Fmaterialname = materialName,
            FGroupId = groupId,
            // 未审核（FStatus=0）→ 不写审核痕迹（FCheckDate 默认 MinValue / FCheckerId 默认空），与 Currency 种子一致
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

    #region 辅助资料种子数据

    private static string AuxDataUid(string number) => SeedUid("AUXDATA", number);
    private static string AuxEntryUid(string number) => SeedUid("AUXENTRY", number);
    private static string GroupUid(string number) => SeedUid("GROUP", number);

    private async Task SeedAssistantDataAsync()
    {
        var now = DateTime.Now;
        const string company = "DEFAULT";
        const string user = "demo-seed";

        // ═══════════════════════════════════════════════════════
        // 辅助资料类别
        // ═══════════════════════════════════════════════════════
        var categories = new List<TBasAssistantdata>
        {
            CreateAssistantData("AUX-001", "结算方式",   "结算方式分类",     "", true,  true,  now, company, user),
            CreateAssistantData("AUX-002", "运输方式",   "运输方式分类",     "", true,  true,  now, company, user),
            CreateAssistantData("AUX-003", "收货方式",   "收货方式分类",     "", true,  false, now, company, user),
            CreateAssistantData("AUX-004", "付款条件",   "付款条件分类",     "", true,  true,  now, company, user),
            CreateAssistantData("AUX-005", "不良原因",   "品质不良原因分类", "", true,  false, now, company, user),
            CreateAssistantData("AUX-006", "包装方式",   "包装方式分类",     "", false, false, now, company, user),
            CreateAssistantData("AUX-007", "产品用途",   "产品用途分类",     "", false, false, now, company, user),
            CreateAssistantData("AUX-008", "退货原因",   "退货原因分类",     "", false, false, now, company, user),
            CreateAssistantData("AUX-009", "费用类型",   "费用类型分类",     "", true,  true,  now, company, user),
            CreateAssistantData("AUX-010", "交货方式",   "交货方式分类",     "", true,  false, now, company, user),
        };

        // 幂等：只插入数据库中不存在的记录（按 Fnumber 判断）
        var existingCatNumbers = await _db.Queryable<TBasAssistantdata>()
            .Where(a => !a.FDeleted)
            .Select(a => a.Fnumber)
            .ToListAsync();

        var existingCatSet = new HashSet<string>(existingCatNumbers);
        var missingCats = categories.Where(a => !existingCatSet.Contains(a.Fnumber)).ToList();

        if (missingCats.Count > 0)
        {
            await _db.Insertable(missingCats).ExecuteCommandAsync();
        }

        // ═══════════════════════════════════════════════════════
        // 辅助资料明细
        // ═══════════════════════════════════════════════════════
        var entries = new List<TBasAssistantdataentry>
        {
            // 结算方式
            CreateAssistantDataEntry("AUXE-001", "现金",         AuxDataUid("AUX-001"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-002", "银行转账",     AuxDataUid("AUX-001"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-003", "支票",         AuxDataUid("AUX-001"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-004", "承兑汇票",     AuxDataUid("AUX-001"), "", 4, now, company, user),
            CreateAssistantDataEntry("AUXE-005", "信用证",       AuxDataUid("AUX-001"), "", 5, now, company, user),

            // 运输方式
            CreateAssistantDataEntry("AUXE-010", "公路运输",     AuxDataUid("AUX-002"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-011", "铁路运输",     AuxDataUid("AUX-002"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-012", "航空运输",     AuxDataUid("AUX-002"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-013", "海运",         AuxDataUid("AUX-002"), "", 4, now, company, user),
            CreateAssistantDataEntry("AUXE-014", "快递",         AuxDataUid("AUX-002"), "", 5, now, company, user),
            CreateAssistantDataEntry("AUXE-015", "自提",         AuxDataUid("AUX-002"), "", 6, now, company, user),

            // 收货方式
            CreateAssistantDataEntry("AUXE-020", "送货上门",     AuxDataUid("AUX-003"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-021", "客户自提",     AuxDataUid("AUX-003"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-022", "第三方物流",   AuxDataUid("AUX-003"), "", 3, now, company, user),

            // 付款条件
            CreateAssistantDataEntry("AUXE-030", "货到付款",     AuxDataUid("AUX-004"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-031", "月结30天",     AuxDataUid("AUX-004"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-032", "月结60天",     AuxDataUid("AUX-004"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-033", "月结90天",     AuxDataUid("AUX-004"), "", 4, now, company, user),
            CreateAssistantDataEntry("AUXE-034", "预付款",       AuxDataUid("AUX-004"), "", 5, now, company, user),
            CreateAssistantDataEntry("AUXE-035", "分期付款",     AuxDataUid("AUX-004"), "", 6, now, company, user),

            // 不良原因
            CreateAssistantDataEntry("AUXE-040", "外观不良",     AuxDataUid("AUX-005"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-041", "尺寸超差",     AuxDataUid("AUX-005"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-042", "功能异常",     AuxDataUid("AUX-005"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-043", "材质不符",     AuxDataUid("AUX-005"), "", 4, now, company, user),
            CreateAssistantDataEntry("AUXE-044", "包装破损",     AuxDataUid("AUX-005"), "", 5, now, company, user),
            CreateAssistantDataEntry("AUXE-045", "数量短缺",     AuxDataUid("AUX-005"), "", 6, now, company, user),
            CreateAssistantDataEntry("AUXE-046", "混料",         AuxDataUid("AUX-005"), "", 7, now, company, user),

            // 包装方式
            CreateAssistantDataEntry("AUXE-050", "纸箱包装",     AuxDataUid("AUX-006"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-051", "木箱包装",     AuxDataUid("AUX-006"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-052", "托盘包装",     AuxDataUid("AUX-006"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-053", "真空包装",     AuxDataUid("AUX-006"), "", 4, now, company, user),
            CreateAssistantDataEntry("AUXE-054", "散装",         AuxDataUid("AUX-006"), "", 5, now, company, user),

            // 产品用途
            CreateAssistantDataEntry("AUXE-060", "工业用",       AuxDataUid("AUX-007"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-061", "民用",         AuxDataUid("AUX-007"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-062", "军工用",       AuxDataUid("AUX-007"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-063", "医疗用",       AuxDataUid("AUX-007"), "", 4, now, company, user),
            CreateAssistantDataEntry("AUXE-064", "汽车用",       AuxDataUid("AUX-007"), "", 5, now, company, user),

            // 退货原因
            CreateAssistantDataEntry("AUXE-070", "质量问题",     AuxDataUid("AUX-008"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-071", "发错货",       AuxDataUid("AUX-008"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-072", "多发货",       AuxDataUid("AUX-008"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-073", "客户取消订单", AuxDataUid("AUX-008"), "", 4, now, company, user),
            CreateAssistantDataEntry("AUXE-074", "运输损坏",     AuxDataUid("AUX-008"), "", 5, now, company, user),

            // 费用类型
            CreateAssistantDataEntry("AUXE-080", "运费",         AuxDataUid("AUX-009"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-081", "包装费",       AuxDataUid("AUX-009"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-082", "保险费",       AuxDataUid("AUX-009"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-083", "报关费",       AuxDataUid("AUX-009"), "", 4, now, company, user),
            CreateAssistantDataEntry("AUXE-084", "仓储费",       AuxDataUid("AUX-009"), "", 5, now, company, user),
            CreateAssistantDataEntry("AUXE-085", "装卸费",       AuxDataUid("AUX-009"), "", 6, now, company, user),

            // 交货方式
            CreateAssistantDataEntry("AUXE-090", "工厂交货(EXW)",   AuxDataUid("AUX-010"), "", 1, now, company, user),
            CreateAssistantDataEntry("AUXE-091", "离岸价(FOB)",     AuxDataUid("AUX-010"), "", 2, now, company, user),
            CreateAssistantDataEntry("AUXE-092", "到岸价(CIF)",     AuxDataUid("AUX-010"), "", 3, now, company, user),
            CreateAssistantDataEntry("AUXE-093", "门到门(DDU)",     AuxDataUid("AUX-010"), "", 4, now, company, user),
        };

        // 幂等：只插入数据库中不存在的记录（按 Fnumber 判断）
        var existingEntryNumbers = await _db.Queryable<TBasAssistantdataentry>()
            .Where(e => !e.FDeleted)
            .Select(e => e.Fnumber)
            .ToListAsync();

        var existingEntrySet = new HashSet<string>(existingEntryNumbers);
        var missingEntries = entries.Where(e => !existingEntrySet.Contains(e.Fnumber)).ToList();

        if (missingEntries.Count > 0)
        {
            await _db.Insertable(missingEntries).ExecuteCommandAsync();
        }
    }

    private static TBasAssistantdata CreateAssistantData(
        string number, string name, string description, string parentId,
        bool isDefault, bool isO3Use,
        DateTime now, string company, string user)
    {
        return new TBasAssistantdata
        {
            Uid = AuxDataUid(number),
            FInterId = $"AUX_{number}",
            Fnumber = number,
            Fname = name,
            Fdescription = description,
            Fparentid = parentId,
            Ftopclassid = "",
            Isdefault = isDefault,
            Fiso3use = isO3Use,
            FCheckerId = user,
            FCheckDate = now,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    private static TBasAssistantdataentry CreateAssistantDataEntry(
        string number, string dataValue, string fid, string parentId, int seq,
        DateTime now, string company, string user)
    {
        return new TBasAssistantdataentry
        {
            Uid = AuxEntryUid(number),
            FInterId = $"AUXE_{number}",
            Fnumber = number,
            Fdatavalue = dataValue,
            Fid = fid,
            Fparentid = parentId,
            Fdescription = "",
            Fseq = seq,
            Isdefault = false,
            Fuseorgid = company,
            FCheckerId = user,
            FCheckDate = now,
            Fisbuildself = false,
            FCompanyId = company,
            FStatus = 0,
            FDeleted = false,
            CYmd = now,
            CUser = user,
            MYmd = now,
            MUser = user
        };
    }

    #endregion

}
