using RabbitMQ.Client;

namespace TheatricalPlayersRefactoring.Infrastructure.Messaging;

public interface IRabbitMQConfiguration
{
    IModel GetChannel();
}