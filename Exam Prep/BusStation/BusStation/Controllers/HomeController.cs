using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusStation.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("All", "Destinations");
            }

            return View();
        }
    }
}