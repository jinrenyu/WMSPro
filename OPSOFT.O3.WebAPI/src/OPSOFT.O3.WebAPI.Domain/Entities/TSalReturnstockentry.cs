using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售退货单明细
/// </summary>
[SugarTable("T_SAL_RETURNSTOCKENTRY")]
public class TSalReturnstockentry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 客户物料
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTMATID")]
    public string Fcustmatid { get; set; } = string.Empty;

    /// <summary>
    /// 零售条形码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 应退数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 实退数量
    /// </summary>
    [SugarColumn(ColumnName = "FREALQTY")]
    public decimal Frealqty { get; set; }

    /// <summary>
    /// 退货类型
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNTYPE")]
    public string Freturntype { get; set; } = string.Empty;

    /// <summary>
    /// 税率%
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public decimal Ftaxrate { get; set; }

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCEDATE")]
    public DateTime? Fproducedate { get; set; }

    /// <summary>
    /// 拒收数量
    /// </summary>
    [SugarColumn(ColumnName = "FREFUSEQTY")]
    public decimal Frefuseqty { get; set; }

    /// <summary>
    /// 补货数量
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRQTY")]
    public decimal Frepairqty { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

    /// <summary>
    /// 金额(本位币
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT_LC")]
    public decimal FamountLc { get; set; }
}
