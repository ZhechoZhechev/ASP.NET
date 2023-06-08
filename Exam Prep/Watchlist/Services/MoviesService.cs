namespace Watchlist.Services;

using Microsoft.EntityFrameworkCore;
using Watchlist.Data;
using Watchlist.Data.Entities;
using Watchlist.Models;
using Watchlist.Services.Contacts;

public class MoviesService : IMoviesService
{
    private readonly WatchlistDbContext context;

    public MoviesService(WatchlistDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<MovieVIewModel>> GetAllAsync()
    {
        return await context.Movies
            .AsNoTracking()
            .Include(t => t.Genre)
            .Select(m => new MovieVIewModel
            {
                Id = m.Id,
                ImageUrl = m.ImageUrl,
                Title = m.Title,
                Director = m.Director,
                Rating = m.Rating,
                Genre = m.Genre.Name
            })
            .OrderBy(m => m.Title)
            .ToListAsync();
    }

    public async Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        return await context.Genres.AsNoTracking().ToListAsync();
    }

    public async Task AddMovieAsync(AddMovieModel model)
    {
        var modelToAdd = new Movie
        {
            Title = model.Title,
            Director = model.Director,
            Rating = model.Rating,
            ImageUrl = model.ImageUrl,
            GenreId = model.GenreId
        };

        context.Movies.Add(modelToAdd);
        await context.SaveChangesAsync();
    }
}
