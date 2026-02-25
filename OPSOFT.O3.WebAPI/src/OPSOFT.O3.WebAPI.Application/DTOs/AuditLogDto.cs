namespace OPSOFT.O3.WebAPI.Application.DTOs;

public class AuditChangeItem
{
    /// <summary>
    /// 类型: 1=新增 2=修改 3=删除
    /// </summary>
    public int Type { get; set; }
    public string TableName { get; set; } = string.Empty;
    public string ColumnName { get; set; } = string.Empty;
    public string BeforeData { get; set; } = string.Empty;
    public string AfterData { get; set; } = string.Empty;
    public int EntryId { get; set; }
    public string DetailId { get; set; } = string.Empty;
}

public class AuditLogListDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fuid { get; set; } = string.Empty;
    public string Fprgkey { get; set; } = string.Empty;
    public string Fdescription { get; set; } = string.Empty;
    public string Fbillno { get; set; } = string.Empty;
    public string Fip { get; set; } = string.Empty;
    public string CUser { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public int EntryCount { get; set; }
}

public class AuditLogDetailDto
{
    public string Uid { get; set; } = string.Empty;
    public string Fdescription { get; set; } = string.Empty;
    public string CUser { get; set; } = string.Empty;
    public DateTime? CYmd { get; set; }
    public List<AuditLogEntryDto> Entries { get; set; } = new();
}

public class AuditLogEntryDto
{
    public int Ftype { get; set; }
    public string Ftablename { get; set; } = string.Empty;
    public string Fcolumnname { get; set; } = string.Empty;
    public string Fbeforedata { get; set; } = string.Empty;
    public string Fafterdata { get; set; } = string.Empty;
    public int Fentryid { get; set; }
}

public class AuditLogQueryRequest : PagedRequest
{
    public string? PrgKey { get; set; }
    public string? TargetUid { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
