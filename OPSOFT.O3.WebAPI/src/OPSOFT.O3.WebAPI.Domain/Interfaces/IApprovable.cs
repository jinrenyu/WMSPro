namespace OPSOFT.O3.WebAPI.Domain.Interfaces;

/// <summary>
/// 可审核实体接口 — 实体包含审核人和审核日期字段
/// </summary>
public interface IApprovable
{
    string FCheckerId { get; set; }
    DateTime FCheckDate { get; set; }
}
