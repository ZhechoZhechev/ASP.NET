using BusStation.Core.Contracts;
using BusStation.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly IDestinationService destinationService;

        public DestinationsController(IDestinationService destinationService)
        {
            this.destinationService = destinationService;
        }

        public async Task<IActionResult> All()
        {
            var allDestinations = await destinationService.GetAllDestinations();
            return View(allDestinations);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            var model = new AddDestinationViewModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddDestinationViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            try
            {
                await destinationService.AddDestination(model);
                return RedirectToAction("All", "Destinations");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
