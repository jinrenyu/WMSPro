using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 同步-数据映射表
/// </summary>
[SugarTable("T_BD_SynDataMappingEntry")]
public class TBdSynDataMappingEntry : BaseEntryEntity
{
    /// <summary>
    /// ERP 表ID
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEID")]
    public int FTableId { get; set; }

    /// <summary>
    /// 是否主表
    /// </summary>
    [SugarColumn(ColumnName = "FISMAIN")]
    public bool FIsMain { get; set; }

    /// <summary>
    /// 源数据类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATATYPE")]
    public int FSrcDataType { get; set; }

    /// <summary>
    /// 源系统实体
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATANAME")]
    public string FSrcDataName { get; set; } = string.Empty;

    /// <summary>
    /// 源系统实体主表
    /// </summary>
    [SugarColumn(ColumnName = "FSRCTABLENAME")]
    public string FSrcTableName { get; set; } = string.Empty;

    /// <summary>
    /// 源单据描述
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATADES")]
    public string FSrcDataDes { get; set; } = string.Empty;

    /// <summary>
    /// 源唯一标识
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATAKEY")]
    public string FSrcDataKey { get; set; } = string.Empty;

    /// <summary>
    /// 源时间戳
    /// </summary>
    [SugarColumn(ColumnName = "FTIMESTAMPNAME")]
    public string FTimestampName { get; set; } = string.Empty;

    /// <summary>
    /// 过滤条件
    /// </summary>
    [SugarColumn(ColumnName = "FILTER")]
    public string Filter { get; set; } = string.Empty;

    /// <summary>
    /// 目标数据类型
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATATYPE")]
    public int FAimDataType { get; set; }

    /// <summary>
    /// O3目标单据实体
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATANAME")]
    public string FAimDataName { get; set; } = string.Empty;

    /// <summary>
    /// O3目标单据描述
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATADES")]
    public string FAimDataDes { get; set; } = string.Empty;

    /// <summary>
    /// O3目标单据唯一标识
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATAKEY")]
    public string FAimDataKey { get; set; } = string.Empty;

    /// <summary>
    /// 关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FRELATIONFIELD")]
    public string FRelationField { get; set; } = string.Empty;

    /// <summary>
    /// 重复是否覆盖
    /// </summary>
    [SugarColumn(ColumnName = "FISCOVER")]
    public bool FIsCover { get; set; }

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
