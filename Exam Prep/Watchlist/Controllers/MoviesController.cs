using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Models;
using Watchlist.Services.Contacts;

namespace Watchlist.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService moviesService;

        public MoviesController(IMoviesService moviesService)
        {
            this.moviesService = moviesService;
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
    }
}
