using TheatricalPlayersRefactoring.Domain.Factories;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.Entities;

public class Play
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Lines Lines { get; private set; }
    public PlayType PlayType { get; private set; }

    public Play(string name, Lines lines, PlayType playType)
    {
        Id = Guid.NewGuid();
        Name = name;
        Lines = lines;
        PlayType = playType;
    }

    public Money CalculateAmount(Audience audience)
    {
        var strategy = new BillingStrategyFactory().GetStrategy(PlayType);
        return strategy.CalculateAmount(Lines, audience);
    }

    public Credits CalculateCredits(Audience audience)
    {
        var strategy = new BillingStrategyFactory().GetStrategy(PlayType);
        return strategy.CalculateCredits(audience);
    }
}