using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 润滑点设定
/// </summary>
[SugarTable("T_ENG_EQLUBRICATIONSET")]
public class TEngEqlubricationset : BaseEntity
{
    /// <summary>
    /// 润滑点编号
    /// </summary>
    [SugarColumn(ColumnName = "FLUNUMBER")]
    public string Flunumber { get; set; } = string.Empty;

    /// <summary>
    /// 润滑点名称
    /// </summary>
    [SugarColumn(ColumnName = "FLUNAME")]
    public string Fluname { get; set; } = string.Empty;

    /// <summary>
    /// 润滑类别内码
    /// </summary>
    [SugarColumn(ColumnName = "FLUTYPEID")]
    public string Flutypeid { get; set; } = string.Empty;

    /// <summary>
    /// 润滑材料内码
    /// </summary>
    [SugarColumn(ColumnName = "FLUMATERIALID")]
    public string Flumaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 牌号
    /// </summary>
    [SugarColumn(ColumnName = "FMAREMARK")]
    public string Fmaremark { get; set; } = string.Empty;

    /// <summary>
    /// 定额
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 计量单位名称
    /// </summary>
    [SugarColumn(ColumnName = "FUNITNAME")]
    public string Funitname { get; set; } = string.Empty;

    /// <summary>
    /// 周期
    /// </summary>
    [SugarColumn(ColumnName = "FPERIOD")]
    public decimal Fperiod { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 周期单位
    /// </summary>
    [SugarColumn(ColumnName = "FPERIODTYPE")]
    public string Fperiodtype { get; set; } = string.Empty;

    /// <summary>
    /// 运行周期
    /// </summary>
    [SugarColumn(ColumnName = "FRUNPERIOD")]
    public decimal Frunperiod { get; set; }

    /// <summary>
    /// 总运行时间
    /// </summary>
    [SugarColumn(ColumnName = "FTOTRUNTIME")]
    public decimal Ftotruntime { get; set; }

    /// <summary>
    /// 润滑后运行时间
    /// </summary>
    [SugarColumn(ColumnName = "FLASTRUNTIME")]
    public decimal Flastruntime { get; set; }

    /// <summary>
    /// 给油脂点数
    /// </summary>
    [SugarColumn(ColumnName = "FOILCOUNT")]
    public decimal Foilcount { get; set; }

    /// <summary>
    /// 责任人
    /// </summary>
    [SugarColumn(ColumnName = "FPRINCIPALS")]
    public string Fprincipals { get; set; } = string.Empty;

    /// <summary>
    /// 是否排计划
    /// </summary>
    [SugarColumn(ColumnName = "FISPLAN")]
    public bool Fisplan { get; set; }

    /// <summary>
    /// 启用日期
    /// </summary>
    [SugarColumn(ColumnName = "FSDATE")]
    public DateTime? Fsdate { get; set; }

    /// <summary>
    /// 上次润滑日期
    /// </summary>
    [SugarColumn(ColumnName = "FPREVMAINTDATE")]
    public DateTime? Fprevmaintdate { get; set; }

    /// <summary>
    /// 末次润滑日期
    /// </summary>
    [SugarColumn(ColumnName = "FLDATE")]
    public DateTime? Fldate { get; set; }

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
