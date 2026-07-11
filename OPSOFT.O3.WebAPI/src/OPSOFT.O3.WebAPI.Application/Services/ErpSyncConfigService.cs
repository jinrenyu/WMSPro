using System.Linq.Expressions;
using System.Reflection;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Constants;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// ERP 集成配置服务（真实表 T_SYN_INFO 主表 + T_SYN_INFOENTRY[实体信息] + T_SYN_INFOENTRY1[字段映射]）。
/// 一主两从：重写 Create/Update/Delete 在单一事务内级联两张从表；字段映射经 Fbodyid 归属到实体行 Fdetailid。
/// 状态：10=草稿(未审)、40=审核(已审)。已审核方可参与定时/手动同步。
/// </summary>
public class ErpSyncConfigService : DocumentService<TSynInfo, TSynInfoentry,
    ErpSyncConfigListDto, ErpSyncConfigDetailDto, CreateErpSyncConfigRequest, UpdateErpSyncConfigRequest>,
    IErpSyncConfigService
{
    private readonly IErpSyncEngine _engine;

    public ErpSyncConfigService(
        IRepository<TSynInfo> headerRepo,
        IRepository<TSynInfoentry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IErpSyncEngine engine,
        IOperationLogService? operationLog = null)
        : base(headerRepo, entryRepo, db, currentUser, operationLog, null)
    {
        _engine = engine;
    }

    protected override string PrgKey => "ErpSyncConfig";

    // ===== 审核 / 反审核 / 关闭 =====

    public override async Task<bool> ApproveAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("配置不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("配置已审核，无需重复审核");

        var result = await Db.Updateable<TSynInfo>()
            .SetColumns(h => h.FStatus == 40)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Approve, uid, header.Fnumber, "审核配置", result);
        return result;
    }

    public override async Task<bool> RejectAsync(string uid, string? reason = null)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("配置不存在");
        if (header.FStatus != 40) throw new InvalidOperationException("只有已审核的配置才能反审核");

        var result = await Db.Updateable<TSynInfo>()
            .SetColumns(h => h.FStatus == 10)
            .SetColumns(h => h.MYmd == DateTime.Now)
            .SetColumns(h => h.MUser == (CurrentUser.UserId ?? string.Empty))
            .Where(h => h.Uid == uid)
            .ExecuteCommandAsync() > 0;

        _ = OperationLog?.LogAsync(PrgKey, OperationType.Reject, uid, header.Fnumber, reason ?? "反审核配置", result);
        return result;
    }

    public override Task<bool> CloseAsync(string uid)
        => throw new InvalidOperationException("ERP 集成配置不支持关闭操作");

    protected override Expression<Func<TSynInfo, bool>> BuildSearchPredicate(string keyword)
        => h => h.Fnumber.Contains(keyword) || h.Fname.Contains(keyword);

    // ===== 列表（按主表一行一张配置） =====

    public override async Task<PagedResult<ErpSyncConfigListDto>> GetPagedListAsync(PagedRequest request)
    {
        var result = await base.GetPagedListAsync(request);
        if (result.Items.Count > 0)
        {
            var statusDict = await LoadStatusDictAsync();
            foreach (var it in result.Items)
            {
                it.FstatusName = statusDict.GetValueOrDefault(it.FStatus, string.Empty);
                it.FtimetypeName = TimeTypeName(it.Ftimetype);
            }
        }
        return result;
    }

    protected override ErpSyncConfigListDto MapToListDto(TSynInfo e) => new()
    {
        Uid = e.Uid,
        Fnumber = e.Fnumber,
        Fname = e.Fname,
        Ferpbillid = e.Ferpbillid,
        Fstart = e.Fstart,
        Fsynrate = e.Fsynrate,
        Ftimetype = e.Ftimetype,
        Fruleid = e.Fruleid,
        Fisenable = e.Fisenable,
        Isdefault = e.Isdefault,
        Fnote = e.Fnote,
        Ftimestamp = e.Ftimestamp,
        FStatus = e.FStatus
    };

    // ===== 详情（主表 + 实体信息 + 字段映射 内嵌） =====

    public override async Task<ErpSyncConfigDetailDto?> GetByIdAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) return null;

        var entities = await GetEntriesByHeaderIdAsync(uid);
        var dto = MapToDetailDto(header, entities);

        // 加载字段映射并按 Fbodyid 归入各实体行
        var fields = await Db.Queryable<TSynInfoentry1>()
            .Where(f => f.FInterId == uid && !f.FDeleted)
            .OrderBy(f => f.Fentryid)
            .ToListAsync();
        var byBody = fields.GroupBy(f => f.Fbodyid).ToDictionary(g => g.Key, g => g.ToList());
        foreach (var ent in dto.Entities)
        {
            if (byBody.TryGetValue(ent.Fdetailid, out var fs))
                ent.FieldMaps = fs.Select(MapFieldToDto).ToList();
        }

        // 名称解析
        dto.FstatusName = (await LoadStatusDictAsync()).GetValueOrDefault(header.FStatus, string.Empty);
        var userDict = await LoadUserNameDictAsync(new[] { header.CUser, header.MUser });
        dto.CuserName = userDict.GetValueOrDefault(header.CUser, string.Empty);
        return dto;
    }

    protected override ErpSyncConfigDetailDto MapToDetailDto(TSynInfo header, List<TSynInfoentry> entries) => new()
    {
        Uid = header.Uid,
        Fnumber = header.Fnumber,
        Fname = header.Fname,
        Ferpbillid = header.Ferpbillid,
        Fstart = header.Fstart,
        Fsynrate = header.Fsynrate,
        Ftimetype = header.Ftimetype,
        Fruleid = header.Fruleid,
        Fisenable = header.Fisenable,
        Isdefault = header.Isdefault,
        Fnote = header.Fnote,
        Ftimestamp = header.Ftimestamp,
        FStatus = header.FStatus,
        CYmd = header.CYmd,
        CUser = header.CUser,
        MYmd = header.MYmd,
        MUser = header.MUser,
        Entities = entries.Select(MapEntityToDto).ToList()
    };

    private static ErpSyncEntityDto MapEntityToDto(TSynInfoentry e) => new()
    {
        Uid = e.Uid,
        Fentryid = e.Fentryid,
        Fdetailid = e.Fdetailid,
        Fismain = e.Fismain,
        Fsrcdatatype = e.Fsrcdatatype,
        Fsrcdataname = e.Fsrcdataname,
        Fsrcdatades = e.Fsrcdatades,
        Fsrcdatakey = e.Fsrcdatakey,
        Ftimestampname = e.Ftimestampname,
        Filter = e.Filter,
        Faimdatatype = e.Faimdatatype,
        Faimdataname = e.Faimdataname,
        Faimdatades = e.Faimdatades,
        Faimdatakey = e.Faimdatakey,
        Frelationfield = e.Frelationfield,
        Fiscover = e.Fiscover,
        Fieldkeys = e.Fieldkeys,
        Ferpbillid = e.Ferpbillid,
        Forderstring = e.Forderstring,
        Fruleid = e.Fruleid
    };

    private static ErpSyncFieldMapDto MapFieldToDto(TSynInfoentry1 f) => new()
    {
        Uid = f.Uid,
        Fentryid = f.Fentryid,
        Fdetailid = f.Fdetailid,
        Fbodyid = f.Fbodyid,
        Fsrcfield = f.Fsrcfield,
        Fsecondsrcfield = f.Fsecondsrcfield,
        Fthirdsrcfield = f.Fthirdsrcfield,
        Fsrcfielddes = f.Fsrcfielddes,
        Fissort = f.Fissort,
        Fissrcfieldkey = f.Fissrcfieldkey,
        Faimfield = f.Faimfield,
        Faimfieldtype = f.Faimfieldtype,
        Faimfielddes = f.Faimfielddes,
        Faimfieldkey = f.Faimfieldkey,
        Fmustinput = f.Fmustinput,
        Fdefaultvalue = f.Fdefaultvalue,
        Fissetfixed = f.Fissetfixed,
        Ffixedvalue = f.Ffixedvalue,
        Flookupclassid = f.Flookupclassid,
        Flookupfield = f.Flookupfield,
        Flookupsavefield = f.Flookupsavefield,
        Fisinnerid = f.Fisinnerid,
        Fislogshow = f.Fislogshow,
        Ffieldtype = f.Ffieldtype,
        Ferpseq = f.Ferpseq
    };

    // ===== 一主两从：重写 Create/Update/Delete =====

    public override async Task<ErpSyncConfigDetailDto> CreateAsync(CreateErpSyncConfigRequest request)
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
            if (string.IsNullOrEmpty(header.FCompanyId))
                header.FCompanyId = CurrentUser.CompanyId ?? string.Empty;

            await Db.Insertable(header).ExecuteCommandAsync();
            await PersistEntriesAsync(header, request.Entities);

            Db.AsTenant().CommitTran();

            if (OperationLog != null) await OperationLog.LogAsync(PrgKey, OperationType.Create, header.Uid);
            return (await GetByIdAsync(header.Uid))!;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    public override async Task<bool> UpdateAsync(string uid, UpdateErpSyncConfigRequest request)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("配置不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的配置不能修改，请先反审核");

        try
        {
            Db.AsTenant().BeginTran();

            UpdateHeaderEntity(header, request);
            header.MYmd = DateTime.Now;
            header.MUser = CurrentUser.UserId ?? string.Empty;
            await Db.Updateable(header).IgnoreColumns(e => new { e.CYmd, e.CUser }).ExecuteCommandAsync();

            // 物理删旧的两套从表，再重建
            await Db.Deleteable<TSynInfoentry>().Where(e => e.FInterId == uid).ExecuteCommandAsync();
            await Db.Deleteable<TSynInfoentry1>().Where(e => e.FInterId == uid).ExecuteCommandAsync();
            await PersistEntriesAsync(header, request.Entities);

            Db.AsTenant().CommitTran();

            if (OperationLog != null) await OperationLog.LogAsync(PrgKey, OperationType.Update, uid);
            return true;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    public override async Task<bool> DeleteAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("配置不存在");
        if (header.FStatus == 40) throw new InvalidOperationException("已审核的配置不能删除，请先反审核");

        try
        {
            Db.AsTenant().BeginTran();

            var result = await HeaderRepo.SoftDeleteAsync(uid);
            if (result)
            {
                await Db.Updateable<TSynInfoentry>()
                    .SetColumns(e => e.FDeleted == true).SetColumns(e => e.MYmd == DateTime.Now)
                    .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
                    .Where(e => e.FInterId == uid).ExecuteCommandAsync();
                await Db.Updateable<TSynInfoentry1>()
                    .SetColumns(e => e.FDeleted == true).SetColumns(e => e.MYmd == DateTime.Now)
                    .SetColumns(e => e.MUser == (CurrentUser.UserId ?? string.Empty))
                    .Where(e => e.FInterId == uid).ExecuteCommandAsync();
            }

            Db.AsTenant().CommitTran();

            if (result && OperationLog != null) await OperationLog.LogAsync(PrgKey, OperationType.Delete, uid);
            return result;
        }
        catch
        {
            Db.AsTenant().RollbackTran();
            throw;
        }
    }

    /// <summary>持久化两套从表（事务内）：实体信息 + 字段映射（Fbodyid 回填实体行 Fdetailid）</summary>
    private async Task PersistEntriesAsync(TSynInfo header, List<ErpSyncEntityRequest> entities)
    {
        // 目标唯一标识（Faimdatakey）为空会导致同步引擎对该实体抛「未配置目标唯一标识」并整表跳过
        // （"只同步主表不同步明细"的根因），故保存即校验：每个实体必须配置唯一标识键。
        var invalid = (entities ?? new()).FirstOrDefault(e => string.IsNullOrWhiteSpace(e.Faimdatakey));
        if (invalid != null)
            throw new ArgumentException($"实体「{(string.IsNullOrWhiteSpace(invalid.Faimdataname) ? "未选目标表" : invalid.Faimdataname)}」未配置「目标唯一标识」，该项为必录。");

        var now = DateTime.Now;
        var user = CurrentUser.UserId ?? string.Empty;
        var company = header.FCompanyId ?? string.Empty;

        var entryList = new List<TSynInfoentry>();
        var fieldList = new List<TSynInfoentry1>();
        int eIdx = 1;
        foreach (var e in entities ?? new())
        {
            var ent = MapEntity(e);
            Stamp(ent, header.Uid, now, user, company);
            ent.Fentryid = eIdx++;
            ent.Fdetailid = ent.Uid;
            entryList.Add(ent);

            int fIdx = 1;
            foreach (var f in e.FieldMaps ?? new())
            {
                var fm = MapField(f);
                Stamp(fm, header.Uid, now, user, company);
                fm.Fentryid = fIdx++;
                fm.Fdetailid = fm.Uid;
                fm.Fbodyid = ent.Fdetailid;
                fieldList.Add(fm);
            }
        }

        if (entryList.Count > 0) await Db.Insertable(entryList).ExecuteCommandAsync();
        if (fieldList.Count > 0) await Db.Insertable(fieldList).ExecuteCommandAsync();
    }

    private static void Stamp(BaseEntity e, string headerUid, DateTime now, string user, string company)
    {
        e.Uid = Guid.NewGuid().ToString("N");
        e.FInterId = headerUid;
        e.CYmd = now; e.CUser = user; e.MYmd = now; e.MUser = user;
        e.FCompanyId = company;
    }

    // ===== 写入映射 =====

    protected override TSynInfo MapToHeaderEntity(CreateErpSyncConfigRequest dto) => new()
    {
        Fnumber = dto.Fnumber?.Trim() ?? string.Empty,
        Fname = dto.Fname?.Trim() ?? string.Empty,
        Ferpbillid = dto.Ferpbillid?.Trim() ?? string.Empty,
        Fstart = dto.Fstart,
        Fsynrate = dto.Fsynrate,
        Ftimetype = string.IsNullOrEmpty(dto.Ftimetype) ? "1" : dto.Ftimetype,
        Fruleid = dto.Fruleid ?? string.Empty,
        Fisenable = dto.Fisenable,
        Isdefault = dto.Isdefault,
        Fnote = dto.Fnote ?? string.Empty,
        // FTIMESTAMP 列在开发库为 NOT NULL：从未同步用 1900 哨兵（<=1900 前端显示空、调度视为立即到点）
        Ftimestamp = new DateTime(1900, 1, 1),
        FStatus = 10
    };

    protected override void UpdateHeaderEntity(TSynInfo entity, UpdateErpSyncConfigRequest dto)
    {
        entity.Fnumber = dto.Fnumber?.Trim() ?? string.Empty;
        entity.Fname = dto.Fname?.Trim() ?? string.Empty;
        entity.Ferpbillid = dto.Ferpbillid?.Trim() ?? string.Empty;
        entity.Fstart = dto.Fstart;
        entity.Fsynrate = dto.Fsynrate;
        entity.Ftimetype = string.IsNullOrEmpty(dto.Ftimetype) ? "1" : dto.Ftimetype;
        entity.Fruleid = dto.Fruleid ?? string.Empty;
        entity.Fisenable = dto.Fisenable;
        entity.Isdefault = dto.Isdefault;
        entity.Fnote = dto.Fnote ?? string.Empty;
    }

    private static TSynInfoentry MapEntity(ErpSyncEntityRequest e) => new()
    {
        Fismain = e.Fismain,
        Fsrcdatatype = e.Fsrcdatatype,
        Fsrcdataname = e.Fsrcdataname ?? string.Empty,
        Fsrcdatades = e.Fsrcdatades ?? string.Empty,
        Fsrcdatakey = e.Fsrcdatakey ?? string.Empty,
        Ftimestampname = e.Ftimestampname ?? string.Empty,
        Filter = e.Filter ?? string.Empty,
        Faimdatatype = e.Faimdatatype == 0 ? 1 : e.Faimdatatype,
        Faimdataname = e.Faimdataname ?? string.Empty,
        Faimdatades = e.Faimdatades ?? string.Empty,
        Faimdatakey = e.Faimdatakey ?? string.Empty,
        Frelationfield = e.Frelationfield ?? string.Empty,
        Fiscover = e.Fiscover,
        Fieldkeys = e.Fieldkeys ?? string.Empty,
        Ferpbillid = e.Ferpbillid ?? string.Empty,
        Forderstring = e.Forderstring ?? string.Empty,
        Fruleid = e.Fruleid ?? string.Empty
    };

    private static TSynInfoentry1 MapField(ErpSyncFieldMapRequest f) => new()
    {
        Fsrcfield = f.Fsrcfield ?? string.Empty,
        Fsecondsrcfield = f.Fsecondsrcfield ?? string.Empty,
        Fthirdsrcfield = f.Fthirdsrcfield ?? string.Empty,
        Fsrcfielddes = f.Fsrcfielddes ?? string.Empty,
        Fissort = f.Fissort,
        Fissrcfieldkey = f.Fissrcfieldkey,
        Faimfield = f.Faimfield ?? string.Empty,
        Faimfieldtype = f.Faimfieldtype ?? string.Empty,
        Faimfielddes = f.Faimfielddes ?? string.Empty,
        Faimfieldkey = f.Faimfieldkey,
        Fmustinput = f.Fmustinput,
        Fdefaultvalue = f.Fdefaultvalue ?? string.Empty,
        Fissetfixed = f.Fissetfixed,
        Ffixedvalue = f.Ffixedvalue ?? string.Empty,
        Flookupclassid = f.Flookupclassid ?? string.Empty,
        Flookupfield = f.Flookupfield ?? string.Empty,
        Flookupsavefield = f.Flookupsavefield ?? string.Empty,
        Fisinnerid = f.Fisinnerid,
        Fislogshow = f.Fislogshow,
        Ffieldtype = f.Ffieldtype,
        Ferpseq = f.Ferpseq ?? string.Empty
    };

    // 基类抽象：这里两从表由重写的 Create/Update 自管，此契约方法返回实体行（不含字段映射）
    protected override List<TSynInfoentry> MapToEntryEntities(CreateErpSyncConfigRequest dto, string headerUid)
        => (dto.Entities ?? new()).Select(MapEntity).ToList();

    protected override List<TSynInfoentry> MapToEntryEntities(UpdateErpSyncConfigRequest dto, string headerUid)
        => (dto.Entities ?? new()).Select(MapEntity).ToList();

    protected override void SetEntryIndex(TSynInfoentry entry, int index)
    {
        entry.Fentryid = index;
        entry.Fdetailid = entry.Uid;
    }

    protected override async Task<List<TSynInfoentry>> GetEntriesByHeaderIdAsync(string headerUid)
        => await Db.Queryable<TSynInfoentry>()
            .Where(e => e.FInterId == headerUid && !e.FDeleted)
            .OrderBy(e => e.Fentryid)
            .ToListAsync();

    // ===== 手动同步 =====

    public async Task<ErpSyncRunResultDto> RunSyncAsync(string uid)
    {
        var header = await HeaderRepo.GetByIdAsync(uid);
        if (header == null || header.FDeleted) throw new KeyNotFoundException("配置不存在");
        if (header.FStatus != 40) throw new InvalidOperationException("请先审核配置后再执行同步");
        return await _engine.RunAsync(uid);
    }

    // ===== 目标目录（反射驱动 UI 下拉） =====

    private static readonly Lazy<List<Type>> DomainEntityTypes = new(() =>
        Assembly.Load("OPSOFT.O3.WebAPI.Domain").GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes(typeof(SugarTable), true).Length > 0)
            .ToList());

    public Task<List<ErpTargetTableDto>> GetTargetTablesAsync(string? keyword = null)
    {
        var kw = keyword?.Trim();
        var list = DomainEntityTypes.Value
            .Select(t => new ErpTargetTableDto
            {
                TableName = ((SugarTable)t.GetCustomAttributes(typeof(SugarTable), true)[0]).TableName,
                EntityName = t.Name,
                Description = string.Empty
            })
            .Where(x => string.IsNullOrEmpty(kw)
                || x.TableName.Contains(kw, StringComparison.OrdinalIgnoreCase)
                || x.EntityName.Contains(kw, StringComparison.OrdinalIgnoreCase))
            .OrderBy(x => x.TableName)
            .ToList();
        return Task.FromResult(list);
    }

    public Task<List<ErpTargetFieldDto>> GetTargetFieldsAsync(string tableName)
    {
        var type = DomainEntityTypes.Value.FirstOrDefault(t =>
            string.Equals(((SugarTable)t.GetCustomAttributes(typeof(SugarTable), true)[0]).TableName, tableName, StringComparison.OrdinalIgnoreCase));
        if (type == null) return Task.FromResult(new List<ErpTargetFieldDto>());

        var fields = new List<ErpTargetFieldDto>();
        foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            // 仅可读写属性才是真实列：与引擎 ResolveColumnMap 口径一致，避免把只读计算属性当作可选目标字段。
            if (!p.CanRead || !p.CanWrite) continue;
            var col = p.GetCustomAttributes(typeof(SugarColumn), true).FirstOrDefault() as SugarColumn;
            if (col != null && col.IsIgnore) continue;
            var colName = col?.ColumnName ?? p.Name;
            if (string.IsNullOrEmpty(colName)) continue;
            fields.Add(new ErpTargetFieldDto
            {
                FieldName = colName,
                FieldType = ClrToFieldType(p.PropertyType),
                Description = string.Empty,
                IsPrimaryKey = col?.IsPrimaryKey ?? false
            });
        }
        return Task.FromResult(fields.OrderBy(f => f.FieldName).ToList());
    }

    public async Task<List<ErpBillTemplateDto>> GetBillTemplatesAsync(string? keyword = null)
    {
        var q = Db.Queryable<SysBillTemplate>().Where(t => !t.FDeleted);
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var kw = keyword.Trim();
            q = q.Where(t => t.Fnumber.Contains(kw) || t.Fname.Contains(kw));
        }
        var rows = await q.OrderBy(t => t.Fnumber).Take(200)
            .Select(t => new ErpBillTemplateDto { Fnumber = t.Fnumber, Fname = t.Fname }).ToListAsync();
        return rows;
    }

    private static string ClrToFieldType(Type t)
    {
        t = Nullable.GetUnderlyingType(t) ?? t;
        if (t == typeof(string)) return "NVARCHAR";
        if (t == typeof(bool)) return "BIT";
        if (t == typeof(int) || t == typeof(short) || t == typeof(byte)) return "INT";
        if (t == typeof(long)) return "BIGINT";
        if (t == typeof(decimal) || t == typeof(double) || t == typeof(float)) return "DECIMAL";
        if (t == typeof(DateTime)) return "DATETIME";
        return "NVARCHAR";
    }

    // ===== 名称字典 =====

    private async Task<Dictionary<int, string>> LoadStatusDictAsync()
    {
        var rows = await Db.Queryable<SysStatus>().Select(s => new { s.Fitemid, s.Fname }).ToListAsync();
        return rows.GroupBy(r => r.Fitemid).ToDictionary(g => g.Key, g => g.First().Fname);
    }

    private async Task<Dictionary<string, string>> LoadUserNameDictAsync(IEnumerable<string> ids)
    {
        var list = ids.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
        if (list.Count == 0) return new();
        var rows = await Db.Queryable<SysLoginUser>().Where(u => list.Contains(u.UserId))
            .Select(u => new { u.UserId, u.UserName }).ToListAsync();
        return rows.GroupBy(r => r.UserId).ToDictionary(g => g.Key, g => g.First().UserName);
    }

    private static string TimeTypeName(string t) => t switch
    {
        "1" => "秒", "2" => "分", "3" => "小时", "4" => "天", _ => string.Empty
    };
}
