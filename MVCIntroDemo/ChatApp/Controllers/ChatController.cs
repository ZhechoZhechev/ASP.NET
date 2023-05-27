﻿using ChatApp.Models.Messages;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> s_messages =
            new List<KeyValuePair<string, string>>();

        public IActionResult Show()
        {
            if (s_messages.Count < 1)
            {
                return View(new ChatViewModel { });
            }

            var chatModel = new ChatViewModel
            {
                Messages = s_messages
                .Select(m => new MessageViewModel
                {
                    Sender = m.Key,
                    Message = m.Value
                })
                .ToList()
            };

            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMessage = chat.CurrentMessage;
            s_messages.Add(new KeyValuePair<string, string>
                (newMessage.Sender, newMessage.Message));

            return RedirectToAction("Show");
        }
    }
}
