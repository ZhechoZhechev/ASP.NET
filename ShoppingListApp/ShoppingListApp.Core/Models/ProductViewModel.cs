namespace ShoppingListApp.Core.Models;

using System.ComponentModel.DataAnnotations;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Length should be between 3 and 100 letters")]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0, int.MaxValue, ErrorMessage ="Quantity should be possitive number")]
    public int Quantity { get; set; }
}
