using OPSOFT.O3.WebAPI.Domain.Entities;

namespace OPSOFT.O3.WebAPI.Domain.Interfaces;

public interface ILoginUserRepository
{
    Task<SysLoginUser?> GetByUserIdAsync(string userId);
    Task<bool> UpdateLastLoginTimeAsync(string uid);
    Task<bool> UpdatePwdErrTimesAsync(string uid, int errTimes, int lockStatus, DateTime? lockTime);
}
