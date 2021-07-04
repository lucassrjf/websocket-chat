using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Server.Models
{
    public class ResponseMessage
    {
        public string MessageText { get; set; }
        public List<string> UsersInRoom { get; set; }

        public ResponseMessage(string messageText, List<string> usersInRoom)
        {
            MessageText = messageText;
            UsersInRoom = usersInRoom;
        }
    }
}
