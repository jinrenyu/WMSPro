using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 表单数据源字段信息
/// </summary>
[SugarTable("SYS_BILLTEMPLATEENTRY2")]
public class SysBilltemplateentry2 : BaseEntity
{
    /// <summary>
    /// 关系内码
    /// </summary>
    [SugarColumn(ColumnName = "FREID")]
    public string Freid { get; set; } = string.Empty;

    /// <summary>
    /// 字段栏位
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDNAME")]
    public string Ffieldname { get; set; } = string.Empty;

    /// <summary>
    /// 字段别名
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDNAMEAS")]
    public string Ffieldnameas { get; set; } = string.Empty;

    /// <summary>
    /// 栏位说明
    /// </summary>
    [SugarColumn(ColumnName = "FHEADCAPTION")]
    public string Fheadcaption { get; set; } = string.Empty;

    /// <summary>
    /// 显示顺序
    /// </summary>
    [SugarColumn(ColumnName = "FSHOWINDEX")]
    public int Fshowindex { get; set; }

    /// <summary>
    /// 字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDTYPE")]
    public string Ffieldtype { get; set; } = string.Empty;

    /// <summary>
    /// 开窗时显示参考字段
    /// </summary>
    [SugarColumn(ColumnName = "FDISPLAYNAME")]
    public string Fdisplayname { get; set; } = string.Empty;

    /// <summary>
    /// 第一语言
    /// </summary>
    [SugarColumn(ColumnName = "FCAPTION_CHS")]
    public string FcaptionChs { get; set; } = string.Empty;

    /// <summary>
    /// 第二语言
    /// </summary>
    [SugarColumn(ColumnName = "FCAPTION_CHT")]
    public string FcaptionCht { get; set; } = string.Empty;

    /// <summary>
    /// 第三语言
    /// </summary>
    [SugarColumn(ColumnName = "FCAPTION_EN")]
    public string FcaptionEn { get; set; } = string.Empty;

    /// <summary>
    /// 控件类型
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPCTLTYPE")]
    public string Flookupctltype { get; set; } = string.Empty;

    /// <summary>
    /// 开窗表单编号
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPCLASSID")]
    public string Flookupclassid { get; set; } = string.Empty;

    /// <summary>
    /// 开窗表单数据源类型
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPTYPE")]
    public string Flookuptype { get; set; } = string.Empty;

    /// <summary>
    /// 开窗数据来源
    /// </summary>
    [SugarColumn(ColumnName = "FLOOKUPLIST")]
    public string Flookuplist { get; set; } = string.Empty;

    /// <summary>
    /// 显示宽度
    /// </summary>
    [SugarColumn(ColumnName = "FWIDTH")]
    public int Fwidth { get; set; }

    /// <summary>
    /// 显示高度
    /// </summary>
    [SugarColumn(ColumnName = "FHEIGHT")]
    public int Fheight { get; set; }

    /// <summary>
    /// 是否排序
    /// </summary>
    [SugarColumn(ColumnName = "FISSORT")]
    public bool Fissort { get; set; }

    /// <summary>
    /// 是否过滤
    /// </summary>
    [SugarColumn(ColumnName = "FISFILTER")]
    public bool Fisfilter { get; set; }

    /// <summary>
    /// 是否合计
    /// </summary>
    [SugarColumn(ColumnName = "FISSUM")]
    public bool Fissum { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    [SugarColumn(ColumnName = "FISVISIBLE")]
    public bool Fisvisible { get; set; }

    /// <summary>
    /// 对齐方式
    /// </summary>
    [SugarColumn(ColumnName = "FTEXTALIGNMENT")]
    public string Ftextalignment { get; set; } = string.Empty;

    /// <summary>
    /// 显示格式
    /// </summary>
    [SugarColumn(ColumnName = "FFORMAT")]
    public string Fformat { get; set; } = string.Empty;

    /// <summary>
    /// 允许回车
    /// </summary>
    [SugarColumn(ColumnName = "FMUSTINPUT")]
    public bool Fmustinput { get; set; }

    /// <summary>
    /// 是否必录
    /// </summary>
    [SugarColumn(ColumnName = "FCANTBLANK")]
    public bool Fcantblank { get; set; }

    /// <summary>
    /// 开窗时需要赋值的字段
    /// </summary>
    [SugarColumn(ColumnName = "FWINSETVAR")]
    public string Fwinsetvar { get; set; } = string.Empty;

    /// <summary>
    /// 分组KEY
    /// </summary>
    [SugarColumn(ColumnName = "FGROUPKEY")]
    public string Fgroupkey { get; set; } = string.Empty;

    /// <summary>
    /// 验证脚本
    /// </summary>
    [SugarColumn(ColumnName = "FVALIDATESCRIPT")]
    public string Fvalidatescript { get; set; } = string.Empty;

    /// <summary>
    /// 给值操作
    /// </summary>
    [SugarColumn(ColumnName = "FEXSCRIPT")]
    public string Fexscript { get; set; } = string.Empty;

    /// <summary>
    /// 背景颜色字段
    /// </summary>
    [SugarColumn(ColumnName = "FBACKGROUNDFIELD")]
    public string Fbackgroundfield { get; set; } = string.Empty;

    /// <summary>
    /// 只读控制
    /// </summary>
    [SugarColumn(ColumnName = "FKEYINTYPE")]
    public string Fkeyintype { get; set; } = string.Empty;

    /// <summary>
    /// 只读控制字段
    /// </summary>
    [SugarColumn(ColumnName = "FCTLREADONLY")]
    public string Fctlreadonly { get; set; } = string.Empty;

    /// <summary>
    /// 数据源层级
    /// </summary>
    [SugarColumn(ColumnName = "FLEV")]
    public int Flev { get; set; }

    /// <summary>
    /// 开窗主字段
    /// </summary>
    [SugarColumn(ColumnName = "FWINKEY")]
    public string Fwinkey { get; set; } = string.Empty;

    /// <summary>
    /// 开窗条件
    /// </summary>
    [SugarColumn(ColumnName = "FWINWHERE")]
    public string Fwinwhere { get; set; } = string.Empty;

    /// <summary>
    /// 开窗时允许多行
    /// </summary>
    [SugarColumn(ColumnName = "FWINMULTIPLE")]
    public bool Fwinmultiple { get; set; }

    /// <summary>
    /// 最大值
    /// </summary>
    [SugarColumn(ColumnName = "FMAXVALUE")]
    public string Fmaxvalue { get; set; } = string.Empty;

    /// <summary>
    /// 最小值
    /// </summary>
    [SugarColumn(ColumnName = "FMINVALUE")]
    public string Fminvalue { get; set; } = string.Empty;

    /// <summary>
    /// 默认值
    /// </summary>
    [SugarColumn(ColumnName = "FDEFAULTVALUE")]
    public string Fdefaultvalue { get; set; } = string.Empty;

    /// <summary>
    /// 合并列数
    /// </summary>
    [SugarColumn(ColumnName = "FCOLUMNSPAN")]
    public int Fcolumnspan { get; set; }

    /// <summary>
    /// 合并行数
    /// </summary>
    [SugarColumn(ColumnName = "FROWSPAN")]
    public int Frowspan { get; set; }

    /// <summary>
    /// 小数精度
    /// </summary>
    [SugarColumn(ColumnName = "FDECIMALDIGITS")]
    public int Fdecimaldigits { get; set; }

    /// <summary>
    /// 开窗标题
    /// </summary>
    [SugarColumn(ColumnName = "FWINTITLE")]
    public string Fwintitle { get; set; } = string.Empty;

    /// <summary>
    /// 开窗SQL
    /// </summary>
    [SugarColumn(ColumnName = "FWINSQL")]
    public string Fwinsql { get; set; } = string.Empty;

    /// <summary>
    /// 关系路径
    /// </summary>
    [SugarColumn(ColumnName = "FPATH")]
    public string Fpath { get; set; } = string.Empty;

    /// <summary>
    /// 启用审计追踪
    /// </summary>
    [SugarColumn(ColumnName = "FENABLEAUDIT")]
    public bool Fenableaudit { get; set; }

    /// <summary>
    /// 行号
    /// </summary>
    [SugarColumn(ColumnName = "FENTRYID")]
    public int Fentryid { get; set; }

    /// <summary>
    /// 父阶表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FBODYID")]
    public string Fbodyid { get; set; } = string.Empty;

    /// <summary>
    /// 表体内码
    /// </summary>
    [SugarColumn(ColumnName = "FDETAILID")]
    public string Fdetailid { get; set; } = string.Empty;
}
