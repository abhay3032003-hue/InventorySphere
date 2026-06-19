using InventoryManagement.API.Interfaces;
using InventoryManagement.API.Models;

namespace InventoryManagement.API.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;

    public InvoiceService(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Invoice>> GetAllInvoices()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Invoice?> GetInvoiceById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Invoice> CreateInvoice(Invoice invoice)
    {
        return await _repository.CreateAsync(invoice);
    }

    public async Task UpdateInvoice(Invoice invoice)
    {
        await _repository.UpdateAsync(invoice);
    }

    public async Task DeleteInvoice(int id)
    {
        var invoice = await _repository.GetByIdAsync(id);

        if (invoice != null)
        {
            await _repository.DeleteAsync(invoice);
        }
    }
}