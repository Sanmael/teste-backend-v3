using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoring.Domain.Entities;
using TheatricalPlayersRefactoring.Domain.Repositories;
using TheatricalPlayersRefactoring.Infrastructure.Persistence;

namespace TheatricalPlayersRefactoring.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly TheatricalContext _context;

    public InvoiceRepository(TheatricalContext context)
    {
        _context = context;
    }

    public async Task<Invoice> GetByIdAsync(Guid id)
    {
        var invoice = await _context.Invoices
            .Include(x => x.Performances)
            .ThenInclude(x => x.Play)
            .FirstOrDefaultAsync(x => x.Id == id);

        return invoice ?? throw new KeyNotFoundException($"Invoice with id {id} not found");
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(x => x.Performances)
            .ThenInclude(x => x.Play)
            .ToListAsync();
    }

    public async Task SaveAsync(Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
    }
}