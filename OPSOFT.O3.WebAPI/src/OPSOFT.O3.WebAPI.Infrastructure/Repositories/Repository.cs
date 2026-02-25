using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Repositories;

/// <summary>
/// 通用仓储实现
/// </summary>
public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;

    public Repository(ISqlSugarClient db, ICurrentUserService currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<T?> GetByIdAsync(string uid)
    {
        return await _db.Queryable<T>().InSingleAsync(uid);
    }

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> predicate)
    {
        return await _db.Queryable<T>().Where(predicate).FirstAsync();
    }

    public async Task<List<T>> GetListAsync()
    {
        return await _db.Queryable<T>().Where(e => !e.FDeleted).ToListAsync();
    }

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
    {
        return await _db.Queryable<T>().Where(predicate).Where(e => !e.FDeleted).ToListAsync();
    }

    public async Task<(List<T> Items, int TotalCount)> GetPagedListAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        string? orderByField = null,
        bool isAsc = true)
    {
        var totalCount = new RefAsync<int>(0);

        var query = _db.Queryable<T>().Where(e => !e.FDeleted);

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (!string.IsNullOrEmpty(orderByField))
        {
            query = query.OrderBy(orderByField + (isAsc ? " asc" : " desc"));
        }
        else
        {
            query = query.OrderBy(e => e.CYmd, OrderByType.Desc);
        }

        var items = await query.ToPageListAsync(pageIndex, pageSize, totalCount);

        return (items, totalCount.Value);
    }

    public async Task<T> InsertAsync(T entity)
    {
        FillAuditFieldsForInsert(entity);
        return await _db.Insertable(entity).ExecuteReturnEntityAsync();
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        FillAuditFieldsForUpdate(entity);
        return await _db.Updateable(entity)
            .IgnoreColumns(e => new { e.CYmd, e.CUser })
            .ExecuteCommandAsync() > 0;
    }

    public async Task<bool> DeleteAsync(string uid)
    {
        return await _db.Deleteable<T>().In(uid).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> SoftDeleteAsync(string uid)
    {
        return await _db.Updateable<T>()
            .SetColumns(e => e.FDeleted == true)
            .SetColumns(e => e.MYmd == DateTime.Now)
            .SetColumns(e => e.MUser == (_currentUser.UserId ?? string.Empty))
            .Where(e => e.Uid == uid)
            .ExecuteCommandAsync() > 0;
    }

    public ISugarQueryable<T> AsQueryable()
    {
        return _db.Queryable<T>();
    }

    private void FillAuditFieldsForInsert(T entity)
    {
        if (string.IsNullOrEmpty(entity.Uid))
        {
            entity.Uid = Guid.NewGuid().ToString("N");
        }
        if (string.IsNullOrEmpty(entity.FInterId))
        {
            entity.FInterId = entity.Uid;
        }
        entity.CYmd = DateTime.Now;
        entity.CUser = _currentUser.UserId ?? string.Empty;
        entity.MYmd = DateTime.Now;
        entity.MUser = _currentUser.UserId ?? string.Empty;
        if (string.IsNullOrEmpty(entity.FCompanyId))
        {
            entity.FCompanyId = _currentUser.CompanyId ?? string.Empty;
        }
        entity.FDeleted = false;
    }

    private void FillAuditFieldsForUpdate(T entity)
    {
        entity.MYmd = DateTime.Now;
        entity.MUser = _currentUser.UserId ?? string.Empty;
    }
}
