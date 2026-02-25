using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备维护方案-设备预防性保养
/// </summary>
[SugarTable("T_ENG_EQMTTASKPROJECTENTRY1")]
public class TEngEqmttaskprojectentry1 : BaseEntity
{
    /// <summary>
    /// 内容及要求
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENTSANDREQUIREMENTS")]
    public string Fcontentsandrequirements { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位
    /// </summary>
    [SugarColumn(ColumnName = "FPART")]
    public string Fpart { get; set; } = string.Empty;

    /// <summary>
    /// 设备部位内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPARTID")]
    public string Fmachinepartid { get; set; } = string.Empty;

    /// <summary>
    /// 维护周期
    /// </summary>
    [SugarColumn(ColumnName = "FCYCLE")]
    public int Fcycle { get; set; }

    /// <summary>
    /// 工种内码
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTID")]
    public string Fcraftid { get; set; } = string.Empty;

    /// <summary>
    /// 工时
    /// </summary>
    [SugarColumn(ColumnName = "FCRAFTTIME")]
    public decimal Fcrafttime { get; set; }

    /// <summary>
    /// 是否停机
    /// </summary>
    [SugarColumn(ColumnName = "FISSTOPMACHINE")]
    public bool Fisstopmachine { get; set; }

    /// <summary>
    /// 维护标准
    /// </summary>
    [SugarColumn(ColumnName = "FSTANDARD")]
    public string Fstandard { get; set; } = string.Empty;

    /// <summary>
    /// 时间单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNIT")]
    public int Funit { get; set; }

    /// <summary>
    /// 维护方法
    /// </summary>
    [SugarColumn(ColumnName = "FMETHOD")]
    public string Fmethod { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 上次保养时间
    /// </summary>
    [SugarColumn(ColumnName = "FPREVMAINTDATE")]
    public DateTime? Fprevmaintdate { get; set; }

    /// <summary>
    /// 下次保养时间
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTMAINTDATE")]
    public DateTime? Fnextmaintdate { get; set; }

    /// <summary>
    /// 保养次数
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTCOUNT")]
    public int Fmaintcount { get; set; }

    /// <summary>
    /// 保养状态
    /// </summary>
    [SugarColumn(ColumnName = "FMAINTSTATUS")]
    public int Fmaintstatus { get; set; }

    /// <summary>
    /// 设备维护内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEMAINTID")]
    public string Fmachinemaintid { get; set; } = string.Empty;

    /// <summary>
    /// 设备启用时间
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINESTARTDATE")]
    public DateTime? Fmachinestartdate { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
