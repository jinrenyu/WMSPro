using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统表结构定义
/// </summary>
[SugarTable("SYS_TABLEDESCRIPTION")]
public class SysTableDescription : BaseEntity
{
    /// <summary>
    /// 实体表名
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 详细描述(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;

    /// <summary>
    /// 详细描述(第二语言)
    /// </summary>
    [SugarColumn(ColumnName = "FTNAME", IsNullable = true)]
    public string FTNAME { get; set; } = string.Empty;

    /// <summary>
    /// 否打开审计日志
    /// </summary>
    [SugarColumn(ColumnName = "FENABLEAUDITLOG", IsNullable = true)]
    public bool? FENABLEAUDITLOG { get; set; }

    /// <summary>
    /// 表属性(1=系统表结构;2=普通表结构)
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE", IsNullable = true)]
    public string FTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 详细描述(第三语言)
    /// </summary>
    [SugarColumn(ColumnName = "FENAME", IsNullable = true)]
    public string FENAME { get; set; } = string.Empty;

    /// <summary>
    /// 表类型(1=基本资料，2=单据表头，3=单据表体,4=单据表体的表体,5=其他)
    /// </summary>
    [SugarColumn(ColumnName = "FTTYPE", IsNullable = true)]
    public string FTTYPE { get; set; } = string.Empty;
}
