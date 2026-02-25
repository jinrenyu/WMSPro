using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

public interface ILoginInfoService
{
    Task RecordLoginAsync(string userId, string? ipAddress, string? userAgent, bool success, string? message = null);
    Task<PagedResult<LoginInfoDto>> GetLoginHistoryAsync(PagedRequest request, string? userId = null);
}

public class LoginInfoDto
{
    public string Uid { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Computer { get; set; } = string.Empty;
    public DateTime? LoginTime { get; set; }
    public DateTime? LastTime { get; set; }
}
