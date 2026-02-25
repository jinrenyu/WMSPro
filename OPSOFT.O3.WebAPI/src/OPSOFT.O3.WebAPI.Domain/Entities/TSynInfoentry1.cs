using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统同步信息表-数据映射字段
/// </summary>
[SugarTable("T_SYN_INFOENTRY1")]
public class TSynInfoentry1 : BaseEntity
{
    /// <summary>
    /// 第一源数据字段
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFIELD")]
    public string Fsrcfield { get; set; } = string.Empty;

    /// <summary>
    /// 源数据字段描述
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFIELDDES")]
    public string Fsrcfielddes { get; set; } = string.Empty;

    /// <summary>
    /// 源主键   0=非 1=是
    /// </summary>
    [SugarColumn(ColumnName = "FISSRCFIELDKEY")]
    public bool Fissrcfieldkey { get; set; }

    /// <summary>
    /// 目标字段
    /// </summary>
    [SugarColumn(ColumnName = "FAIMFIELD")]
    public string Faimfield { get; set; } = string.Empty;

    /// <summary>
    /// 目标字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FAIMFIELDTYPE")]
    public string Faimfieldtype { get; set; } = string.Empty;

    /// <summary>
    /// 目标字段描述
    /// </summary>
    [SugarColumn(ColumnName = "FAIMFIELDDES")]
    public string Faimfielddes { get; set; } = string.Empty;

    /// <summary>
    /// 目标主键 0=非 1=是
    /// </summary>
    [SugarColumn(ColumnName = "FAIMFIELDKEY")]
    public bool Faimfieldkey { get; set; }

    /// <summary>
    /// 是否必录 0=非 1=是
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTINPUT")]
    public bool Fmustinput { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    [SugarColumn(ColumnName = "FDEFAULTVALUE")]
    public string Fdefaultvalue { get; set; } = string.Empty;

    /// <summary>
    /// 是否设定固定值
    /// </summary>
    [SugarColumn(ColumnName = "FISSETFIXED")]
    public bool Fissetfixed { get; set; }

    /// <summary>
    /// 固定值内码
    /// </summary>
    [SugarColumn(ColumnName = "FFIXEDDETAILID")]
    public string Ffixeddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 固定值
    /// </summary>
    [SugarColumn(ColumnName = "FFIXEDVALUE")]
    public string Ffixedvalue { get; set; } = string.Empty;

    /// <summary>
    /// 关联表
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPCLASSID")]
    public string Flookupclassid { get; set; } = string.Empty;

    /// <summary>
    /// 关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPFIELD")]
    public string Flookupfield { get; set; } = string.Empty;

    /// <summary>
    /// 关联取值
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPSAVEFIELD")]
    public string Flookupsavefield { get; set; } = string.Empty;

    /// <summary>
    /// 判断是否存在 0=不存在 1=存在
    /// </summary>
    [SugarColumn(ColumnName = "FISEXITS")]
    public bool Fisexits { get; set; }

    /// <summary>
    /// 是否加入日志 0=不加入 1=加入
    /// </summary>
    [SugarColumn(ColumnName = "FISLOGSHOW")]
    public bool Fislogshow { get; set; }

    /// <summary>
    /// 是否唯一内码
    /// </summary>
    [SugarColumn(ColumnName = "FISINNERID")]
    public bool Fisinnerid { get; set; }

    /// <summary>
    /// 字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDTYPE")]
    public int Ffieldtype { get; set; }

    /// <summary>
    /// ERP标识对应顺序
    /// </summary>
    [SugarColumn(ColumnName = "FERPSEQ")]
    public string Ferpseq { get; set; } = string.Empty;

    /// <summary>
    /// 第二源数据字段
    /// </summary>
    [SugarColumn(ColumnName = "FSECONDSRCFIELD")]
    public string Fsecondsrcfield { get; set; } = string.Empty;

    /// <summary>
    /// 第三源数据字段
    /// </summary>
    [SugarColumn(ColumnName = "FTHIRDSRCFIELD")]
    public string Fthirdsrcfield { get; set; } = string.Empty;

    /// <summary>
    /// 是否排序源字段
    /// </summary>
    [SugarColumn(ColumnName = "FISSORT")]
    public bool Fissort { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
