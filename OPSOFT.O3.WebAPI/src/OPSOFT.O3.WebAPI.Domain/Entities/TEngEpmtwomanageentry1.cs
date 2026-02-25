using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 故障维修工单 表体 工单内容
/// </summary>
[SugarTable("T_ENG_EPMTWOMANAGEENTRY1")]
public class TEngEpmtwomanageentry1 : BaseEntity
{
    /// <summary>
    /// 设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 内容及要求
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENTSANDREQUIREMENTS")]
    public string Fcontentsandrequirements { get; set; } = string.Empty;

    /// <summary>
    /// 计划设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FPMACHINEPARTID")]
    public string Fpmachinepartid { get; set; } = string.Empty;

    /// <summary>
    /// 计划项目名称
    /// </summary>
    [SugarColumn(ColumnName = "FPNAME")]
    public string Fpname { get; set; } = string.Empty;

    /// <summary>
    /// 计划内容及要求
    /// </summary>
    [SugarColumn(ColumnName = "FPCONTENTSANDREQUIREMENTS")]
    public string Fpcontentsandrequirements { get; set; } = string.Empty;

    /// <summary>
    /// 状况
    /// </summary>
    [SugarColumn(ColumnName = "FCONDITION")]
    public string Fcondition { get; set; } = string.Empty;

    /// <summary>
    /// 评分
    /// </summary>
    [SugarColumn(ColumnName = "FSCORE")]
    public decimal Fscore { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    [SugarColumn(ColumnName = "FEXPLAIN")]
    public string Fexplain { get; set; } = string.Empty;

    /// <summary>
    /// 工种内码
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTID")]
    public string Fcraftid { get; set; } = string.Empty;

    /// <summary>
    /// 维护人员内码
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTMANID")]
    public string Fmaintmanid { get; set; } = string.Empty;

    /// <summary>
    /// 实际工时
    /// </summary>
    [SugarColumn(ColumnName = "FACRAFTTIME")]
    public decimal Facrafttime { get; set; }

    /// <summary>
    /// 工时费用
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTCOST")]
    public decimal Fcraftcost { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 是否完成
    /// </summary>
    [SugarColumn(ColumnName = "FISFINISHED")]
    public bool Fisfinished { get; set; }

    /// <summary>
    /// 工单状态(0=未执行，1=执行中，2=完成
    /// </summary>
    [SugarColumn(ColumnName = "FORDERSTATUS")]
    public int Forderstatus { get; set; }
}
