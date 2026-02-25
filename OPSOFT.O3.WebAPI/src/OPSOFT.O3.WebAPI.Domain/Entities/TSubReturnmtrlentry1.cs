using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外退料条码明细
/// </summary>
[SugarTable("T_SUB_RETURNMTRLENTRY1")]
public class TSubReturnmtrlentry1 : BaseEntity
{
    /// <summary>
    /// 生产订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public string Fmoentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYSEQ")]
    public int Fmoentryseq { get; set; }

    /// <summary>
    /// 生产用料清单内码
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMID")]
    public string Fppbomid { get; set; } = string.Empty;

    /// <summary>
    /// 生产用料清单编码
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMBILLNO")]
    public string Fppbombillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产用料清单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMENTRYID")]
    public string Fppbomentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产用料清单行号
    /// </summary>
    [SugarColumn(ColumnName = "FPPBOMENTRYSEQ")]
    public int Fppbomentryseq { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 有效期至(变更后
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }
}
