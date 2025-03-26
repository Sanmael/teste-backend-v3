using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Domain.Entities;

namespace TheatricalPlayersRefactoring.Application.Factories;

public interface IStatementFactory
{
    string Generate(Invoice invoice);
}

public interface IStatementFactoryProvider
{
    IStatementFactory GetFactory(StatementFormat format);
}

public class StatementFactoryProvider : IStatementFactoryProvider
{
    private readonly Dictionary<StatementFormat, IStatementFactory> _factories;

    public StatementFactoryProvider()
    {
        _factories = new Dictionary<StatementFormat, IStatementFactory>
        {
            { StatementFormat.Text, new TextStatementFactory() },
            { StatementFormat.Xml, new XmlStatementFactory() }
        };
    }

    public IStatementFactory GetFactory(StatementFormat format) =>
        _factories.TryGetValue(format, out var factory)
            ? factory
            : throw new ArgumentException($"Unsupported format: {format}");
}