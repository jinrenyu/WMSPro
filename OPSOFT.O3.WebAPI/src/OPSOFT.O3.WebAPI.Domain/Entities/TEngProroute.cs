using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 产品工艺流程
/// </summary>
[SugarTable("T_ENG_PROROUTE")]
public class TEngProroute : BaseEntity
{
    /// <summary>
    /// 产品工艺代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 产品工艺名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 产品代码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 标准工艺
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEID")]
    public string Frouteid { get; set; } = string.Empty;

    /// <summary>
    /// 类别编号
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 是否默认工艺
    /// </summary>
    [SugarColumn(ColumnName = "FISDEFAULT")]
    public bool Fisdefault { get; set; }

    /// <summary>
    /// 首工序预先打印流转条码
    /// </summary>
    [SugarColumn(ColumnName = "FPREPRINTFLOW")]
    public bool Fpreprintflow { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }
}
