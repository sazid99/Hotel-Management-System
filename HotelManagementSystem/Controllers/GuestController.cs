using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class GuestController : Controller
    {
        private static List<Guest> guests = new List<Guest>
        {
            new Guest
            {
                Id = 1,
                Name = "Sazid Md",
                Email = "Sazid@gmail.com",
                Phone = "01711111111",
                Address = "Dhanmondi, Dhaka",
                IdentityDocumentType = "NID",
                IdentityDocumentNumber = "NID-12345",
                Preferences = "High Floor",
                CreatedAt = DateTime.Now
            },
            new Guest
            {
                Id = 2,
                Name = "Raihan Mondol",
                Email = "rahim@gmail.com",
                Phone = "01812345678",
                Address = "Gulshan, Dhaka",
                IdentityDocumentType = "Passport",
                IdentityDocumentNumber = "P-98765",
                Preferences = "Sea View",
                CreatedAt = DateTime.Now
            },
            new Guest
            {
                Id = 3,
                Name = "Johir Raihan",
                Email = "Johir@gmail.com",
                Phone = "01922334455",
                Address = "Uttara, Dhaka",
                IdentityDocumentType = "NID",
                IdentityDocumentNumber = "NID-67890",
                Preferences = "Non-Smoking Room",
                CreatedAt = DateTime.Now
            },
            new Guest
            {
                Id = 4,
                Name = "Nusrat Jahan",
                Email = "nusrat@gmail.com",
                Phone = "01633221100",
                Address = "Mirpur, Dhaka",
                IdentityDocumentType = "Driving License",
                IdentityDocumentNumber = "DL-11223",
                Preferences = "Extra Pillow",
                CreatedAt = DateTime.Now
            }
        };

        // GET: Guest/Index?search=
        public IActionResult Index(string search)
        {
            var result = string.IsNullOrEmpty(search)
                ? guests
                : guests.Where(g =>
                    g.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    g.Phone.Contains(search) ||
                    (g.Email != null && g.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                  ).ToList();
            return View(result);
        }

        // GET: Guest/Create
        public IActionResult Create() => View();

        // POST: Guest/Create
        [HttpPost]
        public IActionResult Create(Guest guest)
        {
            if (ModelState.IsValid)
            {
                guest.Id = guests.Count == 0 ? 1 : guests.Max(g => g.Id) + 1;
                guest.CreatedAt = DateTime.Now;
                guests.Add(guest);
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        // GET: Guest/Edit/5
        public IActionResult Edit(int id)
        {
            var guest = guests.FirstOrDefault(g => g.Id == id);
            if (guest == null) return NotFound();
            return View(guest);
        }

        // POST: Guest/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Guest updatedGuest)
        {
            var guest = guests.FirstOrDefault(g => g.Id == id);
            if (guest == null) return NotFound();

            if (ModelState.IsValid)
            {
                guest.Name = updatedGuest.Name;
                guest.Email = updatedGuest.Email;
                guest.Phone = updatedGuest.Phone;
                guest.Address = updatedGuest.Address;
                guest.IdentityDocumentType = updatedGuest.IdentityDocumentType;
                guest.IdentityDocumentNumber = updatedGuest.IdentityDocumentNumber;
                guest.Preferences = updatedGuest.Preferences;
                // CreatedAt change 
                return RedirectToAction(nameof(Index));
            }
            return View(updatedGuest);
        }

        // POST: Guest/Delete/5  (no confirm page, direct delete from Index)
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var guest = guests.FirstOrDefault(g => g.Id == id);
            if (guest != null) guests.Remove(guest);
            return RedirectToAction(nameof(Index));
        }

        // GET: Guest/Details/5
        public IActionResult Details(int id)
        {
            var guest = guests.FirstOrDefault(g => g.Id == id);
            if (guest == null) return NotFound();
            return View(guest);
        }
    }
}