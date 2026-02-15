namespace MecGestor.Domain.Intefaces.Events;

/// <summary>
/// Interface para handlers de eventos de domínio
/// </summary>
public interface IEventHandler<in TEvent>
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}
