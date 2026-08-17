using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementSystem.Controllers
{
    public class StaffController : Controller
    {
        private static List<Staff> _staffList = new List<Staff>
        {
            new Staff { Id = 1, Name = "Rahim Ahmed", Role = "Manager", Email = "rahim@grandplazaiubat.com", Phone = "+880 1711-223344", Status = "Active" },
            new Staff { Id = 2, Name = "Karim Hossain", Role = "Chef", Email = "karim@grandplazaiubat.com", Phone = "+880 1933-445566", Status = "Inactive" }
        };

        // GET: Staff
        public IActionResult Index()
        {
            return View(_staffList);
        }

        // GET: Staff/Details/5
        public IActionResult Details(int id)
        {
            var staff = _staffList.FirstOrDefault(s => s.Id == id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // GET: Staff/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.Id = _staffList.Any() ? _staffList.Max(s => s.Id) + 1 : 1;
                _staffList.Add(staff);
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staff/Edit/5
        public IActionResult Edit(int id)
        {
            var staff = _staffList.FirstOrDefault(s => s.Id == id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            var existingStaff = _staffList.FirstOrDefault(s => s.Id == id);
            if (existingStaff != null)
            {
                existingStaff.Name = staff.Name;
                existingStaff.Role = staff.Role;
                existingStaff.Email = staff.Email;
                existingStaff.Phone = staff.Phone;
                existingStaff.Status = staff.Status;

                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staff/Delete/5
        public IActionResult Delete(int id)
        {
            var staff = _staffList.FirstOrDefault(s => s.Id == id);
            if (staff != null)
            {
                _staffList.Remove(staff);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}