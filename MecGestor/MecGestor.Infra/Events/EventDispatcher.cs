using MecGestor.Domain.Intefaces.Events;
using Microsoft.Extensions.DependencyInjection;

namespace MecGestor.Infra.Events;

/// <summary>
/// Implementação do dispatcher de eventos
/// Responsável por encontrar e executar todos os handlers registrados para um evento
/// </summary>
public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
    {
        var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

        var tasks = handlers.Select(handler => handler.HandleAsync(@event, cancellationToken));

        await Task.WhenAll(tasks);
    }
}
