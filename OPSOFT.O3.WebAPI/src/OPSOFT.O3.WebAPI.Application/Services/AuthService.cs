using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class AuthService : IAuthService
{
    private readonly ILoginUserRepository _loginUserRepository;
    private readonly IJwtService _jwtService;
    private readonly ISqlSugarClient _db;
    private const int MaxPwdErrTimes = 5;

    public AuthService(ILoginUserRepository loginUserRepository, IJwtService jwtService, ISqlSugarClient db)
    {
        _loginUserRepository = loginUserRepository;
        _jwtService = jwtService;
        _db = db;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _loginUserRepository.GetByUserIdAsync(request.UserId);

        if (user == null)
        {
            return new LoginResponse { Success = false, Message = "用户不存在" };
        }

        if (user.FDeleted)
        {
            return new LoginResponse { Success = false, Message = "用户已被删除" };
        }

        if (user.FStatus != 0)
        {
            return new LoginResponse { Success = false, Message = "用户已被禁用" };
        }

        if (user.LockStatus == 1)
        {
            if (user.LastLockTime.HasValue &&
                DateTime.Now.Subtract(user.LastLockTime.Value).TotalMinutes < 30)
            {
                return new LoginResponse { Success = false, Message = "账户已锁定，请30分钟后再试" };
            }
            await _loginUserRepository.UpdatePwdErrTimesAsync(user.Uid, 0, 0, null);
        }

        if (!VerifyPassword(request.Password, user.PrPassword))
        {
            var errTimes = user.PwdErrTimes + 1;
            var lockStatus = errTimes >= MaxPwdErrTimes ? 1 : 0;
            var lockTime = lockStatus == 1 ? DateTime.Now : (DateTime?)null;

            await _loginUserRepository.UpdatePwdErrTimesAsync(user.Uid, errTimes, lockStatus, lockTime);

            var remainTimes = MaxPwdErrTimes - errTimes;
            var message = lockStatus == 1
                ? "密码错误次数过多，账户已锁定"
                : $"密码错误，还剩{remainTimes}次机会";

            return new LoginResponse { Success = false, Message = message };
        }

        await _loginUserRepository.UpdatePwdErrTimesAsync(user.Uid, 0, 0, null);
        await _loginUserRepository.UpdateLastLoginTimeAsync(user.Uid);

        // 查询用户角色
        var roleRelations = await _db.Queryable<SysUserRoleRelation>()
            .Where(r => r.UserId == user.UserId && !r.FDeleted)
            .ToListAsync();

        var roleIds = roleRelations.Select(r => r.Froleid).ToList();
        var roles = new List<SysUserRole>();
        if (roleIds.Any())
        {
            roles = await _db.Queryable<SysUserRole>()
                .Where(r => roleIds.Contains(r.Uid) && !r.FDeleted)
                .ToListAsync();
        }

        var roleNames = roles.Select(r => r.Frolename).ToList();

        // 查询用户权限
        var permissions = new List<string>();
        if (user.UserId == "superadmin")
        {
            // superadmin 拥有所有权限
            permissions = new List<string> { "*:*" };
        }
        else if (roleIds.Any())
        {
            // 查询角色拥有的权限代码
            var allPermCodes = await _db.Queryable<SysUserPermission>()
                .Where(p => roleIds.Contains(p.Froleid) && !p.FDeleted)
                .Select(p => p.PrCode)
                .ToListAsync();

            // 排除被禁用的菜单按钮对应的权限代码
            var disabledPermCodes = await _db.Queryable<SysMenu>()
                .Where(m => m.MenuType == "B" && !m.FDeleted && m.FStatus != 0 && !string.IsNullOrEmpty(m.PermCode))
                .Select(m => m.PermCode)
                .ToListAsync();
            var disabledSet = new HashSet<string>(disabledPermCodes);

            permissions = allPermCodes.Where(c => !disabledSet.Contains(c)).Distinct().ToList();
        }

        // 查询用户组织
        var orgs = await _db.Queryable<SysUserOrg>()
            .Where(o => o.UserId == user.UserId && !o.FDeleted)
            .ToListAsync();

        var token = _jwtService.GenerateToken(user.Uid, user.UserId, user.UserName, user.FCompanyId, roleNames, user.PaType, user.PaId);

        return new LoginResponse
        {
            Success = true,
            Message = "登录成功",
            Token = token,
            UserInfo = new UserInfo
            {
                Uid = user.Uid,
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                CompanyId = user.FCompanyId,
                PaType = user.PaType,
                PaId = user.PaId,
                Roles = roleNames,
                Permissions = permissions,
                Organizations = orgs.Select(o => new UserOrgInfo
                {
                    OrgId = o.Forgid,
                    IsDefault = o.Fisdefault
                }).ToList()
            }
        };
    }

    private static bool VerifyPassword(string inputPassword, string storedPassword)
    {
        return inputPassword == storedPassword;
    }

    public async Task<List<string>> GetCurrentPermissionsAsync(string userId)
    {
        if (userId == "superadmin")
        {
            return new List<string> { "*:*" };
        }

        var roleIds = await _db.Queryable<SysUserRoleRelation>()
            .Where(r => r.UserId == userId && !r.FDeleted)
            .Select(r => r.Froleid)
            .ToListAsync();

        if (!roleIds.Any())
        {
            return new List<string>();
        }

        var allPermCodes = await _db.Queryable<SysUserPermission>()
            .Where(p => roleIds.Contains(p.Froleid) && !p.FDeleted)
            .Select(p => p.PrCode)
            .ToListAsync();

        var disabledPermCodes = await _db.Queryable<SysMenu>()
            .Where(m => m.MenuType == "B" && !m.FDeleted && m.FStatus != 0 && !string.IsNullOrEmpty(m.PermCode))
            .Select(m => m.PermCode)
            .ToListAsync();
        var disabledSet = new HashSet<string>(disabledPermCodes);

        return allPermCodes.Where(c => !disabledSet.Contains(c)).Distinct().ToList();
    }
}
