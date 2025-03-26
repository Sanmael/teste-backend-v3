namespace TheatricalPlayersRefactoring.Application.Commands;

public record GenerateBillResult(
    Guid InvoiceId,
    string Message
);