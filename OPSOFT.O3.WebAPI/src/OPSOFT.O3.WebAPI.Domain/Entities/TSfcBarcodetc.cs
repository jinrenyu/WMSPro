using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 周转码表
/// </summary>
[SugarTable("T_SFC_BARCODETC")]
public class TSfcBarcodetc : BaseEntity
{
    /// <summary>
    /// 周转码标签
    /// </summary>
    [SugarColumn(ColumnName = "FTCCODE")]
    public string Ftccode { get; set; } = string.Empty;

    /// <summary>
    /// 条码标签
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 原单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETRANTYPE")]
    public int Fsourcetrantype { get; set; }

    /// <summary>
    /// 原单单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEINTERID")]
    public string Fsourceinterid { get; set; } = string.Empty;

    /// <summary>
    /// 原单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEDETAILID")]
    public string Fsourcedetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工单号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 当前工序
    /// </summary>
    [SugarColumn(ColumnName = "FCURPROCESSID")]
    public string Fcurprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 下一工序
    /// </summary>
    [SugarColumn(ColumnName = "FNEXTPROCESSID")]
    public string Fnextprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 累计汇报量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTQTY")]
    public decimal Ftotqty { get; set; }

    /// <summary>
    /// 累计合格量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTQUAQTY")]
    public decimal Ftotquaqty { get; set; }

    /// <summary>
    /// 累计不良量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTBADQTY")]
    public decimal Ftotbadqty { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 是否汇报
    /// </summary>
    [SugarColumn(ColumnName = "FISREPORTED")]
    public bool Fisreported { get; set; }

    /// <summary>
    /// 过程状态
    /// </summary>
    [SugarColumn(ColumnName = "FTCSTATUS")]
    public int Ftcstatus { get; set; }

    /// <summary>
    /// 质量状态
    /// </summary>
    [SugarColumn(ColumnName = "FQUALITYSTATUS")]
    public bool Fqualitystatus { get; set; }

    /// <summary>
    /// 汇报单内码
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTDETAILID")]
    public string Freportdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 不良汇报单内码
    /// </summary>
    [SugarColumn(ColumnName = "FBADREPORTDETAILID")]
    public string Fbadreportdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 是否被消耗
    /// </summary>
    [SugarColumn(ColumnName = "FISDEPLETE")]
    public bool Fisdeplete { get; set; }

    /// <summary>
    /// 是否被检验
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECK")]
    public bool Fischeck { get; set; }

    /// <summary>
    /// 同一时间段唯一内码
    /// </summary>
    [SugarColumn(ColumnName = "FCJGUID")]
    public string Fcjguid { get; set; } = string.Empty;

    /// <summary>
    /// 在制品条码状态
    /// </summary>
    [SugarColumn(ColumnName = "FWIPSTATUS")]
    public int Fwipstatus { get; set; }

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

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
}
