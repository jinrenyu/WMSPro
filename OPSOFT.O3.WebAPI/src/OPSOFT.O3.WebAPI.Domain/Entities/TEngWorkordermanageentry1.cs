using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工单派工管理-工单明细
/// </summary>
[SugarTable("T_ENG_WORKORDERMANAGEENTRY1")]
public class TEngWorkordermanageentry1 : BaseEntity
{
    /// <summary>
    /// 内容及要求
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENTSANDREQUIREMENTS")]
    public string Fcontentsandrequirements { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位
    /// </summary>
    [SugarColumn(ColumnName = "FPART")]
    public string Fpart { get; set; } = string.Empty;

    /// <summary>
    /// 维护标准
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTSTANDARD")]
    public string Fmaintstandard { get; set; } = string.Empty;

    /// <summary>
    /// 维护方法
    /// </summary>
    [SugarColumn(ColumnName = "FMETHOD")]
    public string Fmethod { get; set; } = string.Empty;

    /// <summary>
    /// 是否完成
    /// </summary>
    [SugarColumn(ColumnName = "FISFINISHED")]
    public bool Fisfinished { get; set; }

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
    /// 工时
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTTIME")]
    public decimal Fcrafttime { get; set; }

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
    /// 状况
    /// </summary>
    [SugarColumn(ColumnName = "FCONDITION")]
    public string Fcondition { get; set; } = string.Empty;

    /// <summary>
    /// 评分
    /// </summary>
    [SugarColumn(ColumnName = "FSCORE")]
    public int Fscore { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    [SugarColumn(ColumnName = "FEXPLAIN")]
    public string Fexplain { get; set; } = string.Empty;

    /// <summary>
    /// 维护时间
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTDATE")]
    public DateTime? Fmaintdate { get; set; }

    /// <summary>
    /// 是否停机
    /// </summary>
    [SugarColumn(ColumnName = "FISSTOPMACHINE")]
    public bool Fisstopmachine { get; set; }

    /// <summary>
    /// 设备维护方案表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQMAINTENTRYDETAILID")]
    public string Feqmaintentrydetailid { get; set; } = string.Empty;

    /// <summary>
    /// 上传图片
    /// </summary>
    [SugarColumn(ColumnName = "FIMAGE")]
    public string Fimage { get; set; } = string.Empty;

    /// <summary>
    /// 保养状态(0=未执行，1=执行中，2=完成
    /// </summary>
    [SugarColumn(ColumnName = "FORDERSTATUS")]
    public int Forderstatus { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 执行结果
    /// </summary>
    [SugarColumn(ColumnName = "FRESULT", IsNullable = true)]
    public int? FRESULT { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }
}
