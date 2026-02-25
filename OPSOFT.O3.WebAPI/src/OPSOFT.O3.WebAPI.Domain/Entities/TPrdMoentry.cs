using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 生产任务单明细
/// </summary>
[SugarTable("T_PRD_MOENTRY")]
public class TPrdMoentry : BaseEntity
{
    /// <summary>
    /// 产品类型
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTTYPE")]
    public string Fproducttype { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 基本单位
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITID")]
    public string Fbaseunitid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FCOMMONUNITID")]
    public string Fcommonunitid { get; set; } = string.Empty;

    /// <summary>
    /// 组别
    /// </summary>
    [SugarColumn(ColumnName = "FGROUP")]
    public string Fgroup { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 业务状态
    /// </summary>
    [SugarColumn(ColumnName = "FBSTATUS")]
    public string Fbstatus { get; set; } = string.Empty;

    /// <summary>
    /// 多单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FAUXQTY")]
    public decimal Fauxqty { get; set; }

    /// <summary>
    /// 计划开工时间
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTDATE")]
    public DateTime? Fplanstartdate { get; set; }

    /// <summary>
    /// 计划完工时间
    /// </summary>
    [SugarColumn(ColumnName = "FPLANFINISHDATE")]
    public DateTime? Fplanfinishdate { get; set; }

    /// <summary>
    /// 需求组织
    /// </summary>
    [SugarColumn(ColumnName = "FREQUESTORGID")]
    public string Frequestorgid { get; set; } = string.Empty;

    /// <summary>
    /// BOM版本
    /// </summary>
    [SugarColumn(ColumnName = "FBOMID")]
    public string Fbomid { get; set; } = string.Empty;

    /// <summary>
    /// 倒冲领料
    /// </summary>
    [SugarColumn(ColumnName = "FISBACKFLUSH")]
    public bool Fisbackflush { get; set; }

    /// <summary>
    /// 入库组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKINORGID")]
    public string Fstockinorgid { get; set; } = string.Empty;

    /// <summary>
    /// 需求优先级
    /// </summary>
    [SugarColumn(ColumnName = "FPRIORITY")]
    public int Fpriority { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 批次内码
    /// </summary>
    [SugarColumn(ColumnName = "FLOTID")]
    public string Flotid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 备用栏位
    /// </summary>
    [SugarColumn(ColumnName = "FBCTYPE")]
    public string Fbctype { get; set; } = string.Empty;

    /// <summary>
    /// 已入库数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKQTY")]
    public decimal Fstockqty { get; set; }

    /// <summary>
    /// 已入库基本数量
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKBASEQTY")]
    public decimal Fstockbaseqty { get; set; }

    /// <summary>
    /// 基本单位数量
    /// </summary>
    [SugarColumn(ColumnName = "FBASEUNITQTY")]
    public decimal Fbaseunitqty { get; set; }

    /// <summary>
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 领料状态
    /// </summary>
    [SugarColumn(ColumnName = "FPICKMTRLSTATUS")]
    public string Fpickmtrlstatus { get; set; } = string.Empty;

    /// <summary>
    /// 排产状态
    /// </summary>
    [SugarColumn(ColumnName = "FSCHEDULESTATUS")]
    public string Fschedulestatus { get; set; } = string.Empty;

    /// <summary>
    /// 产品工艺流程内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROROUTEID")]
    public string Fprorouteid { get; set; } = string.Empty;

    /// <summary>
    /// ERP工单号
    /// </summary>
    [SugarColumn(ColumnName = "FERPICMONO")]
    public string Ferpicmono { get; set; } = string.Empty;

    /// <summary>
    /// 班产量
    /// </summary>
    [SugarColumn(ColumnName = "FTECHSHIFT")]
    public decimal Ftechshift { get; set; }

    /// <summary>
    /// 半成品
    /// </summary>
    [SugarColumn(ColumnName = "FELIGIBILITYRATE")]
    public decimal Feligibilityrate { get; set; }

    /// <summary>
    /// 合格率
    /// </summary>
    [SugarColumn(ColumnName = "FFTPASSRATE")]
    public decimal Fftpassrate { get; set; }

    /// <summary>
    /// 派工数量
    /// </summary>
    [SugarColumn(ColumnName = "FFCQTY")]
    public decimal Ffcqty { get; set; }

    /// <summary>
    /// 实际开工时间
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALSTARTDATE")]
    public DateTime? Factualstartdate { get; set; }

    /// <summary>
    /// 实际完工时间
    /// </summary>
    [SugarColumn(ColumnName = "FACTUALFINISHDATE")]
    public DateTime? Factualfinishdate { get; set; }

    /// <summary>
    /// 机型
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEMODEL")]
    public string Fmachinemodel { get; set; } = string.Empty;

    /// <summary>
    /// 累计完工数量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALFINISHQTY")]
    public decimal Ftotalfinishqty { get; set; }

    /// <summary>
    /// 超产比例（%）
    /// </summary>
    [SugarColumn(ColumnName = "FINHIGHLIMIT")]
    public decimal Finhighlimit { get; set; }

    /// <summary>
    /// 换型时长（分）
    /// </summary>
    [SugarColumn(ColumnName = "FRETIME")]
    public decimal Fretime { get; set; }

    /// <summary>
    /// 关闭状态
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSED")]
    public bool Fclosed { get; set; }

    /// <summary>
    /// 是否挂起
    /// </summary>
    [SugarColumn(ColumnName = "FISSUSPEND")]
    public bool Fissuspend { get; set; }

    /// <summary>
    /// 销售订单明细内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERDETAILID")]
    public string Forderdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 销售订单明细行号
    /// </summary>
    [SugarColumn(ColumnName = "FORDERENTRYID")]
    public int Forderentryid { get; set; }

    /// <summary>
    /// 销售订单内码
    /// </summary>
    [SugarColumn(ColumnName = "FORDERINTERID")]
    public string Forderinterid { get; set; } = string.Empty;

    /// <summary>
    /// 订单类型
    /// </summary>
    [SugarColumn(ColumnName = "FORDERTYPEID")]
    public string Fordertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 结案人内码
    /// </summary>
    [SugarColumn(ColumnName = "FFINALID")]
    public string Ffinalid { get; set; } = string.Empty;

    /// <summary>
    /// 结案日期
    /// </summary>
    [SugarColumn(ColumnName = "FFINALDATE")]
    public DateTime? Ffinaldate { get; set; }

    /// <summary>
    /// 条码规则
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODERULEID")]
    public string Fbarcoderuleid { get; set; } = string.Empty;

    /// <summary>
    /// 条码打印模板
    /// </summary>
    [SugarColumn(ColumnName = "FPRINTMODELID")]
    public string Fprintmodelid { get; set; } = string.Empty;

    /// <summary>
    /// 下达日期
    /// </summary>
    [SugarColumn(ColumnName = "FCONVEYDATE")]
    public DateTime? Fconveydate { get; set; }

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
