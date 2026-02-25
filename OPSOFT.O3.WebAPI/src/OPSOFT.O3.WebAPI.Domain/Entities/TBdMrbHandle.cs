using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// MRB处理
/// </summary>
[SugarTable("T_BD_MRBHANDLE")]
public class TBdMrbhandle : BaseEntity
{
    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTATE")]
    public string Fstate { get; set; } = string.Empty;

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
    /// 收料单ID
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIPTID")]
    public string Freceiptid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库ID
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 物料ID
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FNUM")]
    public decimal Fnum { get; set; }

    /// <summary>
    /// 供应商ID
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLIER")]
    public string Fsupplier { get; set; } = string.Empty;

    /// <summary>
    /// 客户ID
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTOMER")]
    public string Fcustomer { get; set; } = string.Empty;

    /// <summary>
    /// 检验状态
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKSTATE")]
    public string Fcheckstate { get; set; } = string.Empty;

    /// <summary>
    /// 合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FQUALIFIEDQUANTITY")]
    public decimal Fqualifiedquantity { get; set; }

    /// <summary>
    /// 不合格数量
    /// </summary>
    [SugarColumn(ColumnName = "FUNQUALIFIEDQUANTITY")]
    public decimal Funqualifiedquantity { get; set; }

    /// <summary>
    /// 加急
    /// </summary>
    [SugarColumn(ColumnName = "FURGENT")]
    public bool Furgent { get; set; }

    /// <summary>
    /// 报废数量
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPQUANTITY")]
    public decimal Fscrapquantity { get; set; }

    /// <summary>
    /// 单位ID
    /// </summary>
    [SugarColumn(ColumnName = "FQUITID")]
    public string Fquitid { get; set; } = string.Empty;

    /// <summary>
    /// 检验时间
    /// </summary>
    [SugarColumn(ColumnName = "FINSPECTIONTIME")]
    public DateTime? Finspectiontime { get; set; }

    /// <summary>
    /// 送检人ID
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITTEDBY")]
    public string Fsubmittedby { get; set; } = string.Empty;

    /// <summary>
    /// 是否审核
    /// </summary>
    [SugarColumn(ColumnName = "FISMRB")]
    public bool Fismrb { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 检验单ID
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKID")]
    public string Fcheckid { get; set; } = string.Empty;

    /// <summary>
    /// 条码主档ID
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// MRB评审结果
    /// </summary>
    [SugarColumn(ColumnName = "FMRBREVIEW")]
    public string Fmrbreview { get; set; } = string.Empty;

    /// <summary>
    /// 挑选良品数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHOOSEQUALIFIEDQUANTITY")]
    public decimal Fchoosequalifiedquantity { get; set; }

    /// <summary>
    /// 挑选不良品数量
    /// </summary>
    [SugarColumn(ColumnName = "FCHOOSEUNQUALIFIEDQUANTITY")]
    public decimal Fchooseunqualifiedquantity { get; set; }

    /// <summary>
    /// 挑选人ID
    /// </summary>
    [SugarColumn(ColumnName = "FCHOOSEID")]
    public string Fchooseid { get; set; } = string.Empty;

    /// <summary>
    /// 收料单编码
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIPTFNUMBER")]
    public string Freceiptfnumber { get; set; } = string.Empty;

    /// <summary>
    /// 不良品仓ID
    /// </summary>
    [SugarColumn(ColumnName = "FUNSTOCKID")]
    public string Funstockid { get; set; } = string.Empty;

    /// <summary>
    /// 原单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEBILLTYPE")]
    public int Fsourcebilltype { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
