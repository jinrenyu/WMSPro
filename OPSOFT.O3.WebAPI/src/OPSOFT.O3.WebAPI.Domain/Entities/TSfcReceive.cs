using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 记录每一个资源接收信息
/// </summary>
[SugarTable("T_SFC_RECEIVE")]
public class TSfcReceive : BaseEntity
{
    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

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
    /// 操作时间
    /// </summary>
    [SugarColumn(ColumnName = "FSDTIME")]
    public DateTime? Fsdtime { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// ODK_Report流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// ODK_Report工序流转卡行内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// ODK_Report工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工序识别码 流转卡编号+工序代码+相同工序流水号
    /// </summary>
    [SugarColumn(ColumnName = "FCODE")]
    public string Fcode { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
    /// 扫入条码
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
    [SugarColumn(ColumnName = "FWIPBARCODE")]
    public string Fwipbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 数量 -- 米数
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

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
}
