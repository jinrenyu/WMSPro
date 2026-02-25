using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 送样单明细
/// </summary>
[SugarTable("ODK_SRM_SampleDeliveryEntry")]
public class OdkSrmSampleDeliveryEntry : BaseEntity
{
    /// <summary>
    /// 样品单据内码
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEINTERID")]
    public string Fsampleinterid { get; set; } = string.Empty;

    /// <summary>
    /// 样品行内码
    /// </summary>
    [SugarColumn(ColumnName = "FSAMPLEDETAILID")]
    public string Fsampledetailid { get; set; } = string.Empty;

    /// <summary>
    /// 物料内码
    /// </summary>
    [SugarColumn(ColumnName = "FITEMID")]
    public string Fitemid { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 申请数量
    /// </summary>
    [SugarColumn(ColumnName = "FREQUIREQTY")]
    public decimal Frequireqty { get; set; }

    /// <summary>
    /// 需求日期
    /// </summary>
    [SugarColumn(ColumnName = "FREQUIREDATE")]
    public DateTime? Frequiredate { get; set; }

    /// <summary>
    /// 送样数量
    /// </summary>
    [SugarColumn(ColumnName = "FDELIVERYQTY")]
    public decimal Fdeliveryqty { get; set; }

    /// <summary>
    /// 免费标识
    /// </summary>
    [SugarColumn(ColumnName = "FISFREE")]
    public bool Fisfree { get; set; }

    /// <summary>
    /// 通过标识
    /// </summary>
    [SugarColumn(ColumnName = "FISPASS")]
    public int Fispass { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    [SugarColumn(ColumnName = "FAPPENDIX")]
    public string Fappendix { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIBE")]
    public string Fdescribe { get; set; } = string.Empty;

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
