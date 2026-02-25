namespace OPSOFT.O3.WebAPI.Application.Interfaces;

public interface ICorrelationIdProvider
{
    string? CorrelationId { get; }
}
