namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 采购退料单 / 外购退料（真实表 T_PUR_MRB + T_PUR_MRBENTRY[物料汇总] + ENTRY1[录入条码] + ENTRY2[底阶条码]）=====
// 录入类型 Ftypeid：1=物料（直接录物料汇总明细）；2=条码（扫码写 ENTRY1/ENTRY2，服务端按物料汇总到 ENTRY）
// 镜像采购入库单，但语义为「出库」：审核=退料出库过账（冲减库存、条码已入库→未入库），反审核=冲回。

/// <summary>
/// 采购退料单列表项（按物料汇总明细 ENTRY 行展开，一条明细一行，带解析后的名称列，参照设计列表）
/// </summary>
public class PurchaseReturnListDto
{
    /// <summary>主表 Uid（用于进入编辑）</summary>
    public string Uid { get; set; } = string.Empty;
    /// <summary>明细 Uid</summary>
    public string EntryUid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    /// <summary>退料日期</summary>
    public DateTime? Fdate { get; set; }
    public string FbilltypeName { get; set; } = string.Empty;
    /// <summary>录入类型 1=物料 2=条码</summary>
    public int Ftypeid { get; set; }
    public string FtypeName { get; set; } = string.Empty;
    /// <summary>行号</summary>
    public int? Fentryid { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    public bool FDisabled { get; set; }
    // 供应商
    public string Fsupplyid { get; set; } = string.Empty;
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;
    // 退料部门（列表"部门名称"列）
    public string FmrdeptName { get; set; } = string.Empty;
    // 订单编号
    public string Forderbillno { get; set; } = string.Empty;
    // 物料
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    // 批次 / 实退数量 / 换算率 / 单位
    public string Flot { get; set; } = string.Empty;
    public decimal Frmrealqty { get; set; }
    public decimal Fcoefficient { get; set; }
    public string FunitName { get; set; } = string.Empty;
    public string FbaseunitName { get; set; } = string.Empty;
    // 生产日期/采购日期
    public DateTime? Fkfdate { get; set; }
    // ERP单据编号
    public string Ferpno { get; set; } = string.Empty;
    // 组织（FCOMPANYID -> SYS_ORGSTRUCTURE）
    public string FcompanyName { get; set; } = string.Empty;
    // 制单 / 审核
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
}

/// <summary>
/// 物料汇总明细行（T_PUR_MRBENTRY）
/// </summary>
public class PurchaseReturnMaterialEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int? Fentryid { get; set; }
    // 物料
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    /// <summary>启用批次管理（物料带出，只读）</summary>
    public bool FisBatchManage { get; set; }
    /// <summary>启用保质期（物料带出，只读）</summary>
    public bool FisKfPeriod { get; set; }
    public int FKfPeriod { get; set; }
    public int FKfUnit { get; set; }
    // 辅助属性
    public string Fauxpropid { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;
    public bool FisAuxEnabled { get; set; }
    // 批次
    public string Flot { get; set; } = string.Empty;
    // 仓库 / 仓位
    public string Fstockid { get; set; } = string.Empty;
    public string FstockNumber { get; set; } = string.Empty;
    public string FstockName { get; set; } = string.Empty;
    public bool FisOpenLocation { get; set; }
    public string Fstocklocid { get; set; } = string.Empty;
    public string FstocklocName { get; set; } = string.Empty;
    // 库存状态
    public string Fstockstatusid { get; set; } = string.Empty;
    public string FstockstatusName { get; set; } = string.Empty;
    // 实退数量 / 补料数量 / 扣款数量 / 单位 / 换算率
    public decimal Frmrealqty { get; set; }
    public decimal Freplenishqty { get; set; }
    public decimal Fkeapamtqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string FunitNumber { get; set; } = string.Empty;
    public string FunitName { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    public decimal Fcoefficient { get; set; }
    // 辅助单位
    public string Fsecunitid { get; set; } = string.Empty;
    public decimal Fsecunitqty { get; set; }
    // 日期
    public DateTime? Fkfdate { get; set; }
    public DateTime? Fusefuldate { get; set; }
    // 价格
    public decimal Fprice { get; set; }
    public decimal Ftaxprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Fdiscountrate { get; set; }
    public decimal Fdiscount { get; set; }
    public decimal Ftaxamount { get; set; }
    public decimal Famount { get; set; }
    public decimal Fallamount { get; set; }
    // 源单 / 订单（追溯）
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public int Fsrcentryid { get; set; }
    public string Forderbillno { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
}

/// <summary>
/// 录入明细行（T_PUR_MRBENTRY1，条码级；扫码每码一行）
/// </summary>
public class PurchaseReturnBarcodeEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int Fentryid { get; set; }
    /// <summary>录入类型（同表头）</summary>
    public int Ftypeid { get; set; }
    /// <summary>是否箱码</summary>
    public bool Fisbox { get; set; }
    public string Fboxbarcode { get; set; } = string.Empty;
    public string Fbarcode { get; set; } = string.Empty;
    /// <summary>条码类型 1=单品 2=最小包装 3=批次</summary>
    public int Fbartype { get; set; }
    // 物料
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    public bool FisBatchManage { get; set; }
    // 物料带出：保质期 / 辅助单位启用（只读展示）
    public bool FisKfPeriod { get; set; }
    public int FKfPeriod { get; set; }
    public int FKfUnit { get; set; }
    public bool FisSecUnit { get; set; }
    // 辅助属性
    public string Fauxpropid { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;
    // 批次
    public string Flot { get; set; } = string.Empty;
    // 仓库 / 仓位
    public string Fstockid { get; set; } = string.Empty;
    public string FstockNumber { get; set; } = string.Empty;
    public string FstockName { get; set; } = string.Empty;
    public bool FisOpenLocation { get; set; }
    public string Fstocklocid { get; set; } = string.Empty;
    public string FstocklocName { get; set; } = string.Empty;
    // 数量 / 单位
    public decimal Fqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string FunitNumber { get; set; } = string.Empty;
    public string FunitName { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    // 辅助单位
    public string Fsecunitid { get; set; } = string.Empty;
    public string FsecunitNumber { get; set; } = string.Empty;
    public string FsecunitName { get; set; } = string.Empty;
    public decimal Fsecunitqty { get; set; }
    // 库存状态
    public string Fstockstatusid { get; set; } = string.Empty;
    public string FstockstatusName { get; set; } = string.Empty;
    // 供应商
    public string Fsupplyid { get; set; } = string.Empty;
    // 日期
    public DateTime? Fkfdate { get; set; }
    public DateTime? Fusefuldate { get; set; }
    // 价格
    public decimal Ftaxprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Fdiscountrate { get; set; }
    // 源单 / 订单（追溯）
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public int Fsrcentryid { get; set; }
    public string Fsrcdetailid { get; set; } = string.Empty;
    public string Fordertypeid { get; set; } = string.Empty;
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderbillno { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
}

/// <summary>
/// 底阶条码明细行（T_PUR_MRBENTRY2；箱码展开的子条码 / 非箱码即条码本身）
/// </summary>
public class PurchaseReturnBottomEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int Fentryid { get; set; }
    /// <summary>所属箱码（非箱码时为空）</summary>
    public string Fboxbarcode { get; set; } = string.Empty;
    public string Fbarcode { get; set; } = string.Empty;
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
    public DateTime? Fkfdate { get; set; }
    public DateTime? Fusefuldate { get; set; }
    public decimal Fqty { get; set; }
    public string Fstockid { get; set; } = string.Empty;
    public string Fstocklocid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Funitid { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    public decimal Ftaxprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Fdiscountrate { get; set; }
    public string Fstockstatusid { get; set; } = string.Empty;
    // 源单 / 订单
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public int Fsrcentryid { get; set; }
    public string Fsrcdetailid { get; set; } = string.Empty;
    public string Fordertypeid { get; set; } = string.Empty;
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderbillno { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
}

/// <summary>
/// 采购退料单详情（主表 + 名称解析 + 三套明细）
/// </summary>
public class PurchaseReturnDetailDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public string Fbilltypeid { get; set; } = string.Empty;
    public string FbilltypeName { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    /// <summary>录入类型 1=物料 2=条码</summary>
    public int Ftypeid { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    // 退料类型 A=检验退料 B=库存退料 / 退料方式 A=退料补料 B=退料并扣款 / 补料方式 / 退料原因
    public string Fmrtype { get; set; } = string.Empty;
    public string Fmrmode { get; set; } = string.Empty;
    public string Freplenishmode { get; set; } = string.Empty;
    public string Fmrreason { get; set; } = string.Empty;
    // 源单
    public string Fsrcformid { get; set; } = string.Empty;
    public string FsrcformName { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    // 组织：需求 / 采购 / 库存(=单据所属组织 FCompanyId)
    public string Frequireorgid { get; set; } = string.Empty;
    public string FrequireorgName { get; set; } = string.Empty;
    public string Fpurchaseorgid { get; set; } = string.Empty;
    public string FpurchaseorgName { get; set; } = string.Empty;
    public string Fcompanyid { get; set; } = string.Empty;
    public string FcompanyName { get; set; } = string.Empty;
    // 部门：采购 / 退料
    public string Fpurchasedeptid { get; set; } = string.Empty;
    public string FpurchasedeptName { get; set; } = string.Empty;
    public string Fmrdeptid { get; set; } = string.Empty;
    public string FmrdeptName { get; set; } = string.Empty;
    // 采购员 / 仓管员（均走 employee）
    public string Fpurchaserid { get; set; } = string.Empty;
    public string FpurchaserName { get; set; } = string.Empty;
    public string Fstockerid { get; set; } = string.Empty;
    public string FstockerName { get; set; } = string.Empty;
    // 供应商
    public string Fsupplyid { get; set; } = string.Empty;
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;
    // 币别 / 汇率
    public string Fcurrencyid { get; set; } = string.Empty;
    public string FcurrencyNumber { get; set; } = string.Empty;
    public string FcurrencyName { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public string Fexchangerate { get; set; } = string.Empty;
    // 表头仓库 / 仓位 / 库存状态（默认值，明细可各自覆盖）
    public string Fstockid { get; set; } = string.Empty;
    public string FstockNumber { get; set; } = string.Empty;
    public string FstockName { get; set; } = string.Empty;
    public bool FisOpenLocation { get; set; }
    public string Fstocklocid { get; set; } = string.Empty;
    public string FstocklocName { get; set; } = string.Empty;
    public string Fstockstatusid { get; set; } = string.Empty;
    public string FstockstatusName { get; set; } = string.Empty;
    // ---- 其他页签：制单/审核/修改/禁用 ----
    public string CUser { get; set; } = string.Empty;
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string Fcheckerid { get; set; } = string.Empty;
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
    public string MUser { get; set; } = string.Empty;
    public string MuserName { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public string FdisableName { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
    public bool FDisabled { get; set; }
    // 三套明细
    public List<PurchaseReturnMaterialEntryDto> Entries { get; set; } = new();
    public List<PurchaseReturnBarcodeEntryDto> BarcodeEntries { get; set; } = new();
    public List<PurchaseReturnBottomEntryDto> BottomEntries { get; set; } = new();
}

// ===== 写入请求 =====

public class CreatePurchaseReturnMaterialEntryRequest
{
    public string Fmaterialid { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
    public string Fstockid { get; set; } = string.Empty;
    public string Fstocklocid { get; set; } = string.Empty;
    public string Fstockstatusid { get; set; } = string.Empty;
    public decimal Frmrealqty { get; set; }
    public decimal Freplenishqty { get; set; }
    public decimal Fkeapamtqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    public decimal Fcoefficient { get; set; }
    public string Fsecunitid { get; set; } = string.Empty;
    public decimal Fsecunitqty { get; set; }
    public DateTime? Fkfdate { get; set; }
    public DateTime? Fusefuldate { get; set; }
    public decimal Fprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Fdiscountrate { get; set; }
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public int Fsrcentryid { get; set; }
    public string Forderbillno { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
}

public class CreatePurchaseReturnBarcodeEntryRequest
{
    public bool Fisbox { get; set; }
    public string Fboxbarcode { get; set; } = string.Empty;
    public string Fbarcode { get; set; } = string.Empty;
    public int Fbartype { get; set; }
    public string Fmaterialid { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
    public string Fstockid { get; set; } = string.Empty;
    public string Fstocklocid { get; set; } = string.Empty;
    public string Fstockstatusid { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    public string Fsecunitid { get; set; } = string.Empty;
    public decimal Fsecunitqty { get; set; }
    public string Fsupplyid { get; set; } = string.Empty;
    public DateTime? Fkfdate { get; set; }
    public DateTime? Fusefuldate { get; set; }
    public decimal Ftaxprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Fdiscountrate { get; set; }
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public int Fsrcentryid { get; set; }
    public string Fsrcdetailid { get; set; } = string.Empty;
    public string Fordertypeid { get; set; } = string.Empty;
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderbillno { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
    // 底阶条码 ENTRY2 不由前端携带：保存时服务端按本录入行（箱码重查装箱清单 / 非箱码镜像）重新展开。
}

public class CreatePurchaseReturnRequest
{
    public string Fbillno { get; set; } = string.Empty;
    public string Fbilltypeid { get; set; } = string.Empty;
    /// <summary>录入类型 1=物料 2=条码</summary>
    public int Ftypeid { get; set; } = 1;
    public DateTime? Fdate { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Fmrtype { get; set; } = string.Empty;
    public string Fmrmode { get; set; } = string.Empty;
    public string Freplenishmode { get; set; } = string.Empty;
    public string Fmrreason { get; set; } = string.Empty;
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public string Frequireorgid { get; set; } = string.Empty;
    public string Fpurchaseorgid { get; set; } = string.Empty;
    /// <summary>库存组织（=单据所属组织 FCompanyId）</summary>
    public string Fcompanyid { get; set; } = string.Empty;
    public string Fpurchasedeptid { get; set; } = string.Empty;
    public string Fmrdeptid { get; set; } = string.Empty;
    public string Fpurchaserid { get; set; } = string.Empty;
    public string Fstockerid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public string Fexchangerate { get; set; } = string.Empty;
    // 表头仓库/仓位/库存状态（默认值）
    public string Fstockid { get; set; } = string.Empty;
    public string Fstocklocid { get; set; } = string.Empty;
    public string Fstockstatusid { get; set; } = string.Empty;
    // 物料汇总明细（录入类型=物料 时由前端录入；=条码 时服务端按 BarcodeEntries 汇总，忽略此处传值）
    public List<CreatePurchaseReturnMaterialEntryRequest> Entries { get; set; } = new();
    // 录入明细（录入类型=条码 时扫码生成）
    public List<CreatePurchaseReturnBarcodeEntryRequest> BarcodeEntries { get; set; } = new();
}

public class UpdatePurchaseReturnRequest
{
    public string Fbilltypeid { get; set; } = string.Empty;
    public int Ftypeid { get; set; } = 1;
    public DateTime? Fdate { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Fmrtype { get; set; } = string.Empty;
    public string Fmrmode { get; set; } = string.Empty;
    public string Freplenishmode { get; set; } = string.Empty;
    public string Fmrreason { get; set; } = string.Empty;
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public string Frequireorgid { get; set; } = string.Empty;
    public string Fpurchaseorgid { get; set; } = string.Empty;
    /// <summary>库存组织（=单据所属组织 FCompanyId）</summary>
    public string Fcompanyid { get; set; } = string.Empty;
    public string Fpurchasedeptid { get; set; } = string.Empty;
    public string Fmrdeptid { get; set; } = string.Empty;
    public string Fpurchaserid { get; set; } = string.Empty;
    public string Fstockerid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public string Fexchangerate { get; set; } = string.Empty;
    public string Fstockid { get; set; } = string.Empty;
    public string Fstocklocid { get; set; } = string.Empty;
    public string Fstockstatusid { get; set; } = string.Empty;
    public List<CreatePurchaseReturnMaterialEntryRequest> Entries { get; set; } = new();
    public List<CreatePurchaseReturnBarcodeEntryRequest> BarcodeEntries { get; set; } = new();
}

// ===== 扫码解析（退料：扫已入库条码，校验在库性）=====

public class PurchaseReturnScanResultDto
{
    public bool Found { get; set; }
    public string Message { get; set; } = string.Empty;
    /// <summary>解析出的录入明细行（ENTRY1）</summary>
    public PurchaseReturnBarcodeEntryDto? Entry1 { get; set; }
    /// <summary>该录入行展开的底阶条码（ENTRY2）</summary>
    public List<PurchaseReturnBottomEntryDto> Entry2 { get; set; } = new();
}
