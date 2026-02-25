using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 润滑实施表体
/// </summary>
[SugarTable("T_ENG_EQLUEXECUTEENTRY")]
public class TEngEqluexecuteentry : BaseEntity
{
    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTID")]
    public string Fequipmentid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 润滑点内码
    /// </summary>
    [SugarColumn(ColumnName = "FLUID")]
    public string Fluid { get; set; } = string.Empty;

    /// <summary>
    /// 润滑点编号
    /// </summary>
    [SugarColumn(ColumnName = "FLUNUMBER")]
    public string Flunumber { get; set; } = string.Empty;

    /// <summary>
    /// 润滑点名称
    /// </summary>
    [SugarColumn(ColumnName = "FLUNAME")]
    public string Fluname { get; set; } = string.Empty;

    /// <summary>
    /// 润滑类别内码
    /// </summary>
    [SugarColumn(ColumnName = "FLUTYPEID")]
    public string Flutypeid { get; set; } = string.Empty;

    /// <summary>
    /// 润滑材料内码
    /// </summary>
    [SugarColumn(ColumnName = "FLUMATERIALID")]
    public string Flumaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 计划日期
    /// </summary>
    [SugarColumn(ColumnName = "FLUPDATE")]
    public DateTime? Flupdate { get; set; }

    /// <summary>
    /// 实际日期
    /// </summary>
    [SugarColumn(ColumnName = "FLUADATE")]
    public DateTime? Fluadate { get; set; }

    /// <summary>
    /// 实施情况
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTEINFO")]
    public string Fexecuteinfo { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITNAME")]
    public string Funitname { get; set; } = string.Empty;

    /// <summary>
    /// 定额
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 责任人
    /// </summary>
    [SugarColumn(ColumnName = "FPRINCIPALS")]
    public string Fprincipals { get; set; } = string.Empty;

    /// <summary>
    /// 油质测试方法
    /// </summary>
    [SugarColumn(ColumnName = "FTESTMETHOD")]
    public string Ftestmethod { get; set; } = string.Empty;

    /// <summary>
    /// 油质测试结果
    /// </summary>
    [SugarColumn(ColumnName = "FTESTRESULT")]
    public string Ftestresult { get; set; } = string.Empty;

    /// <summary>
    /// 测试后处理
    /// </summary>
    [SugarColumn(ColumnName = "FTESTDISPOSE")]
    public string Ftestdispose { get; set; } = string.Empty;

    /// <summary>
    /// 润滑计划表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FPLANID")]
    public string Fplanid { get; set; } = string.Empty;

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
