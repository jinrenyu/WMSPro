using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码解析方案规则
/// </summary>
[SugarTable("T_BD_RESOLBARCODERULE")]
public class TBdResolbarcoderule : BaseEntity
{
    /// <summary>
    /// 列名ID
    /// </summary>
    [SugarColumn(ColumnName = "FCOLUMNID")]
    public string Fcolumnid { get; set; } = string.Empty;

    /// <summary>
    /// 分割符
    /// </summary>
    [SugarColumn(ColumnName = "FDIVIDER")]
    public string Fdivider { get; set; } = string.Empty;

    /// <summary>
    /// ID组成部分
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPONENT")]
    public bool Fcomponent { get; set; }

    /// <summary>
    /// 截取索引起始位
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTSPLIT")]
    public int Fstartsplit { get; set; }

    /// <summary>
    /// 截取索引结束位
    /// </summary>
    [SugarColumn(ColumnName = "FENDSPLIT")]
    public int Fendsplit { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
