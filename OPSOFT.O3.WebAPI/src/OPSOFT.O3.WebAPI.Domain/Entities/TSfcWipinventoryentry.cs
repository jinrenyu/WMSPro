using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 在制品盘点表体-盘点明细
/// </summary>
[SugarTable("T_SFC_WIPINVENTORYENTRY")]
public class TSfcWipinventoryentry : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

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
    /// 辅助单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITID")]
    public string Fsecunitid { get; set; } = string.Empty;

    /// <summary>
    /// 换算率
    /// </summary>
    [SugarColumn(ColumnName = "FSECCOEFFICIENT")]
    public decimal Fseccoefficient { get; set; }

    /// <summary>
    /// 默认入库仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 默认入库仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

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
    /// FMTONO
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 供应厂商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性类别
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPCLS")]
    public string Fauxpropcls { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECQTY")]
    public decimal Fsecqty { get; set; }

    /// <summary>
    /// 账存数量
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNTQTY")]
    public decimal Faccountqty { get; set; }

    /// <summary>
    /// 账存 辅助数量
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNTSECQTY")]
    public decimal Faccountsecqty { get; set; }

    /// <summary>
    /// 账存仓库内码
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNTSTOCKID")]
    public string Faccountstockid { get; set; } = string.Empty;

    /// <summary>
    /// 账存仓位内码
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNTSPID")]
    public string Faccountspid { get; set; } = string.Empty;

    /// <summary>
    /// 实存数量
    /// </summary>
    [SugarColumn(ColumnName = "FPRACTICALQTY")]
    public decimal Fpracticalqty { get; set; }

    /// <summary>
    /// 实存辅助数量
    /// </summary>
    [SugarColumn(ColumnName = "FPRACTICALSECQTY")]
    public decimal Fpracticalsecqty { get; set; }

    /// <summary>
    /// 实存仓库内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRACTICALSTOCKID")]
    public string Fpracticalstockid { get; set; } = string.Empty;

    /// <summary>
    /// 实存仓位内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRACTICALSPID")]
    public string Fpracticalspid { get; set; } = string.Empty;

    /// <summary>
    /// 工单内码
    /// </summary>
    [SugarColumn(ColumnName = "FICMOID")]
    public string Ficmoid { get; set; } = string.Empty;

    /// <summary>
    /// 工单编号
    /// </summary>
    [SugarColumn(ColumnName = "FICMOBILLNO")]
    public string Ficmobillno { get; set; } = string.Empty;

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
