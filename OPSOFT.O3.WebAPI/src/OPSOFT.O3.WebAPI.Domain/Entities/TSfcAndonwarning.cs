using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 安灯预警PC界面(异常呼叫控制表)
/// </summary>
[SugarTable("T_SFC_ANDONWARNING")]
public class TSfcAndonwarning : BaseEntity
{
    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKFLOWCARDDETAILID")]
    public string Fworkflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 呼叫事件
    /// </summary>
    [SugarColumn(ColumnName = "FEVENTID")]
    public string Feventid { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FDULESTATUS")]
    public int Fdulestatus { get; set; }

    /// <summary>
    /// 触发人（呼叫人）
    /// </summary>
    [SugarColumn(ColumnName = "FCALLID")]
    public string Fcallid { get; set; } = string.Empty;

    /// <summary>
    /// 触发时间（呼叫时间）
    /// </summary>
    [SugarColumn(ColumnName = "FCALLTIME")]
    public DateTime? Fcalltime { get; set; }

    /// <summary>
    /// 处理人
    /// </summary>
    [SugarColumn(ColumnName = "FDULEID")]
    public string Fduleid { get; set; } = string.Empty;

    /// <summary>
    /// 处理完成时间
    /// </summary>
    [SugarColumn(ColumnName = "FDULETIME")]
    public DateTime? Fduletime { get; set; }

    /// <summary>
    /// 处理结果
    /// </summary>
    [SugarColumn(ColumnName = "FDULERESULT")]
    public string Fduleresult { get; set; } = string.Empty;

    /// <summary>
    /// 关闭人
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSEID")]
    public string Fcloseid { get; set; } = string.Empty;

    /// <summary>
    /// 关闭时间
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSETIME")]
    public DateTime? Fclosetime { get; set; }

    /// <summary>
    /// 是否填写处理结果
    /// </summary>
    [SugarColumn(ColumnName = "FISMUSTDULE")]
    public bool Fismustdule { get; set; }

    /// <summary>
    /// 响应人
    /// </summary>
    [SugarColumn(ColumnName = "FRESPONSEID")]
    public string Fresponseid { get; set; } = string.Empty;

    /// <summary>
    /// 响应时间
    /// </summary>
    [SugarColumn(ColumnName = "FRESPONSETIME")]
    public DateTime? Fresponsetime { get; set; }

    /// <summary>
    /// 资源
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 产品
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 是否通已经通知硬件安灯
    /// </summary>
    [SugarColumn(ColumnName = "FISNOTIFIANDON")]
    public bool Fisnotifiandon { get; set; }

    /// <summary>
    /// 是否是硬件安灯
    /// </summary>
    [SugarColumn(ColumnName = "FHARDWARE")]
    public bool Fhardware { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTID")]
    public string Fequipmentid { get; set; } = string.Empty;

    /// <summary>
    /// 预计换型开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTIME")]
    public DateTime? Fstime { get; set; }

    /// <summary>
    /// 缺料预警是表示（还可以支持多少分钟）
    /// </summary>
    [SugarColumn(ColumnName = "FRATE")]
    public decimal Frate { get; set; }

    /// <summary>
    /// 汇报单号
    /// </summary>
    [SugarColumn(ColumnName = "FREBILLNO")]
    public string Frebillno { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
