namespace ShoppingListApp.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;

public class Product
{
    public Product()
    {
        this.ProductNotes = new HashSet<ProductNote>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }

    public ICollection<ProductNote> ProductNotes { get; set; }

}
