using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;
using OPSOFT.O3.WebAPI.Application.Extensions;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 通用 CRUD 服务基类
/// </summary>
public abstract class CrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : ICrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, new()
{
    protected readonly IRepository<TEntity> Repository;
    protected readonly IOperationLogService? OperationLog;

    protected virtual string PrgKey => string.Empty;

    protected CrudService(IRepository<TEntity> repository, IOperationLogService? operationLog = null)
    {
        Repository = repository;
        OperationLog = operationLog;
    }

    public virtual async Task<PagedResult<TListDto>> GetPagedListAsync(PagedRequest request)
    {
        Expression<Func<TEntity, bool>>? predicate = null;
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            predicate = BuildSearchPredicate(request.Keyword);
        }

        // 通用分组过滤
        if (!string.IsNullOrEmpty(request.GroupId))
        {
            Expression<Func<TEntity, bool>> groupFilter = e => e.FGroupId == request.GroupId;
            if (predicate == null)
            {
                predicate = groupFilter;
            }
            else
            {
                var param = Expression.Parameter(typeof(TEntity), "e");
                var body = Expression.AndAlso(
                    Expression.Invoke(predicate, param),
                    Expression.Invoke(groupFilter, param));
                predicate = Expression.Lambda<Func<TEntity, bool>>(body, param);
            }
        }

        // 动态高级筛选
        var conditionalModels = request.DynamicFilters?.ToConditionalModels<TEntity>() ?? new List<IConditionalModel>();

        var (items, totalCount) = await Repository.GetPagedListAsync(
            request.PageIndex,
            request.PageSize,
            predicate,
            request.SortField,
            request.IsAsc,
            conditionalModels);

        return new PagedResult<TListDto>
        {
            Items = items.Select(MapToListDto).ToList(),
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    public virtual async Task<TDetailDto?> GetByIdAsync(string uid)
    {
        var entity = await Repository.GetByIdAsync(uid);
        if (entity == null || entity.FDeleted) return default;
        return MapToDetailDto(entity);
    }

    public virtual async Task<TDetailDto> CreateAsync(TCreateDto request)
    {
        var entity = MapToEntity(request);
        var created = await Repository.InsertAsync(entity);
        if (OperationLog != null && !string.IsNullOrEmpty(PrgKey))
            _ = OperationLog.LogAsync(PrgKey, OperationType.Create, created.Uid);
        return MapToDetailDto(created);
    }

    public virtual async Task<bool> UpdateAsync(string uid, TUpdateDto request)
    {
        var entity = await Repository.GetByIdAsync(uid);
        if (entity == null || entity.FDeleted)
            throw new KeyNotFoundException("记录不存在");

        UpdateEntity(entity, request);
        var result = await Repository.UpdateAsync(entity);
        if (result && OperationLog != null && !string.IsNullOrEmpty(PrgKey))
            _ = OperationLog.LogAsync(PrgKey, OperationType.Update, uid);
        return result;
    }

    public virtual async Task<bool> DeleteAsync(string uid)
    {
        var result = await Repository.SoftDeleteAsync(uid);
        if (result && OperationLog != null && !string.IsNullOrEmpty(PrgKey))
            _ = OperationLog.LogAsync(PrgKey, OperationType.Delete, uid);
        return result;
    }

    public virtual async Task<List<LookupDto>> GetLookupAsync(LookupRequest request)
    {
        // 基础过滤：未删除且未禁用
        Expression<Func<TEntity, bool>> basePredicate = e => !e.FDeleted && !e.FDisabled;

        var query = Repository.AsQueryable().Where(basePredicate);

        // 关键字搜索
        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var searchPredicate = BuildSearchPredicate(request.Keyword);
            query = query.Where(searchPredicate);
        }

        // 子类自定义过滤（如仓位按仓库、部门按父级）
        var extraFilter = BuildLookupFilter(request);
        if (extraFilter != null)
        {
            query = query.Where(extraFilter);
        }

        var entities = await query.Take(request.MaxCount).ToListAsync();
        return entities.Select(MapToLookupDto).ToList();
    }

    /// <summary>
    /// 子类可重写以添加额外的 Lookup 过滤条件
    /// </summary>
    protected virtual Expression<Func<TEntity, bool>>? BuildLookupFilter(LookupRequest request)
    {
        return null;
    }

    /// <summary>
    /// 实体 → LookupDto，子类可重写以处理命名不一致的实体
    /// </summary>
    protected virtual LookupDto MapToLookupDto(TEntity entity)
    {
        var type = typeof(TEntity);

        // 兼容 FNumber / Fnumber 命名
        var numberProp = type.GetProperty("FNumber") ?? type.GetProperty("Fnumber");
        var nameProp = type.GetProperty("FName") ?? type.GetProperty("Fname");

        return new LookupDto
        {
            Uid = entity.Uid,
            FNumber = numberProp?.GetValue(entity)?.ToString() ?? string.Empty,
            FName = nameProp?.GetValue(entity)?.ToString() ?? string.Empty
        };
    }

    /// <summary>
    /// 构建搜索条件（子类实现，通常按编码+名称模糊搜索）
    /// </summary>
    protected abstract Expression<Func<TEntity, bool>> BuildSearchPredicate(string keyword);

    /// <summary>
    /// 实体 → 列表 DTO
    /// </summary>
    protected abstract TListDto MapToListDto(TEntity entity);

    /// <summary>
    /// 实体 → 详情 DTO
    /// </summary>
    protected abstract TDetailDto MapToDetailDto(TEntity entity);

    /// <summary>
    /// 创建 DTO → 实体
    /// </summary>
    protected abstract TEntity MapToEntity(TCreateDto dto);

    /// <summary>
    /// 更新 DTO → 更新实体字段
    /// </summary>
    protected abstract void UpdateEntity(TEntity entity, TUpdateDto dto);
}
