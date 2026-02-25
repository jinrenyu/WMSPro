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
}
