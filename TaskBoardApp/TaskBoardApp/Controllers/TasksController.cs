namespace TaskBoardApp.Controllers;

using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskBoardApp.Data;
using TaskBoardApp.Data.Entities;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;
using Task = Data.Entities.Task;


public class TasksController : Controller
{
    private readonly TaskBoardAppDbContext context;

    private readonly UserManager<User> userManager;

    public TasksController(TaskBoardAppDbContext context, UserManager<User> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult Create()
    {
        var model = new TaskFormModel()
        {
            Boards = GetBoards()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskFormModel model)
    {
        if (!GetBoards().Any(b => b.Id == model.BoardId))
        {
            this.ModelState.AddModelError(nameof(model.BoardId), "Such board does not exist");
        }

        string userId = await GetUserIdAsync();

        var newTask = new Task()
        {
            Title = model.Title,
            Description = model.Description,
            CreatedOn = DateTime.Now,
            BoardId = model.BoardId,
            OwnerId = userId,
        };

        await context.AddAsync(newTask);
        await context.SaveChangesAsync();

        return RedirectToAction("All", "Boards");
    }

    public async Task<IActionResult> Details(int id) 
    {
        var task = await context.Tasks
            .Include(t => t.Board)
            .Include(t => t.Owner)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (task == null) 
        {
            return BadRequest();
        }

        var taskView = new TaskViewDetailsModel()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CreatedOn = task.CreatedOn.ToString("f", CultureInfo.InvariantCulture),
            Board = task.Board.Name,
            Owner = task.Owner.UserName
        };

        return View(taskView);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id) 
    {
        var taksToDelte = await context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (taksToDelte == null) 
            return BadRequest();

        var userId = await GetUserIdAsync();

        if (taksToDelte.OwnerId != userId) 
            return Unauthorized();


        var deleteModel = new TaskViewModel()
        {
            Id = taksToDelte.Id,
            Title = taksToDelte.Title,
            Description = taksToDelte.Description
        };

        return View(deleteModel);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(TaskViewModel taskViewModel) 
    {
        var taksToDelte = await context.Tasks
    .AsNoTracking()
    .FirstOrDefaultAsync(x => x.Id == taskViewModel.Id);

        if (taksToDelte == null)
            return BadRequest();

        var userId = await GetUserIdAsync();

        if (taksToDelte.OwnerId != userId)
            return Unauthorized();

         context.Tasks.Remove(taksToDelte);
        await context.SaveChangesAsync();

        return RedirectToAction("All", "Boards");
    }

    private IEnumerable<TaskBoardModel> GetBoards()
    {
        var boards = context.Boards
            .AsNoTracking()
            .Select(x => new TaskBoardModel() 
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();
        return boards;
    }

    private async Task<string> GetUserIdAsync()
    {
        var user = await GetCurrentUserAsync();
        var userId = user.Id;
        return userId;
    }

    private Task<User> GetCurrentUserAsync()
        => userManager.GetUserAsync(HttpContext.User);
}

