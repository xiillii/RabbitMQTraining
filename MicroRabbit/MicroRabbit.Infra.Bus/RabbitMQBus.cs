using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;

namespace MicroRabbit.Infra.Bus;

public sealed class RabbitMQBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, Type> _handlers;
    private readonly List<Type> _eventTypes;

    public RabbitMQBus(IMediator mediator)
    {
        _mediator = mediator;
        _eventTypes = new List<Type>();
        _handlers = new Dictionary<string, Type>();
    }

    public Task SendCommand<T>(T command) where T : Command
    {
        return _mediator.Send(command);
    }

    public void Publish<T>(T @event) where T : Event
    {
        throw new NotImplementedException();
    }

    public void Subscribe<T, TH>()
        where T : Event
        where TH : IEventHandler<T>
    {
        throw new NotImplementedException();
    }
}
