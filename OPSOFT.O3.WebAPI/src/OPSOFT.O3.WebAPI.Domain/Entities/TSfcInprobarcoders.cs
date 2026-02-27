using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// XXX
/// </summary>
[SugarTable("T_SFC_INPROBARCODERS")]
public class TSfcInprobarcoders : BaseEntity
{
    /// <summary>
    /// 生产条码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCODE")]
    public string Fprocode { get; set; } = string.Empty;

    /// <summary>
    /// 继承原始条码（多工序用同一条码汇报）
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTPROCODE")]
    public string Freportprocode { get; set; } = string.Empty;

    /// <summary>
    /// 表示同一批次条码(采集单内码
    /// </summary>
    [SugarColumn(ColumnName = "FGUID")]
    public string Fguid { get; set; } = string.Empty;

    /// <summary>
    /// 汇报单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID", IsNullable = true)]
    public string FREPORTDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 本时间段唯一ID
    /// </summary>
    [SugarColumn(ColumnName = "FCJGUID", IsNullable = true)]
    public string FCJGUID { get; set; } = string.Empty;

    /// <summary>
    /// 条码变更状态
    /// </summary>
    [SugarColumn(ColumnName = "FCHANGESTATUS", IsNullable = true)]
    public int? FCHANGESTATUS { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 是否是委外退货条码
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNSUPPLYYN", IsNullable = true)]
    public bool? FRETURNSUPPLYYN { get; set; }

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID", IsNullable = true)]
    public string FRESOURCEID { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY", IsNullable = true)]
    public decimal? FQTY { get; set; }

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID", IsNullable = true)]
    public string FSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡编号
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDNO", IsNullable = true)]
    public string FFLOWCARDNO { get; set; } = string.Empty;

    /// <summary>
    /// 产生日期(长日期)
    /// </summary>
    [SugarColumn(ColumnName = "FDATE", IsNullable = true)]
    public DateTime? FDATE { get; set; }

    /// <summary>
    /// 当前工序
    /// </summary>
    [SugarColumn(ColumnName = "FCURPROCESSID", IsNullable = true)]
    public string FCURPROCESSID { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID", IsNullable = true)]
    public string FMODETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID", IsNullable = true)]
    public string FMOID { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID", IsNullable = true)]
    public string FFLOWCARDDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "FBDQTY", IsNullable = true)]
    public decimal? FBDQTY { get; set; }

    /// <summary>
    /// 在制品条码状态
    /// </summary>
    [SugarColumn(ColumnName = "FWIPSTATUS", IsNullable = true)]
    public int? FWIPSTATUS { get; set; }

    /// <summary>
    /// 整卷号
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT", IsNullable = true)]
    public int? FCOUNT { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID", IsNullable = true)]
    public string FMATERIALID { get; set; } = string.Empty;

    /// <summary>
    /// 下一工序
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTPROCESSID", IsNullable = true)]
    public string FNEXTPROCESSID { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID", IsNullable = true)]
    public string FUNITID { get; set; } = string.Empty;

    /// <summary>
    /// 当前工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCURCODE", IsNullable = true)]
    public string FCURCODE { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID", IsNullable = true)]
    public string FSTOCKID { get; set; } = string.Empty;

    /// <summary>
    /// 质量状态
    /// </summary>
    [SugarColumn(ColumnName = "FQUALITYSTATUS", IsNullable = true)]
    public bool? FQUALITYSTATUS { get; set; }

    /// <summary>
    /// 在制品过程状态
    /// </summary>
    [SugarColumn(ColumnName = "FINSTATUS", IsNullable = true)]
    public int? FINSTATUS { get; set; }

    /// <summary>
    /// 下道工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTCODE", IsNullable = true)]
    public string FNEXTCODE { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FUSEKIND", IsNullable = true)]
    public int? FUSEKIND { get; set; }

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO", IsNullable = true)]
    public string FBATCHNO { get; set; } = string.Empty;

    /// <summary>
    /// 条码类别
    /// </summary>
    [SugarColumn(ColumnName = "FBARTYPE", IsNullable = true)]
    public int? FBARTYPE { get; set; }

    /// <summary>
    /// 分卷号
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNTSMALL", IsNullable = true)]
    public int? FCOUNTSMALL { get; set; }

    /// <summary>
    /// 工作中心
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID", IsNullable = true)]
    public string FWORKCENTERID { get; set; } = string.Empty;

    /// <summary>
    /// 质量
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE", IsNullable = true)]
    public string FTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 母卷号
    /// </summary>
    [SugarColumn(ColumnName = "FPCOUNT", IsNullable = true)]
    public int? FPCOUNT { get; set; }

    /// <summary>
    /// 生产任务单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO", IsNullable = true)]
    public string FMOBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 不良汇报单内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREPORTDETAILID", IsNullable = true)]
    public string FBADREPORTDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 是否被消耗
    /// </summary>
    [SugarColumn(ColumnName = "FISDEPLETE", IsNullable = true)]
    public bool? FISDEPLETE { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 是否被检验
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECK", IsNullable = true)]
    public bool? FISCHECK { get; set; }

    /// <summary>
    /// 操作员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID", IsNullable = true)]
    public string FEMPID { get; set; } = string.Empty;
}
