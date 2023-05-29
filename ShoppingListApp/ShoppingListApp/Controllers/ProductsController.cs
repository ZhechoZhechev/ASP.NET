namespace ShoppingListApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingListApp.Core.Contracts;
using ShoppingListApp.Core.Models;

public class ProductsController : Controller
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var allProducts = await productService.GetAllAsync();

        return View(allProducts);
    }

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await productService.AddAsync(model);

        return RedirectToAction("All");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id) 
    {
        var modelToEdit = await productService.GetByIdAsync(id);

        return View(modelToEdit);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductViewModel model) 
    {
        if (!ModelState.IsValid) 
        {
            return View(model);
        }
        await productService.UpdateAsync(id,model);

        return RedirectToAction("All");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id) 
    {
        await productService.DeleteAsync(id);

        return RedirectToAction("All");
    }

}
