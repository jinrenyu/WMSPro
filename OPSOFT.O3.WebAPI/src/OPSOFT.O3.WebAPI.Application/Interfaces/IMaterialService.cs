using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 物料服务接口
/// </summary>
public interface IMaterialService : IApprovableDisableableService<TBdMaterial, MaterialListDto, MaterialDetailDto, CreateMaterialRequest, UpdateMaterialRequest>
{
    /// <summary>物料维度（辅助属性配置）列表</summary>
    Task<List<MaterialDimensionDto>> GetDimensionsAsync(string materialUid);

    /// <summary>保存物料维度（整组替换）</summary>
    Task SaveDimensionsAsync(string materialUid, List<SaveMaterialDimensionItem> items);

    /// <summary>辅助单位换算列表</summary>
    Task<List<MaterialSecUnitDto>> GetSecUnitsAsync(string materialUid);

    /// <summary>保存辅助单位换算（整组替换）</summary>
    Task SaveSecUnitsAsync(string materialUid, List<SaveMaterialSecUnitItem> items);

    /// <summary>辅助属性下拉（来源 TBdFlexauxproperty）</summary>
    Task<List<LookupDto>> GetAuxPropertyLookupAsync(string? keyword);

    /// <summary>物料类别下拉（来源 TBdMaterialtype）</summary>
    Task<List<LookupDto>> GetMaterialTypeLookupAsync(string? keyword);
}
