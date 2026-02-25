using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 拣货方案表体
/// </summary>
[SugarTable("T_STK_PICKINGPLANENTRY")]
public class TStkPickingplanentry : BaseEntity
{
    /// <summary>
    /// 表体类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public int Ftype { get; set; }

    /// <summary>
    /// 字段类型
    /// </summary>
    [SugarColumn(ColumnName = "FFIELDTYPE")]
    public int Ffieldtype { get; set; }

    /// <summary>
    /// 排序模式
    /// </summary>
    [SugarColumn(ColumnName = "FSORTMODE")]
    public string Fsortmode { get; set; } = string.Empty;

    /// <summary>
    /// 排序固定值
    /// </summary>
    [SugarColumn(ColumnName = "FFIXVALUE")]
    public string Ffixvalue { get; set; } = string.Empty;

    /// <summary>
    /// 过滤类型
    /// </summary>
    [SugarColumn(ColumnName = "FFILTERTYPE")]
    public int Ffiltertype { get; set; }

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FLOT")]
    public string Flot { get; set; } = string.Empty;

    /// <summary>
    /// 辅助属性内码
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 保质期从
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATEFROM")]
    public DateTime? Fkfdatefrom { get; set; }

    /// <summary>
    /// 保质期至
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATETO")]
    public DateTime? Fkfdateto { get; set; }

    /// <summary>
    /// 入库日期
    /// </summary>
    [SugarColumn(ColumnName = "FINSTOCKDATE")]
    public DateTime? Finstockdate { get; set; }

    /// <summary>
    /// 供应商内码
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

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
