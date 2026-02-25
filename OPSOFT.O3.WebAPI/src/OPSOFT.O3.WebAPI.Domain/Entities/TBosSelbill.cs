using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 出入库流程配置
/// </summary>
[SugarTable("T_BOS_SELBILL")]
public class TBosSelbill : BaseEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 源单类型
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCETRANTYPE")]
    public string Fsourcetrantype { get; set; } = string.Empty;

    /// <summary>
    /// 目标单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FDESTTRANTYPE")]
    public string Fdesttrantype { get; set; } = string.Empty;

    /// <summary>
    /// 是否控制数量
    /// </summary>
    [SugarColumn(ColumnName = "FISCONTROLQTY")]
    public bool Fiscontrolqty { get; set; }

    /// <summary>
    /// 是否开放源单
    /// </summary>
    [SugarColumn(ColumnName = "FISOPENSOURCE")]
    public bool Fisopensource { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [SugarColumn(ColumnName = "FISUSE")]
    public bool Fisuse { get; set; }

    /// <summary>
    /// 默认源单
    /// </summary>
    [SugarColumn(ColumnName = "FDEFAULT")]
    public bool Fdefault { get; set; }

    /// <summary>
    /// 是否审核
    /// </summary>
    [SugarColumn(ColumnName = "FCHECK")]
    public bool Fcheck { get; set; }

    /// <summary>
    /// 条码类型
    /// </summary>
    [SugarColumn(ColumnName = "FKIND")]
    public string Fkind { get; set; } = string.Empty;

    /// <summary>
    /// 是否整单录入
    /// </summary>
    [SugarColumn(ColumnName = "FISWHOLEOUT")]
    public bool Fiswholeout { get; set; }

    /// <summary>
    /// 带出仓库仓位
    /// </summary>
    [SugarColumn(ColumnName = "FISDEFAULTSTOCK")]
    public bool Fisdefaultstock { get; set; }

    /// <summary>
    /// 是否检查批次
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECKLOT")]
    public bool Fischecklot { get; set; }

    /// <summary>
    /// 是否检查辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECKAUX")]
    public bool Fischeckaux { get; set; }

    /// <summary>
    /// 是否检查保质日期
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECKKFDATE")]
    public bool Fischeckkfdate { get; set; }

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
    /// 是否可以下推
    /// </summary>
    [SugarColumn(ColumnName = "FCANPUSHDOWN")]
    public bool Fcanpushdown { get; set; }

    /// <summary>
    /// 是否同步到ERP
    /// </summary>
    [SugarColumn(ColumnName = "FCANSYNERP")]
    public bool Fcansynerp { get; set; }

    /// <summary>
    /// 启用标签规则
    /// </summary>
    [SugarColumn(ColumnName = "FISUSECODERULE")]
    public bool Fisusecoderule { get; set; }

    /// <summary>
    /// 检验ERP单据
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERPSTOCK")]
    public bool Fcheckerpstock { get; set; }

    /// <summary>
    /// ERP存储过程
    /// </summary>
    [SugarColumn(ColumnName = "FPRONAME")]
    public string Fproname { get; set; } = string.Empty;
}
