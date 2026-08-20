using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class HousekeepingTask
    {
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public string CleaningStatus { get; set; } = "Dirty"; // Clean, Dirty, In-progress

        [Required]
        public string TaskType { get; set; } = "Cleaning"; // Cleaning, Maintenance, Repair

        public string AssignedStaff { get; set; } = "Unassigned";

        public string? MaintenanceNotes { get; set; }

        public DateTime ScheduledDate { get; set; } = DateTime.Now;

        public DateTime? CompletedDate { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}