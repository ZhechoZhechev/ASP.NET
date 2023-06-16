namespace BusStation.Core.Models;

public class AllDestinationsViewModel
{
    public int Id { get; set; }

    public string DestinationName { get; set; }

    public string Origin { get; set; }

    public string Date { get; set; }

    public string Time { get; set; }

    public string ImageUrl { get; set; }

    public int TicketsCount { get; set; }
}
