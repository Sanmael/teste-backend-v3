using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.ValueObjects;

public class MoneyTests
{
    [Fact]
    public void Money_WhenAdding_ThenReturnsSumValue()
    {
        // Arrange
        var money1 = new Money(100m);
        var money2 = new Money(50m);

        // Act
        var result = money1.Add(money2);

        // Assert
        Assert.Equal(150m, result.Value);
    }

    [Fact]
    public void Money_WhenCreatingZero_ThenReturnsZeroValue()
    {
        // Act
        var zeroMoney = Money.Zero;

        // Assert
        Assert.Equal(0m, zeroMoney.Value);
    }
}
