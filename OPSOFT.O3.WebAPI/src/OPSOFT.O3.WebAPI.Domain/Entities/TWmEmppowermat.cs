using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 人员能力矩阵
/// </summary>
[SugarTable("T_WM_EMPPOWERMAT")]
public class TWmEmppowermat : BaseEntity
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
    /// 产品代码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 工序内码
    /// </summary>
    [SugarColumn(ColumnName = "FPROCESSID")]
    public string Fprocessid { get; set; } = string.Empty;

    /// <summary>
    /// 员工代码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 设备内码
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTID")]
    public string Fequipmentid { get; set; } = string.Empty;

    /// <summary>
    /// 同步累计产量(PCS)
    /// </summary>
    [SugarColumn(ColumnName = "FSYSOUTPUT")]
    public int Fsysoutput { get; set; }

    /// <summary>
    /// 同步累计工时（H)
    /// </summary>
    [SugarColumn(ColumnName = "FSYSHOUR")]
    public decimal Fsyshour { get; set; }

    /// <summary>
    /// 评估累计产量(PCS)
    /// </summary>
    [SugarColumn(ColumnName = "FEVAOUTPUT")]
    public int Fevaoutput { get; set; }

    /// <summary>
    /// 评估累计工时(H)
    /// </summary>
    [SugarColumn(ColumnName = "FEVAHOUR")]
    public decimal Fevahour { get; set; }

    /// <summary>
    /// 设备累计产量(PCS)
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPOUTPUT")]
    public int Fequipoutput { get; set; }

    /// <summary>
    /// 设备评估累计工时(H)
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPHOUR")]
    public decimal Fequiphour { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTDATE")]
    public DateTime? Fstartdate { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    [SugarColumn(ColumnName = "FENDDATE")]
    public DateTime? Fenddate { get; set; }

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
    /// 设备累计工时(H)
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPOUHOUR")]
    public decimal Fequipouhour { get; set; }

    /// <summary>
    /// 设备评估累计产量(PCS)
    /// </summary>
    [SugarColumn(ColumnName = "FEVAEQUIPOUTPUT")]
    public int Fevaequipoutput { get; set; }

    /// <summary>
    /// 工艺累计产量(PCS)
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEOUTPUT")]
    public int Frouteoutput { get; set; }

    /// <summary>
    /// 工艺累计工时(H)
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEOUHOUR")]
    public decimal Frouteouhour { get; set; }

    /// <summary>
    /// 工艺评估累计产量(PCS)
    /// </summary>
    [SugarColumn(ColumnName = "FEVAROUTEOUTPUT")]
    public int Fevarouteoutput { get; set; }

    /// <summary>
    /// 工艺评估累计工时(H)
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEHOUR")]
    public decimal Froutehour { get; set; }

    /// <summary>
    /// 加工纬度
    /// </summary>
    [SugarColumn(ColumnName = "FWORKITEMID")]
    public string Fworkitemid { get; set; } = string.Empty;

    /// <summary>
    /// 设备纬度
    /// </summary>
    [SugarColumn(ColumnName = "FEQUITEMID")]
    public string Fequitemid { get; set; } = string.Empty;

    /// <summary>
    /// 工艺纬度
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEITEMID")]
    public string Frouteitemid { get; set; } = string.Empty;

    /// <summary>
    /// 加工纬度等级
    /// </summary>
    [SugarColumn(ColumnName = "FWORKLEVEL")]
    public int Fworklevel { get; set; }

    /// <summary>
    /// 设备纬度等级
    /// </summary>
    [SugarColumn(ColumnName = "FEQULEVEL")]
    public int Fequlevel { get; set; }

    /// <summary>
    /// 工艺维度等级
    /// </summary>
    [SugarColumn(ColumnName = "FROUTELEVEL")]
    public int Froutelevel { get; set; }

    /// <summary>
    /// 培训日期
    /// </summary>
    [SugarColumn(ColumnName = "FTEACHDATE")]
    public DateTime? Fteachdate { get; set; }

    /// <summary>
    /// 讲师内码
    /// </summary>
    [SugarColumn(ColumnName = "FTEACHERID")]
    public string Fteacherid { get; set; } = string.Empty;

    /// <summary>
    /// 工艺技能内码
    /// </summary>
    [SugarColumn(ColumnName = "FSKILLID")]
    public string Fskillid { get; set; } = string.Empty;

    /// <summary>
    /// 加工纬度初始值
    /// </summary>
    [SugarColumn(ColumnName = "FWORKINITOUTPUT")]
    public int Fworkinitoutput { get; set; }

    /// <summary>
    /// 加工纬度初始工时
    /// </summary>
    [SugarColumn(ColumnName = "FWORKINITHOUR")]
    public decimal Fworkinithour { get; set; }

    /// <summary>
    /// 加工纬度起算时间
    /// </summary>
    [SugarColumn(ColumnName = "FWORKINITDATE")]
    public DateTime? Fworkinitdate { get; set; }

    /// <summary>
    /// 设备纬度初始值
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPINITOUTPUT")]
    public int Fequipinitoutput { get; set; }

    /// <summary>
    /// 设备纬度初始工时
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPINITHOUR")]
    public decimal Fequipinithour { get; set; }

    /// <summary>
    /// 设备纬度起算时间
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPINITDATE")]
    public DateTime? Fequipinitdate { get; set; }

    /// <summary>
    /// 工艺技能纬度初始值
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEINITOUTPUT")]
    public int Frouteinitoutput { get; set; }

    /// <summary>
    /// 工艺技能纬度初始工时
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEINITHOUR")]
    public decimal Frouteinithour { get; set; }

    /// <summary>
    /// 工艺技能纬度起算时间
    /// </summary>
    [SugarColumn(ColumnName = "FROUTEINITDATE")]
    public DateTime? Frouteinitdate { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
