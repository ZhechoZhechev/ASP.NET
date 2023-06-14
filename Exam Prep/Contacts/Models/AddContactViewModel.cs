namespace Contacts.Models;

using System.ComponentModel.DataAnnotations;

public class AddContactViewModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(60, MinimumLength = 10)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(13, MinimumLength = 10)]
    [RegularExpression(@"^(?:\+359|0)\s?\d{3}(?:[-\s]?\d{2}){3}$")]
    public string PhoneNumber { get; set; } = null!;

    public string? Address { get; set; }

    [Required]
    [RegularExpression(@"^www\.[\w\-]+\.bg$")]
    public string Website { get; set; } = null!;
}

