using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 送货单表体
/// </summary>
[SugarTable("ODK_SRM_DeliveryEntry")]
public class OdkSrmDeliveryEntry : BaseEntity
{
    /// <summary>
    /// 采购内码
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTERID")]
    public string Fpointerid { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FPODETAILID")]
    public string Fpodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FPOENTRYID")]
    public int Fpoentryid { get; set; }

    /// <summary>
    /// 排程单内码
    /// </summary>
    [SugarColumn(ColumnName = "FPCDETAILID")]
    public string Fpcdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 排程单行号
    /// </summary>
    [SugarColumn(ColumnName = "FPCENTRYID")]
    public int Fpcentryid { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 订单量
    /// </summary>
    [SugarColumn(ColumnName = "FPOQTY")]
    public decimal Fpoqty { get; set; }

    /// <summary>
    /// 可送货量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 送货数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 箱数
    /// </summary>
    [SugarColumn(ColumnName = "FBOXNUM")]
    public int Fboxnum { get; set; }

    /// <summary>
    /// 数量/箱
    /// </summary>
    [SugarColumn(ColumnName = "FBOXQTY")]
    public decimal Fboxqty { get; set; }

    /// <summary>
    /// 收货数量
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIPTQTY")]
    public decimal Freceiptqty { get; set; }

    /// <summary>
    /// 退货量
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNQTY")]
    public decimal Freturnqty { get; set; }

    /// <summary>
    /// 不合格量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

    /// <summary>
    /// 状态(0:初始 1:行取消 2:行关闭
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILSTATUS")]
    public int Fdetailstatus { get; set; }
}
