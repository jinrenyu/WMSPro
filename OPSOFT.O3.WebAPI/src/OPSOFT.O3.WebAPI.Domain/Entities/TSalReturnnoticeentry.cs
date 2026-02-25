using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 退货通知单明细
/// </summary>
[SugarTable("T_SAL_RETURNNOTICEENTRY")]
public class TSalReturnnoticeentry : BaseEntity
{
    /// <summary>
    /// 客户物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FMAPID")]
    public string Fmapid { get; set; } = string.Empty;

    /// <summary>
    /// 客户物料名称
    /// </summary>
    [SugarColumn(ColumnName = "FMAPNAME")]
    public string Fmapname { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码内码
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
    [SugarColumn(ColumnName = "FCOMMONUNITID")]
    public string Fcommonunitid { get; set; } = string.Empty;

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
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 库存数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKQTY")]
    public decimal Fstockqty { get; set; }

    /// <summary>
    /// 基础库存数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKBASEQTY")]
    public decimal Fstockbaseqty { get; set; }

    /// <summary>
    /// 批次文体
    /// </summary>
    [SugarColumn(ColumnName = "FLOT_TEXT")]
    public string FlotText { get; set; } = string.Empty;

    /// <summary>
    /// 出货仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

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
