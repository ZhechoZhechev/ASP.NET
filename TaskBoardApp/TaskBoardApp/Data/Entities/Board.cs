namespace TaskBoardApp.Data.Entities;

using System.ComponentModel.DataAnnotations;
using static Constants.DataConstants.BoardConstants;

public class Board
{
    public Board() => this.Tasks = new HashSet<Task>();

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(MaxBoardName)]
    public string Name { get; set; } = null!;

    public ICollection<Task> Tasks { get; set; }
}
