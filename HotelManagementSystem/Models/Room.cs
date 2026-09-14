using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Room Type")]
        public string RoomType { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Available";

        [Required]
        [Display(Name = "Price Per Night")]
        public decimal PricePerNight { get; set; }

        public string Amenities { get; set; } = string.Empty;
    }
}