using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 记录登录者使用排程时保存的信息
/// </summary>
[SugarTable("T_SFC_LoginUserRecord")]
public class TSfcLoginUserRecord : BaseEntity
{
    /// <summary>
    /// 用户内码
    /// </summary>
    [SugarColumn(ColumnName = "USER_ID")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 排程查询条件内码
    /// </summary>
    [SugarColumn(ColumnName = "FQUERYID")]
    public string Fqueryid { get; set; } = string.Empty;

    /// <summary>
    /// 排程方案内码
    /// </summary>
    [SugarColumn(ColumnName = "FSCHEMEID")]
    public string Fschemeid { get; set; } = string.Empty;

    /// <summary>
    /// 工序流转卡内码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDID")]
    public string Fflowcardid { get; set; } = string.Empty;

    /// <summary>
    /// 生产任务单内码
    /// </summary>
    [SugarColumn(ColumnName = "FMOID")]
    public string Fmoid { get; set; } = string.Empty;

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 资源内码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCEID")]
    public string Fresourceid { get; set; } = string.Empty;

    /// <summary>
    /// 时间范围-开始
    /// </summary>
    [SugarColumn(ColumnName = "FPLANSTARTDATE")]
    public DateTime? Fplanstartdate { get; set; }

    /// <summary>
    /// 时间范围-结束
    /// </summary>
    [SugarColumn(ColumnName = "FPLANENDDATE")]
    public DateTime? Fplanenddate { get; set; }

    /// <summary>
    /// 工序流转卡编码
    /// </summary>
    [SugarColumn(ColumnName = "FFLOWCARDBILLNO")]
    public string Fflowcardbillno { get; set; } = string.Empty;

    /// <summary>
    /// 任务单编码
    /// </summary>
    [SugarColumn(ColumnName = "FMOBILLNO")]
    public string Fmobillno { get; set; } = string.Empty;

    /// <summary>
    /// 产品编码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTNUMBER")]
    public string Fproductnumber { get; set; } = string.Empty;

    /// <summary>
    /// 产品名称
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTNAME")]
    public string Fproductname { get; set; } = string.Empty;

    /// <summary>
    /// 资源编码
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCENUMBER")]
    public string Fresourcenumber { get; set; } = string.Empty;

    /// <summary>
    /// 资源名称
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCENAME")]
    public string Fresourcename { get; set; } = string.Empty;

    /// <summary>
    /// 排程数据源
    /// </summary>
    [SugarColumn(ColumnName = "FDATASOURCE")]
    public string Fdatasource { get; set; } = string.Empty;

    /// <summary>
    /// 排程所有资源
    /// </summary>
    [SugarColumn(ColumnName = "FRESOURCECOLLECTION")]
    public string Fresourcecollection { get; set; } = string.Empty;
}
