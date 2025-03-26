using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.Entities;

public class PerformanceTests
{
    private readonly Play _play;
    private readonly Audience _audience;

    public PerformanceTests()
    {
        _play = new Play("Hamlet", new Lines(2000), PlayType.Tragedy);
        _audience = new Audience(55);
    }

    [Fact]
    public void Performance_WhenCreated_ThenHasValidId()
    {
        // Act
        var performance = Performance.Create(_play, _audience);

        // Assert
        Assert.NotEqual(Guid.Empty, performance.Id);
        Assert.Equal(_play.Id, performance.PlayId);
        Assert.Equal(_play, performance.Play);
        Assert.Equal(_audience, performance.Audience);
    }

    [Fact]
    public void Performance_WhenCalculatingAmount_ThenUsesPlayStrategy()
    {
        // Arrange
        var performance = Performance.Create(_play, _audience);
        var expectedAmount = _play.CalculateAmount(_audience);

        // Act
        var amount = performance.CalculateAmount();

        // Assert
        Assert.Equal(expectedAmount.Value, amount.Value);
    }

    [Fact]
    public void Performance_WhenCalculatingCredits_ThenUsesPlayStrategy()
    {
        // Arrange
        var performance = Performance.Create(_play, _audience);
        var expectedCredits = _play.CalculateCredits(_audience);

        // Act
        var credits = performance.CalculateCredits();

        // Assert
        Assert.Equal(expectedCredits.Value, credits.Value);
    }
}
