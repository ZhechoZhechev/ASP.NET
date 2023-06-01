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

        modelBuilder.Entity<Post>()
           .Property(p => p.IsDeleted)
           .HasDefaultValue(false);

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Post> Posts { get; set; }
}
