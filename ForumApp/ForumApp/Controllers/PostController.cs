namespace ForumApp.Controllers;

using Microsoft.AspNetCore.Mvc;

using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class PostController : Controller
{
    private readonly IPostService postService;

    public PostController(IPostService postService)
    {
        this.postService = postService;
    }

    public async Task<IActionResult> Index()
    {
        var models = await postService.GetAllAsync();
        return View(models);
    }

    [HttpGet]
    public IActionResult Add() 
    {
        return View(new PostFormModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(PostFormModel model) 
    {
        if (!ModelState.IsValid) 
        {
            return View(model);
        }

        await postService.AddAsync(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id) 
    {
        var viewModel = await postService.GetByIdAsync(id);

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PostFormModel viewModel) 
    {
        if (!ModelState.IsValid) 
        {
            return View(viewModel);
        }

        await postService.UpdateAsync(id, viewModel);

        return RedirectToAction(nameof(Index));
    }

}
