using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementSystem.Controllers
{
    public class RestaurantController : Controller
    {
        // Temporary in-memory storage — replace with DbContext (EF Core) later
        private static List<RestaurantOrder> _orders = new List<RestaurantOrder>
        {
            new RestaurantOrder
            {
                Id = 1,
                GuestName = "Rahim Uddin",
                RoomNumber = "204",
                ItemName = "Chicken Biryani",
                Quantity = 2,
                Price = 350,
                OrderDate = DateTime.Now.AddMinutes(-30),
                Status = OrderStatus.Preparing
            },
            new RestaurantOrder
            {
                Id = 2,
                GuestName = "Karim Ahmed",
                RoomNumber = "310",
                ItemName = "Club Sandwich",
                Quantity = 1,
                Price = 250,
                OrderDate = DateTime.Now.AddMinutes(-10),
                Status = OrderStatus.Pending
            }
        };

        private static int _nextId = 3;

        // GET: /Restaurant
        public IActionResult Index()
        {
            return View(_orders);
        }

        // GET: /Restaurant/Order
        [HttpGet]
        public IActionResult Order()
        {
            return View(new RestaurantOrder());
        }

        // POST: /Restaurant/Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order(RestaurantOrder order)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            order.Id = _nextId++;
            _orders.Add(order);

            TempData["SuccessMessage"] = "Order placed successfully!";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Restaurant/UpdateStatus/5
        [HttpPost]
        public IActionResult UpdateStatus(int id, OrderStatus status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.Status = status;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: /Restaurant/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                _orders.Remove(order);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}