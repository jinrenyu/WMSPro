using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备安装
/// </summary>
[SugarTable("T_ENG_EQINSTALL")]
public class TEngEqinstall : BaseEntity
{
    /// <summary>
    /// 开箱验收单内码
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCEINTERID")]
    public string Fsourceinterid { get; set; } = string.Empty;

    /// <summary>
    /// 申请日期
    /// </summary>
    [SugarColumn(ColumnName = "FDATE")]
    public DateTime? Fdate { get; set; }

    /// <summary>
    /// 安装位置
    /// </summary>
    [SugarColumn(ColumnName = "FPLACE")]
    public string Fplace { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 设备型号
    /// </summary>
    [SugarColumn(ColumnName = "FEQMODEL")]
    public string Feqmodel { get; set; } = string.Empty;

    /// <summary>
    /// 设备规格
    /// </summary>
    [SugarColumn(ColumnName = "FEQSIZE")]
    public string Feqsize { get; set; } = string.Empty;

    /// <summary>
    /// 出厂编号
    /// </summary>
    [SugarColumn(ColumnName = "FFANUMBER")]
    public string Ffanumber { get; set; } = string.Empty;

    /// <summary>
    /// 出厂日期
    /// </summary>
    [SugarColumn(ColumnName = "FFADATE")]
    public DateTime? Ffadate { get; set; }

    /// <summary>
    /// 外形尺寸
    /// </summary>
    [SugarColumn(ColumnName = "FBODIMENSION")]
    public string Fbodimension { get; set; } = string.Empty;

    /// <summary>
    /// 重量(T
    /// </summary>
    [SugarColumn(ColumnName = "FWEIGHT")]
    public decimal Fweight { get; set; }

    /// <summary>
    /// 设备管理部门
    /// </summary>
    [SugarColumn(ColumnName = "FDEVICEDEPID", IsNullable = true)]
    public string FDEVICEDEPID { get; set; } = string.Empty;

    /// <summary>
    /// 安全管理部门
    /// </summary>
    [SugarColumn(ColumnName = "FSAFETYDEPID", IsNullable = true)]
    public string FSAFETYDEPID { get; set; } = string.Empty;

    /// <summary>
    /// 外观检查
    /// </summary>
    [SugarColumn(ColumnName = "FFACECHECK", IsNullable = true)]
    public string FFACECHECK { get; set; } = string.Empty;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID", IsNullable = true)]
    public string FCHECKERID { get; set; } = string.Empty;

    /// <summary>
    /// 验收人
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTOR", IsNullable = true)]
    public string FACCEPTOR { get; set; } = string.Empty;

    /// <summary>
    /// 使用单位
    /// </summary>
    [SugarColumn(ColumnName = "FBUILDINGDEPID", IsNullable = true)]
    public string FBUILDINGDEPID { get; set; } = string.Empty;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID", IsNullable = true)]
    public string FDISABLEID { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE", IsNullable = true)]
    public DateTime? FCHECKDATE { get; set; }

    /// <summary>
    /// 立项日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODATE", IsNullable = true)]
    public DateTime? FPRODATE { get; set; }

    /// <summary>
    /// 负荷实验
    /// </summary>
    [SugarColumn(ColumnName = "FSTRESSMRL", IsNullable = true)]
    public string FSTRESSMRL { get; set; } = string.Empty;

    /// <summary>
    /// 总评和其他
    /// </summary>
    [SugarColumn(ColumnName = "FEVALUATE", IsNullable = true)]
    public string FEVALUATE { get; set; } = string.Empty;

    /// <summary>
    /// 检验部门
    /// </summary>
    [SugarColumn(ColumnName = "FTESTDEPID", IsNullable = true)]
    public string FTESTDEPID { get; set; } = string.Empty;

    /// <summary>
    /// 精度检查
    /// </summary>
    [SugarColumn(ColumnName = "FACCURACYCHECKING", IsNullable = true)]
    public string FACCURACYCHECKING { get; set; } = string.Empty;

    /// <summary>
    /// 经办部门
    /// </summary>
    [SugarColumn(ColumnName = "FTRANSACTIONDEPID", IsNullable = true)]
    public string FTRANSACTIONDEPID { get; set; } = string.Empty;

    /// <summary>
    /// 验收日期
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTANCEDATES", IsNullable = true)]
    public DateTime? FACCEPTANCEDATES { get; set; }

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE", IsNullable = true)]
    public DateTime? FDISABLEDATE { get; set; }

    /// <summary>
    /// 生产厂商内码
    /// </summary>
    [SugarColumn(ColumnName = "FMASUPPLYID", IsNullable = true)]
    public string FMASUPPLYID { get; set; } = string.Empty;

    /// <summary>
    /// 开箱日期
    /// </summary>
    [SugarColumn(ColumnName = "FOUTDATE", IsNullable = true)]
    public DateTime? FOUTDATE { get; set; }

    /// <summary>
    /// 分管领导
    /// </summary>
    [SugarColumn(ColumnName = "FCHARGELEADERID", IsNullable = true)]
    public string FCHARGELEADERID { get; set; } = string.Empty;

    /// <summary>
    /// 安装单位
    /// </summary>
    [SugarColumn(ColumnName = "FMOUNTINGFLATID", IsNullable = true)]
    public string FMOUNTINGFLATID { get; set; } = string.Empty;

    /// <summary>
    /// 空载实验
    /// </summary>
    [SugarColumn(ColumnName = "FNOLOADTEST", IsNullable = true)]
    public string FNOLOADTEST { get; set; } = string.Empty;

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO", IsNullable = true)]
    public string FBILLNO { get; set; } = string.Empty;
}
