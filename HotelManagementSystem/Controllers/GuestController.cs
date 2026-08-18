using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}