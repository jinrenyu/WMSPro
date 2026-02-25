using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修汇报-送修单表体的表体-汇报明细
/// </summary>
[SugarTable("T_SFC_REPAIRENTRY1")]
public class TSfcRepairentry1 : BaseEntity
{
    /// <summary>
    /// 同一时间段内码
    /// </summary>
    [SugarColumn(ColumnName = "FTIMEID")]
    public string Ftimeid { get; set; } = string.Empty;

    /// <summary>
    /// 表体单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 汇报类型
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTTYPE")]
    public int Freporttype { get; set; }

    /// <summary>
    /// 回流工序
    /// </summary>
    [SugarColumn(ColumnName = "FRETPROCESSID")]
    public string Fretprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 下一维修站
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTPROCESSID")]
    public string Fnextprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCESID")]
    public string Fresourcesid { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTID")]
    public string Fequipmentid { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 班组内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMID")]
    public string Fteamid { get; set; } = string.Empty;

    /// <summary>
    /// 班次内码
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTID")]
    public string Fshiftid { get; set; } = string.Empty;

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 第一次开工时间
    /// </summary>
    [SugarColumn(ColumnName = "FFSDTIME")]
    public DateTime? Ffsdtime { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSDTIME")]
    public DateTime? Fsdtime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FEDTIME")]
    public DateTime? Fedtime { get; set; }

    /// <summary>
    /// 累计用时(秒
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALSEC")]
    public decimal Ftotalsec { get; set; }
}
