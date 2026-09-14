using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class CheckInOut
    {
        [Key]
        public int CheckInOutId { get; set; }

        [Required]
        public int ReservationId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public string? Status { get; set; }
    }
}
