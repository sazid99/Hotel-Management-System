using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class HousekeepingController : Controller
    {
        private static List<HousekeepingTask> tasks = new List<HousekeepingTask>
        {
            new HousekeepingTask { Id = 1, RoomNumber = "101", CleaningStatus = "Dirty", TaskType = "Cleaning", AssignedStaff = "Rahim", MaintenanceNotes = "AC Filter check needed" },
            new HousekeepingTask { Id = 2, RoomNumber = "102", CleaningStatus = "Clean", TaskType = "Cleaning", AssignedStaff = "Karim" }
        };

        // GET: Housekeeping/Index?statusFilter=
        public IActionResult Index(string statusFilter)
        {
            var result = string.IsNullOrEmpty(statusFilter)
                ? tasks
                : tasks.Where(t => t.CleaningStatus == statusFilter).ToList();

            ViewBag.StatusFilter = statusFilter;
            return View(result);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(HousekeepingTask task)
        {
            if (ModelState.IsValid)
            {
                task.Id = tasks.Count == 0 ? 1 : tasks.Max(t => t.Id) + 1;
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

                if (existingTask.CleaningStatus == "Clean")
                {
                    existingTask.IsCompleted = true;
                    existingTask.CompletedDate = DateTime.Now;
                }
                else
                {
                    existingTask.IsCompleted = false;
                    existingTask.CompletedDate = null;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Housekeeping/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null) tasks.Remove(task);
            return RedirectToAction(nameof(Index));
        }
    }
}