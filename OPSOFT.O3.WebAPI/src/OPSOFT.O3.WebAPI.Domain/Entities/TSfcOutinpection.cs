using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 委外检验
/// </summary>
[SugarTable("T_SFC_OUTINPECTION")]
public class TSfcOutinpection : BaseEntity
{
    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTYPE")]
    public int Fworktype { get; set; }

    /// <summary>
    /// 检验方案
    /// </summary>
    [SugarColumn(ColumnName = "FTESTWAYID")]
    public string Ftestwayid { get; set; } = string.Empty;

    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

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
    /// 检验数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT")]
    public decimal Fcount { get; set; }

    /// <summary>
    /// 检验部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPARTMENTID")]
    public string Fdepartmentid { get; set; } = string.Empty;

    /// <summary>
    /// 质检员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FBEGTIME")]
    public DateTime? Fbegtime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 耗用时间
    /// </summary>
    [SugarColumn(ColumnName = "FSPENDTIME")]
    public int Fspendtime { get; set; }

    /// <summary>
    /// 检验方式
    /// </summary>
    [SugarColumn(ColumnName = "FTESTTYPE")]
    public int Ftesttype { get; set; }

    /// <summary>
    /// 合格数
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTQTY")]
    public decimal Facceptqty { get; set; }

    /// <summary>
    /// 不合格数
    /// </summary>
    [SugarColumn(ColumnName = "FUNACCEPTQTY")]
    public decimal Funacceptqty { get; set; }

    /// <summary>
    /// 合格数(旧
    /// </summary>
    [SugarColumn(ColumnName = "FOLDACCEPTQTY")]
    public decimal Foldacceptqty { get; set; }
}
