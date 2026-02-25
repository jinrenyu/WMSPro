using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 同步-数据映射表头
/// </summary>
[SugarTable("T_BD_SynDataMapping")]
public class TBdSynDataMapping : BaseEntity
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
    /// 业务单据
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPE")]
    public string FBillType { get; set; } = string.Empty;

    /// <summary>
    /// 数据库连接
    /// </summary>
    [SugarColumn(ColumnName = "FDBCONNECTID")]
    public string FDbConnectId { get; set; } = string.Empty;

    /// <summary>
    /// 主表和子表的关联字段
    /// </summary>
    [SugarColumn(ColumnName = "FRELATIONFIELD")]
    public string FRelationField { get; set; } = string.Empty;

    /// <summary>
    /// 执行周期
    /// </summary>
    [SugarColumn(ColumnName = "FEXECUTIONCYCLE")]
    public string FExecutionCycle { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string FNote { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSDTIME")]
    public DateTime? FSdTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FEDTIME")]
    public DateTime? FEdTime { get; set; }

    /// <summary>
    /// 周期 月;周;天;时;分;秒
    /// </summary>
    [SugarColumn(ColumnName = "FCYCLE")]
    public string FCycle { get; set; } = string.Empty;

    /// <summary>
    /// 频率
    /// </summary>
    [SugarColumn(ColumnName = "FREQUENCY")]
    public int Frequency { get; set; }

    /// <summary>
    /// 同步成功是否产生LOG
    /// </summary>
    [SugarColumn(ColumnName = "FISWRITELOG")]
    public bool FIsWriteLog { get; set; }

    /// <summary>
    /// 是否启用 0 =否 1=启用
    /// </summary>
    [SugarColumn(ColumnName = "FENABLED")]
    public bool FEnabled { get; set; }

    /// <summary>
    /// 数据重复是否覆盖 0 =否 1=是
    /// </summary>
    [SugarColumn(ColumnName = "FISCOVER")]
    public bool FIsCover { get; set; }

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
    /// 插件名称
    /// </summary>
    [SugarColumn(ColumnName = "FASSEMBLYNAME")]
    public string FAssemblyName { get; set; } = string.Empty;

    /// <summary>
    /// 类名称
    /// </summary>
    [SugarColumn(ColumnName = "FCLASSNAME")]
    public string FClassName { get; set; } = string.Empty;

    /// <summary>
    /// 是否Cloud系统
    /// </summary>
    [SugarColumn(ColumnName = "FISCLOUD")]
    public bool FIsCloud { get; set; }

    /// <summary>
    /// 单据类型（10：基础资料20：业务单据）
    /// </summary>
    [SugarColumn(ColumnName = "FSYNGROUP")]
    public string FSynGroup { get; set; } = string.Empty;

    /// <summary>
    /// 同步类型(在数据映射中（对应FSynTranType）)
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string FType { get; set; } = string.Empty;

    /// <summary>
    /// 是否标准
    /// </summary>
    [SugarColumn(ColumnName = "FISSTAD")]
    public bool FIsStad { get; set; }

    /// <summary>
    /// MES单据表名
    /// </summary>
    [SugarColumn(ColumnName = "FTABLENAME")]
    public string FTableName { get; set; } = string.Empty;

    /// <summary>
    /// Cloud同步表名
    /// </summary>
    [SugarColumn(ColumnName = "FCLOUDFORMID")]
    public string FCloudFormId { get; set; } = string.Empty;

    /// <summary>
    /// 数据库版本
    /// </summary>
    [SugarColumn(ColumnName = "FSOFTWAREID")]
    public string FSoftwareId { get; set; } = string.Empty;

    /// <summary>
    /// 是否保存
    /// </summary>
    [SugarColumn(ColumnName = "FISSAVE")]
    public bool FIsSave { get; set; }

    /// <summary>
    /// 差异是否删除
    /// </summary>
    [SugarColumn(ColumnName = "FISDELETE")]
    public bool FIsDelete { get; set; }

    /// <summary>
    /// 删除条件
    /// </summary>
    [SugarColumn(ColumnName = "FDELETEFILTER")]
    public string FDeleteFilter { get; set; } = string.Empty;

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
