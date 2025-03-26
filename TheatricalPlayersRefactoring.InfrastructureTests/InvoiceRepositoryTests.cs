using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Domain.ValueObjects;
using TheatricalPlayersRefactoring.Infrastructure.Persistence;
using TheatricalPlayersRefactoring.Infrastructure.Repositories;

namespace TheatricalPlayersRefactoring.InfrastructureTests;

public class InvoiceRepositoryTests
{
    private readonly IInvoiceRepository _repository;

    public InvoiceRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TheatricalContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new TheatricalContext(options);

        _repository = new InvoiceRepository(context);
    }

    [Fact]
    public async Task InvoiceRepository_WhenGettingExistingInvoice_ThenReturnsInvoice()
    {
        // Arrange             
        var invoice = new Invoice(new CustomerName("Test Customer"));
        var performance = Performance.Create(new Play("John Doe", new Lines(4130), PlayType.Tragedy), new Audience(35));
        invoice.AddPerformance(performance);

        await _repository.SaveAsync(invoice);

        // Act
        var result = await _repository.GetByIdAsync(invoice.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(invoice.Id, result.Id);
    }

    [Fact]
    public async Task InvoiceRepository_WhenInvoiceNotFound_ThenThrowsKeyNotFoundException()
    {               
        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _repository.GetByIdAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task InvoiceRepository_WhenSavingNewInvoice_ThenPersistsToDatabase()
    {
        // Arrange        
        var invoice = new Invoice(new CustomerName("Test Customer"));

        // Act
        await _repository.SaveAsync(invoice);

        // Assert
        var savedInvoice = await _repository.GetByIdAsync(invoice.Id);
        Assert.NotNull(savedInvoice);
        Assert.Equal(invoice.Id, savedInvoice.Id);
    }

    [Fact]
    public async Task InvoiceRepository_WhenUpdatePath_ThenPersistsToDatabase()
    {
        // Arrange        
        string path = @"C\FakePath";
        var invoice = new Invoice(new CustomerName("Test Customer"));
        var performance = Performance.Create(new Play("John Doe", new Lines(4130), PlayType.Tragedy), new Audience(35));
        invoice.AddPerformance(performance);

        // Act
        await _repository.SaveAsync(invoice);

        invoice.SaveExtract(path);

        await _repository.UpdateAsync(invoice);

        // Assert
        var updatedPath = await _repository.GetByIdAsync(invoice.Id);        
        Assert.Equal(path, updatedPath.ExtractPath);
    }
}