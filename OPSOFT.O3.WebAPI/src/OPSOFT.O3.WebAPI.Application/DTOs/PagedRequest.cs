using System.Collections.Generic;

namespace OPSOFT.O3.WebAPI.Application.DTOs;

/// <summary>
/// 分页请求
/// </summary>
public class PagedRequest
{
    private int _pageIndex = 1;
    private int _pageSize = 20;

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 20 : (value > 100 ? 100 : value);
    }

    public string? SortField { get; set; }
    public bool IsAsc { get; set; } = true;
    public string? Keyword { get; set; }
    public string? GroupId { get; set; }

    /// <summary>
    /// 通用外键过滤（如辅助资料明细按类别过滤）
    /// </summary>
    public string? Fid { get; set; }

    /// <summary>
    /// 动态筛选条件集合
    /// </summary>
    public List<DynamicFilterInfo>? DynamicFilters { get; set; }
}

/// <summary>
/// 动态筛选信息
/// </summary>
public class DynamicFilterInfo
{
    /// <summary>
    /// 字段名 (建议与实体属性名一致，首字母大写等)
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 操作符：例如 eq(等于), like(包含), gt(大于), lt(小于), ge(大于等于), le(小于等于)
    /// </summary>
    public string Operator { get; set; } = "eq";

    /// <summary>
    /// 筛选值
    /// </summary>
    public string? Value { get; set; }
}
