using FluentAssertions;
using Moq;
using TheatricalPlayersRefactoring.Application.Exceptions;
using TheatricalPlayersRefactoring.Application.Handlers;
using TheatricalPlayersRefactoring.Application.Queries;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Domain.ValueObjects;
using System.IO.Abstractions;
using TheatricalPlayersRefactoring.Application.Services;

namespace TheatricalPlayersRefactoring.ApplicationTests.GetBillTests
{
    public class GetBillQueryTests
    {
        private readonly Mock<IInvoiceRepository> _repositoryMock;
        private readonly GetBillQueryHandler _handler;
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<IFileBuilder> _fileSystemMock;

        public GetBillQueryTests()
        {
            _repositoryMock = new Mock<IInvoiceRepository>();
            _fileSystemMock = new Mock<IFileBuilder>();
            _handler = new GetBillQueryHandler(_repositoryMock.Object, _fileSystemMock.Object);
        }

        [Theory]
        [InlineData("txt")]
        [InlineData("xml")]
        public async Task GetBillQuery_WhenInvoiceExists_ThenReturnsBillDto(string extractFormat)
        {
            // Arrange            
            var extractPath = $"fake/path/to/extract.{extractFormat}";
            var extractContent = "Statement content";
            string contentType = "text/plain";
            FileDto fileDto = new FileDto(extractContent, extractFormat, contentType);

            var invoice = new Invoice(new CustomerName("BigCo"));
            invoice.SaveExtract(extractPath);

            _repositoryMock.Setup(x => x.GetByIdAsync(invoice.Id))
                .ReturnsAsync(invoice);

            _fileSystemMock.Setup(x => x.ReadFileAsync(extractPath)
                .Result)
                .Returns(fileDto);            

            // Act
            var result = await _handler.Handle(new GetBillQuery(invoice.Id), _cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(invoice.Id);
            result.CustomerName.Should().Be("BigCo");
            result.Content.Should().Be(extractContent);
            result.Format.Should().Be(extractFormat);
            result.ContentType.Should().Be(contentType);

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

        [Fact]
        public async Task GetBillQuery_WhenBillPathIsNull_ThenThrowsFileNotFoundException()
        {
            // Arrange            
            var invoice = new Invoice(new CustomerName("BigCo"));            

            _repositoryMock.Setup(x => x.GetByIdAsync(invoice.Id))
                .ReturnsAsync(invoice);

            // Act
            var act = () => _handler.Handle(new GetBillQuery(invoice.Id), _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<FileNotFoundException>()
                .WithMessage("Statement has not yet been generated, try again later");
        }
    }
}