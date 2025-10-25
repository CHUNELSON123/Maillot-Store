namespace MaillotStore.Models
{
    public class CommissionSetting
    {
        // This will only ever have one row
        public int Id { get; set; }
        public decimal CurrentRate { get; set; } // Stored as 0.07 for 7%
    }
}