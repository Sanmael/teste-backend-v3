using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.ValueObjects;

public class CreditsTests
{
    [Fact]
    public void Credits_WhenAdding_ThenReturnsSumValue()
    {
        // Arrange
        var credits1 = new Credits(10);
        var credits2 = new Credits(5);

        // Act
        var result = credits1.Add(credits2);

        // Assert
        Assert.Equal(15, result.Value);
    }

    [Fact]
    public void Credits_WhenCreatingZero_ThenReturnsZeroValue()
    {
        // Act
        var zeroCredits = Credits.Zero;

        // Assert
        Assert.Equal(0, zeroCredits.Value);
    }
}
