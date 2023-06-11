namespace Library.Controllers;

using Microsoft.AspNetCore.Mvc;

using Library.Contracts;

public class BookController : Controller
{
    private readonly IBookService bookService;

    public BookController(IBookService bookService)
    {
        this.bookService = bookService;
    }

    public async Task<IActionResult> All()
    {
        var model = await bookService.GetAllBooksAsync();

        return View(model);
    }
}
