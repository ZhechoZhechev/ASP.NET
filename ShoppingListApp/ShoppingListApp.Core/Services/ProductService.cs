namespace ShoppingListApp.Core.Services;

using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Core.Contracts;
using ShoppingListApp.Core.Models;
using ShoppingListApp.Infrastructure;
using ShoppingListApp.Infrastructure.Data;

public class ProductService : IProductService
{
    private readonly AppDbContext context;

    public ProductService(AppDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(ProductViewModel productViewModel)
    {
        var productToadd = new Product()
        {
            Name = productViewModel.Name,
            Quantity = productViewModel.Quantity
        };

        await context.AddAsync(productToadd);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var modelToDelete = await context.Products.FindAsync(id);
        context.Remove(modelToDelete);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
    {
        return await context.Products
            .AsNoTracking()
            .Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity
            })
            .OrderBy(p => p.Name)
            .ThenBy(p => p.Quantity)
            .ToListAsync();

    }

    public async Task<ProductViewModel> GetByIdAsync(int id)
    {
        var model = await context.Products.FindAsync(id);

        return new ProductViewModel()
        {
            Name = model.Name,
            Quantity = model.Quantity
        };
    }


    public async Task UpdateAsync(int id, ProductViewModel productViewModel)
    {
        var entityToUpdate = await context.Products.FindAsync(id);

        entityToUpdate.Name = productViewModel.Name;
        entityToUpdate.Quantity = productViewModel.Quantity;

        await context.SaveChangesAsync();
    }
}
