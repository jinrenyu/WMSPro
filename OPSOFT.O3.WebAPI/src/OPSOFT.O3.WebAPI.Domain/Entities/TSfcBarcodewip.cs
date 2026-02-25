using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 在制品条码表
/// </summary>
[SugarTable("T_SFC_BARCODEWIP")]
public class TSfcBarcodewip : BaseEntity
{
    /// <summary>
    /// 在制品条码状态
    /// </summary>
    [SugarColumn(ColumnName = "FBARSTATUS")]
    public int Fbarstatus { get; set; }

    /// <summary>
    /// 在制品序列号
    /// </summary>
    [SugarColumn(ColumnName = "FWIPCODE")]
    public string Fwipcode { get; set; } = string.Empty;

    /// <summary>
    /// 采集汇报单编号
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTNO")]
    public string Freportno { get; set; } = string.Empty;

    /// <summary>
    /// 产生日期(长日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }
}
