using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Server.Models
{
    /// <summary>
    /// Classe responsável pelo formato da mensagem recebida pelo FrontEnd
    /// </summary>
    public class Message
    {
        public string UserName { get; set; }
        public string Action { get; set; }
        public string MessageTo { get; set; }
        public bool IsPrivate { get; set; }
        public string MessageText { get; set; }

        /// <summary>
        /// Construtor default pois a deserialização demanda este construtor
        /// </summary>
        public Message() : base() { }

        /// <summary>
        /// Recebe uma string contendo um objeto serializado e cria um objeto
        /// Mensagem a partir dela
        /// </summary>
        /// <param name="jsonString"></param>
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
