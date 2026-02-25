using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 绩效改善单
/// </summary>
[SugarTable("ODK_SRM_Improve")]
public class OdkSrmImprove : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 计划员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

    /// <summary>
    /// 改善类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETYPE")]
    public string Fsourcetype { get; set; } = string.Empty;

    /// <summary>
    /// 供应商需回复日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUNREPLYDATE")]
    public DateTime? Fsunreplydate { get; set; }

    /// <summary>
    /// 供应商回复日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUREPLYDATE")]
    public DateTime? Fsureplydate { get; set; }

    /// <summary>
    /// 处理结果
    /// </summary>
    [SugarColumn(ColumnName = "FRESULT")]
    public string Fresult { get; set; } = string.Empty;

    /// <summary>
    /// 改善确认日期
    /// </summary>
    [SugarColumn(ColumnName = "FFINISHDATE")]
    public DateTime? Ffinishdate { get; set; }

    /// <summary>
    /// 是否重复问题
    /// </summary>
    [SugarColumn(ColumnName = "FREPEATPROBLEM")]
    public bool Frepeatproblem { get; set; }

    /// <summary>
    /// 问题描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 处理意见
    /// </summary>
    [SugarColumn(ColumnName = "FSUGGESTION")]
    public string Fsuggestion { get; set; } = string.Empty;

    /// <summary>
    /// 供应商根本原因分析
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSIS")]
    public string Fanalysis { get; set; } = string.Empty;

    /// <summary>
    /// 供应商整改措施
    /// </summary>
    [SugarColumn(ColumnName = "FMEASURES")]
    public string Fmeasures { get; set; } = string.Empty;

    /// <summary>
    /// 供方附件
    /// </summary>
    [SugarColumn(ColumnName = "FSUPAPPEND")]
    public string Fsupappend { get; set; } = string.Empty;

    /// <summary>
    /// 采购方附件
    /// </summary>
    [SugarColumn(ColumnName = "FPURAPPEND")]
    public string Fpurappend { get; set; } = string.Empty;

    /// <summary>
    /// 已读状态
    /// </summary>
    [SugarColumn(ColumnName = "FHASREAD")]
    public int Fhasread { get; set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME")]
    public DateTime? Fhasreadtime { get; set; }

    /// <summary>
    /// 改善理由
    /// </summary>
    [SugarColumn(ColumnName = "FREFUREASON")]
    public string Frefureason { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL")]
    public int Fchecklevel { get; set; }

    /// <summary>
    /// 改善来源
    /// </summary>
    [SugarColumn(ColumnName = "FIMPROVEMENTSOURCE")]
    public string Fimprovementsource { get; set; } = string.Empty;

    /// <summary>
    /// 改善来源单号内码
    /// </summary>
    [SugarColumn(ColumnName = "FIMPROVEMENTSOURCEID")]
    public string Fimprovementsourceid { get; set; } = string.Empty;

    /// <summary>
    /// 是否发布
    /// </summary>
    [SugarColumn(ColumnName = "FISRELEASE")]
    public bool Fisrelease { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
