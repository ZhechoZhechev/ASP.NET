namespace Watchlist.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserMovie
{
    [Key]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    [Key]
    [ForeignKey(nameof(Movie))]
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
}