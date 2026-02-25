using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 开箱验收
/// </summary>
[SugarTable("T_ENG_EQOUTBOXACCEPT")]
public class TEngEqoutboxaccept : BaseEntity
{
    /// <summary>
    /// 原立项单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEINTERID")]
    public string Fsourceinterid { get; set; } = string.Empty;

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    [SugarColumn(ColumnName = "FEQNAME")]
    public string Feqname { get; set; } = string.Empty;

    /// <summary>
    /// 设备型号
    /// </summary>
    [SugarColumn(ColumnName = "FEQMODEL")]
    public string Feqmodel { get; set; } = string.Empty;

    /// <summary>
    /// 设备规格
    /// </summary>
    [SugarColumn(ColumnName = "FEQSIZE")]
    public string Feqsize { get; set; } = string.Empty;

    /// <summary>
    /// 出厂编号
    /// </summary>
    [SugarColumn(ColumnName = "FFANUMBER")]
    public string Ffanumber { get; set; } = string.Empty;

    /// <summary>
    /// 生产厂商内码
    /// </summary>
    [SugarColumn(ColumnName = "FMASUPPLYID")]
    public string Fmasupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 随机附件
    /// </summary>
    [SugarColumn(ColumnName = "FATTACHMENT")]
    public string Fattachment { get; set; } = string.Empty;

    /// <summary>
    /// 随机工具
    /// </summary>
    [SugarColumn(ColumnName = "FTOOLS")]
    public string Ftools { get; set; } = string.Empty;

    /// <summary>
    /// 外形尺寸
    /// </summary>
    [SugarColumn(ColumnName = "FBODIMENSION")]
    public string Fbodimension { get; set; } = string.Empty;

    /// <summary>
    /// 设备部验收人内码
    /// </summary>
    [SugarColumn(ColumnName = "FINPERSONID")]
    public string Finpersonid { get; set; } = string.Empty;

    /// <summary>
    /// 使用部门验收人内码
    /// </summary>
    [SugarColumn(ColumnName = "FUSEPERSONID")]
    public string Fusepersonid { get; set; } = string.Empty;

    /// <summary>
    /// 安装/档案负责人
    /// </summary>
    [SugarColumn(ColumnName = "FARPERSONID")]
    public string Farpersonid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 已验收入帐
    /// </summary>
    [SugarColumn(ColumnName = "FISCHECK")]
    public bool Fischeck { get; set; }

    /// <summary>
    /// 已转移交单
    /// </summary>
    [SugarColumn(ColumnName = "FISSETUP")]
    public bool Fissetup { get; set; }

    /// <summary>
    /// 立项日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODATE")]
    public DateTime? Fprodate { get; set; }

    /// <summary>
    /// 安装日期
    /// </summary>
    [SugarColumn(ColumnName = "FINSTALLATIONDATE")]
    public DateTime? Finstallationdate { get; set; }

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
