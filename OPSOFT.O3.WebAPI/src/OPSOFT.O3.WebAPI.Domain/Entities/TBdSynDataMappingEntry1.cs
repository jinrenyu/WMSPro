using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 同步-数据映射字段
/// </summary>
[SugarTable("T_BD_SynDataMappingEntry1")]
public class TBdSynDataMappingEntry1 : BaseEntryEntity
{
    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string FBodyId { get; set; } = string.Empty;

    /// <summary>
    /// 源数据字段
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFIELD")]
    public string FSrcField { get; set; } = string.Empty;

    /// <summary>
    /// 源数据字段描述
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFIELDDES")]
    public string FSrcFieldDes { get; set; } = string.Empty;

    /// <summary>
    /// 源主键   0=非 1=是
    /// </summary>
    [SugarColumn(ColumnName = "FISSRCFIELDKEY")]
    public bool FIsSrcFieldKey { get; set; }

    /// <summary>
    /// 目标字段
    /// </summary>
    [SugarColumn(ColumnName = "FAIMFIELD")]
    public string FAimField { get; set; } = string.Empty;

    /// <summary>
    /// 目标字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FAIMFIELDTYPE")]
    public string FAimFieldType { get; set; } = string.Empty;

    /// <summary>
    /// 目标字段描述
    /// </summary>
    [SugarColumn(ColumnName = "FAIMFIELDDES")]
    public string FAimFieldDes { get; set; } = string.Empty;

    /// <summary>
    /// 目标主键 0=非 1=是
    /// </summary>
    [SugarColumn(ColumnName = "FAIMFIELDKEY")]
    public bool FAimFieldKey { get; set; }

    /// <summary>
    /// 是否必录 0=非 1=是
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTINPUT")]
    public bool FMustInput { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    [SugarColumn(ColumnName = "FDEFAULTVALUE")]
    public string FDefaultValue { get; set; } = string.Empty;

    /// <summary>
    /// 是否设定固定值
    /// </summary>
    [SugarColumn(ColumnName = "FISSETFIXED")]
    public bool FIsSetFixed { get; set; }

    /// <summary>
    /// 固定值内码
    /// </summary>
    [SugarColumn(ColumnName = "FFIXEDDETAILID")]
    public string FFixedDetailId { get; set; } = string.Empty;

    /// <summary>
    /// 固定值
    /// </summary>
    [SugarColumn(ColumnName = "FFIXEDVALUE")]
    public string FFixedValue { get; set; } = string.Empty;

    /// <summary>
    /// 关联表
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPCLASSID")]
    public string FLookupClassId { get; set; } = string.Empty;

    /// <summary>
    /// 关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPFIELD")]
    public string FLookupField { get; set; } = string.Empty;

    /// <summary>
    /// 关联取值
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPSAVEFIELD")]
    public string FLookupSaveField { get; set; } = string.Empty;

    /// <summary>
    /// 判断是否存在 0=不存在 1=存在
    /// </summary>
    [SugarColumn(ColumnName = "FISEXITS")]
    public bool FIsExits { get; set; }

    /// <summary>
    /// 是否加入日志 0=不加入 1=加入
    /// </summary>
    [SugarColumn(ColumnName = "FISLOGSHOW")]
    public bool FIsLogShow { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }
}
