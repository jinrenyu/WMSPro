using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售订单明细
/// </summary>
[SugarTable("T_SAL_ORDERENTRY")]
public class TSalOrderentry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWTYPE")]
    public string Frowtype { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// BOM代码
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 行业务关闭
    /// </summary>
    [SugarColumn(ColumnName = "FMRPFREEZESTATUS")]
    public string Fmrpfreezestatus { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 库存单位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKUNITID")]
    public string Fstockunitid { get; set; } = string.Empty;

    /// <summary>
    /// 库存数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKQTY")]
    public decimal Fstockqty { get; set; }

    /// <summary>
    /// 批次文体
    /// </summary>
    [SugarColumn(ColumnName = "FLOT_TEXT")]
    public string FlotText { get; set; } = string.Empty;

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
    /// 计价单位
    /// </summary>
    [SugarColumn(ColumnName = "FPRICEUNITID")]
    public string Fpriceunitid { get; set; } = string.Empty;

    /// <summary>
    /// 计价数量
    /// </summary>
    [SugarColumn(ColumnName = "FPRICEUNITQTY")]
    public decimal Fpriceunitqty { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 含税单价
    /// </summary>
    [SugarColumn(ColumnName = "FTAXPRICE")]
    public decimal Ftaxprice { get; set; }

    /// <summary>
    /// 是否赠品
    /// </summary>
    [SugarColumn(ColumnName = "FISFREE")]
    public bool Fisfree { get; set; }

    /// <summary>
    /// 税率%
    /// </summary>
    [SugarColumn(ColumnName = "FTAXRATE")]
    public decimal Ftaxrate { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT")]
    public decimal Ftaxamount { get; set; }

    /// <summary>
    /// 税额(本位币
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMOUNT_LC")]
    public decimal FtaxamountLc { get; set; }
}
