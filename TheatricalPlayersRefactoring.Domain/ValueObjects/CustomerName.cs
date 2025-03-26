namespace TheatricalPlayersRefactoring.Domain.ValueObjects;

public record CustomerName
{
    protected CustomerName() { }
    public string Value { get; }

    public CustomerName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Customer name cannot be empty");

        Value = value;
    }
}
