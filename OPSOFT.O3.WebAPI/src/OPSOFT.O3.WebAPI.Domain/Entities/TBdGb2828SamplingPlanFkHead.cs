using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// GB2828抽样水平放宽表头
/// </summary>
[SugarTable("T_BD_GB2828SAMPLINGPLANFK_HEAD")]
public class TBdGb2828samplingplanfkHead : BaseEntity
{
    /// <summary>
    /// 编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
