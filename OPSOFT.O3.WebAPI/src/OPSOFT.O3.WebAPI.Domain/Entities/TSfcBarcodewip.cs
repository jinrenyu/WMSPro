using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 在制品条码表
/// </summary>
[SugarTable("T_SFC_BARCODEWIP")]
public class TSfcBarcodewip : BaseEntity
{
    /// <summary>
    /// 在制品条码状态
    /// </summary>
    [SugarColumn(ColumnName = "FBARSTATUS")]
    public int Fbarstatus { get; set; }

    /// <summary>
    /// 在制品序列号
    /// </summary>
    [SugarColumn(ColumnName = "FWIPCODE")]
    public string Fwipcode { get; set; } = string.Empty;

    /// <summary>
    /// 采集汇报单编号
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTNO")]
    public string Freportno { get; set; } = string.Empty;

    /// <summary>
    /// 产生日期(长日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 汇报单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID", IsNullable = true)]
    public string FREPORTDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 同一时间段唯一内码
    /// </summary>
    [SugarColumn(ColumnName = "FCJGUID", IsNullable = true)]
    public string FCJGUID { get; set; } = string.Empty;

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY", IsNullable = true)]
    public decimal? FBADQTY { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

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
    /// 质量
    /// </summary>
    [SugarColumn(ColumnName = "FQSTYPE", IsNullable = true)]
    public string FQSTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID", IsNullable = true)]
    public string FSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID", IsNullable = true)]
    public string FMODETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID", IsNullable = true)]
    public string FMOID { get; set; } = string.Empty;

    /// <summary>
    /// 是否汇报
    /// </summary>
    [SugarColumn(ColumnName = "FISREPORTED", IsNullable = true)]
    public bool? FISREPORTED { get; set; }

    /// <summary>
    /// 工序流转卡工序明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID", IsNullable = true)]
    public string FFLOWCARDDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 在制品过程状态
    /// </summary>
    [SugarColumn(ColumnName = "FWIPSTATUS", IsNullable = true)]
    public int? FWIPSTATUS { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID", IsNullable = true)]
    public string FMATERIALID { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE", IsNullable = true)]
    public string FCODE { get; set; } = string.Empty;

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
    /// 质量状态
    /// </summary>
    [SugarColumn(ColumnName = "FQUALITYSTATUS", IsNullable = true)]
    public bool? FQUALITYSTATUS { get; set; }

    /// <summary>
    /// 生产任务明细行号
    /// </summary>
    [SugarColumn(ColumnName = "FMOENTRYID", IsNullable = true)]
    public int? FMOENTRYID { get; set; }

    /// <summary>
    /// 工序流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID", IsNullable = true)]
    public string FFLOWCARDID { get; set; } = string.Empty;

    /// <summary>
    /// 下道工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTCODE", IsNullable = true)]
    public string FNEXTCODE { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO", IsNullable = true)]
    public string FBATCHNO { get; set; } = string.Empty;

    /// <summary>
    /// 当前工序
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID", IsNullable = true)]
    public string FPROCESSID { get; set; } = string.Empty;

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FBARTYPE", IsNullable = true)]
    public string FBARTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 不良汇报单内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREPORTDETAILID", IsNullable = true)]
    public string FBADREPORTDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO", IsNullable = true)]
    public string FMOBILLNO { get; set; } = string.Empty;

    /// <summary>
    /// 是否被消耗
    /// </summary>
    [SugarColumn(ColumnName = "FISDEPLETE", IsNullable = true)]
    public bool? FISDEPLETE { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID", IsNullable = true)]
    public string FSTOCKID { get; set; } = string.Empty;

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
