using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 拣货任务表头
/// </summary>
[SugarTable("T_STK_PICKTAST")]
public class TStkPicktast : BaseEntity
{
    /// <summary>
    /// 单据类型
    /// </summary>
    [SugarColumn(ColumnName = "FBILLTYPE")]
    public int Fbilltype { get; set; }

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 任务状态
    /// </summary>
    [SugarColumn(ColumnName = "FTASKSTATUS")]
    public int Ftaskstatus { get; set; }

    /// <summary>
    /// 拣货方案
    /// </summary>
    [SugarColumn(ColumnName = "FPLANID")]
    public string Fplanid { get; set; } = string.Empty;

    /// <summary>
    /// 是否分播
    /// </summary>
    [SugarColumn(ColumnName = "FISPOINTON")]
    public bool Fispointon { get; set; }

    /// <summary>
    /// 是否急单
    /// </summary>
    [SugarColumn(ColumnName = "FISURGENCY")]
    public bool Fisurgency { get; set; }

    /// <summary>
    /// 分播方式
    /// </summary>
    [SugarColumn(ColumnName = "FPOINTONTYPE")]
    public int Fpointontype { get; set; }

    /// <summary>
    /// 订单数
    /// </summary>
    [SugarColumn(ColumnName = "FORDERAMOUNT")]
    public int Forderamount { get; set; }

    /// <summary>
    /// 物料行数
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIALAMOUNT")]
    public int Fmaterialamount { get; set; }

    /// <summary>
    /// 总拣货数量
    /// </summary>
    [SugarColumn(ColumnName = "FPICKAMOUNT")]
    public decimal Fpickamount { get; set; }

    /// <summary>
    /// 使用到的仓库名
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKNAEM")]
    public string Fstocknaem { get; set; } = string.Empty;

    /// <summary>
    /// 最早发货时间
    /// </summary>
    [SugarColumn(ColumnName = "FEARLIESTTIME")]
    public DateTime? Fearliesttime { get; set; }

    /// <summary>
    /// 拣货员
    /// </summary>
    [SugarColumn(ColumnName = "FPICKER")]
    public string Fpicker { get; set; } = string.Empty;

    /// <summary>
    /// 拣货容器号
    /// </summary>
    [SugarColumn(ColumnName = "FPICKCONTAINERNO")]
    public string Fpickcontainerno { get; set; } = string.Empty;

    /// <summary>
    /// 拣货开始时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTARTTIME")]
    public DateTime? Fstarttime { get; set; }

    /// <summary>
    /// 拣货结束时间
    /// </summary>
    [SugarColumn(ColumnName = "FENDTIME")]
    public DateTime? Fendtime { get; set; }

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
