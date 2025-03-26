using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.ValueObjects;

public class LinesTests
{
    [Fact]
    public void Lines_WhenBelowMinimum_ThenClampsToMin()
    {
        // Arrange & Act
        var lines = new Lines(500);

        // Assert
        Assert.Equal(1000, lines.Value);
    }

    [Fact]
    public void Lines_WhenAboveMaximum_ThenClampsToMax()
    {
        // Arrange & Act
        var lines = new Lines(5000);

        // Assert
        Assert.Equal(4000, lines.Value);
    }

    [Fact]
    public void Lines_WhenCalculatingBaseAmount_ThenDividesByTen()
    {
        // Arrange
        var lines = new Lines(2000);

        // Act
        var baseAmount = lines.GetBaseAmount();

        // Assert
        Assert.Equal(200m, baseAmount);
    }
}
