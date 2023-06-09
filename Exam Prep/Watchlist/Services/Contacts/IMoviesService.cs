namespace Watchlist.Services.Contacts;

using Watchlist.Data.Entities;
using Watchlist.Models;

public interface IMoviesService
{
    Task<IEnumerable<MovieVIewModel>> GetAllAsync();

    Task<IEnumerable<Genre>> GetAllGenresAsync();

    Task AddMovieAsync(AddMovieModel model);

    Task AddToCollectionAsync(int movieId, string userId);

    Task RemoveFromCollectionAsync(int movieId, string userId);

    Task<IEnumerable<MovieVIewModel>> GetWatchedAsync(string userId);
}
