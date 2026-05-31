using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 仓位（仓位集 T_BAS_FLEXVALUES）主数据服务，含值列表（T_BAS_FLEXVALUESENTRY）
/// </summary>
public class FlexValuesService : ApprovableDisableableCrudService<TBasFlexvalues, FlexValuesListDto, FlexValuesDetailDto, CreateFlexValuesRequest, UpdateFlexValuesRequest>
{
    private readonly IRepository<SysOrgStructure> _orgRepo;
    private readonly IRepository<TBasFlexvaluesentry> _entryRepo;

    public FlexValuesService(
        IRepository<TBasFlexvalues> repository,
        IRepository<SysOrgStructure> orgRepo,
        IRepository<TBasFlexvaluesentry> entryRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog)
    {
        _orgRepo = orgRepo;
        _entryRepo = entryRepo;
    }

    protected override string PrgKey => "FlexValues";

    protected override Expression<Func<TBasFlexvalues, bool>> BuildSearchPredicate(string keyword)
    {
        return v => v.Fnumber.Contains(keyword) || v.Fname.Contains(keyword);
    }

    /// <summary>子表关联键：优先用仓位集 FInterId（app 数据 FInterId==Uid）</summary>
    private static string ResolveLinkKey(TBasFlexvalues fv)
        => string.IsNullOrEmpty(fv.FInterId) ? fv.Uid : fv.FInterId;

    public override async Task<FlexValuesDetailDto?> GetByIdAsync(string uid)
    {
        var detail = await base.GetByIdAsync(uid);
        if (detail == null) return null;

        if (!string.IsNullOrEmpty(detail.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(detail.FCompanyId);
            if (org != null) detail.FCompanyName = org.Fname;
        }

        var entity = await Repository.GetByIdAsync(uid);
        if (entity != null)
        {
            var linkKey = ResolveLinkKey(entity);
            var entries = await _entryRepo.GetListAsync(e => e.FInterId == linkKey && !e.FDeleted);
            detail.Entries = entries.OrderBy(e => e.Fentryid).Select(e => new FlexValuesEntryDto
            {
                Uid = e.Uid,
                FEntryId = e.Fentryid,
                FNumber = e.Fnumber,
                FName = e.Fname,
                FDescription = e.Fdescription,
                FForbid = e.Fforbid
            }).ToList();
        }
        return detail;
    }

    public override async Task<FlexValuesDetailDto> CreateAsync(CreateFlexValuesRequest request)
    {
        if (!string.IsNullOrEmpty(request.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(request.FCompanyId);
            if (org != null)
                request.FCompanyId = !string.IsNullOrEmpty(org.Fparaid) ? org.Fparaid : org.Uid;
        }
        return await base.CreateAsync(request);
    }

    protected override FlexValuesListDto MapToListDto(TBasFlexvalues entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.Fnumber,
        FName = entity.Fname,
        FFlexTypeId = entity.Fflextypeid,
        FDescription = entity.Fdescription,
        CYmd = entity.CYmd,
        FGroupId = entity.FGroupId
    };

    protected override FlexValuesDetailDto MapToDetailDto(TBasFlexvalues entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.Fnumber,
        FName = entity.Fname,
        FFlexTypeId = entity.Fflextypeid,
        FDescription = entity.Fdescription,
        CYmd = entity.CYmd,
        FGroupId = entity.FGroupId,
        FLength = entity.Flength,
        FWidth = entity.Fwidth,
        FHeight = entity.Fheight,
        FVolume = entity.Fvolume,
        FVolumeLimit = entity.Fvolumelimit,
        FWeightLimit = entity.Fweightlimit,
        FMixTypeLimit = entity.Fmixtypelimit,
        FCompanyId = entity.FCompanyId,
        CUser = entity.CUser,
        MUser = entity.MUser,
        MYmd = entity.MYmd,
        FCheckerId = entity.FCheckerId,
        FCheckDate = entity.FCheckDate,
        Fdisableid = entity.Fdisableid,
        Fdisabledate = entity.Fdisabledate
    };

    protected override TBasFlexvalues MapToEntity(CreateFlexValuesRequest dto) => new()
    {
        Fnumber = dto.FNumber,
        Fname = dto.FName,
        Fflexnumber = dto.FNumber,
        Fflextypeid = "仓位",
        FCompanyId = dto.FCompanyId,
        Fdescription = dto.FDescription,
        Flength = dto.FLength,
        Fwidth = dto.FWidth,
        Fheight = dto.FHeight,
        Fvolume = dto.FVolume,
        Fvolumelimit = dto.FVolumeLimit,
        Fweightlimit = dto.FWeightLimit,
        Fmixtypelimit = dto.FMixTypeLimit,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBasFlexvalues entity, UpdateFlexValuesRequest dto)
    {
        entity.Fname = dto.FName;
        entity.Fdescription = dto.FDescription;
        entity.Flength = dto.FLength;
        entity.Fwidth = dto.FWidth;
        entity.Fheight = dto.FHeight;
        entity.Fvolume = dto.FVolume;
        entity.Fvolumelimit = dto.FVolumeLimit;
        entity.Fweightlimit = dto.FWeightLimit;
        entity.Fmixtypelimit = dto.FMixTypeLimit;
        entity.FGroupId = dto.FGroupId;
    }

    // ---- 值列表（T_BAS_FLEXVALUESENTRY，按 FInterId 关联）----

    public async Task<List<FlexValuesEntryDto>> GetEntriesAsync(string flexUid)
    {
        var fv = await Repository.GetByIdAsync(flexUid);
        if (fv == null) return new List<FlexValuesEntryDto>();
        var linkKey = ResolveLinkKey(fv);
        var entries = await _entryRepo.GetListAsync(e => e.FInterId == linkKey && !e.FDeleted);
        return entries.OrderBy(e => e.Fentryid).Select(e => new FlexValuesEntryDto
        {
            Uid = e.Uid,
            FEntryId = e.Fentryid,
            FNumber = e.Fnumber,
            FName = e.Fname,
            FDescription = e.Fdescription,
            FForbid = e.Fforbid
        }).ToList();
    }

    /// <summary>
    /// 保存值列表：按 Uid 合并 —— 已存在条目原地更新（保留 FDETAILID，避免破坏仓库 STOCKFLEXDETAIL.FFLEXENTRYID 引用），
    /// 新增条目生成新 FDETAILID，删除条目软删除（仍可用于历史引用名称解析）。
    /// </summary>
    public async Task SaveEntriesAsync(string flexUid, List<SaveFlexValuesEntryItem> items)
    {
        var fv = await Repository.GetByIdAsync(flexUid);
        if (fv == null) throw new KeyNotFoundException("仓位不存在");
        var linkKey = ResolveLinkKey(fv);

        items ??= new List<SaveFlexValuesEntryItem>();
        // 过滤完全空白行
        var valid = items.Where(i => !string.IsNullOrWhiteSpace(i.FNumber) || !string.IsNullOrWhiteSpace(i.FName)).ToList();

        var existing = await _entryRepo.GetListAsync(e => e.FInterId == linkKey && !e.FDeleted);
        var existingByUid = existing.ToDictionary(e => e.Uid);
        var keptUids = new HashSet<string>(valid.Where(i => !string.IsNullOrEmpty(i.Uid)).Select(i => i.Uid));

        try
        {
            Db.Ado.BeginTran();

            // 软删除被移除的条目
            var removed = existing.Where(e => !keptUids.Contains(e.Uid)).Select(e => e.Uid).ToList();
            if (removed.Count > 0)
            {
                await Db.Updateable<TBasFlexvaluesentry>()
                    .SetColumns(e => e.FDeleted == true)
                    .Where(e => removed.Contains(e.Uid))
                    .ExecuteCommandAsync();
            }

            var index = 1;
            foreach (var it in valid)
            {
                if (!string.IsNullOrEmpty(it.Uid) && existingByUid.TryGetValue(it.Uid, out var entity))
                {
                    // 原地更新，保留 Fdetailid
                    entity.Fentryid = index;
                    entity.Fnumber = it.FNumber;
                    entity.Fname = it.FName;
                    entity.Fdescription = it.FDescription;
                    entity.Fforbid = it.FForbid;
                    await _entryRepo.UpdateAsync(entity);
                }
                else
                {
                    await _entryRepo.InsertAsync(new TBasFlexvaluesentry
                    {
                        FInterId = linkKey,
                        Fentryid = index,
                        Fdetailid = Guid.NewGuid().ToString("N"),
                        Fnumber = it.FNumber,
                        Fname = it.FName,
                        Fdescription = it.FDescription,
                        Fforbid = it.FForbid
                    });
                }
                index++;
            }
            Db.Ado.CommitTran();
        }
        catch
        {
            Db.Ado.RollbackTran();
            throw;
        }
    }
}
