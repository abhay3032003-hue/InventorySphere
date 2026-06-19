using System;

namespace InventoryManagement.API.Models
{
    // Invoice Table
    public class Invoice
    {
        public int InvoiceId { get; set; }                 // Primary Key
        public int CustomerId { get; set; }                // Foreign Key
        public Customer? Customer { get; set; }             // Navigation Property
        public DateTime Date { get; set; } = DateTime.Now;

        // Navigation property (One Invoice -> Many InvoiceItems)
        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
