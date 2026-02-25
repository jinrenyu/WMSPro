using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 纯禁用 CRUD 服务基类 — 只有禁用/反禁用能力，不包含审核
/// </summary>
public abstract class DisableableCrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : CrudService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>,
      IDisableableService<TEntity, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity, IDisableable, new()
{
    protected readonly ISqlSugarClient Db;
    protected readonly ICurrentUserService CurrentUser;

    protected DisableableCrudService(
        IRepository<TEntity> repository,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null) : base(repository, operationLog)
    {
        Db = db;
        CurrentUser = currentUser;
    }

    /// <summary>
    /// 禁用 — FDisabled → true，记录禁用人和禁用日期
    /// </summary>
    public virtual async Task<bool> DisableAsync(string uid)
    {
        var entity = await Repository.GetByIdAsync(uid);
        if (entity == null || entity.FDeleted)
            throw new KeyNotFoundException("记录不存在");
        if (entity.FDisabled)
            throw new InvalidOperationException("该记录已禁用，不能重复禁用");

        var result = await Db.Updateable<TEntity>()
            .SetColumns(e => e.FDisabled == true)
            .SetColumns(e => e.Fdisableid == (CurrentUser.UserId ?? string.Empty))
            .SetColumns(e => e.Fdisabledate == DateTime.Now)
            .SetColumns(e => e.MYmd == DateTime.Now)
            .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(e => e.Uid == uid)
            .ExecuteCommandAsync() > 0;
        if (result && OperationLog != null && !string.IsNullOrEmpty(PrgKey))
            _ = OperationLog.LogAsync(PrgKey, OperationType.Disable, uid);
        return result;
    }

    /// <summary>
    /// 反禁用 — FDisabled → false，清除禁用人和禁用日期
    /// </summary>
    public virtual async Task<bool> EnableAsync(string uid)
    {
        var entity = await Repository.GetByIdAsync(uid);
        if (entity == null || entity.FDeleted)
            throw new KeyNotFoundException("记录不存在");
        if (!entity.FDisabled)
            throw new InvalidOperationException("该记录未禁用，不能反禁用");

        var result = await Db.Updateable<TEntity>()
            .SetColumns(e => e.FDisabled == false)
            .SetColumns(e => e.Fdisableid == string.Empty)
            .SetColumns(e => e.Fdisabledate == DateTime.MinValue)
            .SetColumns(e => e.MYmd == DateTime.Now)
            .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(e => e.Uid == uid)
            .ExecuteCommandAsync() > 0;
        if (result && OperationLog != null && !string.IsNullOrEmpty(PrgKey))
            _ = OperationLog.LogAsync(PrgKey, OperationType.Enable, uid);
        return result;
    }
}
