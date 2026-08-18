using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        public string? IdentityDocumentNumber { get; set; }
        public string? Preferences { get; set; }
    }
}