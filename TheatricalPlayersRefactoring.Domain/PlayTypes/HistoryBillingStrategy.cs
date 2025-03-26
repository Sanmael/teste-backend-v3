using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.PlayTypes;

public class HistoryBillingStrategy : IBillingStrategy
{
    private readonly IBillingStrategy _tragedyStrategy;
    private readonly IBillingStrategy _comedyStrategy;

    public HistoryBillingStrategy()
    {
        _tragedyStrategy = new TragedyBillingStrategy();
        _comedyStrategy = new ComedyBillingStrategy();
    }

    public Money CalculateAmount(Lines lines, Audience audience)
    {
        var tragedyAmount = _tragedyStrategy.CalculateAmount(lines, audience);
        var comedyAmount = _comedyStrategy.CalculateAmount(lines, audience);
        return tragedyAmount.Add(comedyAmount);
    }

    public Credits CalculateCredits(Audience audience)
    {
        var tragedyCredits = _tragedyStrategy.CalculateCredits(audience);
        var comedyCredits = _comedyStrategy.CalculateCredits(audience);
        return tragedyCredits.Add(comedyCredits);
    }
}
