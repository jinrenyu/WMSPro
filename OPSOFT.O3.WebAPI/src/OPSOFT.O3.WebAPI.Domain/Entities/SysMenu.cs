using SqlSugar;

namespace OPSOFT.O3.WebAPI.Domain.Entities;

/// <summary>
/// 系统菜单 - 支持目录(D)、菜单(M)、按钮(B)三种类型
/// </summary>
[SugarTable("SYS_MENU")]
public class SysMenu : BaseEntity
{
    /// <summary>
    /// 父级ID（空字符串表示顶级）
    /// </summary>
    [SugarColumn(ColumnName = "PARENT_ID")]
    public string ParentId { get; set; } = string.Empty;

    /// <summary>
    /// 菜单/按钮名称
    /// </summary>
    [SugarColumn(ColumnName = "MENU_NAME")]
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 类型：D=目录, M=菜单, B=按钮
    /// </summary>
    [SugarColumn(ColumnName = "MENU_TYPE")]
    public string MenuType { get; set; } = string.Empty;

    /// <summary>
    /// 路由路径（菜单类型用）
    /// </summary>
    [SugarColumn(ColumnName = "ROUTE_PATH")]
    public string RoutePath { get; set; } = string.Empty;

    /// <summary>
    /// 图标名称（目录/菜单用）
    /// </summary>
    [SugarColumn(ColumnName = "ICON")]
    public string Icon { get; set; } = string.Empty;

    /// <summary>
    /// 权限代码（按钮类型用，如 user:add）
    /// </summary>
    [SugarColumn(ColumnName = "PERM_CODE")]
    public string PermCode { get; set; } = string.Empty;

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "SORT_ORDER")]
    public int SortOrder { get; set; }
}
