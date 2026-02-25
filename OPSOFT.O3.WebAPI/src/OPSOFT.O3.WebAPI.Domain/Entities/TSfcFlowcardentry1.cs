using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡表体的表体-物料明细
/// </summary>
[SugarTable("T_SFC_FLOWCARDENTRY1")]
public class TSfcFlowcardentry1 : BaseEntity
{
    /// <summary>
    /// 物料代码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 物料单位用量
    /// </summary>
    [SugarColumn(ColumnName = "FUNITQTY")]
    public decimal Funitqty { get; set; }

    /// <summary>
    /// 物料辅助用量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 常用单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 已投料数量
    /// </summary>
    [SugarColumn(ColumnName = "FINPUTQTY")]
    public decimal Finputqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 投料类型
    /// </summary>
    [SugarColumn(ColumnName = "FRELEASETYPE")]
    public int Freleasetype { get; set; }

    /// <summary>
    /// 来源序列
    /// </summary>
    [SugarColumn(ColumnName = "FORSEQ")]
    public string Forseq { get; set; } = string.Empty;

    /// <summary>
    /// 来源工序
    /// </summary>
    [SugarColumn(ColumnName = "FORPROCESSID")]
    public string Forprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 来源产品工艺工序表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDETAILID")]
    public string Fordetailid { get; set; } = string.Empty;

    /// <summary>
    /// 投入序列
    /// </summary>
    [SugarColumn(ColumnName = "FTRSEQ")]
    public string Ftrseq { get; set; } = string.Empty;

    /// <summary>
    /// 投入工序
    /// </summary>
    [SugarColumn(ColumnName = "FTRPROCESSID")]
    public string Ftrprocessid { get; set; } = string.Empty;

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
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
