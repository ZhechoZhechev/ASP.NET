using BusStation.Core.Models;

namespace BusStation.Core.Contracts
{
    public interface ITicketService
    {
        Task AddTicketToDestination(int id, CreateTicketViewModel model);

        Task BookATripForUser(int id, string userId);

        Task<IEnumerable<MyTicketsViewModel>> GetMytickets(string userId);
    }
}
