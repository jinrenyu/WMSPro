using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序委外接收表体-条码信息
/// </summary>
[SugarTable("T_SFC_SUBRECEIVEENTRY1")]
public class TSfcSubreceiveentry1 : BaseEntity
{
    /// <summary>
    /// 条码号
    /// </summary>
    [SugarColumn(ColumnName = "FBARNUMBER")]
    public string Fbarnumber { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARTYPE")]
    public int Fbartype { get; set; }

    /// <summary>
    /// 是否下推
    /// </summary>
    [SugarColumn(ColumnName = "FISPUSH")]
    public bool Fispush { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 不良原因内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREASONSID")]
    public string Fbadreasonsid { get; set; } = string.Empty;

    /// <summary>
    /// 工序委外接受-明细信息FDETAILID
    /// </summary>
    [SugarColumn(ColumnName = "FSUBRECEIVEENTRYDETAILID")]
    public string Fsubreceiveentrydetailid { get; set; } = string.Empty;

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
