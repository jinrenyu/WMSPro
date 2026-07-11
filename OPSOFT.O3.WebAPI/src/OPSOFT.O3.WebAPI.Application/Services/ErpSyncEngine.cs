using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 配置驱动的通用同步引擎：读取一个同步任务（T_SYN_INFO + 实体信息 + 字段映射），
/// 逐个实体调用 IErpDataSource 取数，按字段映射构造目标行并动态 upsert 到目标表（.AS(表名)）。
/// 目标列的 CLR 类型经反射目标实体获得，用于把 ERP 源值转成正确类型写入。
/// </summary>
public class ErpSyncEngine : IErpSyncEngine
{
    private readonly ISqlSugarClient _db;
    private readonly IErpDataSource _erp;
    private readonly ICurrentUserService _currentUser;

    private static readonly Regex IdentifierRegex = new("^[A-Za-z0-9_]+$", RegexOptions.Compiled);
    private static readonly DateTime DateSentinel = new(1900, 1, 1);

    // 单配置串行闸：定时调度与手动同步（不同 scope/连接）对同一配置并发跑时，
    // 避免 check-then-act 竞态重复插入 / SQLite 'database is locked'。进程内静态共享。
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> SyncLocks = new();

    public ErpSyncEngine(ISqlSugarClient db, IErpDataSource erp, ICurrentUserService currentUser)
    {
        _db = db;
        _erp = erp;
        _currentUser = currentUser;
    }

    public async Task<ErpSyncRunResultDto> RunAsync(string configUid)
    {
        var gate = SyncLocks.GetOrAdd(configUid, _ => new SemaphoreSlim(1, 1));
        await gate.WaitAsync();
        TSynInfo? header = null;
        var sw = Stopwatch.StartNew();
        try
        {
            header = await _db.Queryable<TSynInfo>().InSingleAsync(configUid);
            if (header == null || header.FDeleted) throw new KeyNotFoundException("配置不存在");

            // 同步落库的组织归属：用配置头 FCompanyId（定时/手动两路径一致，后台无 HttpContext 也稳定），
            // 为空才回落当前登录组织。
            var orgId = string.IsNullOrEmpty(header.FCompanyId) ? (_currentUser.CompanyId ?? string.Empty) : header.FCompanyId;

            var entities = await _db.Queryable<TSynInfoentry>()
                .Where(e => e.FInterId == configUid && !e.FDeleted).OrderBy(e => e.Fentryid).ToListAsync();
            var fields = await _db.Queryable<TSynInfoentry1>()
                .Where(f => f.FInterId == configUid && !f.FDeleted).ToListAsync();
            var fieldsByBody = fields.GroupBy(f => f.Fbodyid).ToDictionary(g => g.Key, g => g.OrderBy(x => x.Fentryid).ToList());

            int okTotal = 0, failTotal = 0;
            var errors = new List<string>();

            foreach (var ent in entities)
            {
                var maps = fieldsByBody.GetValueOrDefault(ent.Fdetailid) ?? new List<TSynInfoentry1>();
                var formId = string.IsNullOrEmpty(ent.Ferpbillid) ? header.Ferpbillid : ent.Ferpbillid;
                var filter = string.IsNullOrEmpty(ent.Fruleid) ? header.Fruleid : ent.Fruleid;
                try
                {
                    var qr = await _erp.ExecuteBillQueryAsync(formId, ent.Fieldkeys, filter, ent.Forderstring);
                    var (ok, fail, errs) = await UpsertEntityAsync(ent, maps, qr, orgId);
                    okTotal += ok; failTotal += fail; errors.AddRange(errs);
                }
                catch (Exception ex)
                {
                    failTotal++;
                    errors.Add($"实体[{ent.Faimdataname}]同步失败：{ex.Message}");
                }
            }

            sw.Stop();
            var success = failTotal == 0;
            var message = success
                ? $"同步完成，成功 {okTotal} 条"
                : $"成功 {okTotal} 条，失败 {failTotal} 条；{string.Join("；", errors.Take(5))}";

            // 仅同步成功才推进「最后同步时间」水位——失败不推进，避免（将来增量同步时）把未成功的时间窗标记为已同步致漏数。
            if (success)
                await _db.Updateable<TSynInfo>()
                    .SetColumns(h => h.Ftimestamp == DateTime.Now)
                    .Where(h => h.Uid == configUid)
                    .ExecuteCommandAsync();

            var logUid = await WriteLogAsync(header, success, okTotal, failTotal, message);
            return new ErpSyncRunResultDto
            {
                Success = success,
                SuccessCount = okTotal,
                FailCount = failTotal,
                Message = message,
                LogUid = logUid,
                ElapsedMs = sw.ElapsedMilliseconds
            };
        }
        catch (Exception ex)
        {
            // 早期失败（取配置/明细查询抛错等）：仍写一条失败日志（有审计痕迹），不推进时间戳。
            var logUid = string.Empty;
            if (header != null)
            {
                try { logUid = await WriteLogAsync(header, false, 0, 0, $"同步异常：{ex.Message}"); } catch { /* 日志失败不掩盖原异常 */ }
            }
            return new ErpSyncRunResultDto
            {
                Success = false,
                FailCount = 1,
                Message = $"同步失败：{ex.Message}",
                LogUid = logUid,
                ElapsedMs = sw.ElapsedMilliseconds
            };
        }
        finally
        {
            gate.Release();
        }
    }

    /// <summary>按一个实体的字段映射，将查询结果逐行 upsert 到目标表</summary>
    private async Task<(int ok, int fail, List<string> errors)> UpsertEntityAsync(
        TSynInfoentry ent, List<TSynInfoentry1> maps, ErpQueryResult qr, string orgId)
    {
        var errors = new List<string>();
        var table = ent.Faimdataname?.Trim() ?? string.Empty;
        if (!IdentifierRegex.IsMatch(table))
            throw new InvalidOperationException($"非法目标表名：{table}");

        var cols = ResolveColumnMap(table); // UPPER(col) -> (ColName, ClrType)
        var keyCols = (ent.Faimdatakey ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(c => c.ToUpperInvariant())
            .Where(c => IdentifierRegex.IsMatch(c))
            .ToList();
        if (keyCols.Count == 0)
            throw new InvalidOperationException("未配置目标唯一标识（Faimdatakey）");

        var fieldIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < qr.Fields.Count; i++) fieldIndex[qr.Fields[i]] = i;

        int ok = 0, fail = 0;
        foreach (var row in qr.Rows)
        {
            try
            {
                var biz = BuildRow(maps, row, fieldIndex, cols);
                // 唯一标识列归一化：把缺失或源值为 NULL 的 key 列，归一为「最终会落库的值」——
                // FCOMPANYID→配置组织、其余系统/普通列→CLR 类型默认（与 FillInsertSystemColumns 一致）。
                // 关键：存在性判定必须用与插入相同的最终值，否则 key 列取自源缺失字段(如 FCOMPANYID/FDELETED)时
                // 判定用 NULL 而落库用默认 → 永不匹配 → 反复 INSERT 同一 UID → 撞唯一约束。
                foreach (var k in keyCols)
                {
                    var colName = ResolveColName(cols, k);
                    var hasVal = biz.TryGetValue(colName, out var kv) && kv != null && kv != DBNull.Value;
                    if (hasVal) continue;
                    if (k == "FCOMPANYID" && cols.ContainsKey("FCOMPANYID")) { biz[colName] = orgId; continue; }
                    if (cols.TryGetValue(k, out var meta)) { biz[colName] = DefaultForClr(meta.Clr); continue; }
                    throw new InvalidOperationException($"唯一标识列 {k} 不是目标表的列");
                }

                var exists = await ExistsByKeysAsync(table, keyCols, cols, biz);
                if (exists)
                {
                    if (!ent.Fiscover) continue; // 已存在且不覆盖 → 跳过
                    var upd = new Dictionary<string, object>(biz, StringComparer.OrdinalIgnoreCase);
                    AddIfExists(upd, cols, "M_YMD", DateTime.Now);
                    AddIfExists(upd, cols, "M_USER", CurrentUserId);
                    await _db.Updateable(upd).AS(table)
                        .WhereColumns(keyCols.Select(k => ResolveColName(cols, k)).ToArray())
                        .ExecuteCommandAsync();
                }
                else
                {
                    var ins = new Dictionary<string, object>(biz, StringComparer.OrdinalIgnoreCase);
                    FillInsertSystemColumns(ins, cols, orgId);
                    await _db.Insertable(ins).AS(table).ExecuteCommandAsync();
                }
                ok++;
            }
            catch (Exception ex)
            {
                fail++;
                errors.Add(ex.Message);
            }
        }
        return (ok, fail, errors);
    }

    /// <summary>按字段映射把一行源数据构造成 目标列名->值 的字典（null 以 DBNull 占位）</summary>
    private static Dictionary<string, object> BuildRow(
        List<TSynInfoentry1> maps, object?[] row, Dictionary<string, int> fieldIndex,
        Dictionary<string, (string ColName, Type Clr)> cols)
    {
        var dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        foreach (var m in maps)
        {
            var target = m.Faimfield?.Trim();
            if (string.IsNullOrEmpty(target)) continue;

            object? val;
            if (m.Fissetfixed)
            {
                val = m.Ffixedvalue;
            }
            else
            {
                object? sv = null;
                if (!string.IsNullOrEmpty(m.Fsrcfield)
                    && fieldIndex.TryGetValue(m.Fsrcfield.Trim(), out var idx)
                    && idx < row.Length)
                    sv = row[idx];
                if ((sv == null || (sv is string s && s.Length == 0)) && !string.IsNullOrEmpty(m.Fdefaultvalue))
                    sv = m.Fdefaultvalue;
                val = sv;
                // 关联取值/仓库/仓位/自定义（Ffieldtype!=0）在此按 Flookup* 解析为目标 Uid —
                // 物料首验以标准字段(Ffieldtype=0)为主，非标准字段暂原样透传，后续增强。
            }

            if (cols.TryGetValue(target.ToUpperInvariant(), out var meta))
                dict[meta.ColName] = ConvertValue(val, meta.Clr) ?? (object)DBNull.Value;
            else
                dict[target] = val ?? (object)DBNull.Value; // 目标列不在实体元数据中（自定义），原样写
        }
        return dict;
    }

    private async Task<bool> ExistsByKeysAsync(string table, List<string> keyCols,
        Dictionary<string, (string ColName, Type Clr)> cols, Dictionary<string, object> biz)
    {
        var conds = new List<string>();
        var pars = new List<SugarParameter>();
        for (int i = 0; i < keyCols.Count; i++)
        {
            var colName = ResolveColName(cols, keyCols[i]);
            biz.TryGetValue(colName, out var v);
            conds.Add($"{colName} = @k{i}");
            pars.Add(new SugarParameter($"@k{i}", v ?? DBNull.Value));
        }
        var sql = $"SELECT COUNT(1) FROM {table} WHERE {string.Join(" AND ", conds)}";
        var cnt = await _db.Ado.GetIntAsync(sql, pars);
        return cnt > 0;
    }

    private void FillInsertSystemColumns(Dictionary<string, object> dict, Dictionary<string, (string ColName, Type Clr)> cols, string orgId)
    {
        var now = DateTime.Now;
        var user = CurrentUserId;
        // UID 是 WMS 自有主键：恒取新 GUID（除非配置显式把某源字段映射到 UID）；绝不从 FINTERID 继承。
        // 否则配置把 ERP 内码映射到 FINTERID 时 UID 会变成 ERP 内码(固定值)，遇软删/换组织等"业务键不匹配"的
        // 存在性判定会误判"不存在"→INSERT→撞已存在的固定 UID(唯一约束失败)。
        // FINTERID 保留其映射值(ERP 内码，供追溯/将来按内码增量)，未映射时才回落 = UID。
        var uidCol = ResolveColName(cols, "UID");
        string uidVal = Guid.NewGuid().ToString("N");
        if (cols.ContainsKey("UID") && dict.TryGetValue(uidCol, out var uv) && uv is string us && us.Length > 0)
            uidVal = us;
        if (cols.ContainsKey("UID")) dict[uidCol] = uidVal;
        if (cols.ContainsKey("FINTERID"))
        {
            var fidCol = ResolveColName(cols, "FINTERID");
            if (!(dict.TryGetValue(fidCol, out var fv) && fv is string fs && fs.Length > 0))
                dict[fidCol] = uidVal;
        }

        AddIfMissing(dict, cols, "FGROUPID", string.Empty);
        AddIfMissing(dict, cols, "FSTATUS", 0);
        AddIfMissing(dict, cols, "FDELETED", false);
        AddIfMissing(dict, cols, "FDISABLED", false);
        AddIfMissing(dict, cols, "FCOMPANYID", orgId);
        AddIfMissing(dict, cols, "C_YMD", now);
        AddIfMissing(dict, cols, "C_USER", user);
        AddIfMissing(dict, cols, "M_YMD", now);
        AddIfMissing(dict, cols, "M_USER", user);
        // 其余列按 CLR 类型补默认值（目标表多为 NOT NULL：DateTime→1900 哨兵、string→""、数值→0），
        // 与手写新增单据"全列赋值"一致，避免 NOT NULL 约束失败。
        // 注意：已映射但源值为 NULL 的列在 dict 中是 DBNull，也一并回填默认（否则生产 NOT NULL 列插入失败）。
        foreach (var kv in cols)
        {
            var colName = kv.Value.ColName;
            if (dict.TryGetValue(colName, out var ev) && ev != DBNull.Value) continue;
            dict[colName] = DefaultForClr(kv.Value.Clr);
        }
    }

    private static object DefaultForClr(Type t)
    {
        if (t == typeof(string)) return string.Empty;
        if (t == typeof(DateTime)) return DateSentinel;
        if (t == typeof(bool)) return false;
        if (t == typeof(Guid)) return Guid.Empty;
        // 二进制/图片列（IMAGE/BLOB，如职员/物料 FPICTURE）补空字节数组而非 NULL：
        // 部分目标表该列 NOT NULL（开发库 T_HR_EMPINFO.FPICTURE），补 NULL 会撞 NOT NULL 约束；空 blob 语义=无图，对可空列亦无害。
        if (t == typeof(byte[])) return Array.Empty<byte>();
        if (t.IsValueType) return Activator.CreateInstance(t)!; // int/decimal/... → 0
        return DBNull.Value; // 其余引用类型 → NULL
    }

    private async Task<string> WriteLogAsync(TSynInfo header, bool success, int ok, int fail, string message)
    {
        var now = DateTime.Now;
        var log = new TBdSynlog
        {
            Uid = Guid.NewGuid().ToString("N"),
            Finterno = header.Fnumber,
            Fintername = header.Fname,
            Fformid = header.Ferpbillid,
            Fisasync = 1,
            Fsendtime = now,
            Freceivetime = now,
            Fissuccess = success,
            Fdealtimes = 1,
            Fmessage = message.Length > 2000 ? message[..2000] : message,
            Fguid = header.Uid,
            FCompanyId = header.FCompanyId,
            CYmd = now,
            CUser = CurrentUserId,
            MYmd = now,
            MUser = CurrentUserId
        };
        log.FInterId = log.Uid;
        await _db.Insertable(log).ExecuteCommandAsync();
        return log.Uid;
    }

    // ===== 反射：目标表列元数据 =====

    private static readonly Lazy<List<Type>> DomainEntityTypes = new(() =>
        Assembly.Load("OPSOFT.O3.WebAPI.Domain").GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes(typeof(SugarTable), true).Length > 0)
            .ToList());

    private static readonly Dictionary<string, Dictionary<string, (string ColName, Type Clr)>> ColMapCache = new(StringComparer.OrdinalIgnoreCase);
    private static readonly object CacheLock = new();

    private static Dictionary<string, (string ColName, Type Clr)> ResolveColumnMap(string table)
    {
        lock (CacheLock)
        {
            if (ColMapCache.TryGetValue(table, out var cached)) return cached;
            var type = DomainEntityTypes.Value.FirstOrDefault(t =>
                string.Equals(((SugarTable)t.GetCustomAttributes(typeof(SugarTable), true)[0]).TableName, table, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException($"目标表 {table} 未找到对应实体");
            var map = new Dictionary<string, (string, Type)>(StringComparer.OrdinalIgnoreCase);
            foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // 仅可读写属性才是真实列：跳过只读计算属性，避免扩展新实体时把非列属性当列致动态 Insert "no such column"。
                if (!p.CanRead || !p.CanWrite) continue;
                var col = p.GetCustomAttributes(typeof(SugarColumn), true).FirstOrDefault() as SugarColumn;
                if (col != null && col.IsIgnore) continue;
                var colName = col?.ColumnName ?? p.Name;
                if (string.IsNullOrEmpty(colName)) continue;
                map[colName.ToUpperInvariant()] = (colName, Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType);
            }
            ColMapCache[table] = map;
            return map;
        }
    }

    private static string ResolveColName(Dictionary<string, (string ColName, Type Clr)> cols, string upper)
        => cols.TryGetValue(upper, out var m) ? m.ColName : upper;

    private static void AddIfMissing(Dictionary<string, object> dict, Dictionary<string, (string ColName, Type Clr)> cols, string upper, object value)
    {
        if (!cols.ContainsKey(upper)) return;
        var colName = ResolveColName(cols, upper);
        if (!dict.TryGetValue(colName, out var ev) || ev == DBNull.Value) dict[colName] = value;
    }

    private static void AddIfExists(Dictionary<string, object> dict, Dictionary<string, (string ColName, Type Clr)> cols, string upper, object value)
    {
        if (!cols.ContainsKey(upper)) return;
        dict[ResolveColName(cols, upper)] = value;
    }

    private static object? ConvertValue(object? val, Type target)
    {
        if (val == null || val is DBNull) return null;
        target = Nullable.GetUnderlyingType(target) ?? target;
        var inv = CultureInfo.InvariantCulture;
        try
        {
            if (target == typeof(string)) return val.ToString();
            var s = val.ToString()?.Trim() ?? string.Empty;
            if (target == typeof(bool))
                return s is "1" or "true" or "True" or "TRUE" or "Y" or "是";
            if (s.Length == 0)
                // 空值转值类型：DateTime 用 1900 哨兵（与 DefaultForClr 一致，避免 0001 触发生产 DATETIME 溢出），其余给类型默认。
                return target == typeof(DateTime) ? DateSentinel : (target.IsValueType ? Activator.CreateInstance(target) : null);
            if (target == typeof(int) || target == typeof(short) || target == typeof(byte))
                return Convert.ToInt32(decimal.Parse(s, NumberStyles.Any, inv));
            if (target == typeof(long)) return Convert.ToInt64(decimal.Parse(s, NumberStyles.Any, inv));
            if (target == typeof(decimal)) return decimal.Parse(s, NumberStyles.Any, inv);
            if (target == typeof(double)) return double.Parse(s, NumberStyles.Any, inv);
            if (target == typeof(float)) return float.Parse(s, NumberStyles.Any, inv);
            if (target == typeof(DateTime)) return DateTime.Parse(s, inv, DateTimeStyles.None);
            if (target == typeof(Guid)) return Guid.Parse(s);
            return Convert.ChangeType(val, target, inv);
        }
        catch
        {
            // 转换失败：字符串列原样、DateTime 用 1900 哨兵、其余值类型给默认，避免整批中断，也避免 0001 溢出。
            if (target == typeof(string)) return val.ToString();
            if (target == typeof(DateTime)) return DateSentinel;
            return target.IsValueType ? Activator.CreateInstance(target) : null;
        }
    }

    private string CurrentUserId => _currentUser.UserId ?? "system";
}
