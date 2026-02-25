using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 库存状态转换-明细信息
/// </summary>
[SugarTable("T_STK_STOCKCONVERTENTRY")]
public class TStkStockconvertentry : BaseEntity
{
    /// <summary>
    /// 转换类型
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTTYPE")]
    public int Fconverttype { get; set; }

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 转换数量
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTQTY")]
    public decimal Fconvertqty { get; set; }

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCEDATE")]
    public DateTime? Fproducedate { get; set; }

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FEXPIRYDATE")]
    public DateTime? Fexpirydate { get; set; }

    /// <summary>
    /// 在架寿命期
    /// </summary>
    [SugarColumn(ColumnName = "FSHELFLIFE")]
    public decimal Fshelflife { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 项目编号
    /// </summary>
    [SugarColumn(ColumnName = "FPROJECTNO")]
    public string Fprojectno { get; set; } = string.Empty;

    /// <summary>
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID")]
    public string Fkeepertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 保管者
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERID")]
    public string Fkeeperid { get; set; } = string.Empty;

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 转换数量(库存辅单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECQTY")]
    public decimal Fsecqty { get; set; }
}
