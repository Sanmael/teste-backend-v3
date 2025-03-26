using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.PlayTypes;

public class TragedyBillingStrategy : IBillingStrategy
{
    private const int BaseAudience = 30;
    private const decimal ExtraCharge = 10.00m;

    public Money CalculateAmount(Lines lines, Audience audience)
    {
        var baseAmount = lines.GetBaseAmount();
        if (audience.Value > BaseAudience)
            baseAmount += ExtraCharge * (audience.Value - BaseAudience);

        return new Money(baseAmount);
    }

    public Credits CalculateCredits(Audience audience)
    {
        return audience.Value > BaseAudience
            ? new Credits(audience.Value - BaseAudience)
            : Credits.Zero;
    }
}
