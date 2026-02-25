using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 技能升级
/// </summary>
[SugarTable("T_WM_SKILLUP")]
public class TWmSkillup : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 职员内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 成本中心内码
    /// </summary>
    [SugarColumn(ColumnName = "FCOSTCENTERID")]
    public string Fcostcenterid { get; set; } = string.Empty;

    /// <summary>
    /// 工艺技能内码
    /// </summary>
    [SugarColumn(ColumnName = "FROUTESKILLID")]
    public string Frouteskillid { get; set; } = string.Empty;

    /// <summary>
    /// 工艺技能等级
    /// </summary>
    [SugarColumn(ColumnName = "FROUTESKILLGRADE")]
    public int Frouteskillgrade { get; set; }

    /// <summary>
    /// 设备技能内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTSKILLID")]
    public string Fequipmentskillid { get; set; } = string.Empty;

    /// <summary>
    /// 设备技能等级
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTSKILLGRADE")]
    public int Fequipmentskillgrade { get; set; }

    /// <summary>
    /// 开始日期
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTDATE")]
    public DateTime? Fstartdate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    [SugarColumn(ColumnName = "FENDDATE")]
    public DateTime? Fenddate { get; set; }

    /// <summary>
    /// 培训日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 讲师内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEACHERID")]
    public string Fteacherid { get; set; } = string.Empty;

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 累计生产数量
    /// </summary>
    [SugarColumn(ColumnName = "FQTY")]
    public decimal Fqty { get; set; }

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 技能等级
    /// </summary>
    [SugarColumn(ColumnName = "FSKILLGRADE")]
    public int Fskillgrade { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
}
