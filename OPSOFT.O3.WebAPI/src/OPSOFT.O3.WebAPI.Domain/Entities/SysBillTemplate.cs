using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统表单信息表
/// </summary>
[SugarTable("SYS_BILLTEMPLATE")]
public class SysBillTemplate : BaseEntity
{
    /// <summary>
    /// 表单代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 单据标题
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTITLE")]
    public string Fbilltitle { get; set; } = string.Empty;

    /// <summary>
    /// 序时簿标题
    /// </summary>
    [SugarColumn(ColumnName = "FLISTTITLE")]
    public string Flisttitle { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 表单标识字段
    /// </summary>
    [SugarColumn(ColumnName = "FDISPLAYCOLUMN")]
    public string Fdisplaycolumn { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称(第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCNAME")]
    public string Fcname { get; set; } = string.Empty;

    /// <summary>
    /// 反提交前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FRESUBMITBEFORE", IsNullable = true)]
    public string FRESUBMITBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 自定义序时簿界面
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTLISTUI", IsNullable = true)]
    public bool? FCUSTLISTUI { get; set; }

    /// <summary>
    /// 单据编号字段
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNOFILED", IsNullable = true)]
    public string FBILLNOFILED { get; set; } = string.Empty;

    /// <summary>
    /// 序时簿URL(WEB)
    /// </summary>
    [SugarColumn(ColumnName = "FWEBLIST", IsNullable = true)]
    public string FWEBLIST { get; set; } = string.Empty;

    /// <summary>
    /// 每页显示行数
    /// </summary>
    [SugarColumn(ColumnName = "FPAGESIZE", IsNullable = true)]
    public int? FPAGESIZE { get; set; }

    /// <summary>
    /// 作废前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FFAILEDBEFORE", IsNullable = true)]
    public string FFAILEDBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 显示最大行数
    /// </summary>
    [SugarColumn(ColumnName = "FMAXROW", IsNullable = true)]
    public int? FMAXROW { get; set; }

    /// <summary>
    /// 单据URL(WEB)
    /// </summary>
    [SugarColumn(ColumnName = "FWEBBILL", IsNullable = true)]
    public string FWEBBILL { get; set; } = string.Empty;

    /// <summary>
    /// 反复核后状态
    /// </summary>
    [SugarColumn(ColumnName = "FREREVIEWSTATUS", IsNullable = true)]
    public int? FREREVIEWSTATUS { get; set; }

    /// <summary>
    /// 启用组织
    /// </summary>
    [SugarColumn(ColumnName = "FISENABLEOR", IsNullable = true)]
    public bool? FISENABLEOR { get; set; }

    /// <summary>
    /// 反确认前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FRESUREBEFORE", IsNullable = true)]
    public string FRESUREBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 自定义单据界面代码
    /// </summary>
    [SugarColumn(ColumnName = "FUXMLUI", IsNullable = true)]
    public string FUXMLUI { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称(第三语言)
    /// </summary>
    [SugarColumn(ColumnName = "FENAME", IsNullable = true)]
    public string FENAME { get; set; } = string.Empty;

    /// <summary>
    /// 反结案前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FREFINALBEFORE", IsNullable = true)]
    public string FREFINALBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 分组标识
    /// </summary>
    [SugarColumn(ColumnName = "FGROUPKEY", IsNullable = true)]
    public string FGROUPKEY { get; set; } = string.Empty;

    /// <summary>
    /// 反确认后状态
    /// </summary>
    [SugarColumn(ColumnName = "FRESURESTATUS", IsNullable = true)]
    public int? FRESURESTATUS { get; set; }

    /// <summary>
    /// 序时簿和单据关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FHEADKEY", IsNullable = true)]
    public string FHEADKEY { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称(第二语言)
    /// </summary>
    [SugarColumn(ColumnName = "FTNAME", IsNullable = true)]
    public string FTNAME { get; set; } = string.Empty;

    /// <summary>
    /// 反复核前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FREREVIEWBEFORE", IsNullable = true)]
    public string FREREVIEWBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 启用审计追踪
    /// </summary>
    [SugarColumn(ColumnName = "FENABLEAUDITLOG", IsNullable = true)]
    public bool? FENABLEAUDITLOG { get; set; }

    /// <summary>
    /// 自定义单据界面
    /// </summary>
    [SugarColumn(ColumnName = "FCUSTBILLUI", IsNullable = true)]
    public bool? FCUSTBILLUI { get; set; }

    /// <summary>
    /// 程序集名称
    /// </summary>
    [SugarColumn(ColumnName = "FASNAME", IsNullable = true)]
    public string FASNAME { get; set; } = string.Empty;

    /// <summary>
    /// 反提交后状态
    /// </summary>
    [SugarColumn(ColumnName = "FRESUBMITSTATUS", IsNullable = true)]
    public int? FRESUBMITSTATUS { get; set; }

    /// <summary>
    /// 判断重复字段
    /// </summary>
    [SugarColumn(ColumnName = "FEXISTLIST", IsNullable = true)]
    public string FEXISTLIST { get; set; } = string.Empty;

    /// <summary>
    /// 自定义序时簿界面代码
    /// </summary>
    [SugarColumn(ColumnName = "FUXMLLISTUI", IsNullable = true)]
    public string FUXMLLISTUI { get; set; } = string.Empty;

    /// <summary>
    /// 复核前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FREVIEWBEFORE", IsNullable = true)]
    public string FREVIEWBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 主表内码
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEID", IsNullable = true)]
    public string FTABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 单据界面代码
    /// </summary>
    [SugarColumn(ColumnName = "FXMLUI", IsNullable = true)]
    public string FXMLUI { get; set; } = string.Empty;

    /// <summary>
    /// 系统单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FSYSBILLTYPE", IsNullable = true)]
    public string FSYSBILLTYPE { get; set; } = string.Empty;

    /// <summary>
    /// 确认前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FSUREBEFORE", IsNullable = true)]
    public string FSUREBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 结案前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FFINALBEFORE", IsNullable = true)]
    public string FFINALBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 反作废后状态
    /// </summary>
    [SugarColumn(ColumnName = "FREFAILEDSTATUS", IsNullable = true)]
    public int? FREFAILEDSTATUS { get; set; }

    /// <summary>
    /// 启用分组
    /// </summary>
    [SugarColumn(ColumnName = "FISENABLEGROUP", IsNullable = true)]
    public bool? FISENABLEGROUP { get; set; }

    /// <summary>
    /// 启用电子签
    /// </summary>
    [SugarColumn(ColumnName = "FENABLESIGNATURE", IsNullable = true)]
    public bool? FENABLESIGNATURE { get; set; }

    /// <summary>
    /// 反关闭后状态
    /// </summary>
    [SugarColumn(ColumnName = "FRECLOSESTATUS", IsNullable = true)]
    public int? FRECLOSESTATUS { get; set; }

    /// <summary>
    /// 单据类
    /// </summary>
    [SugarColumn(ColumnName = "FASBILLNAME", IsNullable = true)]
    public string FASBILLNAME { get; set; } = string.Empty;

    /// <summary>
    /// 反审核前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FREVERIFYBEFORE", IsNullable = true)]
    public string FREVERIFYBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 反作废前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FREFAILEDBEFORE", IsNullable = true)]
    public string FREFAILEDBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 审核前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FVERIFYBEFORE", IsNullable = true)]
    public string FVERIFYBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 反审核后状态
    /// </summary>
    [SugarColumn(ColumnName = "FREVERIFYSTATUS", IsNullable = true)]
    public int? FREVERIFYSTATUS { get; set; }

    /// <summary>
    /// 反关闭前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FRECLOSEBEFORE", IsNullable = true)]
    public string FRECLOSEBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 序时簿类
    /// </summary>
    [SugarColumn(ColumnName = "FASLISTNAME", IsNullable = true)]
    public string FASLISTNAME { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用过滤
    /// </summary>
    [SugarColumn(ColumnName = "FISFILTERABLE", IsNullable = true)]
    public bool? FISFILTERABLE { get; set; }

    /// <summary>
    /// 提交前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FSUBMITBEFORE", IsNullable = true)]
    public string FSUBMITBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 图片
    /// </summary>
    [SugarColumn(ColumnName = "FIMAGE", IsNullable = true)]
    public byte[] FIMAGE { get; set; }

    /// <summary>
    /// 关闭前置状态
    /// </summary>
    [SugarColumn(ColumnName = "FCLOSEBEFORE", IsNullable = true)]
    public string FCLOSEBEFORE { get; set; } = string.Empty;

    /// <summary>
    /// 序时簿界面代码
    /// </summary>
    [SugarColumn(ColumnName = "FXMLLISTUI", IsNullable = true)]
    public string FXMLLISTUI { get; set; } = string.Empty;

    /// <summary>
    /// 反结案后状态
    /// </summary>
    [SugarColumn(ColumnName = "FREFINALSTATUS", IsNullable = true)]
    public int? FREFINALSTATUS { get; set; }
}
