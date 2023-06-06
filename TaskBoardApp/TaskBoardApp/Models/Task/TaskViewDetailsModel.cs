namespace TaskBoardApp.Models.Task
{
    public class TaskViewDetailsModel : TaskViewModel
    {
        public string CreatedOn { get; set; } = null!;

        public string Board { get; set;} = null!;
    }
}
