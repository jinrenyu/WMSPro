using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class WarehouseListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FPrincipal { get; set; } = string.Empty;
    public string FTel { get; set; } = string.Empty;
    public string FType { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string FDescription { get; set; } = string.Empty;
    public string FStockProperty { get; set; } = string.Empty;
    public bool FAllowMinusQty { get; set; }
    public bool FIsOpenLocation { get; set; }
    public bool FBonded { get; set; }
    public bool FAllowMrpPlan { get; set; }
    public bool FAllowLock { get; set; }
    public bool FAvailableAlert { get; set; }
    public bool FAvailablePicking { get; set; }
    public int FSortingPriority { get; set; }
    public bool FIsVirtual { get; set; }
    public string ErpNumber { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
    public string FGroupName { get; set; } = string.Empty;
}

public class WarehouseDetailDto : WarehouseListDto
{
    // 库存状态相关
    public string FStockStatusType { get; set; } = string.Empty;
    public string FDefStockStatusId { get; set; } = string.Empty;
    public string FDefReceiveStatusId { get; set; } = string.Empty;
    // 所在车间（部门）
    public string FWorkshopId { get; set; } = string.Empty;

    // ---- 使用组织 / 审计（只读） ----
    public string FCompanyId { get; set; } = string.Empty;
    public string FCompanyName { get; set; } = string.Empty;
    public string CUser { get; set; } = string.Empty;
    public string MUser { get; set; } = string.Empty;
    public DateTime? MYmd { get; set; }
    public string FCheckerId { get; set; } = string.Empty;
    public DateTime? FCheckDate { get; set; }
    public string Fdisableid { get; set; } = string.Empty;
    public DateTime? Fdisabledate { get; set; }
}

public class CreateWarehouseRequest
{
    [Required(ErrorMessage = "仓库代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "仓库名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCompanyId { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public string FPrincipal { get; set; } = string.Empty;
    public string FTel { get; set; } = string.Empty;
    public string FType { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FStockProperty { get; set; } = string.Empty;
    public string FStockStatusType { get; set; } = string.Empty;
    public string FDefStockStatusId { get; set; } = string.Empty;
    public string FDefReceiveStatusId { get; set; } = string.Empty;
    public string FWorkshopId { get; set; } = string.Empty;
    public bool FBonded { get; set; }
    public bool FAllowMinusQty { get; set; }
    public bool FIsOpenLocation { get; set; }
    public bool FAllowMrpPlan { get; set; }
    public bool FAllowLock { get; set; }
    public bool FAvailableAlert { get; set; }
    public bool FIsVirtual { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

// ============ 仓位信息（启用仓位管理时维护，两级主从：仓位集 → 仓位集值）============
// 仓位集/值的 代码·名称·描述 来自主表 JOIN：
//   T_BD_STOCKFLEXITEM.FFLEXID      -> T_BAS_FLEXVALUES.FINTERID
//   T_BD_STOCKFLEXDETAIL.FFLEXENTRYID -> T_BAS_FLEXVALUESENTRY.FDETAILID

/// <summary>仓位集（T_BD_STOCKFLEXITEM）含其仓位集值明细</summary>
public class WarehouseFlexItemDto
{
    public string Uid { get; set; } = string.Empty;
    public int FEntryId { get; set; }
    /// <summary>仓位集引用键（= T_BAS_FLEXVALUES.FINTERID）</summary>
    public string FFlexId { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public bool FIsMustInput { get; set; }
    public List<WarehouseFlexDetailDto> Details { get; set; } = new();
}

/// <summary>仓位集值（T_BD_STOCKFLEXDETAIL）</summary>
public class WarehouseFlexDetailDto
{
    public string Uid { get; set; } = string.Empty;
    public int FEntryId { get; set; }
    /// <summary>仓位集值引用键（= T_BAS_FLEXVALUESENTRY.FDETAILID）</summary>
    public string FFlexEntryId { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
}

public class SaveWarehouseFlexRequest
{
    public List<SaveWarehouseFlexItem> Items { get; set; } = new();
}

public class SaveWarehouseFlexItem
{
    /// <summary>仓位集引用键（= T_BAS_FLEXVALUES.FINTERID）</summary>
    public string FFlexId { get; set; } = string.Empty;
    public bool FIsMustInput { get; set; }
    public List<SaveWarehouseFlexDetailItem> Details { get; set; } = new();
}

public class SaveWarehouseFlexDetailItem
{
    /// <summary>仓位集值引用键（= T_BAS_FLEXVALUESENTRY.FDETAILID）</summary>
    public string FFlexEntryId { get; set; } = string.Empty;
}

public class UpdateWarehouseRequest
{
    [Required(ErrorMessage = "仓库名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FDescription { get; set; } = string.Empty;
    public string FPrincipal { get; set; } = string.Empty;
    public string FTel { get; set; } = string.Empty;
    public string FType { get; set; } = string.Empty;
    public string FAddress { get; set; } = string.Empty;
    public string FStockProperty { get; set; } = string.Empty;
    public string FStockStatusType { get; set; } = string.Empty;
    public string FDefStockStatusId { get; set; } = string.Empty;
    public string FDefReceiveStatusId { get; set; } = string.Empty;
    public string FWorkshopId { get; set; } = string.Empty;
    public bool FBonded { get; set; }
    public bool FAllowMinusQty { get; set; }
    public bool FIsOpenLocation { get; set; }
    public bool FAllowMrpPlan { get; set; }
    public bool FAllowLock { get; set; }
    public bool FAvailableAlert { get; set; }
    public bool FIsVirtual { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}
