using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace Server.Models
{
    /// <summary>
    /// Representa um cliente que estabeleceu conexão com o servidor
    /// </summary>
    public class Client
    {
        public string Uuid { get; set; }
        public string UserName { get; set; }
        public string Room { get; set; }
        public WebSocket WebSocket { get; set; }

        /// <summary>
        /// Cria um Cliente através de um Websocket e gera um UUID
        /// </summary>
        /// <param name="webSocket"></param>
        public Client(WebSocket webSocket)
        {
            Uuid = Guid.NewGuid().ToString();
            WebSocket = webSocket;
        }

        /// <summary>
        /// Seta o username e a sala que o usuário entrou
        /// Atualmente está somente uma sala, mas em uma evolução
        /// deixará de ser uma constante
        /// </summary>
        /// <param name="username"></param>
        public void Login(string username)
        {
            UserName = username;
            Room = "#general";
        }
    }
}
