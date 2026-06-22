namespace InvoiceItemService.API.Models
{
    // InvoiceItem Table
    public class InvoiceItem
    {
        public int InvoiceItemId { get; set; }             // Primary Key
        public int InvoiceId { get; set; }                 // Foreign Key
        public int ProductId { get; set; }                 // Foreign Key
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
