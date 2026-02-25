using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 对账单-表体
/// </summary>
[SugarTable("ODK_SRM_AccountEntry")]
public class OdkSrmAccountEntry : BaseEntity
{
    /// <summary>
    /// 入库单内码
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKINTERID")]
    public string Finstockinterid { get; set; } = string.Empty;

    /// <summary>
    /// 入库单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKDETAILID")]
    public string Finstockdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 入库单行号
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKENTRYID")]
    public string Finstockentryid { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单内码
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
    public string Fpoentryid { get; set; } = string.Empty;

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
    /// 送货单表体行号
    /// </summary>
    [SugarColumn(ColumnName = "FDLENTRYID")]
    public int Fdlentryid { get; set; }

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
    /// 收货数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 是否含税
    /// </summary>
    [SugarColumn(ColumnName = "FISTAX")]
    public bool Fistax { get; set; }

    /// <summary>
    /// 运费
    /// </summary>
    [SugarColumn(ColumnName = "FFREIGHT")]
    public decimal Ffreight { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAX")]
    public decimal Ftax { get; set; }

    /// <summary>
    /// 税前金额
    /// </summary>
    [SugarColumn(ColumnName = "FPRETAXAMT")]
    public decimal Fpretaxamt { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMT")]
    public decimal Ftaxamt { get; set; }

    /// <summary>
    /// 总额
    /// </summary>
    [SugarColumn(ColumnName = "FAMT")]
    public decimal Famt { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 状态(0:初始 1:行关闭
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILSTATUS")]
    public int Fdetailstatus { get; set; }
}
