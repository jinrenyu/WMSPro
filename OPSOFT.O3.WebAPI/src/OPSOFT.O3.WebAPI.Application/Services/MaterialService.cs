using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Extensions;
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
    private readonly IRepository<TBdMaterialAuxPty> _auxPtyRepo;
    private readonly IRepository<TBdSecunitconvertrate> _secUnitRepo;
    private readonly IRepository<TBdFlexauxproperty> _flexRepo;
    private readonly IRepository<TBdMaterialtype> _typeRepo;
    private readonly IRepository<SysOrgStructure> _orgRepo;
    private readonly ISqlSugarClient _db;

    public MaterialService(
        IRepository<TBdMaterial> repository,
        IRepository<TBdUnit> unitRepo,
        IRepository<TBdStock> stockRepo,
        IRepository<TBdStockPlace> stockPlaceRepo,
        IRepository<SysBaseDataGroup> groupRepo,
        IRepository<TBdMaterialAuxPty> auxPtyRepo,
        IRepository<TBdSecunitconvertrate> secUnitRepo,
        IRepository<TBdFlexauxproperty> flexRepo,
        IRepository<TBdMaterialtype> typeRepo,
        IRepository<SysOrgStructure> orgRepo,
        ISqlSugarClient db,
        ICurrentUserService currentUser,
        IOperationLogService? operationLog = null) : base(repository, db, currentUser, operationLog)
    {
        _unitRepo = unitRepo;
        _stockRepo = stockRepo;
        _stockPlaceRepo = stockPlaceRepo;
        _groupRepo = groupRepo;
        _auxPtyRepo = auxPtyRepo;
        _secUnitRepo = secUnitRepo;
        _flexRepo = flexRepo;
        _typeRepo = typeRepo;
        _orgRepo = orgRepo;
        _db = db;
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

        // 动态高级筛选（与基类 CrudService 保持一致，修复物料列表高级筛选不生效的问题）
        var conditionalModels = request.DynamicFilters?.ToConditionalModels<TBdMaterial>() ?? new List<IConditionalModel>();

        var (items, totalCount) = await Repository.GetPagedListAsync(
            request.PageIndex,
            request.PageSize,
            predicate,
            request.SortField,
            request.IsAsc,
            conditionalModels);

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

        // 批量查询物料类别名称
        var typeIds = result.Items
            .Where(i => !string.IsNullOrEmpty(i.FTypeId))
            .Select(i => i.FTypeId)
            .Distinct()
            .ToList();

        if (typeIds.Count > 0)
        {
            var types = await _typeRepo.GetListAsync(t => typeIds.Contains(t.Uid));
            var typeDict = types.ToDictionary(t => t.Uid, t => t.Fname);

            foreach (var item in result.Items)
            {
                if (typeDict.TryGetValue(item.FTypeId, out var typeName))
                    item.FTypeName = typeName;
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

        // 查询物料类别名称
        if (!string.IsNullOrEmpty(detail.FTypeId))
        {
            var type = await _typeRepo.GetByIdAsync(detail.FTypeId);
            if (type != null)
                detail.FTypeName = type.Fname;
        }

        // 查询使用组织名称
        if (!string.IsNullOrEmpty(detail.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(detail.FCompanyId);
            if (org != null)
                detail.FCompanyName = org.Fname;
        }

        return detail;
    }

    public override async Task<MaterialDetailDto> CreateAsync(CreateMaterialRequest request)
    {
        // 使用组织(FCompanyId)记录所选组织的上级(SysOrgStructure.FPARAID)；无上级则记录其自身
        if (!string.IsNullOrEmpty(request.FCompanyId))
        {
            var org = await _orgRepo.GetByIdAsync(request.FCompanyId);
            if (org != null)
                request.FCompanyId = !string.IsNullOrEmpty(org.Fparaid) ? org.Fparaid : org.Uid;
        }
        return await base.CreateAsync(request);
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
        FCheckDate = entity.FCheckDate,
        // ---- 扩展字段 ----
        FChartNumber = entity.FCHARTNUMBER,
        FTaxRate = entity.FTAXRATE,
        FCompanyId = entity.FCompanyId,
        FBarType = entity.FBARTYPE,
        FSubConUnitId = entity.FSUBCONUNITID,
        FProduceUnitId = entity.FPRODUCEUNITID,
        FAuxUnitId = entity.FAUXUNITID,
        FLength = entity.FLENGTH,
        FWidth = entity.FWIDTH,
        FHeight = entity.FHEIGHT,
        FVolume = entity.FVOLUME,
        FLowLimit = entity.FLOWLIMIT,
        FFeedSn = entity.FFEEDSN,
        FOtherSn = entity.FOTHERSN,
        FIsSecUnit = entity.FISSECUNIT,
        FIsFeed = entity.FISFEED,
        FIsPinal = entity.FISPINAL,
        FSuite = entity.FSUITE,
        FPrice = entity.FPRICE,
        CUser = entity.CUser,
        MUser = entity.MUser,
        MYmd = entity.MYmd,
        Fdisableid = entity.Fdisableid,
        Fdisabledate = entity.Fdisabledate,
        FPictureBase64 = (entity.FPICTURE != null && entity.FPICTURE.Length > 0)
            ? Convert.ToBase64String(entity.FPICTURE)
            : null
    };

    /// <summary>Base64（可含 dataURL 前缀）→ byte[]；空或非法返回 null</summary>
    private static byte[]? DecodePicture(string? base64)
    {
        if (string.IsNullOrEmpty(base64)) return null;
        var idx = base64.IndexOf("base64,", StringComparison.Ordinal);
        if (idx >= 0) base64 = base64[(idx + 7)..];
        try { return Convert.FromBase64String(base64); }
        catch (FormatException) { return null; }
    }

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
        FGroupId = dto.FGroupId,
        FCompanyId = dto.FCompanyId,
        // ---- 扩展字段 ----
        FCHARTNUMBER = dto.FChartNumber,
        FTAXRATE = dto.FTaxRate,
        FBARTYPE = dto.FBarType,
        FSUBCONUNITID = dto.FSubConUnitId,
        FPRODUCEUNITID = dto.FProduceUnitId,
        FAUXUNITID = dto.FAuxUnitId,
        FLENGTH = dto.FLength,
        FWIDTH = dto.FWidth,
        FHEIGHT = dto.FHeight,
        FVOLUME = dto.FVolume,
        FLOWLIMIT = dto.FLowLimit,
        FFEEDSN = dto.FFeedSn,
        FOTHERSN = dto.FOtherSn,
        FISSECUNIT = dto.FIsSecUnit,
        FISFEED = dto.FIsFeed,
        FISPINAL = dto.FIsPinal,
        FSUITE = dto.FSuite,
        FPRICE = dto.FPrice,
        FPICTURE = DecodePicture(dto.FPictureBase64)
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
        // ---- 扩展字段 ----
        entity.FCHARTNUMBER = dto.FChartNumber;
        entity.FTAXRATE = dto.FTaxRate;
        entity.FBARTYPE = dto.FBarType;
        entity.FSUBCONUNITID = dto.FSubConUnitId;
        entity.FPRODUCEUNITID = dto.FProduceUnitId;
        entity.FAUXUNITID = dto.FAuxUnitId;
        entity.FLENGTH = dto.FLength;
        entity.FWIDTH = dto.FWidth;
        entity.FHEIGHT = dto.FHeight;
        entity.FVOLUME = dto.FVolume;
        entity.FLOWLIMIT = dto.FLowLimit;
        entity.FFEEDSN = dto.FFeedSn;
        entity.FOTHERSN = dto.FOtherSn;
        entity.FISSECUNIT = dto.FIsSecUnit;
        entity.FISFEED = dto.FIsFeed;
        entity.FISPINAL = dto.FIsPinal;
        entity.FSUITE = dto.FSuite;
        entity.FPRICE = dto.FPrice;
        entity.FPICTURE = DecodePicture(dto.FPictureBase64);
    }

    /// <summary>计算子表关联键：优先用物料 FInterId（app 数据 FInterId==Uid），为空回退 Uid</summary>
    private static string ResolveLinkKey(TBdMaterial material)
        => string.IsNullOrEmpty(material.FInterId) ? material.Uid : material.FInterId;

    // ============ 物料维度（辅助属性配置 TBdMaterialAuxPty，按 FInterId 关联）============

    public async Task<List<MaterialDimensionDto>> GetDimensionsAsync(string materialUid)
    {
        var material = await Repository.GetByIdAsync(materialUid);
        if (material == null) return new List<MaterialDimensionDto>();
        var linkKey = ResolveLinkKey(material);

        var rows = await _auxPtyRepo.GetListAsync(x => x.FInterId == linkKey);
        var dtos = rows.OrderBy(r => r.FEntryId).Select(r => new MaterialDimensionDto
        {
            Uid = r.Uid,
            FAuxPropertyId = r.FAuxPropertyId,
            FIsEnable = r.FIsEnable,
            FIsComControl = r.FIsComControl,
            FIsMustInput = r.FIsMustInput,
            FIsAffectPrice = r.FIsAffectPrice,
            FIsAffectPlan = r.FIsAffectPlan,
            FIsAffectCost = r.FIsAffectCost,
            FValueSet = r.FValueSet,
            FEntryId = r.FEntryId
        }).ToList();

        var propIds = dtos.Where(d => !string.IsNullOrEmpty(d.FAuxPropertyId))
            .Select(d => d.FAuxPropertyId).Distinct().ToList();
        if (propIds.Count > 0)
        {
            var props = await _flexRepo.GetListAsync(p => propIds.Contains(p.Uid));
            var dict = props.ToDictionary(p => p.Uid);
            foreach (var d in dtos)
            {
                if (dict.TryGetValue(d.FAuxPropertyId, out var p))
                {
                    d.FAuxPropertyNumber = p.Fnumber;
                    d.FAuxPropertyName = p.Fname;
                }
            }
        }
        return dtos;
    }

    public async Task SaveDimensionsAsync(string materialUid, List<SaveMaterialDimensionItem> items)
    {
        var material = await Repository.GetByIdAsync(materialUid);
        if (material == null) throw new KeyNotFoundException("物料不存在");
        var linkKey = ResolveLinkKey(material);

        try
        {
            _db.Ado.BeginTran();
            await _db.Deleteable<TBdMaterialAuxPty>().Where(x => x.FInterId == linkKey).ExecuteCommandAsync();

            var index = 1;
            foreach (var it in items)
            {
                if (string.IsNullOrEmpty(it.FAuxPropertyId)) continue;
                await _auxPtyRepo.InsertAsync(new TBdMaterialAuxPty
                {
                    FInterId = linkKey,
                    FMasterId = linkKey,
                    FEntryId = index++,
                    FDetailId = Guid.NewGuid().ToString("N"),
                    FAuxPropertyId = it.FAuxPropertyId,
                    FIsEnable = it.FIsEnable,
                    FIsComControl = it.FIsComControl,
                    FIsMustInput = it.FIsMustInput,
                    FIsAffectPrice = it.FIsAffectPrice,
                    FIsAffectPlan = it.FIsAffectPlan,
                    FIsAffectCost = it.FIsAffectCost,
                    FValueSet = it.FValueSet
                });
            }
            _db.Ado.CommitTran();
        }
        catch
        {
            _db.Ado.RollbackTran();
            throw;
        }
    }

    // ============ 辅助单位换算（TBdSecunitconvertrate，按 FInterId 关联）============

    public async Task<List<MaterialSecUnitDto>> GetSecUnitsAsync(string materialUid)
    {
        var material = await Repository.GetByIdAsync(materialUid);
        if (material == null) return new List<MaterialSecUnitDto>();
        var linkKey = ResolveLinkKey(material);

        var rows = await _secUnitRepo.GetListAsync(x => x.FInterId == linkKey);
        var dtos = rows.OrderBy(r => r.Fentryid).Select(r => new MaterialSecUnitDto
        {
            Uid = r.Uid,
            FConvertType = r.Fconverttype,
            FBaseUnitId = r.Fbaseunitid,
            FSecUnitId = r.Fsecunitid,
            FConvertNumerator = r.Fconvertnumerator,
            FConvertDenominator = r.Fconvertdenominator,
            FEntryId = r.Fentryid
        }).ToList();

        var unitIds = dtos.SelectMany(d => new[] { d.FBaseUnitId, d.FSecUnitId })
            .Where(id => !string.IsNullOrEmpty(id)).Distinct().ToList();
        if (unitIds.Count > 0)
        {
            var units = await _unitRepo.GetListAsync(u => unitIds.Contains(u.Uid));
            var dict = units.ToDictionary(u => u.Uid, u => u.FName);
            foreach (var d in dtos)
            {
                if (dict.TryGetValue(d.FBaseUnitId, out var bn)) d.FBaseUnitName = bn;
                if (dict.TryGetValue(d.FSecUnitId, out var sn)) d.FSecUnitName = sn;
            }
        }
        return dtos;
    }

    public async Task SaveSecUnitsAsync(string materialUid, List<SaveMaterialSecUnitItem> items)
    {
        var material = await Repository.GetByIdAsync(materialUid);
        if (material == null) throw new KeyNotFoundException("物料不存在");
        var linkKey = ResolveLinkKey(material);

        try
        {
            _db.Ado.BeginTran();
            await _db.Deleteable<TBdSecunitconvertrate>().Where(x => x.FInterId == linkKey).ExecuteCommandAsync();

            var index = 1;
            foreach (var it in items)
            {
                if (string.IsNullOrEmpty(it.FSecUnitId)) continue;
                await _secUnitRepo.InsertAsync(new TBdSecunitconvertrate
                {
                    FInterId = linkKey,
                    Fentryid = index++,
                    Fdetailid = Guid.NewGuid().ToString("N"),
                    Fconverttype = it.FConvertType,
                    Fbaseunitid = it.FBaseUnitId,
                    Fsecunitid = it.FSecUnitId,
                    Fconvertnumerator = it.FConvertNumerator,
                    Fconvertdenominator = it.FConvertDenominator,
                    // 该表 FCHECKDATE/FDISABLEDATE 列为 NOT NULL，赋默认值避免约束失败
                    Fcheckdate = DateTime.MinValue,
                    Fdisabledate = DateTime.MinValue
                });
            }
            _db.Ado.CommitTran();
        }
        catch
        {
            _db.Ado.RollbackTran();
            throw;
        }
    }

    // ============ 辅助属性下拉（TBdFlexauxproperty）============

    public async Task<List<LookupDto>> GetAuxPropertyLookupAsync(string? keyword)
    {
        var query = _flexRepo.AsQueryable().Where(p => !p.FDeleted && !p.FDisabled);
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(p => p.Fnumber.Contains(keyword) || p.Fname.Contains(keyword));
        }
        var list = await query.Take(50).ToListAsync();
        return list.Select(p => new LookupDto { Uid = p.Uid, FNumber = p.Fnumber, FName = p.Fname }).ToList();
    }

    // ============ 物料类别下拉（TBdMaterialtype）============

    public async Task<List<LookupDto>> GetMaterialTypeLookupAsync(string? keyword)
    {
        var query = _typeRepo.AsQueryable().Where(t => !t.FDeleted && !t.FDisabled);
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(t => t.Fnumber.Contains(keyword) || t.Fname.Contains(keyword));
        }
        var list = await query.Take(50).ToListAsync();
        return list.Select(t => new LookupDto { Uid = t.Uid, FNumber = t.Fnumber, FName = t.Fname }).ToList();
    }

}
