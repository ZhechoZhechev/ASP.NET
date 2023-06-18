using Homies.Contracts;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Homies.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        public async Task<IActionResult> All()
        {
            var model = await eventService.GetAllEventsAsync();

            return View(model);
        }

        public async Task<IActionResult> Add() 
        {
            var model = new AddEventViewModel()
            {
                Types = await eventService.GetAllTypessAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddEventViewModel model) 
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //if (!ModelState.IsValid) 
            //{
            //    return View(model);
            //}

            try
            {
                await eventService.AddEventAsync(model, userId);
                return RedirectToAction("Event", "All");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Join(int id) 
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await eventService.JoinAnEvent(id, userId);
                return RedirectToAction("Joined", "Event");
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Leave(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await eventService.LeaveAnEvent(id, userId);
                return RedirectToAction("All", "Event");
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Joined() 
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var model = await eventService.GetMyEventsAsync(userId);
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Edit(int id) 
        {
            var model = await eventService.GetSingleEvent(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddEventViewModel model) 
        {


            try
            {
                await eventService.UpdateContactAsync(id, model);
            }
            catch
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}
