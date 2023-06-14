namespace Contacts.Contracts;

using Contacts.Data.Entities;
using Contacts.Models;
using System.Drawing;

public interface IContactService
{
    Task<IEnumerable<AllContactViewModel>> GetAllContactsAsync();

    Task AddContactAsync(AddContactViewModel model);

    Task<AddContactViewModel?> GetContactByIdAsync(int contactId);

    Task UpdateContactAsync(int contactId, AddContactViewModel model);

    Task AddToTeamAsync(int contactId, string userId);

    Task RemoveFromTeamAsync(int contactId, string userId);

    Task<IEnumerable<AllContactViewModel>> GetMyContactsAsync(string userId);

}
