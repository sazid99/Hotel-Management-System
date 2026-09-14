using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Models
{
    // NOTE: This is the shared "room" entity used by both the Room Management
    // module and this Booking & Reservation module (Booking.RoomId points here).
    // It was an empty stub before; fleshed out just enough for room search /
    // booking to work. If the Room Management teammate builds RoomController
    // CRUD around a different shape, sync field names with them.
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty; // Single, Double, Suite, Deluxe

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Available"; // Available, Occupied, Under Maintenance

        public string? Amenities { get; set; } // e.g. "WiFi, AC, TV, Mini Bar"

        public int Capacity { get; set; } = 2; // max guests

        // Photo shown on the search/details pages. Placeholder stock photo for
        // now — swap for a real uploaded photo once Room Management supports it.
        public string ImageUrl { get; set; } = "https://placehold.co/600x400?text=Room";
    }
}
