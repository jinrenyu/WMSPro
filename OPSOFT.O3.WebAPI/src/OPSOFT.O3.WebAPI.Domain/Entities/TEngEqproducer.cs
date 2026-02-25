using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备生产厂商
/// </summary>
[SugarTable("T_ENG_EQPRODUCER")]
public class TEngEqproducer : BaseEntity
{
    /// <summary>
    /// 代号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 公司传真
    /// </summary>
    [SugarColumn(ColumnName = "FFAX")]
    public string Ffax { get; set; } = string.Empty;

    /// <summary>
    /// 公司网站
    /// </summary>
    [SugarColumn(ColumnName = "FWEBSITE")]
    public string Fwebsite { get; set; } = string.Empty;

    /// <summary>
    /// 公司电话
    /// </summary>
    [SugarColumn(ColumnName = "FPHONE")]
    public string Fphone { get; set; } = string.Empty;

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACTS")]
    public string Fcontacts { get; set; } = string.Empty;

    /// <summary>
    /// 联系人职务
    /// </summary>
    [SugarColumn(ColumnName = "FCOPOST")]
    public string Fcopost { get; set; } = string.Empty;

    /// <summary>
    /// 联系人邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FCOEMAIL")]
    public string Fcoemail { get; set; } = string.Empty;

    /// <summary>
    /// 公司邮箱
    /// </summary>
    [SugarColumn(ColumnName = "FEMAIL")]
    public string Femail { get; set; } = string.Empty;

    /// <summary>
    /// 联系人电话
    /// </summary>
    [SugarColumn(ColumnName = "FCOPHONE")]
    public string Fcophone { get; set; } = string.Empty;

    /// <summary>
    /// 公司地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string Faddress { get; set; } = string.Empty;

    /// <summary>
    /// 联系人手机
    /// </summary>
    [SugarColumn(ColumnName = "FCOTEL")]
    public string Fcotel { get; set; } = string.Empty;

    /// <summary>
    /// 邮政编码
    /// </summary>
    [SugarColumn(ColumnName = "FPOSTCODE")]
    public string Fpostcode { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }
}
