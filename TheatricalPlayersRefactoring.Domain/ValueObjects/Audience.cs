namespace TheatricalPlayersRefactoring.Domain.ValueObjects;

public record Audience
{
    public int Value { get; }
    protected Audience() { }
    public Audience(int value)
    {
        if (value < 0)
            throw new ArgumentException("Audience cannot be negative");

        Value = value;
    }
}
