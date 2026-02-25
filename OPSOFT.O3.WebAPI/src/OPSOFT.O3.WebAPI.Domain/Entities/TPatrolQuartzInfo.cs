using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 巡检调度数据队列
/// </summary>
[SugarTable("T_PatrolQuartz_Info")]
public class TPatrolQuartzInfo : BaseEntity
{
    /// <summary>
    /// 总累计时间
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALTIME")]
    public decimal Ftotaltime { get; set; }

    /// <summary>
    /// 流转卡表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 汇报单组织内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTCOMPANYID")]
    public string Freportcompanyid { get; set; } = string.Empty;

    /// <summary>
    /// 当前触发累计时长
    /// </summary>
    [SugarColumn(ColumnName = "FTIME")]
    public decimal Ftime { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// IP
    /// </summary>
    [SugarColumn(ColumnName = "FIP")]
    public string Fip { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 设定时长
    /// </summary>
    [SugarColumn(ColumnName = "FSETTIME")]
    public decimal Fsettime { get; set; }

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;
}
