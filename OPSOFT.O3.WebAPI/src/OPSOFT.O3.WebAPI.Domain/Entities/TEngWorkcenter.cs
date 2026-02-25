using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工作中心
/// </summary>
[SugarTable("T_ENG_WORKCENTER")]
public class TEngWorkcenter : BaseEntity
{
    /// <summary>
    /// 工作中心代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 开工时自动带出上岗人员
    /// </summary>
    [SugarColumn(ColumnName = "FAUTOPERSON")]
    public bool Fautoperson { get; set; }

    /// <summary>
    /// 标准生产率
    /// </summary>
    [SugarColumn(ColumnName = "FSTANDPRO")]
    public decimal Fstandpro { get; set; }

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 类别码
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 默认仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSCSTOCKID")]
    public string Fscstockid { get; set; } = string.Empty;

    /// <summary>
    /// 默认仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSCSPID")]
    public string Fscspid { get; set; } = string.Empty;

    /// <summary>
    /// 关键工作中心
    /// </summary>
    [SugarColumn(ColumnName = "FKEYWORK")]
    public string Fkeywork { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心是否共享
    /// </summary>
    [SugarColumn(ColumnName = "FISSHARE")]
    public bool Fisshare { get; set; }

    /// <summary>
    /// 设备组状态
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPSTATUS")]
    public bool Fequipstatus { get; set; }

    /// <summary>
    /// 工作台汇报唯一ID
    /// </summary>
    [SugarColumn(ColumnName = "FCOMPUTERID")]
    public string Fcomputerid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }
}
