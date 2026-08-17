using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class CheckInOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
