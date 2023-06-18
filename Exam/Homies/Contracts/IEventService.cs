namespace Homies.Contracts;

using Homies.Models;

public interface IEventService
{
    Task AddEventAsync(AddEventViewModel model, string userid);

    Task<IEnumerable<AllTypesViesModel>> GetAllTypessAsync();

    Task<IEnumerable<AllEventsViewModel>> GetAllEventsAsync();

    Task JoinAnEvent(int id, string userid);

    Task<IEnumerable<AllEventsViewModel>> GetMyEventsAsync(string userId);

    Task LeaveAnEvent(int id, string userid);

    Task<AddEventViewModel> GetSingleEvent(int id);
    Task UpdateContactAsync(int id, AddEventViewModel model);
}
