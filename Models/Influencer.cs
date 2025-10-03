namespace MaillotStore.Models
{
    public class Influencer
    {
        public int InfluencerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public decimal CommissionRate { get; set; } // e.g., 0.10 for 10%
    }
}