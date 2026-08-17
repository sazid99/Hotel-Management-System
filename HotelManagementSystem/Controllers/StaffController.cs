using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
