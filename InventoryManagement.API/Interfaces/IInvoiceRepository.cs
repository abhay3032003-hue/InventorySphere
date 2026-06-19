using InventoryManagement.API.Models;

namespace InventoryManagement.API.Interfaces;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllAsync();

    Task<Invoice?> GetByIdAsync(int id);

    Task<Invoice> CreateAsync(Invoice invoice);

    Task UpdateAsync(Invoice invoice);

    Task DeleteAsync(Invoice invoice);
}