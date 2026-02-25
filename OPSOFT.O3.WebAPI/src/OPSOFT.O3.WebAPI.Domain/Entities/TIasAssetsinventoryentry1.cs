using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 资产盘点表体-差异
/// </summary>
[SugarTable("T_IAS_ASSETSINVENTORYENTRY1")]
public class TIasAssetsinventoryentry1 : BaseEntity
{
    /// <summary>
    /// 资产内码
    /// </summary>
    [SugarColumn(ColumnName = "FASSETID")]
    public string Fassetid { get; set; } = string.Empty;

    /// <summary>
    /// 帐存存放地点代码
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNTLOCATIONNUMBER")]
    public string Faccountlocationnumber { get; set; } = string.Empty;

    /// <summary>
    /// 帐存存放地点名称
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNTLOCATIONNAME")]
    public string Faccountlocationname { get; set; } = string.Empty;

    /// <summary>
    /// 帐存数量
    /// </summary>
    [SugarColumn(ColumnName = "FACCOUNTNUM")]
    public decimal Faccountnum { get; set; }

    /// <summary>
    /// 实存存放地点代码
    /// </summary>
    [SugarColumn(ColumnName = "FPRACTICALLOCATIONNUMBER")]
    public string Fpracticallocationnumber { get; set; } = string.Empty;

    /// <summary>
    /// 实存存放地点名称
    /// </summary>
    [SugarColumn(ColumnName = "FPRACTICALLOCATIONNAME")]
    public string Fpracticallocationname { get; set; } = string.Empty;

    /// <summary>
    /// 实存数量
    /// </summary>
    [SugarColumn(ColumnName = "FPRACTICALNUM")]
    public decimal Fpracticalnum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

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
