namespace Watchlist.Data.Entities;

using System.ComponentModel.DataAnnotations;
using static Constants.DataConstants.GenreConstants;

public class Genre
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(GenreNameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}


