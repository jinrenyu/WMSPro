using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产入库单明细
/// </summary>
[SugarTable("T_PRD_INSTOCKENTRY")]
public class TPrdInstockentry : BaseEntity
{
    /// <summary>
    /// 应收数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 实收数量
    /// </summary>
    [SugarColumn(ColumnName = "FREALQTY")]
    public decimal Frealqty { get; set; }

    /// <summary>
    /// 总成本
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

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
    /// Bom版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

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
    /// 辅助单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITQTY", IsNullable = true)]
    public decimal? FSECUNITQTY { get; set; }

    /// <summary>
    /// 项目号
    /// </summary>
    [SugarColumn(ColumnName = "FPROJECTNO", IsNullable = true)]
    public string FPROJECTNO { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID", IsNullable = true)]
    public string FSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// 同步ERP表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPENTRYID", IsNullable = true)]
    public string FERPENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 生产订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID", IsNullable = true)]
    public string FMOID { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID", IsNullable = true)]
    public string FWORKSHOPID { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSRCFORMID", IsNullable = true)]
    public string FSRCFORMID { get; set; } = string.Empty;

    /// <summary>
    /// 工序号
    /// </summary>
    [SugarColumn(ColumnName = "FOPERID", IsNullable = true)]
    public string FOPERID { get; set; } = string.Empty;

    /// <summary>
    /// 物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID", IsNullable = true)]
    public string FMATERIALID { get; set; } = string.Empty;

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
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID", IsNullable = true)]
    public string FSUPPLYID { get; set; } = string.Empty;

    /// <summary>
    /// 批号内码
    /// </summary>
    [SugarColumn(ColumnName = "FLOTID", IsNullable = true)]
    public string FLOTID { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单分录内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID", IsNullable = true)]
    public string FMOENTRYID { get; set; } = string.Empty;

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE", IsNullable = true)]
    public decimal? FPRICE { get; set; }

    /// <summary>
    /// 仓库库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID", IsNullable = true)]
    public string FSTOCKSTATUSID { get; set; } = string.Empty;

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
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID", IsNullable = true)]
    public string FKEEPERTYPEID { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYSEQ", IsNullable = true)]
    public int? FMOENTRYSEQ { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT", IsNullable = true)]
    public string FLOT { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO", IsNullable = true)]
    public string FMOBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 辅助单位
    /// </summary>
    [SugarColumn(ColumnName = "FSECUNITID", IsNullable = true)]
    public string FSECUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSRCBILLNO", IsNullable = true)]
    public string FSRCBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID", IsNullable = true)]
    public string FSTOCKID { get; set; } = string.Empty;
}
