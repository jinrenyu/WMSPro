using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 核价明细/决标明细
/// </summary>
[SugarTable("ODK_SRM_Award")]
public class OdkSrmAward : BaseEntity
{
    /// <summary>
    /// 询价单号
    /// </summary>
    [SugarColumn(ColumnName = "FINQUIRYBILLNO")]
    public string Finquirybillno { get; set; } = string.Empty;

    /// <summary>
    /// 询价单内码
    /// </summary>
    [SugarColumn(ColumnName = "FINQUIRYINTERID")]
    public string Finquiryinterid { get; set; } = string.Empty;

    /// <summary>
    /// 询价单物料行内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATDETAILID")]
    public string Fmatdetailid { get; set; } = string.Empty;

    /// <summary>
    /// 报价单号
    /// </summary>
    [SugarColumn(ColumnName = "FQUOTEBILLNO")]
    public string Fquotebillno { get; set; } = string.Empty;

    /// <summary>
    /// 报价单内码
    /// </summary>
    [SugarColumn(ColumnName = "FQUOTEINTERID")]
    public string Fquoteinterid { get; set; } = string.Empty;

    /// <summary>
    /// 报价方内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 报价总价
    /// </summary>
    [SugarColumn(ColumnName = "FTOTQUOTEPRICE")]
    public decimal Ftotquoteprice { get; set; }

    /// <summary>
    /// 报价数量
    /// </summary>
    [SugarColumn(ColumnName = "FTOTQUOTEQTY")]
    public decimal Ftotquoteqty { get; set; }

    /// <summary>
    /// 总分
    /// </summary>
    [SugarColumn(ColumnName = "FTOTSCORE")]
    public decimal Ftotscore { get; set; }

    /// <summary>
    /// 中标数量
    /// </summary>
    [SugarColumn(ColumnName = "FWINQTY")]
    public decimal Fwinqty { get; set; }

    /// <summary>
    /// 中标比例
    /// </summary>
    [SugarColumn(ColumnName = "FWINRATIO")]
    public decimal Fwinratio { get; set; }

    /// <summary>
    /// 中标金额
    /// </summary>
    [SugarColumn(ColumnName = "FWINAMT")]
    public decimal Fwinamt { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 是否选中
    /// </summary>
    [SugarColumn(ColumnName = "FISSELECTED")]
    public bool Fisselected { get; set; }

    /// <summary>
    /// 选择理由
    /// </summary>
    [SugarColumn(ColumnName = "FSEREASONS")]
    public string Fsereasons { get; set; } = string.Empty;

    /// <summary>
    /// 招标单编号
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERBILLNO")]
    public string Ftenderbillno { get; set; } = string.Empty;

    /// <summary>
    /// 招标单内码
    /// </summary>
    [SugarColumn(ColumnName = "FTENDERINTERID")]
    public string Ftenderinterid { get; set; } = string.Empty;

    /// <summary>
    /// 投标单号
    /// </summary>
    [SugarColumn(ColumnName = "FBIDBILLNO")]
    public string Fbidbillno { get; set; } = string.Empty;

    /// <summary>
    /// 投标单内码
    /// </summary>
    [SugarColumn(ColumnName = "FBIDINTERID")]
    public string Fbidinterid { get; set; } = string.Empty;

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 备货/施工周期天
    /// </summary>
    [SugarColumn(ColumnName = "FPREPARECYCLE")]
    public int Fpreparecycle { get; set; }

    /// <summary>
    /// 交付日期
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYDATE")]
    public DateTime? Fdeliverydate { get; set; }

    /// <summary>
    /// 是否含税
    /// </summary>
    [SugarColumn(ColumnName = "FISTAX")]
    public bool Fistax { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAX")]
    public decimal Ftax { get; set; }

    /// <summary>
    /// 未税金额
    /// </summary>
    [SugarColumn(ColumnName = "FPRETAXAMT")]
    public decimal Fpretaxamt { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    [SugarColumn(ColumnName = "FTAXAMT")]
    public decimal Ftaxamt { get; set; }

    /// <summary>
    /// 总额
    /// </summary>
    [SugarColumn(ColumnName = "FAMT")]
    public decimal Famt { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
