using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 保修记录
/// </summary>
[SugarTable("T_ENG_EQWARRANTY")]
public class TEngEqwarranty : BaseEntity
{
    /// <summary>
    /// 保修序号
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRID")]
    public string Frepairid { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTID")]
    public string Fequipmentid { get; set; } = string.Empty;

    /// <summary>
    /// 保修商内码
    /// </summary>
    [SugarColumn(ColumnName = "FWARRANTYID")]
    public string Fwarrantyid { get; set; } = string.Empty;

    /// <summary>
    /// 保修到期日
    /// </summary>
    [SugarColumn(ColumnName = "FDUEDATE")]
    public DateTime? Fduedate { get; set; }

    /// <summary>
    /// 工单号
    /// </summary>
    [SugarColumn(ColumnName = "FWORKORDERNUM")]
    public string Fworkordernum { get; set; } = string.Empty;

    /// <summary>
    /// 故障日期
    /// </summary>
    [SugarColumn(ColumnName = "FFAILUREDATE")]
    public DateTime? Ffailuredate { get; set; }

    /// <summary>
    /// 送修日期
    /// </summary>
    [SugarColumn(ColumnName = "FSENDDATE")]
    public DateTime? Fsenddate { get; set; }

    /// <summary>
    /// 费用
    /// </summary>
    [SugarColumn(ColumnName = "FCOST")]
    public decimal Fcost { get; set; }

    /// <summary>
    /// 修理人
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRER")]
    public string Frepairer { get; set; } = string.Empty;

    /// <summary>
    /// 完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FFINISHDATE")]
    public DateTime? Ffinishdate { get; set; }

    /// <summary>
    /// 故障情况
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTCONDITION")]
    public string Ffaultcondition { get; set; } = string.Empty;

    /// <summary>
    /// 修理情况
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRSITUATION")]
    public string Frepairsituation { get; set; } = string.Empty;

    /// <summary>
    /// 跟换部件
    /// </summary>
    [SugarColumn(ColumnName = "FPARTCHANGE")]
    public string Fpartchange { get; set; } = string.Empty;

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
