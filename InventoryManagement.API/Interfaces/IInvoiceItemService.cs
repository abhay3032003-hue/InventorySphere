using InventoryManagement.API.Models;

namespace InventoryManagement.API.Interfaces;

public interface IInvoiceItemService
{
    Task<IEnumerable<InvoiceItem>> GetAllInvoiceItems();

    Task<InvoiceItem?> GetInvoiceItemById(int id);

    Task<InvoiceItem> CreateInvoiceItem(
        InvoiceItem invoiceItem);

    Task UpdateInvoiceItem(
        InvoiceItem invoiceItem);

    Task DeleteInvoiceItem(int id);
}