using TheatricalPlayersRefactoring.Domain.PlayTypes;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.Entities;

public class Play
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Lines Lines { get; private set; }
    public PlayType PlayType { get; private set; }

    private IBillingStrategy _billingStrategy;
    protected Play()
    {
    }

    public Play(string name, Lines lines, PlayType playType)
    {
        Id = Guid.NewGuid();
        Name = name;
        Lines = lines;
        PlayType = playType;
    }

    private IBillingStrategy SetBillingStrategy(PlayType playType)
    {
        return playType switch
        {
            PlayType.Comedy => new ComedyBillingStrategy(),
            PlayType.Tragedy => new TragedyBillingStrategy(),
            PlayType.History => new HistoryBillingStrategy(),
            _ => throw new ArgumentException($"Invalid play type: {playType}")
        };
    }

    public Money CalculateAmount(Audience audience)
    {
        _billingStrategy ??= SetBillingStrategy(PlayType);
        return _billingStrategy.CalculateAmount(Lines, audience);
    }

    public Credits CalculateCredits(Audience audience)
    {
        _billingStrategy ??= SetBillingStrategy(PlayType);
        return _billingStrategy.CalculateCredits(audience);
    }
}