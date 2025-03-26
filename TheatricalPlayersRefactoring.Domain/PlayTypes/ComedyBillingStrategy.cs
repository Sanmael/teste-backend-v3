using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.PlayTypes;

public class ComedyBillingStrategy : IBillingStrategy
{
    private const int BaseExtraAudience = 20;
    private const int CreditThreshold = 30;
    private const decimal BaseAudienceCharge = 3.00m;
    private const decimal ExtraBaseCharge = 100.00m;
    private const decimal ExtraAudienceCharge = 5.00m;

    public Money CalculateAmount(Lines lines, Audience audience)
    {
        var amount = lines.GetBaseAmount() + audience.Value * BaseAudienceCharge;

        if (audience.Value > BaseExtraAudience)
        {
            amount += ExtraBaseCharge + ExtraAudienceCharge * (audience.Value - BaseExtraAudience);
        }

        return new Money(amount);
    }

    public Credits CalculateCredits(Audience audience)
    {
        var baseCredits = audience.Value > CreditThreshold
            ? audience.Value - CreditThreshold
            : 0;

        var bonusCredits = (int)(audience.Value / 5.0);

        return new Credits(baseCredits + bonusCredits);
    }
}
