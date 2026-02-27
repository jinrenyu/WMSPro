using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 收货单表体
/// </summary>
[SugarTable("ODK_SRM_ReceiptEntry")]
public class OdkSrmReceiptEntry : BaseEntity
{
    /// <summary>
    /// 送货单内码
    /// </summary>
    [SugarColumn(ColumnName = "FDLINTERID")]
    public string Fdlinterid { get; set; } = string.Empty;

    /// <summary>
    /// 送货单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FDLDETAILID")]
    public string Fdldetailid { get; set; } = string.Empty;

    /// <summary>
    /// 送货单行号
    /// </summary>
    [SugarColumn(ColumnName = "FDLENTRYID")]
    public int Fdlentryid { get; set; }

    /// <summary>
    /// 采购单内码
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
    public string Fpcentryid { get; set; } = string.Empty;

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
    /// 可收货数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 实收货量
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
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 收货仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 收货仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSPID")]
    public string Fspid { get; set; } = string.Empty;

    /// <summary>
    /// 检验类型(1:免检 2:抽检 3:全检
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKTYPE")]
    public string Fchecktype { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 退货数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY", IsNullable = true)]
    public decimal? FBADQTY { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX", IsNullable = true)]
    public string FAPPENDIX { get; set; } = string.Empty;

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID", IsNullable = true)]
    public string FBODYID { get; set; } = string.Empty;
}
