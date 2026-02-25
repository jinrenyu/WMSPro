using System.Linq.Expressions;
using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 辅助资料类别服务
/// </summary>
public class AssistantDataService : ApprovableDisableableCrudService<TBasAssistantdata, AssistantDataListDto, AssistantDataDetailDto, CreateAssistantDataRequest, UpdateAssistantDataRequest>
{
    public AssistantDataService(IRepository<TBasAssistantdata> repository, ISqlSugarClient db, ICurrentUserService currentUser, IOperationLogService? operationLog = null) : base(repository, db, currentUser, operationLog) { }

    protected override string PrgKey => "AssistantData";

    protected override Expression<Func<TBasAssistantdata, bool>> BuildSearchPredicate(string keyword)
    {
        return a => a.Fnumber.Contains(keyword) || a.Fname.Contains(keyword);
    }

    protected override AssistantDataListDto MapToListDto(TBasAssistantdata entity) => new()
    {
        Uid = entity.Uid,
        Fnumber = entity.Fnumber,
        Fname = entity.Fname,
        Fdescription = entity.Fdescription,
        Fparentid = entity.Fparentid,
        Ftopclassid = entity.Ftopclassid,
        Isdefault = entity.Isdefault,
        Fiso3use = entity.Fiso3use,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        CYmd = entity.CYmd
    };

    protected override AssistantDataDetailDto MapToDetailDto(TBasAssistantdata entity) => new()
    {
        Uid = entity.Uid,
        Fnumber = entity.Fnumber,
        Fname = entity.Fname,
        Fdescription = entity.Fdescription,
        Fparentid = entity.Fparentid,
        Ftopclassid = entity.Ftopclassid,
        Isdefault = entity.Isdefault,
        Fiso3use = entity.Fiso3use,
        Fcheckerid = entity.FCheckerId,
        Fcheckdate = entity.FCheckDate,
        Fdisableid = entity.Fdisableid,
        Fdisabledate = entity.Fdisabledate,
        FGroupId = entity.FGroupId,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        CYmd = entity.CYmd
    };

    protected override TBasAssistantdata MapToEntity(CreateAssistantDataRequest dto) => new()
    {
        Fnumber = dto.Fnumber,
        Fname = dto.Fname,
        Fdescription = dto.Fdescription,
        Fparentid = dto.Fparentid,
        Ftopclassid = dto.Ftopclassid,
        Isdefault = dto.Isdefault,
        Fiso3use = dto.Fiso3use,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBasAssistantdata entity, UpdateAssistantDataRequest dto)
    {
        entity.Fname = dto.Fname;
        entity.Fdescription = dto.Fdescription;
        entity.Fparentid = dto.Fparentid;
        entity.Ftopclassid = dto.Ftopclassid;
        entity.Isdefault = dto.Isdefault;
        entity.Fiso3use = dto.Fiso3use;
        entity.FGroupId = dto.FGroupId;
    }
}

/// <summary>
/// 辅助资料明细服务
/// </summary>
public class AssistantDataEntryService : ApprovableDisableableCrudService<TBasAssistantdataentry, AssistantDataEntryListDto, AssistantDataEntryDetailDto, CreateAssistantDataEntryRequest, UpdateAssistantDataEntryRequest>
{
    public AssistantDataEntryService(IRepository<TBasAssistantdataentry> repository, ISqlSugarClient db, ICurrentUserService currentUser, IOperationLogService? operationLog = null) : base(repository, db, currentUser, operationLog) { }

    protected override string PrgKey => "AssistantDataEntry";

    public override async Task<PagedResult<AssistantDataEntryListDto>> GetPagedListAsync(PagedRequest request)
    {
        Expression<Func<TBasAssistantdataentry, bool>>? predicate = null;

        // 按类别过滤
        if (!string.IsNullOrEmpty(request.Fid))
        {
            predicate = e => e.Fid == request.Fid;
        }

        // 关键字搜索
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            var searchPredicate = BuildSearchPredicate(request.Keyword);
            if (predicate == null)
            {
                predicate = searchPredicate;
            }
            else
            {
                var param = Expression.Parameter(typeof(TBasAssistantdataentry), "e");
                var body = Expression.AndAlso(
                    Expression.Invoke(predicate, param),
                    Expression.Invoke(searchPredicate, param));
                predicate = Expression.Lambda<Func<TBasAssistantdataentry, bool>>(body, param);
            }
        }

        // 分组过滤
        if (!string.IsNullOrEmpty(request.GroupId))
        {
            Expression<Func<TBasAssistantdataentry, bool>> groupFilter = e => e.FGroupId == request.GroupId;
            if (predicate == null)
            {
                predicate = groupFilter;
            }
            else
            {
                var param = Expression.Parameter(typeof(TBasAssistantdataentry), "e");
                var body = Expression.AndAlso(
                    Expression.Invoke(predicate, param),
                    Expression.Invoke(groupFilter, param));
                predicate = Expression.Lambda<Func<TBasAssistantdataentry, bool>>(body, param);
            }
        }

        var (items, totalCount) = await Repository.GetPagedListAsync(
            request.PageIndex,
            request.PageSize,
            predicate,
            request.SortField,
            request.IsAsc);

        return new PagedResult<AssistantDataEntryListDto>
        {
            Items = items.Select(MapToListDto).ToList(),
            TotalCount = totalCount,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    protected override Expression<Func<TBasAssistantdataentry, bool>> BuildSearchPredicate(string keyword)
    {
        return e => e.Fnumber.Contains(keyword) || e.Fdatavalue.Contains(keyword);
    }

    protected override AssistantDataEntryListDto MapToListDto(TBasAssistantdataentry entity) => new()
    {
        Uid = entity.Uid,
        Fnumber = entity.Fnumber,
        Fdatavalue = entity.Fdatavalue,
        Fid = entity.Fid,
        Fparentid = entity.Fparentid,
        Fdescription = entity.Fdescription,
        Fseq = entity.Fseq,
        Isdefault = entity.Isdefault,
        Fuseorgid = entity.Fuseorgid,
        Fisbuildself = entity.Fisbuildself,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        CYmd = entity.CYmd
    };

    protected override AssistantDataEntryDetailDto MapToDetailDto(TBasAssistantdataentry entity) => new()
    {
        Uid = entity.Uid,
        Fnumber = entity.Fnumber,
        Fdatavalue = entity.Fdatavalue,
        Fid = entity.Fid,
        Fparentid = entity.Fparentid,
        Fdescription = entity.Fdescription,
        Fseq = entity.Fseq,
        Isdefault = entity.Isdefault,
        Fuseorgid = entity.Fuseorgid,
        Fisbuildself = entity.Fisbuildself,
        Fcheckerid = entity.FCheckerId,
        Fcheckdate = entity.FCheckDate,
        Fdisableid = entity.Fdisableid,
        Fdisabledate = entity.Fdisabledate,
        FGroupId = entity.FGroupId,
        FStatus = entity.FStatus,
        FDisabled = entity.FDisabled,
        CYmd = entity.CYmd
    };

    protected override TBasAssistantdataentry MapToEntity(CreateAssistantDataEntryRequest dto) => new()
    {
        Fnumber = dto.Fnumber,
        Fdatavalue = dto.Fdatavalue,
        Fid = dto.Fid,
        Fparentid = dto.Fparentid,
        Fdescription = dto.Fdescription,
        Fseq = dto.Fseq,
        Isdefault = dto.Isdefault,
        Fuseorgid = dto.Fuseorgid,
        Fisbuildself = dto.Fisbuildself,
        FGroupId = dto.FGroupId
    };

    protected override void UpdateEntity(TBasAssistantdataentry entity, UpdateAssistantDataEntryRequest dto)
    {
        entity.Fdatavalue = dto.Fdatavalue;
        entity.Fparentid = dto.Fparentid;
        entity.Fdescription = dto.Fdescription;
        entity.Fseq = dto.Fseq;
        entity.Isdefault = dto.Isdefault;
        entity.Fuseorgid = dto.Fuseorgid;
        entity.Fisbuildself = dto.Fisbuildself;
        entity.FGroupId = dto.FGroupId;
    }
}
