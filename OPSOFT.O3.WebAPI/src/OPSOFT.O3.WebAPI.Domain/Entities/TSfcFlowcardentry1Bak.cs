using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡表体的表体-物料明细-备份
/// </summary>
[SugarTable("T_SFC_FLOWCARDENTRY1_BAK")]
public class TSfcFlowcardentry1Bak : BaseEntity
{
    /// <summary>
    /// 变更单单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHGINTERID")]
    public string Fchginterid { get; set; } = string.Empty;

    /// <summary>
    /// 变更单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHGDETAILID")]
    public string Fchgdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 物料代码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 物料单位用量
    /// </summary>
    [SugarColumn(ColumnName = "FUNITQTY")]
    public decimal Funitqty { get; set; }

    /// <summary>
    /// 物料辅助用量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 常用单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 已投料数量
    /// </summary>
    [SugarColumn(ColumnName = "FINPUTQTY")]
    public decimal Finputqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
