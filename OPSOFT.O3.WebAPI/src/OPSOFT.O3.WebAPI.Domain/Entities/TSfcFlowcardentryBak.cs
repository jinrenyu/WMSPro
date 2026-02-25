using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡表体-工序-备份
/// </summary>
[SugarTable("T_SFC_FLOWCARDENTRY_BAK")]
public class TSfcFlowcardentryBak : BaseEntity
{
    /// <summary>
    /// 变更单单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHGINTERID")]
    public string Fchginterid { get; set; } = string.Empty;

    /// <summary>
    /// 变更单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHGDETAILID")]
    public string Fchgdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 工序序号
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSSEQ")]
    public string Fprocessseq { get; set; } = string.Empty;

    /// <summary>
    /// 工序状态
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSSTATUS")]
    public int Fprocessstatus { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal Fauxqty { get; set; }

    /// <summary>
    /// 流转数量
    /// </summary>
    [SugarColumn(ColumnName = "FREMQTY")]
    public decimal Fremqty { get; set; }

    /// <summary>
    /// 单位转换率
    /// </summary>
    [SugarColumn(ColumnName = "FUNITCONVERT")]
    public decimal Funitconvert { get; set; }

    /// <summary>
    /// 计划开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTDATE")]
    public DateTime? Fplanstartdate { get; set; }

    /// <summary>
    /// 计划完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANFINISHDATE")]
    public DateTime? Fplanfinishdate { get; set; }

    /// <summary>
    /// 实际开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALSTARTDATE")]
    public DateTime? Factualstartdate { get; set; }

    /// <summary>
    /// 实际完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALFINISHDATE")]
    public DateTime? Factualfinishdate { get; set; }

    /// <summary>
    /// 关闭状态
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSED")]
    public bool Fclosed { get; set; }

    /// <summary>
    /// 是否挂起
    /// </summary>
    [SugarColumn(ColumnName = "FHANGUP")]
    public bool Fhangup { get; set; }

    /// <summary>
    /// 是否已确认
    /// </summary>
    [SugarColumn(ColumnName = "FISSCHEDULED")]
    public bool Fisscheduled { get; set; }

    /// <summary>
    /// 完工数量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALFINISHQTY")]
    public decimal Ftotalfinishqty { get; set; }

    /// <summary>
    /// 合格数
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public decimal Fquaqty { get; set; }

    /// <summary>
    /// 不良品数
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 检验方案
    /// </summary>
    [SugarColumn(ColumnName = "FQCTYPE")]
    public string Fqctype { get; set; } = string.Empty;

    /// <summary>
    /// 条码打印方式
    /// </summary>
    [SugarColumn(ColumnName = "FPRSEQBAR")]
    public int Fprseqbar { get; set; }

    /// <summary>
    /// 标准换型时间(分
    /// </summary>
    [SugarColumn(ColumnName = "FRETIME")]
    public decimal Fretime { get; set; }
}
