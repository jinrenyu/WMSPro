namespace OPSOFT.O3.WebAPI.Infrastructure.ErpIntegration;

/// <summary>
/// ERP 集成配置节（appsettings: "ErpIntegration"）。
/// </summary>
public class ErpIntegrationOptions
{
    public const string SectionName = "ErpIntegration";

    /// <summary>取数实现：Stub=模拟数据（默认，先行联调）/ K3Cloud=金蝶云星空真连</summary>
    public string Provider { get; set; } = "Stub";

    /// <summary>定时调度总开关</summary>
    public bool SchedulerEnabled { get; set; } = true;

    /// <summary>调度轮询间隔（秒）：每隔多久检查一次是否有任务到点</summary>
    public int SchedulerPollSeconds { get; set; } = 30;

    // ===== K3Cloud 连接参数（Provider=K3Cloud 时生效）=====
    /// <summary>K3Cloud 站点地址，如 http://kingdee-host/K3Cloud</summary>
    public string ServerUrl { get; set; } = string.Empty;
    /// <summary>账套 ID</summary>
    public string AcctId { get; set; } = string.Empty;
    /// <summary>第三方集成 AppId</summary>
    public string AppId { get; set; } = string.Empty;
    /// <summary>第三方集成 AppSecret（走 LoginByAppSecret 时使用；留空则走用户名密码 ValidateUser 登录）</summary>
    public string AppSecret { get; set; } = string.Empty;
    /// <summary>用户名（ValidateUser / LoginByAppSecret 均使用；支持中文，须 UTF-8 传输）</summary>
    public string UserName { get; set; } = string.Empty;
    /// <summary>用户密码（ValidateUser 登录使用；AppSecret 留空时启用此方式）</summary>
    public string Password { get; set; } = string.Empty;
    /// <summary>语言 ID，简体中文 2052</summary>
    public int LcId { get; set; } = 2052;

    /// <summary>ExecuteBillQuery 单页行数（Limit）。默认 500。
    /// 注意：页越大响应越大越慢；若查询字段含图片/二进制大字段(如 FImage1)，单页响应会爆炸(2000行曾达290MB/126s致超时)，
    /// 正确做法是别在批量同步里查此类大字段，而非靠调小页数。</summary>
    public int QueryPageSize { get; set; } = 500;
}
