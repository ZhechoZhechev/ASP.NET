namespace TaskBoardApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;

public class BoardsController : Controller
{
    private readonly TaskBoardAppDbContext context;

    public BoardsController(TaskBoardAppDbContext context)
    {
        this.context = context;
    }

    public IActionResult All()
    {
        var boards = context.Boards
             .AsNoTracking()
             .Select(x => new BoardViewModel
             {
                 Id = x.Id,
                 Name = x.Name,
                 Tasks = x.Tasks
                 .Select(t => new TaskViewModel
                 {
                     Id = t.Id,
                     Title = t.Title,
                     Description = t.Description,
                     Owner = t.Owner.UserName
                 })
             })
             .ToList();

        return View(boards);
    }
}
