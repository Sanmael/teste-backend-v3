namespace TheatricalPlayersRefactoring.Domain.ValueObjects;

public record Money
{
    protected Money() { }
    public decimal Value { get; private set; }

    public Money(decimal value)
    {
        Value = value;
    }

    public static Money Zero => new(0);

    public Money Add(Money other) => new(Value + other.Value);
}
