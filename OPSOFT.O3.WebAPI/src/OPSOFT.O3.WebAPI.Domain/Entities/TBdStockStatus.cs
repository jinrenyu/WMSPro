using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 库存状态
/// </summary>
[SugarTable("T_BD_STOCKSTATUS")]
public class TBdStockstatus : BaseEntity
{
    /// <summary>
    /// 库存状态代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// 可使用
    /// </summary>
    [SugarColumn(ColumnName = "FAVAILABLE")]
    public bool Favailable { get; set; }

    /// <summary>
    /// 不可销售
    /// </summary>
    [SugarColumn(ColumnName = "FNOTSALE")]
    public bool Fnotsale { get; set; }

    /// <summary>
    /// 不可以领用
    /// </summary>
    [SugarColumn(ColumnName = "FNOTGET")]
    public bool Fnotget { get; set; }

    /// <summary>
    /// 可锁库
    /// </summary>
    [SugarColumn(ColumnName = "FAVAILABLELOCK")]
    public bool Favailablelock { get; set; }

    /// <summary>
    /// MRP可用
    /// </summary>
    [SugarColumn(ColumnName = "FAVAILABLEMRP")]
    public bool Favailablemrp { get; set; }

    /// <summary>
    /// 参与预警
    /// </summary>
    [SugarColumn(ColumnName = "FAVAILABLEALERT")]
    public bool Favailablealert { get; set; }

    /// <summary>
    /// 使用组织
    /// </summary>
    [SugarColumn(ColumnName = "FUSEORGID")]
    public string Fuseorgid { get; set; } = string.Empty;

    /// <summary>
    /// 系统预置
    /// </summary>
    [SugarColumn(ColumnName = "ISDEFAULT")]
    public bool Isdefault { get; set; }

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
