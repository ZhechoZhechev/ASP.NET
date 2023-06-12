namespace Library.Models;

using System.ComponentModel.DataAnnotations;

using Library.Data.Entities;
using static Data.Constants.DataConstants.BookConstants;

public class AddBookViewModel
{
    [Required]
    [StringLength(BookTitleMaxLength, MinimumLength = BookTitleMinLength,
        ErrorMessage = "{0} have to be between {2} and {1} letters long")]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(BookAuthorMaxLength, MinimumLength = BookAuthorMinLength,
    ErrorMessage = "{0} have to be between {2} and {1} letters long")]
    public string Author { get; set; } = null!;

    [Required]
    [StringLength(BookDescriptiobMaxLength, MinimumLength = BookDescriptiobMinLength,
    ErrorMessage = "{0} have to be between {2} and {1} letters long")]
    public string Description { get; set; } = null!;

    [Required]
    public string Url { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), BookRatingLowerBorder, BookRatingUpperBorder)]
    public decimal Rating { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public IEnumerable<CategoriesViewModel> Categories { get; set; } = new List<CategoriesViewModel>();
}

