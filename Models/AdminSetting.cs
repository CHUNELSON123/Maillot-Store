using System.ComponentModel.DataAnnotations;

namespace MaillotStore.Models
{
    public class AdminSetting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Key { get; set; } // e.g., "AdminWhatsAppNumber"

        [Required]
        public string Value { get; set; } // e.g., "+237..."
    }
}