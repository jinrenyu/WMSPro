using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 专家评分表体2-投标方明细
/// </summary>
[SugarTable("ODK_SRM_EvaluateEntry1")]
public class OdkSrmEvaluateEntry1 : BaseEntity
{
    /// <summary>
    /// 物料表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATDETAILID")]
    public string Fmatdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 投标单号
    /// </summary>
    [SugarColumn(ColumnName = "FBIDBILLNO")]
    public string Fbidbillno { get; set; } = string.Empty;

    /// <summary>
    /// 投标单内码
    /// </summary>
    [SugarColumn(ColumnName = "FBIDINTERID")]
    public string Fbidinterid { get; set; } = string.Empty;

    /// <summary>
    /// 投标方内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 投标方物料行内码
    /// </summary>
    [SugarColumn(ColumnName = "FBIDDETAILID")]
    public string Fbiddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 投标单价
    /// </summary>
    [SugarColumn(ColumnName = "FFTOTBIDPRICE")]
    public decimal Fftotbidprice { get; set; }

    /// <summary>
    /// 投标数量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTBIDQTY")]
    public decimal Ftotbidqty { get; set; }

    /// <summary>
    /// 总分
    /// </summary>
    [SugarColumn(ColumnName = "FTOTSCORE")]
    public decimal Ftotscore { get; set; }

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
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
