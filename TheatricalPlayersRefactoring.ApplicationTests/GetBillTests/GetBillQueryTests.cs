using FluentAssertions;
using Moq;
using TheatricalPlayersRefactoring.Application.Exceptions;
using TheatricalPlayersRefactoring.Application.Handlers;
using TheatricalPlayersRefactoring.Application.Queries;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Domain.ValueObjects;

namespace TheatricalPlayersRefactoring.ApplicationTests.GetBillTests
{
    public class GetBillQueryTests
    {
        private readonly Mock<IInvoiceRepository> _repositoryMock;
        private readonly GetBillQueryHandler _handler;
        private readonly CancellationToken _cancellationToken;

        public GetBillQueryTests()
        {
            _repositoryMock = new Mock<IInvoiceRepository>();
            _handler = new GetBillQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetBillQuery_WhenInvoiceExists_ThenReturnsBillDto()
        {
            // Arrange
            var extractPath = "fake/path/to/extract.txt";

            var invoice = new Invoice(new CustomerName("BigCo"));
            invoice.SaveExtract(extractPath);

            _repositoryMock.Setup(x => x.GetByIdAsync(invoice.Id))
                .ReturnsAsync(invoice);

            // Act
            var result = await _handler.Handle(new GetBillQuery(invoice.Id), _cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(invoice.Id);
            result.CustomerName.Should().Be("BigCo");
            //result.Statement.Should().Be(extractContent); TODO: refatorar posteriormente quando houver extrato

            _repositoryMock.Verify(x => x.GetByIdAsync(invoice.Id), Times.Once);
        }

        [Fact]
        public async Task GetBillQuery_WhenInvoiceNotFound_ThenThrowsNotFoundException()
        {
            // Arrange
            var invoiceId = Guid.NewGuid();
            _repositoryMock.Setup(x => x.GetByIdAsync(invoiceId))
                .ThrowsAsync(new NotFoundException($"Invoice not found: {invoiceId}"));

            // Act
            var act = () => _handler.Handle(new GetBillQuery(invoiceId), _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Invoice not found: {invoiceId}");
        }
    }
}