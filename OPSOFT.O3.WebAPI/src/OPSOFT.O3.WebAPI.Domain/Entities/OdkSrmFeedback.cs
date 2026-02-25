using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 质量问题反馈单
/// </summary>
[SugarTable("ODK_SRM_Feedback")]
public class OdkSrmFeedback : BaseEntity
{
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 收货单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEINTERID")]
    public string Fsourceinterid { get; set; } = string.Empty;

    /// <summary>
    /// 收货单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEDETAILID")]
    public string Fsourcedetailid { get; set; } = string.Empty;

    /// <summary>
    /// 收货单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEENTRYID")]
    public string Fsourceentryid { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

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
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 处理结果
    /// </summary>
    [SugarColumn(ColumnName = "FRESULT")]
    public string Fresult { get; set; } = string.Empty;

    /// <summary>
    /// 供应商需回复日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUNREPLYDATE")]
    public DateTime? Fsunreplydate { get; set; }

    /// <summary>
    /// 供应商实物退货需处理日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUHANDLEDATE")]
    public DateTime? Fsuhandledate { get; set; }

    /// <summary>
    /// 实物处理日期
    /// </summary>
    [SugarColumn(ColumnName = "FHANDLEDATE")]
    public DateTime? Fhandledate { get; set; }

    /// <summary>
    /// 供应商回复日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUREPLYDATE")]
    public DateTime? Fsureplydate { get; set; }

    /// <summary>
    /// 退货接收状态
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNSTATUS")]
    public int Freturnstatus { get; set; }

    /// <summary>
    /// 币种
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 索赔金额
    /// </summary>
    [SugarColumn(ColumnName = "FCLAIMAMT")]
    public decimal Fclaimamt { get; set; }

    /// <summary>
    /// 反馈来源
    /// </summary>
    [SugarColumn(ColumnName = "FFEEDSOURCE")]
    public string Ffeedsource { get; set; } = string.Empty;

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
    /// 品质部跟踪验证
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFICATION")]
    public string Fverification { get; set; } = string.Empty;

    /// <summary>
    /// 供方附件附件
    /// </summary>
    [SugarColumn(ColumnName = "FSUPAPPEND")]
    public string Fsupappend { get; set; } = string.Empty;

    /// <summary>
    /// 采购方附件
    /// </summary>
    [SugarColumn(ColumnName = "FPURAPPEND")]
    public string Fpurappend { get; set; } = string.Empty;

    /// <summary>
    /// 退货接受原因理由
    /// </summary>
    [SugarColumn(ColumnName = "FREFUREASON")]
    public string Frefureason { get; set; } = string.Empty;

    /// <summary>
    /// 已读状态
    /// </summary>
    [SugarColumn(ColumnName = "FHASREAD")]
    public int Fhasread { get; set; }

    /// <summary>
    /// 已读日期
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME")]
    public DateTime? Fhasreadtime { get; set; }

    /// <summary>
    /// 完成日期
    /// </summary>
    [SugarColumn(ColumnName = "FFINISHDATE")]
    public DateTime? Ffinishdate { get; set; }

    /// <summary>
    /// 是否发布
    /// </summary>
    [SugarColumn(ColumnName = "FISRELEASE")]
    public bool Fisrelease { get; set; }

    /// <summary>
    /// 审核人内码
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
