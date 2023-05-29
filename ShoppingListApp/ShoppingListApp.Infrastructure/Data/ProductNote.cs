namespace ShoppingListApp.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductNote
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Content { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }

    public Product Product { get; set; }

}
