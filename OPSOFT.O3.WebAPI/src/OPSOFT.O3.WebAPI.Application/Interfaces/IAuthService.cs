using OPSOFT.O3.WebAPI.Application.DTOs;

namespace OPSOFT.O3.WebAPI.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, string? ipAddress = null, string? userAgent = null);
    Task<SmsCodeSendResult> SendSmsCodeAsync(string mfaTicket, string? ipAddress = null);
    Task<LoginResponse> VerifyMfaAsync(string mfaTicket, string code, string? ipAddress = null, string? userAgent = null);
    Task<RefreshTokenResponse?> RefreshTokenAsync(string refreshToken, string? ipAddress = null, string? userAgent = null);
    Task LogoutAsync(string userUid, string? refreshToken = null);
    Task<List<string>> GetCurrentPermissionsAsync(string userId);
}
