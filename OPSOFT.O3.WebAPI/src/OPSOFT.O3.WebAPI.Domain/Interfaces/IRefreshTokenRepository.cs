using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<SysRefreshToken?> GetByTokenHashAsync(string tokenHash);
    Task<bool> InsertAsync(SysRefreshToken entity);
    Task<bool> RevokeByUserUidAsync(string userUid, string reason);
    Task<bool> RevokeByTokenHashAsync(string tokenHash, string reason);
    Task<bool> UpdateLastUsedAtAsync(string uid);
    Task<int> DeleteExpiredAsync(DateTime before);
}
