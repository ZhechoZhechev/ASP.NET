namespace BusStation.Core.Services;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using BusStation.Core.Contracts;
using BusStation.Core.Models;
using BusStation.Infrastructure.Data;
using BusStation.Infrastructure.Data.Entities;

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
}
