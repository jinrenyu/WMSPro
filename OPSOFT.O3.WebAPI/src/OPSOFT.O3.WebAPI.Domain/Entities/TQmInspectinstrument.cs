using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验仪器
/// </summary>
[SugarTable("T_QM_INSPECTINSTRUMENT")]
public class TQmInspectinstrument : BaseEntity
{
    /// <summary>
    /// 仪器代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 仪器名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 购入日期
    /// </summary>
    [SugarColumn(ColumnName = "FBUYINTODATE")]
    public DateTime? Fbuyintodate { get; set; }

    /// <summary>
    /// 投入使用日期
    /// </summary>
    [SugarColumn(ColumnName = "FUSEDATE")]
    public DateTime? Fusedate { get; set; }

    /// <summary>
    /// 下次校准日期
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTADJUSTDATE")]
    public DateTime? Fnextadjustdate { get; set; }

    /// <summary>
    /// 购入金额
    /// </summary>
    [SugarColumn(ColumnName = "FBUYINTOAMOUNT")]
    public decimal Fbuyintoamount { get; set; }

    /// <summary>
    /// 使用状态
    /// </summary>
    [SugarColumn(ColumnName = "FUSESTATUS")]
    public int Fusestatus { get; set; }

    /// <summary>
    /// 仪器照片
    /// </summary>
    [SugarColumn(ColumnName = "FPICTURE")]
    public byte[]? Fpicture { get; set; }

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
    /// 测量单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;
}
