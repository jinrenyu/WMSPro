using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 领料申请单明细
/// </summary>
[SugarTable("T_DEV_FLTZENTRY")]
public class TDevFltzentry : BaseEntity
{
    /// <summary>
    /// 项次
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACEGROUP")]
    public string Freplacegroup { get; set; } = string.Empty;

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
    /// 应发数量
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTQTY")]
    public decimal Fmustqty { get; set; }

    /// <summary>
    /// 发料数量
    /// </summary>
    [SugarColumn(ColumnName = "FISSUEQTY")]
    public decimal Fissueqty { get; set; }

    /// <summary>
    /// 基本单位发料数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEISSUEQTY")]
    public decimal Fbaseissueqty { get; set; }

    /// <summary>
    /// 基本单位应发数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEMUSTQTY")]
    public decimal Fbasemustqty { get; set; }

    /// <summary>
    /// 发料组织
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYORG")]
    public string Fsupplyorg { get; set; } = string.Empty;

    /// <summary>
    /// 计划跟踪号
    /// </summary>
    [SugarColumn(ColumnName = "FMTONO")]
    public string Fmtono { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 原单明细ID
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEENTRYID")]
    public string Fsourceentryid { get; set; } = string.Empty;

    /// <summary>
    /// 原行号
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCESEQ")]
    public int Fsourceseq { get; set; }

    /// <summary>
    /// 仓库库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 父级行主键
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTROWID")]
    public string Fparentrowid { get; set; } = string.Empty;

    /// <summary>
    /// 行展开类型
    /// </summary>
    [SugarColumn(ColumnName = "FROWEXPANDTYPE")]
    public int Frowexpandtype { get; set; }

    /// <summary>
    /// 行标识
    /// </summary>
    [SugarColumn(ColumnName = "FROWID")]
    public string Frowid { get; set; } = string.Empty;

    /// <summary>
    /// 项目编号
    /// </summary>
    [SugarColumn(ColumnName = "FPROJECTNO")]
    public string Fprojectno { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 原单ID
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEID")]
    public string Fsourceid { get; set; } = string.Empty;

    /// <summary>
    /// 子项类型
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALTYPE")]
    public string Fmaterialtype { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FSUBBOMID")]
    public string Fsubbomid { get; set; } = string.Empty;

    /// <summary>
    /// 预留类型
    /// </summary>
    [SugarColumn(ColumnName = "FRESERVETYPE")]
    public string Freservetype { get; set; } = string.Empty;

    /// <summary>
    /// 预留ID
    /// </summary>
    [SugarColumn(ColumnName = "FYLID")]
    public string Fylid { get; set; } = string.Empty;

    /// <summary>
    /// 预留明细ID
    /// </summary>
    [SugarColumn(ColumnName = "FYLENTRYID")]
    public string Fylentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID")]
    public string Fmoentryid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYSEQ")]
    public int Fmoentryseq { get; set; }

    /// <summary>
    /// 生产订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单批号
    /// </summary>
    [SugarColumn(ColumnName = "FMOLOTID")]
    public string Fmolotid { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 源单号码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBILLNO")]
    public string Fsourcebillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产订单状态
    /// </summary>
    [SugarColumn(ColumnName = "FMOSTATUS")]
    public string Fmostatus { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单ID
    /// </summary>
    [SugarColumn(ColumnName = "FSOID")]
    public string Fsoid { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单号码
    /// </summary>
    [SugarColumn(ColumnName = "FSONO")]
    public string Fsono { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单明细ID
    /// </summary>
    [SugarColumn(ColumnName = "FSOENTRYID")]
    public string Fsoentryid { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单明细行号
    /// </summary>
    [SugarColumn(ColumnName = "FSOENTRYSEQ")]
    public int Fsoentryseq { get; set; }

    /// <summary>
    /// 特殊批次
    /// </summary>
    [SugarColumn(ColumnName = "FISTSPC")]
    public bool Fistspc { get; set; }

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
