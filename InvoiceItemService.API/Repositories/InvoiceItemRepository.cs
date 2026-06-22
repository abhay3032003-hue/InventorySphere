using InvoiceItemService.API.Data;
using InvoiceItemService.API.Interfaces;
using InvoiceItemService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceItemService.API.Repositories;

public class InvoiceItemRepository : IInvoiceItemRepository
{
    private readonly InvoiceItemDbContext _context;

    public InvoiceItemRepository(
        InvoiceItemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InvoiceItem>> GetAllAsync()
    {
        return await _context.InvoiceItems.ToListAsync();
    }

    public async Task<InvoiceItem?> GetByIdAsync(int id)
    {
        return await _context.InvoiceItems.FindAsync(id);
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