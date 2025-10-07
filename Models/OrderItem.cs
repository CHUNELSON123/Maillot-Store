namespace MaillotStore.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price of the product at the time of order

        // New properties for customization
        public string Size { get; set; }
        public string? CustomName { get; set; }
        public int? CustomNumber { get; set; }
    }
}