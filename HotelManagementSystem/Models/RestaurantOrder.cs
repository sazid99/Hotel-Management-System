using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public enum OrderStatus
    {
        Pending,
        Preparing,
        Served,
        Cancelled
    }

    public class RestaurantOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Guest name is required")]
        [StringLength(100)]
        public string? GuestName { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        public string? RoomNumber { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        public string? ItemName { get; set; }

        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public decimal TotalPrice => Quantity * Price;

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
    }
}