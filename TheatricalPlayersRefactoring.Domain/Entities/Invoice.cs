using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.Entities;

public class Invoice
{
    public Guid Id { get; private set; }
    public CustomerName Customer { get; private set; }
    public string? ExtractPath { get; private set; }
    private readonly List<Performance> _performances = new();
    public IReadOnlyCollection<Performance> Performances => _performances.AsReadOnly();
    public Credits TotalCredits { get; private set; }
    public Invoice() { }
    public Invoice(CustomerName customer)
    {
        Id = Guid.NewGuid();
        Customer = customer;
        TotalCredits = Credits.Zero;
    }

    public void AddPerformance(Performance performance)
    {
        _performances.Add(performance);
        UpdateCredits();
    }

    private void UpdateCredits()
    {
        TotalCredits = _performances
            .Select(p => p.CalculateCredits())
            .Aggregate(Credits.Zero, (total, current) => total.Add(current));
    }

    public void SaveExtract(string path)
    {
        ExtractPath = path;
    }

    public Money CalculateTotalAmount()
    {
        return _performances
            .Select(p => p.CalculateAmount())
            .Aggregate(Money.Zero, (total, current) => total.Add(current));
    }
}