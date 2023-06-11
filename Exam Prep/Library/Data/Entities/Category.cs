namespace Library.Data.Entities;

using System.ComponentModel.DataAnnotations;
using static Constants.DataConstants.CategoryConstants;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(CategoryNameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Book> Books { get; set; } = new HashSet<Book>();
}
