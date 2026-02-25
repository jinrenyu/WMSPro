using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// ERP同步日志表
/// </summary>
[SugarTable("T_BD_SYNLOG")]
public class TBdSynlog : BaseEntity
{
    /// <summary>
    /// 接口统一编码
    /// </summary>
    [SugarColumn(ColumnName = "FINTERNO")]
    public string Finterno { get; set; } = string.Empty;

    /// <summary>
    /// 接口名称
    /// </summary>
    [SugarColumn(ColumnName = "FINTERNAME")]
    public string Fintername { get; set; } = string.Empty;

    /// <summary>
    /// 同步/异步
    /// </summary>
    [SugarColumn(ColumnName = "FISASYNC")]
    public int Fisasync { get; set; }

    /// <summary>
    /// 发送时间
    /// </summary>
    [SugarColumn(ColumnName = "FSENDTIME")]
    public DateTime? Fsendtime { get; set; }

    /// <summary>
    /// 接收时间
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVETIME")]
    public DateTime? Freceivetime { get; set; }

    /// <summary>
    /// 关键字
    /// </summary>
    [SugarColumn(ColumnName = "FFORMID")]
    public string Fformid { get; set; } = string.Empty;

    /// <summary>
    /// 系统发送数据
    /// </summary>
    [SugarColumn(ColumnName = "FSENDDATA")]
    public string Fsenddata { get; set; } = string.Empty;

    /// <summary>
    /// 系统接收数据
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEDATA")]
    public string Freceivedata { get; set; } = string.Empty;

    /// <summary>
    /// 是否成功
    /// </summary>
    [SugarColumn(ColumnName = "FISSUCCESS")]
    public bool Fissuccess { get; set; }

    /// <summary>
    /// 处理次数
    /// </summary>
    [SugarColumn(ColumnName = "FDEALTIMES")]
    public int Fdealtimes { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FMESSAGE")]
    public string Fmessage { get; set; } = string.Empty;

    /// <summary>
    /// 系统统一内码
    /// </summary>
    [SugarColumn(ColumnName = "FGUID")]
    public string Fguid { get; set; } = string.Empty;
}
