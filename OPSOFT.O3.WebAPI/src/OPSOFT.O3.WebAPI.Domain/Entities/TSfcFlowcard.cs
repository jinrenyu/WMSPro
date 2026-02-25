using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序流转卡
/// </summary>
[SugarTable("T_SFC_FLOWCARD")]
public class TSfcFlowcard : BaseEntity
{
    /// <summary>
    /// 流转卡类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FBATCHNO")]
    public string Fbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID")]
    public string Fdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 产品工艺路线内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROROUTEID")]
    public string Fprorouteid { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单编号
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FMODETAILID")]
    public string Fmodetailid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal Fauxqty { get; set; }

    /// <summary>
    /// 工资计算方式
    /// </summary>
    [SugarColumn(ColumnName = "FPAYTYPE")]
    public int Fpaytype { get; set; }

    /// <summary>
    /// 计划开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTDATE")]
    public DateTime? Fplanstartdate { get; set; }

    /// <summary>
    /// 计划完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FPLANFINISHDATE")]
    public DateTime? Fplanfinishdate { get; set; }

    /// <summary>
    /// 实际开工日期
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALSTARTDATE")]
    public DateTime? Factualstartdate { get; set; }

    /// <summary>
    /// 实际完工日期
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALFINISHDATE")]
    public DateTime? Factualfinishdate { get; set; }

    /// <summary>
    /// 返工批号
    /// </summary>
    [SugarColumn(ColumnName = "FREWORKBATCHNO")]
    public string Freworkbatchno { get; set; } = string.Empty;

    /// <summary>
    /// 流转卡关闭状态
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCLOSE")]
    public bool Fflowclose { get; set; }

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL")]
    public int Fchecklevel { get; set; }

    /// <summary>
    /// 原始流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEFLOWCARDID")]
    public string Fsourceflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 上一级流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FLASTFLOWCARDID")]
    public string Flastflowcardid { get; set; } = string.Empty;

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
    /// 检验方案
    /// </summary>
    [SugarColumn(ColumnName = "FTESTWAYID")]
    public string Ftestwayid { get; set; } = string.Empty;

    /// <summary>
    /// 完工数量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALFINISHQTY")]
    public decimal Ftotalfinishqty { get; set; }

    /// <summary>
    /// 返工流转卡工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FRETFLOWCARDDETAILID")]
    public string Fretflowcarddetailid { get; set; } = string.Empty;

    /// <summary>
    /// 返工类型
    /// </summary>
    [SugarColumn(ColumnName = "FRETURNTYPE")]
    public int Freturntype { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
