﻿using BusStation.Core.Contracts;
using BusStation.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    
    public async Task<IActionResult> Reserve(int id) 
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            await ticketService.BookATripForUser(id, userId);
            return RedirectToAction("All", "Destinations");
        }
        catch
        {
            return RedirectToAction("All", "Destinations");
        }
    }

    public async Task<IActionResult> MyTickets() 
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var model = await ticketService.GetMytickets(userId);

        return View(model);
    }
}
