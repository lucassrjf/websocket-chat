using Server.Models;
using Server.Serivices;
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
            //ClientService.Add(webSocket);

            string jsonString = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Message message = new Message(jsonString);
            //Client.Login(message.UserName);

            if (message.Action == "LOGIN")
            {
                Client client = ClientService.GetBySocket(webSocket);
                ClientService.Login(client.Uuid, message.UserName);
                await SendLoginMessageForAll(message);
            }

            if (message.Action == "MESSAGE")
            {
                SendMessageTextForAll(webSocket, message);
            }

            //await SendMessageAsync(webSocket, "Joaozin entrou na sala");
        }

        private async Task SendLoginMessageForAll(Message message)
        {
            var clients = ClientService.GetAll();

            foreach (var client in clients)
            {
                await SendMessageAsync(client.WebSocket, $"{message.UserName} entrou na sala.");
            }
        }

        private async Task SendMessageTextForAll(WebSocket webSocket, Message message)
        {
            var clients = ClientService.GetAll();
            var sender = ClientService.GetBySocket(webSocket);
            var messageText = $"{sender.UserName}: {message.MessageText}";
            var usersInRoom = ClientService.GetAllUserNamesInRoom();

            var responseMessage = new ResponseMessage(messageText, usersInRoom);

            foreach (var client in clients)
            {
                await SendMessageAsync(client.WebSocket, JsonSerializer.Serialize(responseMessage));
            }
        }







    }
}
