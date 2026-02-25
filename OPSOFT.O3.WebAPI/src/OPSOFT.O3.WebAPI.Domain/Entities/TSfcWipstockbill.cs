using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 在制品单据表
/// </summary>
[SugarTable("T_SFC_WIPSTOCKBILL")]
public class TSfcWipstockbill : BaseEntity
{
    /// <summary>
    /// 类别编号
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETRANTYPE")]
    public int Fsourcetrantype { get; set; }

    /// <summary>
    /// 源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBILLNO")]
    public string Fsourcebillno { get; set; } = string.Empty;

    /// <summary>
    /// 出入库单类型
    /// </summary>
    [SugarColumn(ColumnName = "FTRANTYPE")]
    public int Ftrantype { get; set; }

    /// <summary>
    /// 默认仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 默认仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 调出仓库
    /// </summary>
    [SugarColumn(ColumnName = "FDCSTOCKID")]
    public string Fdcstockid { get; set; } = string.Empty;

    /// <summary>
    /// 调出仓位
    /// </summary>
    [SugarColumn(ColumnName = "FDCSTOCKLOCID")]
    public string Fdcstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 经办员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 客户
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTID")]
    public string Fcustid { get; set; } = string.Empty;

    /// <summary>
    /// 供应厂商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 记账
    /// </summary>
    [SugarColumn(ColumnName = "FPOSTERID")]
    public string Fposterid { get; set; } = string.Empty;

    /// <summary>
    /// 验收
    /// </summary>
    [SugarColumn(ColumnName = "FFMANAGERID")]
    public string Ffmanagerid { get; set; } = string.Empty;

    /// <summary>
    /// 保管
    /// </summary>
    [SugarColumn(ColumnName = "FSMANAGERID")]
    public string Fsmanagerid { get; set; } = string.Empty;

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 销售方式
    /// </summary>
    [SugarColumn(ColumnName = "FSALESTYLE")]
    public string Fsalestyle { get; set; } = string.Empty;

    /// <summary>
    /// 对方科目
    /// </summary>
    [SugarColumn(ColumnName = "FACCTID")]
    public string Facctid { get; set; } = string.Empty;

    /// <summary>
    /// 红蓝字
    /// </summary>
    [SugarColumn(ColumnName = "FROB")]
    public int Frob { get; set; }

    /// <summary>
    /// 更新库存标识
    /// </summary>
    [SugarColumn(ColumnName = "FUPSTOCKWHENSAVE")]
    public bool Fupstockwhensave { get; set; }

    /// <summary>
    /// 采购方式
    /// </summary>
    [SugarColumn(ColumnName = "FPOSTYLE")]
    public string Fpostyle { get; set; } = string.Empty;

    /// <summary>
    /// 倒冲标记
    /// </summary>
    [SugarColumn(ColumnName = "FBACKFLUSHED")]
    public bool Fbackflushed { get; set; }

    /// <summary>
    /// 供货机构:/受托机构
    /// </summary>
    [SugarColumn(ColumnName = "FRELATEBRID")]
    public string Frelatebrid { get; set; } = string.Empty;

    /// <summary>
    /// 领料类型:/委外类型
    /// </summary>
    [SugarColumn(ColumnName = "FPURPOSEID")]
    public string Fpurposeid { get; set; } = string.Empty;

    /// <summary>
    /// 是否导入
    /// </summary>
    [SugarColumn(ColumnName = "FIMPORT")]
    public int Fimport { get; set; }

    /// <summary>
    /// 系统
    /// </summary>
    [SugarColumn(ColumnName = "FSYSTEMTYPE")]
    public int Fsystemtype { get; set; }

    /// <summary>
    /// 摘要
    /// </summary>
    [SugarColumn(ColumnName = "FEXPLANATION")]
    public string Fexplanation { get; set; } = string.Empty;

    /// <summary>
    /// 交货地点
    /// </summary>
    [SugarColumn(ColumnName = "FFETCHADD")]
    public string Ffetchadd { get; set; } = string.Empty;

    /// <summary>
    /// 主管
    /// </summary>
    [SugarColumn(ColumnName = "FMANAGERID")]
    public string Fmanagerid { get; set; } = string.Empty;

    /// <summary>
    /// 制单机构
    /// </summary>
    [SugarColumn(ColumnName = "FBRID")]
    public string Fbrid { get; set; } = string.Empty;

    /// <summary>
    /// 收货方
    /// </summary>
    [SugarColumn(ColumnName = "FCONSIGNEE")]
    public string Fconsignee { get; set; } = string.Empty;

    /// <summary>
    /// 打印次数
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTCOUNT")]
    public int Fprintcount { get; set; }

    /// <summary>
    /// 资料同步好的时间
    /// </summary>
    [SugarColumn(ColumnName = "FSYNTIME")]
    public DateTime? Fsyntime { get; set; }

    /// <summary>
    /// 同步后目标单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FSYNOBJINTERID")]
    public string Fsynobjinterid { get; set; } = string.Empty;

    /// <summary>
    /// 同步后目标单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FSYNOBJBILLNO")]
    public string Fsynobjbillno { get; set; } = string.Empty;

    /// <summary>
    /// 同步后目标单据内码（盘亏）
    /// </summary>
    [SugarColumn(ColumnName = "FSYNOBJINTERIDLOSS")]
    public string Fsynobjinteridloss { get; set; } = string.Empty;

    /// <summary>
    /// 同步后目标单据内码（盘亏）
    /// </summary>
    [SugarColumn(ColumnName = "FSYNOBJBILLNOLOSS")]
    public string Fsynobjbillnoloss { get; set; } = string.Empty;

    /// <summary>
    /// 正在同步的时间标识
    /// </summary>
    [SugarColumn(ColumnName = "FCURRSYNTIME")]
    public string Fcurrsyntime { get; set; } = string.Empty;

    /// <summary>
    /// 复核人代码
    /// </summary>
    [SugarColumn(ColumnName = "FRECHECKERID")]
    public string Frecheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 复核日期
    /// </summary>
    [SugarColumn(ColumnName = "FRECHECKDATE")]
    public DateTime? Frecheckdate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 任务单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FICMODETAILID")]
    public string Ficmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 不良品数
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
