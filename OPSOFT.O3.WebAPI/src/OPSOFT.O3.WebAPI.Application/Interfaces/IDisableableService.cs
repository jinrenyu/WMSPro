using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 纯禁用服务接口 — 只有禁用/反禁用能力，不包含审核
/// </summary>
public interface IDisableableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, IDisableable, new()
{
    Task<bool> DisableAsync(string uid);
    Task<bool> EnableAsync(string uid);
}
