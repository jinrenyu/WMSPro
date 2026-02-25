using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 检验方案
/// </summary>
[SugarTable("T_QM_INSPECTSOLUTION")]
public class TQmInspectsolution : BaseEntity
{
    /// <summary>
    /// 检验方案代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 检验方案名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 方案类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 抽样方案
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEPLAN")]
    public string Fsampleplan { get; set; } = string.Empty;

    /// <summary>
    /// 检验水平
    /// </summary>
    [SugarColumn(ColumnName = "FTESTLEVEL")]
    public int Ftestlevel { get; set; }

    /// <summary>
    /// 严格度
    /// </summary>
    [SugarColumn(ColumnName = "FSTRINGENCY")]
    public int Fstringency { get; set; }

    /// <summary>
    /// AQL
    /// </summary>
    [SugarColumn(ColumnName = "FAQL")]
    public string Faql { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FEFFECTIVEDATE")]
    public DateTime? Feffectivedate { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    [SugarColumn(ColumnName = "FENDDATE")]
    public DateTime? Fenddate { get; set; }

    /// <summary>
    /// 触发方式
    /// </summary>
    [SugarColumn(ColumnName = "FTRIGGERTYPE")]
    public int Ftriggertype { get; set; }

    /// <summary>
    /// 频率
    /// </summary>
    [SugarColumn(ColumnName = "FREQUENCY")]
    public decimal Frequency { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    [SugarColumn(ColumnName = "FPICTURE")]
    public byte[]? Fpicture { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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

    /// <summary>
    /// 检验标准
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONSTDID")]
    public string Finspectionstdid { get; set; } = string.Empty;
}
