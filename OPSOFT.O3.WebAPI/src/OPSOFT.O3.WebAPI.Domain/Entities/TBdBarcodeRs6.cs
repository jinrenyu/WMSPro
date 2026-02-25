using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 条码主档-瓣叶信息表
/// </summary>
[SugarTable("T_BD_BARCODERS6")]
public class TBdBarcoders6 : BaseEntity
{
    /// <summary>
    /// 条码
    /// </summary>
    [SugarColumn(ColumnName = "FBARCODE")]
    public string Fbarcode { get; set; } = string.Empty;

    /// <summary>
    /// 供应商-瓣叶
    /// </summary>
    [SugarColumn(ColumnName = "FLEAFSUPPLYID")]
    public string Fleafsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 厚度
    /// </summary>
    [SugarColumn(ColumnName = "FLAND")]
    public decimal Fland { get; set; }

    /// <summary>
    /// 柔软度
    /// </summary>
    [SugarColumn(ColumnName = "FSOFTNESS")]
    public decimal Fsoftness { get; set; }

    /// <summary>
    /// 弹性
    /// </summary>
    [SugarColumn(ColumnName = "FSTRESS")]
    public string Fstress { get; set; } = string.Empty;

    /// <summary>
    /// 等级
    /// </summary>
    [SugarColumn(ColumnName = "FGRADE")]
    public string Fgrade { get; set; } = string.Empty;

    /// <summary>
    /// 入库时间
    /// </summary>
    [SugarColumn(ColumnName = "FINDATE")]
    public DateTime? Findate { get; set; }

    /// <summary>
    /// 检验模式
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKTYPE")]
    public string Fchecktype { get; set; } = string.Empty;

    /// <summary>
    /// 是否匹配锁定
    /// </summary>
    [SugarColumn(ColumnName = "FISMATCHLOCK")]
    public bool Fismatchlock { get; set; }
}
