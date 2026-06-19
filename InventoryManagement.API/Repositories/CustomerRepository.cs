using InventoryManagement.API.Data;
using InventoryManagement.API.Interfaces;
using InventoryManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.API.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly InventoryContext _context;

    public CustomerRepository(InventoryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<Customer> CreateAsync(Customer customer)
    {
        _context.Customers.Add(customer);

        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task UpdateAsync(Customer customer)
    {
        _context.Customers.Update(customer);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _context.Customers.Remove(customer);

        await _context.SaveChangesAsync();
    }
}