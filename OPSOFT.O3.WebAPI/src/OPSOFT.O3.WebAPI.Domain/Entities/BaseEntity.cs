using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 实体基类 - 包含所有表的公共字段
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(ColumnName = "UID", IsPrimaryKey = true)]
    public string Uid { get; set; } = string.Empty;

    /// <summary>
    /// 表内码
    /// </summary>
    [SugarColumn(ColumnName = "FINTERID")]
    public string FInterId { get; set; } = string.Empty;

    /// <summary>
    /// 分组内码
    /// </summary>
    [SugarColumn(ColumnName = "FGROUPID")]
    public string FGroupId { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTATUS")]
    public int FStatus { get; set; }

    /// <summary>
    /// 软删除标志
    /// </summary>
    [SugarColumn(ColumnName = "FDELETED")]
    public bool FDeleted { get; set; }

    /// <summary>
    /// 禁用状态 0=正常 1=禁用
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLED")]
    public bool FDisabled { get; set; }

    /// <summary>
    /// 组织代码
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPANYID")]
    public string FCompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 资料创建日期
    /// </summary>
    [SugarColumn(ColumnName = "C_YMD")]
    public DateTime? CYmd { get; set; }

    /// <summary>
    /// 资料创建帐号
    /// </summary>
    [SugarColumn(ColumnName = "C_USER")]
    public string CUser { get; set; } = string.Empty;

    /// <summary>
    /// 最后异动日期
    /// </summary>
    [SugarColumn(ColumnName = "M_YMD")]
    public DateTime? MYmd { get; set; }

    /// <summary>
    /// 最后异动帐号
    /// </summary>
    [SugarColumn(ColumnName = "M_USER")]
    public string MUser { get; set; } = string.Empty;
}
