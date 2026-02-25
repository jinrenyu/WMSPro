using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 工序汇报表体ENTRY的表体-工序投料信息
/// </summary>
[SugarTable("T_SFC_REPORTENTRY3")]
public class TSfcReportentry3 : BaseEntity
{
    /// <summary>
    /// 项次(因为批量条码时, 一个条码会投入多次
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public int Fseq { get; set; }
}
