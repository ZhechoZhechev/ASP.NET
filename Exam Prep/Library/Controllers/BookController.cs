namespace Library.Controllers;

using Microsoft.AspNetCore.Mvc;

using Library.Contracts;
using Library.Models;
using Microsoft.AspNetCore.Identity;

public class BookController : Controller
{
    private readonly IBookService bookService;
    private readonly UserManager<IdentityUser> userManager;

    public BookController(IBookService bookService, UserManager<IdentityUser> userManager)
    {
        this.bookService = bookService;
        this.userManager = userManager;
    }

    public async Task<IActionResult> All()
    {
        var model = await bookService.GetAllBooksAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Add() 
    {
        var model = new AddBookViewModel()
        {
            Categories = await bookService.GetAllCategoriesAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddBookViewModel model) 
    {
        if (!ModelState.IsValid) 
        {
            return View(model);
        }

        await bookService.AdBookAsync(model);

        return RedirectToAction("All", "Book");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCollection(int Id) 
    {
        var userId = GetUserId();

        await bookService.AddToCollectionAsync(Id, userId);

        return RedirectToAction("All", "Book");
    }

    public async Task<IActionResult> Mine()
    {
        var userId = GetUserId();
        var model = await bookService.GetMyBooksAsync(userId);

        return View(model);
    }

    public async Task<IActionResult> RemoveFromCollection(int id) 
    {
        var userId = GetUserId();

        await bookService.RemoveFromCollectionAsync(id, userId);

        return RedirectToAction("Mine", "Book");
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id) 
    {
        var model = await bookService.GetBookById(id);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AddBookViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await bookService.UpdateBookAsync(id, viewModel);

        return RedirectToAction("All", "Book");
    }

    private string GetUserId() 
    {
        return userManager.GetUserId(HttpContext.User);
    }
}
