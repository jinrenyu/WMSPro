using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// GB2828样本量字码表加严表头
/// </summary>
[SugarTable("T_BD_GB2828SAMPLESIZECODEJY_HEAD")]
public class TBdGb2828samplesizecodejyHead : BaseEntity
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
