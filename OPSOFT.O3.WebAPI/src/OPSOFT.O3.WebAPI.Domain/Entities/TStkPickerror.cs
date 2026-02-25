using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 拣货异常表头
/// </summary>
[SugarTable("T_STK_PICKERROR")]
public class TStkPickerror : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 拣货任务内码
    /// </summary>
    [SugarColumn(ColumnName = "FTASKID")]
    public string Ftaskid { get; set; } = string.Empty;

    /// <summary>
    /// 拣货任务明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FTASKDETAILID")]
    public string Ftaskdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 缺货数量
    /// </summary>
    [SugarColumn(ColumnName = "FOOSQTY")]
    public decimal Foosqty { get; set; }

    /// <summary>
    /// 异常原因
    /// </summary>
    [SugarColumn(ColumnName = "FREASON")]
    public string Freason { get; set; } = string.Empty;

    /// <summary>
    /// 拣货仓库行内码
    /// </summary>
    [SugarColumn(ColumnName = "FTASKSTKENTRYID")]
    public string Ftaskstkentryid { get; set; } = string.Empty;

    /// <summary>
    /// 是否已生成拣货任务
    /// </summary>
    [SugarColumn(ColumnName = "FISCRTASK")]
    public bool Fiscrtask { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
