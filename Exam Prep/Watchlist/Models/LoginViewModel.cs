namespace Watchlist.Models;

using System.ComponentModel.DataAnnotations;

using static Data.Constants.DataConstants.UserConstants;

public class LoginViewModel
{
    [Required]
    [StringLength(UserUserNameMaxLength, MinimumLength = UserUserNameMinLength)]
    public string UserName { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(UserPasswordMaxLength, MinimumLength = UserPasswordMinLength)]
    public string Password { get; set; } = null!;
}
