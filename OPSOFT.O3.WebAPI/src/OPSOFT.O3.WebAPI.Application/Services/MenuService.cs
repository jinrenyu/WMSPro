using OPSOFT.O3.WebAPI.Application.DTOs;
using OPSOFT.O3.WebAPI.Application.Interfaces;
using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Application.Services;

public class MenuService : IMenuService
{
    private readonly IRepository<SysMenu> _menuRepo;
    private readonly ISqlSugarClient _db;
    private readonly ICurrentUserService _currentUser;

    public MenuService(IRepository<SysMenu> menuRepo, ISqlSugarClient db, ICurrentUserService currentUser)
    {
        _menuRepo = menuRepo;
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<List<MenuTreeDto>> GetTreeAsync(bool activeOnly = false)
    {
        var query = _db.Queryable<SysMenu>().Where(m => !m.FDeleted);
        if (activeOnly)
        {
            query = query.Where(m => m.FStatus == 0);
        }
        var allMenus = await query.OrderBy(m => m.SortOrder).ToListAsync();

        var dtoList = allMenus.Select(MapToDto).ToList();
        return BuildTree(dtoList, string.Empty);
    }

    public async Task<MenuTreeDto?> GetByIdAsync(string uid)
    {
        var menu = await _menuRepo.GetByIdAsync(uid);
        if (menu == null || menu.FDeleted) return null;
        return MapToDto(menu);
    }

    public async Task<MenuTreeDto> CreateAsync(CreateMenuRequest request)
    {
        // 按钮类型必须有权限代码
        if (request.MenuType == "B" && string.IsNullOrWhiteSpace(request.PermCode))
            throw new ArgumentException("按钮类型必须填写权限代码");

        // 权限代码唯一性校验
        if (!string.IsNullOrWhiteSpace(request.PermCode))
        {
            var exists = await _db.Queryable<SysMenu>()
                .Where(m => m.PermCode == request.PermCode && !m.FDeleted)
                .AnyAsync();
            if (exists)
                throw new ArgumentException($"权限代码 '{request.PermCode}' 已存在");
        }

        var menu = new SysMenu
        {
            ParentId = request.ParentId,
            MenuName = request.MenuName,
            MenuType = request.MenuType,
            RoutePath = request.RoutePath,
            Icon = request.Icon,
            PermCode = request.PermCode,
            SortOrder = request.SortOrder,
            FStatus = request.FStatus
        };

        var created = await _menuRepo.InsertAsync(menu);
        return MapToDto(created);
    }

    public async Task<bool> UpdateAsync(string uid, UpdateMenuRequest request)
    {
        var menu = await _menuRepo.GetByIdAsync(uid);
        if (menu == null || menu.FDeleted)
            throw new KeyNotFoundException("菜单不存在");

        if (request.MenuType == "B" && string.IsNullOrWhiteSpace(request.PermCode))
            throw new ArgumentException("按钮类型必须填写权限代码");

        // 权限代码唯一性校验（排除自身）
        if (!string.IsNullOrWhiteSpace(request.PermCode))
        {
            var exists = await _db.Queryable<SysMenu>()
                .Where(m => m.PermCode == request.PermCode && m.Uid != uid && !m.FDeleted)
                .AnyAsync();
            if (exists)
                throw new ArgumentException($"权限代码 '{request.PermCode}' 已存在");
        }

        menu.ParentId = request.ParentId;
        menu.MenuName = request.MenuName;
        menu.MenuType = request.MenuType;
        menu.RoutePath = request.RoutePath;
        menu.Icon = request.Icon;
        menu.PermCode = request.PermCode;
        menu.SortOrder = request.SortOrder;
        menu.FStatus = request.FStatus;

        return await _menuRepo.UpdateAsync(menu);
    }

    public async Task<bool> DeleteAsync(string uid)
    {
        var menu = await _menuRepo.GetByIdAsync(uid);
        if (menu == null || menu.FDeleted)
            throw new KeyNotFoundException("菜单不存在");

        // 检查是否有子节点
        var hasChildren = await _db.Queryable<SysMenu>()
            .Where(m => m.ParentId == uid && !m.FDeleted)
            .AnyAsync();
        if (hasChildren)
            throw new InvalidOperationException("该菜单下有子节点，不能删除");

        return await _menuRepo.SoftDeleteAsync(uid);
    }

    private static MenuTreeDto MapToDto(SysMenu menu)
    {
        return new MenuTreeDto
        {
            Uid = menu.Uid,
            ParentId = menu.ParentId,
            MenuName = menu.MenuName,
            MenuType = menu.MenuType,
            RoutePath = menu.RoutePath,
            Icon = menu.Icon,
            PermCode = menu.PermCode,
            SortOrder = menu.SortOrder,
            FStatus = menu.FStatus,
            CYmd = menu.CYmd
        };
    }

    private static List<MenuTreeDto> BuildTree(List<MenuTreeDto> allNodes, string parentId)
    {
        return allNodes
            .Where(n => n.ParentId == parentId)
            .Select(n =>
            {
                n.Children = BuildTree(allNodes, n.Uid);
                return n;
            })
            .ToList();
    }
}
