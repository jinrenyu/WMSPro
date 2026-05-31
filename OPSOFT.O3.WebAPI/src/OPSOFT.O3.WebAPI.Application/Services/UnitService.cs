using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class UnitService : ApprovableDisableableCrudService<TBdUnit, UnitListDto, UnitDetailDto, CreateUnitRequest, UpdateUnitRequest>
{
    private readonly IRepository<SysOrgStructure> _orgRepo;
    private readonly IRepository<TBdUnitgroup> _unitGroupRepo;
    private readonly IRepository<TBdUnitconvertrate> _convertRateRepo;

    public UnitService(
        IRepository<TBdUnit> repository,
        IRepository<SysOrgStructure> orgRepo,
        IRepository<TBdUnitgroup> unitGroupRepo,
        IRepository<TBdUnitconvertrate> convertRateRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null)
        : base(repository, db, currentUser, operationLog)
    {
        _orgRepo = orgRepo;
        _unitGroupRepo = unitGroupRepo;
        _convertRateRepo = convertRateRepo;
    }

    protected override string PrgKey => "Unit";

    protected override Expression<Func<TBdUnit, bool>> BuildSearchPredicate(string keyword)
    {
        return u => u.FNumber.Contains(keyword) || u.FName.Contains(keyword);
    }

    private static string ResolveLinkKey(TBdUnit u)
        => string.IsNullOrEmpty(u.FInterId) ? u.Uid : u.FInterId;

    /// <summary>所属分组（单位组）下拉，含基准单位（值=单位组 FInterId）</summary>
    public async Task<List<UnitGroupOptionDto>> GetUnitGroupsAsync(string? keyword)
    {
        var list = await _unitGroupRepo.GetListAsync(g => !g.FDeleted && !g.FDisabled);
        var query = list.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var kw = keyword.Trim();
            query = query.Where(g => g.Fnumber.Contains(kw) || g.Fname.Contains(kw));
        }
        return query.OrderBy(g => g.Fnumber).Select(g => new UnitGroupOptionDto
        {
            Uid = g.FInterId,
            FNumber = g.Fnumber,
            FName = g.Fname,
            FBaseUnitNumber = g.Fbaseunitnumber,
            FBaseUnitName = g.Fbaseunitname
        }).ToList();
    }

    public override async Task<UnitDetailDto?> GetByIdAsync(string uid)
    {
        var detail = await base.GetByIdAsync(uid);
        if (detail == null) return null;

        // 所属分组（单位组）名称 + 基准单位
        if (!string.IsNullOrEmpty(detail.FUnitGroupId))
        {
            var group = (await _unitGroupRepo.GetListAsync(g => g.FInterId == detail.FUnitGroupId)).FirstOrDefault();
            if (group != null)
            {
                detail.FUnitGroupNumber = group.Fnumber;
                detail.FUnitGroupName = group.Fname;
                detail.FBaseUnitNumber = group.Fbaseunitnumber;
                detail.FBaseUnitName = group.Fbaseunitname;
            }
        }

        // 使用组织名称
        if (!string.IsNullOrEmpty(detail.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(detail.FCompanyId);
            if (org != null) detail.FCompanyName = org.Fname;
        }

        // 换算关系：取换算率表；无记录时回落 numerator=FCoefficient / denominator=1
        var entity = await Repository.GetByIdAsync(uid);
        var linkKey = entity != null ? ResolveLinkKey(entity) : uid;
        var rate = (await _convertRateRepo.GetListAsync(r => r.Funitid == linkKey && !r.FDeleted)).FirstOrDefault();
        if (rate != null)
        {
            detail.FConvertNumerator = rate.Fconvertnumerator;
            detail.FConvertDenominator = rate.Fconvertdenominator;
        }
        else
        {
            detail.FConvertNumerator = detail.FCoefficient;
            detail.FConvertDenominator = 1;
        }
        return detail;
    }

    public override async Task<UnitDetailDto> CreateAsync(CreateUnitRequest request)
    {
        if (!string.IsNullOrEmpty(request.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(request.FCompanyId);
            if (org != null)
                request.FCompanyId = !string.IsNullOrEmpty(org.Fparaid) ? org.Fparaid : org.Uid;
        }
        var detail = await base.CreateAsync(request);
        var entity = await Repository.GetByIdAsync(detail.Uid);
        if (entity != null)
            await SaveConvertRateAsync(ResolveLinkKey(entity), request.FUnitGroupId, request.FConvertNumerator, request.FConvertDenominator);
        return detail;
    }

    public override async Task<bool> UpdateAsync(string uid, UpdateUnitRequest request)
    {
        var result = await base.UpdateAsync(uid, request);
        if (result)
        {
            var entity = await Repository.GetByIdAsync(uid);
            if (entity != null)
                await SaveConvertRateAsync(ResolveLinkKey(entity), request.FUnitGroupId, request.FConvertNumerator, request.FConvertDenominator);
        }
        return result;
    }

    /// <summary>换算率 1:1（按 FUnitId 关联）：整组替换该单位的换算率行</summary>
    private async Task SaveConvertRateAsync(string linkKey, string unitGroupId, decimal numerator, decimal denominator)
    {
        // 目标单位 = 所属单位组的基准单位代码
        var destUnitNumber = string.Empty;
        if (!string.IsNullOrEmpty(unitGroupId))
        {
            var group = (await _unitGroupRepo.GetListAsync(g => g.FInterId == unitGroupId)).FirstOrDefault();
            if (group != null) destUnitNumber = group.Fbaseunitnumber;
        }

        try
        {
            Db.Ado.BeginTran();
            await Db.Deleteable<TBdUnitconvertrate>().Where(r => r.Funitid == linkKey).ExecuteCommandAsync();
            await _convertRateRepo.InsertAsync(new TBdUnitconvertrate
            {
                Funitid = linkKey,
                Fcurrentunitid = linkKey,
                Fdestunitid = destUnitNumber,
                Fconvertnumerator = numerator,
                Fconvertdenominator = denominator,
                // NOT NULL 日期列需赋 DateTime.MinValue（K3 遗留子表约束）
                Fcheckdate = DateTime.MinValue,
                Fdisabledate = DateTime.MinValue
            });
            Db.Ado.CommitTran();
        }
        catch
        {
            Db.Ado.RollbackTran();
            throw;
        }
    }

    private static decimal CalcCoefficient(decimal numerator, decimal denominator)
        => denominator > 0 ? numerator / denominator : 0;

    protected override UnitListDto MapToListDto(TBdUnit entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FUnitGroupId = entity.FUnitGroupId,
        FIsBaseUnit = entity.FIsBaseUnit,
        FPrecision = entity.FPrecision,
        FCoefficient = entity.FCoefficient,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FRoundType = entity.FRoundType,
        FConvertType = entity.FConvertType,
        FGroupId = entity.FGroupId
    };

    protected override UnitDetailDto MapToDetailDto(TBdUnit entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FUnitGroupId = entity.FUnitGroupId,
        FIsBaseUnit = entity.FIsBaseUnit,
        FPrecision = entity.FPrecision,
        FCoefficient = entity.FCoefficient,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FRoundType = entity.FRoundType,
        FConvertType = entity.FConvertType,
        FGroupId = entity.FGroupId,
        FCompanyId = entity.FCompanyId,
        CUser = entity.CUser,
        MUser = entity.MUser,
        MYmd = entity.MYmd,
        FCheckerId = entity.FCheckerId,
        FCheckDate = entity.FCheckDate,
        Fdisableid = entity.Fdisableid,
        Fdisabledate = entity.Fdisabledate
    };

    protected override TBdUnit MapToEntity(CreateUnitRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FCompanyId = dto.FCompanyId,
        FDescription = dto.FDescription,
        FUnitGroupId = dto.FUnitGroupId,
        FIsBaseUnit = dto.FIsBaseUnit,
        FPrecision = dto.FPrecision,
        FRoundType = dto.FRoundType,
        FConvertType = dto.FConvertType,
        FCoefficient = CalcCoefficient(dto.FConvertNumerator, dto.FConvertDenominator),
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdUnit entity, UpdateUnitRequest dto)
    {
        entity.FName = dto.FName;
        entity.FDescription = dto.FDescription;
        entity.FUnitGroupId = dto.FUnitGroupId;
        entity.FIsBaseUnit = dto.FIsBaseUnit;
        entity.FPrecision = dto.FPrecision;
        entity.FRoundType = dto.FRoundType;
        entity.FConvertType = dto.FConvertType;
        entity.FCoefficient = CalcCoefficient(dto.FConvertNumerator, dto.FConvertDenominator);
        entity.FGroupId = dto.FGroupId;
    }
}
