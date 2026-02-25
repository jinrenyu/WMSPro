namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class OperationLogDto
{
    public string Uid { get; set; } = string.Empty;
    public DateTime? Fdate { get; set; }
    public string Fuserid { get; set; } = string.Empty;
    public string Fusername { get; set; } = string.Empty;
    public string Fprgkey { get; set; } = string.Empty;
    public string Foperationtype { get; set; } = string.Empty;
    public string Ftargetuid { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public string Fip { get; set; } = string.Empty;
    public bool Fsuccess { get; set; }
    public string Ferrormsg { get; set; } = string.Empty;
    public string Fstatement { get; set; } = string.Empty;
}

public class OperationLogQueryRequest : PagedRequest
{
    public string? UserId { get; set; }
    public string? PrgKey { get; set; }
    public string? OperationType { get; set; }
    public string? TargetUid { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
