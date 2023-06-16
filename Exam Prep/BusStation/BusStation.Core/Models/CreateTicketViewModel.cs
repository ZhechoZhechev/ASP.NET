using System.ComponentModel.DataAnnotations;

namespace BusStation.Core.Models;

public class CreateTicketViewModel
{
    [Required]
    [Range(typeof(decimal), "10", "90")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, 10)]
    public int TicketsCount { get; set; }

    public int DestinationId { get; set; }
}
