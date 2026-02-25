using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// BOM子集物料
/// </summary>
[SugarTable("T_BD_BOMENTRY")]
public class TBdBomentry : BaseEntity
{
    /// <summary>
    /// 项次
    /// </summary>
    [SugarColumn(ColumnName = "FREPLACEGROUP")]
    public int Freplacegroup { get; set; }

    /// <summary>
    /// 子项物料
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALID")]
    public string Fmaterialid { get; set; } = string.Empty;

    /// <summary>
    /// 子项类型
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALTYPE")]
    public string Fmaterialtype { get; set; } = string.Empty;

    /// <summary>
    /// 子项单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKLOCID")]
    public string Fstocklocid { get; set; } = string.Empty;

    /// <summary>
    /// 分子
    /// </summary>
    [SugarColumn(ColumnName = "FNUMERATOR")]
    public decimal Fnumerator { get; set; }

    /// <summary>
    /// 工序号
    /// </summary>
    [SugarColumn(ColumnName = "FOPERID")]
    public string Foperid { get; set; } = string.Empty;

    /// <summary>
    /// 分母
    /// </summary>
    [SugarColumn(ColumnName = "FDENOMINATOR")]
    public decimal Fdenominator { get; set; }

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "FBEGINDAY")]
    public DateTime? Fbeginday { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    [SugarColumn(ColumnName = "FENDDAY")]
    public DateTime? Fendday { get; set; }

    /// <summary>
    /// 变动损耗率%
    /// </summary>
    [SugarColumn(ColumnName = "FSCRAPRATE")]
    public decimal Fscraprate { get; set; }

    /// <summary>
    /// 固定损损耗
    /// </summary>
    [SugarColumn(ColumnName = "FFIXSCRAPQTY")]
    public decimal Ffixscrapqty { get; set; }

    /// <summary>
    /// 跳层
    /// </summary>
    [SugarColumn(ColumnName = "FISSKIP")]
    public bool Fisskip { get; set; }

    /// <summary>
    /// 位置号
    /// </summary>
    [SugarColumn(ColumnName = "FPOSITIONNO")]
    public string Fpositionno { get; set; } = string.Empty;

    /// <summary>
    /// 工位
    /// </summary>
    [SugarColumn(ColumnName = "FMACHINEPOS")]
    public string Fmachinepos { get; set; } = string.Empty;

    /// <summary>
    /// 提前期偏置
    /// </summary>
    [SugarColumn(ColumnName = "FOFFSETDAY")]
    public decimal Foffsetday { get; set; }

    /// <summary>
    /// 倒冲
    /// </summary>
    [SugarColumn(ColumnName = "FBACKFLUSH")]
    public bool Fbackflush { get; set; }

    /// <summary>
    /// 关键件
    /// </summary>
    [SugarColumn(ColumnName = "FISKEYITEM")]
    public bool Fiskeyitem { get; set; }

    /// <summary>
    /// 不投料
    /// </summary>
    [SugarColumn(ColumnName = "FISFEED")]
    public bool Fisfeed { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
    /// 辅助属性
    /// </summary>
    [SugarColumn(ColumnName = "FAUXPROPID")]
    public string Fauxpropid { get; set; } = string.Empty;

    /// <summary>
    /// 启用序列号
    /// </summary>
    [SugarColumn(ColumnName = "FFEEDSN")]
    public bool Ffeedsn { get; set; }

    /// <summary>
    /// 启用第三方序列号
    /// </summary>
    [SugarColumn(ColumnName = "FOTHERSN")]
    public bool Fothersn { get; set; }

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
