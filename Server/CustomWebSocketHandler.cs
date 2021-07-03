using Server.Models;
using Server.Serivices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class CustomWebSocketHandler : WebSocketHandler
    {
        public CustomWebSocketHandler(ClientService clientService) : base(clientService) { }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
        }
        
        public override async Task OnDisconnected(WebSocket socket)
        {
            //var user = ClientService.GetBySocket(socket);
            //if (user.Logged)
            //{
            //    await SendMessageToAllAsync($"{Message.ExitedRoom} {user.Nickname} saiu da sala.");
            //}
            await base.OnDisconnected(socket);
        }


        public override async Task ReceiveAsync(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer)
        {
            Client Client = new Client(webSocket);

            Debug.Print(Encoding.UTF8.GetString(buffer, 0, result.Count));

            await SendMessageAsync(webSocket, "MEnsagem vinda do servidor");
        }
    }
}
