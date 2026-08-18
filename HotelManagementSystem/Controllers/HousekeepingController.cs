using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class HousekeepingController : Controller
    {
        private static List<HousekeepingTask> tasks = new List<HousekeepingTask>
        {
            new HousekeepingTask { Id = 1, RoomNumber = "101", CleaningStatus = "Dirty", AssignedStaff = "Rahim", MaintenanceNotes = "AC Filter check needed" },
            new HousekeepingTask { Id = 2, RoomNumber = "102", CleaningStatus = "Clean", AssignedStaff = "Karim" }
        };

        public IActionResult Index()
        {
            return View(tasks);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(HousekeepingTask task)
        {
            if (ModelState.IsValid)
            {
                task.Id = tasks.Count + 1;
                tasks.Add(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        public IActionResult UpdateStatus(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost]
        public IActionResult UpdateStatus(HousekeepingTask task)
        {
            var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask != null)
            {
                existingTask.CleaningStatus = task.CleaningStatus;
                existingTask.AssignedStaff = task.AssignedStaff;
                existingTask.MaintenanceNotes = task.MaintenanceNotes;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}