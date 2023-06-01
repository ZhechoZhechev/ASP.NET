using ForumApp.Infrastructure.Common;

namespace ForumApp.Infrastructure.Data;

using System.ComponentModel.DataAnnotations;
using static DataConstants.PostCostants;

public class Post
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(PostTitleMaxLenght)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(PostContentMaxLenght)]
    public string Content { get; set; } = null!;

    [Required]
    public bool IsDeleted { get; set; } = false;
}
