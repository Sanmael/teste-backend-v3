using MediatR;
using TheatricalPlayersRefactoring.Application.Exceptions;
using TheatricalPlayersRefactoring.Application.Queries;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Application.Services;

namespace TheatricalPlayersRefactoring.Application.Handlers;

public class GetBillQueryHandler : IRequestHandler<GetBillQuery, BillDto>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IFileBuilder _fileBuilder;

    public GetBillQueryHandler(
        IInvoiceRepository invoiceRepository,        
        IFileBuilder fileBuilder)
    {
        _fileBuilder = fileBuilder;
        _invoiceRepository = invoiceRepository;        
    }

    public async Task<BillDto> Handle(GetBillQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var invoice = await _invoiceRepository.GetByIdAsync(query.InvoiceId);

            if (invoice == null)
                throw new NotFoundException($"Invoice not found for InvoiceId:{query.InvoiceId}");

            if (invoice.BillPath == null)
                throw new FileNotFoundException($"Statement has not yet been generated, try again later");

            FileDto file = await _fileBuilder.ReadFileAsync(invoice.BillPath);

            return new BillDto(
                invoice.Id,
                invoice.Customer.Value,
                file.Content,
                file.Format,
                file.ContentType
            );
        }
        catch (Exception ex)
        {
            throw ex switch
            {
                FileNotFoundException => ex,
                NotFoundException => ex,
                _ => new Exception($"Error retrieving bill for invoice {query.InvoiceId}", ex)
            };
        }
    }
}