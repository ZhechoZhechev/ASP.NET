namespace Homies.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Homies.Data.Entities;

public class HomiesDbContext : IdentityDbContext
{
    public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<EventParticipant> EventsParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Type>()
            .HasData(new Type()
            {
                Id = 1,
                Name = "Animals"
            },
            new Type()
            {
                Id = 2,
                Name = "Fun"
            },
            new Type()
            {
                Id = 3,
                Name = "Discussion"
            },
            new Type()
            {
                Id = 4,
                Name = "Work"
            });

        modelBuilder.Entity<EventParticipant>()
            .HasKey(pk => new { pk.EventId, pk.HelperId });

        modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Event)
            .WithMany(e => e.EventsParticipants)
            .HasForeignKey(ep => ep.EventId)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder);
    }
}