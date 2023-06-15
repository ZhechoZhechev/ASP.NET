namespace BusStation.Infrastructure.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Destination))]
    public int DestinationId { get; set; }
    public Destination Destination { get; set; } = null!;
}

//⦁	Has Id – an int, Primary Key
//⦁	Has Price – a decimal, between 10 and 90
//⦁	Has UserId – an int
//⦁	Has DestinationId – an int
