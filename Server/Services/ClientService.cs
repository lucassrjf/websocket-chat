﻿using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Services
{
    public class ClientService
    {
        private List<Client> Clients = new List<Client>();

        public List<Client> GetAll()
        {
            return Clients;
        }

        public List<Client> GetAllInSpecificRoom(string room)
        {
            return Clients.Where(client => client.Room == room).ToList();
        }

        public Client GetBySocket(WebSocket webSocket)
        {
            return Clients.Where(client => client?.WebSocket == webSocket)?.FirstOrDefault();
        }

        public Client GetByUserName(string username)
        {
            return Clients.Where(client => client.UserName == username)?.FirstOrDefault();
        }

        public Client GetByUuid(string uuid)
        {
            return Clients.Where(client => client.Uuid == uuid)?.FirstOrDefault();
        }

        public void Add(WebSocket webSocket)
        {
            Clients.Add(new Client(webSocket));
        }

        public List<string> GetAllUserNamesInRoom()
        {
            var listUsernames = Clients.Where(client => !String.IsNullOrEmpty(client.UserName)).Select(client => client.UserName).ToList();

            return listUsernames;
        }

        public async Task RemoveSocket(string uuid)
        {
            
                Client client = Clients.Where(client => client.Uuid == uuid).FirstOrDefault();

                Clients.Remove(client);
                
            await client.WebSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                        statusDescription: "Closed by the Service",
                        cancellationToken: CancellationToken.None);
                         
        }

        public bool Login(string uuid, string userName)
        {
            var clientWithSameUserName = Clients.Where(client => client.UserName != null && client.UserName?.ToLower() == userName.ToLower()).FirstOrDefault();
            
            if (clientWithSameUserName != null) { return false; }

            Clients.FirstOrDefault(client => client.Uuid == uuid)?.Login(userName);
            return true;
        }

        // TODO talvez tirar isso daqui
        public WebSocket GetSocketByUserUuid(string uuid)
        {
            return Clients.FirstOrDefault(client => client.Uuid == uuid).WebSocket;
        }
    }
}
