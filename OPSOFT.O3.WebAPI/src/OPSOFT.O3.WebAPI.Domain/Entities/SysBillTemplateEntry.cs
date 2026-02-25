using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 表单数据源信息
/// </summary>
[SugarTable("SYS_BILLTEMPLATEENTRY")]
public class SysBillTemplateEntry : BaseEntity
{
    /// <summary>
    /// 数据源类型
    /// </summary>
    [SugarColumn(ColumnName = "FSTYPE")]
    public string Fstype { get; set; } = string.Empty;

    /// <summary>
    /// 数据源名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 父阶内码
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTID")]
    public string Fparentid { get; set; } = string.Empty;

    /// <summary>
    /// 数据源标识
    /// </summary>
    [SugarColumn(ColumnName = "FKEY")]
    public string Fkey { get; set; } = string.Empty;

    /// <summary>
    /// 主表内码
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEID")]
    public string Ftableid { get; set; } = string.Empty;

    /// <summary>
    /// 数据表标识
    /// </summary>
    [SugarColumn(ColumnName = "FTABLEKEY")]
    public string Ftablekey { get; set; } = string.Empty;

    /// <summary>
    /// 是否用户定义
    /// </summary>
    [SugarColumn(ColumnName = "FISUSERDEFINED")]
    public bool Fisuserdefined { get; set; }

    /// <summary>
    /// 用户定义SQL
    /// </summary>
    [SugarColumn(ColumnName = "FUSQL")]
    public string Fusql { get; set; } = string.Empty;

    /// <summary>
    /// 系统自动SQL
    /// </summary>
    [SugarColumn(ColumnName = "FSQL")]
    public string Fsql { get; set; } = string.Empty;

    /// <summary>
    /// 条码编码字段
    /// </summary>
    [SugarColumn(ColumnName = "FBARLIST")]
    public string Fbarlist { get; set; } = string.Empty;

    /// <summary>
    /// 其他条件
    /// </summary>
    [SugarColumn(ColumnName = "FWHERE")]
    public string Fwhere { get; set; } = string.Empty;

    /// <summary>
    /// 保存时过滤条件
    /// </summary>
    [SugarColumn(ColumnName = "FSWHERE")]
    public string Fswhere { get; set; } = string.Empty;

    /// <summary>
    /// 是否抓所有资料
    /// </summary>
    [SugarColumn(ColumnName = "FISALL")]
    public bool Fisall { get; set; }

    /// <summary>
    /// 批号编码字段
    /// </summary>
    [SugarColumn(ColumnName = "FLOTLIST")]
    public string Flotlist { get; set; } = string.Empty;

    /// <summary>
    /// 排序字段
    /// </summary>
    [SugarColumn(ColumnName = "FORDERKEY")]
    public string Forderkey { get; set; } = string.Empty;

    /// <summary>
    /// 是否主数据源
    /// </summary>
    [SugarColumn(ColumnName = "FISMAIN")]
    public bool Fismain { get; set; }

    /// <summary>
    /// 是否保存
    /// </summary>
    [SugarColumn(ColumnName = "FISSAVE")]
    public bool Fissave { get; set; }

    /// <summary>
    /// 表头关系字段-表头字段
    /// </summary>
    [SugarColumn(ColumnName = "FHEADKEY")]
    public string Fheadkey { get; set; } = string.Empty;

    /// <summary>
    /// 表头关系字段-本表字段
    /// </summary>
    [SugarColumn(ColumnName = "FBODYKEY")]
    public string Fbodykey { get; set; } = string.Empty;

    /// <summary>
    /// 父阶关系字段-父阶表字段
    /// </summary>
    [SugarColumn(ColumnName = "FPARENTKEY")]
    public string Fparentkey { get; set; } = string.Empty;

    /// <summary>
    /// 父阶关系字段-本表字段
    /// </summary>
    [SugarColumn(ColumnName = "FSONKEY")]
    public string Fsonkey { get; set; } = string.Empty;

    /// <summary>
    /// 只读Python脚本
    /// </summary>
    [SugarColumn(ColumnName = "FPYTHON1")]
    public string Fpython1 { get; set; } = string.Empty;

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
