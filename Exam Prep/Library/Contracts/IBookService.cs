namespace Library.Contracts;

using Library.Models;

public interface IBookService
{
    Task<IEnumerable<BookAllViewModel>> GetAllBooksAsync();
}
