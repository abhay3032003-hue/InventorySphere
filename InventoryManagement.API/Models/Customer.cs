namespace InventoryManagement.API.Models
{
    // Customer Table
    public class Customer
    {
        public int CustomerId { get; set; }                // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = "";
        // Navigation property (One Customer -> Many Invoices)
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
