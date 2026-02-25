using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 过程检验表体-检验明细
/// </summary>
[SugarTable("T_SFC_PROCESSINPECTIONENTRY3")]
public class TSfcProcessinpectionentry3 : BaseEntity
{
    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

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
    /// 送检数量
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTQTY")]
    public decimal Finspectqty { get; set; }

    /// <summary>
    /// 测试结果
    /// </summary>
    [SugarColumn(ColumnName = "FQCRESULT")]
    public string Fqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 汇报内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

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
