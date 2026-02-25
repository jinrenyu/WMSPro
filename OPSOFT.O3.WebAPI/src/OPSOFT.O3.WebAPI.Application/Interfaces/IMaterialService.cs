using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 物料服务接口
/// </summary>
public interface IMaterialService : IApprovableDisableableService<TBdMaterial, MaterialListDto, MaterialDetailDto, CreateMaterialRequest, UpdateMaterialRequest>
{
}
