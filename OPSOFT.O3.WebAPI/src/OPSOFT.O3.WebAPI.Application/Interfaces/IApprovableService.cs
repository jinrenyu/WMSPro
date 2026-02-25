using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 可审核服务接口 — 在 CRUD 基础上扩展审核/反审核
/// </summary>
public interface IApprovableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, IApprovable, new()
{
    Task<bool> ApproveAsync(string uid);
    Task<bool> UnapproveAsync(string uid);
}
