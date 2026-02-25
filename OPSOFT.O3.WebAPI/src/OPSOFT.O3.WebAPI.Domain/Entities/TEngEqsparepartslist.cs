using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 备件清单
/// </summary>
[SugarTable("T_ENG_EQSPAREPARTSLIST")]
public class TEngEqsparepartslist : BaseEntity
{
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// ABC类型
    /// </summary>
    [SugarColumn(ColumnName = "FABCTYPE")]
    public string Fabctype { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 位号
    /// </summary>
    [SugarColumn(ColumnName = "FBITNUMBERID")]
    public string Fbitnumberid { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 备件图号
    /// </summary>
    [SugarColumn(ColumnName = "FCHARTNUMBER")]
    public string Fchartnumber { get; set; } = string.Empty;

    /// <summary>
    /// 批号
    /// </summary>
    [SugarColumn(ColumnName = "FBACHNO")]
    public string Fbachno { get; set; } = string.Empty;

    /// <summary>
    /// 单价
    /// </summary>
    [SugarColumn(ColumnName = "FPRICE")]
    public decimal Fprice { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FNUM")]
    public int Fnum { get; set; }

    /// <summary>
    /// 在用量
    /// </summary>
    [SugarColumn(ColumnName = "FUSINGNUMBER")]
    public int Fusingnumber { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    [SugarColumn(ColumnName = "FDCSTOCKID")]
    public string Fdcstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位
    /// </summary>
    [SugarColumn(ColumnName = "FDCSPID")]
    public string Fdcspid { get; set; } = string.Empty;

    /// <summary>
    /// 原装部件
    /// </summary>
    [SugarColumn(ColumnName = "FORIGINALPARTS")]
    public string Foriginalparts { get; set; } = string.Empty;

    /// <summary>
    /// 规格
    /// </summary>
    [SugarColumn(ColumnName = "FEQSIZE")]
    public string Feqsize { get; set; } = string.Empty;

    /// <summary>
    /// 型号
    /// </summary>
    [SugarColumn(ColumnName = "FEQMODEL")]
    public string Feqmodel { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITNAME")]
    public string Funitname { get; set; } = string.Empty;

    /// <summary>
    /// 生产厂商
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTIONSUPPLIER")]
    public string Fproductionsupplier { get; set; } = string.Empty;

    /// <summary>
    /// 用途
    /// </summary>
    [SugarColumn(ColumnName = "FUSE")]
    public string Fuse { get; set; } = string.Empty;

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNIT")]
    public string Funit { get; set; } = string.Empty;

    /// <summary>
    /// 外形
    /// </summary>
    [SugarColumn(ColumnName = "FSHAPE")]
    public string Fshape { get; set; } = string.Empty;

    /// <summary>
    /// 重量
    /// </summary>
    [SugarColumn(ColumnName = "FWEIGHT")]
    public decimal Fweight { get; set; }

    /// <summary>
    /// 材质
    /// </summary>
    [SugarColumn(ColumnName = "FMATERIAL")]
    public string Fmaterial { get; set; } = string.Empty;

    /// <summary>
    /// 来源
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCE")]
    public string Fsource { get; set; } = string.Empty;

    /// <summary>
    /// 寿命
    /// </summary>
    [SugarColumn(ColumnName = "FUSETIMELIMIT")]
    public int Fusetimelimit { get; set; }

    /// <summary>
    /// 最小订货量
    /// </summary>
    [SugarColumn(ColumnName = "FMINIMUMORDERQUANTITY")]
    public decimal Fminimumorderquantity { get; set; }

    /// <summary>
    /// 采购周期
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASECYCLE")]
    public int Fpurchasecycle { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime? Fdisabledate { get; set; }

    /// <summary>
    /// 禁用人内码
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
