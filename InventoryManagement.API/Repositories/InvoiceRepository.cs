using InventoryManagement.API.Data;
using InventoryManagement.API.Interfaces;
using InventoryManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly InventoryContext _context;

    public InvoiceRepository(InventoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(i => i.Customer)
            .ToListAsync();
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        return await _context.Invoices
            .Include(i => i.Customer)
            .FirstOrDefaultAsync(i => i.InvoiceId == id);
    }

    public async Task<Invoice> CreateAsync(Invoice invoice)
    {
        _context.Invoices.Add(invoice);

        await _context.SaveChangesAsync();

        return invoice;
    }

    public async Task UpdateAsync(Invoice invoice)
    {
        _context.Invoices.Update(invoice);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Invoice invoice)
    {
        _context.Invoices.Remove(invoice);

        await _context.SaveChangesAsync();
    }
}