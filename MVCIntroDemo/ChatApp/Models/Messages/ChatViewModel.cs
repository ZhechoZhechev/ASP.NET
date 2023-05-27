using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Messages
{
    public class ChatViewModel
    {
        [Required]
        public MessageViewModel CurrentMessage { get; set; } = null!;

        public List<MessageViewModel> Messages { get; set; } = null!;
    }
}
