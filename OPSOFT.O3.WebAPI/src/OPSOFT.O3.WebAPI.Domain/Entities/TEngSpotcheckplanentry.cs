using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 点检方案表体
/// </summary>
[SugarTable("T_ENG_SPOTCHECKPLANENTRY")]
public class TEngSpotcheckplanentry : BaseEntity
{
    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEID")]
    public string Fmachineid { get; set; } = string.Empty;

    /// <summary>
    /// 部位
    /// </summary>
    [SugarColumn(ColumnName = "FPARTID")]
    public string Fpartid { get; set; } = string.Empty;

    /// <summary>
    /// 点检项目
    /// </summary>
    [SugarColumn(ColumnName = "FCONTENT")]
    public string Fcontent { get; set; } = string.Empty;

    /// <summary>
    /// 点检方式
    /// </summary>
    [SugarColumn(ColumnName = "FANALYSISWAY")]
    public int Fanalysisway { get; set; }

    /// <summary>
    /// 点检结果，0=未点检，1=合格，2=不合格，
    /// </summary>
    [SugarColumn(ColumnName = "FTESTRESULT")]
    public string Ftestresult { get; set; } = string.Empty;

    /// <summary>
    /// 未点检原因
    /// </summary>
    [SugarColumn(ColumnName = "FCAUSESKETCH")]
    public string Fcausesketch { get; set; } = string.Empty;

    /// <summary>
    /// 执行部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 缺陷等级
    /// </summary>
    [SugarColumn(ColumnName = "FFAULTLEVEL")]
    public int Ffaultlevel { get; set; }

    /// <summary>
    /// 目标值
    /// </summary>
    [SugarColumn(ColumnName = "FVALUE")]
    public decimal Fvalue { get; set; }

    /// <summary>
    /// 上限
    /// </summary>
    [SugarColumn(ColumnName = "FUPPERLIMIT")]
    public decimal Fupperlimit { get; set; }

    /// <summary>
    /// 下限
    /// </summary>
    [SugarColumn(ColumnName = "FLWERLIMIT")]
    public decimal Flwerlimit { get; set; }

    /// <summary>
    /// 检验值
    /// </summary>
    [SugarColumn(ColumnName = "FTESTVALUE")]
    public decimal Ftestvalue { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 检验项目
    /// </summary>
    [SugarColumn(ColumnName = "FTESTPROJECTID")]
    public string Ftestprojectid { get; set; } = string.Empty;

    /// <summary>
    /// 点检参数范围
    /// </summary>
    [SugarColumn(ColumnName = "FRANGE")]
    public string Frange { get; set; } = string.Empty;

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
