namespace InventoryManagement.API.Models
{
    // InvoiceItem Table
    public class InvoiceItem
    {
        public int InvoiceItemId { get; set; }             // Primary Key
        public int InvoiceId { get; set; }                 // Foreign Key
        public Invoice? Invoice { get; set; }               // Navigation Property
        public int ProductId { get; set; }                 // Foreign Key
        public Product? Product { get; set; }               // Navigation Property
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
