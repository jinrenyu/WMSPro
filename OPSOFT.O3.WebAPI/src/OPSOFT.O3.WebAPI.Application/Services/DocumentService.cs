using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 通用单据服务基类（主表 + 明细表模式）
/// </summary>
public abstract class DocumentService<THeader, TEntry, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    : IDocumentService<THeader, TEntry, TListDto, TDetailDto, TCreateDto, TUpdateDto>
    where THeader : BaseEntity, new()
    where TEntry : BaseEntity, new()
{
    protected readonly IRepository<THeader> HeaderRepo;
    protected readonly IRepository<TEntry> EntryRepo;
    protected readonly ISqlSugarClient Db;
    protected readonly ICurrentUserService CurrentUser;
    protected readonly IOperationLogService? OperationLog;

    protected virtual string PrgKey => string.Empty;

    protected DocumentService(
        IRepository<THeader> headerRepo,
        IRepository<TEntry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
    {
        HeaderRepo = headerRepo;
        EntryRepo = entryRepo;
        Db = db;
        CurrentUser = currentUser;
        OperationLog = operationLog;
    }

    public virtual async Task<PagedResult<TListDto>> GetPagedListAsync(PagedRequest request)
    {
        Expression<Func<THeader, bool>>? predicate = null;
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            predicate = BuildSearchPredicate(request.Keyword);
        }

        var (items, totalCount) = await HeaderRepo.GetPagedListAsync(
            request.PageIndex,
            request.PageSize,
            predicate,
            request.SortField,
            request.IsAsc);

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
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return default;

        var entries = await GetEntriesByHeaderIdAsync(uid);
        return MapToDetailDto(header, entries);
    }

    public virtual async Task<TDetailDto> CreateAsync(TCreateDto request)
    {
        try
        {
            Db.AsTenant().BeginTran();

            var header = MapToHeaderEntity(request);
            header.Uid = Guid.NewGuid().ToString("N");
            header.FInterId = header.Uid;
            header.CYmd = DateTime.Now;
            header.CUser = CurrentUser.UserId ?? string.Empty;
            header.MYmd = DateTime.Now;
            header.MUser = CurrentUser.UserId ?? string.Empty;
            header.FCompanyId = CurrentUser.CompanyId ?? string.Empty;

            await Db.Insertable(header).ExecuteCommandAsync();

            var entries = MapToEntryEntities(request, header.Uid);
            if (entries.Any())
            {
                int entryIndex = 1;
                foreach (var entry in entries)
                {
                    entry.Uid = Guid.NewGuid().ToString("N");
                    entry.FInterId = header.Uid;
                    entry.CYmd = DateTime.Now;
                    entry.CUser = CurrentUser.UserId ?? string.Empty;
                    entry.MYmd = DateTime.Now;
                    entry.MUser = CurrentUser.UserId ?? string.Empty;
                    entry.FCompanyId = CurrentUser.CompanyId ?? string.Empty;
                    SetEntryIndex(entry, entryIndex++);
                }
                await Db.Insertable(entries).ExecuteCommandAsync();
            }

            Db.AsTenant().CommitTran();

            if (OperationLog != null && !string.IsNullOrEmpty(PrgKey))
                _ = OperationLog.LogAsync(PrgKey, OperationType.Create, header.Uid);

            return (await GetByIdAsync(header.Uid))!;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    public virtual async Task<bool> UpdateAsync(string uid, TUpdateDto request)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted)
            throw new KeyNotFoundException("单据不存在");

        try
        {
            Db.AsTenant().BeginTran();

            UpdateHeaderEntity(header, request);
            header.MYmd = DateTime.Now;
            header.MUser = CurrentUser.UserId ?? string.Empty;
            await Db.Updateable(header).IgnoreColumns(e => new { e.CYmd, e.CUser }).ExecuteCommandAsync();

            // 删除旧明细
            var oldEntries = await GetEntriesByHeaderIdAsync(uid);
            if (oldEntries.Any())
            {
                var oldIds = oldEntries.Select(e => e.Uid).ToList();
                await Db.Deleteable<TEntry>().In(oldIds).ExecuteCommandAsync();
            }

            // 插入新明细
            var newEntries = MapToEntryEntities(request, uid);
            if (newEntries.Any())
            {
                int entryIndex = 1;
                foreach (var entry in newEntries)
                {
                    entry.Uid = Guid.NewGuid().ToString("N");
                    entry.FInterId = uid;
                    entry.CYmd = DateTime.Now;
                    entry.CUser = CurrentUser.UserId ?? string.Empty;
                    entry.MYmd = DateTime.Now;
                    entry.MUser = CurrentUser.UserId ?? string.Empty;
                    entry.FCompanyId = CurrentUser.CompanyId ?? string.Empty;
                    SetEntryIndex(entry, entryIndex++);
                }
                await Db.Insertable(newEntries).ExecuteCommandAsync();
            }

            Db.AsTenant().CommitTran();

            if (OperationLog != null && !string.IsNullOrEmpty(PrgKey))
                _ = OperationLog.LogAsync(PrgKey, OperationType.Update, uid);

            return true;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    public virtual async Task<bool> DeleteAsync(string uid)
    {
        var result = await HeaderRepo.SoftDeleteAsync(uid);
        if (result && OperationLog != null && !string.IsNullOrEmpty(PrgKey))
            _ = OperationLog.LogAsync(PrgKey, OperationType.Delete, uid);
        return result;
    }

    public abstract Task<bool> ApproveAsync(string uid);
    public abstract Task<bool> RejectAsync(string uid, string? reason = null);
    public abstract Task<bool> CloseAsync(string uid);

    protected abstract Expression<Func<THeader, bool>> BuildSearchPredicate(string keyword);
    protected abstract TListDto MapToListDto(THeader entity);
    protected abstract TDetailDto MapToDetailDto(THeader header, List<TEntry> entries);
    protected abstract THeader MapToHeaderEntity(TCreateDto dto);
    protected abstract List<TEntry> MapToEntryEntities(TCreateDto dto, string headerUid);
    protected abstract void UpdateHeaderEntity(THeader entity, TUpdateDto dto);
    protected abstract List<TEntry> MapToEntryEntities(TUpdateDto dto, string headerUid);
    protected abstract void SetEntryIndex(TEntry entry, int index);
    protected abstract Task<List<TEntry>> GetEntriesByHeaderIdAsync(string headerUid);
}
