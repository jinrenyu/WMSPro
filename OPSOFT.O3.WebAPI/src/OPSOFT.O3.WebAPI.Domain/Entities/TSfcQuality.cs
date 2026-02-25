using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// MES生产检验单信息
/// </summary>
[SugarTable("T_SFC_QUALITY")]
public class TSfcQuality : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 送检数量
    /// </summary>
    [SugarColumn(ColumnName = "FMQTY")]
    public decimal Fmqty { get; set; }

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FGOODQTY")]
    public decimal Fgoodqty { get; set; }

    /// <summary>
    /// 不合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 检验批次
    /// </summary>
    [SugarColumn(ColumnName = "FQCLOT")]
    public string Fqclot { get; set; } = string.Empty;

    /// <summary>
    /// 检验状态
    /// </summary>
    [SugarColumn(ColumnName = "FQCSTATE")]
    public string Fqcstate { get; set; } = string.Empty;

    /// <summary>
    /// 检验人
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTID")]
    public string Finspectid { get; set; } = string.Empty;

    /// <summary>
    /// 检验日期
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTDATE")]
    public DateTime? Finspectdate { get; set; }

    /// <summary>
    /// 检验部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 人工检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FQCRESULT")]
    public string Fqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 是否加急
    /// </summary>
    [SugarColumn(ColumnName = "FURGENT")]
    public bool Furgent { get; set; }

    /// <summary>
    /// 发起类型
    /// </summary>
    [SugarColumn(ColumnName = "FLAUTYPE")]
    public string Flautype { get; set; } = string.Empty;

    /// <summary>
    /// 是否免检
    /// </summary>
    [SugarColumn(ColumnName = "FISEXEMPT")]
    public bool Fisexempt { get; set; }

    /// <summary>
    /// 已审核
    /// </summary>
    [SugarColumn(ColumnName = "FISAUDIT")]
    public bool Fisaudit { get; set; }

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
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO")]
    public string Fsrcbillno { get; set; } = string.Empty;

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID")]
    public string Fsrcdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public int Fsrcentryid { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 系统检测结果
    /// </summary>
    [SugarColumn(ColumnName = "FSYSQCRESULT")]
    public string Fsysqcresult { get; set; } = string.Empty;

    /// <summary>
    /// 样本破坏数
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPQTY")]
    public decimal Fscrapqty { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 检验文件号
    /// </summary>
    [SugarColumn(ColumnName = "FFILENO")]
    public string Ffileno { get; set; } = string.Empty;

    /// <summary>
    /// 检验文件版本
    /// </summary>
    [SugarColumn(ColumnName = "FFILEAUXPROPID")]
    public string Ffileauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 图纸文件号
    /// </summary>
    [SugarColumn(ColumnName = "FDRAWNO")]
    public string Fdrawno { get; set; } = string.Empty;

    /// <summary>
    /// 图纸文件版本
    /// </summary>
    [SugarColumn(ColumnName = "FDRAWAUXPROPID")]
    public string Fdrawauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 提交人
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITID")]
    public string Fsubmitid { get; set; } = string.Empty;

    /// <summary>
    /// 确认人
    /// </summary>
    [SugarColumn(ColumnName = "FSUREID")]
    public string Fsureid { get; set; } = string.Empty;

    /// <summary>
    /// 最后暂存时间
    /// </summary>
    [SugarColumn(ColumnName = "FLASTSAVEDATE")]
    public DateTime? Flastsavedate { get; set; }

    /// <summary>
    /// MES资源
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 汇报单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTID")]
    public string Freportid { get; set; } = string.Empty;

    /// <summary>
    /// 汇报单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 检验方案内码
    /// </summary>
    [SugarColumn(ColumnName = "FINSSOLUID")]
    public string Finssoluid { get; set; } = string.Empty;

    /// <summary>
    /// 确认日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUREDATE")]
    public DateTime? Fsuredate { get; set; }

    /// <summary>
    /// 提交日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITDATE")]
    public DateTime? Fsubmitdate { get; set; }

    /// <summary>
    /// 检验区域内码
    /// </summary>
    [SugarColumn(ColumnName = "FAREAID")]
    public string Fareaid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
