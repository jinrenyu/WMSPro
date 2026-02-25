using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 仓位值集
/// </summary>
[SugarTable("T_BAS_FLEXVALUES")]
public class TBasFlexvalues : BaseEntity
{
    /// <summary>
    /// 仓位集代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集字段名
    /// </summary>
    [SugarColumn(ColumnName = "FFLEXNUMBER")]
    public string Fflexnumber { get; set; } = string.Empty;

    /// <summary>
    /// 值集类型
    /// </summary>
    [SugarColumn(ColumnName = "FFLEXTYPEID")]
    public string Fflextypeid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集使用组织
    /// </summary>
    [SugarColumn(ColumnName = "FUSEORGID")]
    public string Fuseorgid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 仓位集禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位集禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 长
    /// </summary>
    [SugarColumn(ColumnName = "FLENGTH")]
    public decimal Flength { get; set; }

    /// <summary>
    /// 宽
    /// </summary>
    [SugarColumn(ColumnName = "FWIDTH")]
    public decimal Fwidth { get; set; }

    /// <summary>
    /// 高
    /// </summary>
    [SugarColumn(ColumnName = "FHEIGHT")]
    public decimal Fheight { get; set; }

    /// <summary>
    /// 容积
    /// </summary>
    [SugarColumn(ColumnName = "FVOLUME")]
    public decimal Fvolume { get; set; }

    /// <summary>
    /// 容积限制
    /// </summary>
    [SugarColumn(ColumnName = "FVOLUMELIMIT")]
    public decimal Fvolumelimit { get; set; }

    /// <summary>
    /// 重量限制
    /// </summary>
    [SugarColumn(ColumnName = "FWEIGHTLIMIT")]
    public decimal Fweightlimit { get; set; }

    /// <summary>
    /// 混放种类限制
    /// </summary>
    [SugarColumn(ColumnName = "FMIXTYPELIMIT")]
    public string Fmixtypelimit { get; set; } = string.Empty;
}
