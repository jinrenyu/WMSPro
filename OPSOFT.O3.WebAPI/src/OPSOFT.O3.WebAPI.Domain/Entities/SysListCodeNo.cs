using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 单据编码流水号档。(规码ID, 编码标识) 唯一索引与生产建表脚本一致（SYS_LISTCODENO_1），
/// 兜底并发首插重号——取号服务 INSERT 撞索引时回退重试 UPDATE 自增路径。
/// </summary>
[SugarTable("SYS_LISTCODENO")]
[SugarIndex("SYS_LISTCODENO_1", nameof(Fclasstypeid), OrderByType.Asc, nameof(Fbillcode), OrderByType.Asc, true)]
public class SysListCodeNo : BaseEntity
{
    /// <summary>
    /// 规码ID
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSTYPEID")]
    public string Fclasstypeid { get; set; } = string.Empty;

    /// <summary>
    /// 编码标识
    /// </summary>
    [SugarColumn(ColumnName = "FBILLCODE")]
    public string Fbillcode { get; set; } = string.Empty;

    /// <summary>
    /// 目前流水号
    /// </summary>
    [SugarColumn(ColumnName = "FSEQ")]
    public int Fseq { get; set; }
}
