using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 蓝牙打印机配置
/// </summary>
[SugarTable("T_BAS_BTPRINTER")]
public class TBasBtprinter : BaseEntity
{
    /// <summary>
    /// 配置类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 打印份数
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTCOUNT")]
    public int Fprintcount { get; set; }

    /// <summary>
    /// 标签纸长度
    /// </summary>
    [SugarColumn(ColumnName = "FWIDTH")]
    public decimal Fwidth { get; set; }

    /// <summary>
    /// 标签纸高度
    /// </summary>
    [SugarColumn(ColumnName = "FHEIGHT")]
    public decimal Fheight { get; set; }

    /// <summary>
    /// 旋转角度
    /// </summary>
    [SugarColumn(ColumnName = "FORIENTATION")]
    public decimal Forientation { get; set; }

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
