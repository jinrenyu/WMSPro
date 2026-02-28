namespace OPSOFT.O3.WebAPI.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(string uid, string userId, string userName, string companyId, List<string>? roles = null, string? paType = null, string? paId = null, int? expirationMinutes = null);
}
