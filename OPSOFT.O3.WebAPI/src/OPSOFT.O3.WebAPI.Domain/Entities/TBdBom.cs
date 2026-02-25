using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// BOM物料清单
/// </summary>
[SugarTable("T_BD_BOM")]
public class TBdBom : BaseEntity
{
    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string FBillNo { get; set; } = string.Empty;

    /// <summary>
    /// BOM单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? FDate { get; set; }

    /// <summary>
    /// 跳层
    /// </summary>
    [SugarColumn(ColumnName = "FBOMSKIP")]
    public int FBomSkip { get; set; }

    /// <summary>
    /// 用量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal FAuxQty { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string FBaseUnitId { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal FQty { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string FMaterialId { get; set; } = string.Empty;

    /// <summary>
    /// 物料属性
    /// </summary>
    [SugarColumn(ColumnName = "FERPCLSID")]
    public string FErpClsId { get; set; } = string.Empty;

    /// <summary>
    /// 规格型号
    /// </summary>
    [SugarColumn(ColumnName = "FMODEL")]
    public string FModel { get; set; } = string.Empty;

    /// <summary>
    /// 成品率
    /// </summary>
    [SugarColumn(ColumnName = "FYIELD")]
    public decimal FYield { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    [SugarColumn(ColumnName = "FVERSION")]
    public string FVersion { get; set; } = string.Empty;

    /// <summary>
    /// 客户内码
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTID")]
    public string FCustId { get; set; } = string.Empty;

    /// <summary>
    /// 客户料号内码
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTITEMID")]
    public string FCustItemId { get; set; } = string.Empty;

    /// <summary>
    /// 图号
    /// </summary>
    [SugarColumn(ColumnName = "FCHARTNUMBER")]
    public string FChartNumber { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? FCheckDate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string FDisableId { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? FDisableDate { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string FAuxPropId { get; set; } = string.Empty;
}
