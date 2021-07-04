using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace Server.Models
{
    public class Client
    {
        public string Uuid { get; set; }
        public string UserName { get; set; }
        public string Room { get; set; }
        public WebSocket WebSocket { get; set; }

        public Client(WebSocket webSocket)
        {
            Uuid = Guid.NewGuid().ToString();
            WebSocket = webSocket;
        }

        public void Login(string username)
        {
            UserName = username;
            Room = "#general";
        }
    }
}
