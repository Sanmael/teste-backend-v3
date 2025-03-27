using MediatR;

namespace TheatricalPlayersRefactoring.Application.Queries;

public record GetBillQuery(Guid InvoiceId) : IRequest<BillDto>;