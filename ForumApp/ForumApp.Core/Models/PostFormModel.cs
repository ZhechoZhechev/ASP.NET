using ForumApp.Infrastructure.Common;

namespace ForumApp.Core.Models;

using System.ComponentModel.DataAnnotations;

using static DataConstants.PostCostants;

public class PostFormModel
{
    [Required]
    [StringLength(PostTitleMaxLenght, MinimumLength = PostTitleMinLenght, ErrorMessage = "{0} be between {2} and {1} characters long ")]
    public string Title { get; set; } = null!;
    [Required]
    [StringLength(PostContentMaxLenght, MinimumLength = PostContentMinLenght, ErrorMessage = "{0} be between {2} and {1} characters long ")]
    public string Content { get; set; } = null!;

}
