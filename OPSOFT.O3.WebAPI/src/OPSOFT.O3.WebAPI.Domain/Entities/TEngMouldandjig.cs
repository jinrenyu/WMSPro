using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 模治具卡片
/// </summary>
[SugarTable("T_ENG_MOULDANDJIG")]
public class TEngMouldandjig : BaseEntity
{
    /// <summary>
    /// 模治具代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 模治具名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string Fname { get; set; } = string.Empty;

    /// <summary>
    /// 模具状态
    /// </summary>
    [SugarColumn(ColumnName = "FMOULDSTATUS")]
    public int Fmouldstatus { get; set; }

    /// <summary>
    /// 类别
    /// </summary>
    [SugarColumn(ColumnName = "FTYPEID")]
    public int Ftypeid { get; set; }

    /// <summary>
    /// 规格型号
    /// </summary>
    [SugarColumn(ColumnName = "FMODEL")]
    public string Fmodel { get; set; } = string.Empty;

    /// <summary>
    /// 可见性状态
    /// </summary>
    [SugarColumn(ColumnName = "FVISSTATUS")]
    public int Fvisstatus { get; set; }

    /// <summary>
    /// 币别
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCYID")]
    public string Fcurrencyid { get; set; } = string.Empty;

    /// <summary>
    /// 设定寿命
    /// </summary>
    [SugarColumn(ColumnName = "FSTDUSELIFE")]
    public int Fstduselife { get; set; }

    /// <summary>
    /// 生产厂商内码
    /// </summary>
    [SugarColumn(ColumnName = "FMASUPPLYID")]
    public string Fmasupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [SugarColumn(ColumnName = "FCOUNT")]
    public int Fcount { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    [SugarColumn(ColumnName = "FCONTACT")]
    public string Fcontact { get; set; } = string.Empty;

    /// <summary>
    /// 联系人电话
    /// </summary>
    [SugarColumn(ColumnName = "FCOPHONE")]
    public string Fcophone { get; set; } = string.Empty;

    /// <summary>
    /// 未税单价
    /// </summary>
    [SugarColumn(ColumnName = "FNOTAXPRICE")]
    public decimal Fnotaxprice { get; set; }

    /// <summary>
    /// 含税总价
    /// </summary>
    [SugarColumn(ColumnName = "FTOTTAXPRICE")]
    public decimal Ftottaxprice { get; set; }

    /// <summary>
    /// 产品内码
    /// </summary>
    [SugarColumn(ColumnName = "FPRODUCTID")]
    public string Fproductid { get; set; } = string.Empty;

    /// <summary>
    /// 仓库内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKID")]
    public string Fstockid { get; set; } = string.Empty;

    /// <summary>
    /// 仓位内码
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKPLACEID")]
    public string Fstockplaceid { get; set; } = string.Empty;

    /// <summary>
    /// 采购部门内码
    /// </summary>
    [SugarColumn(ColumnName = "FDEPARTMENTID")]
    public string Fdepartmentid { get; set; } = string.Empty;

    /// <summary>
    /// 员工内码
    /// </summary>
    [SugarColumn(ColumnName = "FEMPID")]
    public string Fempid { get; set; } = string.Empty;

    /// <summary>
    /// 采购日期
    /// </summary>
    [SugarColumn(ColumnName = "FKFDATE")]
    public DateTime? Fkfdate { get; set; }

    /// <summary>
    /// 已付金额
    /// </summary>
    [SugarColumn(ColumnName = "FAMOUNTPAID")]
    public decimal Famountpaid { get; set; }

    /// <summary>
    /// 未付金额
    /// </summary>
    [SugarColumn(ColumnName = "FOUTSTANDINGAMT")]
    public decimal Foutstandingamt { get; set; }

    /// <summary>
    /// 一出几
    /// </summary>
    [SugarColumn(ColumnName = "FNUMOFPOINTS")]
    public int Fnumofpoints { get; set; }

    /// <summary>
    /// 预计寿命
    /// </summary>
    [SugarColumn(ColumnName = "FEXPECTLIFE")]
    public int Fexpectlife { get; set; }

    /// <summary>
    /// 剩余寿命
    /// </summary>
    [SugarColumn(ColumnName = "FREMAINLIFE")]
    public int Fremainlife { get; set; }

    /// <summary>
    /// 修模增加寿命
    /// </summary>
    [SugarColumn(ColumnName = "FREPAIRADDLIFE")]
    public int Frepairaddlife { get; set; }

    /// <summary>
    /// 标准准备时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTDREADYLIFE")]
    public decimal Fstdreadylife { get; set; }

    /// <summary>
    /// 标准设置时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTDSETTIME")]
    public decimal Fstdsettime { get; set; }

    /// <summary>
    /// 标准首件时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTDFIRSTTIME")]
    public decimal Fstdfirsttime { get; set; }

    /// <summary>
    /// 标准调试时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTDDEBUGTIME")]
    public decimal Fstddebugtime { get; set; }

    /// <summary>
    /// 标准维护时间
    /// </summary>
    [SugarColumn(ColumnName = "FSTDMAINTAINTIME")]
    public decimal Fstdmaintaintime { get; set; }

    /// <summary>
    /// 标准工装寿命
    /// </summary>
    [SugarColumn(ColumnName = "FSTDTOOLLIFE")]
    public int Fstdtoollife { get; set; }

    /// <summary>
    /// 初始使用寿命
    /// </summary>
    [SugarColumn(ColumnName = "FINITUSELIFE")]
    public int Finituselife { get; set; }

    /// <summary>
    /// 已使用寿命
    /// </summary>
    [SugarColumn(ColumnName = "FCOLUSELIFE")]
    public int Fcoluselife { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [SugarColumn(ColumnName = "FUNITID")]
    public string Funitid { get; set; } = string.Empty;

    /// <summary>
    /// 领用日期
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEDATE")]
    public DateTime? Freceivedate { get; set; }

    /// <summary>
    /// 工装类型ID
    /// </summary>
    [SugarColumn(ColumnName = "FWORKTYPEID")]
    public string Fworktypeid { get; set; } = string.Empty;

    /// <summary>
    /// 起算时间
    /// </summary>
    [SugarColumn(ColumnName = "FINITDATE")]
    public DateTime? Finitdate { get; set; }

    /// <summary>
    /// 领用部门
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEDEPTID")]
    public string Freceivedeptid { get; set; } = string.Empty;

    /// <summary>
    /// 领用人
    /// </summary>
    [SugarColumn(ColumnName = "FRECEIVEEMPID")]
    public string Freceiveempid { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

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
