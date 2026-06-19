using Microsoft.EntityFrameworkCore;
using InventoryManagement.API.Models;
using InventoryManagement.API.Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace InventoryManagement.API.Data;

public class InventoryContext : IdentityDbContext<ApplicationUser>
{
    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
}