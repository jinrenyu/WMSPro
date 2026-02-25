using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 程式其它功能信息
/// </summary>
[SugarTable("SYS_PRGFUNCTION")]
public class SysPrgFunction : BaseEntity
{
    /// <summary>
    /// 程式代号
    /// </summary>
    [SugarColumn(ColumnName = "PRG_ID")]
    public string PrgId { get; set; } = string.Empty;

    /// <summary>
    /// 功能代号
    /// </summary>
    [SugarColumn(ColumnName = "PB_FUNC")]
    public string PbFunc { get; set; } = string.Empty;

    /// <summary>
    /// 功能说明_CN
    /// </summary>
    [SugarColumn(ColumnName = "PB_FCNAME")]
    public string PbFcname { get; set; } = string.Empty;

    /// <summary>
    /// 功能说明_TW
    /// </summary>
    [SugarColumn(ColumnName = "PB_FTNAME")]
    public string PbFtname { get; set; } = string.Empty;

    /// <summary>
    /// 功能说明_EN
    /// </summary>
    [SugarColumn(ColumnName = "PB_FENAME")]
    public string PbFename { get; set; } = string.Empty;

    /// <summary>
    /// 功能说明
    /// </summary>
    [SugarColumn(ColumnName = "PB_FNAME")]
    public string PbFname { get; set; } = string.Empty;

    /// <summary>
    /// 项次
    /// </summary>
    [SugarColumn(ColumnName = "PB_SEQ")]
    public string PbSeq { get; set; } = string.Empty;

    /// <summary>
    /// 权限数值
    /// </summary>
    [SugarColumn(ColumnName = "PB_BITMAP")]
    public int PbBitmap { get; set; }

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
