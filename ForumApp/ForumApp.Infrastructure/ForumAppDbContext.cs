namespace ForumApp.Infrastructure;

using Microsoft.EntityFrameworkCore;

using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Configure;

public class ForumAppDbContext : DbContext
{
    public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
        :base(options) 
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Post>(new PostConfiguration());

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Post> Posts { get; set; }
}
