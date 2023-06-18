namespace Homies.Data.Entities;

using System.ComponentModel.DataAnnotations;

using static Constants.DataConstants.TypeCosntants;

public class Type
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(TypeNameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Event> Events { get; set; } = new List<Event>();
}

