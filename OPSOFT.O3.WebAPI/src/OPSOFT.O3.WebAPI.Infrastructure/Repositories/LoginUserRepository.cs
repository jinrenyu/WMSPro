using OPSOFT.O3.WebAPI.Domain.Entities;
using OPSOFT.O3.WebAPI.Domain.Interfaces;
using SqlSugar;

namespace OPSOFT.O3.WebAPI.Infrastructure.Repositories;

public class LoginUserRepository : ILoginUserRepository
{
    private readonly ISqlSugarClient _db;

    public LoginUserRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task<SysLoginUser?> GetByUserIdAsync(string userId)
    {
        return await _db.Queryable<SysLoginUser>()
            .Where(u => u.UserId == userId)
            .FirstAsync();
    }

    public async Task<bool> UpdateLastLoginTimeAsync(string uid)
    {
        return await _db.Updateable<SysLoginUser>()
            .SetColumns(u => u.LastLoginTime == DateTime.Now)
            .Where(u => u.Uid == uid)
            .ExecuteCommandAsync() > 0;
    }

    public async Task<bool> UpdatePwdErrTimesAsync(string uid, int errTimes, int lockStatus, DateTime? lockTime)
    {
        var actualLockTime = lockTime ?? DateTime.Now;
        return await _db.Updateable<SysLoginUser>()
            .SetColumns(u => u.PwdErrTimes == errTimes)
            .SetColumns(u => u.LockStatus == lockStatus)
            .SetColumns(u => u.LastLockTime == actualLockTime)
            .Where(u => u.Uid == uid)
            .ExecuteCommandAsync() > 0;
    }
}
