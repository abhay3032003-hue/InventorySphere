using InventoryManagement.API.Models;

namespace InventoryManagement.API.Interfaces;

public interface IInvoiceItemRepository
{
    Task<IEnumerable<InvoiceItem>> GetAllAsync();

    Task<InvoiceItem?> GetByIdAsync(int id);

    Task<InvoiceItem> CreateAsync(InvoiceItem invoiceItem);

    Task UpdateAsync(InvoiceItem invoiceItem);

    Task DeleteAsync(InvoiceItem invoiceItem);
}