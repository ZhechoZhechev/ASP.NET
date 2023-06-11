namespace Library.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Constants.DataConstants.BookConstants;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(BookTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(BookAuthorMaxLength)]
    public string Author { get; set; } = null!;

    [Required]
    [MaxLength(BookDescriptiobMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public decimal Rating { get; set; }

    [Required]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; }= null!;

    public ICollection<IdentityUserBook> UsersBooks { get; set; } = new HashSet<IdentityUserBook>();
}
