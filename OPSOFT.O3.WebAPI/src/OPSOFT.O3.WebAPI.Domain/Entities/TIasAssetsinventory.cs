using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资产盘点方案表
/// </summary>
[SugarTable("T_IAS_ASSETSINVENTORY")]
public class TIasAssetsinventory : BaseEntity
{
    /// <summary>
    /// 盘点方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 起始资产类别代码
    /// </summary>
    [SugarColumn(ColumnName = "FBEGGROUPNAME")]
    public string Fbeggroupname { get; set; } = string.Empty;

    /// <summary>
    /// 截止资产类别代码
    /// </summary>
    [SugarColumn(ColumnName = "FENDGROUPNAME")]
    public string Fendgroupname { get; set; } = string.Empty;

    /// <summary>
    /// 起始资产编码
    /// </summary>
    [SugarColumn(ColumnName = "FBEGASSETNUMBER")]
    public string Fbegassetnumber { get; set; } = string.Empty;

    /// <summary>
    /// 截止资产编码
    /// </summary>
    [SugarColumn(ColumnName = "FENDASSETNUMBER")]
    public string Fendassetnumber { get; set; } = string.Empty;

    /// <summary>
    /// 起始存放地点 --部门
    /// </summary>
    [SugarColumn(ColumnName = "FBEGLOCATIONNAME")]
    public string Fbeglocationname { get; set; } = string.Empty;

    /// <summary>
    /// 截止存放地点 --部门
    /// </summary>
    [SugarColumn(ColumnName = "FENDLOCATIONNAME")]
    public string Fendlocationname { get; set; } = string.Empty;

    /// <summary>
    /// 起始使用部门 --部门
    /// </summary>
    [SugarColumn(ColumnName = "FBEGUSEDEPTNAME")]
    public string Fbegusedeptname { get; set; } = string.Empty;

    /// <summary>
    /// 截止使用部门 --部门
    /// </summary>
    [SugarColumn(ColumnName = "FENDUSEDEPTNAME")]
    public string Fendusedeptname { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
