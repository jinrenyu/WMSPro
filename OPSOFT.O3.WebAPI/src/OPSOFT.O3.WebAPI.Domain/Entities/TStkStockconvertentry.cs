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

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID", IsNullable = true)]
    public string FAUXPROPID { get; set; } = string.Empty;

    /// <summary>
    /// 入库日期
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSDATE", IsNullable = true)]
    public DateTime? FBUSINESSDATE { get; set; }

    /// <summary>
    /// 发货检验出库数量
    /// </summary>
    [SugarColumn(ColumnName = "FOUTSTOCKQTY", IsNullable = true)]
    public decimal? FOUTSTOCKQTY { get; set; }

    /// <summary>
    /// 转换数量（辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FEXTAUXUNITQTY", IsNullable = true)]
    public decimal? FEXTAUXUNITQTY { get; set; }

    /// <summary>
    /// 辅助单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITQTY", IsNullable = true)]
    public decimal? FSECUNITQTY { get; set; }

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID", IsNullable = true)]
    public string FSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// ERP表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPENTRYID", IsNullable = true)]
    public string FERPENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 生产日期/采购日期/保质期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE", IsNullable = true)]
    public DateTime? FKFDATE { get; set; }

    /// <summary>
    /// 总成本
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT", IsNullable = true)]
    public decimal? FAMOUNT { get; set; }

    /// <summary>
    /// 库存更新标志
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKFLAG", IsNullable = true)]
    public bool? FSTOCKFLAG { get; set; }

    /// <summary>
    /// 基本转换数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEQTY", IsNullable = true)]
    public decimal? FBASEQTY { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID", IsNullable = true)]
    public string FUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID", IsNullable = true)]
    public string FBASEUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE", IsNullable = true)]
    public DateTime? FUSEFULDATE { get; set; }

    /// <summary>
    /// 成本价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE", IsNullable = true)]
    public decimal? FPRICE { get; set; }

    /// <summary>
    /// 序列号单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSNQTY", IsNullable = true)]
    public decimal? FSNQTY { get; set; }

    /// <summary>
    /// 序列号单位
    /// </summary>
    [SugarColumn(ColumnName = "FSNUNITID", IsNullable = true)]
    public string FSNUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT", IsNullable = true)]
    public string FLOT { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITID", IsNullable = true)]
    public string FSECUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 对应转换前分录ID
    /// </summary>
    [SugarColumn(ColumnName = "FCONVERTENTRYID", IsNullable = true)]
    public string FCONVERTENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 辅单位
    /// </summary>
    [SugarColumn(ColumnName = "FEXTAUXUNITID", IsNullable = true)]
    public string FEXTAUXUNITID { get; set; } = string.Empty;
}
