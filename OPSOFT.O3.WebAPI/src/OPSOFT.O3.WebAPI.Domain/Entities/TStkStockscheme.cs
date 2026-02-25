using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 盘点方案
/// </summary>
[SugarTable("T_STK_STOCKSCHEME")]
public class TStkStockscheme : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPEID")]
    public string Fbilltypeid { get; set; } = string.Empty;

    /// <summary>
    /// 单据名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 单据日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 是否同步
    /// </summary>
    [SugarColumn(ColumnName = "ISSYN")]
    public bool Issyn { get; set; }

    /// <summary>
    /// ERP单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FERPID")]
    public string Ferpid { get; set; } = string.Empty;

    /// <summary>
    /// ERP单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FERPNO")]
    public string Ferpno { get; set; } = string.Empty;

    /// <summary>
    /// 库存组织
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKORGID")]
    public string Fstockorgid { get; set; } = string.Empty;

    /// <summary>
    /// 零库存参与盘点
    /// </summary>
    [SugarColumn(ColumnName = "FZEROSTOCKINCOUNT")]
    public bool Fzerostockincount { get; set; }

    /// <summary>
    /// 物料盘点作业是否允许新增物料
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWADDMATERIAL")]
    public bool Fallowaddmaterial { get; set; }

    /// <summary>
    /// 禁用物料不参与盘点
    /// </summary>
    [SugarColumn(ColumnName = "FNOTINCLUDEFORBIDMAT")]
    public bool Fnotincludeforbidmat { get; set; }

    /// <summary>
    /// 实盘默认数值
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKQTYDEFAULT")]
    public string Fcheckqtydefault { get; set; } = string.Empty;

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
    /// 保管者类型
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERTYPEID")]
    public string Fkeepertypeid { get; set; } = string.Empty;

    /// <summary>
    /// 保管者
    /// </summary>
    [SugarColumn(ColumnName = "FKEEPERID")]
    public string Fkeeperid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库代码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库代码（至）
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKIDTO")]
    public string Fstockidto { get; set; } = string.Empty;

    /// <summary>
    /// 物料从
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 物料至
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALIDTO")]
    public string Fmaterialidto { get; set; } = string.Empty;

    /// <summary>
    /// 截止日期
    /// </summary>
    [SugarColumn(ColumnName = "FLOSEDATE")]
    public DateTime? Flosedate { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 提交人
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITID")]
    public string Fsubmitid { get; set; } = string.Empty;

    /// <summary>
    /// 提交日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITDATE")]
    public DateTime? Fsubmitdate { get; set; }

    /// <summary>
    /// 确认人
    /// </summary>
    [SugarColumn(ColumnName = "FCONFIRMID")]
    public string Fconfirmid { get; set; } = string.Empty;

    /// <summary>
    /// 确认日期
    /// </summary>
    [SugarColumn(ColumnName = "FCONFIRMDATE")]
    public DateTime? Fconfirmdate { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
