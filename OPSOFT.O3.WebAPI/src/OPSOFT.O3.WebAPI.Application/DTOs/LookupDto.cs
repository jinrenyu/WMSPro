namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 通用下拉选择 DTO
/// </summary>
public class LookupDto
{
    public string Uid { get; set; } = string.Empty;
    public string FNumber { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
}

/// <summary>
/// 下拉查询请求
/// </summary>
public class LookupRequest
{
    /// <summary>
    /// 模糊搜索关键字（按编码/名称）
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 父级过滤（如仓位按仓库过滤、部门按父部门过滤）
    /// </summary>
    public string? ParentId { get; set; }

    private int _maxCount = 50;

    /// <summary>
    /// 最大返回条数，默认50，上限200
    /// </summary>
    public int MaxCount
    {
        get => _maxCount;
        set => _maxCount = value < 1 ? 50 : (value > 200 ? 200 : value);
    }
}
