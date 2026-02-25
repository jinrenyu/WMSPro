using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资源
/// </summary>
[SugarTable("T_ENG_RESOURCE")]
public class TEngResource : BaseEntity
{
    /// <summary>
    /// 资源代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 资源名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

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
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 是否关键资源
    /// </summary>
    [SugarColumn(ColumnName = "FISKEY")]
    public bool Fiskey { get; set; }

    /// <summary>
    /// 资源是否共享
    /// </summary>
    [SugarColumn(ColumnName = "FISSHARE")]
    public bool Fisshare { get; set; }

    /// <summary>
    /// 工序
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 开工自动带出上岗员工
    /// </summary>
    [SugarColumn(ColumnName = "FAUTOPERSON")]
    public bool Fautoperson { get; set; }

    /// <summary>
    /// 是否维修站
    /// </summary>
    [SugarColumn(ColumnName = "FISREPAIR")]
    public bool Fisrepair { get; set; }

    /// <summary>
    /// OPC服务器IP
    /// </summary>
    [SugarColumn(ColumnName = "FOPCIP")]
    public string Fopcip { get; set; } = string.Empty;

    /// <summary>
    /// OPC服务器名称
    /// </summary>
    [SugarColumn(ColumnName = "FOPCNAME")]
    public string Fopcname { get; set; } = string.Empty;

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

    /// <summary>
    /// 跨过休息日
    /// </summary>
    [SugarColumn(ColumnName = "FTHINKBREAKINGDAY")]
    public bool Fthinkbreakingday { get; set; }
}
