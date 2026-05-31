using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

// ============ 仓位（仓位集 T_BAS_FLEXVALUES）主数据 ============

public class FlexValuesListDto
{
    public string Uid { get; set; } = string.Empty;
    public int FStatus { get; set; }
    public bool FDisabled { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FFlexTypeId { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public string FGroupId { get; set; } = string.Empty;
}

public class FlexValuesDetailDto : FlexValuesListDto
{
    // 仓位信息详情 - 尺寸
    public decimal FLength { get; set; }
    public decimal FWidth { get; set; }
    public decimal FHeight { get; set; }
    public decimal FVolume { get; set; }
    // 仓位信息详情 - 限制
    public decimal FVolumeLimit { get; set; }
    public decimal FWeightLimit { get; set; }
    public string FMixTypeLimit { get; set; } = string.Empty;
    // 值列表
    public List<FlexValuesEntryDto> Entries { get; set; } = new();
    // 使用组织 / 审计（只读）
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

/// <summary>仓位集值（T_BAS_FLEXVALUESENTRY）</summary>
public class FlexValuesEntryDto
{
    public string Uid { get; set; } = string.Empty;
    public int FEntryId { get; set; }
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public bool FForbid { get; set; }
}

public class CreateFlexValuesRequest
{
    [Required(ErrorMessage = "代码不能为空")]
    public string FNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FCompanyId { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public decimal FLength { get; set; }
    public decimal FWidth { get; set; }
    public decimal FHeight { get; set; }
    public decimal FVolume { get; set; }
    public decimal FVolumeLimit { get; set; }
    public decimal FWeightLimit { get; set; }
    public string FMixTypeLimit { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

public class UpdateFlexValuesRequest
{
    [Required(ErrorMessage = "名称不能为空")]
    public string FName { get; set; } = string.Empty;

    public string FDescription { get; set; } = string.Empty;
    public decimal FLength { get; set; }
    public decimal FWidth { get; set; }
    public decimal FHeight { get; set; }
    public decimal FVolume { get; set; }
    public decimal FVolumeLimit { get; set; }
    public decimal FWeightLimit { get; set; }
    public string FMixTypeLimit { get; set; } = string.Empty;
    public string FGroupId { get; set; } = string.Empty;
}

// ---- 值列表（整组按 uid 合并保存，保留已被仓库引用的 FDetailid）----

public class SaveFlexValuesEntriesRequest
{
    public List<SaveFlexValuesEntryItem> Items { get; set; } = new();
}

public class SaveFlexValuesEntryItem
{
    /// <summary>已存在条目的 Uid（为空表示新增）</summary>
    public string Uid { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string FDescription { get; set; } = string.Empty;
    public bool FForbid { get; set; }
}
