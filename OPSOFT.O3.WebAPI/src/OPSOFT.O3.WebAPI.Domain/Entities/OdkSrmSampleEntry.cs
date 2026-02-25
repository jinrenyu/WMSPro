using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 样品申请单明细
/// </summary>
[SugarTable("ODK_SRM_SampleEntry")]
public class OdkSrmSampleEntry : BaseEntity
{
    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 单位内码
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 申请数量
    /// </summary>
    [SugarColumn(ColumnName = "FREQUIREQTY")]
    public decimal Frequireqty { get; set; }

    /// <summary>
    /// 送样数量
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYQTY")]
    public decimal Fdeliveryqty { get; set; }

    /// <summary>
    /// 通过状态
    /// </summary>
    [SugarColumn(ColumnName = "FPASSSTATUS")]
    public int Fpassstatus { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 需求日期
    /// </summary>
    [SugarColumn(ColumnName = "FREQUIREDATE")]
    public DateTime? Frequiredate { get; set; }

    /// <summary>
    /// 合格量
    /// </summary>
    [SugarColumn(ColumnName = "FQUAQTY")]
    public decimal Fquaqty { get; set; }

    /// <summary>
    /// 不合格量
    /// </summary>
    [SugarColumn(ColumnName = "FBADQTY")]
    public decimal Fbadqty { get; set; }

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
