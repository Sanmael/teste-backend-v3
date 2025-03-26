using TheatricalPlayersRefactoring.Domain.Strategies;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.PlayTypes;

public class TragedyStrategyTests
{
    private readonly TragedyBillingStrategy _strategy;
    private readonly Lines _lines;

    public TragedyStrategyTests()
    {
        _strategy = new TragedyBillingStrategy();
        _lines = new Lines(2000);
    }

    [Theory]
    [InlineData(20, 200)]
    [InlineData(40, 300)]
    public void TragedyStrategy_WhenCalculatingAmount_ThenAppliesFormula(int audienceCount, decimal expectedAmount)
    {
        // Arrange
        var audience = new Audience(audienceCount);

        // Act
        var amount = _strategy.CalculateAmount(_lines, audience);

        // Assert
        Assert.Equal(expectedAmount, amount.Value);
    }

    [Theory]
    [InlineData(20, 0)]
    [InlineData(40, 10)]
    public void TragedyStrategy_WhenCalculatingCredits_ThenChecksThreshold(int audienceCount, int expectedCredits)
    {
        // Arrange
        var audience = new Audience(audienceCount);

        // Act
        var credits = _strategy.CalculateCredits(audience);

        // Assert
        Assert.Equal(expectedCredits, credits.Value);
    }
}
