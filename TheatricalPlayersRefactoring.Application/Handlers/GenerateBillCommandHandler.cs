using MediatR;
using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Application.Factories;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Application.Handlers;

public class GenerateBillCommandHandler : IRequestHandler<GenerateBillCommand, GenerateBillResult>
{
    private readonly IInvoiceRepository _repository;    
    private readonly IStatementFactoryProvider _statementFactoryProvider;    

    public GenerateBillCommandHandler(
        IInvoiceRepository repository)
    {
        _statementFactoryProvider = new StatementFactoryProvider();
        _repository = repository;        
    }

    public async Task<GenerateBillResult> Handle(GenerateBillCommand command, CancellationToken cancellationToken)
    {
        var invoice = new Invoice(new CustomerName(command.CustomerName));

        foreach (var p in command.Performances)
        {
            var play = new Play(
                p.PlayName,
                new Lines(p.Lines),
                Enum.Parse<PlayType>(p.PlayType)
            );

            var performance = Performance.Create(play, new Audience(p.Audience));

            invoice.AddPerformance(performance);
        }

        await _repository.SaveAsync(invoice);

        var factory = _statementFactoryProvider.GetFactory(command.Format);

        var statement = factory.Generate(invoice);

        return new GenerateBillResult(invoice.Id, statement);
    }
}