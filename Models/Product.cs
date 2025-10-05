namespace MaillotStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrl2 { get; set; } // New field
        public string? ImageUrl3 { get; set; } // New field
        public int Stock { get; set; }
        public string? Season { get; set; } // This will store the combined season, e.g., "HOME (2025/2026)"
        public string? Sizes { get; set; }
        public bool IsFeatured { get; set; }
    }
}