using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 仓库
/// </summary>
[SugarTable("T_BD_STOCK")]
public class TBdStock : BaseEntity, IApprovable, IDisableable
{
    /// <summary>
    /// 仓库代码
    /// </summary>
    [SugarColumn(ColumnName = "FNUMBER")]
    public string FNumber { get; set; } = string.Empty;

    /// <summary>
    /// 仓库名称
    /// </summary>
    [SugarColumn(ColumnName = "FNAME")]
    public string FName { get; set; } = string.Empty;

    /// <summary>
    /// 仓库描述
    /// </summary>
    [SugarColumn(ColumnName = "FDESCRIPTION")]
    public string FDescription { get; set; } = string.Empty;

    /// <summary>
    /// 仓库负责人
    /// </summary>
    [SugarColumn(ColumnName = "FPRINCIPAL")]
    public string FPrincipal { get; set; } = string.Empty;

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "FTEL")]
    public string FTel { get; set; } = string.Empty;

    /// <summary>
    /// 允许负库存
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWMINUSQTY")]
    public bool FAllowMinusQty { get; set; }

    /// <summary>
    /// 启用仓位管理
    /// </summary>
    [SugarColumn(ColumnName = "FISOPENLOCATION")]
    public bool FIsOpenLocation { get; set; }

    /// <summary>
    /// 仓库属性
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKPROPERTY")]
    public string FStockProperty { get; set; } = string.Empty;

    /// <summary>
    /// 库存状态类型
    /// </summary>
    [SugarColumn(ColumnName = "FSTOCKSTATUSTYPE")]
    public string FStockStatusType { get; set; } = string.Empty;

    /// <summary>
    /// 默认库存状态
    /// </summary>
    [SugarColumn(ColumnName = "FDEFSTOCKSTATUSID")]
    public string FDefStockStatusId { get; set; } = string.Empty;

    /// <summary>
    /// 默认收料状态
    /// </summary>
    [SugarColumn(ColumnName = "FDEFRECEIVESTATUSID")]
    public string FDefReceiveStatusId { get; set; } = string.Empty;

    /// <summary>
    /// 是否保税
    /// </summary>
    [SugarColumn(ColumnName = "FBONDED")]
    public bool FBonded { get; set; }

    /// <summary>
    /// 仓库类型
    /// </summary>
    [SugarColumn(ColumnName = "FTYPE")]
    public string FType { get; set; } = string.Empty;

    /// <summary>
    /// 仓库地址
    /// </summary>
    [SugarColumn(ColumnName = "FADDRESS")]
    public string FAddress { get; set; } = string.Empty;

    /// <summary>
    /// 允许MRP计划
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWMRPPLAN")]
    public bool FAllowMrpPlan { get; set; }

    /// <summary>
    /// 允许锁库
    /// </summary>
    [SugarColumn(ColumnName = "FALLOWLOCK")]
    public bool FAllowLock { get; set; }

    /// <summary>
    /// 参与预警
    /// </summary>
    [SugarColumn(ColumnName = "FAVAILABLEALERT")]
    public bool FAvailableAlert { get; set; }

    /// <summary>
    /// 参与拣货
    /// </summary>
    [SugarColumn(ColumnName = "FAVAILABLEPICKING")]
    public bool FAvailablePicking { get; set; }

    /// <summary>
    /// 拣货优先级
    /// </summary>
    [SugarColumn(ColumnName = "FSORTINGPRIORITY")]
    public int FSortingPriority { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKERID")]
    public string FCheckerId { get; set; } = string.Empty;

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "FCHECKDATE")]
    public DateTime FCheckDate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// 禁用人
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEID")]
    public string Fdisableid { get; set; } = string.Empty;

    /// <summary>
    /// 禁用日期
    /// </summary>
    [SugarColumn(ColumnName = "FDISABLEDATE")]
    public DateTime Fdisabledate { get; set; } = DateTime.MinValue;

    /// <summary>
    /// ERP编码
    /// </summary>
    [SugarColumn(ColumnName = "ERPNUMBER")]
    public string ErpNumber { get; set; } = string.Empty;

    /// <summary>
    /// 车间内码
    /// </summary>
    [SugarColumn(ColumnName = "FWORKSHOPID")]
    public string FWorkshopId { get; set; } = string.Empty;

    /// <summary>
    /// 是否虚仓
    /// </summary>
    [SugarColumn(ColumnName = "FISVIRTUAL")]
    public bool FIsVirtual { get; set; }
}
