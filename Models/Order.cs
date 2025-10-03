namespace MaillotStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // e.g., "Pending", "Completed", "Cancelled"
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}