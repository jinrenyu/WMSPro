using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序
/// </summary>
[SugarTable("T_SFC_PROCESS")]
public class TSfcProcess : BaseEntity
{
    /// <summary>
    /// ERP工序编码
    /// </summary>
    [SugarColumn(ColumnName = "FERPCODE")]
    public string Ferpcode { get; set; } = string.Empty;

    /// <summary>
    /// K3工序代码
    /// </summary>
    [SugarColumn(ColumnName = "FK3CODE")]
    public string Fk3code { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 标准换型时间(分
    /// </summary>
    [SugarColumn(ColumnName = "FRETIME")]
    public decimal Fretime { get; set; }
}
