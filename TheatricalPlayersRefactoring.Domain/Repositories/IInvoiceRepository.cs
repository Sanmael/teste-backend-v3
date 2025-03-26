using TheatricalPlayersRefactoring.Domain.Entities;

namespace TheatricalPlayersRefactoring.Domain.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice> GetByIdAsync(Guid id);
    Task SaveAsync(Invoice invoice);
    Task UpdateAsync(Invoice invoice);
    Task<IEnumerable<Invoice>> GetAllAsync();
}
