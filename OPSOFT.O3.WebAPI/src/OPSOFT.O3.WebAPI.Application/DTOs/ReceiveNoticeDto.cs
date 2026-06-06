namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ===== 收料通知单（真实表 T_PUR_RECEIVE / T_PUR_RECEIVEENTRY）=====

/// <summary>
/// 收料通知单列表项（按明细行展开，一条明细一行，带解析后的名称列，参照设计列表）
/// </summary>
public class ReceiveNoticeListDto
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
    // 物料
    public string Fmaterialid { get; set; } = string.Empty;
    public string FmaterialNumber { get; set; } = string.Empty;
    public string FmaterialName { get; set; } = string.Empty;
    public string FSpecification { get; set; } = string.Empty;
    // 数量
    public decimal Factreceiveqty { get; set; }
    public decimal Fgodqty { get; set; }
    public decimal Fscrapqty { get; set; }
    public decimal Finstockqty { get; set; }
    public string FunitName { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    public string FbaseunitName { get; set; } = string.Empty;
    // 辅助属性
    public string FauxpropName { get; set; } = string.Empty;
    // 物料带出
    public bool FisBatchManage { get; set; }
    public bool FisKfPeriod { get; set; }
    public int FKfPeriod { get; set; }
    public int FKfUnit { get; set; }
    // 批次/单价
    public string Flot { get; set; } = string.Empty;
    public decimal Fprice { get; set; }
    public DateTime? Fpredeliverydate { get; set; }
    public DateTime? Fkfdate { get; set; }
    // 供应商
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;
    // 采购员（业务员）
    public string FpurchaserName { get; set; } = string.Empty;
    // 收料部门
    public string FreceivedeptNumber { get; set; } = string.Empty;
    public string FreceivedeptName { get; set; } = string.Empty;
    // 仓库 / 仓位
    public string FstockNumber { get; set; } = string.Empty;
    public string FstockName { get; set; } = string.Empty;
    public bool FisOpenLocation { get; set; }
    public string FstocklocName { get; set; } = string.Empty;
    // 订单
    public string Forderbillno { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
    // 采购组织
    public string FpurorgName { get; set; } = string.Empty;
}

/// <summary>
/// 收料通知单明细行
/// </summary>
public class ReceiveNoticeEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int? Fentryid { get; set; }
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
    /// <summary>来料检验</summary>
    public bool Fcheckincoming { get; set; }
    public string Flot { get; set; } = string.Empty;
    public string Fsupplylot { get; set; } = string.Empty;
    // 数量
    public decimal Factreceiveqty { get; set; }
    public decimal Finstockqty { get; set; }
    public decimal Fgodqty { get; set; }
    public decimal Fscrapqty { get; set; }
    // 单位
    public string Funitid { get; set; } = string.Empty;
    public string FunitNumber { get; set; } = string.Empty;
    public string FunitName { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    // 仓库 / 仓位
    public string Fstockid { get; set; } = string.Empty;
    public string FstockNumber { get; set; } = string.Empty;
    public string FstockName { get; set; } = string.Empty;
    public bool FisOpenLocation { get; set; }
    public string Fstocklocid { get; set; } = string.Empty;
    public string FstocklocName { get; set; } = string.Empty;
    // 价格
    public decimal Fprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Ftaxprice { get; set; }
    public decimal Fdiscountrate { get; set; }
    public decimal Ftaxamount { get; set; }
    public decimal Famount { get; set; }
    public decimal Fallamount { get; set; }
    // 日期
    public DateTime? Fpredeliverydate { get; set; }
    public DateTime? Fkfdate { get; set; }
    public DateTime? Fexpiredate { get; set; }
    // 订单（源单追溯）
    public string Forderbillno { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
    public string Frepnote { get; set; } = string.Empty;
}

/// <summary>
/// 收料通知单详情（主表 + 名称解析 + 明细）
/// </summary>
public class ReceiveNoticeDetailDto
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
    // 源单
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    // 需求组织 / 采购组织
    public string Fdemandorgid { get; set; } = string.Empty;
    public string FdemandorgName { get; set; } = string.Empty;
    public string Fpurorgid { get; set; } = string.Empty;
    public string FpurorgName { get; set; } = string.Empty;
    // 收料部门 / 采购部门
    public string Freceivedeptid { get; set; } = string.Empty;
    public string FreceivedeptName { get; set; } = string.Empty;
    public string Fpurdeptid { get; set; } = string.Empty;
    public string FpurdeptName { get; set; } = string.Empty;
    // 收料员 / 采购员
    public string Freceiverid { get; set; } = string.Empty;
    public string FreceiverName { get; set; } = string.Empty;
    public string Fpurchaserid { get; set; } = string.Empty;
    public string FpurchaserName { get; set; } = string.Empty;
    // 供应商
    public string Fsupplyid { get; set; } = string.Empty;
    public string FsupplyNumber { get; set; } = string.Empty;
    public string FsupplyName { get; set; } = string.Empty;
    // 结算币别
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
    public List<ReceiveNoticeEntryDto> Entries { get; set; } = new();
}

public class CreateReceiveNoticeEntryRequest
{
    public string Fmaterialid { get; set; } = string.Empty;
    public string Fauxpropid { get; set; } = string.Empty;
    public bool Fcheckincoming { get; set; }
    public string Flot { get; set; } = string.Empty;
    public string Fsupplylot { get; set; } = string.Empty;
    public decimal Factreceiveqty { get; set; }
    public decimal Fgodqty { get; set; }
    public decimal Fscrapqty { get; set; }
    public string Funitid { get; set; } = string.Empty;
    public string Fbaseunitid { get; set; } = string.Empty;
    public decimal Fbaseunitqty { get; set; }
    public string Fstockid { get; set; } = string.Empty;
    public string Fstocklocid { get; set; } = string.Empty;
    public decimal Fprice { get; set; }
    public decimal Ftaxrate { get; set; }
    public decimal Ftaxprice { get; set; }
    public decimal Fdiscountrate { get; set; }
    public decimal Ftaxamount { get; set; }
    public decimal Famount { get; set; }
    public decimal Fallamount { get; set; }
    public DateTime? Fpredeliverydate { get; set; }
    public DateTime? Fkfdate { get; set; }
    public DateTime? Fexpiredate { get; set; }
    public string Forderbillno { get; set; } = string.Empty;
    public int Forderentryid { get; set; }
    public string Forderinterid { get; set; } = string.Empty;
    public string Forderdetailid { get; set; } = string.Empty;
    public string Frepnote { get; set; } = string.Empty;
}

public class CreateReceiveNoticeRequest
{
    public string Fbillno { get; set; } = string.Empty;
    public string Fbilltypeid { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public string Fdemandorgid { get; set; } = string.Empty;
    public string Fpurorgid { get; set; } = string.Empty;
    public string Freceivedeptid { get; set; } = string.Empty;
    public string Fpurdeptid { get; set; } = string.Empty;
    public string Freceiverid { get; set; } = string.Empty;
    public string Fpurchaserid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fsettlecurrid { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; } = 1;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateReceiveNoticeEntryRequest> Entries { get; set; } = new();
}

public class UpdateReceiveNoticeRequest
{
    public string Fbilltypeid { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fbusinesstype { get; set; } = string.Empty;
    public string Fsrcformid { get; set; } = string.Empty;
    public string Fsrcbillno { get; set; } = string.Empty;
    public string Fdemandorgid { get; set; } = string.Empty;
    public string Fpurorgid { get; set; } = string.Empty;
    public string Freceivedeptid { get; set; } = string.Empty;
    public string Fpurdeptid { get; set; } = string.Empty;
    public string Freceiverid { get; set; } = string.Empty;
    public string Fpurchaserid { get; set; } = string.Empty;
    public string Fsupplyid { get; set; } = string.Empty;
    public string Fsettlecurrid { get; set; } = string.Empty;
    public string Fexchangetypeid { get; set; } = string.Empty;
    public decimal Fexchangerate { get; set; } = 1;
    public string Fnote { get; set; } = string.Empty;
    public List<CreateReceiveNoticeEntryRequest> Entries { get; set; } = new();
}
