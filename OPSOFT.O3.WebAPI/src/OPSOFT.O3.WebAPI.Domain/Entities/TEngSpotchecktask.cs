using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 点检任务
/// </summary>
[SugarTable("T_ENG_SPOTCHECKTASK")]
public class TEngSpotchecktask : BaseEntity
{
    /// <summary>
    /// 任务名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEID")]
    public string Fmachineid { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FEFFECTIVEDATE")]
    public DateTime? Feffectivedate { get; set; }

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 完成状态；0=待检；1=进行中；2=已完成
    /// </summary>
    [SugarColumn(ColumnName = "FFTYPE")]
    public int Fftype { get; set; }

    /// <summary>
    /// 负责人内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 是否委外
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIROUT")]
    public bool Frepairout { get; set; }

    /// <summary>
    /// 单据来源 1=手工生成;2=自动生成
    /// </summary>
    [SugarColumn(ColumnName = "FCTYPE")]
    public int Fctype { get; set; }

    /// <summary>
    /// 计划内码
    /// </summary>
    [SugarColumn(ColumnName = "FPLANID")]
    public string Fplanid { get; set; } = string.Empty;

    /// <summary>
    /// 班次
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTID")]
    public string Fshiftid { get; set; } = string.Empty;

    /// <summary>
    /// 是否产生维修单
    /// </summary>
    [SugarColumn(ColumnName = "FISCREATEREPAIR")]
    public bool Fiscreaterepair { get; set; }

    /// <summary>
    /// 维修单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRID")]
    public string Frepairid { get; set; } = string.Empty;

    /// <summary>
    /// 是否显示上下限值
    /// </summary>
    [SugarColumn(ColumnName = "FISSHOWLIMIT")]
    public bool Fisshowlimit { get; set; }

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
    /// 任务类别
    /// </summary>
    [SugarColumn(ColumnName = "FTASKTYPEID")]
    public string Ftasktypeid { get; set; } = string.Empty;

    /// <summary>
    /// 生效时长(H
    /// </summary>
    [SugarColumn(ColumnName = "FEFFECTIVESPAN")]
    public decimal Feffectivespan { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;
}
