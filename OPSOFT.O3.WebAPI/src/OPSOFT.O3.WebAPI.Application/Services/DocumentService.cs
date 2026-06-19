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
    /// <summary>编码规则取号引擎（可选注入；需要按规则取号的单据由 DI 传入）</summary>
    protected readonly IBillCodeService? BillCode;

    protected virtual string PrgKey => string.Empty;

    protected DocumentService(
        IRepository<THeader> headerRepo,
        IRepository<TEntry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null,
        IBillCodeService? billCode = null)
    {
        HeaderRepo = headerRepo;
        EntryRepo = entryRepo;
        Db = db;
        CurrentUser = currentUser;
        OperationLog = operationLog;
        BillCode = billCode;
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
            // 仅当 MapToHeaderEntity 未设置组织时才回落当前登录组织，避免覆盖表单选择的组织（如采购订单的采购组织）
            if (string.IsNullOrEmpty(header.FCompanyId))
                header.FCompanyId = CurrentUser.CompanyId ?? string.Empty;

            // 事务内异步补全表头（如按编码规则取号），与流水占号同事务、回滚时一并释放
            await PrepareHeaderForCreateAsync(header, request);

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

            // await 而非 fire-and-forget：Scoped SqlSugarClient 非线程安全，未等待的日志插入会与
            // 紧随其后的 GetByIdAsync 查询并发使用同一连接（MSSQL 下间歇 500，单据已落库→前端重试产生重单）
            if (OperationLog != null && !string.IsNullOrEmpty(PrgKey))
                await OperationLog.LogAsync(PrgKey, OperationType.Create, header.Uid);

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
                await OperationLog.LogAsync(PrgKey, OperationType.Update, uid);

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
        try
        {
            Db.AsTenant().BeginTran();

            var result = await HeaderRepo.SoftDeleteAsync(uid);
            if (result)
            {
                // 级联软删明细，避免按明细行展开的列表出现"主单已删、明细残留"的孤儿行
                await Db.Updateable<TEntry>()
                    .SetColumns(e => e.FDeleted == true)
                    .SetColumns(e => e.MYmd == DateTime.Now)
                    .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
                    .Where(e => e.FInterId == uid)
                    .ExecuteCommandAsync();
            }

            Db.AsTenant().CommitTran();

            if (result && OperationLog != null && !string.IsNullOrEmpty(PrgKey))
                await OperationLog.LogAsync(PrgKey, OperationType.Delete, uid);
            return result;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    /// <summary>表头单据编号属性（约定名 Fbillno，列 FBILLNO）。按 THeader 缓存，供基类反射默认取号读写/查重；
    /// 未覆盖 Get/SetBillNo 的单据即走此反射路径，无该属性则反射默认体安全退化为不取号。</summary>
    private static readonly System.Reflection.PropertyInfo? BillNoProperty = typeof(THeader).GetProperty("Fbillno");

    /// <summary>创建钩子：表头插入前（事务内）调用。默认按编码规则取号——
    /// 单据要么显式声明 <see cref="BillCodeFormKey"/>，要么在数据驱动目录（SYS_BILLCODEFORM）按表头实体类名登记；
    /// 编号读写/查重默认反射 Fbillno 属性，纯常量/日期/流水规则无需任何代码；带字段段的在 <see cref="PopulateBillCodeContextAsync"/> 补来源值。
    /// 未注入 BillCode、或既未声明又未登记的单据直接跳过，不取号、不影响行为。</summary>
    protected virtual async Task PrepareHeaderForCreateAsync(THeader header, TCreateDto dto)
    {
        if (BillCode is null) return; // 未注入取号引擎 = 该单据未接入编码规则
        // formKey 优先取子类显式声明；未声明则按表头实体类名查数据驱动目录反射取号（新单据零代码上架的关键）
        var formKey = BillCodeFormKey ?? await BillCode.ResolveFormKeyByEntityAsync(typeof(THeader).Name);
        if (formKey is null) return; // 既未声明又未登记 = 不取号
        var ctx = new Dictionary<string, string>
        {
            [BillCodeFields.Date] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")  // 默认当前；子类可在 Populate 覆盖为单据日期
        };
        await PopulateBillCodeContextAsync(ctx, header, dto);
        // existsAsync 查重含软删行（生产 FBILLNO 唯一聚簇索引不放过软删），手工号重复抛业务异常、自动取号撞占用跳号
        SetBillNo(header, await BillCode.ResolveBillNoAsync(formKey, GetBillNo(header), ctx, BillNoExistsAsync));
    }

    /// <summary>该单据的编码规则表单标识（formKey）；返回 null 则回落按表头实体名查目录（仍查不到才不取号）</summary>
    protected virtual string? BillCodeFormKey => null;
    /// <summary>读取表头当前单据编号（手工号或空），供取号判定手工/自动。默认反射 Fbillno 属性。</summary>
    protected virtual string GetBillNo(THeader header) => BillNoProperty?.GetValue(header) as string ?? string.Empty;
    /// <summary>把取号结果写回表头单据编号列。默认反射 Fbillno 属性。</summary>
    protected virtual void SetBillNo(THeader header, string billNo) => BillNoProperty?.SetValue(header, billNo);
    /// <summary>向取号上下文补充来源字段值（如单据日期、单据类型编码、供应商编码）</summary>
    protected virtual Task PopulateBillCodeContextAsync(IDictionary<string, string> ctx, THeader header, TCreateDto dto) => Task.CompletedTask;
    /// <summary>判断某单据编号是否已被占用（含软删行），用于手工号查重与自动取号跳号。
    /// 默认按 Fbillno 反射构造表达式查表头表（不过滤软删，与生产 FBILLNO 唯一索引一致）。</summary>
    protected virtual Task<bool> BillNoExistsAsync(string billNo)
    {
        if (BillNoProperty is null) return Task.FromResult(false);
        var p = Expression.Parameter(typeof(THeader), "h");
        var body = Expression.Equal(Expression.Property(p, BillNoProperty), Expression.Constant(billNo));
        var lambda = Expression.Lambda<Func<THeader, bool>>(body, p);
        return Db.Queryable<THeader>().Where(lambda).AnyAsync();
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
