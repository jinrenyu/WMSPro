namespace OPSOFT.O3.WebAPI.Domain.Interfaces;

/// <summary>
/// 可禁用实体接口 — 实体包含禁用标志、禁用人和禁用日期字段
/// </summary>
public interface IDisableable
{
    bool FDisabled { get; set; }
    string Fdisableid { get; set; }
    DateTime Fdisabledate { get; set; }
}
