namespace TheatricalPlayersRefactoring.Domain.Entities;

public class Invoice
{
    public Guid Id { get; private set; }
    public string? ExtractPath { get; private set; }
    private readonly List<Performance> _performances = new();
    public IReadOnlyCollection<Performance> Performances => _performances.AsReadOnly();
}