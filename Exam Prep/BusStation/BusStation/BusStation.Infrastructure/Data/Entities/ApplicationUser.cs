namespace BusStation.Infrastructure.Data.Entities;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
}
