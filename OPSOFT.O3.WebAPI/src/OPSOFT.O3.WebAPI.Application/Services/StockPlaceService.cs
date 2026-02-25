using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class StockPlaceService : ApprovableDisableableCrudService<TBdStockPlace, StockPlaceListDto, StockPlaceDetailDto, CreateStockPlaceRequest, UpdateStockPlaceRequest>
{
    private readonly IRepository<TBdStock> _stockRepo;

    public StockPlaceService(
        IRepository<TBdStockPlace> repository,
        IRepository<TBdStock> stockRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null) : base(repository, db, currentUser, operationLog)
    {
        _stockRepo = stockRepo;
    }

    protected override string PrgKey => "StockPlace";

    protected override Expression<Func<TBdStockPlace, bool>> BuildSearchPredicate(string keyword)
    {
        return sp => sp.FNumber.Contains(keyword) || sp.FName.Contains(keyword);
    }

    protected override Expression<Func<TBdStockPlace, bool>>? BuildLookupFilter(LookupRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.ParentId))
        {
            return sp => sp.FStockId == request.ParentId;
        }
        return null;
    }

    public override async Task<PagedResult<StockPlaceListDto>> GetPagedListAsync(PagedRequest request)
    {
        var result = await base.GetPagedListAsync(request);

        var stockIds = result.Items
            .Where(i => !string.IsNullOrEmpty(i.FStockId))
            .Select(i => i.FStockId)
            .Distinct()
            .ToList();

        if (stockIds.Count > 0)
        {
            var stocks = await _stockRepo.GetListAsync(s => stockIds.Contains(s.Uid));
            var stockDict = stocks.ToDictionary(s => s.Uid, s => s.FName);

            foreach (var item in result.Items)
            {
                if (stockDict.TryGetValue(item.FStockId, out var stockName))
                    item.FStockName = stockName;
            }
        }

        return result;
    }

    protected override StockPlaceListDto MapToListDto(TBdStockPlace entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FStockId = entity.FStockId,
        FStockName = string.Empty,
        FSpProperty = entity.FSpProperty,
        FAllowMix = entity.FAllowMix,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FMaxCapacity = entity.FMaxCapacity,
        FGroupId = entity.FGroupId
    };

    protected override StockPlaceDetailDto MapToDetailDto(TBdStockPlace entity) => new()
    {
        Uid = entity.Uid,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FStockId = entity.FStockId,
        FStockName = string.Empty,
        FSpProperty = entity.FSpProperty,
        FAllowMix = entity.FAllowMix,
        CYmd = entity.CYmd,
        FDescription = entity.FDescription,
        FMaxCapacity = entity.FMaxCapacity,
        FGroupId = entity.FGroupId
    };

    protected override TBdStockPlace MapToEntity(CreateStockPlaceRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FDescription = dto.FDescription,
        FStockId = dto.FStockId,
        FSpProperty = dto.FSpProperty,
        FAllowMix = dto.FAllowMix,
        FMaxCapacity = dto.FMaxCapacity,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdStockPlace entity, UpdateStockPlaceRequest dto)
    {
        entity.FName = dto.FName;
        entity.FDescription = dto.FDescription;
        entity.FStockId = dto.FStockId;
        entity.FSpProperty = dto.FSpProperty;
        entity.FAllowMix = dto.FAllowMix;
        entity.FMaxCapacity = dto.FMaxCapacity;
        entity.FGroupId = dto.FGroupId;
    }
}
