using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.Domain.Strategies;

public interface IBillingStrategy
{
    Money CalculateAmount(Lines lines, Audience audience);
    Credits CalculateCredits(Audience audience);
}
