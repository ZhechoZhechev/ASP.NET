namespace Watchlist.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Data.Constants.DataConstants.MovieConstants;

public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(MovieTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(MovieDirectorMaxLength)]
    public string Director { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public decimal Rating { get; set; }

    [Required]
    [ForeignKey(nameof(Genre))]
    public int GenreId { get; set; }

    public Genre Genre { get; set; } = null!;

    public ICollection<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
}
