using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 产品工艺流程表体的表体-联副产品
/// </summary>
[SugarTable("T_ENG_PROROUTEENTRY7")]
public class TEngProrouteentry7 : BaseEntity
{
    /// <summary>
    /// 联副产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FCOBYTYPE")]
    public string Fcobytype { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 产出序列
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTSEQ")]
    public string Fproductseq { get; set; } = string.Empty;

    /// <summary>
    /// 产出工序
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 单位产量
    /// </summary>
    [SugarColumn(ColumnName = "FUNITQTY")]
    public decimal Funitqty { get; set; }

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
