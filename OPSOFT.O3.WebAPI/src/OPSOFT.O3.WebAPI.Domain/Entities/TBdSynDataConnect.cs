using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 数据库配置
/// </summary>
[SugarTable("T_BD_SynDataConnect")]
public class TBdSynDataConnect : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 同步模式(1=时间戳，2=时间异动表)
    /// </summary>
    [SugarColumn(ColumnName = "FSYNTRANTYPE")]
    public int FSynTranType { get; set; }

    /// <summary>
    /// 源数据库类型  SQLServer;Oracle
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATABASETYPE")]
    public string FSrcDatabaseType { get; set; } = string.Empty;

    /// <summary>
    /// 源数据库版本
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATABASEVERSION")]
    public string FSrcDatabaseVersion { get; set; } = string.Empty;

    /// <summary>
    /// 源数据库服务器
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATABASESERVER")]
    public string FSrcDatabaseServer { get; set; } = string.Empty;

    /// <summary>
    /// 源数据库端口
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATABASEPORT")]
    public int FSrcDatabasePort { get; set; }

    /// <summary>
    /// 源数据库用户名
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATABASEUSER")]
    public string FSrcDatabaseUser { get; set; } = string.Empty;

    /// <summary>
    /// 源数据库密码
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATABASEPASSWORD")]
    public string FSrcDatabasePassword { get; set; } = string.Empty;

    /// <summary>
    /// 源数据库实体
    /// </summary>
    [SugarColumn(ColumnName = "FSRCDATABASENAME")]
    public string FSrcDatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 目标数据库类型  SQLServer;Oracle
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATABASETYPE")]
    public string FAimDatabaseType { get; set; } = string.Empty;

    /// <summary>
    /// 目标数据库版本
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATABASEVERSION")]
    public string FAimDatabaseVersion { get; set; } = string.Empty;

    /// <summary>
    /// 目标数据库服务器
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATABASESERVER")]
    public string FAimDatabaseServer { get; set; } = string.Empty;

    /// <summary>
    /// 目标数据库端口
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATABASEPORT")]
    public int FAimDatabasePort { get; set; }

    /// <summary>
    /// 目标数据库用户名
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATABASEUSER")]
    public string FAimDatabaseUser { get; set; } = string.Empty;

    /// <summary>
    /// 目标数据库密码
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATABASEPASSWORD")]
    public string FAimDatabasePassword { get; set; } = string.Empty;

    /// <summary>
    /// 目标数据库实体
    /// </summary>
    [SugarColumn(ColumnName = "FAIMDATABASENAME")]
    public string FAimDatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// ODK数据库 S=源数据库 A=目标数据库
    /// </summary>
    [SugarColumn(ColumnName = "FODKDB")]
    public string FOdkDb { get; set; } = string.Empty;

    /// <summary>
    /// 软件版本
    /// </summary>
    [SugarColumn(ColumnName = "FSOFTWAREID")]
    public string FSoftwareId { get; set; } = string.Empty;

    /// <summary>
    /// 审核级别
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKLEVEL")]
    public int FCheckLevel { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? FCheckDate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string FDisableId { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? FDisableDate { get; set; }
}
