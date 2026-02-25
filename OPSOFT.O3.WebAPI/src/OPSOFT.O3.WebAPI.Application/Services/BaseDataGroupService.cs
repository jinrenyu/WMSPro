using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 基础资料分组服务
/// </summary>
public class BaseDataGroupService : IBaseDataGroupService
{
    private readonly IRepository<SysBaseDataGroup> _repository;
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// prgKey → 业务表名映射
    /// </summary>
    private static readonly Dictionary<string, string> PrgKeyTableMap = new()
    {
        ["BD_Material"] = "T_BD_MATERIAL",
        ["BD_Customer"] = "T_BD_CUSTOMER",
        ["BD_Supplier"] = "T_BD_SUPPLIER",
        ["BD_Stock"] = "T_BD_STOCK",
        ["BD_StockPlace"] = "T_BD_STOCKPLACE",
        ["BD_Unit"] = "T_BD_UNIT",
        ["BD_Employee"] = "T_BD_EMPINFO",
        ["BD_Currency"] = "T_BD_CURRENCY",
        ["BD_AssistantData"] = "T_BAS_ASSISTANTDATA",
        ["BD_AssistantDataEntry"] = "T_BAS_ASSISTANTDATAENTRY"
    };

    public BaseDataGroupService(IRepository<SysBaseDataGroup> repository, ISqlSugarClient db)
    {
        _repository = repository;
        _db = db;
    }

    public async Task<PagedResult<BaseDataGroupListDto>> GetPagedListAsync(string prgKey, PagedRequest request)
    {
        Expression<Func<SysBaseDataGroup, bool>> predicate = g => g.Fprgkey == prgKey && !g.FDeleted;

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var keyword = request.Keyword;
            predicate = g => g.Fprgkey == prgKey && !g.FDeleted
                && (g.Fnumber.Contains(keyword) || g.Fname.Contains(keyword));
        }

        var (items, totalCount) = await _repository.GetPagedListAsync(
            request.PageIndex,
            request.PageSize,
            predicate,
            request.SortField,
            request.IsAsc);

        return new PagedResult<BaseDataGroupListDto>
        {
            Items = items.Select(MapToListDto).ToList(),
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    public async Task<BaseDataGroupDetailDto?> GetByIdAsync(string prgKey, string uid)
    {
        var entity = await _repository.GetByIdAsync(uid);
        if (entity == null || entity.FDeleted || entity.Fprgkey != prgKey) return null;
        return MapToDetailDto(entity);
    }

    public async Task<BaseDataGroupDetailDto> CreateAsync(string prgKey, CreateBaseDataGroupRequest request)
    {
        var entity = new SysBaseDataGroup
        {
            Fprgkey = prgKey,
            Fnumber = request.FNumber,
            Fname = request.FName,
            Fcname = request.FCName,
            Ftname = request.FTName,
            Fename = request.FEName,
            Fparentid = request.FParentId,
            Fnote = request.FNote
        };

        // 拼接长编码
        if (!string.IsNullOrEmpty(request.FParentId))
        {
            var parent = await _repository.GetByIdAsync(request.FParentId);
            if (parent != null && !string.IsNullOrEmpty(parent.Ffullnumber))
                entity.Ffullnumber = parent.Ffullnumber + "." + request.FNumber;
            else
                entity.Ffullnumber = request.FNumber;
        }
        else
        {
            entity.Ffullnumber = request.FNumber;
        }

        var created = await _repository.InsertAsync(entity);
        return MapToDetailDto(created);
    }

    public async Task<bool> UpdateAsync(string prgKey, string uid, UpdateBaseDataGroupRequest request)
    {
        var entity = await _repository.GetByIdAsync(uid);
        if (entity == null || entity.FDeleted || entity.Fprgkey != prgKey)
            throw new KeyNotFoundException("记录不存在");

        entity.Fname = request.FName;
        entity.Fcname = request.FCName;
        entity.Ftname = request.FTName;
        entity.Fename = request.FEName;
        entity.Fparentid = request.FParentId;
        entity.Fnote = request.FNote;

        // 重新拼接长编码
        if (!string.IsNullOrEmpty(request.FParentId))
        {
            var parent = await _repository.GetByIdAsync(request.FParentId);
            if (parent != null && !string.IsNullOrEmpty(parent.Ffullnumber))
                entity.Ffullnumber = parent.Ffullnumber + "." + entity.Fnumber;
            else
                entity.Ffullnumber = entity.Fnumber;
        }
        else
        {
            entity.Ffullnumber = entity.Fnumber;
        }

        return await _repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(string prgKey, string uid)
    {
        var entity = await _repository.GetByIdAsync(uid);
        if (entity == null || entity.Fprgkey != prgKey)
            return false;

        // 检查是否存在子分组
        var hasChildren = await _repository.AsQueryable()
            .AnyAsync(g => g.Fparentid == uid && !g.FDeleted);
        if (hasChildren)
            throw new InvalidOperationException("该分组下存在子分组，请先删除子分组");

        // 检查是否有业务数据引用该分组
        if (PrgKeyTableMap.TryGetValue(prgKey, out var tableName))
        {
            var refCount = await _db.Ado.GetIntAsync(
                $"SELECT COUNT(1) FROM {tableName} WHERE FGROUPID = @uid AND FDELETED = 0",
                new { uid });
            if (refCount > 0)
                throw new InvalidOperationException("该分组下存在关联数据，无法删除");
        }

        return await _repository.SoftDeleteAsync(uid);
    }

    public async Task<List<LookupDto>> GetLookupAsync(string prgKey, LookupRequest request)
    {
        var query = _repository.AsQueryable()
            .Where(g => g.Fprgkey == prgKey && !g.FDeleted);

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var keyword = request.Keyword;
            query = query.Where(g => g.Fnumber.Contains(keyword) || g.Fname.Contains(keyword));
        }

        var entities = await query.Take(request.MaxCount).ToListAsync();
        return entities.Select(e => new LookupDto
        {
            Uid = e.Uid,
            FNumber = e.Fnumber,
            FName = e.Fname
        }).ToList();
    }

    public async Task<List<BaseDataGroupListDto>> GetTreeAsync(string prgKey)
    {
        var entities = await _repository.GetListAsync(g => g.Fprgkey == prgKey && !g.FDeleted);
        return entities.Select(MapToListDto).ToList();
    }

    private static BaseDataGroupListDto MapToListDto(SysBaseDataGroup entity) => new()
    {
        Uid = entity.Uid,
        FPrgKey = entity.Fprgkey,
        FNumber = entity.Fnumber,
        FName = entity.Fname,
        FCName = entity.Fcname,
        FTName = entity.Ftname,
        FEName = entity.Fename,
        FParentId = entity.Fparentid,
        FFullNumber = entity.Ffullnumber,
        FNote = entity.Fnote,
        CYmd = entity.CYmd
    };

    private static BaseDataGroupDetailDto MapToDetailDto(SysBaseDataGroup entity) => new()
    {
        Uid = entity.Uid,
        FPrgKey = entity.Fprgkey,
        FNumber = entity.Fnumber,
        FName = entity.Fname,
        FCName = entity.Fcname,
        FTName = entity.Ftname,
        FEName = entity.Fename,
        FParentId = entity.Fparentid,
        FFullNumber = entity.Ffullnumber,
        FNote = entity.Fnote,
        CYmd = entity.CYmd
    };
}
