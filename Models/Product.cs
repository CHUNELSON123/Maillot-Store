namespace MaillotStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
        public string? Season { get; set; } // To store values like "HOME(2025/2026)"
        public string? Sizes { get; set; } // To store available sizes, e.g., "S, M, L, XL"
    }
}