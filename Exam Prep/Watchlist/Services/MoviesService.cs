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

    public async Task AddToCollectionAsync(int movieId, string userId)
    {
        var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
        var user = await context.Users
            .Include(x => x.UsersMovies)
            .FirstOrDefaultAsync(x => x.Id == userId);

        var objectToAdd = new UserMovie()
        {
            Movie = movie!,
            User = user!
        };

        if (!user.UsersMovies.Any(m => m.MovieId == movieId)) 
        {
            user.UsersMovies.Add(objectToAdd);

            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<MovieVIewModel>> GetWatchedAsync(string userId)
    {
        var user = await context.Users
            .Include(x => x.UsersMovies)
            .ThenInclude(x => x.Movie)
            .ThenInclude(x => x.Genre)
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null)
            throw new ArgumentException("Invalid user Id.");

        return user.UsersMovies
            .Select(m => new MovieVIewModel()
            {
                Id = m.Movie.Id,
                ImageUrl = m.Movie.ImageUrl,
                Title = m.Movie.Title,
                Director = m.Movie.Director,
                Rating = m.Movie.Rating,
                Genre = m.Movie.Genre.Name

            })
            .ToList();
    }

    public async Task RemoveFromCollectionAsync(int movieId, string userId)
    {

        var user = await context.Users
            .Include(x => x.UsersMovies)
            .FirstOrDefaultAsync(x => x.Id == userId);

        var movie = user.UsersMovies.FirstOrDefault(x => x.MovieId == movieId);

        if (movie != null)
        {
            user.UsersMovies.Remove(movie);

            await context.SaveChangesAsync();
        }
    }
}
