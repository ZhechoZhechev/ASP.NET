namespace ForumApp;

using Microsoft.EntityFrameworkCore;
using ForumApp.Infrastructure.Common;
using ForumApp.Core.Contracts;
using ForumApp.Core.Services;
using ForumApp.Infrastructure;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IRepository, Repository>();

        builder.Services.AddScoped<IPostService, PostService>();

        builder.Services.AddDbContext<ForumAppDbContext>(opt => 
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}