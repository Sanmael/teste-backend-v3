using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.DomainTests.Entities;

public class InvoiceTests
{
    [Fact]
    public void Invoice_WhenCreated_ThenHasNewId()
    {
        // Arrange
        var customerName = new CustomerName("John Doe");

        // Act
        var invoice = new Invoice(customerName);

        // Assert
        Assert.NotEqual(Guid.Empty, invoice.Id);
        Assert.Equal(customerName, invoice.Customer);
    }

    [Fact]
    public void Invoice_WhenAddingPerformance_ThenUpdatesTotalCredits()
    {
        // Arrange
        var invoice = new Invoice(new CustomerName("John Doe"));
        var play = new Play("Hamlet", new Lines(2000), PlayType.Tragedy);
        var performance = Performance.Create(play, new Audience(40));

        // Act
        invoice.AddPerformance(performance);

        // Assert
        Assert.Equal(10, invoice.TotalCredits.Value);
    }

    [Fact]
    public void Invoice_WhenCalculatingTotalAmount_ThenSumsAllPerformances()
    {
        // Arrange
        var invoice = new Invoice(new CustomerName("John Doe"));
        var play1 = new Play("Hamlet", new Lines(2000), PlayType.Tragedy);
        var play2 = new Play("Comedy", new Lines(2000), PlayType.Comedy);

        var perf1 = Performance.Create(play1, new Audience(40));
        var perf2 = Performance.Create(play2, new Audience(40));

        invoice.AddPerformance(perf1);
        invoice.AddPerformance(perf2);

        // Act
        var totalAmount = invoice.CalculateTotalAmount();

        // Assert
        Assert.True(totalAmount.Value > 0);
        Assert.Equal(perf1.CalculateAmount().Value + perf2.CalculateAmount().Value, totalAmount.Value);
    }

    [Fact]
    public void Invoice_WhenSavingExtract_ThenUpdatesPath()
    {
        // Arrange
        var invoice = new Invoice(new CustomerName("John Doe"));
        var extractPath = "C:\\extracts\\invoice.pdf";

        // Act
        invoice.SaveExtract(extractPath);

        // Assert
        Assert.Equal(extractPath, invoice.ExtractPath);
    }
}
