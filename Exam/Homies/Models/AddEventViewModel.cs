namespace Homies.Models;

using System.ComponentModel.DataAnnotations;

using static Homies.Data.Constants.DataConstants.EventCosntants;

public class AddEventViewModel
{
    [Required]
    [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(EventDescMaxLength, MinimumLength = EventDescMinLength)]
    public string Description { get; set; }

   
    public string OrganiserId { get; set; } = null!;

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime Start { get; set; }

    [Required]
    public DateTime End { get; set; }

    public int TypeId { get; set; }

    public IEnumerable<AllTypesViesModel> Types { get; set; } = new List<AllTypesViesModel>();
}
