using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 排程过滤方案
/// </summary>
[SugarTable("T_SFC_QUERYCONDITIONS")]
public class TSfcQueryconditions : BaseEntity
{
    /// <summary>
    /// 查询方案编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 查询方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 是否存在工序流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FISEXISTFLOWCARD")]
    public int Fisexistflowcard { get; set; }

    /// <summary>
    /// 是否存在生产任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FISEXISTMO")]
    public int Fisexistmo { get; set; }

    /// <summary>
    /// 是否存在产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FISEXISTPRODUCT")]
    public int Fisexistproduct { get; set; }

    /// <summary>
    /// 是否存在资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FISEXISTRESOURCE")]
    public int Fisexistresource { get; set; }

    /// <summary>
    /// 是否存在是否存在时间
    /// </summary>
    [SugarColumn(ColumnName = "FISEXISTDATE")]
    public int Fisexistdate { get; set; }

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
