using Microsoft.AspNetCore.Authorization;
namespace Watchlist.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Watchlist.Data.Entities;
using Watchlist.Models;
using Watchlist.Services.Contacts;

public class MoviesController : Controller
{
    private readonly IMoviesService moviesService;
    private readonly UserManager<User> userManager;

    public MoviesController(IMoviesService moviesService, UserManager<User> userManager)
    {
        this.moviesService = moviesService;
        this.userManager = userManager;
    }

    [Authorize]
    public async Task<IActionResult> All()
    {
        var model = await moviesService.GetAllAsync();

        return View(model);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddMovieModel
        {
            Genres = await moviesService.GetAllGenresAsync(),
        };

        return View(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add(AddMovieModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await moviesService.AddMovieAsync(model);

            return RedirectToAction("All", "Movies");
        }
        catch (Exception)
        {

            ModelState.AddModelError("", "Something went wrong!");
            return View(model);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToCollection(int movieId)
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        var userId = user.Id;

        await moviesService.AddToCollectionAsync(movieId, userId);

        return RedirectToAction("All", "Movies");
    }

    public async Task<IActionResult> RemoveFromCollection(int movieId) 
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        var userId = user.Id;

        await moviesService.RemoveFromCollectionAsync(movieId, userId);

        return RedirectToAction("Watched", "Movies");
    }

    public async Task<IActionResult> Watched() 
    {
        var user = await userManager.GetUserAsync(HttpContext.User);
        var userId = user.Id;

        var model = await moviesService.GetWatchedAsync(userId);

        return View(model);
    }
}
