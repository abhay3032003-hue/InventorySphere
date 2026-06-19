using InventoryManagement.API.Data;
using InventoryManagement.API.Interfaces;
using InventoryManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Repositories;

public class InvoiceItemRepository : IInvoiceItemRepository
{
    private readonly InventoryContext _context;

    public InvoiceItemRepository(InventoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InvoiceItem>> GetAllAsync()
    {
        return await _context.InvoiceItems
            .Include(i => i.Invoice)
            .Include(i => i.Product)
            .ToListAsync();
    }

    public async Task<InvoiceItem?> GetByIdAsync(int id)
    {
        return await _context.InvoiceItems
            .Include(i => i.Invoice)
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.InvoiceItemId == id);
    }

    public async Task<InvoiceItem> CreateAsync(
        InvoiceItem invoiceItem)
    {
        _context.InvoiceItems.Add(invoiceItem);

        await _context.SaveChangesAsync();

        return invoiceItem;
    }

    public async Task UpdateAsync(
        InvoiceItem invoiceItem)
    {
        _context.InvoiceItems.Update(invoiceItem);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        InvoiceItem invoiceItem)
    {
        _context.InvoiceItems.Remove(invoiceItem);

        await _context.SaveChangesAsync();
    }
}