using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 简单生产退库单-明细
/// </summary>
[SugarTable("T_SP_OUTSTOCKENTRY")]
public class TSpOutstockentry : BaseEntity
{
    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 应退数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 基本单位应退数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEMUSTQTY")]
    public decimal Fbasemustqty { get; set; }

    /// <summary>
    /// 实退数量
    /// </summary>
    [SugarColumn(ColumnName = "FREALQTY")]
    public decimal Frealqty { get; set; }

    /// <summary>
    /// 基本单位实退数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEREALQTY")]
    public decimal Fbaserealqty { get; set; }

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
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 批号内码
    /// </summary>
    [SugarColumn(ColumnName = "FLOTID")]
    public string Flotid { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 库存单位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKUNITID")]
    public string Fstockunitid { get; set; } = string.Empty;

    /// <summary>
    /// 库存单位实退数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKREALQTY")]
    public decimal Fstockrealqty { get; set; }

    /// <summary>
    /// 实退数量(库存辅单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECREALQTY")]
    public decimal Fsecrealqty { get; set; }

    /// <summary>
    /// 保管者内码
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERID", IsNullable = true)]
    public string FKEEPERID { get; set; } = string.Empty;

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID", IsNullable = true)]
    public string FOWNERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 实收数量（辅单位）
    /// </summary>
    [SugarColumn(ColumnName = "FEXTAUXUNITQTY", IsNullable = true)]
    public decimal? FEXTAUXUNITQTY { get; set; }

    /// <summary>
    /// 生产日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCEDATE", IsNullable = true)]
    public DateTime? FPRODUCEDATE { get; set; }

    /// <summary>
    /// 辅助单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITQTY", IsNullable = true)]
    public decimal? FSECUNITQTY { get; set; }

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
    /// 退库类型
    /// </summary>
    [SugarColumn(ColumnName = "FOUTSTOCKTYPE", IsNullable = true)]
    public string FOUTSTOCKTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID", IsNullable = true)]
    public string FSRCFORMID { get; set; } = string.Empty;

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
    /// 源单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCID", IsNullable = true)]
    public string FSRCID { get; set; } = string.Empty;

    /// <summary>
    /// 源单行号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCENTRYID", IsNullable = true)]
    public int? FSRCENTRYID { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 有效期至(变更后)
    /// </summary>
    [SugarColumn(ColumnName = "FUSEFULDATE", IsNullable = true)]
    public DateTime? FUSEFULDATE { get; set; }

    /// <summary>
    /// 班组
    /// </summary>
    [SugarColumn(ColumnName = "FSHIFTGROUPID", IsNullable = true)]
    public string FSHIFTGROUPID { get; set; } = string.Empty;

    /// <summary>
    /// 成本价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE", IsNullable = true)]
    public decimal? FPRICE { get; set; }

    /// <summary>
    /// 仓库库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID", IsNullable = true)]
    public string FSTOCKSTATUSID { get; set; } = string.Empty;

    /// <summary>
    /// 序列号单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSNQTY", IsNullable = true)]
    public string FSNQTY { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID", IsNullable = true)]
    public string FOWNERID { get; set; } = string.Empty;

    /// <summary>
    /// 源单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDETAILID", IsNullable = true)]
    public string FSRCDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 序列号单位
    /// </summary>
    [SugarColumn(ColumnName = "FSNUNITID", IsNullable = true)]
    public string FSNUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 生产编号
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTNO", IsNullable = true)]
    public string FPRODUCTNO { get; set; } = string.Empty;

    /// <summary>
    /// 生产编号影响成本
    /// </summary>
    [SugarColumn(ColumnName = "FISAFFECTCOST", IsNullable = true)]
    public bool? FISAFFECTCOST { get; set; }

    /// <summary>
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID", IsNullable = true)]
    public string FKEEPERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITID", IsNullable = true)]
    public string FSECUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO", IsNullable = true)]
    public string FSRCBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 辅单位
    /// </summary>
    [SugarColumn(ColumnName = "FEXTAUXUNITID", IsNullable = true)]
    public string FEXTAUXUNITID { get; set; } = string.Empty;
}
