using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class CheckInOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckIn(CheckInOutViewModel model)
        {
            if (!model.IdVerified)
            {
                ViewBag.Message = "Guest ID verification is required.";
                return View("Index", model);
            }

            if (string.IsNullOrEmpty(model.RoomNumber))
            {
                ViewBag.Message = "Please assign a room.";
                return View("Index", model);
            }

            model.RoomKeyIssued = true;

            if (model.EarlyCheckIn)
            {
                ViewBag.Message =
                    "Early Check-In approved. Room assigned and key/access issued.";
            }
            else
            {
                ViewBag.Message =
                    "Check-In successful. Room assigned and key/access issued.";
            }

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult CheckOut(CheckInOutViewModel model)
        {
            if (model.LateCheckOut)
            {
                ViewBag.Message =
                    "Late Check-Out processed. Room status updated to Available.";
            }
            else
            {
                ViewBag.Message =
                    "Check-Out successful. Room status updated to Available.";
            }

            return View("Index", model);
        }
    }
}