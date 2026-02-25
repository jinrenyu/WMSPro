using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 设备入账
/// </summary>
[SugarTable("T_ENG_EQBOOKED")]
public class TEngEqbooked : BaseEntity
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
    /// 安装位置
    /// </summary>
    [SugarColumn(ColumnName = "FPLACE")]
    public string Fplace { get; set; } = string.Empty;

    /// <summary>
    /// 部门/位置
    /// </summary>
    [SugarColumn(ColumnName = "FDEPID")]
    public string Fdepid { get; set; } = string.Empty;

    /// <summary>
    /// 进厂日期
    /// </summary>
    [SugarColumn(ColumnName = "FINDATE")]
    public DateTime? Findate { get; set; }

    /// <summary>
    /// 生产厂商内码
    /// </summary>
    [SugarColumn(ColumnName = "FMASUPPLYID")]
    public string Fmasupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 主要·技术参数
    /// </summary>
    [SugarColumn(ColumnName = "FSPECIFICATION")]
    public string Fspecification { get; set; } = string.Empty;

    /// <summary>
    /// 随机附件
    /// </summary>
    [SugarColumn(ColumnName = "FATTACHMENT")]
    public string Fattachment { get; set; } = string.Empty;

    /// <summary>
    /// 随机资料
    /// </summary>
    [SugarColumn(ColumnName = "FDATA")]
    public string Fdata { get; set; } = string.Empty;

    /// <summary>
    /// 设备安装及调试情况
    /// </summary>
    [SugarColumn(ColumnName = "FINSTALLATION")]
    public string Finstallation { get; set; } = string.Empty;

    /// <summary>
    /// 设备验收结论
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTANCE")]
    public string Facceptance { get; set; } = string.Empty;

    /// <summary>
    /// 资金来源
    /// </summary>
    [SugarColumn(ColumnName = "FFUNDSFROM")]
    public string Ffundsfrom { get; set; } = string.Empty;

    /// <summary>
    /// 移交单位
    /// </summary>
    [SugarColumn(ColumnName = "FMOVEDEPID")]
    public string Fmovedepid { get; set; } = string.Empty;

    /// <summary>
    /// 类别编号
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public string Ftypeid { get; set; } = string.Empty;

    /// <summary>
    /// 使用单位
    /// </summary>
    [SugarColumn(ColumnName = "FUSEDEPID")]
    public string Fusedepid { get; set; } = string.Empty;

    /// <summary>
    /// 检验单位
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDEPID")]
    public string Fcheckdepid { get; set; } = string.Empty;

    /// <summary>
    /// 设备能源处
    /// </summary>
    [SugarColumn(ColumnName = "FPOWERID")]
    public string Fpowerid { get; set; } = string.Empty;

    /// <summary>
    /// 相关专业部门
    /// </summary>
    [SugarColumn(ColumnName = "FMAJORDEPID")]
    public string Fmajordepid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FNOTE")]
    public string Fnote { get; set; } = string.Empty;

    /// <summary>
    /// 设备费
    /// </summary>
    [SugarColumn(ColumnName = "FINSTALLATIONCOST")]
    public decimal Finstallationcost { get; set; }

    /// <summary>
    /// 运杂费
    /// </summary>
    [SugarColumn(ColumnName = "FTRANSPOTCOST")]
    public decimal Ftranspotcost { get; set; }

    /// <summary>
    /// 包装费
    /// </summary>
    [SugarColumn(ColumnName = "FPACKCOST")]
    public decimal Fpackcost { get; set; }

    /// <summary>
    /// 安装费
    /// </summary>
    [SugarColumn(ColumnName = "FINSTALLCOST")]
    public decimal Finstallcost { get; set; }

    /// <summary>
    /// 管理费
    /// </summary>
    [SugarColumn(ColumnName = "FMANAGECOST")]
    public decimal Fmanagecost { get; set; }

    /// <summary>
    /// 其他
    /// </summary>
    [SugarColumn(ColumnName = "FOTHERCOST")]
    public decimal Fothercost { get; set; }

    /// <summary>
    /// 开箱日期
    /// </summary>
    [SugarColumn(ColumnName = "FOUTDATE")]
    public DateTime? Foutdate { get; set; }

    /// <summary>
    /// 立项日期
    /// </summary>
    [SugarColumn(ColumnName = "FPRODATE")]
    public DateTime? Fprodate { get; set; }

    /// <summary>
    /// 安装日期
    /// </summary>
    [SugarColumn(ColumnName = "FINSTALLATIONDATE")]
    public DateTime? Finstallationdate { get; set; }

    /// <summary>
    /// 设备安装内码
    /// </summary>
    [SugarColumn(ColumnName = "FINSTALLID")]
    public string Finstallid { get; set; } = string.Empty;

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

    /// <summary>
    /// 单据编号
    /// </summary>
    [SugarColumn(ColumnName = "FBILLNO")]
    public string Fbillno { get; set; } = string.Empty;
}
