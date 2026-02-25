using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 出库申请单
/// </summary>
[SugarTable("T_STK_OUTSTOCKAPPLY")]
public class TStkOutstockapply : BaseEntity
{
    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 客户
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTID")]
    public string Fcustid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

    /// <summary>
    /// 关闭日期
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSEDATE")]
    public DateTime? Fclosedate { get; set; }

    /// <summary>
    /// 关闭人
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSERID")]
    public string Fcloserid { get; set; } = string.Empty;

    /// <summary>
    /// 申请类型
    /// </summary>
    [SugarColumn(ColumnName = "FAPPLYTYPE")]
    public string Fapplytype { get; set; } = string.Empty;

    /// <summary>
    /// 手工关闭标识
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSEFLAG")]
    public bool Fcloseflag { get; set; }

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 研发项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FYFXM")]
    public string Fyfxm { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
