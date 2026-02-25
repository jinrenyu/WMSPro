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
}
