using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 维修汇报-送修单表体-每次投料信息
/// </summary>
[SugarTable("T_SFC_REPAIRENTRY2")]
public class TSfcRepairentry2 : BaseEntity
{
    /// <summary>
    /// 时间段内码
    /// </summary>
    [SugarColumn(ColumnName = "FTIMEID")]
    public string Ftimeid { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKCENTERID")]
    public string Fworkcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 项次
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public int Fseq { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    [SugarColumn(ColumnName = "FSDTIME")]
    public DateTime? Fsdtime { get; set; }

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 是否产生领料单
    /// </summary>
    [SugarColumn(ColumnName = "FAUTOYN")]
    public string Fautoyn { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 移转条码最原始条码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTNUMBER")]
    public string Freportnumber { get; set; } = string.Empty;

    /// <summary>
    /// 物料条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 在制品条码
    /// </summary>
    [SugarColumn(ColumnName = "FWIPNUMBER")]
    public string Fwipnumber { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 累计消耗数量
    /// </summary>
    [SugarColumn(ColumnName = "FOUTQTY")]
    public decimal Foutqty { get; set; }

    /// <summary>
    /// 条码类别
    /// </summary>
    [SugarColumn(ColumnName = "FBARTYPE")]
    public int Fbartype { get; set; }

    /// <summary>
    /// 父阶条码
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTBARCODE")]
    public string Fparentbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 顶阶条码
    /// </summary>
    [SugarColumn(ColumnName = "FHBARCODE")]
    public string Fhbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
