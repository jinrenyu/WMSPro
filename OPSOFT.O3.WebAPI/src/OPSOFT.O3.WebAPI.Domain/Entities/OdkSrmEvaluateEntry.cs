using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 专家评分表体1-招标明细
/// </summary>
[SugarTable("ODK_SRM_EvaluateEntry")]
public class OdkSrmEvaluateEntry : BaseEntity
{
    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 需求日期
    /// </summary>
    [SugarColumn(ColumnName = "FDEMANDDATE")]
    public DateTime? Fdemanddate { get; set; }

    /// <summary>
    /// 招标数量
    /// </summary>
    [SugarColumn(ColumnName = "FBIDQTY")]
    public decimal Fbidqty { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 备货/施工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPREPARECYCLE")]
    public int Fpreparecycle { get; set; }

    /// <summary>
    /// 交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDATE")]
    public DateTime? Fdeliverydate { get; set; }

    /// <summary>
    /// 是否含税
    /// </summary>
    [SugarColumn(ColumnName = "FISTAX")]
    public bool Fistax { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAX")]
    public decimal Ftax { get; set; }

    /// <summary>
    /// 税前金额
    /// </summary>
    [SugarColumn(ColumnName = "FPRETAXAMT")]
    public decimal Fpretaxamt { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMT")]
    public decimal Ftaxamt { get; set; }

    /// <summary>
    /// 总额
    /// </summary>
    [SugarColumn(ColumnName = "FAMT")]
    public decimal Famt { get; set; }

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
