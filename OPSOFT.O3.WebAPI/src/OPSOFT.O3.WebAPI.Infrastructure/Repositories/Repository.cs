using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;
using System.Linq.Expressions;
using System.Reflection;

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
        bool isAsc = true,
        List<IConditionalModel>? conditionalModels = null)
    {
        var totalCount = new RefAsync<int>(0);

        var query = _db.Queryable<T>().Where(e => !e.FDeleted);

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (conditionalModels != null && conditionalModels.Count > 0)
        {
            query = query.Where(conditionalModels);
        }

        if (!string.IsNullOrEmpty(orderByField))
        {
            // 将前端传来的 camelCase 属性名转为 PascalCase 以匹配 C# 实体属性
            var propertyName = char.ToUpper(orderByField[0]) + orderByField.Substring(1);
            var entityType = typeof(T);
            var prop = entityType.GetProperty(propertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase);
            if (prop != null && prop.GetCustomAttributes(typeof(SugarColumn), false)
                    .OfType<SugarColumn>().All(a => !a.IsIgnore))
            {
                // 实体自身属性 → 直接按属性名排序
                query = query.OrderByPropertyName(prop.Name, isAsc ? OrderByType.Asc : OrderByType.Desc);
            }
            else
            {
                // 尝试通过导航属性实现关联排序
                // 约定: 前端字段 XxxName → 导航属性名 Xxx（去掉 Name 后缀）
                var resolved = TryResolveNavigateSort(query, entityType, propertyName, isAsc, out var sortedQuery);
                query = resolved ? sortedQuery! : query.OrderBy(e => e.CYmd, OrderByType.Desc);
            }
        }
        else
        {
            query = query.OrderBy(e => e.CYmd, OrderByType.Desc);
        }

        var items = await query.ToPageListAsync(pageIndex, pageSize, totalCount);

        return (items, totalCount.Value);
    }

    /// <summary>
    /// 尝试通过导航属性上的 [Navigate] 属性解析关联排序
    /// 扫描实体所有带 Navigate 的导航属性，匹配 XxxName → 导航属性 Xxx → FK + LEFT JOIN
    /// </summary>
    private bool TryResolveNavigateSort(ISugarQueryable<T> query, Type entityType, string propertyName, bool isAsc, out ISugarQueryable<T>? sortedQuery)
    {
        sortedQuery = null;

        // 1. 排序字段必须以 Name 结尾（如 FBaseUnitName, FGroupName）
        if (!propertyName.EndsWith("Name", StringComparison.Ordinal))
            return false;

        // 2. 去掉 Name 后缀 → FBaseUnit / FGroup
        var baseName = propertyName.Substring(0, propertyName.Length - 4);

        // 3. 去掉 F 前缀得到导航属性名 → BaseUnit / Group
        var navPropName = baseName;
        if (navPropName.StartsWith("F", StringComparison.Ordinal) && navPropName.Length > 1 && char.IsUpper(navPropName[1]))
            navPropName = navPropName.Substring(1);

        // 4. 查找导航属性（如 BaseUnit: TBdUnit?）
        var navProp = entityType.GetProperty(navPropName, BindingFlags.Public | BindingFlags.Instance);
        if (navProp == null) return false;

        // 5. 查找导航属性上的 [Navigate] 属性
        var navigateAttr = navProp.GetCustomAttributes(typeof(Navigate), false).FirstOrDefault();
        if (navigateAttr == null) return false;

        // 6. 从 Navigate 属性中获取 FK 属性名（Navigate 的属性是私有 backing field）
        var nameField = typeof(Navigate).GetField("<Name>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
        var fkPropName = nameField?.GetValue(navigateAttr)?.ToString();
        if (string.IsNullOrEmpty(fkPropName)) return false;

        // 8. 获取关联实体类型（从导航属性的属性类型推断）
        var relatedType = navProp.PropertyType;
        if (relatedType.IsValueType || relatedType == typeof(string)) return false;
        relatedType = Nullable.GetUnderlyingType(relatedType) ?? relatedType;

        // 9. 获取 FK 属性及其数据库列名
        var fkProp = entityType.GetProperty(fkPropName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy);
        if (fkProp == null) return false;
        var fkColumnName = fkProp.Name;
        {
            var colAttr = fkProp.GetCustomAttributes(typeof(SugarColumn), false).FirstOrDefault() as SugarColumn;
            if (colAttr != null && !string.IsNullOrEmpty(colAttr.ColumnName))
                fkColumnName = colAttr.ColumnName;
        }

        // 10. 获取关联表的表名
        var tableAttr = relatedType.GetCustomAttributes(typeof(SugarTable), false).FirstOrDefault() as SugarTable;
        var relatedTableName = tableAttr?.TableName ?? relatedType.Name;

        // 11. 获取关联表中名称字段的列名（FName 或 Fname）
        var nameColumnName = "FNAME";
        var nameProp = relatedType.GetProperty("FName") ?? relatedType.GetProperty("Fname");
        if (nameProp != null)
        {
            var colAttr = nameProp.GetCustomAttributes(typeof(SugarColumn), false).FirstOrDefault() as SugarColumn;
            if (colAttr != null && !string.IsNullOrEmpty(colAttr.ColumnName))
                nameColumnName = colAttr.ColumnName;
        }

        // 12. 获取关联表主键列名
        var pkColumnName = "UID";
        foreach (var pkProp in relatedType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
        {
            var colAttr = pkProp.GetCustomAttributes(typeof(SugarColumn), false).FirstOrDefault() as SugarColumn;
            if (colAttr is { IsPrimaryKey: true })
            {
                pkColumnName = !string.IsNullOrEmpty(colAttr.ColumnName) ? colAttr.ColumnName : pkProp.Name;
                break;
            }
        }

        // 13. 获取主表表名
        var mainTableAttr = entityType.GetCustomAttributes(typeof(SugarTable), false).FirstOrDefault() as SugarTable;
        var mainTableName = mainTableAttr?.TableName ?? entityType.Name;

        // 14. 使用子查询排序（避免 JOIN 导致的列名歧义问题）
        // 生成 SQL: ORDER BY (SELECT FNAME FROM T_BD_UNIT WHERE UID = T_BD_MATERIAL.FBASEUNITID) ASC
        var direction = isAsc ? "ASC" : "DESC";
        var subQuery = $"(SELECT {nameColumnName} FROM {relatedTableName} WHERE {pkColumnName} = {mainTableName}.{fkColumnName})";

        sortedQuery = query.OrderBy($"{subQuery} {direction}");

        return true;
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
