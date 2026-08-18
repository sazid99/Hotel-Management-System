using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class GuestController : Controller
    {
        private static List<Guest> guests = new List<Guest>
        {
            new Guest { Id = 1, Name = "John Doe", Email = "john@gmail.com", Phone = "01711111111", IdentityDocumentNumber = "NID-12345", Preferences = "High Floor" }
        };

        public IActionResult Index(string search)
        {
            var result = string.IsNullOrEmpty(search)
                ? guests
                : guests.Where(g => g.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || g.Phone.Contains(search)).ToList();
            return View(result);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Guest guest)
        {
            if (ModelState.IsValid)
            {
                guest.Id = guests.Count + 1;
                guests.Add(guest);
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        public IActionResult Details(int id)
        {
            var guest = guests.FirstOrDefault(g => g.Id == id);
            if (guest == null) return NotFound();
            return View(guest);
        }
    }
}