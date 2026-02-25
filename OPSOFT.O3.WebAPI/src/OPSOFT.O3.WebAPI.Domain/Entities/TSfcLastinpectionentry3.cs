using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 质量终检表体-检验明细
/// </summary>
[SugarTable("T_SFC_LASTINPECTIONENTRY3")]
public class TSfcLastinpectionentry3 : BaseEntity
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
    /// 流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 检验方案内码
    /// </summary>
    [SugarColumn(ColumnName = "FTESTWAYID")]
    public string Ftestwayid { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODETYPE")]
    public int Fbarcodetype { get; set; }

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
