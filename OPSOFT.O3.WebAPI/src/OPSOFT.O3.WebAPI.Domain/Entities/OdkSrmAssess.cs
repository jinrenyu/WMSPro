using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 供应商评估表头
/// </summary>
[SugarTable("ODK_SRM_Assess")]
public class OdkSrmAssess : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 评鉴人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 评鉴类型
    /// </summary>
    [SugarColumn(ColumnName = "FAPPRAISALTYPE")]
    public string Fappraisaltype { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 标准得分
    /// </summary>
    [SugarColumn(ColumnName = "FSTDSCORE")]
    public decimal Fstdscore { get; set; }

    /// <summary>
    /// 得分
    /// </summary>
    [SugarColumn(ColumnName = "FSCORE")]
    public decimal Fscore { get; set; }

    /// <summary>
    /// 评鉴结果
    /// </summary>
    [SugarColumn(ColumnName = "FYNQUALIFIED")]
    public bool Fynqualified { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL")]
    public int Fchecklevel { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
