using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 虚拟线边仓物料转移-条码
/// </summary>
[SugarTable("T_PRD_WORKSHOPVIRTUALSTOCKTRANSENTRY")]
public class TPrdWorkshopvirtualstocktransentry : BaseEntity
{
    /// <summary>
    /// 类别编号
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 是否箱码
    /// </summary>
    [SugarColumn(ColumnName = "FISBOX")]
    public bool Fisbox { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 转移数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 目标仓库
    /// </summary>
    [SugarColumn(ColumnName = "FTARGETSTOCKID")]
    public string Ftargetstockid { get; set; } = string.Empty;

    /// <summary>
    /// 目标仓位
    /// </summary>
    [SugarColumn(ColumnName = "FTARGETSTOCKLOCID")]
    public string Ftargetstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 调出仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSRCSTOCKID")]
    public string Fsrcstockid { get; set; } = string.Empty;

    /// <summary>
    /// 调出仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSRCSTOCKLOCID")]
    public string Fsrcstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 生产/采购日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 保质期单位
    /// </summary>
    [SugarColumn(ColumnName = "FKFUNIT")]
    public int Fkfunit { get; set; }

    /// <summary>
    /// 保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFPERIOD")]
    public int Fkfperiod { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 任务单号
    /// </summary>
    [SugarColumn(ColumnName = "FICMOBILLNO")]
    public string Ficmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FICMOID")]
    public string Ficmoid { get; set; } = string.Empty;

    /// <summary>
    /// 任务单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FICMODETAILID")]
    public string Ficmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FPTNO")]
    public string Fptno { get; set; } = string.Empty;

    /// <summary>
    /// 下道工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FHPROCESSID")]
    public string Fhprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 下道工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FHPTNO")]
    public string Fhptno { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FBARSTATUS")]
    public int Fbarstatus { get; set; }

    /// <summary>
    /// 质量类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 条码变更状态
    /// </summary>
    [SugarColumn(ColumnName = "FCHANGESTATUS")]
    public int Fchangestatus { get; set; }

    /// <summary>
    /// 转移条码类别
    /// </summary>
    [SugarColumn(ColumnName = "FBARTYPE")]
    public int Fbartype { get; set; }

    /// <summary>
    /// 是否是委外退货条码
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNSUPPLYYN")]
    public bool Freturnsupplyyn { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 资源
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCESID")]
    public string Fresourcesid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 生产用料清单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMENTRYID")]
    public string Fppbomentryid { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODETYPE")]
    public int Fbarcodetype { get; set; }

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
