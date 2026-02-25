using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 销售订单
/// </summary>
[SugarTable("T_SAL_ORDER")]
public class TSalOrder : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBUSINESSTYPE")]
    public string Fbusinesstype { get; set; } = string.Empty;

    /// <summary>
    /// 订单客户
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMERID")]
    public string Fcustomerid { get; set; } = string.Empty;

    /// <summary>
    /// 结算币别
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLECURRID")]
    public string Fsettlecurrid { get; set; } = string.Empty;

    /// <summary>
    /// 销售部门
    /// </summary>
    [SugarColumn(ColumnName = "FSALEDEPTID")]
    public string Fsaledeptid { get; set; } = string.Empty;

    /// <summary>
    /// 销售员
    /// </summary>
    [SugarColumn(ColumnName = "FSALERID")]
    public string Fsalerid { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FDOCUMENTSTATUS")]
    public string Fdocumentstatus { get; set; } = string.Empty;

    /// <summary>
    /// 关闭状态
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSESTATUS")]
    public string Fclosestatus { get; set; } = string.Empty;

    /// <summary>
    /// 关闭人员
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSERID")]
    public string Fcloserid { get; set; } = string.Empty;

    /// <summary>
    /// 关闭日期
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSEDATE")]
    public DateTime? Fclosedate { get; set; }

    /// <summary>
    /// 作废状态
    /// </summary>
    [SugarColumn(ColumnName = "FCANCELSTATE")]
    public string Fcancelstate { get; set; } = string.Empty;

    /// <summary>
    /// 作废人员
    /// </summary>
    [SugarColumn(ColumnName = "FCANCELRID")]
    public string Fcancelrid { get; set; } = string.Empty;

    /// <summary>
    /// 作废日期
    /// </summary>
    [SugarColumn(ColumnName = "FCANCELDATE")]
    public DateTime? Fcanceldate { get; set; }

    /// <summary>
    /// 结算方
    /// </summary>
    [SugarColumn(ColumnName = "FSETTLEID")]
    public string Fsettleid { get; set; } = string.Empty;

    /// <summary>
    /// 付款方
    /// </summary>
    [SugarColumn(ColumnName = "FCHARGEID")]
    public string Fchargeid { get; set; } = string.Empty;

    /// <summary>
    /// 版本号
    /// </summary>
    [SugarColumn(ColumnName = "FVERSIONNO")]
    public string Fversionno { get; set; } = string.Empty;

    /// <summary>
    /// 交货方式
    /// </summary>
    [SugarColumn(ColumnName = "FHEADDELIVERYWAY")]
    public string Fheaddeliveryway { get; set; } = string.Empty;

    /// <summary>
    /// 交货地址
    /// </summary>
    [SugarColumn(ColumnName = "FHEADLOCID")]
    public string Fheadlocid { get; set; } = string.Empty;

    /// <summary>
    /// 变更原因
    /// </summary>
    [SugarColumn(ColumnName = "FCHANGEREASON")]
    public string Fchangereason { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 收货地址
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEADDRESS")]
    public string Freceiveaddress { get; set; } = string.Empty;

    /// <summary>
    /// 收货方
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEID")]
    public string Freceiveid { get; set; } = string.Empty;

    /// <summary>
    /// 收货人姓名
    /// </summary>
    [SugarColumn(ColumnName = "FLINKMAN")]
    public string Flinkman { get; set; } = string.Empty;

    /// <summary>
    /// 收货方联系人
    /// </summary>
    [SugarColumn(ColumnName = "FRECCONTACTID")]
    public string Freccontactid { get; set; } = string.Empty;

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "FLINKPHONE")]
    public string Flinkphone { get; set; } = string.Empty;

    /// <summary>
    /// 汇率类型
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGETYPEID")]
    public string Fexchangetypeid { get; set; } = string.Empty;

    /// <summary>
    /// 汇率
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATE")]
    public decimal Fexchangerate { get; set; }

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
    /// 预交日期
    /// </summary>
    [SugarColumn(ColumnName = "FPREDATE")]
    public DateTime? Fpredate { get; set; }

    /// <summary>
    /// 销售方式
    /// </summary>
    [SugarColumn(ColumnName = "FSALESTYLE")]
    public string Fsalestyle { get; set; } = string.Empty;

    /// <summary>
    /// 运输提前期
    /// </summary>
    [SugarColumn(ColumnName = "FTRANSITAHEADTIME")]
    public decimal Ftransitaheadtime { get; set; }

    /// <summary>
    /// 销售主管
    /// </summary>
    [SugarColumn(ColumnName = "FMANGERID")]
    public string Fmangerid { get; set; } = string.Empty;

    /// <summary>
    /// 计划类别
    /// </summary>
    [SugarColumn(ColumnName = "FPLANCATEGORY")]
    public string Fplancategory { get; set; } = string.Empty;

    /// <summary>
    /// 审批状态
    /// </summary>
    [SugarColumn(ColumnName = "FOASTATUS")]
    public string Foastatus { get; set; } = string.Empty;

    /// <summary>
    /// 审批结果
    /// </summary>
    [SugarColumn(ColumnName = "FOARESULT")]
    public string Foaresult { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
