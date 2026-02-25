using OPSOFT.O3.WebAPI.Domain.Entities;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Services;

/// <summary>
/// 数据种子服务 - 初始化默认数据
/// </summary>
public class DataSeedService
{
    private readonly ISqlSugarClient _db;

    public DataSeedService(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task SeedAsync()
    {
        await SeedRoleTypeAsync();
        await SeedAdminRoleAsync();
        await SeedAdminUserAsync();
        await SeedAdminRoleRelationAsync();
        await SeedMenusAsync();
        await SeedAdminPermissionsAsync();
        await SeedSuperAdminAsync();
        await SeedStatusAsync();
    }

    private async Task SeedRoleTypeAsync()
    {
        var exists = await _db.Queryable<SysRoleType>()
            .Where(r => r.Froletype == 1)
            .AnyAsync();

        if (!exists)
        {
            await _db.Insertable(new SysRoleType
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = "RT001",
                Froletype = 1,
                Frolename = "系统管理员",
                FCompanyId = "DEFAULT",
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    private async Task SeedAdminRoleAsync()
    {
        var exists = await _db.Queryable<SysUserRole>()
            .Where(r => r.Frolenumber == "ADMIN")
            .AnyAsync();

        if (!exists)
        {
            await _db.Insertable(new SysUserRole
            {
                Uid = "admin_role",
                FInterId = "admin_role",
                Frolenumber = "ADMIN",
                Frolename = "系统管理员",
                Froletype = 1,
                Isdefault = true,
                Fnote = "系统默认管理员角色",
                FCompanyId = "DEFAULT",
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    private async Task SeedAdminUserAsync()
    {
        var exists = await _db.Queryable<SysLoginUser>()
            .Where(u => u.UserId == "admin")
            .AnyAsync();

        if (!exists)
        {
            await _db.Insertable(new SysLoginUser
            {
                Uid = "admin_user",
                FInterId = "admin_user",
                UserId = "admin",
                UserName = "系统管理员",
                PrPassword = "admin123",
                Email = "admin@opsoft.com",
                IsDefault = true,
                FCompanyId = "DEFAULT",
                FStatus = 0,
                FDeleted = false,
                LastLoginTime = DateTime.Now,
                LastCpTime = DateTime.Now,
                LastLockTime = DateTime.Now,
                PwdErrTimes = 0,
                LockStatus = 0,
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    private async Task SeedAdminRoleRelationAsync()
    {
        var exists = await _db.Queryable<SysUserRoleRelation>()
            .Where(r => r.UserId == "admin" && r.Froleid == "admin_role")
            .AnyAsync();

        if (!exists)
        {
            await _db.Insertable(new SysUserRoleRelation
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = Guid.NewGuid().ToString("N"),
                UserId = "admin",
                Froleid = "admin_role",
                FCompanyId = "DEFAULT",
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    private async Task SeedMenusAsync()
    {
        var now = DateTime.Now;
        var menus = new List<SysMenu>();

        // Dashboard (M) - 顶级菜单
        menus.Add(new SysMenu
        {
            Uid = "menu_dashboard", FInterId = "menu_dashboard", ParentId = "", MenuName = "Dashboard",
            MenuType = "M", RoutePath = "/dashboard", Icon = "Odometer", PermCode = "", SortOrder = 0,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 系统管理 (D)
        var sysManageId = "menu_sys_manage";
        menus.Add(new SysMenu
        {
            Uid = sysManageId, FInterId = sysManageId, ParentId = "", MenuName = "系统管理",
            MenuType = "D", RoutePath = "", Icon = "Setting", PermCode = "", SortOrder = 99,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 用户管理 (M)
        var userMenuId = "menu_user";
        menus.Add(new SysMenu
        {
            Uid = userMenuId, FInterId = userMenuId, ParentId = sysManageId, MenuName = "用户管理",
            MenuType = "M", RoutePath = "/system/users", Icon = "User", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_user_list", userMenuId, "查看用户", "user:list", 1, now);
        AddButton(menus, "menu_user_add", userMenuId, "新增用户", "user:add", 2, now);
        AddButton(menus, "menu_user_edit", userMenuId, "编辑用户", "user:edit", 3, now);
        AddButton(menus, "menu_user_delete", userMenuId, "删除用户", "user:delete", 4, now);
        AddButton(menus, "menu_user_assign", userMenuId, "分配角色", "user:assign", 5, now);
        AddButton(menus, "menu_user_resetpwd", userMenuId, "重置密码", "user:reset-pwd", 6, now);
        AddButton(menus, "menu_user_unlock", userMenuId, "解锁用户", "user:unlock", 7, now);
        AddButton(menus, "menu_user_toggle", userMenuId, "启用/禁用", "user:toggle-status", 8, now);

        // 角色管理 (M)
        var roleMenuId = "menu_role";
        menus.Add(new SysMenu
        {
            Uid = roleMenuId, FInterId = roleMenuId, ParentId = sysManageId, MenuName = "角色管理",
            MenuType = "M", RoutePath = "/system/roles", Icon = "UserFilled", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_role_list", roleMenuId, "查看角色", "role:list", 1, now);
        AddButton(menus, "menu_role_add", roleMenuId, "新增角色", "role:add", 2, now);
        AddButton(menus, "menu_role_edit", roleMenuId, "编辑角色", "role:edit", 3, now);
        AddButton(menus, "menu_role_delete", roleMenuId, "删除角色", "role:delete", 4, now);
        AddButton(menus, "menu_role_assign", roleMenuId, "分配权限", "role:assign", 5, now);

        // 菜单管理 (M)
        var menuMenuId = "menu_menu";
        menus.Add(new SysMenu
        {
            Uid = menuMenuId, FInterId = menuMenuId, ParentId = sysManageId, MenuName = "菜单管理",
            MenuType = "M", RoutePath = "/system/menus", Icon = "Menu", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_menu_list", menuMenuId, "查看菜单", "menu:list", 1, now);
        AddButton(menus, "menu_menu_add", menuMenuId, "新增菜单", "menu:add", 2, now);
        AddButton(menus, "menu_menu_edit", menuMenuId, "编辑菜单", "menu:edit", 3, now);
        AddButton(menus, "menu_menu_delete", menuMenuId, "删除菜单", "menu:delete", 4, now);

        // 基础资料 (D)
        var masterDataId = "menu_master_data";
        menus.Add(new SysMenu
        {
            Uid = masterDataId, FInterId = masterDataId, ParentId = "", MenuName = "基础资料",
            MenuType = "D", RoutePath = "", Icon = "Files", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 物料管理 (M)
        var materialMenuId = "menu_material";
        menus.Add(new SysMenu
        {
            Uid = materialMenuId, FInterId = materialMenuId, ParentId = masterDataId, MenuName = "物料管理",
            MenuType = "M", RoutePath = "/master/materials", Icon = "Box", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_material_list", materialMenuId, "查看物料", "material:list", 1, now);
        AddButton(menus, "menu_material_add", materialMenuId, "新增物料", "material:add", 2, now);
        AddButton(menus, "menu_material_edit", materialMenuId, "编辑物料", "material:edit", 3, now);
        AddButton(menus, "menu_material_delete", materialMenuId, "删除物料", "material:delete", 4, now);
        AddButton(menus, "menu_material_approve", materialMenuId, "审核物料", "material:approve", 5, now);

        // 客户管理 (M)
        var customerMenuId = "menu_customer";
        menus.Add(new SysMenu
        {
            Uid = customerMenuId, FInterId = customerMenuId, ParentId = masterDataId, MenuName = "客户管理",
            MenuType = "M", RoutePath = "/master/customers", Icon = "Avatar", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_customer_list", customerMenuId, "查看客户", "customer:list", 1, now);
        AddButton(menus, "menu_customer_add", customerMenuId, "新增客户", "customer:add", 2, now);
        AddButton(menus, "menu_customer_edit", customerMenuId, "编辑客户", "customer:edit", 3, now);
        AddButton(menus, "menu_customer_delete", customerMenuId, "删除客户", "customer:delete", 4, now);
        AddButton(menus, "menu_customer_approve", customerMenuId, "审核客户", "customer:approve", 5, now);

        // 供应商管理 (M)
        var supplierMenuId = "menu_supplier";
        menus.Add(new SysMenu
        {
            Uid = supplierMenuId, FInterId = supplierMenuId, ParentId = masterDataId, MenuName = "供应商管理",
            MenuType = "M", RoutePath = "/master/suppliers", Icon = "Van", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_supplier_list", supplierMenuId, "查看供应商", "supplier:list", 1, now);
        AddButton(menus, "menu_supplier_add", supplierMenuId, "新增供应商", "supplier:add", 2, now);
        AddButton(menus, "menu_supplier_edit", supplierMenuId, "编辑供应商", "supplier:edit", 3, now);
        AddButton(menus, "menu_supplier_delete", supplierMenuId, "删除供应商", "supplier:delete", 4, now);
        AddButton(menus, "menu_supplier_approve", supplierMenuId, "审核供应商", "supplier:approve", 5, now);

        // 币种管理 (M)
        var currencyMenuId = "menu_currency";
        menus.Add(new SysMenu
        {
            Uid = currencyMenuId, FInterId = currencyMenuId, ParentId = masterDataId, MenuName = "币种管理",
            MenuType = "M", RoutePath = "/master/currencies", Icon = "Money", PermCode = "", SortOrder = 4,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_currency_list", currencyMenuId, "查看币种", "currency:list", 1, now);
        AddButton(menus, "menu_currency_add", currencyMenuId, "新增币种", "currency:add", 2, now);
        AddButton(menus, "menu_currency_edit", currencyMenuId, "编辑币种", "currency:edit", 3, now);
        AddButton(menus, "menu_currency_delete", currencyMenuId, "删除币种", "currency:delete", 4, now);
        AddButton(menus, "menu_currency_approve", currencyMenuId, "审核币种", "currency:approve", 5, now);

        // 仓库管理 (M)
        var warehouseMenuId = "menu_warehouse";
        menus.Add(new SysMenu
        {
            Uid = warehouseMenuId, FInterId = warehouseMenuId, ParentId = masterDataId, MenuName = "仓库管理",
            MenuType = "M", RoutePath = "/master/warehouses", Icon = "House", PermCode = "", SortOrder = 5,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_warehouse_list", warehouseMenuId, "查看仓库", "warehouse:list", 1, now);
        AddButton(menus, "menu_warehouse_add", warehouseMenuId, "新增仓库", "warehouse:add", 2, now);
        AddButton(menus, "menu_warehouse_edit", warehouseMenuId, "编辑仓库", "warehouse:edit", 3, now);
        AddButton(menus, "menu_warehouse_delete", warehouseMenuId, "删除仓库", "warehouse:delete", 4, now);
        AddButton(menus, "menu_warehouse_approve", warehouseMenuId, "审核仓库", "warehouse:approve", 5, now);

        // 日志管理 (D) - 系统管理下的三级目录
        var logManageId = "menu_log_manage";
        menus.Add(new SysMenu
        {
            Uid = logManageId, FInterId = logManageId, ParentId = sysManageId, MenuName = "日志管理",
            MenuType = "D", RoutePath = "", Icon = "Notebook", PermCode = "", SortOrder = 90,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 请求日志 (M) - 日志管理下
        var requestLogMenuId = "menu_requestlog";
        menus.Add(new SysMenu
        {
            Uid = requestLogMenuId, FInterId = requestLogMenuId, ParentId = logManageId, MenuName = "请求日志",
            MenuType = "M", RoutePath = "/system/logs/request", Icon = "Connection", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_requestlog_list", requestLogMenuId, "查看请求日志", "requestlog:list", 1, now);
        AddButton(menus, "menu_requestlog_export", requestLogMenuId, "导出请求日志", "requestlog:export", 2, now);
        AddButton(menus, "menu_requestlog_statistics", requestLogMenuId, "请求日志统计", "requestlog:statistics", 3, now);

        // 部门管理 (M) - 放在系统管理下
        var deptMenuId = "menu_dept";
        menus.Add(new SysMenu
        {
            Uid = deptMenuId, FInterId = deptMenuId, ParentId = sysManageId, MenuName = "部门管理",
            MenuType = "M", RoutePath = "/system/depts", Icon = "OfficeBuilding", PermCode = "", SortOrder = 4,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_dept_list", deptMenuId, "查看部门", "dept:list", 1, now);
        AddButton(menus, "menu_dept_add", deptMenuId, "新增部门", "dept:add", 2, now);
        AddButton(menus, "menu_dept_edit", deptMenuId, "编辑部门", "dept:edit", 3, now);
        AddButton(menus, "menu_dept_delete", deptMenuId, "删除部门", "dept:delete", 4, now);

        // 单位管理 (M)
        var unitMenuId = "menu_unit";
        menus.Add(new SysMenu
        {
            Uid = unitMenuId, FInterId = unitMenuId, ParentId = masterDataId, MenuName = "单位管理",
            MenuType = "M", RoutePath = "/master/units", Icon = "ScaleToOriginal", PermCode = "", SortOrder = 7,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_unit_list", unitMenuId, "查看单位", "unit:list", 1, now);
        AddButton(menus, "menu_unit_add", unitMenuId, "新增单位", "unit:add", 2, now);
        AddButton(menus, "menu_unit_edit", unitMenuId, "编辑单位", "unit:edit", 3, now);
        AddButton(menus, "menu_unit_delete", unitMenuId, "删除单位", "unit:delete", 4, now);
        AddButton(menus, "menu_unit_approve", unitMenuId, "审核单位", "unit:approve", 5, now);

        // 仓位管理 (M)
        var stockPlaceMenuId = "menu_stockplace";
        menus.Add(new SysMenu
        {
            Uid = stockPlaceMenuId, FInterId = stockPlaceMenuId, ParentId = masterDataId, MenuName = "仓位管理",
            MenuType = "M", RoutePath = "/master/stockplaces", Icon = "Grid", PermCode = "", SortOrder = 8,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_stockplace_list", stockPlaceMenuId, "查看仓位", "stockplace:list", 1, now);
        AddButton(menus, "menu_stockplace_add", stockPlaceMenuId, "新增仓位", "stockplace:add", 2, now);
        AddButton(menus, "menu_stockplace_edit", stockPlaceMenuId, "编辑仓位", "stockplace:edit", 3, now);
        AddButton(menus, "menu_stockplace_delete", stockPlaceMenuId, "删除仓位", "stockplace:delete", 4, now);
        AddButton(menus, "menu_stockplace_approve", stockPlaceMenuId, "审核仓位", "stockplace:approve", 5, now);

        // 辅助资料 (M)
        var assistantDataMenuId = "menu_assistantdata";
        menus.Add(new SysMenu
        {
            Uid = assistantDataMenuId, FInterId = assistantDataMenuId, ParentId = masterDataId, MenuName = "辅助资料",
            MenuType = "M", RoutePath = "/master/assistantdata", Icon = "Collection", PermCode = "", SortOrder = 9,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_assistantdata_list", assistantDataMenuId, "查看辅助资料", "assistantdata:list", 1, now);
        AddButton(menus, "menu_assistantdata_add", assistantDataMenuId, "新增辅助资料", "assistantdata:add", 2, now);
        AddButton(menus, "menu_assistantdata_edit", assistantDataMenuId, "编辑辅助资料", "assistantdata:edit", 3, now);
        AddButton(menus, "menu_assistantdata_delete", assistantDataMenuId, "删除辅助资料", "assistantdata:delete", 4, now);
        AddButton(menus, "menu_assistantdata_approve", assistantDataMenuId, "审核辅助资料", "assistantdata:approve", 5, now);

        // 职员管理 (M)
        var employeeMenuId = "menu_employee";
        menus.Add(new SysMenu
        {
            Uid = employeeMenuId, FInterId = employeeMenuId, ParentId = masterDataId, MenuName = "职员管理",
            MenuType = "M", RoutePath = "/master/employees", Icon = "Postcard", PermCode = "", SortOrder = 10,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_employee_list", employeeMenuId, "查看职员", "employee:list", 1, now);
        AddButton(menus, "menu_employee_add", employeeMenuId, "新增职员", "employee:add", 2, now);
        AddButton(menus, "menu_employee_edit", employeeMenuId, "编辑职员", "employee:edit", 3, now);
        AddButton(menus, "menu_employee_delete", employeeMenuId, "删除职员", "employee:delete", 4, now);
        AddButton(menus, "menu_employee_approve", employeeMenuId, "审核职员", "employee:approve", 5, now);

        // 物料条码类型 (M)
        var materialBarTypeMenuId = "menu_materialbartype";
        menus.Add(new SysMenu
        {
            Uid = materialBarTypeMenuId, FInterId = materialBarTypeMenuId, ParentId = masterDataId, MenuName = "物料条码类型",
            MenuType = "M", RoutePath = "/master/materialbartypes", Icon = "Ticket", PermCode = "", SortOrder = 11,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_materialbartype_list", materialBarTypeMenuId, "查看物料条码类型", "materialbartype:list", 1, now);
        AddButton(menus, "menu_materialbartype_add", materialBarTypeMenuId, "新增物料条码类型", "materialbartype:add", 2, now);
        AddButton(menus, "menu_materialbartype_edit", materialBarTypeMenuId, "编辑物料条码类型", "materialbartype:edit", 3, now);
        AddButton(menus, "menu_materialbartype_delete", materialBarTypeMenuId, "删除物料条码类型", "materialbartype:delete", 4, now);
        AddButton(menus, "menu_materialbartype_approve", materialBarTypeMenuId, "审核物料条码类型", "materialbartype:approve", 5, now);

        // 过滤出数据库中不存在的菜单，补入
        var existingUids = await _db.Queryable<SysMenu>()
            .Select(m => m.Uid)
            .ToListAsync();
        var existingUidSet = new HashSet<string>(existingUids);
        var missingMenus = menus.Where(m => !existingUidSet.Contains(m.Uid)).ToList();

        if (missingMenus.Any())
        {
            await _db.Insertable(missingMenus).ExecuteCommandAsync();
        }
    }

    private static void AddButton(List<SysMenu> menus, string uid, string parentId, string name, string permCode, int sort, DateTime now)
    {
        menus.Add(new SysMenu
        {
            Uid = uid, FInterId = uid, ParentId = parentId, MenuName = name,
            MenuType = "B", RoutePath = "", Icon = "", PermCode = permCode, SortOrder = sort,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
    }

    private async Task SeedAdminPermissionsAsync()
    {
        // 从菜单表动态读取所有按钮权限代码
        var permissionCodes = await _db.Queryable<SysMenu>()
            .Where(m => m.MenuType == "B" && !m.FDeleted && !string.IsNullOrEmpty(m.PermCode))
            .Select(m => m.PermCode)
            .ToListAsync();

        var existingCodes = await _db.Queryable<SysUserPermission>()
            .Where(p => p.Froleid == "admin_role" && !p.FDeleted)
            .Select(p => p.PrCode)
            .ToListAsync();

        var missingCodes = permissionCodes.Where(c => !existingCodes.Contains(c)).ToList();

        if (missingCodes.Any())
        {
            var permissions = missingCodes.Select(code => new SysUserPermission
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = Guid.NewGuid().ToString("N"),
                Froleid = "admin_role",
                PrCode = code,
                FCompanyId = "DEFAULT",
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ToList();

            await _db.Insertable(permissions).ExecuteCommandAsync();
        }
    }

    private async Task SeedSuperAdminAsync()
    {
        // 创建超级管理员用户（superadmin 默认拥有所有权限，无需角色授权）
        var exists = await _db.Queryable<SysLoginUser>()
            .Where(u => u.UserId == "superadmin")
            .AnyAsync();

        if (!exists)
        {
            await _db.Insertable(new SysLoginUser
            {
                Uid = "superadmin_user",
                FInterId = "superadmin_user",
                UserId = "superadmin",
                UserName = "超级管理员",
                PrPassword = "123456",
                Email = "superadmin@opsoft.com",
                IsDefault = true,
                FCompanyId = "DEFAULT",
                FStatus = 0,
                FDeleted = false,
                LastLoginTime = DateTime.Now,
                LastCpTime = DateTime.Now,
                LastLockTime = DateTime.Now,
                PwdErrTimes = 0,
                LockStatus = 0,
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    private async Task SeedStatusAsync()
    {
        var now = DateTime.Now;
        var statusList = new List<SysStatus>
        {
            new() { Uid = "01", FInterId = "10", Fitemid = 10, Fname = "暂存", Isdefault = true, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system" },
            new() { Uid = "02", FInterId = "20", Fitemid = 20, Fname = "提交", Isdefault = true, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system" },
            new() { Uid = "03", FInterId = "30", Fitemid = 30, Fname = "确认", Isdefault = true, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system" },
            new() { Uid = "04", FInterId = "40", Fitemid = 40, Fname = "审核", Isdefault = true, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system" },
            new() { Uid = "05", FInterId = "50", Fitemid = 50, Fname = "复核", Isdefault = true, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system" },
            new() { Uid = "06", FInterId = "60", Fitemid = 60, Fname = "结案", Isdefault = true, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system" },
            new() { Uid = "07", FInterId = "70", Fitemid = 70, Fname = "关闭", Isdefault = true, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system" },
            new() { Uid = "08", FInterId = "80", Fitemid = 80, Fname = "作废", Isdefault = true, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system" },
        };

        var existingUids = await _db.Queryable<SysStatus>()
            .Select(s => s.Uid)
            .ToListAsync();
        var existingUidSet = new HashSet<string>(existingUids);
        var missingStatus = statusList.Where(s => !existingUidSet.Contains(s.Uid)).ToList();

        if (missingStatus.Any())
        {
            await _db.Insertable(missingStatus).ExecuteCommandAsync();
        }
    }
}
