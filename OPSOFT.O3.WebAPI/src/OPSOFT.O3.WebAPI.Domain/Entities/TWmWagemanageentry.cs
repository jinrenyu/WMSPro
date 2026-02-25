using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工资计算汇总表表体
/// </summary>
[SugarTable("T_WM_WAGEMANAGEENTRY")]
public class TWmWagemanageentry : BaseEntity
{
    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 工资项目内码
    /// </summary>
    [SugarColumn(ColumnName = "FWAGEPROJECTID")]
    public string Fwageprojectid { get; set; } = string.Empty;

    /// <summary>
    /// 件数
    /// </summary>
    [SugarColumn(ColumnName = "FPIECEQTY")]
    public decimal Fpieceqty { get; set; }

    /// <summary>
    /// 单件工资
    /// </summary>
    [SugarColumn(ColumnName = "FPIECEPRICE")]
    public decimal Fpieceprice { get; set; }

    /// <summary>
    /// 工时
    /// </summary>
    [SugarColumn(ColumnName = "FWORKHOUR")]
    public decimal Fworkhour { get; set; }

    /// <summary>
    /// 每时工资
    /// </summary>
    [SugarColumn(ColumnName = "FTIMINGPRICE")]
    public decimal Ftimingprice { get; set; }

    /// <summary>
    /// 计件总工资
    /// </summary>
    [SugarColumn(ColumnName = "FPIECEAMOUNT")]
    public decimal Fpieceamount { get; set; }

    /// <summary>
    /// 计时总工资
    /// </summary>
    [SugarColumn(ColumnName = "FTIMINGAMOUNT")]
    public decimal Ftimingamount { get; set; }

    /// <summary>
    /// 总工资
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 操作人员
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

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
