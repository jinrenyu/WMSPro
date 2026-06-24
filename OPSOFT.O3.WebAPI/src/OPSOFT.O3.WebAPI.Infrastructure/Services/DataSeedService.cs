using OPSOFT.O3.WebAPI.Application.Services;
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
        await SeedFlexAuxPropertiesAsync();
        await SeedOrgsAsync();
        await SeedMaterialTypesAsync();
        await SeedBillTypesAsync();
        await SeedBillCodeFormsAsync();
        await SeedBillCodeRulesAsync();
        await SeedSourceBillsAsync();
        await SeedApproveExistingMaterialsAsync();
    }

    private async Task SeedFlexAuxPropertiesAsync()
    {
        var defs = new (string Number, string Name)[]
        {
            ("AUXP001", "颜色"),
            ("AUXP002", "尺寸"),
            ("AUXP003", "批次"),
            ("AUXP004", "等级"),
        };
        foreach (var (number, name) in defs)
        {
            var exists = await _db.Queryable<TBdFlexauxproperty>().Where(p => p.Fnumber == number).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBdFlexauxproperty
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = number,
                Fnumber = number,
                Fname = name,
                Fvaluetype = "1",
                FCompanyId = "DEFAULT",
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    private async Task SeedOrgsAsync()
    {
        var orgs = new[]
        {
            new { Uid = "org_hq", Num = "ORG-HQ", Name = "总公司",     Parent = "" },
            new { Uid = "org_sh", Num = "ORG-SH", Name = "上海分公司", Parent = "org_hq" },
            new { Uid = "org_bj", Num = "ORG-BJ", Name = "北京分公司", Parent = "org_hq" },
        };
        foreach (var o in orgs)
        {
            var exists = await _db.Queryable<SysOrgStructure>().Where(x => x.Uid == o.Uid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new SysOrgStructure
            {
                Uid = o.Uid,
                FInterId = o.Uid,
                Fparaid = o.Parent,
                Fnumber = o.Num,
                Fname = o.Name,
                Fcheckdate = DateTime.MinValue,
                Fdisabledate = DateTime.MinValue,
                FCompanyId = "DEFAULT",
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }

        // 用户-组织关系：admin / superadmin 各挂 3 个组织，默认总公司
        var assignments = new[]
        {
            new { User = "admin",      Org = "org_hq", Def = true },
            new { User = "admin",      Org = "org_sh", Def = false },
            new { User = "admin",      Org = "org_bj", Def = false },
            new { User = "superadmin", Org = "org_hq", Def = true },
            new { User = "superadmin", Org = "org_sh", Def = false },
            new { User = "superadmin", Org = "org_bj", Def = false },
        };
        foreach (var a in assignments)
        {
            var exists = await _db.Queryable<SysUserOrg>().Where(x => x.UserId == a.User && x.Forgid == a.Org).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new SysUserOrg
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = Guid.NewGuid().ToString("N"),
                UserId = a.User,
                Forgid = a.Org,
                Fisdefault = a.Def,
                FCompanyId = "DEFAULT",
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    private async Task SeedMaterialTypesAsync()
    {
        var types = new (string Num, string Name)[]
        {
            ("MT-RAW",  "原材料"),
            ("MT-SEMI", "半成品"),
            ("MT-FIN",  "成品"),
            ("MT-OUT",  "外购件"),
            ("MT-CONS", "消耗品"),
        };
        foreach (var (num, name) in types)
        {
            var exists = await _db.Queryable<TBdMaterialtype>().Where(x => x.Fnumber == num).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBdMaterialtype
            {
                Uid = Guid.NewGuid().ToString("N"),
                FInterId = num,
                Fnumber = num,
                Fname = name,
                Fcheckdate = DateTime.MinValue,
                Fdisabledate = DateTime.MinValue,
                FCompanyId = "DEFAULT",
                CYmd = DateTime.Now,
                CUser = "system",
                MYmd = DateTime.Now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
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
                PrPassword = PasswordHelper.HashPassword("admin123"),
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

        // 编码规则 (M) - 系统管理下（单据编号/条码编号规则配置）
        var billCodeRuleMenuId = "menu_billcoderule";
        menus.Add(new SysMenu
        {
            Uid = billCodeRuleMenuId, FInterId = billCodeRuleMenuId, ParentId = sysManageId, MenuName = "编码规则",
            MenuType = "M", RoutePath = "/system/code-rules", Icon = "Tickets", PermCode = "", SortOrder = 5,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_billcoderule_list", billCodeRuleMenuId, "查看编码规则", "billcoderule:list", 1, now);
        AddButton(menus, "menu_billcoderule_edit", billCodeRuleMenuId, "编辑编码规则", "billcoderule:edit", 2, now);

        // 出入库流程配置 (M) - 系统管理下（源单类型/下推目标映射，T_BOS_SELBILL）
        var selBillMenuId = "menu_selbill";
        menus.Add(new SysMenu
        {
            Uid = selBillMenuId, FInterId = selBillMenuId, ParentId = sysManageId, MenuName = "出入库流程配置",
            MenuType = "M", RoutePath = "/system/selbills", Icon = "Connection", PermCode = "", SortOrder = 6,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_selbill_list", selBillMenuId, "查看出入库流程", "selbill:list", 1, now);
        AddButton(menus, "menu_selbill_add", selBillMenuId, "新增出入库流程", "selbill:add", 2, now);
        AddButton(menus, "menu_selbill_edit", selBillMenuId, "编辑出入库流程", "selbill:edit", 3, now);
        AddButton(menus, "menu_selbill_approve", selBillMenuId, "审核出入库流程", "selbill:approve", 4, now);
        AddButton(menus, "menu_selbill_delete", selBillMenuId, "删除出入库流程", "selbill:delete", 5, now);
        AddButton(menus, "menu_selbill_disable", selBillMenuId, "禁用出入库流程", "selbill:disable", 6, now);

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
        // 仓位（仓位集 FlexValues）按钮 —— "仓位管理"菜单已重指向 T_BAS_FLEXVALUES 维护页
        AddButton(menus, "menu_flexvalues_list", stockPlaceMenuId, "查看仓位", "flexvalues:list", 1, now);
        AddButton(menus, "menu_flexvalues_add", stockPlaceMenuId, "新增仓位", "flexvalues:add", 2, now);
        AddButton(menus, "menu_flexvalues_edit", stockPlaceMenuId, "编辑仓位", "flexvalues:edit", 3, now);
        AddButton(menus, "menu_flexvalues_delete", stockPlaceMenuId, "删除仓位", "flexvalues:delete", 4, now);
        AddButton(menus, "menu_flexvalues_approve", stockPlaceMenuId, "审核仓位", "flexvalues:approve", 5, now);
        AddButton(menus, "menu_flexvalues_disable", stockPlaceMenuId, "禁用仓位", "flexvalues:disable", 6, now);
        // 旧 StockPlace 按钮保留（不再有界面引用）
        AddButton(menus, "menu_stockplace_list", stockPlaceMenuId, "查看仓位", "stockplace:list", 7, now);
        AddButton(menus, "menu_stockplace_add", stockPlaceMenuId, "新增仓位", "stockplace:add", 8, now);
        AddButton(menus, "menu_stockplace_edit", stockPlaceMenuId, "编辑仓位", "stockplace:edit", 9, now);
        AddButton(menus, "menu_stockplace_delete", stockPlaceMenuId, "删除仓位", "stockplace:delete", 10, now);
        AddButton(menus, "menu_stockplace_approve", stockPlaceMenuId, "审核仓位", "stockplace:approve", 11, now);

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
        AddButton(menus, "menu_materialbartype_disable", materialBarTypeMenuId, "禁用物料条码类型", "materialbartype:disable", 6, now);

        // ═══════════════════════════════════════════════════════════════
        // 采购管理 (D) - 一级菜单，落在基础资料(1)与系统管理(99)之间
        // ═══════════════════════════════════════════════════════════════
        var purchaseId = "menu_purchase";
        menus.Add(new SysMenu
        {
            Uid = purchaseId, FInterId = purchaseId, ParentId = "", MenuName = "采购管理",
            MenuType = "D", RoutePath = "", Icon = "ShoppingCart", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // ── 二级分组：采购业务 (D) ──
        var purchaseBizId = "menu_purchase_biz";
        menus.Add(new SysMenu
        {
            Uid = purchaseBizId, FInterId = purchaseBizId, ParentId = purchaseId, MenuName = "采购业务",
            MenuType = "D", RoutePath = "", Icon = "Tickets", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 采购申请单 (M)
        var purchaseReqId = "menu_purchase_req";
        menus.Add(new SysMenu
        {
            Uid = purchaseReqId, FInterId = purchaseReqId, ParentId = purchaseBizId, MenuName = "采购申请单",
            MenuType = "M", RoutePath = "/purchase/requests", Icon = "DocumentAdd", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_purchase_req_list", purchaseReqId, "查看采购申请单", "purchasereq:list", 1, now);
        AddButton(menus, "menu_purchase_req_add", purchaseReqId, "新增采购申请单", "purchasereq:add", 2, now);
        AddButton(menus, "menu_purchase_req_edit", purchaseReqId, "编辑采购申请单", "purchasereq:edit", 3, now);

        // 采购订单 (M)
        var purchaseOrderId = "menu_purchase_order";
        menus.Add(new SysMenu
        {
            Uid = purchaseOrderId, FInterId = purchaseOrderId, ParentId = purchaseBizId, MenuName = "采购订单",
            MenuType = "M", RoutePath = "/purchase/orders", Icon = "Document", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_purchase_order_list", purchaseOrderId, "查看采购订单", "purchaseorder:list", 1, now);
        AddButton(menus, "menu_purchase_order_add", purchaseOrderId, "新增采购订单", "purchaseorder:add", 2, now);
        AddButton(menus, "menu_purchase_order_edit", purchaseOrderId, "编辑采购订单", "purchaseorder:edit", 3, now);
        AddButton(menus, "menu_purchase_order_approve", purchaseOrderId, "审核采购订单", "purchaseorder:approve", 4, now);
        AddButton(menus, "menu_purchase_order_delete", purchaseOrderId, "删除采购订单", "purchaseorder:delete", 5, now);
        AddButton(menus, "menu_purchase_order_push", purchaseOrderId, "下推采购订单", "purchaseorder:push", 6, now);
        AddButton(menus, "menu_purchase_order_trace", purchaseOrderId, "下查采购订单", "purchaseorder:trace", 7, now);

        // 收料通知单 (M)
        var receiveNoticeId = "menu_receive_notice";
        menus.Add(new SysMenu
        {
            Uid = receiveNoticeId, FInterId = receiveNoticeId, ParentId = purchaseBizId, MenuName = "收料通知单",
            MenuType = "M", RoutePath = "/purchase/receive-notices", Icon = "Bell", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_receive_notice_list", receiveNoticeId, "查看收料通知单", "receivenotice:list", 1, now);
        AddButton(menus, "menu_receive_notice_add", receiveNoticeId, "新增收料通知单", "receivenotice:add", 2, now);
        AddButton(menus, "menu_receive_notice_edit", receiveNoticeId, "编辑收料通知单", "receivenotice:edit", 3, now);
        AddButton(menus, "menu_receive_notice_approve", receiveNoticeId, "审核收料通知单", "receivenotice:approve", 4, now);
        AddButton(menus, "menu_receive_notice_delete", receiveNoticeId, "删除收料通知单", "receivenotice:delete", 5, now);
        AddButton(menus, "menu_receive_notice_push", receiveNoticeId, "下推收料通知单", "receivenotice:push", 6, now);
        AddButton(menus, "menu_receive_notice_trace", receiveNoticeId, "下查收料通知单", "receivenotice:trace", 7, now);

        // 采购入库单 (M)
        var purchaseInId = "menu_purchase_in";
        menus.Add(new SysMenu
        {
            Uid = purchaseInId, FInterId = purchaseInId, ParentId = purchaseBizId, MenuName = "采购入库单",
            MenuType = "M", RoutePath = "/purchase/inbounds", Icon = "Box", PermCode = "", SortOrder = 4,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_purchase_in_list", purchaseInId, "查看采购入库单", "purchasein:list", 1, now);
        AddButton(menus, "menu_purchase_in_add", purchaseInId, "新增采购入库单", "purchasein:add", 2, now);
        AddButton(menus, "menu_purchase_in_edit", purchaseInId, "编辑采购入库单", "purchasein:edit", 3, now);
        AddButton(menus, "menu_purchase_in_approve", purchaseInId, "审核采购入库单", "purchasein:approve", 4, now);
        AddButton(menus, "menu_purchase_in_delete", purchaseInId, "删除采购入库单", "purchasein:delete", 5, now);

        // ── 二级分组：采购退料 (D) ──
        var returnGroupId = "menu_return_group";
        menus.Add(new SysMenu
        {
            Uid = returnGroupId, FInterId = returnGroupId, ParentId = purchaseId, MenuName = "采购退料",
            MenuType = "D", RoutePath = "", Icon = "RefreshLeft", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 退料申请单 (M)
        var returnReqId = "menu_return_req";
        menus.Add(new SysMenu
        {
            Uid = returnReqId, FInterId = returnReqId, ParentId = returnGroupId, MenuName = "退料申请单",
            MenuType = "M", RoutePath = "/purchase/return-requests", Icon = "DocumentDelete", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_return_req_list", returnReqId, "查看退料申请单", "returnreq:list", 1, now);
        AddButton(menus, "menu_return_req_add", returnReqId, "新增退料申请单", "returnreq:add", 2, now);
        AddButton(menus, "menu_return_req_edit", returnReqId, "编辑退料申请单", "returnreq:edit", 3, now);
        AddButton(menus, "menu_return_req_approve", returnReqId, "审核退料申请单", "returnreq:approve", 4, now);
        AddButton(menus, "menu_return_req_delete", returnReqId, "删除退料申请单", "returnreq:delete", 5, now);
        AddButton(menus, "menu_return_req_push", returnReqId, "下推退料申请单", "returnreq:push", 6, now);
        AddButton(menus, "menu_return_req_trace", returnReqId, "下查退料申请单", "returnreq:trace", 7, now);

        // 采购退料单 (M)
        var purchaseReturnId = "menu_purchase_return";
        menus.Add(new SysMenu
        {
            Uid = purchaseReturnId, FInterId = purchaseReturnId, ParentId = returnGroupId, MenuName = "采购退料单",
            MenuType = "M", RoutePath = "/purchase/returns", Icon = "Back", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_purchase_return_list", purchaseReturnId, "查看采购退料单", "purchasereturn:list", 1, now);
        AddButton(menus, "menu_purchase_return_add", purchaseReturnId, "新增采购退料单", "purchasereturn:add", 2, now);
        AddButton(menus, "menu_purchase_return_edit", purchaseReturnId, "编辑采购退料单", "purchasereturn:edit", 3, now);
        AddButton(menus, "menu_purchase_return_approve", purchaseReturnId, "审核采购退料单", "purchasereturn:approve", 4, now);
        AddButton(menus, "menu_purchase_return_delete", purchaseReturnId, "删除采购退料单", "purchasereturn:delete", 5, now);

        // ── 二级分组：采购标签打印 (D) ──
        var purchaseLabelId = "menu_purchase_label";
        menus.Add(new SysMenu
        {
            Uid = purchaseLabelId, FInterId = purchaseLabelId, ParentId = purchaseId, MenuName = "采购标签打印",
            MenuType = "D", RoutePath = "", Icon = "Printer", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 收料通知单标签 (M)
        var labelReceiveNoticeId = "menu_label_receive_notice";
        menus.Add(new SysMenu
        {
            Uid = labelReceiveNoticeId, FInterId = labelReceiveNoticeId, ParentId = purchaseLabelId, MenuName = "收料通知单标签",
            MenuType = "M", RoutePath = "/purchase/labels/receive-notice", Icon = "PriceTag", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_label_receive_notice_list", labelReceiveNoticeId, "查看收料通知单标签", "labelreceivenotice:list", 1, now);
        AddButton(menus, "menu_label_receive_notice_print", labelReceiveNoticeId, "打印收料通知单标签", "labelreceivenotice:print", 2, now);
        AddButton(menus, "menu_label_receive_notice_generate", labelReceiveNoticeId, "生成收料通知单条码", "labelreceivenotice:generate", 3, now);
        AddButton(menus, "menu_label_receive_notice_void", labelReceiveNoticeId, "作废收料通知单条码", "labelreceivenotice:void", 4, now);

        // 采购订单标签 (M)
        var labelPurchaseOrderId = "menu_label_purchase_order";
        menus.Add(new SysMenu
        {
            Uid = labelPurchaseOrderId, FInterId = labelPurchaseOrderId, ParentId = purchaseLabelId, MenuName = "采购订单标签",
            MenuType = "M", RoutePath = "/purchase/labels/purchase-order", Icon = "PriceTag", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_label_purchase_order_list", labelPurchaseOrderId, "查看采购订单标签", "labelpurchaseorder:list", 1, now);
        AddButton(menus, "menu_label_purchase_order_print", labelPurchaseOrderId, "打印采购订单标签", "labelpurchaseorder:print", 2, now);
        AddButton(menus, "menu_label_purchase_order_generate", labelPurchaseOrderId, "生成采购订单条码", "labelpurchaseorder:generate", 3, now);
        AddButton(menus, "menu_label_purchase_order_void", labelPurchaseOrderId, "作废采购订单条码", "labelpurchaseorder:void", 4, now);

        // 标签模板设计 (M)
        var labelTemplateId = "menu_label_template";
        menus.Add(new SysMenu
        {
            Uid = labelTemplateId, FInterId = labelTemplateId, ParentId = purchaseLabelId, MenuName = "标签模板设计",
            MenuType = "M", RoutePath = "/purchase/labels/template", Icon = "EditPen", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_label_template_list", labelTemplateId, "查看标签模板", "labeltemplate:list", 1, now);
        AddButton(menus, "menu_label_template_add", labelTemplateId, "新增标签模板", "labeltemplate:add", 2, now);
        AddButton(menus, "menu_label_template_edit", labelTemplateId, "编辑标签模板", "labeltemplate:edit", 3, now);
        AddButton(menus, "menu_label_template_delete", labelTemplateId, "删除标签模板", "labeltemplate:delete", 4, now);

        // ── 二级分组：库存管理 (D) ──
        var stockMgmtId = "menu_stock_mgmt";
        menus.Add(new SysMenu
        {
            Uid = stockMgmtId, FInterId = stockMgmtId, ParentId = purchaseId, MenuName = "库存管理",
            MenuType = "D", RoutePath = "", Icon = "Coin", PermCode = "", SortOrder = 4,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 即时库存 (M) — 只读列表查询页（无详情页）
        var inventoryId = "menu_stock_inventory";
        menus.Add(new SysMenu
        {
            Uid = inventoryId, FInterId = inventoryId, ParentId = stockMgmtId, MenuName = "即时库存",
            MenuType = "M", RoutePath = "/purchase/inventory", Icon = "List", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_stock_inventory_list", inventoryId, "查看即时库存", "inventory:list", 1, now);

        // ═══════════════════════════════════════════════════════════════
        // 生产管理 (D) - 一级菜单，落在采购管理(2)与系统管理(99)之间
        // 占位脚手架：菜单树 + 占位页，后续逐张单据替换为真实页面
        // ═══════════════════════════════════════════════════════════════
        var productionId = "menu_production";
        menus.Add(new SysMenu
        {
            Uid = productionId, FInterId = productionId, ParentId = "", MenuName = "生产管理",
            MenuType = "D", RoutePath = "", Icon = "SetUp", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // ── 二级分组：生产业务 (D) ──
        var prodBizId = "menu_production_biz";
        menus.Add(new SysMenu
        {
            Uid = prodBizId, FInterId = prodBizId, ParentId = productionId, MenuName = "生产业务",
            MenuType = "D", RoutePath = "", Icon = "Tickets", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 生产订单 (M)
        var prodOrderId = "menu_production_order";
        menus.Add(new SysMenu
        {
            Uid = prodOrderId, FInterId = prodOrderId, ParentId = prodBizId, MenuName = "生产订单",
            MenuType = "M", RoutePath = "/production/orders", Icon = "Document", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_order_list", prodOrderId, "查看生产订单", "productionorder:list", 1, now);
        AddButton(menus, "menu_production_order_add", prodOrderId, "新增生产订单", "productionorder:add", 2, now);
        AddButton(menus, "menu_production_order_edit", prodOrderId, "编辑生产订单", "productionorder:edit", 3, now);

        // 生产用料清单 (M)
        var prodMtrlListId = "menu_production_mtrllist";
        menus.Add(new SysMenu
        {
            Uid = prodMtrlListId, FInterId = prodMtrlListId, ParentId = prodBizId, MenuName = "生产用料清单",
            MenuType = "M", RoutePath = "/production/material-lists", Icon = "Memo", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_mtrllist_list", prodMtrlListId, "查看生产用料清单", "prodmateriallist:list", 1, now);
        AddButton(menus, "menu_production_mtrllist_add", prodMtrlListId, "新增生产用料清单", "prodmateriallist:add", 2, now);
        AddButton(menus, "menu_production_mtrllist_edit", prodMtrlListId, "编辑生产用料清单", "prodmateriallist:edit", 3, now);

        // ── 二级分组：生产领料 (D) ──
        var prodIssueGroupId = "menu_production_issue";
        menus.Add(new SysMenu
        {
            Uid = prodIssueGroupId, FInterId = prodIssueGroupId, ParentId = productionId, MenuName = "生产领料",
            MenuType = "D", RoutePath = "", Icon = "Sell", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 生产领料单 (M)
        var prodPickId = "menu_production_pick";
        menus.Add(new SysMenu
        {
            Uid = prodPickId, FInterId = prodPickId, ParentId = prodIssueGroupId, MenuName = "生产领料单",
            MenuType = "M", RoutePath = "/production/picks", Icon = "Sell", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_pick_list", prodPickId, "查看生产领料单", "productionpick:list", 1, now);
        AddButton(menus, "menu_production_pick_add", prodPickId, "新增生产领料单", "productionpick:add", 2, now);
        AddButton(menus, "menu_production_pick_edit", prodPickId, "编辑生产领料单", "productionpick:edit", 3, now);

        // 生产退料单 (M)
        var prodReturnId = "menu_production_return";
        menus.Add(new SysMenu
        {
            Uid = prodReturnId, FInterId = prodReturnId, ParentId = prodIssueGroupId, MenuName = "生产退料单",
            MenuType = "M", RoutePath = "/production/returns", Icon = "Back", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_return_list", prodReturnId, "查看生产退料单", "productionreturn:list", 1, now);
        AddButton(menus, "menu_production_return_add", prodReturnId, "新增生产退料单", "productionreturn:add", 2, now);
        AddButton(menus, "menu_production_return_edit", prodReturnId, "编辑生产退料单", "productionreturn:edit", 3, now);

        // 生产补料单 (M)
        var prodSupplementId = "menu_production_supplement";
        menus.Add(new SysMenu
        {
            Uid = prodSupplementId, FInterId = prodSupplementId, ParentId = prodIssueGroupId, MenuName = "生产补料单",
            MenuType = "M", RoutePath = "/production/supplements", Icon = "CirclePlus", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_supplement_list", prodSupplementId, "查看生产补料单", "productionsupplement:list", 1, now);
        AddButton(menus, "menu_production_supplement_add", prodSupplementId, "新增生产补料单", "productionsupplement:add", 2, now);
        AddButton(menus, "menu_production_supplement_edit", prodSupplementId, "编辑生产补料单", "productionsupplement:edit", 3, now);

        // ── 二级分组：生产入库 (D) ──
        var prodInGroupId = "menu_production_in_group";
        menus.Add(new SysMenu
        {
            Uid = prodInGroupId, FInterId = prodInGroupId, ParentId = productionId, MenuName = "生产入库",
            MenuType = "D", RoutePath = "", Icon = "Box", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 生产入库单 (M)
        var prodInId = "menu_production_in";
        menus.Add(new SysMenu
        {
            Uid = prodInId, FInterId = prodInId, ParentId = prodInGroupId, MenuName = "生产入库单",
            MenuType = "M", RoutePath = "/production/inbounds", Icon = "Box", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_in_list", prodInId, "查看生产入库单", "productionin:list", 1, now);
        AddButton(menus, "menu_production_in_add", prodInId, "新增生产入库单", "productionin:add", 2, now);
        AddButton(menus, "menu_production_in_edit", prodInId, "编辑生产入库单", "productionin:edit", 3, now);

        // 生产退库单 (M)
        var prodStockReturnId = "menu_production_stockreturn";
        menus.Add(new SysMenu
        {
            Uid = prodStockReturnId, FInterId = prodStockReturnId, ParentId = prodInGroupId, MenuName = "生产退库单",
            MenuType = "M", RoutePath = "/production/stock-returns", Icon = "TakeawayBox", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_stockreturn_list", prodStockReturnId, "查看生产退库单", "productionstockreturn:list", 1, now);
        AddButton(menus, "menu_production_stockreturn_add", prodStockReturnId, "新增生产退库单", "productionstockreturn:add", 2, now);
        AddButton(menus, "menu_production_stockreturn_edit", prodStockReturnId, "编辑生产退库单", "productionstockreturn:edit", 3, now);

        // ── 二级分组：简单生产 (D) ──
        var simpleProdGroupId = "menu_production_simple";
        menus.Add(new SysMenu
        {
            Uid = simpleProdGroupId, FInterId = simpleProdGroupId, ParentId = productionId, MenuName = "简单生产",
            MenuType = "D", RoutePath = "", Icon = "Operation", PermCode = "", SortOrder = 4,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });

        // 简单生产领料单 (M)
        var simplePickId = "menu_production_simple_pick";
        menus.Add(new SysMenu
        {
            Uid = simplePickId, FInterId = simplePickId, ParentId = simpleProdGroupId, MenuName = "简单生产领料单",
            MenuType = "M", RoutePath = "/production/simple-picks", Icon = "Sell", PermCode = "", SortOrder = 1,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_simple_pick_list", simplePickId, "查看简单生产领料单", "simpleprodpick:list", 1, now);
        AddButton(menus, "menu_production_simple_pick_add", simplePickId, "新增简单生产领料单", "simpleprodpick:add", 2, now);
        AddButton(menus, "menu_production_simple_pick_edit", simplePickId, "编辑简单生产领料单", "simpleprodpick:edit", 3, now);

        // 简单生产退料单 (M)
        var simpleReturnId = "menu_production_simple_return";
        menus.Add(new SysMenu
        {
            Uid = simpleReturnId, FInterId = simpleReturnId, ParentId = simpleProdGroupId, MenuName = "简单生产退料单",
            MenuType = "M", RoutePath = "/production/simple-returns", Icon = "Back", PermCode = "", SortOrder = 2,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_simple_return_list", simpleReturnId, "查看简单生产退料单", "simpleprodreturn:list", 1, now);
        AddButton(menus, "menu_production_simple_return_add", simpleReturnId, "新增简单生产退料单", "simpleprodreturn:add", 2, now);
        AddButton(menus, "menu_production_simple_return_edit", simpleReturnId, "编辑简单生产退料单", "simpleprodreturn:edit", 3, now);

        // 简单生产入库单 (M)
        var simpleInId = "menu_production_simple_in";
        menus.Add(new SysMenu
        {
            Uid = simpleInId, FInterId = simpleInId, ParentId = simpleProdGroupId, MenuName = "简单生产入库单",
            MenuType = "M", RoutePath = "/production/simple-inbounds", Icon = "Box", PermCode = "", SortOrder = 3,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_simple_in_list", simpleInId, "查看简单生产入库单", "simpleprodin:list", 1, now);
        AddButton(menus, "menu_production_simple_in_add", simpleInId, "新增简单生产入库单", "simpleprodin:add", 2, now);
        AddButton(menus, "menu_production_simple_in_edit", simpleInId, "编辑简单生产入库单", "simpleprodin:edit", 3, now);

        // 简单生产退库单 (M)
        var simpleStockReturnId = "menu_production_simple_stockreturn";
        menus.Add(new SysMenu
        {
            Uid = simpleStockReturnId, FInterId = simpleStockReturnId, ParentId = simpleProdGroupId, MenuName = "简单生产退库单",
            MenuType = "M", RoutePath = "/production/simple-stock-returns", Icon = "TakeawayBox", PermCode = "", SortOrder = 4,
            FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        });
        AddButton(menus, "menu_production_simple_stockreturn_list", simpleStockReturnId, "查看简单生产退库单", "simpleprodstockreturn:list", 1, now);
        AddButton(menus, "menu_production_simple_stockreturn_add", simpleStockReturnId, "新增简单生产退库单", "simpleprodstockreturn:add", 2, now);
        AddButton(menus, "menu_production_simple_stockreturn_edit", simpleStockReturnId, "编辑简单生产退库单", "simpleprodstockreturn:edit", 3, now);

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

        // 重指向：基础资料下"仓位管理"(StockPlace) → "仓位"(仓位集 T_BAS_FLEXVALUES 维护页)
        // 菜单按 Uid 仅补缺、不更新存量，故对已存在的该菜单行显式重指向（幂等）
        await _db.Updateable<SysMenu>()
            .SetColumns(m => m.MenuName == "仓位管理")
            .SetColumns(m => m.RoutePath == "/master/flexvalues")
            .Where(m => m.Uid == stockPlaceMenuId)
            .ExecuteCommandAsync();
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
                PrPassword = PasswordHelper.HashPassword("123456"),
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

    /// <summary>
    /// 采购订单单据类型（T_BAS_BILLTYPE，FBillFormid = PUR_PurchaseOrder）。
    /// Uid 直接采用 K3 的 FInterId，幂等：按 Uid 判重。
    /// </summary>
    private async Task SeedBillTypesAsync()
    {
        var now = DateTime.Now;
        var bills = new (string Uid, string Number, string Name)[]
        {
            ("83d822ca3e374b4ab01e5dd46a0062bd", "CGDD01_SYS", "标准采购订单"),
            ("6d01d059713d42a28bb976c90a121142", "CGDD02_SYS", "标准委外订单"),
            ("b8df755fd92b4c2baedef2439c29f793", "CGDD03_SYS", "直运采购订单"),
            ("b0677860cd16433895be5f520086b69f", "CGDD04_SYS", "资产采购订单"),
            ("b1985f24f35841fdb418329af6ed7bd0", "CGDD05_SYS", "费用采购订单"),
            ("ba3ad5fc48d44271a048da26b615b589", "CGDD06-SYS", "补料采购订单"),
            ("0023240234df807511e308990e04cf6a", "CGDD07_SYS", "VMI采购订单"),
            ("5abd9deba59210",                   "CGDD08_SYS", "现购订单"),
        };
        foreach (var (uid, number, name) in bills)
        {
            var exists = await _db.Queryable<TBasBilltype>().Where(b => b.Uid == uid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBasBilltype
            {
                Uid = uid,
                FInterId = uid,
                Fnumber = number,
                Fname = name,
                Fbillformid = "PUR_PurchaseOrder",
                Isdefault = number == "CGDD01_SYS",
                Fcheckdate = DateTime.MinValue,
                // 1900 哨兵：满足开发库 SQLite 的 NOT NULL，且生产 SQLServer DATETIME(下限1753)安全；前端按 <=1900 视为空
                Fdisabledate = new DateTime(1900, 1, 1),
                FStatus = 40,
                FCompanyId = "DEFAULT",
                CYmd = now,
                CUser = "system",
                MYmd = now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }

        // 收料通知单单据类型（FBillFormid = PUR_ReceiveBill）。开发库原本无此业务表单的单据类型，幂等种入。
        var receiveBills = new (string Uid, string Number, string Name)[]
        {
            ("slr_billtype_std_0001", "SLTZ01_SYS", "标准收料单"),
            ("slr_billtype_ww_0002",  "SLTZ02_SYS", "委外收料单"),
            ("slr_billtype_zy_0003",  "SLTZ03_SYS", "直运收料单"),
        };
        foreach (var (uid, number, name) in receiveBills)
        {
            var exists = await _db.Queryable<TBasBilltype>().Where(b => b.Uid == uid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBasBilltype
            {
                Uid = uid,
                FInterId = uid,
                Fnumber = number,
                Fname = name,
                Fbillformid = "PUR_ReceiveBill",
                Isdefault = number == "SLTZ01_SYS",
                Fcheckdate = DateTime.MinValue,
                Fdisabledate = new DateTime(1900, 1, 1), // 1900哨兵：满足开发库NOT NULL，且生产DATETIME(下限1753)安全
                FStatus = 40,
                FCompanyId = "DEFAULT",
                CYmd = now,
                CUser = "system",
                MYmd = now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }

        // 采购入库单单据类型（FBillFormid = STK_InStock）。幂等种入。
        var inStockBills = new (string Uid, string Number, string Name)[]
        {
            ("stk_billtype_std_0001", "RKD01_SYS", "标准采购入库"),
            ("stk_billtype_ww_0002",  "RKD02_SYS", "委外采购入库"),
            ("stk_billtype_zy_0003",  "RKD03_SYS", "直运采购入库"),
        };
        foreach (var (uid, number, name) in inStockBills)
        {
            var exists = await _db.Queryable<TBasBilltype>().Where(b => b.Uid == uid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBasBilltype
            {
                Uid = uid,
                FInterId = uid,
                Fnumber = number,
                Fname = name,
                Fbillformid = "STK_InStock",
                Isdefault = number == "RKD01_SYS",
                Fcheckdate = DateTime.MinValue,
                Fdisabledate = new DateTime(1900, 1, 1), // 1900哨兵：满足开发库NOT NULL，且生产DATETIME(下限1753)安全
                FStatus = 40,
                FCompanyId = "DEFAULT",
                CYmd = now,
                CUser = "system",
                MYmd = now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }

        // 退料申请单单据类型（FBillFormid = PUR_MRAPP）。幂等种入。
        var mrAppBills = new (string Uid, string Number, string Name)[]
        {
            ("mrapp_billtype_std_0001", "TLSQ01_SYS", "标准退料申请"),
            ("mrapp_billtype_ww_0002",  "TLSQ02_SYS", "委外退料申请"),
            ("mrapp_billtype_zy_0003",  "TLSQ03_SYS", "直运退料申请"),
        };
        foreach (var (uid, number, name) in mrAppBills)
        {
            var exists = await _db.Queryable<TBasBilltype>().Where(b => b.Uid == uid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBasBilltype
            {
                Uid = uid,
                FInterId = uid,
                Fnumber = number,
                Fname = name,
                Fbillformid = "PUR_MRAPP",
                Isdefault = number == "TLSQ01_SYS",
                Fcheckdate = DateTime.MinValue,
                Fdisabledate = new DateTime(1900, 1, 1), // 1900哨兵：满足开发库NOT NULL，且生产DATETIME(下限1753)安全
                FStatus = 40,
                FCompanyId = "DEFAULT",
                CYmd = now,
                CUser = "system",
                MYmd = now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }

        // 采购退料单 / 外购退料单据类型（FBillFormid = PUR_MRB）。幂等种入。
        var mrbBills = new (string Uid, string Number, string Name)[]
        {
            ("mrb_billtype_std_0001", "TLD01_SYS", "标准退料单"),
            ("mrb_billtype_ww_0002",  "TLD02_SYS", "委外退料单"),
            ("mrb_billtype_zy_0003",  "TLD03_SYS", "直运退料单"),
        };
        foreach (var (uid, number, name) in mrbBills)
        {
            var exists = await _db.Queryable<TBasBilltype>().Where(b => b.Uid == uid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBasBilltype
            {
                Uid = uid,
                FInterId = uid,
                Fnumber = number,
                Fname = name,
                Fbillformid = "PUR_MRB",
                Isdefault = number == "TLD01_SYS",
                Fcheckdate = DateTime.MinValue,
                Fdisabledate = new DateTime(1900, 1, 1), // 1900哨兵：满足开发库NOT NULL，且生产DATETIME(下限1753)安全
                FStatus = 40,
                FCompanyId = "DEFAULT",
                CYmd = now,
                CUser = "system",
                MYmd = now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }

        // 生产订单 / 生产任务单单据类型（FBillFormid = PRD_MO）。幂等种入。
        var prdMoBills = new (string Uid, string Number, string Name)[]
        {
            ("prdmo_billtype_std_0001",    "SCDD01_SYS", "标准生产订单"),
            ("prdmo_billtype_rework_0002", "SCDD02_SYS", "返工生产订单"),
            ("prdmo_billtype_ww_0003",     "SCDD03_SYS", "委外生产订单"),
        };
        foreach (var (uid, number, name) in prdMoBills)
        {
            var exists = await _db.Queryable<TBasBilltype>().Where(b => b.Uid == uid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBasBilltype
            {
                Uid = uid,
                FInterId = uid,
                Fnumber = number,
                Fname = name,
                Fbillformid = "PRD_MO",
                Isdefault = number == "SCDD01_SYS",
                Fcheckdate = DateTime.MinValue,
                Fdisabledate = new DateTime(1900, 1, 1), // 1900哨兵：满足开发库NOT NULL，且生产DATETIME(下限1753)安全
                FStatus = 40,
                FCompanyId = "DEFAULT",
                CYmd = now,
                CUser = "system",
                MYmd = now,
                MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 源单配置种子：采购入库单（目标单据 STK_InStock）的可选源单（T_BOS_SELBILL），
    /// 及源单类型名称解析所需的表单模板（SYS_BILLTEMPLATE）。开发库两表原本为空，前端"源单类型"下拉数据驱动取此处。
    /// 幂等：T_BOS_SELBILL 按 Uid、SYS_BILLTEMPLATE 按 FNUMBER 判重。
    /// </summary>
    private async Task SeedSourceBillsAsync()
    {
        var now = DateTime.Now;

        // 表单模板（仅用于源单类型/单据名称解析）
        var templates = new (string Number, string Name)[]
        {
            ("PUR_PurchaseOrder", "采购订单"),
            ("PUR_ReceiveBill",   "收料通知单"),
            ("STK_InStock",       "采购入库单"),
            ("PUR_MRAPP",         "退料申请单"),
            ("PUR_MRB",           "采购退料单"),
        };
        foreach (var (number, name) in templates)
        {
            var exists = await _db.Queryable<SysBillTemplate>().Where(t => t.Fnumber == number).AnyAsync();
            if (exists) continue;
            var uid = Guid.NewGuid().ToString("N");
            await _db.Insertable(new SysBillTemplate
            {
                Uid = uid, FInterId = uid, Fnumber = number, Fname = name,
                FStatus = 40, FCompanyId = "DEFAULT",
                CYmd = now, CUser = "system", MYmd = now, MUser = "system"
            }).ExecuteCommandAsync();
        }

        // 可选源单（数据驱动；含"无源单"空类型）。目标=采购入库单(STK_InStock) / 采购退料单(PUR_MRB)。
        var sels = new (string Uid, string DestType, string SourceType, string Name, bool IsDefault)[]
        {
            // 采购入库单
            ("selbill_instock_none",    "STK_InStock", "",                  "无源单",     false),
            ("selbill_instock_po",      "STK_InStock", "PUR_PurchaseOrder", "采购订单",   true),
            ("selbill_instock_receive", "STK_InStock", "PUR_ReceiveBill",   "收料通知单", false),
            // 采购退料单 / 外购退料
            ("selbill_mrb_none",        "PUR_MRB",     "",                  "无源单",     false),
            ("selbill_mrb_po",          "PUR_MRB",     "PUR_PurchaseOrder", "采购订单",   true),
            ("selbill_mrb_receive",     "PUR_MRB",     "PUR_ReceiveBill",   "收料通知单", false),
            // 退料申请单 -> 采购退料单（支持退料申请单维护页「下推」生成采购退料单）
            ("selbill_mrb_mrapp",       "PUR_MRB",     "PUR_MRAPP",         "退料申请单", false),
        };
        foreach (var (uid, destType, srcType, name, isDef) in sels)
        {
            var exists = await _db.Queryable<TBosSelbill>().Where(s => s.Uid == uid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBosSelbill
            {
                Uid = uid, FInterId = uid, Fname = name, Fnumber = uid,
                Fsourcetrantype = srcType, Fdesttrantype = destType,
                Fisuse = true, Fdefault = isDef, Fkind = string.Empty,
                // 开发库该两列 NOT NULL；用 1900 哨兵：满足 SQLite NOT NULL，且生产 DATETIME(下限1753)安全，前端按<=1900过滤
                Fcheckdate = new DateTime(1900, 1, 1), Fdisabledate = new DateTime(1900, 1, 1),
                FStatus = 40, FCompanyId = "DEFAULT",
                CYmd = now, CUser = "system", MYmd = now, MUser = "system"
            }).ExecuteCommandAsync();
        }

        // 示例「单据类型映射」明细（T_BOS_SELBILLENTRY）：为默认入库流程(selbill_instock_po)配置
        // 源单单据类型(采购订单) -> 目标单据类型(采购入库单) 的映射，使配置页开箱即演示主从效果。
        // 源/目标存 T_BAS_BILLTYPE.Uid（与前端 billtype lookup 选中值一致）。幂等：按 Uid 判重。
        var selEntries = new (string Uid, string Header, string SourceBtUid, string DestBtUid, bool IsDefault, int Idx)[]
        {
            // CGDD01_SYS 标准采购订单 -> RKD01_SYS 标准采购入库（默认）
            ("selentry_instock_po_1", "selbill_instock_po", "83d822ca3e374b4ab01e5dd46a0062bd", "stk_billtype_std_0001", true, 1),
            // CGDD02_SYS 标准委外订单 -> RKD02_SYS 委外采购入库
            ("selentry_instock_po_2", "selbill_instock_po", "6d01d059713d42a28bb976c90a121142", "stk_billtype_ww_0002", false, 2),
        };
        foreach (var (eUid, headerUid, srcBt, destBt, isDef, idx) in selEntries)
        {
            var exists = await _db.Queryable<TBosSelbillentry>().Where(e => e.Uid == eUid).AnyAsync();
            if (exists) continue;
            await _db.Insertable(new TBosSelbillentry
            {
                Uid = eUid, FInterId = headerUid, Fdetailid = eUid, Fentryid = idx,
                Fsourceid = srcBt, Fdestid = destBt, Fdefault = isDef,
                FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 编码规则业务表单目录种子（SYS_BILLCODEFORM，数据驱动"哪些单据可配编码规则"）：
    /// 登记采购订单 / 收料通知单两张（含表头实体类名，供基类反射取号匹配）。
    /// 幂等：按表单键存在（含软删）即跳过，尊重用户在界面的登记/注销。
    /// </summary>
    private async Task SeedBillCodeFormsAsync()
    {
        var now = DateTime.Now;
        var forms = new (string FormKey, string FormName, string EntityName)[]
        {
            ("PUR_PurchaseOrder", "采购订单",   nameof(TPurPoOrder)),
            ("PUR_ReceiveBill",   "收料通知单", nameof(TPurReceive)),
            ("STK_InStock",       "采购入库单", nameof(TStkInstock)),
            ("PUR_MRAPP",         "退料申请单", nameof(TPurMrApp)),
            ("PUR_MRB",           "采购退料单", nameof(TPurMrb)),
            ("PRD_MO",            "生产订单",   nameof(TPrdMo)),
        };
        foreach (var (formKey, formName, entityName) in forms)
        {
            var exists = await _db.Queryable<SysBillCodeForm>().Where(f => f.Fformkey == formKey).AnyAsync();
            if (exists) continue;
            var uid = Guid.NewGuid().ToString("N");
            await _db.Insertable(new SysBillCodeForm
            {
                Uid = uid, FInterId = uid, Fformkey = formKey, Fformname = formName, Fentityname = entityName,
                FStatus = 40, FCompanyId = "DEFAULT",
                CYmd = now, CUser = "system", MYmd = now, MUser = "system"
            }).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 默认编码规则（系统管理→编码规则可调整）：
    /// 单据编号（SYS_LISTCODE/ENTRY）——采购订单 CGDD+yyyyMMdd+4位日流水、收料通知单 SLTZ+yyyyMMdd+4位日流水
    ///（替代原"前缀+yyyyMMddHHmmss"时间戳硬编码，修复同秒并发重号隐患）；
    /// 条码编号（SYS_BARCODE/ENTRY）——采购订单 yyyyMMdd+6位日流水（与原硬编码格式一致）。
    /// 日期段勾选"编码依据"= 按日重置流水。幂等：规则头已存在（按表单键）则整套跳过，不覆盖用户修改。
    /// 另：迁移旧硬编码时期 SYS_BARCODENO 的 FCLASSTYPEID='1' 计数行为表单键，保证切换当日条码流水连续不重号。
    /// </summary>
    private async Task SeedBillCodeRulesAsync()
    {
        var now = DateTime.Now;
        const string poFormKey = "PUR_PurchaseOrder";
        const string rnFormKey = "PUR_ReceiveBill";

        // —— 单据编号规则 ——
        const string inFormKey = "STK_InStock";
        const string mrFormKey = "PUR_MRAPP";
        const string mrbFormKey = "PUR_MRB";
        var listRules = new (string Uid, string FormKey, string Name, string Prefix)[]
        {
            ("listcode_pur_order",   poFormKey, "采购订单编号规则",   "CGDD"),
            ("listcode_pur_receive", rnFormKey, "收料通知单编号规则", "SLTZ"),
            ("listcode_stk_instock", inFormKey, "采购入库单编号规则", "RKD"),
            ("listcode_pur_mrapp",   mrFormKey, "退料申请单编号规则", "TLSQ"),
            ("listcode_pur_mrb",     mrbFormKey, "采购退料单编号规则", "TLD"),
            ("listcode_prd_mo",      "PRD_MO",  "生产订单编号规则",   "SCDD"),
        };
        foreach (var (uid, formKey, name, prefix) in listRules)
        {
            var exists = await _db.Queryable<SysListCode>().Where(r => r.Uid == uid || r.Fclasstypeid == formKey).AnyAsync();
            if (exists) continue;
            // 头+分段同事务：避免中途失败留下"有头无段"的半配置规则（守卫只查头，残留后不自愈）
            try
            {
                _db.AsTenant().BeginTran();
                await _db.Insertable(new SysListCode
                {
                    Uid = uid, FInterId = uid, Fclasstypeid = formKey, Fname = name,
                    Ismodify = true, Fhex = 10,
                    Fcheckdate = DateTime.MinValue, // 开发库该列 NOT NULL（生产为 DATE NULL），按惯例赋 MinValue 哨兵
                    FStatus = 40, FCompanyId = "DEFAULT",
                    CYmd = now, CUser = "system", MYmd = now, MUser = "system"
                }).ExecuteCommandAsync();
                await _db.Insertable(new List<SysListCodeEntry>
                {
                    NewListCodeEntry($"{uid}_e1", uid, formKey, 1, "1", now, value: prefix, note: "固定前缀"),
                    NewListCodeEntry($"{uid}_e2", uid, formKey, 2, "3", now, fieldId: "FDATE", fieldName: "单据日期", format: "yyyyMMdd", isSerial: true, note: "日期段（依据=按日重置）"),
                    NewListCodeEntry($"{uid}_e3", uid, formKey, 3, "4", now, length: 4, min: 1, step: 1, note: "日流水"),
                }).ExecuteCommandAsync();
                _db.AsTenant().CommitTran();
            }
            catch
            {
                _db.AsTenant().RollbackTran();
                throw;
            }
        }

        // —— 条码编号规则（采购订单标签 / 收料通知单标签，格式同：yyyyMMdd + 6位日流水）——
        var barcodeRules = new (string Uid, string FormKey, string Name)[]
        {
            ("barcoderule_pur_order",   poFormKey, "采购订单条码规则"),
            ("barcoderule_pur_receive", rnFormKey, "收料通知单条码规则"),
        };
        foreach (var (bcUid, formKey, bcName) in barcodeRules)
        {
            var bcExists = await _db.Queryable<SysBarcode>().Where(r => r.Uid == bcUid || r.Fclasstypeid == formKey).AnyAsync();
            if (bcExists) continue;
            try
            {
                _db.AsTenant().BeginTran();
                await _db.Insertable(new SysBarcode
                {
                    Uid = bcUid, FInterId = bcUid, Fclasstypeid = formKey, Fprgkey = formKey,
                    Fname = bcName, Ismodify = false, Fhex = 10,
                    Fcheckdate = DateTime.MinValue, // 开发库该列 NOT NULL（生产为 DATE NULL），按惯例赋 MinValue 哨兵
                    FStatus = 40, FCompanyId = "DEFAULT",
                    CYmd = now, CUser = "system", MYmd = now, MUser = "system"
                }).ExecuteCommandAsync();
                await _db.Insertable(new List<SysBarcodeEntry>
                {
                    NewBarcodeEntry($"{bcUid}_e1", bcUid, formKey, 1, "3", now, fieldId: "FDATE", fieldName: "打印日期", format: "yyyyMMdd", isSerial: true, note: "日期段（依据=按日重置）"),
                    NewBarcodeEntry($"{bcUid}_e2", bcUid, formKey, 2, "4", now, length: 6, min: 1, step: 1, note: "日流水"),
                }).ExecuteCommandAsync();
                _db.AsTenant().CommitTran();
            }
            catch
            {
                _db.AsTenant().RollbackTran();
                throw;
            }
        }

        // 旧硬编码时期条码计数行迁移为表单键，幂等（迁移后不再命中）。
        // 键 '1' 是旧 NextSeqAsync 最终版写入的 FCLASSTYPEID；'PO' 是更早中间版残留的死数据，一并收编
        await _db.Updateable<SysBarcodeNo>()
            .SetColumns(s => s.Fclasstypeid == poFormKey)
            .Where(s => s.Fclasstypeid == "1" || s.Fclasstypeid == "PO")
            .ExecuteCommandAsync();
    }

    private static SysListCodeEntry NewListCodeEntry(string uid, string headerUid, string formKey, int entryId, string type, DateTime now,
        string value = "", string fieldId = "", string fieldName = "", string format = "",
        int length = 0, int min = 0, int max = 0, int step = 0, bool isSerial = false, string note = "")
        => new()
        {
            Uid = uid, FInterId = headerUid, FDETAILID = uid, Fclasstypeid = formKey, FENTRYID = entryId,
            Ftype = type, FVALUE = value, FIELDID = fieldId, FIELDNAME = fieldName, FORMATSTRING = format,
            FLENGHT = length, FMIN = min, FMAX = max, FSTEP = step, FCHAR = string.Empty, FCHARALIGNMENT = string.Empty,
            ISSERIAL = isSerial, ISMEMBER = true, Fnote = note, FCODECONTRAST = string.Empty,
            FStatus = 40, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        };

    private static SysBarcodeEntry NewBarcodeEntry(string uid, string headerUid, string formKey, int entryId, string type, DateTime now,
        string value = "", string fieldId = "", string fieldName = "", string format = "",
        int length = 0, int min = 0, int max = 0, int step = 0, bool isSerial = false, string note = "")
        => new()
        {
            Uid = uid, FInterId = headerUid, FDETAILID = uid, Fclasstypeid = formKey, FENTRYID = entryId,
            Ftype = type, FVALUE = value, FIELDID = fieldId, FIELDNAME = fieldName, FORMATSTRING = format,
            FLENGHT = length, FMIN = min, FMAX = max, FSTEP = step, FCHAR = string.Empty, FCHARALIGNMENT = string.Empty,
            ISSERIAL = isSerial, ISMEMBER = true, Fnote = note, FCODECONTRAST = string.Empty,
            FStatus = 40, FCompanyId = "DEFAULT", CYmd = now, CUser = "system", MYmd = now, MUser = "system"
        };

    /// <summary>
    /// 一次性把开发库中现有未审核物料置为已审核(FStatus=40)，使采购订单等单据的"已审核物料"下拉可用。
    /// 自禁用守卫：库中一旦已存在已审核物料即跳过，避免误审用户后续新建的草稿物料。
    /// </summary>
    private async Task SeedApproveExistingMaterialsAsync()
    {
        var hasApproved = await _db.Queryable<TBdMaterial>().Where(m => !m.FDeleted && m.FStatus == 40).AnyAsync();
        if (hasApproved) return;

        var now = DateTime.Now;
        await _db.Updateable<TBdMaterial>()
            .SetColumns(m => m.FStatus == 40)
            .SetColumns(m => m.MYmd == now)
            .Where(m => !m.FDeleted && m.FStatus != 40)
            .ExecuteCommandAsync();
    }
}
