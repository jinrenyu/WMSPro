using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 接口统一代码对照表
/// </summary>
[SugarTable("T_BD_SYNCODE")]
public class TBdSyncode : BaseEntity
{
    /// <summary>
    /// 接口统一代码(序号
    /// </summary>
    [SugarColumn(ColumnName = "FSERIALNO")]
    public string Fserialno { get; set; } = string.Empty;

    /// <summary>
    /// 功能名
    /// </summary>
    [SugarColumn(ColumnName = "FFUNCNAME", IsNullable = true)]
    public string FFUNCNAME { get; set; } = string.Empty;

    /// <summary>
    /// WMS系统代码
    /// </summary>
    [SugarColumn(ColumnName = "FWMSFORMID", IsNullable = true)]
    public string FWMSFORMID { get; set; } = string.Empty;

    /// <summary>
    /// 接口名称
    /// </summary>
    [SugarColumn(ColumnName = "FSYNNAME", IsNullable = true)]
    public string FSYNNAME { get; set; } = string.Empty;
}
