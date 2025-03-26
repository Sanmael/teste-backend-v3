namespace TheatricalPlayersRefactoring.Domain.ValueObjects;

public record Credits
{
    protected Credits() { }

    public int Value { get; }

    public Credits(int value)
    {
        Value = value;
    }

    public static Credits Zero => new(0);

    public Credits Add(Credits other) => new(Value + other.Value);
}
