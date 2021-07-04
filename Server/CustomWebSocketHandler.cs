using Server.Models;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Classe responsável por implementar os métodos do servidor aplicando as regras de negócio específicas
    /// </summary>
    public class CustomWebSocketHandler : WebSocketHandler
    {
        public CustomWebSocketHandler(ClientService clientService) : base(clientService) { }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);
        }
        
        public override async Task OnDisconnected(WebSocket webSocket)
        {
            await SendLogoutMessageForAll(webSocket);
            await base.OnDisconnected(webSocket);
        }

        /// <summary>
        /// Responsável pelo encaminhamento da mensagem para as regras específicas de acordo com
        /// a ação e os destinatários da mensagem
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="result"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public override async Task ReceiveAsync(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer)
        {
            string jsonString = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Message message = new Message(jsonString);

            if (message.Action == "LOGIN")
            {
                Client client = ClientService.GetBySocket(webSocket);
                ClientService.Login(client.Uuid, message.UserName);
                await SendLoginMessageForAll(webSocket, message);
            }

            if (message.Action == "MESSAGE")
            {
                if (message.MessageTo == "0")
                {
                    await SendMessageTextForAll(webSocket, message);

                } else
                {
                    if (message.IsPrivate)
                    {
                        await SendPrivateMessageText(webSocket, message);
                    }
                    else
                    {
                        await SendMessageTextForAllWithMention(webSocket, message);
                    }
                }
            }
        }

        /// <summary>
        /// Envia mensagem informando a entrada de novo cliente à sala
        /// Envia para todos os clientes da sala
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task SendLoginMessageForAll(WebSocket webSocket, Message message)
        {
            var clients = ClientService.GetAllInSpecificRoom("#general");
            var sender = ClientService.GetBySocket(webSocket);
            var messageText = $"{message.UserName}: entrou na sala {sender.Room}";
            var usersInRoom = ClientService.GetAllUserNamesInRoom();

            var responseMessage = new ResponseMessage(messageText, usersInRoom);

            foreach (var client in clients)
            {
                await SendMessageAsync(client.WebSocket, JsonSerializer.Serialize(responseMessage));
            }
        }

        /// <summary>
        /// Envia mensagem informando a saída de cliente da sala
        /// Envia para todos os clientes da sala
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        private async Task SendLogoutMessageForAll(WebSocket webSocket)
        {
            var clients = ClientService.GetAllInSpecificRoom("#general");
            var sender = ClientService.GetBySocket(webSocket);
            var messageText = $"{sender.UserName}: saiu da sala {sender.Room}";
            var usersInRoom = ClientService.GetAllUserNamesInRoom();
            usersInRoom.Remove(sender.UserName);

            var responseMessage = new ResponseMessage(messageText, usersInRoom);

            foreach (var client in clients)
            {
                await SendMessageAsync(client.WebSocket, JsonSerializer.Serialize(responseMessage));
            }
        }

        /// <summary>
        /// Envia mensagem para todos da sala
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task SendMessageTextForAll(WebSocket webSocket, Message message)
        {
            var clients = ClientService.GetAllInSpecificRoom("#general");
            var sender = ClientService.GetBySocket(webSocket);
            var messageText = $"{sender.UserName} diz: {message.MessageText}";
            var usersInRoom = ClientService.GetAllUserNamesInRoom();

            var responseMessage = new ResponseMessage(messageText, usersInRoom);

            foreach (var client in clients)
            {
                await SendMessageAsync(client.WebSocket, JsonSerializer.Serialize(responseMessage));
            }
        }

        /// <summary>
        /// Enviar uma mensagem para todos da sala, porém informando uma menção
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task SendMessageTextForAllWithMention(WebSocket webSocket, Message message)
        {
            var clients = ClientService.GetAllInSpecificRoom("#general");
            var sender = ClientService.GetBySocket(webSocket);
            var receiver = ClientService.GetByUserName(message.MessageTo);
            var messageText = $"{sender.UserName} diz para @{receiver.UserName}: {message.MessageText}";
            var usersInRoom = ClientService.GetAllUserNamesInRoom();

            var responseMessage = new ResponseMessage(messageText, usersInRoom);

            foreach (var client in clients)
            {
                await SendMessageAsync(client.WebSocket, JsonSerializer.Serialize(responseMessage));
            }
        }

        /// <summary>
        /// Envia uma mensagem privada para determinado cliente
        /// O remetente e o destinatário recebem esta mensagem
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task SendPrivateMessageText(WebSocket webSocket, Message message)
        {
            var sender = ClientService.GetBySocket(webSocket);
            var receiver = ClientService.GetByUserName(message.MessageTo);
            var messageText = $"{sender.UserName} diz para @{receiver.UserName} (privado): {message.MessageText}";
            var usersInRoom = ClientService.GetAllUserNamesInRoom();

            var responseMessage = new ResponseMessage(messageText, usersInRoom);

            await SendMessageAsync(sender.WebSocket, JsonSerializer.Serialize(responseMessage));
            await SendMessageAsync(receiver.WebSocket, JsonSerializer.Serialize(responseMessage));
        }
    }
}
