using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 不良品处理表体-不良处理列表
/// </summary>
[SugarTable("T_SFC_REJECTSETTLEENTRY1")]
public class TSfcRejectsettleentry1 : BaseEntity
{
    /// <summary>
    /// 处理方案
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 汇报表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡工序表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 处理数量
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLECOUNT")]
    public decimal Fsettlecount { get; set; }

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 条码号
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 不良原因内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREASONID")]
    public string Fbadreasonid { get; set; } = string.Empty;

    /// <summary>
    /// 历史不良原因
    /// </summary>
    [SugarColumn(ColumnName = "FHISBADREASONID")]
    public string Fhisbadreasonid { get; set; } = string.Empty;

    /// <summary>
    /// 不良所属工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPREPROCESSID")]
    public string Fpreprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 改判结果
    /// </summary>
    [SugarColumn(ColumnName = "FMODRESULT")]
    public int Fmodresult { get; set; }

    /// <summary>
    /// 维修单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRINTERID")]
    public string Frepairinterid { get; set; } = string.Empty;

    /// <summary>
    /// 维修单单号
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRBILLNO")]
    public string Frepairbillno { get; set; } = string.Empty;

    /// <summary>
    /// 同一时间维修方案
    /// </summary>
    [SugarColumn(ColumnName = "FTIMEID")]
    public string Ftimeid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 改判类型
    /// </summary>
    [SugarColumn(ColumnName = "FGPTYPE")]
    public int Fgptype { get; set; }

    /// <summary>
    /// 放行处理结果
    /// </summary>
    [SugarColumn(ColumnName = "FFXRESULT")]
    public bool Ffxresult { get; set; }

    /// <summary>
    /// 放行工序
    /// </summary>
    [SugarColumn(ColumnName = "FFXPROCESSID")]
    public string Ffxprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 归零情况
    /// </summary>
    [SugarColumn(ColumnName = "FZEROSITUATION")]
    public string Fzerosituation { get; set; } = string.Empty;

    /// <summary>
    /// 类型（1：返修；2：返工）
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNTYPE")]
    public int Freturntype { get; set; }

    /// <summary>
    /// 临时ID
    /// </summary>
    [SugarColumn(ColumnName = "FTEMPID")]
    public string Ftempid { get; set; } = string.Empty;

    /// <summary>
    /// 任务单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
