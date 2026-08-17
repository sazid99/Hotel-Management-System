namespace HotelManagementSystem.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string? GuestName { get; set; }
        public string? Email { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }
}