using Microsoft.AspNetCore.Identity;
namespace Homies.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EventParticipant
{
    [Required]
    [ForeignKey(nameof(Helper))]
    public string HelperId { get; set; }
    public IdentityUser Helper { get; set; }

    [Required]
    [ForeignKey(nameof(Event))]
    public int EventId { get; set; }
    public Event Event { get; set; }
}
