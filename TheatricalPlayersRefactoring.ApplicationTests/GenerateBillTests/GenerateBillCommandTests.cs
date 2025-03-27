using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;
using RabbitMQ.Client;
using TheatricalPlayersRefactoring.Application.Commands;
using TheatricalPlayersRefactoring.Application.Handlers;
using TheatricalPlayersRefactoring.Application.Validation;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Infrastructure.Messaging;

namespace TheatricalPlayersRefactoring.ApplicationTests.GenerateBillTests
{
    public class GenerateBillCommandTests
    {
        private readonly Mock<IInvoiceRepository> _repositoryMock;
        private readonly IValidator<GenerateBillCommand> _validator;
        private readonly GenerateBillCommandHandler _handler;
        private readonly ValidateGenerateBillCommandBehavior _validateHandler;
        private readonly CancellationToken _cancellationToken;
        private readonly Mock<IRabbitMQConfiguration> _rabbitMQMock;

        public GenerateBillCommandTests()
        {
            _repositoryMock = new Mock<IInvoiceRepository>();
            _rabbitMQMock = new Mock<IRabbitMQConfiguration>();
            _validator = new GenerateBillCommandValidator();
            _handler = new GenerateBillCommandHandler(_repositoryMock.Object, _rabbitMQMock.Object);
            _validateHandler = new ValidateGenerateBillCommandBehavior(_validator);
        }

        [Fact]
        public async Task GenerateBillCommand_WhenCustomerNameIsEmpty_ThenThrowsValidationException()
        {
            // Arrange
            var command = new GenerateBillCommand(
                string.Empty,
                new[] { new PerformanceRequest("Hamlet", 4024, "Tragedy", 55) },
                StatementFormat.Text
            );

            // Act
            var act = () => _validateHandler.Handle(command, null, _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("*'Customer Name' must not be empty.*");
        }

        [Fact]
        public async Task GenerateBillCommand_WhenCustomerNameExceeds100Chars_ThenThrowsValidationException()
        {
            // Arrange
            var longName = new string('x', 101);
            var command = new GenerateBillCommand(
                longName,
                new[] { new PerformanceRequest("Hamlet", 4024, "Tragedy", 55) },
                StatementFormat.Text
            );

            // Act
            var act = () => _validateHandler.Handle(command, null, _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("*'Customer Name' must be 100 characters or fewer.*");
        }

        [Fact]
        public async Task GenerateBillCommand_WhenPerformanceListIsEmpty_ThenThrowsValidationException()
        {
            // Arrange
            var command = new GenerateBillCommand(
                "BigCo",
                Array.Empty<PerformanceRequest>(),
                StatementFormat.Text
            );

            // Act
            var act = () => _validateHandler.Handle(command, null, _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("*At least one performance is required*");
        }

        [Fact]
        public async Task GenerateBillCommand_WhenDatabaseIsUnavailable_ThenThrowsException()
        {
            // Arrange
            var command = new GenerateBillCommand(
                "BigCo",
                new[] { new PerformanceRequest("Hamlet", 4024, "Tragedy", 55) },
                StatementFormat.Text
            );

            _repositoryMock.Setup(x => x.SaveAsync(It.IsAny<Invoice>()))
                .ThrowsAsync(new Exception("Database connection failed"));

            RequestHandlerDelegate<GenerateBillResult> next = new(() => _handler.Handle(command, _cancellationToken));

            // Act
            var act = () => _validateHandler.Handle(command, next, _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Database connection failed");
        }

        [Fact]
        public async Task GenerateBillCommand_WhenPlayTypeIsInvalid_ThenThrowsValidationException()
        {
            // Arrange
            var command = new GenerateBillCommand(
                "BigCo",
                new[] { new PerformanceRequest("Hamlet", 4024, "InvalidType", 55) },
                StatementFormat.Text
            );

            // Act
            var act = () => _validateHandler.Handle(command, null, _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("*Invalid play type. Allowed values are: Tragedy, Comedy, History*");
        }

        [Fact]
        public async Task GenerateBillCommand_WhenAudienceIsNegative_ThenThrowsValidationException()
        {
            // Arrange
            var command = new GenerateBillCommand(
                "BigCo",
                new[] { new PerformanceRequest("Hamlet", 4024, "Tragedy", -1) },
                StatementFormat.Text
            );

            // Act
            var act = () => _validateHandler.Handle(command, null, _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<ValidationException>()
                .WithMessage("*'Audience' must be greater than or equal to '0'*");
        }

        [Fact]
        public async Task GenerateBillCommand_WhenInputIsValid_ThenSavesInvoiceAndPublishesMessage()
        {
            // Arrange
            var command = new GenerateBillCommand(
                "BigCo",
                new[]
                {
                new PerformanceRequest("Hamlet", 4024, "Tragedy", 55),
                new PerformanceRequest("As You Like", 2670, "Comedy", 35)
                },
                StatementFormat.Text
            );

            _repositoryMock.Setup(x => x.SaveAsync(It.IsAny<Invoice>()))
                .Returns(Task.CompletedTask);

            var mockChannel = new Mock<IModel>();
            _rabbitMQMock.Setup(x => x.GetChannel())
                .Returns(mockChannel.Object);

            RequestHandlerDelegate<GenerateBillResult> next = new(() => _handler.Handle(command, _cancellationToken));

            // Act
            var result = await _validateHandler.Handle(command, next, _cancellationToken);

            // Assert
            result.Should().NotBeNull();
            result.InvoiceId.Should().NotBe(Guid.Empty);
            result.Message.Should().Be("Bill generation queued for processing");

            _repositoryMock.Verify(x => x.SaveAsync(It.IsAny<Invoice>()), Times.Once);
            _rabbitMQMock.Verify(x => x.GetChannel(), Times.Once);
        }

        [Fact]
        public async Task GenerateBillCommand_WhenQueueIsUnavailable_ThenThrowsException()
        {
            // Arrange
            var command = new GenerateBillCommand(
                "BigCo",
                new[] { new PerformanceRequest("Hamlet", 4024, "Tragedy", 55) },
                StatementFormat.Text
            );

            RequestHandlerDelegate<GenerateBillResult> next = new(() => _handler.Handle(command, _cancellationToken));

            _repositoryMock.Setup(x => x.SaveAsync(It.IsAny<Invoice>()))
                .Returns(Task.CompletedTask);

            _rabbitMQMock.Setup(x => x.GetChannel())
                .Throws(new Exception("Queue connection failed"));

            // Act
            var act = () => _validateHandler.Handle(command, next, _cancellationToken);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Queue connection failed");
        }
    }
}