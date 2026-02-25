using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 安灯预警呼叫类别
/// </summary>
[SugarTable("T_AS_WARNINGCALLTYPE")]
public class TAsWarningcalltype : BaseEntity
{
    /// <summary>
    /// 代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 部门描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 按钮序号
    /// </summary>
    [SugarColumn(ColumnName = "FBTNNUMBER")]
    public int Fbtnnumber { get; set; }

    /// <summary>
    /// 播报类别
    /// </summary>
    [SugarColumn(ColumnName = "FBTYPE")]
    public int Fbtype { get; set; }

    /// <summary>
    /// 呼叫类别分类
    /// </summary>
    [SugarColumn(ColumnName = "FSTYPE")]
    public int Fstype { get; set; }

    /// <summary>
    /// 播报设备IP
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTIP")]
    public string Freportip { get; set; } = string.Empty;

    /// <summary>
    /// 播报设备Port
    /// </summary>
    [SugarColumn(ColumnName = "FREPORTPORT")]
    public string Freportport { get; set; } = string.Empty;

    /// <summary>
    /// 停工呼叫事件
    /// </summary>
    [SugarColumn(ColumnName = "FSTOPCALLID")]
    public string Fstopcallid { get; set; } = string.Empty;

    /// <summary>
    /// 换型预警提前时间
    /// </summary>
    [SugarColumn(ColumnName = "FHXTIME")]
    public decimal Fhxtime { get; set; }

    /// <summary>
    /// 报警名称
    /// </summary>
    [SugarColumn(ColumnName = "FALERTNAME")]
    public string Falertname { get; set; } = string.Empty;

    /// <summary>
    /// 报警源
    /// </summary>
    [SugarColumn(ColumnName = "FALERTSOURCE")]
    public byte[]? Falertsource { get; set; }

    /// <summary>
    /// 音调类型
    /// </summary>
    [SugarColumn(ColumnName = "FTONETYPE")]
    public int Ftonetype { get; set; }

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
    /// ERP编码
    /// </summary>
    [SugarColumn(ColumnName = "FERPNUMBER")]
    public string Ferpnumber { get; set; } = string.Empty;
}
