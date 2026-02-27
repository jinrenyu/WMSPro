using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 员工任岗信息
/// </summary>
[SugarTable("T_BD_STAFF")]
public class TBdStaff : BaseEntity
{
    /// <summary>
    /// 员工内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPLOYEEID")]
    public string FEmployeeId { get; set; } = string.Empty;

    /// <summary>
    /// 岗位内码
    /// </summary>
    [SugarColumn(ColumnName = "FPOSTID")]
    public string FPostId { get; set; } = string.Empty;

    /// <summary>
    /// 任职日期
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTDATE")]
    public DateTime? FStartDate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 就任岗位编码
    /// </summary>
    [SugarColumn(ColumnName = "FPOSTNUMBER", IsNullable = true)]
    public string FPOSTNUMBER { get; set; } = string.Empty;

    /// <summary>
    /// 是否主岗位
    /// </summary>
    [SugarColumn(ColumnName = "FISFIRSTPOST", IsNullable = true)]
    public bool? FISFIRSTPOST { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID", IsNullable = true)]
    public string FCHECKERID { get; set; } = string.Empty;

    /// <summary>
    /// 员工
    /// </summary>
    [SugarColumn(ColumnName = "FEMPINFOID", IsNullable = true)]
    public string FEMPINFOID { get; set; } = string.Empty;

    /// <summary>
    /// 使用组织
    /// </summary>
    [SugarColumn(ColumnName = "FUSEORGID", IsNullable = true)]
    public string FUSEORGID { get; set; } = string.Empty;

    /// <summary>
    /// 创建组织
    /// </summary>
    [SugarColumn(ColumnName = "FCREATEORGID", IsNullable = true)]
    public string FCREATEORGID { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID", IsNullable = true)]
    public string FDISABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 所属部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID", IsNullable = true)]
    public string FDEPTID { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE", IsNullable = true)]
    public DateTime? FCHECKDATE { get; set; }

    /// <summary>
    /// 员工编码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPINFONUMBER", IsNullable = true)]
    public string FEMPINFONUMBER { get; set; } = string.Empty;

    /// <summary>
    /// 员工任岗编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER", IsNullable = true)]
    public string FNUMBER { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE", IsNullable = true)]
    public DateTime? FDISABLEDATE { get; set; }

    /// <summary>
    /// 业务员类型
    /// </summary>
    [SugarColumn(ColumnName = "FOPERATORTYPE", IsNullable = true)]
    public string FOPERATORTYPE { get; set; } = string.Empty;
}
