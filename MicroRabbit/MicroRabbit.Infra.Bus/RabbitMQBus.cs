using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MicroRabbit.Infra.Bus;

public sealed class RabbitMQBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;

    public RabbitMQBus(IMediator mediator)
    {
        _mediator = mediator;
        _eventTypes = new List<Type>();
        _handlers = new Dictionary<string, List<Type>>();
    }

    public Task SendCommand<T>(T command) where T : Command
    {
        return _mediator.Send(command);
    }

    public void Publish<T>(T @event) where T : Event
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using var connection = factory.CreateConnection();

        using (var channel = connection.CreateModel())
        {
            var eventName = @event.GetType().Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var message = JsonSerializer.Serialize(@event);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("", eventName, null, body);
        }
    }

    public void Subscribe<T, TH>()
        where T : Event
        where TH : IEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);

        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, new List<Type>());
        }

        if (_handlers[eventName].Any(t => t.GetType() == handlerType))
        {
            throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for {eventName}",
                                        nameof(handlerType));
        }

        _handlers[eventName].Add(handlerType);

        // TODO: Start a basic consume
    }
}
