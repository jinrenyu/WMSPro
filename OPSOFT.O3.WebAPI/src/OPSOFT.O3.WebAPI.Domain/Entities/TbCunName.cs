using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 存储字段名
/// </summary>
[SugarTable("TB_CUNNAME")]
public class TbCunName : BaseEntity
{
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
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
