namespace MecGestor.Domain.Intefaces.Events;

/// <summary>
/// Interface para dispatcher de eventos de domínio
/// </summary>
public interface IEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default);
}
