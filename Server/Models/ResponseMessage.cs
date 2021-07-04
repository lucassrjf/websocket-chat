using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Server.Models
{
    /// <summary>
    /// Representa o formato da mensagem de saída (Server -> Client)
    /// </summary>
    public class ResponseMessage
    {
        public string MessageText { get; set; }
        public List<string> UsersInRoom { get; set; }

        /// <summary>
        /// Contrutor que seta a mensagem de retorno e quais os clientes ainda logados
        /// </summary>
        /// <param name="messageText"></param>
        /// <param name="usersInRoom"></param>
        public ResponseMessage(string messageText, List<string> usersInRoom)
        {
            MessageText = messageText;
            UsersInRoom = usersInRoom;
        }
    }
}
