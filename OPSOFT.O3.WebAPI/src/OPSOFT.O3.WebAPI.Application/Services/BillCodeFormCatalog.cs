using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 编码规则业务表单目录实现（SYS_BILLCODEFORM）。数据驱动"哪些单据可配编码规则"，
/// 取代写死的 BillCodeFieldRegistry.Forms；字段段白名单仍在代码侧 BillCodeFieldRegistry。
/// </summary>
public class BillCodeFormCatalog : IBillCodeFormCatalog
{
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;

    public BillCodeFormCatalog(ISqlSugarClient db, ICurrentUserService currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<List<BillCodeFormDto>> GetFormsAsync()
    {
        var rows = await _db.Queryable<SysBillCodeForm>().Where(f => !f.FDeleted)
            .OrderBy(f => f.Fformname).ToListAsync();
        return rows.Select(Map).ToList();
    }

    public async Task<List<BillCodeDocumentDto>> GetAvailableDocumentsAsync()
    {
        var registered = await _db.Queryable<SysBillCodeForm>().Where(f => !f.FDeleted).ToListAsync();
        // 反射发现的单据类型 ∪ 登记状态：已登记的用目录实际 formKey/名称，未登记的用建议值
        return DocumentTypeRegistry.Discover().Select(d =>
        {
            var reg = registered.FirstOrDefault(r => r.Fentityname == d.EntityName);
            return new BillCodeDocumentDto
            {
                EntityName = d.EntityName,
                TableName = d.TableName,
                FormName = reg?.Fformname ?? d.SuggestedName,
                FormKey = reg?.Fformkey ?? d.SuggestedFormKey,
                Registered = reg != null
            };
        }).ToList();
    }

    public async Task<BillCodeFormDto?> GetFormAsync(string formKey)
    {
        var row = await _db.Queryable<SysBillCodeForm>()
            .Where(f => f.Fformkey == formKey && !f.FDeleted).FirstAsync();
        return row == null ? null : Map(row);
    }

    public async Task<string?> GetFormNameAsync(string formKey)
        => (await GetFormAsync(formKey))?.FormName;

    public async Task<string?> ResolveFormKeyByEntityAsync(string entityName)
    {
        if (string.IsNullOrWhiteSpace(entityName)) return null;
        // 实体名命中即返回对应 formKey；未登记或未填实体名返回 null（基类据此跳过取号）
        return await _db.Queryable<SysBillCodeForm>()
            .Where(f => f.Fentityname == entityName && !f.FDeleted)
            .Select(f => f.Fformkey).FirstAsync();
    }

    public async Task RegisterAsync(BillCodeFormRegisterRequest request)
    {
        var formKey = request.FormKey?.Trim() ?? string.Empty;
        var formName = request.FormName?.Trim() ?? string.Empty;
        var entityName = request.EntityName?.Trim() ?? string.Empty;
        if (formKey.Length == 0) throw new InvalidOperationException("业务表单标识（FormKey）不能为空");
        if (formName.Length == 0) throw new InvalidOperationException("表单名称不能为空");
        if (formKey.Length > 100) throw new InvalidOperationException("业务表单标识不能超过 100 字符");
        if (formName.Length > 100) throw new InvalidOperationException("表单名称不能超过 100 字符");
        if (entityName.Length > 100) throw new InvalidOperationException("实体类名不能超过 100 字符");

        var now = DateTime.Now;
        var user = _currentUser.UserId ?? string.Empty;
        // 含软删行查重：注销后重新登记同 key 复活原行，避免唯一索引冲突
        var existing = await _db.Queryable<SysBillCodeForm>().Where(f => f.Fformkey == formKey).FirstAsync();
        if (existing == null)
        {
            var uid = Guid.NewGuid().ToString("N");
            await _db.Insertable(new SysBillCodeForm
            {
                Uid = uid, FInterId = uid, Fformkey = formKey, Fformname = formName, Fentityname = entityName,
                FStatus = 40, FCompanyId = "DEFAULT",
                CYmd = now, CUser = user, MYmd = now, MUser = user
            }).ExecuteCommandAsync();
        }
        else
        {
            existing.Fformname = formName;
            existing.Fentityname = entityName;
            existing.FDeleted = false;
            existing.MYmd = now;
            existing.MUser = user;
            await _db.Updateable(existing).IgnoreColumns(e => new { e.CYmd, e.CUser }).ExecuteCommandAsync();
        }
    }

    public async Task UnregisterAsync(string formKey)
    {
        var now = DateTime.Now;
        var user = _currentUser.UserId ?? string.Empty;
        await _db.Updateable<SysBillCodeForm>()
            .SetColumns(f => f.FDeleted == true).SetColumns(f => f.MYmd == now).SetColumns(f => f.MUser == user)
            .Where(f => f.Fformkey == formKey && !f.FDeleted).ExecuteCommandAsync();
    }

    private static BillCodeFormDto Map(SysBillCodeForm f) => new()
    {
        FormKey = f.Fformkey, FormName = f.Fformname, EntityName = f.Fentityname
    };
}
