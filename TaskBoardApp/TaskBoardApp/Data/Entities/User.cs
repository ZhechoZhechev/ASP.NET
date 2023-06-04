namespace TaskBoardApp.Data.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

using static Constants.DataConstants.UserConstants;

public class User : IdentityUser
{
    [Required]
    [MaxLength(UserFirstLastNameMaxLength)]
    public string FirstName { get; init; } = null!;

    [Required]
    [MaxLength(UserFirstLastNameMaxLength)]
    public string LasttName { get; init; } = null!;
}
