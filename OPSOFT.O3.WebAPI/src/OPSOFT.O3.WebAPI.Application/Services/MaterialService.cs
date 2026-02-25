using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class MaterialService : ApprovableDisableableCrudService<TBdMaterial, MaterialListDto, MaterialDetailDto, CreateMaterialRequest, UpdateMaterialRequest>, IMaterialService
{
    private readonly IRepository<TBdUnit> _unitRepo;
    private readonly IRepository<TBdStock> _stockRepo;
    private readonly IRepository<TBdStockPlace> _stockPlaceRepo;
    private readonly IRepository<SysBaseDataGroup> _groupRepo;

    public MaterialService(
        IRepository<TBdMaterial> repository,
        IRepository<TBdUnit> unitRepo,
        IRepository<TBdStock> stockRepo,
        IRepository<TBdStockPlace> stockPlaceRepo,
        IRepository<SysBaseDataGroup> groupRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null) : base(repository, db, currentUser, operationLog)
    {
        _unitRepo = unitRepo;
        _stockRepo = stockRepo;
        _stockPlaceRepo = stockPlaceRepo;
        _groupRepo = groupRepo;
    }

    protected override string PrgKey => "Material";

    protected override Expression<Func<TBdMaterial, bool>> BuildSearchPredicate(string keyword)
    {
        return m => m.FNumber.Contains(keyword) || m.FName.Contains(keyword) || m.FSpecification.Contains(keyword);
    }

    public override async Task<PagedResult<MaterialListDto>> GetPagedListAsync(PagedRequest request)
    {
        // Build combined predicate for keyword + groupId filtering
        Expression<Func<TBdMaterial, bool>>? predicate = null;

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            predicate = BuildSearchPredicate(request.Keyword);
        }

        if (!string.IsNullOrEmpty(request.GroupId))
        {
            Expression<Func<TBdMaterial, bool>> groupFilter = m => m.FGroupId == request.GroupId;
            if (predicate == null)
            {
                predicate = groupFilter;
            }
            else
            {
                // Combine with AndAlso
                var param = Expression.Parameter(typeof(TBdMaterial), "m");
                var body = Expression.AndAlso(
                    Expression.Invoke(predicate, param),
                    Expression.Invoke(groupFilter, param));
                predicate = Expression.Lambda<Func<TBdMaterial, bool>>(body, param);
            }
        }

        var (items, totalCount) = await Repository.GetPagedListAsync(
            request.PageIndex,
            request.PageSize,
            predicate,
            request.SortField,
            request.IsAsc);

        var result = new PagedResult<MaterialListDto>
        {
            Items = items.Select(MapToListDto).ToList(),
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };

        // 批量查询所有单位名称
        var unitIds = result.Items
            .SelectMany(i => new[] { i.FBaseUnitId, i.FStoreUnitId, i.FSaleUnitId, i.FPurchaseUnitId })
            .Where(id => !string.IsNullOrEmpty(id))
            .Distinct()
            .ToList();

        if (unitIds.Count > 0)
        {
            var units = await _unitRepo.GetListAsync(u => unitIds.Contains(u.Uid));
            var unitDict = units.ToDictionary(u => u.Uid, u => u.FName);

            foreach (var item in result.Items)
            {
                if (unitDict.TryGetValue(item.FBaseUnitId, out var baseUnitName))
                    item.FBaseUnitName = baseUnitName;
                if (unitDict.TryGetValue(item.FStoreUnitId, out var storeUnitName))
                    item.FStoreUnitName = storeUnitName;
                if (unitDict.TryGetValue(item.FSaleUnitId, out var saleUnitName))
                    item.FSaleUnitName = saleUnitName;
                if (unitDict.TryGetValue(item.FPurchaseUnitId, out var purchaseUnitName))
                    item.FPurchaseUnitName = purchaseUnitName;
            }
        }

        // 批量查询默认仓库名称
        var stockIds = result.Items
            .Where(i => !string.IsNullOrEmpty(i.FDeStockId))
            .Select(i => i.FDeStockId)
            .Distinct()
            .ToList();

        if (stockIds.Count > 0)
        {
            var stocks = await _stockRepo.GetListAsync(s => stockIds.Contains(s.Uid));
            var stockDict = stocks.ToDictionary(s => s.Uid, s => s.FName);

            foreach (var item in result.Items)
            {
                if (stockDict.TryGetValue(item.FDeStockId, out var stockName))
                    item.FDeStockName = stockName;
            }
        }

        // 批量查询默认仓位名称
        var spIds = result.Items
            .Where(i => !string.IsNullOrEmpty(i.FDeSpId))
            .Select(i => i.FDeSpId)
            .Distinct()
            .ToList();

        if (spIds.Count > 0)
        {
            var sps = await _stockPlaceRepo.GetListAsync(sp => spIds.Contains(sp.Uid));
            var spDict = sps.ToDictionary(sp => sp.Uid, sp => sp.FName);

            foreach (var item in result.Items)
            {
                if (spDict.TryGetValue(item.FDeSpId, out var spName))
                    item.FDeSpName = spName;
            }
        }

        // 批量查询分组名称
        var groupIds = result.Items
            .Where(i => !string.IsNullOrEmpty(i.FGroupId))
            .Select(i => i.FGroupId)
            .Distinct()
            .ToList();

        if (groupIds.Count > 0)
        {
            var groups = await _groupRepo.GetListAsync(g => groupIds.Contains(g.Uid));
            var groupDict = groups.ToDictionary(g => g.Uid, g => g.Fname);

            foreach (var item in result.Items)
            {
                if (groupDict.TryGetValue(item.FGroupId, out var groupName))
                    item.FGroupName = groupName;
            }
        }

        return result;
    }

    public override async Task<MaterialDetailDto?> GetByIdAsync(string uid)
    {
        var detail = await base.GetByIdAsync(uid);
        if (detail == null) return null;

        // 收集所有需要查询的单位 ID
        var unitIds = new[] { detail.FBaseUnitId, detail.FStoreUnitId, detail.FSaleUnitId, detail.FPurchaseUnitId }
            .Where(id => !string.IsNullOrEmpty(id))
            .Distinct()
            .ToList();

        if (unitIds.Count > 0)
        {
            var units = await _unitRepo.GetListAsync(u => unitIds.Contains(u.Uid));
            var unitDict = units.ToDictionary(u => u.Uid, u => u.FName);

            if (unitDict.TryGetValue(detail.FBaseUnitId, out var baseUnitName))
                detail.FBaseUnitName = baseUnitName;
            if (unitDict.TryGetValue(detail.FStoreUnitId, out var storeUnitName))
                detail.FStoreUnitName = storeUnitName;
            if (unitDict.TryGetValue(detail.FSaleUnitId, out var saleUnitName))
                detail.FSaleUnitName = saleUnitName;
            if (unitDict.TryGetValue(detail.FPurchaseUnitId, out var purchaseUnitName))
                detail.FPurchaseUnitName = purchaseUnitName;
        }

        // 查询默认仓库名称
        if (!string.IsNullOrEmpty(detail.FDeStockId))
        {
            var stock = await _stockRepo.GetByIdAsync(detail.FDeStockId);
            if (stock != null)
                detail.FDeStockName = stock.FName;
        }

        // 查询默认仓位名称
        if (!string.IsNullOrEmpty(detail.FDeSpId))
        {
            var sp = await _stockPlaceRepo.GetByIdAsync(detail.FDeSpId);
            if (sp != null)
                detail.FDeSpName = sp.FName;
        }

        // 查询分组名称
        if (!string.IsNullOrEmpty(detail.FGroupId))
        {
            var group = await _groupRepo.GetByIdAsync(detail.FGroupId);
            if (group != null)
                detail.FGroupName = group.Fname;
        }

        return detail;
    }

    protected override MaterialListDto MapToListDto(TBdMaterial entity) => new()
    {
        Uid = entity.Uid,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FSpecification = entity.FSpecification,
        FErpClsId = entity.FErpClsId,
        FBaseUnitId = entity.FBaseUnitId,
        FBaseUnitName = string.Empty,
        FTypeId = entity.FTypeId,
        FIsBatchManage = entity.FIsBatchManage,
        CYmd = entity.CYmd,
        FMasterId = entity.FMasterId,
        FDescription = entity.FDescription,
        FBarcode = entity.FBarcode,
        FAbc = entity.FAbc,
        FMaxQty = entity.FMaxQty,
        FSafeQty = entity.FSafeQty,
        FNetWeight = entity.FNetWeight,
        FGrossWeight = entity.FGrossWeight,
        FStoreUnitId = entity.FStoreUnitId,
        FStoreUnitName = string.Empty,
        FSaleUnitId = entity.FSaleUnitId,
        FSaleUnitName = string.Empty,
        FPurchaseUnitId = entity.FPurchaseUnitId,
        FPurchaseUnitName = string.Empty,
        FIsKfPeriod = entity.FIsKfPeriod,
        FKfUnit = entity.FKfUnit,
        FKfPeriod = entity.FKfPeriod,
        FMinPoQty = entity.FMinPoQty,
        FIncreaseQty = entity.FIncreaseQty,
        FCheckIncoming = entity.FCheckIncoming,
        FOldNumber = entity.FOldNumber,
        FDeStockId = entity.FDeStockId,
        FDeStockName = string.Empty,
        FDeSpId = entity.FDeSpId,
        FDeSpName = string.Empty,
        FVPart = entity.FVPart,
        FGroupId = entity.FGroupId,
        FGroupName = string.Empty,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FCheckerId = entity.FCheckerId,
        FCheckDate = entity.FCheckDate
    };

    protected override MaterialDetailDto MapToDetailDto(TBdMaterial entity) => new()
    {
        Uid = entity.Uid,
        FNumber = entity.FNumber,
        FName = entity.FName,
        FSpecification = entity.FSpecification,
        FErpClsId = entity.FErpClsId,
        FBaseUnitId = entity.FBaseUnitId,
        FBaseUnitName = string.Empty,
        FTypeId = entity.FTypeId,
        FIsBatchManage = entity.FIsBatchManage,
        CYmd = entity.CYmd,
        FMasterId = entity.FMasterId,
        FDescription = entity.FDescription,
        FBarcode = entity.FBarcode,
        FAbc = entity.FAbc,
        FMaxQty = entity.FMaxQty,
        FSafeQty = entity.FSafeQty,
        FNetWeight = entity.FNetWeight,
        FGrossWeight = entity.FGrossWeight,
        FStoreUnitId = entity.FStoreUnitId,
        FStoreUnitName = string.Empty,
        FSaleUnitId = entity.FSaleUnitId,
        FSaleUnitName = string.Empty,
        FPurchaseUnitId = entity.FPurchaseUnitId,
        FPurchaseUnitName = string.Empty,
        FIsKfPeriod = entity.FIsKfPeriod,
        FKfUnit = entity.FKfUnit,
        FKfPeriod = entity.FKfPeriod,
        FMinPoQty = entity.FMinPoQty,
        FIncreaseQty = entity.FIncreaseQty,
        FCheckIncoming = entity.FCheckIncoming,
        FOldNumber = entity.FOldNumber,
        FDeStockId = entity.FDeStockId,
        FDeStockName = string.Empty,
        FDeSpId = entity.FDeSpId,
        FDeSpName = string.Empty,
        FVPart = entity.FVPart,
        FGroupId = entity.FGroupId,
        FGroupName = string.Empty,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        FCheckerId = entity.FCheckerId,
        FCheckDate = entity.FCheckDate
    };

    protected override TBdMaterial MapToEntity(CreateMaterialRequest dto) => new()
    {
        FNumber = dto.FNumber,
        FName = dto.FName,
        FSpecification = dto.FSpecification,
        FDescription = dto.FDescription,
        FBarcode = dto.FBarcode,
        FErpClsId = dto.FErpClsId,
        FAbc = dto.FAbc,
        FMaxQty = dto.FMaxQty,
        FSafeQty = dto.FSafeQty,
        FNetWeight = dto.FNetWeight,
        FGrossWeight = dto.FGrossWeight,
        FBaseUnitId = dto.FBaseUnitId,
        FStoreUnitId = dto.FStoreUnitId,
        FSaleUnitId = dto.FSaleUnitId,
        FPurchaseUnitId = dto.FPurchaseUnitId,
        FIsKfPeriod = dto.FIsKfPeriod,
        FKfUnit = dto.FKfUnit,
        FKfPeriod = dto.FKfPeriod,
        FIsBatchManage = dto.FIsBatchManage,
        FMinPoQty = dto.FMinPoQty,
        FIncreaseQty = dto.FIncreaseQty,
        FCheckIncoming = dto.FCheckIncoming,
        FOldNumber = dto.FOldNumber,
        FDeStockId = dto.FDeStockId,
        FDeSpId = dto.FDeSpId,
        FVPart = dto.FVPart,
        FTypeId = dto.FTypeId,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBdMaterial entity, UpdateMaterialRequest dto)
    {
        entity.FName = dto.FName;
        entity.FSpecification = dto.FSpecification;
        entity.FDescription = dto.FDescription;
        entity.FBarcode = dto.FBarcode;
        entity.FErpClsId = dto.FErpClsId;
        entity.FAbc = dto.FAbc;
        entity.FMaxQty = dto.FMaxQty;
        entity.FSafeQty = dto.FSafeQty;
        entity.FNetWeight = dto.FNetWeight;
        entity.FGrossWeight = dto.FGrossWeight;
        entity.FBaseUnitId = dto.FBaseUnitId;
        entity.FStoreUnitId = dto.FStoreUnitId;
        entity.FSaleUnitId = dto.FSaleUnitId;
        entity.FPurchaseUnitId = dto.FPurchaseUnitId;
        entity.FIsKfPeriod = dto.FIsKfPeriod;
        entity.FKfUnit = dto.FKfUnit;
        entity.FKfPeriod = dto.FKfPeriod;
        entity.FIsBatchManage = dto.FIsBatchManage;
        entity.FMinPoQty = dto.FMinPoQty;
        entity.FIncreaseQty = dto.FIncreaseQty;
        entity.FCheckIncoming = dto.FCheckIncoming;
        entity.FOldNumber = dto.FOldNumber;
        entity.FDeStockId = dto.FDeStockId;
        entity.FDeSpId = dto.FDeSpId;
        entity.FVPart = dto.FVPart;
        entity.FTypeId = dto.FTypeId;
        entity.FGroupId = dto.FGroupId;
    }

}
