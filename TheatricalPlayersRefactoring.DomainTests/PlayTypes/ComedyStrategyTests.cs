using TheatricalPlayersRefactoring.Domain.Strategies;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.PlayTypes;

public class ComedyStrategyTests
{
    private readonly ComedyBillingStrategy _strategy;
    private readonly Lines _lines;

    public ComedyStrategyTests()
    {
        _strategy = new ComedyBillingStrategy();
        _lines = new Lines(2000);
    }

    [Theory]
    [InlineData(10, 230)]
    [InlineData(30, 440)]
    public void ComedyStrategy_WhenCalculatingAmount_ThenAppliesFormula(int audienceCount, decimal expectedAmount)
    {
        // Arrange
        var audience = new Audience(audienceCount);

        // Act
        var amount = _strategy.CalculateAmount(_lines, audience);

        // Assert
        Assert.Equal(expectedAmount, amount.Value);
    }

    [Theory]
    [InlineData(35, 12)]
    [InlineData(20, 4)]
    public void ComedyStrategy_WhenCalculatingCredits_ThenIncludesBonus(int audienceCount, int expectedCredits)
    {
        // Arrange
        var audience = new Audience(audienceCount);

        // Act
        var credits = _strategy.CalculateCredits(audience);

        // Assert
        Assert.Equal(expectedCredits, credits.Value);
    }
}
