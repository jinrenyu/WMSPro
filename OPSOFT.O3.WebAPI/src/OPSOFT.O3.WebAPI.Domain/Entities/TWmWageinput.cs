using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工资数据
/// </summary>
[SugarTable("T_WM_WAGEINPUT")]
public class TWmWageinput : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 工资年
    /// </summary>
    [SugarColumn(ColumnName = "FYEAR")]
    public int Fyear { get; set; }

    /// <summary>
    /// 工资月
    /// </summary>
    [SugarColumn(ColumnName = "FMONTH")]
    public int Fmonth { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
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

    /// <summary>
    /// 关闭状态 0=正常 1=禁用
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSED")]
    public bool Fclosed { get; set; }

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL")]
    public int Fchecklevel { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
