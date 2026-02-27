using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产任务单工序
/// </summary>
[SugarTable("T_PRD_MOPROCESS")]
public class TPrdMoprocess : BaseEntity
{
    /// <summary>
    /// 工单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public int Fmoentryid { get; set; }

    /// <summary>
    /// 工序代码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 加工时间(分
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTIME")]
    public decimal Fworktime { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID", IsNullable = true)]
    public string FUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 累计合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY", IsNullable = true)]
    public decimal? FQUAQTY { get; set; }

    /// <summary>
    /// 必须开工
    /// </summary>
    [SugarColumn(ColumnName = "FISSTART", IsNullable = true)]
    public bool? FISSTART { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 开工时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME", IsNullable = true)]
    public DateTime? FSTARTTIME { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID", IsNullable = true)]
    public string FBASEUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 累计不合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FFAILQTY", IsNullable = true)]
    public decimal? FFAILQTY { get; set; }

    /// <summary>
    /// 累计加工数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY", IsNullable = true)]
    public decimal? FQTY { get; set; }

    /// <summary>
    /// 必须汇报
    /// </summary>
    [SugarColumn(ColumnName = "FISREPORT", IsNullable = true)]
    public bool? FISREPORT { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID", IsNullable = true)]
    public string FBODYID { get; set; } = string.Empty;
}
