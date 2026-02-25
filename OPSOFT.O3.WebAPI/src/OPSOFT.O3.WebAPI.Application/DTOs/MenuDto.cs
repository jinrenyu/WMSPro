using System.ComponentModel.DataAnnotations;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 菜单树节点 DTO
/// </summary>
public class MenuTreeDto
{
    public string Uid { get; set; } = string.Empty;
    public string ParentId { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string MenuType { get; set; } = string.Empty;
    public string RoutePath { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string PermCode { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int FStatus { get; set; }
    public DateTime? CYmd { get; set; }
    public List<MenuTreeDto> Children { get; set; } = new();
}

/// <summary>
/// 创建菜单请求
/// </summary>
public class CreateMenuRequest
{
    public string ParentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "菜单名称不能为空")]
    public string MenuName { get; set; } = string.Empty;

    [Required(ErrorMessage = "菜单类型不能为空")]
    public string MenuType { get; set; } = string.Empty;

    public string RoutePath { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string PermCode { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int FStatus { get; set; }
}

/// <summary>
/// 更新菜单请求
/// </summary>
public class UpdateMenuRequest
{
    public string ParentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "菜单名称不能为空")]
    public string MenuName { get; set; } = string.Empty;

    [Required(ErrorMessage = "菜单类型不能为空")]
    public string MenuType { get; set; } = string.Empty;

    public string RoutePath { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string PermCode { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int FStatus { get; set; }
}
