using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售出库单明细
/// </summary>
[SugarTable("T_SAL_OUTSTOCKENTRY")]
public class TSalOutstockentry : BaseEntity
{
    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// ERP表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPENTRYID")]
    public string Ferpentryid { get; set; } = string.Empty;

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
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 关联应收金额
    /// </summary>
    [SugarColumn(ColumnName = "FARJOINAMOUNT")]
    public decimal Farjoinamount { get; set; }

    /// <summary>
    /// 关联应收数量
    /// </summary>
    [SugarColumn(ColumnName = "FARJOINQTY")]
    public decimal Farjoinqty { get; set; }

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE")]
    public decimal Ftaxprice { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 价税合计
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT")]
    public decimal Fallamount { get; set; }

    /// <summary>
    /// 价税合计(本位币
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT_LC")]
    public decimal FallamountLc { get; set; }
}
