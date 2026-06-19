using InventoryManagement.API.Models;

namespace InventoryManagement.API.Interfaces;

public interface IInvoiceService
{
    Task<IEnumerable<Invoice>> GetAllInvoices();

    Task<Invoice?> GetInvoiceById(int id);

    Task<Invoice> CreateInvoice(Invoice invoice);

    Task UpdateInvoice(Invoice invoice);

    Task DeleteInvoice(int id);
}