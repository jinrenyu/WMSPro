using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 简单生产领料单-单据头
/// </summary>
[SugarTable("T_SP_PICKMTRL")]
public class TSpPickmtrl : BaseEntity
{
    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 发料组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKORGID")]
    public string Fstockorgid { get; set; } = string.Empty;

    /// <summary>
    /// 生产组织
    /// </summary>
    [SugarColumn(ColumnName = "FPRDORGID")]
    public string Fprdorgid { get; set; } = string.Empty;

    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 货主类型
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERTYPEID")]
    public string Fownertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 货主
    /// </summary>
    [SugarColumn(ColumnName = "FOWNERID")]
    public string Fownerid { get; set; } = string.Empty;

    /// <summary>
    /// 本位币
    /// </summary>
    [SugarColumn(ColumnName = "FCURRID")]
    public string Fcurrid { get; set; } = string.Empty;

    /// <summary>
    /// 生产车间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string Fworkshopid { get; set; } = string.Empty;

    /// <summary>
    /// 库存组
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKERGROUPID")]
    public string Fstockergroupid { get; set; } = string.Empty;

    /// <summary>
    /// 仓管员
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKERID")]
    public string Fstockerid { get; set; } = string.Empty;

    /// <summary>
    /// 领料人
    /// </summary>
    [SugarColumn(ColumnName = "FPICKERID")]
    public string Fpickerid { get; set; } = string.Empty;

    /// <summary>
    /// 跨组织业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FTRANSFERBIZTYPEID")]
    public string Ftransferbiztypeid { get; set; } = string.Empty;

    /// <summary>
    /// 组织间结算跨法人标识
    /// </summary>
    [SugarColumn(ColumnName = "FISINTERLEGALPERSON")]
    public bool Fisinterlegalperson { get; set; }

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "FBIZTYPE")]
    public string Fbiztype { get; set; } = string.Empty;

    /// <summary>
    /// 录入类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

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
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 是否同步
    /// </summary>
    [SugarColumn(ColumnName = "ISSYN")]
    public bool Issyn { get; set; }

    /// <summary>
    /// ERP编号
    /// </summary>
    [SugarColumn(ColumnName = "FERPNO")]
    public string Ferpno { get; set; } = string.Empty;

    /// <summary>
    /// ERP内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPID")]
    public string Ferpid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSID")]
    public string Fstockstatusid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
