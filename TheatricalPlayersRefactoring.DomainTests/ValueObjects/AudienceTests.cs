using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.ValueObjects;

public class AudienceTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Audience_WhenNegative_ThenThrowsException(int negativeValue)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Audience(negativeValue));
    }

    [Fact]
    public void Audience_WhenValid_ThenSetsValue()
    {
        // Arrange
        var expectedValue = 100;

        // Act
        var audience = new Audience(expectedValue);

        // Assert
        Assert.Equal(expectedValue, audience.Value);
    }
}
