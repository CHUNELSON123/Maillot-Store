namespace MaillotStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; } // <-- Add this new line
        public string? ImageUrl { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public int Stock { get; set; }
        public string? Season { get; set; }
        public string? Sizes { get; set; }
        public bool IsFeatured { get; set; }
    }
}