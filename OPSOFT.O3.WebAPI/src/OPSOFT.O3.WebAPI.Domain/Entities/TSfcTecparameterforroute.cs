using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 产品生产线工艺参数
/// </summary>
[SugarTable("T_SFC_TECPARAMETERFORROUTE")]
public class TSfcTecparameterforroute : BaseEntity
{
    /// <summary>
    /// 参数代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 参数名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 工艺类型
    /// </summary>
    [SugarColumn(ColumnName = "FROUTETYPEID")]
    public string Froutetypeid { get; set; } = string.Empty;

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 标准值
    /// </summary>
    [SugarColumn(ColumnName = "FSTADVALUE")]
    public decimal Fstadvalue { get; set; }

    /// <summary>
    /// 极低值
    /// </summary>
    [SugarColumn(ColumnName = "FMINVALUE")]
    public decimal Fminvalue { get; set; }

    /// <summary>
    /// 极高值
    /// </summary>
    [SugarColumn(ColumnName = "FMAXVALUE")]
    public decimal Fmaxvalue { get; set; }

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
