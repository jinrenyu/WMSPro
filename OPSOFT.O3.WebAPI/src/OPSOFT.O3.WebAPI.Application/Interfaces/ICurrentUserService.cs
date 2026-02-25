namespace OPSOFT.O3.WebAPI.Application.Interfaces;

/// <summary>
/// 当前用户上下文服务
/// </summary>
public interface ICurrentUserService
{
    string? Uid { get; }
    string? UserId { get; }
    string? UserName { get; }
    string? CompanyId { get; }
    string? PaType { get; }
    string? PaId { get; }
    bool IsAuthenticated { get; }
}
