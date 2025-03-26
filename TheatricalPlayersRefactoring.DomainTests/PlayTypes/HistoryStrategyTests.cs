using TheatricalPlayersRefactoring.Domain.Strategies;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.PlayTypes;

public class HistoryStrategyTests
{
    private readonly HistoryBillingStrategy _strategy;
    private readonly Lines _lines;

    public HistoryStrategyTests()
    {
        _strategy = new HistoryBillingStrategy();
        _lines = new Lines(2000);
    }

    [Fact]
    public void HistoryStrategy_WhenCalculatingAmount_ThenCombinesStrategies()
    {
        // Arrange
        var audience = new Audience(40);
        var tragedyStrategy = new TragedyBillingStrategy();
        var comedyStrategy = new ComedyBillingStrategy();

        var expectedAmount = tragedyStrategy.CalculateAmount(_lines, audience)
            .Add(comedyStrategy.CalculateAmount(_lines, audience));

        // Act
        var amount = _strategy.CalculateAmount(_lines, audience);

        // Assert
        Assert.Equal(expectedAmount.Value, amount.Value);
    }

    [Fact]
    public void HistoryStrategy_WhenCalculatingCredits_ThenCombinesCredits()
    {
        var expectedCredits = 10;

        // Arrange
        var audience = new Audience(40);

        // Act
        var credits = _strategy.CalculateCredits(audience);

        // Assert
        Assert.Equal(expectedCredits, credits.Value);
    }
}
