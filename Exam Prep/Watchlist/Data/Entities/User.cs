namespace Watchlist.Data.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public ICollection<UserMovie> UsersMovies { get; set; } = new List<UserMovie>();
}

