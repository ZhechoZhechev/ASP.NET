using BusStation.Core.Contracts;
using BusStation.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusStation.Controllers;

public class TicketsController : Controller
{
    private readonly ITicketService ticketService;

    public TicketsController(ITicketService ticketService)
    {
        this.ticketService = ticketService;
    }

    [HttpGet]
    public IActionResult Create()
    {

        var model = new CreateTicketViewModel();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int id, CreateTicketViewModel model) 
    {
        await ticketService.AddTicketToDestination(id, model);

        return RedirectToAction("All", "Destinations");
    }
}
