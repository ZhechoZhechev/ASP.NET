namespace TaskBoardApp.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using TaskBoardApp.Data.Entities;

public class TaskBoardAppDbContext : IdentityDbContext<User>
{
    private User GuestUser { get; set; } = null!;
    private Board OpenBoard { get; set; } = null!;
    private Board InProgressBoard { get; set; } = null!;
    private Board DoneBoard { get; set; } = null!;

    private ICollection<Task> DoneTasks = new List<Task>();

    public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Task>()
            .HasOne(x => x.Board)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.BoardId)
            .OnDelete(DeleteBehavior.Restrict);

        SeedUsers();
        builder
            .Entity<User>()
            .HasData(this.GuestUser);

        SeedBoards();
        builder
            .Entity<Board>()
            .HasData(this.OpenBoard, this.InProgressBoard, this.DoneBoard);

        SeedTasks();
        builder
            .Entity<Task>()
            .HasData(DoneTasks);

        base.OnModelCreating(builder);
    }

    private void SeedUsers()
    {
        var hasher = new PasswordHasher<IdentityUser>();

        this.GuestUser = new User()
        {
            UserName = "Guest",
            NormalizedUserName = "GUEST",
            Email = "guest@mail.com",
            NormalizedEmail = "GUEST@MAIL.COM",
            FirstName = "Guest",
            LasttName = "User"
        };

        this.GuestUser.PasswordHash = hasher.HashPassword(this.GuestUser, "guest");
    }

    private void SeedBoards()
    {
        this.OpenBoard = new Board
        {
            Id = 1,
            Name = "Open"
        };
        this.InProgressBoard = new Board
        {
            Id = 2,
            Name = "In Progress"
        };
        this.DoneBoard = new Board
        {
            Id = 3,
            Name = "Done"
        };
    }

    private void SeedTasks()
    {
        this.DoneTasks = new List<Task>()
        {
            new Task()
            {
                Id = 1,
                Title = "Prepare for ASP.NET Fundamentals exam",
                Description = "Learn to use ASP.NET Core Identity",
                CreatedOn = DateTime.Now.AddMonths(-1),
                OwnerId = this.GuestUser.Id,
                BoardId = this.OpenBoard.Id
            },
            new Task()
            {
                Id = 2,
                Title = "Improve EF Core skills",
                Description = "Learn using EF Core and MS SQL Server Management Studio",
                CreatedOn = DateTime.Now.AddMonths(-5),
                OwnerId = this.GuestUser.Id,
                BoardId = this.DoneBoard.Id,
            },
            new Task()
            {
                Id = 3,
                Title = "Improve ASP.NET Core skills",
                Description = "Learn using ASP.NET Core Identity",
                CreatedOn = DateTime.Now.AddDays(-10),
                OwnerId = this.GuestUser.Id,
                BoardId = this.InProgressBoard.Id,
            },
            new Task()
            {
                Id = 4,
                Title = "Prepare for C# Fundamentals Exam",
                Description = "Prepare by solving old Mid and Final exams",
                CreatedOn = DateTime.Now.AddYears(-1),
                OwnerId = this.GuestUser.Id,
                BoardId = this.DoneBoard.Id,
            }
        };
    }
}
