using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class LoginInfoService : ILoginInfoService
{
    private readonly ISqlSugarClient _db;

    public LoginInfoService(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task RecordLoginAsync(string userId, string? ipAddress, string? userAgent, bool success, string? message = null)
    {
        var loginInfo = new SysLoginInfo
        {
            Uid = Guid.NewGuid().ToString("N"),
            FInterId = Guid.NewGuid().ToString("N"),
            Fuserid = userId,
            Fcomputer = ipAddress ?? string.Empty,
            Flogintime = DateTime.Now,
            Flasttime = DateTime.Now,
            FCompanyId = string.Empty,
            CYmd = DateTime.Now,
            CUser = userId,
            MYmd = DateTime.Now,
            MUser = userId
        };

        await _db.Insertable(loginInfo).ExecuteCommandAsync();
    }

    public async Task<PagedResult<LoginInfoDto>> GetLoginHistoryAsync(PagedRequest request, string? userId = null)
    {
        var query = _db.Queryable<SysLoginInfo>();

        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(l => l.Fuserid == userId);
        }

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            query = query.Where(l => l.Fuserid.Contains(request.Keyword) || l.Fcomputer.Contains(request.Keyword));
        }

        var totalCount = new RefAsync<int>(0);
        var items = await query
            .OrderBy(l => l.Flogintime, OrderByType.Desc)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        return new PagedResult<LoginInfoDto>
        {
            Items = items.Select(l => new LoginInfoDto
            {
                Uid = l.Uid,
                UserId = l.Fuserid,
                Computer = l.Fcomputer,
                LoginTime = l.Flogintime,
                LastTime = l.Flasttime
            }).ToList(),
            TotalCount = totalCount.Value,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }
}
