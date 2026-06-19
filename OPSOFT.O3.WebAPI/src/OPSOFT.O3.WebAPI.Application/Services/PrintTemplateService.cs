using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class PrintTemplateService : CrudService<TBdPrintTemplate, PrintTemplateListDto, PrintTemplateDetailDto, CreatePrintTemplateRequest, UpdatePrintTemplateRequest>
{
    private readonly ISqlSugarClient _db;

    public PrintTemplateService(
        IRepository<TBdPrintTemplate> repository,
        ISqlSugarClient db,
        IOperationLogService? operationLog = null)
        : base(repository, operationLog)
    {
        _db = db;
    }

    protected override string PrgKey => "PrintTemplate";

    /// <summary>
    /// 适用单据来源（表单键）→ 中文名映射
    /// </summary>
    private static readonly Dictionary<string, string> BillSourceNames = new()
    {
        ["PUR_PurchaseOrder"] = "采购订单标签",
        ["PUR_ReceiveBill"] = "收料通知单标签",
    };

    private static string ResolveBillSourceName(string billSource)
        => !string.IsNullOrEmpty(billSource) && BillSourceNames.TryGetValue(billSource, out var name) ? name : billSource;

    protected override Expression<Func<TBdPrintTemplate, bool>> BuildSearchPredicate(string keyword)
    {
        return t => t.FNumber.Contains(keyword) || t.FName.Contains(keyword);
    }

    /// <summary>
    /// 按适用单据来源取启用的模板（含模板 JSON），默认模板排在最前，供打印页使用
    /// </summary>
    public async Task<List<PrintTemplateDetailDto>> GetBySourceAsync(string billSource)
    {
        var list = await Repository.GetListAsync(t => !t.FDeleted && !t.FDisabled && t.FBillSource == billSource);
        return list
            .OrderByDescending(t => t.FIsDefault)
            .ThenBy(t => t.FNumber)
            .Select(MapToDetailDto)
            .ToList();
    }

    public override async Task<PrintTemplateDetailDto> CreateAsync(CreatePrintTemplateRequest request)
    {
        var detail = await base.CreateAsync(request);
        if (request.FIsDefault)
            await ClearOtherDefaultsAsync(request.FBillSource, detail.Uid);
        return detail;
    }

    public override async Task<bool> UpdateAsync(string uid, UpdatePrintTemplateRequest request)
    {
        var result = await base.UpdateAsync(uid, request);
        if (result && request.FIsDefault)
            await ClearOtherDefaultsAsync(request.FBillSource, uid);
        return result;
    }

    /// <summary>
    /// 同一单据来源仅保留当前模板为默认，其余置非默认
    /// </summary>
    private async Task ClearOtherDefaultsAsync(string billSource, string keepUid)
    {
        if (string.IsNullOrEmpty(billSource)) return;
        await _db.Updateable<TBdPrintTemplate>()
            .SetColumns(t => new TBdPrintTemplate { FIsDefault = false, MYmd = DateTime.Now })
            .Where(t => t.FBillSource == billSource && t.Uid != keepUid && !t.FDeleted && t.FIsDefault)
            .ExecuteCommandAsync();
    }

    protected override PrintTemplateListDto MapToListDto(TBdPrintTemplate entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FBillSource = entity.FBillSource,
        FBillSourceName = ResolveBillSourceName(entity.FBillSource),
        FPaperWidth = entity.FPaperWidth,
        FPaperHeight = entity.FPaperHeight,
        FIsDefault = entity.FIsDefault,
        FDescription = entity.FDescription,
        CYmd = entity.CYmd
    };

    protected override PrintTemplateDetailDto MapToDetailDto(TBdPrintTemplate entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FBillSource = entity.FBillSource,
        FBillSourceName = ResolveBillSourceName(entity.FBillSource),
        FPaperWidth = entity.FPaperWidth,
        FPaperHeight = entity.FPaperHeight,
        FIsDefault = entity.FIsDefault,
        FDescription = entity.FDescription,
        CYmd = entity.CYmd,
        FTemplate = entity.FTemplate,
        // ---- 系统信息（只读）----
        FCompanyId = entity.FCompanyId,
        CUser = entity.CUser,
        MUser = entity.MUser,
        MYmd = entity.MYmd
    };

    protected override TBdPrintTemplate MapToEntity(CreatePrintTemplateRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FBillSource = dto.FBillSource,
        FTemplate = dto.FTemplate,
        FPaperWidth = dto.FPaperWidth,
        FPaperHeight = dto.FPaperHeight,
        FIsDefault = dto.FIsDefault,
        FDescription = dto.FDescription
    };

    protected override void UpdateEntity(TBdPrintTemplate entity, UpdatePrintTemplateRequest dto)
    {
        entity.FName = dto.FName;
        entity.FBillSource = dto.FBillSource;
        entity.FTemplate = dto.FTemplate;
        entity.FPaperWidth = dto.FPaperWidth;
        entity.FPaperHeight = dto.FPaperHeight;
        entity.FIsDefault = dto.FIsDefault;
        entity.FDescription = dto.FDescription;
    }
}
