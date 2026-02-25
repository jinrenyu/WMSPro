using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Interfaces;

/// <summary>
/// 通用仓储接口
/// </summary>
public interface IRepository<T> where T : BaseEntity, new()
{
    Task<T?> GetByIdAsync(string uid);
    Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetListAsync();
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
    Task<(List<T> Items, int TotalCount)> GetPagedListAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        string? orderByField = null,
        bool isAsc = true);
    Task<T> InsertAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(string uid);
    Task<bool> SoftDeleteAsync(string uid);
    ISugarQueryable<T> AsQueryable();
}
