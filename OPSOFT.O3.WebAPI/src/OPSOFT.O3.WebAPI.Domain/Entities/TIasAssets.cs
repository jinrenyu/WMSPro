using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资产清单
/// </summary>
[SugarTable("T_IAS_ASSETS")]
public class TIasAssets : BaseEntity
{
    /// <summary>
    /// 资产编码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 资产名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 类别编号
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 计量单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 单位名称
    /// </summary>
    [SugarColumn(ColumnName = "FUNITNAME")]
    public string Funitname { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FNUM")]
    public decimal Fnum { get; set; }

    /// <summary>
    /// 入账日期
    /// </summary>
    [SugarColumn(ColumnName = "FCARDDATE")]
    public DateTime? Fcarddate { get; set; }

    /// <summary>
    /// 存放地点内码
    /// </summary>
    [SugarColumn(ColumnName = "FLOCATIONID")]
    public string Flocationid { get; set; } = string.Empty;

    /// <summary>
    /// 存放地点名称
    /// </summary>
    [SugarColumn(ColumnName = "FLOCATIONNAME")]
    public string Flocationname { get; set; } = string.Empty;

    /// <summary>
    /// 使用状态
    /// </summary>
    [SugarColumn(ColumnName = "FUSESTATUS")]
    public int Fusestatus { get; set; }

    /// <summary>
    /// 规格型号
    /// </summary>
    [SugarColumn(ColumnName = "FMODEL")]
    public string Fmodel { get; set; } = string.Empty;

    /// <summary>
    /// 产地
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTINGAREA")]
    public string Fproductingarea { get; set; } = string.Empty;

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FVENDERID")]
    public string Fvenderid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商名称
    /// </summary>
    [SugarColumn(ColumnName = "FVENDERNAME")]
    public string Fvendername { get; set; } = string.Empty;

    /// <summary>
    /// 使用部门
    /// </summary>
    [SugarColumn(ColumnName = "FUSEDEPTID")]
    public string Fusedeptid { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "FUSEDEPTNAME")]
    public string Fusedeptname { get; set; } = string.Empty;

    /// <summary>
    /// 制造商
    /// </summary>
    [SugarColumn(ColumnName = "FMANUFACTURER")]
    public string Fmanufacturer { get; set; } = string.Empty;

    /// <summary>
    /// 使用寿命
    /// </summary>
    [SugarColumn(ColumnName = "FLIFE")]
    public decimal Flife { get; set; }

    /// <summary>
    /// 剩余寿命
    /// </summary>
    [SugarColumn(ColumnName = "FLEFTLIFE")]
    public decimal Fleftlife { get; set; }

    /// <summary>
    /// 来源
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCE")]
    public int Fsource { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    [SugarColumn(ColumnName = "FPICTURE")]
    public byte[]? Fpicture { get; set; }

    /// <summary>
    /// 流转时间
    /// </summary>
    [SugarColumn(ColumnName = "FALTERDATE")]
    public DateTime? Falterdate { get; set; }

    /// <summary>
    /// 流转人
    /// </summary>
    [SugarColumn(ColumnName = "FALTERUSERNAME")]
    public string Falterusername { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }
}
