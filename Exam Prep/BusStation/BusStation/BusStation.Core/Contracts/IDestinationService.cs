namespace BusStation.Core.Contracts;

using BusStation.Core.Models;

public interface IDestinationService
{
    Task<IEnumerable<AllDestinationsViewModel>> GetAllDestinations();

    Task AddDestination(AddDestinationViewModel destination);
}
