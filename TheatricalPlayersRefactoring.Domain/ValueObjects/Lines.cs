namespace TheatricalPlayersRefactoring.Domain.ValueObjects;

public class Lines
{
    protected Lines() { }
    private const int MinLines = 1000;
    private const int MaxLines = 4000;

    public int Value { get; }

    public Lines(int value)
    {
        Value = Math.Clamp(value, MinLines, MaxLines);
    }

    public decimal GetBaseAmount() => Value / 10m;
}
