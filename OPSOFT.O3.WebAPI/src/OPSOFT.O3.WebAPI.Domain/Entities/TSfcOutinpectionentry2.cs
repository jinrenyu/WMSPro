using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外检验表体-检验明细
/// </summary>
[SugarTable("T_SFC_OUTINPECTIONENTRY2")]
public class TSfcOutinpectionentry2 : BaseEntity
{
    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 使用决策备注
    /// </summary>
    [SugarColumn(ColumnName = "FITEMCHARDESC")]
    public string Fitemchardesc { get; set; } = string.Empty;

    /// <summary>
    /// 序列号
    /// </summary>
    [SugarColumn(ColumnName = "FSERIALNO")]
    public string Fserialno { get; set; } = string.Empty;

    /// <summary>
    /// 合格数
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public decimal Fquaqty { get; set; }

    /// <summary>
    /// 不合格数
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUAQTY")]
    public decimal Funquaqty { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 送检数量
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTQTY")]
    public decimal Finspectqty { get; set; }

    /// <summary>
    /// 汇报内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 汇报模式
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTMODE")]
    public string Freportmode { get; set; } = string.Empty;

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
