namespace BusStation.Core.Models;

using BusStation.Infrastructure.Data.Entities;

public class MyTicketsViewModel
{
    public decimal Price { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int? DestinationId { get; set; }
    public Destination Destination { get; set; }
}
