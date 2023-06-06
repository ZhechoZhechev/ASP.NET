namespace TaskBoardApp.Models.Task;

using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Models.Board;
using static Data.Constants.DataConstants.TaskConstants;

public class TaskFormModel
{
    [Required]
    [StringLength(MaxTaskTitle, MinimumLength = MinTaskTitle,
        ErrorMessage = "{0} should be between {2} and {1} letters long!")]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(MaxTaskDescription, MinimumLength = MinTaskDescription,
        ErrorMessage = "{0} should be between {2} and {1} letters long!")]
    public string Description { get; set; } = null!;

    [Display(Name = "Board")]
    public int BoardId { get; set; }

    public IEnumerable<TaskBoardModel> Boards { get; set; }
}
