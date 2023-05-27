using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TextSplitterApp.Models;
using TextSplitterApp.Models.TextModels;

namespace TextSplitterApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;



        public IActionResult Index(TextViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Split(TextViewModel model)
        {
            var splitTextArray = model.Text
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            model.SplitText = string.Join(Environment.NewLine, splitTextArray);

            return View("Index", model);
        }
    }
}