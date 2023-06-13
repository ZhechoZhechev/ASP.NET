namespace Contacts.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Contacts.Data.Entities;

public class ContactsDbContext : IdentityDbContext<ApplicationUser>
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<ApplicationUserContact> ApplicationUsersContacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Contact>()
            .HasData(new Contact()
            {
                Id = 1,
                FirstName = "Bruce",
                LastName = "Wayne",
                PhoneNumber = "+359881223344",
                Address = "Gotham City",
                Email = "imbatman@batman.com",
                Website = "www.batman.com"
            });

        builder.Entity<ApplicationUserContact>()
            .HasKey(pk => new { pk.ApplicationUserId, pk.ContactId });

        builder.Entity<ApplicationUser>()
            .Property(p => p.UserName)
            .HasMaxLength(20)
            .IsRequired(true);

        builder.Entity<ApplicationUser>()
            .Property(p => p.Email)
            .HasMaxLength(60)
            .IsRequired(true);

        base.OnModelCreating(builder);
    }
}