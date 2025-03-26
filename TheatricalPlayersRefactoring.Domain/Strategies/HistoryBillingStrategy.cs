using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.Strategies;

public class HistoryBillingStrategy : IBillingStrategy
{
    private const int CreditThreshold = 30;
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
        return audience.Value > CreditThreshold
            ? new Credits(audience.Value - CreditThreshold)
            : Credits.Zero;
    }
}
