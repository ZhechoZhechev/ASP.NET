namespace Library.Services;

using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

using Library.Contracts;
using Library.Data;
using Library.Data.Entities;
using Library.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

public class BookService : IBookService
{
    private readonly LibraryDbContext context;

    public BookService(LibraryDbContext context)
    {
        this.context = context;
    }

    public async Task AdBookAsync(AddBookViewModel model)
    {
        var book = new Book()
        {
            Title = model.Title,
            Author = model.Author,
            Description = model.Description,
            ImageUrl = model.Url,
            Rating = model.Rating,
            CategoryId = model.CategoryId
        };

        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public async Task AddToCollectionAsync(int bookId, string userId)
    {
        if (!context.IdentityUsersBooks.Any(x => x.BookId == bookId && x.CollectorId == userId))
        {
            var userBook = new IdentityUserBook()
            {
                BookId = bookId,
                CollectorId = userId,
            };

            await context.IdentityUsersBooks.AddAsync(userBook);
            await context.SaveChangesAsync();
        }
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

    public async Task<IEnumerable<CategoriesViewModel>> GetAllCategoriesAsync()
    {
        var model = await context.Categories
            .AsNoTracking()
            .Select(c => new CategoriesViewModel() 
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToArrayAsync();

        return model;
    }

    public async Task<AddBookViewModel> GetBookById(int bookId)
    {
        var book = await context.Books
            .FirstOrDefaultAsync(b => b.Id == bookId);

        if (book != null)
        {
            var model = new AddBookViewModel() 
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Rating = book.Rating,
                Categories = await GetAllCategoriesAsync(),
                Url = book.ImageUrl
            };

            return model;
        }

        return new AddBookViewModel();
    }

    public async Task<IEnumerable<BookAllViewModel>> GetMyBooksAsync(string userId)
    {
        var userBooks = await context.Books
            .Include(ub => ub.UsersBooks)
            .Include(c => c.Category)
            .Where(x => x.UsersBooks.Any(u => u.CollectorId == userId))
            .ToListAsync();

        var models = userBooks
            .Select(x => new BookAllViewModel()
            {
                Id = x.Id,
                Author = x.Author,
                Category = x.Category.Name,
                ImageUrl = x.ImageUrl,
                Rating = x.Rating,
                Title = x.Title,
                Description = x.Description
            });

        return models;
    }

    public async Task RemoveFromCollectionAsync(int bookId, string userId)
    {
        var bookToRemove = await context.IdentityUsersBooks
            .FirstOrDefaultAsync(x => x.BookId == bookId && x.CollectorId == userId);

        if (bookToRemove != null) 
        {
            context.IdentityUsersBooks.Remove(bookToRemove);
            await context.SaveChangesAsync();
        }

    }

    public async Task UpdateBookAsync(int id, AddBookViewModel model)
    {
        var modelToUpadte = await context.Books.FirstOrDefaultAsync(x => x.Id == id);

        modelToUpadte.Title = model.Title;
        modelToUpadte.Author = model.Author;
        modelToUpadte.Description = model.Description;
        modelToUpadte.Rating = model.Rating;
        modelToUpadte.CategoryId = model.CategoryId;
        modelToUpadte.ImageUrl = model.Url;

        await context.SaveChangesAsync();
    }
}
