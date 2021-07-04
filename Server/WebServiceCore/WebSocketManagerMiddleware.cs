using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler { get; set; }

        public WebSocketManagerMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            // Verifica se é uma requisição de websocket
            if (!context.WebSockets.IsWebSocketRequest) { return; }

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            await this._webSocketHandler.OnConnected(socket);

            // Controla o fluxo da requisição de acordo com o tipo da mensagem
            await Receive(socket, async (result, buffer) =>
            {
 
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await this._webSocketHandler.ReceiveAsync(socket, result, buffer);
                    return;
                }
                else if (result.MessageType == WebSocketMessageType.Binary)
                {
                    // at this time, we won't deal this part
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await this._webSocketHandler.OnDisconnected(socket);
                    return;
                }
            });
        }

        /// <summary>
        /// Encapsula a lógica de conversão dos dados recebidos para uma forma mais simples de manipular
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="handleMessage"></param>
        /// <returns></returns>
        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            const int BUFFER_LENGTG = 4096; // 4 * 1024;
            var buffer = new byte[BUFFER_LENGTG];
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                handleMessage(result, buffer);
            }
        }
    }
}
