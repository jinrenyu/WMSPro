namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ============ 物料维度（辅助属性配置 TBdMaterialAuxPty）============

public class MaterialDimensionDto
{
    public string Uid { get; set; } = string.Empty;
    public string FAuxPropertyId { get; set; } = string.Empty;
    public string FAuxPropertyNumber { get; set; } = string.Empty; // 来自 TBdFlexauxproperty
    public string FAuxPropertyName { get; set; } = string.Empty;   // 来自 TBdFlexauxproperty
    public bool FIsEnable { get; set; }
    public bool FIsComControl { get; set; }
    public bool FIsMustInput { get; set; }
    public bool FIsAffectPrice { get; set; }
    public bool FIsAffectPlan { get; set; }
    public bool FIsAffectCost { get; set; }
    public bool FValueSet { get; set; }
    public int FEntryId { get; set; }
}

public class SaveMaterialDimensionItem
{
    public string FAuxPropertyId { get; set; } = string.Empty;
    public bool FIsEnable { get; set; }
    public bool FIsComControl { get; set; }
    public bool FIsMustInput { get; set; }
    public bool FIsAffectPrice { get; set; }
    public bool FIsAffectPlan { get; set; }
    public bool FIsAffectCost { get; set; }
    public bool FValueSet { get; set; }
}

public class SaveMaterialDimensionsRequest
{
    public List<SaveMaterialDimensionItem> Items { get; set; } = new();
}

// ============ 辅助单位换算（TBdSecunitconvertrate）============

public class MaterialSecUnitDto
{
    public string Uid { get; set; } = string.Empty;
    public string FConvertType { get; set; } = string.Empty;       // 换算类型 固定/浮动
    public string FBaseUnitId { get; set; } = string.Empty;
    public string FBaseUnitName { get; set; } = string.Empty;
    public string FSecUnitId { get; set; } = string.Empty;
    public string FSecUnitName { get; set; } = string.Empty;
    public decimal FConvertNumerator { get; set; }                 // 辅助单位数量（分子）
    public decimal FConvertDenominator { get; set; }               // 基本单位数量（分母）
    public int FEntryId { get; set; }
}

public class SaveMaterialSecUnitItem
{
    public string FConvertType { get; set; } = string.Empty;
    public string FBaseUnitId { get; set; } = string.Empty;
    public string FSecUnitId { get; set; } = string.Empty;
    public decimal FConvertNumerator { get; set; }
    public decimal FConvertDenominator { get; set; }
}

public class SaveMaterialSecUnitsRequest
{
    public List<SaveMaterialSecUnitItem> Items { get; set; } = new();
}
