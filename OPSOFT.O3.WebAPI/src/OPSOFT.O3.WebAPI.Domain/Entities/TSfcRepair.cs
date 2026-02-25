using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修汇报--送修单
/// </summary>
[SugarTable("T_SFC_REPAIR")]
public class TSfcRepair : BaseEntity
{
    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 委外发出源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBILLID")]
    public string Fsourcebillid { get; set; } = string.Empty;

    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCESID")]
    public string Fresourcesid { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTID")]
    public string Fequipmentid { get; set; } = string.Empty;

    /// <summary>
    /// 班组内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEAMID")]
    public string Fteamid { get; set; } = string.Empty;

    /// <summary>
    /// 班次内码
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTID")]
    public string Fshiftid { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 任务单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 产品\物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 维修站内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRID")]
    public string Frepairid { get; set; } = string.Empty;

    /// <summary>
    /// 回流工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FRETPROCESSID")]
    public string Fretprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 当前状态
    /// </summary>
    [SugarColumn(ColumnName = "FRUNTYPE")]
    public string Fruntype { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPE")]
    public string Fbilltype { get; set; } = string.Empty;

    /// <summary>
    /// 前道在制品条码
    /// </summary>
    [SugarColumn(ColumnName = "FWIPNUMBER")]
    public string Fwipnumber { get; set; } = string.Empty;

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
