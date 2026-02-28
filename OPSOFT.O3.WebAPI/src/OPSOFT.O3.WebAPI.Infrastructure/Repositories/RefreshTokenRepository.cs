using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ISqlSugarClient _db;

    public RefreshTokenRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task<SysRefreshToken?> GetByTokenHashAsync(string tokenHash)
    {
        return await _db.Queryable<SysRefreshToken>()
            .Where(t => t.TokenHash == tokenHash && !t.IsRevoked)
            .FirstAsync();
    }

    public async Task<bool> InsertAsync(SysRefreshToken entity)
    {
        return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> RevokeByUserUidAsync(string userUid, string reason)
    {
        return await _db.Updateable<SysRefreshToken>()
            .SetColumns(t => t.IsRevoked == true)
            .SetColumns(t => t.RevokedAt == DateTime.Now)
            .SetColumns(t => t.RevokedReason == reason)
            .Where(t => t.UserUid == userUid && !t.IsRevoked)
            .ExecuteCommandAsync() >= 0;
    }

    public async Task<bool> RevokeByTokenHashAsync(string tokenHash, string reason)
    {
        return await _db.Updateable<SysRefreshToken>()
            .SetColumns(t => t.IsRevoked == true)
            .SetColumns(t => t.RevokedAt == DateTime.Now)
            .SetColumns(t => t.RevokedReason == reason)
            .Where(t => t.TokenHash == tokenHash && !t.IsRevoked)
            .ExecuteCommandAsync() >= 0;
    }

    public async Task<bool> UpdateLastUsedAtAsync(string uid)
    {
        return await _db.Updateable<SysRefreshToken>()
            .SetColumns(t => t.LastUsedAt == DateTime.Now)
            .Where(t => t.Uid == uid)
            .ExecuteCommandAsync() > 0;
    }

    public async Task<int> DeleteExpiredAsync(DateTime before)
    {
        return await _db.Deleteable<SysRefreshToken>()
            .Where(t => t.ExpiresAt < before || (t.IsRevoked && t.RevokedAt < before))
            .ExecuteCommandAsync();
    }
}
