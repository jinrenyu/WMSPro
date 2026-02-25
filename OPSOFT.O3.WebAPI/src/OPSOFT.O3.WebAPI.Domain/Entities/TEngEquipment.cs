using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备
/// </summary>
[SugarTable("T_ENG_EQUIPMENT")]
public class TEngEquipment : BaseEntity
{
    /// <summary>
    /// 设备代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 设备名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 设备状态
    /// </summary>
    [SugarColumn(ColumnName = "FUSESTATUS")]
    public int Fusestatus { get; set; }

    /// <summary>
    /// 设备类别内码
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 启用日期
    /// </summary>
    [SugarColumn(ColumnName = "FBEGUSEDATE")]
    public DateTime? Fbegusedate { get; set; }

    /// <summary>
    /// 其它编码
    /// </summary>
    [SugarColumn(ColumnName = "FONUMBER")]
    public string Fonumber { get; set; } = string.Empty;

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
    /// 生产国别
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNTRY")]
    public string Fcountry { get; set; } = string.Empty;

    /// <summary>
    /// 出厂日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODATE")]
    public DateTime? Fprodate { get; set; }

    /// <summary>
    /// 安装日期
    /// </summary>
    [SugarColumn(ColumnName = "FSETUPDATE")]
    public DateTime? Fsetupdate { get; set; }

    /// <summary>
    /// 供应厂商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 部门/位置
    /// </summary>
    [SugarColumn(ColumnName = "FUDEPARTID")]
    public string Fudepartid { get; set; } = string.Empty;

    /// <summary>
    /// 位号/部位
    /// </summary>
    [SugarColumn(ColumnName = "FBITNUMBER")]
    public string Fbitnumber { get; set; } = string.Empty;

    /// <summary>
    /// 设备组号
    /// </summary>
    [SugarColumn(ColumnName = "FGPNUMBER")]
    public string Fgpnumber { get; set; } = string.Empty;

    /// <summary>
    /// 图号/档案号
    /// </summary>
    [SugarColumn(ColumnName = "FDRAWNUMBER")]
    public string Fdrawnumber { get; set; } = string.Empty;

    /// <summary>
    /// 设备来源
    /// </summary>
    [SugarColumn(ColumnName = "FEQSOURCE")]
    public string Feqsource { get; set; } = string.Empty;

    /// <summary>
    /// 设备存放位置
    /// </summary>
    [SugarColumn(ColumnName = "FADDID")]
    public string Faddid { get; set; } = string.Empty;

    /// <summary>
    /// 安装地点
    /// </summary>
    [SugarColumn(ColumnName = "FSETUPADD")]
    public string Fsetupadd { get; set; } = string.Empty;

    /// <summary>
    /// 父设备编号
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTID")]
    public string Fparentid { get; set; } = string.Empty;

    /// <summary>
    /// 设备原值
    /// </summary>
    [SugarColumn(ColumnName = "FORIGINALVALUE")]
    public decimal Foriginalvalue { get; set; }

    /// <summary>
    /// 重量(T
    /// </summary>
    [SugarColumn(ColumnName = "FWEIGHT")]
    public decimal Fweight { get; set; }
}
