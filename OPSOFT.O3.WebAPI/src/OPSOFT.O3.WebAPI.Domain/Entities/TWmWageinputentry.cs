using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工资计算表体
/// </summary>
[SugarTable("T_WM_WAGEINPUTENTRY")]
public class TWmWageinputentry : BaseEntity
{
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡工序表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDDETAILID")]
    public string Fflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工资计算方式(1:计件/2:计时)
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 计件流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FPAYFLOWCARDID")]
    public string Fpayflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 计件工工序
    /// </summary>
    [SugarColumn(ColumnName = "FPAYPROCESSID")]
    public string Fpayprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 计件工工序行内码
    /// </summary>
    [SugarColumn(ColumnName = "FPAYFLOWCARDDETAILID")]
    public string Fpayflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 流转数量
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWQTY")]
    public decimal Fflowqty { get; set; }

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public decimal Fquaqty { get; set; }

    /// <summary>
    /// 不良数量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

    /// <summary>
    /// 前道追踪数量
    /// </summary>
    [SugarColumn(ColumnName = "FPREBADQTY")]
    public decimal Fprebadqty { get; set; }

    /// <summary>
    /// 计件数量
    /// </summary>
    [SugarColumn(ColumnName = "FPIECEQTY")]
    public decimal Fpieceqty { get; set; }

    /// <summary>
    /// 累计计件数量
    /// </summary>
    [SugarColumn(ColumnName = "FALLPIECEQTY")]
    public decimal Fallpieceqty { get; set; }

    /// <summary>
    /// 工时
    /// </summary>
    [SugarColumn(ColumnName = "FWORKHOUR")]
    public decimal Fworkhour { get; set; }

    /// <summary>
    /// 员工系数
    /// </summary>
    [SugarColumn(ColumnName = "FCOEFFICIENT")]
    public decimal Fcoefficient { get; set; }

    /// <summary>
    /// 员工系数总和
    /// </summary>
    [SugarColumn(ColumnName = "FSUMCOEFFICIENT")]
    public decimal Fsumcoefficient { get; set; }

    /// <summary>
    /// 工步系数
    /// </summary>
    [SugarColumn(ColumnName = "FSTEPCOEFFICIENT")]
    public decimal Fstepcoefficient { get; set; }

    /// <summary>
    /// 员工系数总和
    /// </summary>
    [SugarColumn(ColumnName = "FSTEPSUMCOEFFICIENT")]
    public decimal Fstepsumcoefficient { get; set; }

    /// <summary>
    /// 单价工资
    /// </summary>
    [SugarColumn(ColumnName = "FPIECEPRICE")]
    public decimal Fpieceprice { get; set; }

    /// <summary>
    /// 单价工时
    /// </summary>
    [SugarColumn(ColumnName = "FTIMINGPRICE")]
    public decimal Ftimingprice { get; set; }

    /// <summary>
    /// 计件工资
    /// </summary>
    [SugarColumn(ColumnName = "FPIECEAMOUNT")]
    public decimal Fpieceamount { get; set; }

    /// <summary>
    /// 计时工资
    /// </summary>
    [SugarColumn(ColumnName = "FTIMINGAMOUNT")]
    public decimal Ftimingamount { get; set; }

    /// <summary>
    /// 总金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNT")]
    public decimal Famount { get; set; }

    /// <summary>
    /// 累计已发总额
    /// </summary>
    [SugarColumn(ColumnName = "FALLAMOUNT")]
    public decimal Fallamount { get; set; }

    /// <summary>
    /// 累计个人总额
    /// </summary>
    [SugarColumn(ColumnName = "FALLEACHAMOUNT")]
    public decimal Falleachamount { get; set; }

    /// <summary>
    /// 个人金额总额
    /// </summary>
    [SugarColumn(ColumnName = "FEACHAMOUNT")]
    public decimal Feachamount { get; set; }

    /// <summary>
    /// 是否计件
    /// </summary>
    [SugarColumn(ColumnName = "FISPIECEWORK")]
    public bool Fispiecework { get; set; }

    /// <summary>
    /// 是否计时
    /// </summary>
    [SugarColumn(ColumnName = "FISTIMINGWORK")]
    public bool Fistimingwork { get; set; }

    /// <summary>
    /// 是否启用工步
    /// </summary>
    [SugarColumn(ColumnName = "FISPROCESSSTEP")]
    public bool Fisprocessstep { get; set; }

    /// <summary>
    /// 工步单价工资
    /// </summary>
    [SugarColumn(ColumnName = "FSTEPPIECEPRICE")]
    public decimal Fsteppieceprice { get; set; }

    /// <summary>
    /// 工步计件数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTEPPIECEQTY")]
    public decimal Fsteppieceqty { get; set; }

    /// <summary>
    /// 工步内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSSTEPID")]
    public string Fprocessstepid { get; set; } = string.Empty;

    /// <summary>
    /// 工步合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTEPQUAQTY")]
    public decimal Fstepquaqty { get; set; }

    /// <summary>
    /// 工步不良数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTEPBADQTY")]
    public decimal Fstepbadqty { get; set; }

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
