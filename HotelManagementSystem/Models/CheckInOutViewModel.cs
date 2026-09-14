namespace HotelManagementSystem.Models
{
    public class CheckInOutViewModel
    {
        public string ReservationId { get; set; }

        public string GuestName { get; set; }

        public string GuestId { get; set; }

        public string RoomNumber { get; set; }

        public DateTime? CheckInDate { get; set; }

        public DateTime? CheckOutDate { get; set; }

        public bool IdVerified { get; set; }

        public bool EarlyCheckIn { get; set; }

        public bool LateCheckOut { get; set; }

        public bool RoomKeyIssued { get; set; }

        public string Message { get; set; }
    }
}