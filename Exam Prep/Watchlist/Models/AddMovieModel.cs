namespace Watchlist.Models;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using Watchlist.Data.Entities;
using static Data.Constants.DataConstants.MovieConstants;


public class AddMovieModel
{

    [Required]
    [StringLength(MovieTitleMaxLength, MinimumLength = MovieTitleMinLength,
        ErrorMessage = "{0} should be between {2} and {1} letters long!")]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(MovieDirectorMaxLength, MinimumLength = MovieDirectorMinLength,
        ErrorMessage = "{0} should be between {2} and {1} letters long!")]
    public string Director { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), MovieRatingLowerBorder, MovieRatingUpperBorder, ConvertValueInInvariantCulture = true,
        ErrorMessage = "{0} should ne in the range of {1} and {2}")]
    public decimal Rating { get; set; }

    public int GenreId { get; set; }

    public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
}
