namespace Watchlist.Models;

using System.ComponentModel.DataAnnotations;
using static Data.Constants.DataConstants.UserConstants;

public class RegisterViewModel : LoginViewModel
{
    [Required]
    [EmailAddress]
    [StringLength(UserEmailMaxLength, MinimumLength = UserEmailMinLength)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}
