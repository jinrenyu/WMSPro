using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 可审核+可禁用服务接口 — 同时具备审核和禁用能力，两者互相独立
/// </summary>
public interface IApprovableDisableableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, IApprovable, IDisableable, new()
{
    Task<bool> ApproveAsync(string uid);
    Task<bool> UnapproveAsync(string uid);
    Task<bool> DisableAsync(string uid);
    Task<bool> EnableAsync(string uid);
}
