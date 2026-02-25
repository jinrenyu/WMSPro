using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 在制品盘点
/// </summary>
[SugarTable("T_SFC_WIPINVENTORY")]
public class TSfcWipinventory : BaseEntity
{
    /// <summary>
    /// 方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 资料同步好的时间
    /// </summary>
    [SugarColumn(ColumnName = "FSYNTIME")]
    public DateTime? Fsyntime { get; set; }

    /// <summary>
    /// 复核人代码
    /// </summary>
    [SugarColumn(ColumnName = "FRECHECKERID")]
    public string Frecheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 复核日期
    /// </summary>
    [SugarColumn(ColumnName = "FRECHECKDATE")]
    public DateTime? Frecheckdate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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

    /// <summary>
    /// 是否盘点
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECK")]
    public bool Fischeck { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
