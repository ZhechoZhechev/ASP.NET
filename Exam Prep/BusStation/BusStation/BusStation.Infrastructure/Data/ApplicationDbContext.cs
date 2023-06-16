namespace BusStation.Infrastructure.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using BusStation.Infrastructure.Data.Entities;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<ApplicationUser>()
            .Property(p => p.UserName)
            .IsRequired(true)
            .HasMaxLength(20);

        builder.Entity<ApplicationUser>()
            .Property(p => p.Email)
            .IsRequired(true)
            .HasMaxLength(60);

        builder.Entity<Ticket>()
            .Property(p => p.UserId)
            .IsRequired(false);

        builder.Entity<Ticket>()
            .Property(p => p.DestinationId)
            .IsRequired(false);

        base.OnModelCreating(builder);
    }
}