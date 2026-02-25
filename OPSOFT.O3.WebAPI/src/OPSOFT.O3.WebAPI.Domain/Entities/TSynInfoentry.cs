using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统同步信息表-数据映射表
/// </summary>
[SugarTable("T_SYN_INFOENTRY")]
public class TSynInfoentry : BaseEntity
{
    /// <summary>
    /// ERP 表ID
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEID")]
    public int Ftableid { get; set; }

    /// <summary>
    /// 是否主表   0=非 1=是
    /// </summary>
    [SugarColumn(ColumnName = "FISMAIN")]
    public bool Fismain { get; set; }

    /// <summary>
    /// 源数据类型  1=表;2=视图
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATATYPE")]
    public int Fsrcdatatype { get; set; }

    /// <summary>
    /// 源系统实体
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATANAME")]
    public string Fsrcdataname { get; set; } = string.Empty;

    /// <summary>
    /// 源单据描述
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATADES")]
    public string Fsrcdatades { get; set; } = string.Empty;

    /// <summary>
    /// 源唯一标识
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATAKEY")]
    public string Fsrcdatakey { get; set; } = string.Empty;

    /// <summary>
    /// 源时间戳
    /// </summary>
    [SugarColumn(ColumnName = "FTIMESTAMPNAME")]
    public string Ftimestampname { get; set; } = string.Empty;

    /// <summary>
    /// 过滤条件
    /// </summary>
    [SugarColumn(ColumnName = "FILTER")]
    public string Filter { get; set; } = string.Empty;

    /// <summary>
    /// 目标数据类型(0=自定义;1=表;2=视图)
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATATYPE")]
    public int Faimdatatype { get; set; }

    /// <summary>
    /// O3目标单据实体
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATANAME")]
    public string Faimdataname { get; set; } = string.Empty;

    /// <summary>
    /// O3目标单据描述
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATADES")]
    public string Faimdatades { get; set; } = string.Empty;

    /// <summary>
    /// O3目标单据唯一标识
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATAKEY")]
    public string Faimdatakey { get; set; } = string.Empty;

    /// <summary>
    /// 关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FRELATIONFIELD")]
    public string Frelationfield { get; set; } = string.Empty;

    /// <summary>
    /// 重复是否覆盖
    /// </summary>
    [SugarColumn(ColumnName = "FISCOVER")]
    public bool Fiscover { get; set; }

    /// <summary>
    /// ERP查询标识脚本
    /// </summary>
    [SugarColumn(ColumnName = "FIELDKEYS")]
    public string Fieldkeys { get; set; } = string.Empty;

    /// <summary>
    /// 同步过滤条件
    /// </summary>
    [SugarColumn(ColumnName = "FRULEID")]
    public string Fruleid { get; set; } = string.Empty;

    /// <summary>
    /// ERP表单代码
    /// </summary>
    [SugarColumn(ColumnName = "FERPBILLID")]
    public string Ferpbillid { get; set; } = string.Empty;

    /// <summary>
    /// ERP排序标识
    /// </summary>
    [SugarColumn(ColumnName = "FORDERSTRING")]
    public string Forderstring { get; set; } = string.Empty;

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
