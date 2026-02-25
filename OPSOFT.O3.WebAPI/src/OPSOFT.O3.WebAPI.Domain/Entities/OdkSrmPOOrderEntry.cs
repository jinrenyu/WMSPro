using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 采购订单表体--物料明细
/// </summary>
[SugarTable("ODK_SRM_POOrderEntry")]
public class OdkSrmPOOrderEntry : BaseEntity
{
    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 常用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 需求数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FDEMANDDATE")]
    public DateTime? Fdemanddate { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 运费
    /// </summary>
    [SugarColumn(ColumnName = "FRREIGHT")]
    public decimal Frreight { get; set; }

    /// <summary>
    /// 是否含税
    /// </summary>
    [SugarColumn(ColumnName = "FISTAX")]
    public bool Fistax { get; set; }

    /// <summary>
    /// 税前金额
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
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETRANTYPE")]
    public int Fsourcetrantype { get; set; }

    /// <summary>
    /// 源单编号
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBILLNO")]
    public string Fsourcebillno { get; set; } = string.Empty;

    /// <summary>
    /// 源单单据
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEINTERID")]
    public string Fsourceinterid { get; set; } = string.Empty;

    /// <summary>
    /// 源单行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEDETAILID")]
    public string Fsourcedetailid { get; set; } = string.Empty;

    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 检验方式
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKTYPE")]
    public string Fchecktype { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILSTATUS")]
    public int Fdetailstatus { get; set; }

    /// <summary>
    /// 承诺交货日期
    /// </summary>
    [SugarColumn(ColumnName = "FFBDATE")]
    public DateTime? Ffbdate { get; set; }

    /// <summary>
    /// 承诺交货数量
    /// </summary>
    [SugarColumn(ColumnName = "FFBQTY")]
    public decimal Ffbqty { get; set; }

    /// <summary>
    /// 反馈信息
    /// </summary>
    [SugarColumn(ColumnName = "FFBINF")]
    public string Ffbinf { get; set; } = string.Empty;

    /// <summary>
    /// 税率
    /// </summary>
    [SugarColumn(ColumnName = "FTAX")]
    public decimal Ftax { get; set; }

    /// <summary>
    /// 标准单价
    /// </summary>
    [SugarColumn(ColumnName = "FSTADPRICE")]
    public decimal Fstadprice { get; set; }

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
