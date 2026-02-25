using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// IQC物料明细
/// </summary>
[SugarTable("T_BD_IQCDETAILED")]
public class TBdIqcdetailed : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 报废数
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPNUM")]
    public decimal Fscrapnum { get; set; }

    /// <summary>
    /// 不良
    /// </summary>
    [SugarColumn(ColumnName = "FBAD")]
    public string Fbad { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 检验批次
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONLOT")]
    public string Finspectionlot { get; set; } = string.Empty;

    /// <summary>
    /// 检验结果
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKRESULT")]
    public string Fcheckresult { get; set; } = string.Empty;

    /// <summary>
    /// MRB评审结果
    /// </summary>
    [SugarColumn(ColumnName = "FMRBREVIEW")]
    public string Fmrbreview { get; set; } = string.Empty;

    /// <summary>
    /// DC
    /// </summary>
    [SugarColumn(ColumnName = "FDC")]
    public string Fdc { get; set; } = string.Empty;

    /// <summary>
    /// 生产批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 制造商内码
    /// </summary>
    [SugarColumn(ColumnName = "FMANUFACTURERID")]
    public string Fmanufacturerid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FREMARKS")]
    public string Fremarks { get; set; } = string.Empty;

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
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID")]
    public string Fsrcid { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID")]
    public string Fsrcformid { get; set; } = string.Empty;

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID")]
    public string Fsrcentryid { get; set; } = string.Empty;

    /// <summary>
    /// 是否不良
    /// </summary>
    [SugarColumn(ColumnName = "FISBAD")]
    public bool Fisbad { get; set; }

    /// <summary>
    /// 来源条码
    /// </summary>
    [SugarColumn(ColumnName = "FFBARCODE")]
    public string Ffbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 挑选后结果
    /// </summary>
    [SugarColumn(ColumnName = "FSELECTAFTERRESULT")]
    public string Fselectafterresult { get; set; } = string.Empty;

    /// <summary>
    /// 保质期限
    /// </summary>
    [SugarColumn(ColumnName = "FKFPERIOD")]
    public int Fkfperiod { get; set; }

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 过期日期
    /// </summary>
    [SugarColumn(ColumnName = "FOUTOFDATE")]
    public DateTime? Foutofdate { get; set; }

    /// <summary>
    /// 条码备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 处置意见
    /// </summary>
    [SugarColumn(ColumnName = "FOPINION")]
    public string Fopinion { get; set; } = string.Empty;

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
