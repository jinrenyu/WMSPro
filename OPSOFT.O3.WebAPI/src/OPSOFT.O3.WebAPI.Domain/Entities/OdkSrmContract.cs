using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 合同单表头
/// </summary>
[SugarTable("ODK_SRM_Contract")]
public class OdkSrmContract : BaseEntity
{
    /// <summary>
    /// 合同类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 申请人
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 合同名称
    /// </summary>
    [SugarColumn(ColumnName = "FTITLE")]
    public string Ftitle { get; set; } = string.Empty;

    /// <summary>
    /// 物料组
    /// </summary>
    [SugarColumn(ColumnName = "FMATGROUPID")]
    public string Fmatgroupid { get; set; } = string.Empty;

    /// <summary>
    /// 单据状态
    /// </summary>
    [SugarColumn(ColumnName = "FBILLSTATUS")]
    public int Fbillstatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 拒绝原因
    /// </summary>
    [SugarColumn(ColumnName = "FREFUREASON")]
    public string Frefureason { get; set; } = string.Empty;

    /// <summary>
    /// 已读状态
    /// </summary>
    [SugarColumn(ColumnName = "FHASREAD")]
    public int Fhasread { get; set; }

    /// <summary>
    /// 已读时间
    /// </summary>
    [SugarColumn(ColumnName = "FHASREADTIME")]
    public DateTime? Fhasreadtime { get; set; }

    /// <summary>
    /// 签署地点
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string Faddress { get; set; } = string.Empty;

    /// <summary>
    /// 合同总金额
    /// </summary>
    [SugarColumn(ColumnName = "FTOTALAMT")]
    public decimal Ftotalamt { get; set; }

    /// <summary>
    /// 合同条款模板
    /// </summary>
    [SugarColumn(ColumnName = "FCLAUSETEMPID")]
    public string Fclausetempid { get; set; } = string.Empty;

    /// <summary>
    /// 合同条款
    /// </summary>
    [SugarColumn(ColumnName = "FCLAUSE")]
    public string Fclause { get; set; } = string.Empty;

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 有效期从
    /// </summary>
    [SugarColumn(ColumnName = "FBDATE")]
    public DateTime? Fbdate { get; set; }

    /// <summary>
    /// 有效期至
    /// </summary>
    [SugarColumn(ColumnName = "FEDATE")]
    public DateTime? Fedate { get; set; }

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
    /// 是否发布
    /// </summary>
    [SugarColumn(ColumnName = "FISRELEASE")]
    public bool Fisrelease { get; set; }

    /// <summary>
    /// 禁用人内码
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 签订日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEYPE")]
    public string Fsourceype { get; set; } = string.Empty;

    /// <summary>
    /// 源单信息
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEINTERID")]
    public string Fsourceinterid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
