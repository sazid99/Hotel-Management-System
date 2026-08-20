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

        public string? Address { get; set; }

        public string? IdentityDocumentType { get; set; } // NID, Passport, Driving License

        public string? IdentityDocumentNumber { get; set; }

        public string? Preferences { get; set; } // special requirements/notes

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}