using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
