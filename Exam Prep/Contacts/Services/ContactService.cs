namespace Contacts.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Contacts.Contracts;
using Contacts.Data;
using Contacts.Data.Entities;
using Contacts.Models;
using Microsoft.EntityFrameworkCore;

public class ContactService : IContactService
{
    private readonly ContactsDbContext context;

    public ContactService(ContactsDbContext context)
    {
        this.context = context;
    }

    public async Task AddContactAsync(AddContactViewModel viewModel)
    {
        var contact = new Contact() 
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            Email = viewModel.Email,
            PhoneNumber = viewModel.PhoneNumber,
            Address = viewModel.Address,
            Website = viewModel.Website
        };

        await context.AddAsync(contact);
        await context.SaveChangesAsync();
    }

    public async Task UpdateContactAsync(int contactId, AddContactViewModel model)
    {
        var entity = await context.Contacts.FindAsync(contactId);

        if (entity != null)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Address = model.Address;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Email = model.Email;
            entity.Website = model.Website;
        }

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AllContactViewModel>> GetAllContactsAsync()
    {
        return await context.Contacts
            .AsNoTracking()
            .Select(c => new AllContactViewModel() 
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Website = c.Website
            })
            .ToListAsync();
    }

    public async Task<AddContactViewModel?> GetContactByIdAsync(int contactId)
    {
        var entity = await context.Contacts.FindAsync(contactId);

        if (entity != null)
        {
            AddContactViewModel model = new()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Address = entity.Address,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Website = entity.Website
            };
            return model;
        }
        return null;
    }

    public async Task AddToTeamAsync(int contactId, string userId)
    {
        var contactUser = new ApplicationUserContact()
        {
            ContactId = contactId,
            ApplicationUserId = userId
        };

        if (!context.ApplicationUsersContacts.Contains(contactUser)) 
        {
            await context.ApplicationUsersContacts.AddAsync(contactUser);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<AllContactViewModel>> GetMyContactsAsync(string userId)
    {
        var enities = await context.ApplicationUsersContacts
            .Where(x => x.ApplicationUserId == userId)
            .Select(con => con.Contact)
            .ToListAsync();

        var models = enities
            .Select(en => new AllContactViewModel()
            {
                Id = en.Id,
                FirstName = en.FirstName,
                LastName = en.LastName,
                Address = en.Address,
                Email = en.Email,
                PhoneNumber = en.PhoneNumber,
                Website = en.Website
            })
            .ToList();

        return models;
    }

    public async Task RemoveFromTeamAsync(int contactId, string userId)
    {
        var userContact = await context.ApplicationUsersContacts.FirstOrDefaultAsync(x => x.ContactId == contactId
        && x.ApplicationUserId == userId);

        if (userContact != null)
        {
            context.Remove(userContact);
            await context.SaveChangesAsync();
        }
    }
}
