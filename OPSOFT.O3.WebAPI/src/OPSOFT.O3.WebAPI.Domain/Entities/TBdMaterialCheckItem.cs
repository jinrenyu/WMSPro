using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 物料检验项目
/// </summary>
[SugarTable("T_BD_MATERIALCHECKITEM")]
public class TBdMaterialcheckitem : BaseEntity
{
    /// <summary>
    /// 检验类型内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKTYPEID")]
    public string Fchecktypeid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FREMARKS")]
    public string Fremarks { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTATE")]
    public string Fstate { get; set; } = string.Empty;

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
    /// 物料ID
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 版本
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 来料检验
    /// </summary>
    [SugarColumn(ColumnName = "FINCOMMINGCHECK")]
    public bool Fincommingcheck { get; set; }

    /// <summary>
    /// 超期复检
    /// </summary>
    [SugarColumn(ColumnName = "FOVERDUERECHECK")]
    public bool Foverduerecheck { get; set; }

    /// <summary>
    /// 在库检验
    /// </summary>
    [SugarColumn(ColumnName = "FINLIBRARYCHECK")]
    public bool Finlibrarycheck { get; set; }

    /// <summary>
    /// 出货检验
    /// </summary>
    [SugarColumn(ColumnName = "FSHIPMENTCHECK")]
    public bool Fshipmentcheck { get; set; }

    /// <summary>
    /// 抽样方案内码
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLESCHEMEID")]
    public string Fsampleschemeid { get; set; } = string.Empty;

    /// <summary>
    /// 其他检验
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKOTHER")]
    public bool Fcheckother { get; set; }

    /// <summary>
    /// 退货检验
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKRETURN")]
    public bool Fcheckreturn { get; set; }

    /// <summary>
    /// 产品检验
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKPRODUCT")]
    public bool Fcheckproduct { get; set; }

    /// <summary>
    /// 受托材料检验
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKENTRUSTED")]
    public bool Fcheckentrusted { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
