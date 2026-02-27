using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 收货单表头
/// </summary>
[SugarTable("ODK_SRM_Receipt")]
public class OdkSrmReceipt : BaseEntity
{
    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 收退货标识(0:收货 1:退货
    /// </summary>
    [SugarColumn(ColumnName = "FROB")]
    public bool Frob { get; set; }

    /// <summary>
    /// 是否发布
    /// </summary>
    [SugarColumn(ColumnName = "FISRELEASE", IsNullable = true)]
    public bool? FISRELEASE { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE", IsNullable = true)]
    public DateTime? FDISABLEDATE { get; set; }

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS", IsNullable = true)]
    public int? FBILLSTATUS { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID", IsNullable = true)]
    public string FSUPPLYID { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID", IsNullable = true)]
    public string FCHECKERID { get; set; } = string.Empty;

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME", IsNullable = true)]
    public DateTime? FHASREADTIME { get; set; }

    /// <summary>
    /// 收货人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID", IsNullable = true)]
    public string FEMPID { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID", IsNullable = true)]
    public string FDISABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE", IsNullable = true)]
    public string FNOTE { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEPTID", IsNullable = true)]
    public string FDEPTID { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE", IsNullable = true)]
    public DateTime? FCHECKDATE { get; set; }

    /// <summary>
    /// 收货仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSPID", IsNullable = true)]
    public string FSPID { get; set; } = string.Empty;

    /// <summary>
    /// 收货仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID", IsNullable = true)]
    public string FSTOCKID { get; set; } = string.Empty;

    /// <summary>
    /// 已读状态
    /// </summary>
    [SugarColumn(ColumnName = "FHASREAD", IsNullable = true)]
    public int? FHASREAD { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;
}
