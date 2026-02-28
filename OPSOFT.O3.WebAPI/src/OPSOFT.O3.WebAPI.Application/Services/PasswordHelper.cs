namespace OPSOFT.O3.WebAPI.Application.Services;

/// <summary>
/// 密码哈希工具类，使用 BCrypt 算法
/// </summary>
public static class PasswordHelper
{
    /// <summary>
    /// 对明文密码进行 BCrypt 哈希
    /// </summary>
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// 验证密码是否匹配。
    /// 支持 BCrypt 哈希和明文密码（向后兼容）。
    /// </summary>
    /// <returns>
    /// (isValid, needsUpgrade) — isValid 表示密码正确，needsUpgrade 表示存储的是明文密码需要升级为哈希
    /// </returns>
    public static (bool IsValid, bool NeedsUpgrade) VerifyPassword(string inputPassword, string storedPassword)
    {
        // BCrypt 哈希以 "$2" 开头（$2a$, $2b$, $2y$ 等）
        if (storedPassword.StartsWith("$2"))
        {
            return (BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword), false);
        }

        // 向后兼容：存储的是明文密码，直接比较后标记需要升级
        if (inputPassword == storedPassword)
        {
            return (true, true);
        }

        return (false, false);
    }
}
