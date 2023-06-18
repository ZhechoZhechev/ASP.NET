namespace Homies.Data.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Constants.DataConstants.EventCosntants;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(EventNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(EventDescMaxLength)]
    public string Description { get; set; }

    [Required]
    [ForeignKey(nameof(Organiser))]
    public string OrganiserId { get; set; } = null!;
    [Required]
    public IdentityUser Organiser { get; set; } = null!;

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime Start { get; set; }

    [Required]
    public DateTime End { get; set; }

    [Required]
    [ForeignKey(nameof(Type))]
    public int TypeId { get; set; }
    [Required]
    public Type Type { get; set; } = null!;

    public ICollection<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
}

