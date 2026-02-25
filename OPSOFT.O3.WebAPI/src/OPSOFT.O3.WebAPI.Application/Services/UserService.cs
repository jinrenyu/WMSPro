using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<SysLoginUser> _userRepo;
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;

    public UserService(IRepository<SysLoginUser> userRepo, ISqlSugarClient db, ICurrentUserService currentUser)
    {
        _userRepo = userRepo;
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<PagedResult<UserListDto>> GetPagedListAsync(PagedRequest request)
    {
        var query = _db.Queryable<SysLoginUser>().Where(u => !u.FDeleted);

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            query = query.Where(u => u.UserId.Contains(request.Keyword) || u.UserName.Contains(request.Keyword));
        }

        var totalCount = new RefAsync<int>(0);
        var users = await query
            .OrderBy(u => u.CYmd, OrderByType.Desc)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        var userIds = users.Select(u => u.UserId).ToList();
        var roleRelations = await _db.Queryable<SysUserRoleRelation>()
            .Where(r => userIds.Contains(r.UserId) && !r.FDeleted)
            .ToListAsync();

        var roleIds = roleRelations.Select(r => r.Froleid).Distinct().ToList();
        var roles = roleIds.Any()
            ? await _db.Queryable<SysUserRole>().Where(r => roleIds.Contains(r.Uid) && !r.FDeleted).ToListAsync()
            : new List<SysUserRole>();

        var items = users.Select(u =>
        {
            var userRoleIds = roleRelations.Where(r => r.UserId == u.UserId).Select(r => r.Froleid).ToList();
            return new UserListDto
            {
                Uid = u.Uid,
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                FStatus = u.FStatus,
                LockStatus = u.LockStatus,
                LastLoginTime = u.LastLoginTime,
                CYmd = u.CYmd,
                PaType = u.PaType,
                Roles = roles.Where(r => userRoleIds.Contains(r.Uid)).Select(r => r.Frolename).ToList()
            };
        }).ToList();

        return new PagedResult<UserListDto>
        {
            Items = items,
            TotalCount = totalCount.Value,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    public async Task<UserDetailDto?> GetByIdAsync(string uid)
    {
        var user = await _userRepo.GetByIdAsync(uid);
        if (user == null || user.FDeleted) return null;
        return await MapToDetailDto(user);
    }

    public async Task<UserDetailDto?> GetCurrentUserAsync(string userId)
    {
        var user = await _userRepo.GetFirstAsync(u => u.UserId == userId && !u.FDeleted);
        if (user == null) return null;
        return await MapToDetailDto(user);
    }

    public async Task<UserDetailDto> CreateAsync(CreateUserRequest request)
    {
        var exists = await _userRepo.GetFirstAsync(u => u.UserId == request.UserId);
        if (exists != null)
            throw new ArgumentException($"用户ID '{request.UserId}' 已存在");

        await ValidatePaTypeAndPaIdAsync(request.PaType, request.PaId);

        var user = new SysLoginUser
        {
            UserId = request.UserId,
            UserName = request.UserName,
            PrPassword = request.Password,
            Email = request.Email,
            PaType = request.PaType,
            PaId = request.PaId,
            FCompanyId = string.IsNullOrEmpty(request.CompanyId) ? (_currentUser.CompanyId ?? string.Empty) : request.CompanyId,
            LastLoginTime = DateTime.Now,
            LastCpTime = DateTime.Now,
            LastLockTime = DateTime.Now,
            PwdErrTimes = 0,
            LockStatus = 0
        };

        var created = await _userRepo.InsertAsync(user);

        if (request.RoleIds != null && request.RoleIds.Any())
        {
            await AssignRolesInternalAsync(created.UserId, request.RoleIds);
        }

        return (await MapToDetailDto(created))!;
    }

    public async Task<bool> UpdateAsync(string uid, UpdateUserRequest request)
    {
        var user = await _userRepo.GetByIdAsync(uid);
        if (user == null || user.FDeleted)
            throw new KeyNotFoundException("用户不存在");
        if (user.UserId == "superadmin")
            throw new InvalidOperationException("不能修改超级管理员账号");

        await ValidatePaTypeAndPaIdAsync(request.PaType, request.PaId);

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.PaType = request.PaType;
        user.PaId = request.PaId;
        if (!string.IsNullOrEmpty(request.CompanyId))
            user.FCompanyId = request.CompanyId;

        return await _userRepo.UpdateAsync(user);
    }

    public async Task<bool> DeleteAsync(string uid)
    {
        var user = await _userRepo.GetByIdAsync(uid);
        if (user == null)
            throw new KeyNotFoundException("用户不存在");
        if (user.UserId == "superadmin")
            throw new InvalidOperationException("不能删除超级管理员账号");
        if (user.UserId == "admin")
            throw new InvalidOperationException("不能删除管理员账号");

        return await _userRepo.SoftDeleteAsync(uid);
    }

    public async Task<bool> ChangePasswordAsync(string uid, ChangePasswordRequest request)
    {
        var user = await _userRepo.GetByIdAsync(uid);
        if (user == null || user.FDeleted)
            throw new KeyNotFoundException("用户不存在");

        if (user.PrPassword != request.OldPassword)
            throw new ArgumentException("旧密码不正确");

        return await _db.Updateable<SysLoginUser>()
            .SetColumns(u => u.PrPassword == request.NewPassword)
            .SetColumns(u => u.LastCpTime == DateTime.Now)
            .SetColumns(u => u.MYmd == DateTime.Now)
            .SetColumns(u => u.MUser == (_currentUser.UserId ?? string.Empty))
            .Where(u => u.Uid == uid)
            .ExecuteCommandAsync() > 0;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userRepo.GetFirstAsync(u => u.UserId == request.UserId && !u.FDeleted);
        if (user == null)
            throw new KeyNotFoundException("用户不存在");
        if (user.UserId == "superadmin")
            throw new InvalidOperationException("不能重置超级管理员密码");

        return await _db.Updateable<SysLoginUser>()
            .SetColumns(u => u.PrPassword == request.NewPassword)
            .SetColumns(u => u.LastCpTime == DateTime.Now)
            .SetColumns(u => u.PwdErrTimes == 0)
            .SetColumns(u => u.LockStatus == 0)
            .SetColumns(u => u.MYmd == DateTime.Now)
            .SetColumns(u => u.MUser == (_currentUser.UserId ?? string.Empty))
            .Where(u => u.Uid == user.Uid)
            .ExecuteCommandAsync() > 0;
    }

    public async Task<bool> AssignRolesAsync(string uid, AssignRolesRequest request)
    {
        var user = await _userRepo.GetByIdAsync(uid);
        if (user == null || user.FDeleted)
            throw new KeyNotFoundException("用户不存在");
        if (user.UserId == "superadmin")
            throw new InvalidOperationException("不能修改超级管理员角色");

        // 删除旧的角色关联
        await _db.Deleteable<SysUserRoleRelation>()
            .Where(r => r.UserId == user.UserId)
            .ExecuteCommandAsync();

        return await AssignRolesInternalAsync(user.UserId, request.RoleIds);
    }

    public async Task<bool> UnlockAsync(string uid)
    {
        var user = await _userRepo.GetByIdAsync(uid);
        if (user == null || user.FDeleted)
            throw new KeyNotFoundException("用户不存在");
        if (user.UserId == "superadmin")
            throw new InvalidOperationException("不能操作超级管理员账号");

        return await _db.Updateable<SysLoginUser>()
            .SetColumns(u => u.LockStatus == 0)
            .SetColumns(u => u.PwdErrTimes == 0)
            .SetColumns(u => u.LastLockTime == null)
            .SetColumns(u => u.MYmd == DateTime.Now)
            .SetColumns(u => u.MUser == (_currentUser.UserId ?? string.Empty))
            .Where(u => u.Uid == uid)
            .ExecuteCommandAsync() > 0;
    }

    public async Task<bool> ToggleStatusAsync(string uid)
    {
        var user = await _userRepo.GetByIdAsync(uid);
        if (user == null || user.FDeleted)
            throw new KeyNotFoundException("用户不存在");
        if (user.UserId == "superadmin")
            throw new InvalidOperationException("不能禁用超级管理员账号");
        if (user.UserId == "admin")
            throw new InvalidOperationException("不能禁用管理员账号");

        var newStatus = user.FStatus == 0 ? 1 : 0;
        return await _db.Updateable<SysLoginUser>()
            .SetColumns(u => u.FStatus == newStatus)
            .SetColumns(u => u.MYmd == DateTime.Now)
            .SetColumns(u => u.MUser == (_currentUser.UserId ?? string.Empty))
            .Where(u => u.Uid == uid)
            .ExecuteCommandAsync() > 0;
    }

    private async Task<bool> AssignRolesInternalAsync(string userId, List<string> roleIds)
    {
        if (!roleIds.Any()) return true;

        var relations = roleIds.Select(roleId => new SysUserRoleRelation
        {
            Uid = Guid.NewGuid().ToString("N"),
            FInterId = Guid.NewGuid().ToString("N"),
            UserId = userId,
            Froleid = roleId,
            FCompanyId = _currentUser.CompanyId ?? string.Empty,
            CYmd = DateTime.Now,
            CUser = _currentUser.UserId ?? string.Empty,
            MYmd = DateTime.Now,
            MUser = _currentUser.UserId ?? string.Empty
        }).ToList();

        return await _db.Insertable(relations).ExecuteCommandAsync() > 0;
    }

    private async Task<UserDetailDto> MapToDetailDto(SysLoginUser user)
    {
        var roleRelations = await _db.Queryable<SysUserRoleRelation>()
            .Where(r => r.UserId == user.UserId && !r.FDeleted)
            .ToListAsync();

        var roleIds = roleRelations.Select(r => r.Froleid).ToList();
        var roles = roleIds.Any()
            ? await _db.Queryable<SysUserRole>().Where(r => roleIds.Contains(r.Uid) && !r.FDeleted).ToListAsync()
            : new List<SysUserRole>();

        // superadmin 拥有所有权限
        List<string> permissions;
        if (user.UserId == "superadmin")
        {
            permissions = new List<string> { "*:*" };
        }
        else
        {
            permissions = new List<string>();
            if (roleIds.Any())
            {
                permissions = await _db.Queryable<SysUserPermission>()
                    .Where(p => roleIds.Contains(p.Froleid) && !p.FDeleted)
                    .Select(p => p.PrCode)
                    .ToListAsync();
                permissions = permissions.Distinct().ToList();
            }
        }

        var orgs = await _db.Queryable<SysUserOrg>()
            .Where(o => o.UserId == user.UserId && !o.FDeleted)
            .ToListAsync();

        return new UserDetailDto
        {
            Uid = user.Uid,
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            FStatus = user.FStatus,
            LockStatus = user.LockStatus,
            LastLoginTime = user.LastLoginTime,
            CYmd = user.CYmd,
            PaType = user.PaType,
            PaId = user.PaId,
            IsDefault = user.IsDefault,
            CompanyId = user.FCompanyId,
            Roles = roles.Select(r => r.Frolename).ToList(),
            RoleDetails = roles.Select(r => new UserRoleDto
            {
                RoleId = r.Uid,
                RoleNumber = r.Frolenumber,
                RoleName = r.Frolename
            }).ToList(),
            Organizations = orgs.Select(o => new UserOrgInfo
            {
                OrgId = o.Forgid,
                IsDefault = o.Fisdefault
            }).ToList(),
            Permissions = permissions
        };
    }

    private async Task ValidatePaTypeAndPaIdAsync(string? paType, string? paId)
    {
        var hasType = !string.IsNullOrEmpty(paType);
        var hasId = !string.IsNullOrEmpty(paId);

        if (!hasType && hasId)
            throw new ArgumentException("未指定关联类别(PaType)时，不能设置关联内码(PaId)");

        if (hasType && !hasId)
            throw new ArgumentException("指定了关联类别(PaType)时，关联内码(PaId)不能为空");

        if (!hasType)
            return;

        switch (paType)
        {
            case "1":
                var emp = await _db.Queryable<THrEmpinfo>().Where(e => e.Uid == paId).FirstAsync();
                if (emp == null)
                    throw new ArgumentException($"关联的职员记录不存在(PaId={paId})");
                break;
            case "2":
                var supplier = await _db.Queryable<TBdSupplier>().Where(s => s.Uid == paId).FirstAsync();
                if (supplier == null)
                    throw new ArgumentException($"关联的厂商记录不存在(PaId={paId})");
                break;
            case "3":
                var customer = await _db.Queryable<TBdCustomer>().Where(c => c.Uid == paId).FirstAsync();
                if (customer == null)
                    throw new ArgumentException($"关联的客户记录不存在(PaId={paId})");
                break;
            default:
                throw new ArgumentException($"无效的关联类别(PaType={paType})，有效值为：1=职员, 2=厂商, 3=客户");
        }
    }
}
