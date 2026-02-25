using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡表体-生产序列
/// </summary>
[SugarTable("T_SFC_FLOWCARDENTRY6")]
public class TSfcFlowcardentry6 : BaseEntity
{
    /// <summary>
    /// 序列编号
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTSEQ")]
    public string Fproductseq { get; set; } = string.Empty;

    /// <summary>
    /// 序列名称
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTSEQNAME")]
    public string Fproductseqname { get; set; } = string.Empty;

    /// <summary>
    /// 主序列
    /// </summary>
    [SugarColumn(ColumnName = "FISMAIN")]
    public bool Fismain { get; set; }

    /// <summary>
    /// SN管理
    /// </summary>
    [SugarColumn(ColumnName = "FISSNMANAGE")]
    public bool Fissnmanage { get; set; }

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
