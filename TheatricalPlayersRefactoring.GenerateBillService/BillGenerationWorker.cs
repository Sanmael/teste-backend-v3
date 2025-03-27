using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Application.Factories;
using TheatricalPlayersRefactoring.Application.Services;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Infrastructure.Messaging;

namespace TheatricalPlayersRefactoring.GenerateBillService;

public class BillGenerationWorker
{
    private readonly IRabbitMQConfiguration _rabbitMQ;
    private readonly IInvoiceRepository _repository;
    private readonly IStatementFactoryProvider _statementFactoryProvider;
    private readonly ILogger<BillGenerationWorker> _logger;
    private readonly string _outputDirectory;
    private readonly IFileBuilder _fileBuilder;

    public BillGenerationWorker(
        IRabbitMQConfiguration rabbitMQ,
        IInvoiceRepository repository,
        IStatementFactoryProvider statementFactoryProvider,
        IConfiguration configuration,
        ILogger<BillGenerationWorker> logger,
        IFileBuilder fileBuilder)
    {
        _rabbitMQ = rabbitMQ;
        _repository = repository;
        _statementFactoryProvider = statementFactoryProvider;
        _logger = logger;
        _outputDirectory = configuration["BillGenerator:OutputDirectory"];
        _fileBuilder = fileBuilder;
    }

    public async Task Start(CancellationToken stoppingToken)
    {
        var channel = _rabbitMQ.GetChannel();

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();

                var message = JsonSerializer.Deserialize<BillGenerationMessage>(body);

                var invoice = await _repository.GetByIdAsync(message.InvoiceId);

                var factory = _statementFactoryProvider.GetFactory(message.Format);

                var statement = factory.Generate(invoice);

                await CreateFileAsync(invoice, message, statement);

                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing bill generation message");
                channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        channel.BasicConsume(
            queue: "invoice_queue",
            autoAck: false,
            consumer: consumer);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public async Task CreateFileAsync(Invoice invoice, BillGenerationMessage message, string statement)
    {
        var filePath = _fileBuilder.CreateFile(message.InvoiceId, message.Format, _outputDirectory, statement);
      
        await UpdateInvoiceAsync(invoice, filePath);
    }

    public async Task UpdateInvoiceAsync(Invoice invoice, string filePath)
    {
        invoice.SaveExtract(filePath);

        await _repository.UpdateAsync(invoice);
    }
}

public class BillGenerationMessage
{
    public Guid InvoiceId { get; set; }
    public StatementFormat Format { get; set; }
}