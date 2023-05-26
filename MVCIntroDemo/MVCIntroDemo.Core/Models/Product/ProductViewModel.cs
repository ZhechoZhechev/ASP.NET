namespace MVCIntroDemo.Core.Models.Product;

using System.ComponentModel.DataAnnotations;

public  class ProductViewModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage ="Name should be btween 2 and 50 letters")]
    public string Name { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), "0", "79228162514264337593543950335")]
    public decimal Price { get; set; }
}
