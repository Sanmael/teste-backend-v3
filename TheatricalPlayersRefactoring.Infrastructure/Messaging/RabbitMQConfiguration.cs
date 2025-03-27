using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace TheatricalPlayersRefactoring.Infrastructure.Messaging;

public class RabbitMQConfiguration : IRabbitMQConfiguration
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public RabbitMQConfiguration(IConfiguration configuration)
    {
        var section = configuration.GetSection("RabbitMQ");

        var factory = new ConnectionFactory()
        {
            HostName = section["HostName"],
            UserName = section["UserName"],
            Password = section["Password"]
        };

        _connection = factory.CreateConnection();

        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "invoice_queue",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
    }

    public IModel GetChannel() => _channel;
}