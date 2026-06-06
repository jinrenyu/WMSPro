namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 采购订单（真实表 T_PUR_POORDER / T_PUR_POORDERENTRY）=====

/// <summary>
/// 采购订单列表项（按明细行展开，一条明细一行，带解析后的名称列，参照设计列表）
/// </summary>
public class PurchaseOrderListDto
{
    /// <summary>主表 Uid（用于进入编辑）</summary>
    public string Uid { get; set; } = string.Empty;
    /// <summary>明细 Uid</summary>
    public string EntryUid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public int? Fentryid { get; set; }
    public int FStatus { get; set; }
    public string FstatusName { get; set; } = string.Empty;
    // 采购员
    public string Fpurchaserid { get; set; } = string.Empty;
    public string FpurchaserName { get; set; } = string.Empty;
    // 物料
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    // 辅助属性
    public string FauxpropName { get; set; } = string.Empty;
    // 数量/单位
    public decimal Fqty { get; set; }
    public string FunitName { get; set; } = string.Empty;
    // 供应商
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;
    // 供应商批号
    public string Fsupplierlot { get; set; } = string.Empty;
}

/// <summary>
/// 采购订单明细行
/// </summary>
public class PurchaseOrderEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int? Fentryid { get; set; }
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    /// <summary>启用批次管理（物料带出，只读）</summary>
    public bool FisBatchManage { get; set; }
    public string Flot { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public string FauxpropName { get; set; } = string.Empty;
    /// <summary>该行物料是否启用辅助属性（前端据此决定辅助属性可选/只读）</summary>
    public bool FisAuxEnabled { get; set; }
    /// <summary>启用保质期（物料带出，只读）</summary>
    public bool FisKfPeriod { get; set; }
    /// <summary>保质期限（物料带出，只读）</summary>
    public int FKfPeriod { get; set; }
    /// <summary>保质期单位 0=日/1=月/2=年（物料带出，只读）</summary>
    public int FKfUnit { get; set; }
    public decimal Fqty { get; set; }
    public decimal Finstockqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string FunitNumber { get; set; } = string.Empty;
    public string FunitName { get; set; } = string.Empty;
    public decimal Ftaxprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Fprice { get; set; }
    public decimal Fdiscountrate { get; set; }
    public decimal Ftaxamount { get; set; }
    public decimal Famount { get; set; }
    public decimal Fallamount { get; set; }
    public DateTime? Fdeliverydate { get; set; }
    public string Fsupplierlot { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
}

/// <summary>
/// 采购订单详情（主表 + 名称解析 + 明细）
/// </summary>
public class PurchaseOrderDetailDto
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
    public string Fbusinesstype { get; set; } = string.Empty;
    // 采购组织
    public string Fcompanyid { get; set; } = string.Empty;
    public string FcompanyName { get; set; } = string.Empty;
    // 采购部门
    public string Fpurchasedeptid { get; set; } = string.Empty;
    public string FpurchasedeptName { get; set; } = string.Empty;
    // 采购员
    public string Fpurchaserid { get; set; } = string.Empty;
    public string FpurchaserName { get; set; } = string.Empty;
    // 供应商
    public string Fsupplyid { get; set; } = string.Empty;
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;
    // 交易币别
    public string Fsettlecurrid { get; set; } = string.Empty;
    public string FcurrencyNumber { get; set; } = string.Empty;
    public string FcurrencyName { get; set; } = string.Empty;
    // 汇率
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
    public List<PurchaseOrderEntryDto> Entries { get; set; } = new();
}

public class CreatePurchaseOrderEntryRequest
{
    public string Fmaterialid { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public string Flot { get; set; } = string.Empty;
    public decimal Fqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Ftaxprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Fprice { get; set; }
    public decimal Fdiscountrate { get; set; }
    public decimal Ftaxamount { get; set; }
    public decimal Famount { get; set; }
    public decimal Fallamount { get; set; }
    public DateTime? Fdeliverydate { get; set; }
    public string Fsupplierlot { get; set; } = string.Empty;
    public string Fnote { get; set; } = string.Empty;
}

public class CreatePurchaseOrderRequest
{
    public string Fbillno { get; set; } = string.Empty;
    public string Fbilltypeid { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    /// <summary>采购组织</summary>
    public string Fcompanyid { get; set; } = string.Empty;
    public string Fpurchasedeptid { get; set; } = string.Empty;
    public string Fpurchaserid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fsettlecurrid { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; } = 1;
    public string Fnote { get; set; } = string.Empty;
    public List<CreatePurchaseOrderEntryRequest> Entries { get; set; } = new();
}

public class UpdatePurchaseOrderRequest
{
    public string Fbilltypeid { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    /// <summary>采购组织</summary>
    public string Fcompanyid { get; set; } = string.Empty;
    public string Fpurchasedeptid { get; set; } = string.Empty;
    public string Fpurchaserid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fsettlecurrid { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; } = 1;
    public string Fnote { get; set; } = string.Empty;
    public List<CreatePurchaseOrderEntryRequest> Entries { get; set; } = new();
}
