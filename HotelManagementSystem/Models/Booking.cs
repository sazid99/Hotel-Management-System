using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a guest.")]
        public int GuestId { get; set; }

        [ForeignKey("GuestId")]
        public Guest? Guest { get; set; }

        [Required(ErrorMessage = "Please select a room.")]
        public int RoomId { get; set; }

        [ForeignKey("RoomId")]
        public RoomType? Room { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; } = DateTime.Today;

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; } = DateTime.Today.AddDays(1);

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        // Advance payment / booking deposit collected at reservation time.
        [Column(TypeName = "decimal(18,2)")]
        public decimal AdvancePayment { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled, Modified

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public int Nights => Math.Max((CheckOutDate.Date - CheckInDate.Date).Days, 0);

        [NotMapped]
        public decimal BalanceDue => TotalPrice - AdvancePayment;
    }
}
