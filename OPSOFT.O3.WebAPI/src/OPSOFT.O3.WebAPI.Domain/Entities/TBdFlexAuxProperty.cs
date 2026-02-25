using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 辅助属性
/// </summary>
[SugarTable("T_BD_FLEXAUXPROPERTY")]
public class TBdFlexauxproperty : BaseEntity
{
    /// <summary>
    /// 辅助属性编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 功能标识
    /// </summary>
    [SugarColumn(ColumnName = "FFORMID")]
    public string Fformid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 值类型
    /// </summary>
    [SugarColumn(ColumnName = "FVALUETYPE")]
    public string Fvaluetype { get; set; } = string.Empty;

    /// <summary>
    /// 值来源
    /// </summary>
    [SugarColumn(ColumnName = "FVALUESOURCE")]
    public string Fvaluesource { get; set; } = string.Empty;

    /// <summary>
    /// 系统预置
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

    /// <summary>
    /// 数据字段名称
    /// </summary>
    [SugarColumn(ColumnName = "FFLEXNUMBER")]
    public string Fflexnumber { get; set; } = string.Empty;

    /// <summary>
    /// 数据类型
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMDATATYPE")]
    public string Fcustomdatatype { get; set; } = string.Empty;

    /// <summary>
    /// 数据长度
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMDATASTRMAXLEN")]
    public int Fcustomdatastrmaxlen { get; set; }

    /// <summary>
    /// 固定列宽
    /// </summary>
    [SugarColumn(ColumnName = "FCOLWIDTH")]
    public decimal Fcolwidth { get; set; }

    /// <summary>
    /// 清除首尾空格
    /// </summary>
    [SugarColumn(ColumnName = "FTRIMONNUMBER")]
    public bool Ftrimonnumber { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime Fcheckdate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime Fdisabledate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 辅助属性类别代码
    /// </summary>
    [SugarColumn(ColumnName = "FFULLTYPENUMBER")]
    public string Ffulltypenumber { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性类别名称
    /// </summary>
    [SugarColumn(ColumnName = "FFULLTYPENAME")]
    public string Ffulltypename { get; set; } = string.Empty;
}
