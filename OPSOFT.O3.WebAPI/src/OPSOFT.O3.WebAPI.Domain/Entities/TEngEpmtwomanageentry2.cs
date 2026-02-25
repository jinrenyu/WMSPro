using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 故障维修工单  表体  维修材料/备件
/// </summary>
[SugarTable("T_ENG_EPMTWOMANAGEENTRY2")]
public class TEngEpmtwomanageentry2 : BaseEntity
{
    /// <summary>
    /// 管理部位工单内码
    /// </summary>
    [SugarColumn(ColumnName = "FRELATIONID")]
    public string Frelationid { get; set; } = string.Empty;

    /// <summary>
    /// 备件内码
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTID")]
    public string Fsparepartid { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT")]
    public decimal Fcount { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 更换日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHANGEDATE")]
    public DateTime? Fchangedate { get; set; }

    /// <summary>
    /// 预计寿命
    /// </summary>
    [SugarColumn(ColumnName = "FMOLD")]
    public int Fmold { get; set; }

    /// <summary>
    /// 备件费用
    /// </summary>
    [SugarColumn(ColumnName = "FSPAREPARTSCOST")]
    public decimal Fsparepartscost { get; set; }

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
