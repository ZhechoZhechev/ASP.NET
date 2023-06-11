namespace Library.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Library.Contracts;
using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private readonly LibraryDbContext context;

    public BookService(LibraryDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<BookAllViewModel>> GetAllBooksAsync()
    {
        var model = await context.Books
            .AsNoTracking()
            .Select(b => new BookAllViewModel()
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Category = b.Category.Name,
                ImageUrl = b.ImageUrl,
                Rating = b.Rating
            })
            .OrderBy(b => b.Title)
            .ToListAsync();

        return model;
    }
}
