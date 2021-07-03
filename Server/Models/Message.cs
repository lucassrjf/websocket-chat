using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Server.Models
{
    public class Message
    {
        public string UserName { get; set; }
        public string Action { get; set; }
        public string MessageTo { get; set; }
        public bool IsPrivate { get; set; }
        public string MessageText { get; set; }

        // Construtor default pois a deserialização demanda este construtor
        public Message() : base() { }

        public Message(string jsonString)
        {
            var message = JsonSerializer.Deserialize<Message>(jsonString);
            UserName = message.UserName;
            Action = message.Action;
            MessageTo = message.MessageTo;
            IsPrivate = message.IsPrivate;
            MessageText = message.MessageText;
        }


    }
}
