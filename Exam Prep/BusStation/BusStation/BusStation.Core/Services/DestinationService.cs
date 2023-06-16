namespace BusStation.Core.Services;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

using BusStation.Core.Contracts;
using BusStation.Core.Models;
using BusStation.Infrastructure.Data;
using BusStation.Infrastructure.Data.Entities;
using System.Globalization;

public class DestinationService : IDestinationService
{
    private readonly ApplicationDbContext context;

    public DestinationService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task AddDestination(AddDestinationViewModel destination)
    {
        var destToAdd = new Destination()
        {
            DestinationName = destination.DestinationName,
            Origin = destination.Origin,
            DateTime = destination.DateTime,
            Date = destination.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
            Time = destination.DateTime.ToString("HH:mm", CultureInfo.InvariantCulture),
            ImageUrl = destination.ImageUrl
        };

        await context.Destinations.AddAsync(destToAdd);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AllDestinationsViewModel>> GetAllDestinations()
    {
        return await context.Destinations
            .AsNoTracking()
            .Include(x => x.Tickets)
            .Select(x => new AllDestinationsViewModel()
            {
                Id = x.Id,
                DestinationName = x.DestinationName,
                Origin = x.Origin,
                Date = x.Date,
                Time = x.Time,
                ImageUrl = x.ImageUrl,
                TicketsCount = x.Tickets.Count
            })
            .ToListAsync();
    }
}
