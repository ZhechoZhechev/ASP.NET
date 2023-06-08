namespace Watchlist.Services.Contacts;

using Watchlist.Data.Entities;
using Watchlist.Models;

public interface IMoviesService
{
    Task<IEnumerable<MovieVIewModel>> GetAllAsync();

    Task<IEnumerable<Genre>> GetAllGenresAsync();

    Task AddMovieAsync(AddMovieModel model);
}
