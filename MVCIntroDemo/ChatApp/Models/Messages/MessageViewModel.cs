using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Messages;

public class MessageViewModel
{
    [Required]
    public string Sender { get; set; } = null!;


    [Required]
    public string Message { get; set; } = null!;
}
