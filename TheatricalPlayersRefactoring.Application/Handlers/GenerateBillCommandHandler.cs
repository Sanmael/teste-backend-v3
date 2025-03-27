using MediatR;
using RabbitMQ.Client;
using System.Text.Json;
using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Domain.ValueObjects;
using TheatricalPlayersRefactoring.Infrastructure.Messaging;

namespace TheatricalPlayersRefactoring.Application.Handlers;

public class GenerateBillCommandHandler : IRequestHandler<GenerateBillCommand, GenerateBillResult>
{
    private readonly IInvoiceRepository _repository;       
    private readonly IRabbitMQConfiguration _rabbitMQ;

    public GenerateBillCommandHandler(
        IInvoiceRepository repository,
        IRabbitMQConfiguration rabbitMQ)
    {        
        _repository = repository;
        _rabbitMQ = rabbitMQ;
    }

    public async Task<GenerateBillResult> Handle(GenerateBillCommand command, CancellationToken cancellationToken)
    {
        var invoice = new Invoice(new CustomerName(command.CustomerName));
        
        foreach (var p in command.Performances)
        {
            Enum.TryParse<PlayType>(p.PlayType,true, out var playTypeEnum);

            var play = new Play(
                p.PlayName,
                new Lines(p.Lines),
                playTypeEnum
            );

            var performance = Performance.Create(play, new Audience(p.Audience));

            invoice.AddPerformance(performance);
        }

        await _repository.SaveAsync(invoice);

        var message = new { InvoiceId = invoice.Id, Format = command.Format };

        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        var channel = _rabbitMQ.GetChannel();

        channel.BasicPublish(
            exchange: "",
            routingKey: "invoice_queue",
            basicProperties: null,
            body: body);

        return new GenerateBillResult(invoice.Id, "Bill generation queued for processing");
    }
}