using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序汇报表体ENTRY的表体-工序投料信息
/// </summary>
[SugarTable("T_SFC_REPORTENTRY3")]
public class TSfcReportentry3 : BaseEntity
{
    /// <summary>
    /// 项次(因为批量条码时, 一个条码会投入多次
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public int Fseq { get; set; }

    /// <summary>
    /// 物料辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID", IsNullable = true)]
    public string FAUXPROPID { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY", IsNullable = true)]
    public decimal? FQTY { get; set; }

    /// <summary>
    /// 调出仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID", IsNullable = true)]
    public string FSTOCKLOCID { get; set; } = string.Empty;

    /// <summary>
    /// 是否产生领料单
    /// </summary>
    [SugarColumn(ColumnName = "FAUTOYN", IsNullable = true)]
    public string FAUTOYN { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID", IsNullable = true)]
    public int? FENTRYID { get; set; }

    /// <summary>
    /// 累计消耗数量
    /// </summary>
    [SugarColumn(ColumnName = "FOUTQTY", IsNullable = true)]
    public decimal? FOUTQTY { get; set; }

    /// <summary>
    /// 物料条码
    /// </summary>
    [SugarColumn(ColumnName = "FMBARCODE", IsNullable = true)]
    public string FMBARCODE { get; set; } = string.Empty;

    /// <summary>
    /// 投入的条码类别
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODETYPE", IsNullable = true)]
    public int? FBARCODETYPE { get; set; }

    /// <summary>
    /// 是否虚仓
    /// </summary>
    [SugarColumn(ColumnName = "FISVIRTUAL", IsNullable = true)]
    public bool? FISVIRTUAL { get; set; }

    /// <summary>
    /// T_SFC_REPORT工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID", IsNullable = true)]
    public string FFLOWCARDDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCESID", IsNullable = true)]
    public string FRESOURCESID { get; set; } = string.Empty;

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
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID", IsNullable = true)]
    public string FDETAILID { get; set; } = string.Empty;

    /// <summary>
    /// 余料数量
    /// </summary>
    [SugarColumn(ColumnName = "FMARGINQTY", IsNullable = true)]
    public decimal? FMARGINQTY { get; set; }

    /// <summary>
    /// 在制品条码
    /// </summary>
    [SugarColumn(ColumnName = "FWIPNUMBER", IsNullable = true)]
    public string FWIPNUMBER { get; set; } = string.Empty;

    /// <summary>
    /// T_SFC_REPORT流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID", IsNullable = true)]
    public string FFLOWCARDID { get; set; } = string.Empty;

    /// <summary>
    /// 物料批次
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO", IsNullable = true)]
    public string FBATCHNO { get; set; } = string.Empty;

    /// <summary>
    /// T_SFC_REPORT工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID", IsNullable = true)]
    public string FPROCESSID { get; set; } = string.Empty;

    /// <summary>
    /// 操作时间
    /// </summary>
    [SugarColumn(ColumnName = "FOPTIME", IsNullable = true)]
    public DateTime? FOPTIME { get; set; }

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID", IsNullable = true)]
    public string FWORKCENTERID { get; set; } = string.Empty;

    /// <summary>
    /// 调出仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID", IsNullable = true)]
    public string FSTOCKID { get; set; } = string.Empty;

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID", IsNullable = true)]
    public string FBODYID { get; set; } = string.Empty;

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID", IsNullable = true)]
    public string FEMPID { get; set; } = string.Empty;
}
