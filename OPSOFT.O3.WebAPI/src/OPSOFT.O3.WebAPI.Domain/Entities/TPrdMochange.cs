using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产任务单变更
/// </summary>
[SugarTable("T_PRD_MOCHANGE")]
public class TPrdMochange : BaseEntity
{
    /// <summary>
    /// 生产任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTTYPE")]
    public string Fproducttype { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FCOMMONUNITID")]
    public string Fcommonunitid { get; set; } = string.Empty;

    /// <summary>
    /// 变更前生产数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 变更后生产数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHANGEQTY")]
    public decimal Fchangeqty { get; set; }

    /// <summary>
    /// 变更前计划开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTDATE")]
    public DateTime? Fplanstartdate { get; set; }

    /// <summary>
    /// 变更前计划完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANFINISHDATE")]
    public DateTime? Fplanfinishdate { get; set; }

    /// <summary>
    /// 变更后计划开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHANGEPLANSTARTDATE")]
    public DateTime? Fchangeplanstartdate { get; set; }

    /// <summary>
    /// 变更后计划完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHANGEPLANFINISHDATE")]
    public DateTime? Fchangeplanfinishdate { get; set; }

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPARTMENTID")]
    public string Fdepartmentid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 批次内码
    /// </summary>
    [SugarColumn(ColumnName = "FLOTID")]
    public string Flotid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 备用栏位
    /// </summary>
    [SugarColumn(ColumnName = "FBCTYPE")]
    public string Fbctype { get; set; } = string.Empty;

    /// <summary>
    /// 产品工艺流程内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROROUTEID")]
    public string Fprorouteid { get; set; } = string.Empty;

    /// <summary>
    /// ERP工单号
    /// </summary>
    [SugarColumn(ColumnName = "FERPICMONO")]
    public string Ferpicmono { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 派工数量
    /// </summary>
    [SugarColumn(ColumnName = "FFCQTY")]
    public decimal Ffcqty { get; set; }

    /// <summary>
    /// 实际开工时间
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALSTARTDATE")]
    public DateTime? Factualstartdate { get; set; }

    /// <summary>
    /// 实际完工时间
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALFINISHDATE")]
    public DateTime? Factualfinishdate { get; set; }

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
    /// 条码规则
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODERULEID")]
    public string Fbarcoderuleid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
