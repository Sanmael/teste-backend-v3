using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.ValueObjects;

public class CustomerNameTests
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CustomerName_WhenEmpty_ThenThrowsException(string emptyName)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new CustomerName(emptyName));
    }

    [Fact]
    public void CustomerName_WhenValid_ThenSetsValue()
    {
        // Arrange
        var name = "John Doe";

        // Act
        var customerName = new CustomerName(name);

        // Assert
        Assert.Equal(name, customerName.Value);
    }
}
