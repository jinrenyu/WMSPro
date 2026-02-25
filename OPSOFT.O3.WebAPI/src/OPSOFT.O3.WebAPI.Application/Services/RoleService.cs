using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRepository<SysUserRole> _roleRepo;
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;

    public RoleService(IRepository<SysUserRole> roleRepo, ISqlSugarClient db, ICurrentUserService currentUser)
    {
        _roleRepo = roleRepo;
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<PagedResult<RoleDto>> GetPagedListAsync(PagedRequest request)
    {
        var query = _db.Queryable<SysUserRole>().Where(r => !r.FDeleted);

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            query = query.Where(r => r.Frolenumber.Contains(request.Keyword) || r.Frolename.Contains(request.Keyword));
        }

        var totalCount = new RefAsync<int>(0);
        var roles = await query
            .OrderBy(r => r.CYmd, OrderByType.Desc)
            .ToPageListAsync(request.PageIndex, request.PageSize, totalCount);

        // 查询角色类型名称
        var roleTypeIds = roles.Select(r => r.Froletype).Distinct().ToList();
        var roleTypes = roleTypeIds.Any()
            ? await _db.Queryable<SysRoleType>().Where(t => roleTypeIds.Contains(t.Froletype)).ToListAsync()
            : new List<SysRoleType>();

        var items = roles.Select(r => new RoleDto
        {
            Uid = r.Uid,
            RoleNumber = r.Frolenumber,
            RoleName = r.Frolename,
            RoleType = r.Froletype,
            RoleTypeName = roleTypes.FirstOrDefault(t => t.Froletype == r.Froletype)?.Frolename ?? string.Empty,
            IsDefault = r.Isdefault,
            Note = r.Fnote,
            CYmd = r.CYmd
        }).ToList();

        return new PagedResult<RoleDto>
        {
            Items = items,
            TotalCount = totalCount.Value,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize
        };
    }

    public async Task<RoleDetailDto?> GetByIdAsync(string uid)
    {
        var role = await _roleRepo.GetByIdAsync(uid);
        if (role == null || role.FDeleted) return null;

        var permissions = await _db.Queryable<SysUserPermission>()
            .Where(p => p.Froleid == uid && !p.FDeleted)
            .Select(p => p.PrCode)
            .ToListAsync();

        var roleType = await _db.Queryable<SysRoleType>()
            .Where(t => t.Froletype == role.Froletype)
            .FirstAsync();

        return new RoleDetailDto
        {
            Uid = role.Uid,
            RoleNumber = role.Frolenumber,
            RoleName = role.Frolename,
            RoleType = role.Froletype,
            RoleTypeName = roleType?.Frolename ?? string.Empty,
            IsDefault = role.Isdefault,
            Note = role.Fnote,
            CYmd = role.CYmd,
            Permissions = permissions
        };
    }

    public async Task<RoleDto> CreateAsync(CreateRoleRequest request)
    {
        var exists = await _roleRepo.GetFirstAsync(r => r.Frolenumber == request.RoleNumber && !r.FDeleted);
        if (exists != null)
            throw new ArgumentException($"角色代码 '{request.RoleNumber}' 已存在");

        var role = new SysUserRole
        {
            Frolenumber = request.RoleNumber,
            Frolename = request.RoleName,
            Froletype = request.RoleType,
            Fnote = request.Note
        };

        var created = await _roleRepo.InsertAsync(role);

        return new RoleDto
        {
            Uid = created.Uid,
            RoleNumber = created.Frolenumber,
            RoleName = created.Frolename,
            RoleType = created.Froletype,
            Note = created.Fnote,
            CYmd = created.CYmd
        };
    }

    public async Task<bool> UpdateAsync(string uid, UpdateRoleRequest request)
    {
        var role = await _roleRepo.GetByIdAsync(uid);
        if (role == null || role.FDeleted)
            throw new KeyNotFoundException("角色不存在");

        role.Frolename = request.RoleName;
        role.Froletype = request.RoleType;
        role.Fnote = request.Note;

        return await _roleRepo.UpdateAsync(role);
    }

    public async Task<bool> DeleteAsync(string uid)
    {
        var role = await _roleRepo.GetByIdAsync(uid);
        if (role == null)
            throw new KeyNotFoundException("角色不存在");
        if (role.Isdefault)
            throw new InvalidOperationException("不能删除系统默认角色");

        return await _roleRepo.SoftDeleteAsync(uid);
    }

    public async Task<List<string>> GetPermissionsAsync(string roleId)
    {
        return await _db.Queryable<SysUserPermission>()
            .Where(p => p.Froleid == roleId && !p.FDeleted)
            .Select(p => p.PrCode)
            .ToListAsync();
    }

    public async Task<bool> AssignPermissionsAsync(string roleId, AssignPermissionsRequest request)
    {
        var role = await _roleRepo.GetByIdAsync(roleId);
        if (role == null || role.FDeleted)
            throw new KeyNotFoundException("角色不存在");

        // 删除旧权限
        await _db.Deleteable<SysUserPermission>()
            .Where(p => p.Froleid == roleId)
            .ExecuteCommandAsync();

        if (!request.PermissionCodes.Any()) return true;

        // 插入新权限
        var permissions = request.PermissionCodes.Select(code => new SysUserPermission
        {
            Uid = Guid.NewGuid().ToString("N"),
            FInterId = Guid.NewGuid().ToString("N"),
            Froleid = roleId,
            PrCode = code,
            FCompanyId = _currentUser.CompanyId ?? string.Empty,
            CYmd = DateTime.Now,
            CUser = _currentUser.UserId ?? string.Empty,
            MYmd = DateTime.Now,
            MUser = _currentUser.UserId ?? string.Empty
        }).ToList();

        return await _db.Insertable(permissions).ExecuteCommandAsync() > 0;
    }
}
