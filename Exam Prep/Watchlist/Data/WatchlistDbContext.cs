namespace Watchlist.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Watchlist.Data.Entities;
using static Constants.DataConstants.UserConstants;
public class WatchlistDbContext : IdentityDbContext<User>
{
    public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options)
        : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Genre>()
            .HasData(new Genre()
            {
                Id = 1,
                Name = "Action"
            },
            new Genre()
            {
                Id = 2,
                Name = "Comedy"
            },
            new Genre()
            {
                Id = 3,
                Name = "Drama"
            },
            new Genre()
            {
                Id = 4,
                Name = "Horror"
            },
            new Genre()
            {
                Id = 5,
                Name = "Romantic"
            });

        builder.Entity<UserMovie>()
            .HasKey(pk => new { pk.UserId, pk.MovieId });

        builder.Entity<User>()
            .Property(p => p.UserName)
            .HasMaxLength(UserUserNameMaxLength)
            .IsRequired(true);

        builder.Entity<User>()
            .Property(p => p.Email)
            .HasMaxLength(UserEmailMaxLength)
            .IsRequired(true);

        base.OnModelCreating(builder);
    }
}