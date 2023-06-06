using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models;
using TaskBoardApp.Models.Home;

namespace TaskBoardApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TaskBoardAppDbContext context;

        public HomeController(TaskBoardAppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<string> taskBoards = this.context.Boards.Select(b => b.Name).Distinct().ToList();
            var tasksCount = new List<HomeBoardModel>();
            foreach (var boardName in taskBoards)
            {
                int tasksInBoard = this.context.Tasks.Where(t => t.Board.Name == boardName).Count();
                tasksCount.Add(new HomeBoardModel
                {
                    BoardName = boardName,
                    TasksCount = tasksInBoard
                });
            }

            var userTasksCount = -1;

            if (this.User.Identity.IsAuthenticated)
            {
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                userTasksCount = this.context.Tasks.Where(t => t.OwnerId == currentUserId).Count();
            }

            var homeModel = new HomeViewModel
            {
                AllTasksCount = this.context.Tasks.Count(),
                BoardsWithTasksCount = tasksCount,
                UserTasksCount = userTasksCount
            };

            return View(homeModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}