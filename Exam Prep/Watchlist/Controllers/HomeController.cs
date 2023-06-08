namespace Watchlist.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {

        if (User?.Identity?.IsAuthenticated ?? false) 
        {
            RedirectToAction("All", "Movies");
        }

        return View();
    }
}