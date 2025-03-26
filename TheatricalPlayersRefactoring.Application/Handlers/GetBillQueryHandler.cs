using MediatR;
using TheatricalPlayersRefactoring.Application.Exceptions;
using TheatricalPlayersRefactoring.Application.Queries;
using TheatricalPlayersRefactoring.Domain.Repositories;

namespace TheatricalPlayersRefactoring.Application.Handlers;

public class GetBillQueryHandler : IRequestHandler<GetBillQuery, BillDto>
{
    private readonly IInvoiceRepository _invoiceRepository;    

    public GetBillQueryHandler(
        IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;        
    }

    public async Task<BillDto> Handle(GetBillQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var invoice = await _invoiceRepository.GetByIdAsync(query.InvoiceId);

            if (invoice == null)
                throw new NotFoundException($"Extract not found for invoice {query.InvoiceId}");                        

            return new BillDto(
                invoice.Id,
                invoice.Customer.Value,
                invoice.ExtractPath //TODO: Refatorar - colocar extrato
            );
        }
        catch (Exception ex)
        {
            throw ex switch
            {
                NotFoundException => ex,
                _ => new Exception($"Error retrieving bill for invoice {query.InvoiceId}", ex)
            };
        }
    }
}