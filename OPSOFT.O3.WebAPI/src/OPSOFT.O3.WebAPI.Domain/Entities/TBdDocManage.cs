using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ESOP文件管理
/// </summary>
[SugarTable("T_BD_DOCMANAGE")]
public class TBdDocmanage : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 文件名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 作者
    /// </summary>
    [SugarColumn(ColumnName = "FAUTHOR")]
    public string Fauthor { get; set; } = string.Empty;

    /// <summary>
    /// 文档类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string Ftype { get; set; } = string.Empty;

    /// <summary>
    /// sheet 用,隔开
    /// </summary>
    [SugarColumn(ColumnName = "FSHEETS")]
    public string Fsheets { get; set; } = string.Empty;

    /// <summary>
    /// 使用状态
    /// </summary>
    [SugarColumn(ColumnName = "FUSESTATUS")]
    public bool Fusestatus { get; set; }

    /// <summary>
    /// 是否过期
    /// </summary>
    [SugarColumn(ColumnName = "FISTIMEOUT")]
    public bool Fistimeout { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 文档地址
    /// </summary>
    [SugarColumn(ColumnName = "FDOCADDRESS")]
    public string Fdocaddress { get; set; } = string.Empty;

    /// <summary>
    /// 页码数量
    /// </summary>
    [SugarColumn(ColumnName = "FPAGENUMBER")]
    public int Fpagenumber { get; set; }

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 确认人
    /// </summary>
    [SugarColumn(ColumnName = "FSUREID")]
    public string Fsureid { get; set; } = string.Empty;

    /// <summary>
    /// 确认日期
    /// </summary>
    [SugarColumn(ColumnName = "FSUREDATE")]
    public DateTime? Fsuredate { get; set; }

    /// <summary>
    /// 复核人
    /// </summary>
    [SugarColumn(ColumnName = "FREVIEWID")]
    public string Freviewid { get; set; } = string.Empty;

    /// <summary>
    /// 复核日期
    /// </summary>
    [SugarColumn(ColumnName = "FREVIEWDATE")]
    public DateTime? Freviewdate { get; set; }

    /// <summary>
    /// 文档有效期
    /// </summary>
    [SugarColumn(ColumnName = "FDEADTIME")]
    public DateTime? Fdeadtime { get; set; }

    /// <summary>
    /// 文档开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME")]
    public DateTime? Fstarttime { get; set; }

    /// <summary>
    /// 文档的识别码
    /// </summary>
    [SugarColumn(ColumnName = "FDOCID")]
    public string Fdocid { get; set; } = string.Empty;

    /// <summary>
    /// 文件类型
    /// </summary>
    [SugarColumn(ColumnName = "FMOLD")]
    public int Fmold { get; set; }

    /// <summary>
    /// 提交意见
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITMESS")]
    public string Fsubmitmess { get; set; } = string.Empty;

    /// <summary>
    /// 确认意见
    /// </summary>
    [SugarColumn(ColumnName = "FSUREMESS")]
    public string Fsuremess { get; set; } = string.Empty;

    /// <summary>
    /// 审核意见
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFYMESS")]
    public string Fverifymess { get; set; } = string.Empty;

    /// <summary>
    /// 复核意见
    /// </summary>
    [SugarColumn(ColumnName = "FREVIEWMESS")]
    public string Freviewmess { get; set; } = string.Empty;

    /// <summary>
    /// 版本号
    /// </summary>
    [SugarColumn(ColumnName = "FVERSION")]
    public string Fversion { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFYID")]
    public string Fverifyid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFYDATE")]
    public DateTime? Fverifydate { get; set; }

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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
