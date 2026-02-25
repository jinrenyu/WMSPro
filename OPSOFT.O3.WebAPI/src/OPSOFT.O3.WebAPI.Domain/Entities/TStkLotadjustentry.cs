using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 批号调整单-明细信息
/// </summary>
[SugarTable("T_STK_LOTADJUSTENTRY")]
public class TStkLotadjustentry : BaseEntity
{
    /// <summary>
    /// 转换分组
    /// </summary>
    [SugarColumn(ColumnName = "FGROUP")]
    public int Fgroup { get; set; }

    /// <summary>
    /// 转换类型
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTTYPE")]
    public int Fconverttype { get; set; }

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 转换数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

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
    /// 成本价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 总成本
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

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
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFPERIOD")]
    public int Fkfperiod { get; set; }

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
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 库存更新标志
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKFLAG")]
    public bool Fstockflag { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 对应转换前分录ID
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTENTRYID")]
    public string Fconvertentryid { get; set; } = string.Empty;

    /// <summary>
    /// 入库日期
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSDATE")]
    public DateTime? Fbusinessdate { get; set; }

    /// <summary>
    /// 供应商批号
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLIERLOT")]
    public string Fsupplierlot { get; set; } = string.Empty;

    /// <summary>
    /// ERP表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPENTRYID")]
    public string Ferpentryid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITID")]
    public string Fsecunitid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITQTY")]
    public decimal Fsecunitqty { get; set; }

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE")]
    public DateTime? Fusefuldate { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
