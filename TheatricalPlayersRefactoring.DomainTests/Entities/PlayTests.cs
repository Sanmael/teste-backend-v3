using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.Entities;

public class PlayTests
{
    [Fact]
    public void Play_WhenCreated_ThenHasValidProperties()
    {
        // Arrange
        var name = "Hamlet";
        var lines = new Lines(2000);
        var type = PlayType.Tragedy;

        // Act
        var play = new Play(name, lines, type);

        // Assert
        Assert.NotEqual(Guid.Empty, play.Id);
        Assert.Equal(name, play.Name);
        Assert.Equal(lines, play.Lines);
        Assert.Equal(type, play.PlayType);
    }

    [Fact]
    public void Play_WhenCalculatingAmount_ThenUsesCorrectStrategy()
    {
        // Arrange
        var play = new Play("Hamlet", new Lines(2000), PlayType.Tragedy);
        var audience = new Audience(40);
        var expectedAmount = 200m + 10m * (40 - 30);

        // Act
        var amount = play.CalculateAmount(audience);

        // Assert
        Assert.Equal(expectedAmount, amount.Value);
    }

    [Theory]
    [InlineData(999)]
    public void Play_WhenInvalidPlayType_ThenThrowsException(int invalidPlayType)
    {
        // Arrange
        var play = new Play("Test", new Lines(2000), (PlayType)invalidPlayType);
        var audience = new Audience(40);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => play.CalculateAmount(audience));
    }
}
