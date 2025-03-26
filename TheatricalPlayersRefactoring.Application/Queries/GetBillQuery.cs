using MediatR;

namespace TheatricalPlayersRefactoring.Application.Queries;

public record GetBillQuery(Guid InvoiceId) : IRequest<BillDto>;

public record BillDto(
    Guid Id,
    string CustomerName,
    string Statement
);