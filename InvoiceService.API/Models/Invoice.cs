using System;

namespace InvoiceService.API.Models
{
    // Invoice Table
    public class Invoice
    {
        public int InvoiceId { get; set; }                 // Primary Key
        public int CustomerId { get; set; }                // Foreign Key            // Navigation Property
        public DateTime Date { get; set; } = DateTime.Now;

        // Navigation property (One Invoice -> Many InvoiceItems)
    }
}
