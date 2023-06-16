namespace BusStation.Core.Services;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using BusStation.Core.Contracts;
using BusStation.Core.Models;
using BusStation.Infrastructure.Data;
using BusStation.Infrastructure.Data.Entities;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class TicketService : ITicketService
{
    private readonly ApplicationDbContext context;

    public TicketService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task AddTicketToDestination(int id, CreateTicketViewModel model)
    {

        var destination = await context.Destinations
            .FirstOrDefaultAsync(x => x.Id == id);

        for (int i = 1; i <= model.TicketsCount; i++)
        {
            var ticket = new Ticket()
            {
                Price = model.Price
            };

            destination!.Tickets.Add(ticket);
        }

        await context.SaveChangesAsync();
    }

    public async Task BookATripForUser(int id, string userId)
    {
        var user = await context.Users
            .Include(u => u.Tickets)
            .FirstOrDefaultAsync(u => u.Id == userId);

        var destination = await context.Destinations
            .Include(d => d.Tickets)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (user != null && destination != null)
        {
            var ticket = destination.Tickets.FirstOrDefault();

            if (ticket != null)
            {
                ticket.DestinationId = id;
                user.Tickets.Add(ticket);
                destination.Tickets.Remove(ticket);
            }
            else
            {
                throw new InvalidCastException();
            }

            await context.SaveChangesAsync();

        }
    }

    public async Task<IEnumerable<MyTicketsViewModel>> GetMytickets(string userId)
    {
        var tickets = await context.Tickets
            .Include(u => u.User)
            .Include(d => d.Destination)
            .Where(x => x.UserId == userId)
            .Select(x => new MyTicketsViewModel()
            {
                Price = x.Price,
                UserId = x.UserId,
                DestinationId = x.DestinationId,
                User = x.User,
                Destination = x.Destination

            })
            .ToListAsync();

        return tickets;
    }
}
