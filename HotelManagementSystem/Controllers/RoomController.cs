using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class RoomController : Controller
    {
        // Temporary room data
        // Database ছাড়া এখন এই list-এ room থাকবে
        private static List<Room> rooms = new List<Room>
        {
            new Room
            {
                Id = 1,
                RoomNumber = "101",
                RoomType = "Single",
                Status = "Available",
                PricePerNight = 2500,
                Amenities = "WiFi, AC, TV"
            },

            new Room
            {
                Id = 2,
                RoomNumber = "102",
                RoomType = "Double",
                Status = "Occupied",
                PricePerNight = 4000,
                Amenities = "WiFi, AC, TV, Mini Fridge"
            },

            new Room
            {
                Id = 3,
                RoomNumber = "201",
                RoomType = "Suite",
                Status = "Under Maintenance",
                PricePerNight = 7000,
                Amenities = "WiFi, AC, TV, Balcony"
            }
        };


        // ============================
        // SHOW ALL ROOMS
        // ============================

        public IActionResult Index()
        {
            return View(rooms);
        }


        // ============================
        // CREATE ROOM - GET
        // ============================

        public IActionResult Create()
        {
            return View();
        }


        // ============================
        // CREATE ROOM - POST
        // ============================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                if (rooms.Count == 0)
                {
                    room.Id = 1;
                }
                else
                {
                    room.Id = rooms.Max(r => r.Id) + 1;
                }

                rooms.Add(room);

                return RedirectToAction("Index");
            }

            return View(room);
        }


        // ============================
        // EDIT ROOM - GET
        // ============================

        public IActionResult Edit(int id)
        {
            var room = rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }


        // ============================
        // EDIT ROOM - POST
        // ============================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                var existingRoom =
                    rooms.FirstOrDefault(r => r.Id == room.Id);

                if (existingRoom == null)
                {
                    return NotFound();
                }

                existingRoom.RoomNumber = room.RoomNumber;
                existingRoom.RoomType = room.RoomType;
                existingRoom.Status = room.Status;
                existingRoom.PricePerNight = room.PricePerNight;
                existingRoom.Amenities = room.Amenities;

                return RedirectToAction("Index");
            }

            return View(room);
        }


        // ============================
        // DELETE ROOM - GET
        // ============================

        public IActionResult Delete(int id)
        {
            var room = rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }


        // ============================
        // DELETE ROOM - POST
        // ============================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var room = rooms.FirstOrDefault(r => r.Id == id);

            if (room != null)
            {
                rooms.Remove(room);
            }

            return RedirectToAction("Index");
        }
    }
}