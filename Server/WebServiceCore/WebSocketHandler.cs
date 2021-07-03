using Server.Serivices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public abstract class WebSocketHandler
    {
        protected WebSocketHandler(ClientService clientService)
        {
            ClientService = clientService;
        }

        protected ClientService ClientService { get; set; }

        public virtual async Task OnConnected(WebSocket webSocket)
        {
            ClientService.Add(webSocket);
        }

        public virtual async Task OnDisconnected(WebSocket webSocket)
        {
            string uuid = ClientService.GetBySocket(webSocket).Uuid;
            await ClientService.RemoveSocket(uuid);
           
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            Debug.Print(message);
            if (socket.State != WebSocketState.Open) { return; }
            var bytes = Encoding.UTF8.GetBytes(message);
            var buffer = new ArraySegment<byte>(bytes, 0, bytes.Length);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public abstract Task ReceiveAsync(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer);
    }
}
