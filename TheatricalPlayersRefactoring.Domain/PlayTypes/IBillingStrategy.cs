using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.PlayTypes;

public interface IBillingStrategy
{
    Money CalculateAmount(Lines lines, Audience audience);
    Credits CalculateCredits(Audience audience);
}
