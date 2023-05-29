namespace ShoppingListApp.Core.Contracts;

using Microsoft.EntityFrameworkCore.Diagnostics;
using ShoppingListApp.Core.Models;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllAsync();

    Task AddAsync(ProductViewModel productViewModel);

    Task UpdateAsync(int id,ProductViewModel productViewModel);

    Task DeleteAsync(int id);

    Task<ProductViewModel> GetByIdAsync(int id);
}
