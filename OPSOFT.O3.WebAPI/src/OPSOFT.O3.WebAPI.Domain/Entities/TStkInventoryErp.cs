using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 即时库存_ERP
/// </summary>
[SugarTable("T_STK_INVENTORY_ERP")]
public class TStkInventoryErp : BaseEntity
{
    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

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
    /// 库存组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKORGID")]
    public string Fstockorgid { get; set; } = string.Empty;

    /// <summary>
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID")]
    public string Fkeepertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 保管者内码
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
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 库存主单位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKUNITID")]
    public string Fstockunitid { get; set; } = string.Empty;

    /// <summary>
    /// 库存主单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 库存余额
    /// </summary>
    [SugarColumn(ColumnName = "FBAL")]
    public decimal Fbal { get; set; }

    /// <summary>
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 有效期至(变更后
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }
}
