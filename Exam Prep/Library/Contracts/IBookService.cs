namespace Library.Contracts;

using Library.Data.Entities;
using Library.Models;

public interface IBookService
{
    Task<IEnumerable<BookAllViewModel>> GetAllBooksAsync();

    Task<IEnumerable<CategoriesViewModel>> GetAllCategoriesAsync();

    Task AdBookAsync(AddBookViewModel model);

    Task AddToCollectionAsync(int bookId, string userId);

    Task RemoveFromCollectionAsync(int bookId, string userId);

    Task<IEnumerable<BookAllViewModel>> GetMyBooksAsync(string UserId);

    Task<AddBookViewModel> GetBookById(int bookId);

    Task UpdateBookAsync(int id, AddBookViewModel model);
}
