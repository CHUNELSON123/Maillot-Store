namespace MaillotStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public string? ReferralCode { get; set; }

        public string Status { get; set; } = "Pending";
    }
}