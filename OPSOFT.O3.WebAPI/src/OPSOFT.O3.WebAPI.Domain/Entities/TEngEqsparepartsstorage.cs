using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 备件调整
/// </summary>
[SugarTable("T_ENG_EQSPAREPARTSSTORAGE")]
public class TEngEqsparepartsstorage : BaseEntity
{
    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string Fdescription { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime? Fcheckdate { get; set; }

    /// <summary>
    /// 审核人内码
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string Fcheckerid { get; set; } = string.Empty;

    /// <summary>
    /// 系统编号
    /// </summary>
    [SugarColumn(ColumnName = "FSYSTEMNO")]
    public string Fsystemno { get; set; } = string.Empty;

    /// <summary>
    /// 出入库类型
    /// </summary>
    [SugarColumn(ColumnName = "FADDSTORAGETYPEID")]
    public string Faddstoragetypeid { get; set; } = string.Empty;

    /// <summary>
    /// 出入库日期
    /// </summary>
    [SugarColumn(ColumnName = "FADDSTORAGEDATE")]
    public DateTime? Faddstoragedate { get; set; }

    /// <summary>
    /// 是否出入库
    /// </summary>
    [SugarColumn(ColumnName = "FISINANDOUT")]
    public int Fisinandout { get; set; }

    /// <summary>
    /// 入库编号
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string Fnumber { get; set; } = string.Empty;

    /// <summary>
    /// 领用部门
    /// </summary>
    [SugarColumn(ColumnName = "FRECIPIENTSDEPTID")]
    public string Frecipientsdeptid { get; set; } = string.Empty;

    /// <summary>
    /// 使用部门
    /// </summary>
    [SugarColumn(ColumnName = "FUSEDEPTID")]
    public string Fusedeptid { get; set; } = string.Empty;

    /// <summary>
    /// 批准人
    /// </summary>
    [SugarColumn(ColumnName = "FAPPROVERID")]
    public string Fapproverid { get; set; } = string.Empty;

    /// <summary>
    /// 用途
    /// </summary>
    [SugarColumn(ColumnName = "FUSE")]
    public string Fuse { get; set; } = string.Empty;

    /// <summary>
    /// 去向
    /// </summary>
    [SugarColumn(ColumnName = "FWHEREABOUTS")]
    public string Fwhereabouts { get; set; } = string.Empty;

    /// <summary>
    /// 工单
    /// </summary>
    [SugarColumn(ColumnName = "FWORKORDER")]
    public string Fworkorder { get; set; } = string.Empty;

    /// <summary>
    /// 发料人
    /// </summary>
    [SugarColumn(ColumnName = "FSENDINGPEOID")]
    public string Fsendingpeoid { get; set; } = string.Empty;

    /// <summary>
    /// 来源
    /// </summary>
    [SugarColumn(ColumnName = "FSOURCE")]
    public string Fsource { get; set; } = string.Empty;

    /// <summary>
    /// 供应商
    /// </summary>
    [SugarColumn(ColumnName = "FSUPPLYID")]
    public string Fsupplyid { get; set; } = string.Empty;

    /// <summary>
    /// 经办人/领用人
    /// </summary>
    [SugarColumn(ColumnName = "FAGENTUSERID")]
    public string Fagentuserid { get; set; } = string.Empty;

    /// <summary>
    /// 设备编号
    /// </summary>
    [SugarColumn(ColumnName = "FEQUIPMENTID")]
    public string Fequipmentid { get; set; } = string.Empty;

    /// <summary>
    /// 部位
    /// </summary>
    [SugarColumn(ColumnName = "FPARTID")]
    public string Fpartid { get; set; } = string.Empty;

    /// <summary>
    /// 采购订单
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASEORDER")]
    public string Fpurchaseorder { get; set; } = string.Empty;

    /// <summary>
    /// 发票号
    /// </summary>
    [SugarColumn(ColumnName = "FINVOICENO")]
    public string Finvoiceno { get; set; } = string.Empty;

    /// <summary>
    /// 采购合同
    /// </summary>
    [SugarColumn(ColumnName = "FPURCHASECONTRACT")]
    public string Fpurchasecontract { get; set; } = string.Empty;

    /// <summary>
    /// 运单号
    /// </summary>
    [SugarColumn(ColumnName = "FTHEAWB")]
    public string Ftheawb { get; set; } = string.Empty;

    /// <summary>
    /// 验收单号
    /// </summary>
    [SugarColumn(ColumnName = "FACCEPTANCENO")]
    public string Facceptanceno { get; set; } = string.Empty;

    /// <summary>
    /// 到货日期
    /// </summary>
    [SugarColumn(ColumnName = "FGOODSARRIVEDATE")]
    public DateTime? Fgoodsarrivedate { get; set; }

    /// <summary>
    /// 主机名称
    /// </summary>
    [SugarColumn(ColumnName = "FHOSTNAME")]
    public string Fhostname { get; set; } = string.Empty;

    /// <summary>
    /// 币种
    /// </summary>
    [SugarColumn(ColumnName = "FCURRENCY")]
    public int Fcurrency { get; set; }

    /// <summary>
    /// 汇率
    /// </summary>
    [SugarColumn(ColumnName = "FEXCHANGERATE")]
    public decimal Fexchangerate { get; set; }

    /// <summary>
    /// 系统/位置
    /// </summary>
    [SugarColumn(ColumnName = "FSYSTEM")]
    public string Fsystem { get; set; } = string.Empty;

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
