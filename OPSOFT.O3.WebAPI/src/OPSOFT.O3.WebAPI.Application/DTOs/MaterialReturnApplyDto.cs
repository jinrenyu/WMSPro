namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 退料申请单（真实表 T_PUR_MRAPP / T_PUR_MRAPPENTRY，一主一从）=====
// 业务类型 Fbusinesstype：CG标准采购/WW标准委外/ZCCG资产采购/ZYCG直运采购/FYCG费用采购/VMICG VMI采购/CSCG测试采购
// 退料类型 Frmtype：A检验退料/B库存退料  退料方式 Frmmode：A退料补料/B退料并扣款  补料方式 Freplenishmode：A按源单补料/B创建补料订单
// 产品类型 Frowtype：Parent套件父项/Service服务/Son套件子项/Standard标准产品

/// <summary>
/// 退料申请单列表项（按明细行展开，一条明细一行，带解析后的名称列，参照设计列表）
/// </summary>
public class MaterialReturnApplyListDto
{
    /// <summary>主表 Uid（用于进入编辑）</summary>
    public string Uid { get; set; } = string.Empty;
    /// <summary>明细 Uid</summary>
    public string EntryUid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string FbilltypeName { get; set; } = string.Empty;
    public int? Fentryid { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    public bool FDisabled { get; set; }
    // 业务类型 / 退料类型 / 退料方式
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Frmtype { get; set; } = string.Empty;
    public string Frmmode { get; set; } = string.Empty;
    // 组织 / 部门
    public string FrequireorgName { get; set; } = string.Empty;
    public string FappdeptName { get; set; } = string.Empty;
    public string FpurchaseorgName { get; set; } = string.Empty;
    public string FstockorgName { get; set; } = string.Empty;
    // 物料
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    // 批次 / 辅助属性
    public string Flot { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;
    // 数量
    public decimal Fmrappqty { get; set; }
    public decimal Fmrqty { get; set; }
    public decimal Fmrbqty { get; set; }
    public decimal Freplenishqty { get; set; }
    // 单位 / 库存单位
    public string FunitName { get; set; } = string.Empty;
    public string FstockunitNumber { get; set; } = string.Empty;
    public string FstockunitName { get; set; } = string.Empty;
    // 仓库 / 仓位
    public string FstockNumber { get; set; } = string.Empty;
    public string FstockName { get; set; } = string.Empty;
    public bool FisOpenLocation { get; set; }
    public string FstocklocName { get; set; } = string.Empty;
    // 金额
    public decimal Fprice { get; set; }
    public decimal Famount { get; set; }
    // 供应商
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;
    // 源单
    public string Fsrcbillno { get; set; } = string.Empty;
    // 制单 / 审核
    public string CuserName { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string FcheckerName { get; set; } = string.Empty;
    public DateTime? Fcheckdate { get; set; }
}

/// <summary>
/// 退料申请单明细行
/// </summary>
public class MaterialReturnApplyEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int? Fentryid { get; set; }
    /// <summary>产品类型 Parent/Service/Son/Standard</summary>
    public string Frowtype { get; set; } = string.Empty;
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    /// <summary>启用批次管理（物料带出，只读）</summary>
    public bool FisBatchManage { get; set; }
    /// <summary>启用保质期（物料带出，只读）</summary>
    public bool FisKfPeriod { get; set; }
    /// <summary>保质期限（物料带出，只读）</summary>
    public int FKfPeriod { get; set; }
    /// <summary>保质期单位 0=日/1=月/2=年（物料带出，只读）</summary>
    public int FKfUnit { get; set; }
    // 辅助属性
    public string Fauxpropid { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;
    /// <summary>该行物料是否启用辅助属性（前端据此决定辅助属性可选/只读）</summary>
    public bool FisAuxEnabled { get; set; }
    public string Flot { get; set; } = string.Empty;
    // 数量
    public decimal Fmrappqty { get; set; }
    public decimal Fmrqty { get; set; }
    public decimal Fmrbqty { get; set; }
    public decimal Freplenishqty { get; set; }
    public decimal Fstockqty { get; set; }
    public decimal Fbaseunitqty { get; set; }
    // 单位 / 库存单位 / 基本单位
    public string Funitid { get; set; } = string.Empty;
    public string FunitNumber { get; set; } = string.Empty;
    public string FunitName { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public string Fstockunitid { get; set; } = string.Empty;
    public string FstockunitName { get; set; } = string.Empty;
    // 仓库 / 仓位
    public string Fstockid { get; set; } = string.Empty;
    public string FstockNumber { get; set; } = string.Empty;
    public string FstockName { get; set; } = string.Empty;
    public bool FisOpenLocation { get; set; }
    public string Fstocklocid { get; set; } = string.Empty;
    public string FstocklocName { get; set; } = string.Empty;
    // 价格（金额服务端重算）
    public decimal Fprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Ftaxprice { get; set; }
    public decimal Fdiscountrate { get; set; }
    public decimal Fdiscount { get; set; }
    public decimal Ftaxamount { get; set; }
    public decimal Famount { get; set; }
    public decimal Fallamount { get; set; }
    // 源单追溯
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public string Forderbillno { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
}

/// <summary>
/// 退料申请单详情（主表 + 名称解析 + 明细）
/// </summary>
public class MaterialReturnApplyDetailDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public string Fbilltypeid { get; set; } = string.Empty;
    public string FbilltypeName { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    public string Foastatus { get; set; } = string.Empty;
    public string Foaresult { get; set; } = string.Empty;
    // 业务类型 / 退料类型 / 退料方式 / 补料方式（存码，前端字典映射）
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Frmtype { get; set; } = string.Empty;
    public string Frmmode { get; set; } = string.Empty;
    public string Freplenishmode { get; set; } = string.Empty;
    // 需求组织 / 采购组织 / 退料组织
    public string Frequireorgid { get; set; } = string.Empty;
    public string FrequireorgName { get; set; } = string.Empty;
    public string Fpurchaseorgid { get; set; } = string.Empty;
    public string FpurchaseorgName { get; set; } = string.Empty;
    public string Fstockorgid { get; set; } = string.Empty;
    public string FstockorgName { get; set; } = string.Empty;
    // 退料部门
    public string Fappdeptid { get; set; } = string.Empty;
    public string FappdeptName { get; set; } = string.Empty;
    // 供应商
    public string Fsupplyid { get; set; } = string.Empty;
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;
    // 币别 / 汇率
    public string Fcurrencyid { get; set; } = string.Empty;
    public string FcurrencyNumber { get; set; } = string.Empty;
    public string FcurrencyName { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; }
    public string Fnote { get; set; } = string.Empty;
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
    public List<MaterialReturnApplyEntryDto> Entries { get; set; } = new();
}

public class CreateMaterialReturnApplyEntryRequest
{
    public string Frowtype { get; set; } = string.Empty;
    public string Fmaterialid { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
    public decimal Fmrappqty { get; set; }
    public decimal Fmrqty { get; set; }
    public decimal Freplenishqty { get; set; }
    public decimal Fstockqty { get; set; }
    public decimal Fbaseunitqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public string Fstockunitid { get; set; } = string.Empty;
    public string Fstockid { get; set; } = string.Empty;
    public string Fstocklocid { get; set; } = string.Empty;
    public decimal Fprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Fdiscountrate { get; set; }
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public string Forderbillno { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
}

public class CreateMaterialReturnApplyRequest
{
    public string Fbillno { get; set; } = string.Empty;
    public string Fbilltypeid { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Frmtype { get; set; } = string.Empty;
    public string Frmmode { get; set; } = string.Empty;
    public string Freplenishmode { get; set; } = string.Empty;
    public string Frequireorgid { get; set; } = string.Empty;
    public string Fpurchaseorgid { get; set; } = string.Empty;
    public string Fstockorgid { get; set; } = string.Empty;
    public string Fappdeptid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; } = 1;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateMaterialReturnApplyEntryRequest> Entries { get; set; } = new();
}

public class UpdateMaterialReturnApplyRequest
{
    public string Fbilltypeid { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Frmtype { get; set; } = string.Empty;
    public string Frmmode { get; set; } = string.Empty;
    public string Freplenishmode { get; set; } = string.Empty;
    public string Frequireorgid { get; set; } = string.Empty;
    public string Fpurchaseorgid { get; set; } = string.Empty;
    public string Fstockorgid { get; set; } = string.Empty;
    public string Fappdeptid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fcurrencyid { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; } = 1;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateMaterialReturnApplyEntryRequest> Entries { get; set; } = new();
}
