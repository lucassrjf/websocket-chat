using Server.Models;
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

        /// <summary>
        /// Obtem todos os clientes que estabeleceram conexão com o server
        /// Independente se realizou entrou em alguma sala ou não
        /// </summary>
        /// <returns></returns>
        public List<Client> GetAll()
        {
            return Clients;
        }

        /// <summary>
        /// Obtem todos os clientes logados em uma determinada sala
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public List<Client> GetAllInSpecificRoom(string room)
        {
            return Clients.Where(client => client.Room == room).ToList();
        }

        /// <summary>
        /// Obtem uma Cliente pelo seu websocket
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        public Client GetBySocket(WebSocket webSocket)
        {
            return Clients.Where(client => client?.WebSocket == webSocket)?.FirstOrDefault();
        }

        /// <summary>
        /// Obtem um Cliente pelo seu username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Client GetByUserName(string username)
        {
            return Clients.Where(client => client.UserName == username)?.FirstOrDefault();
        }

        /// <summary>
        /// Obtem um cliente pelo seu UUID
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public Client GetByUuid(string uuid)
        {
            return Clients.Where(client => client.Uuid == uuid)?.FirstOrDefault();
        }

        /// <summary>
        /// Adiciona um Cliente ao array de Clientes de determinada sala
        /// Na implementação atual existe apenas uma sala
        /// </summary>
        /// <param name="webSocket"></param>
        public void Add(WebSocket webSocket)
        {
            Clients.Add(new Client(webSocket));
        }

        /// <summary>
        /// Obtem todos os usernames dos usuários de determinada sala
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllUserNamesInRoom()
        {
            var listUsernames = Clients.Where(client => !String.IsNullOrEmpty(client.UserName)).Select(client => client.UserName).ToList();

            return listUsernames;
        }

        /// <summary>
        /// Remove um cliente da lista de clientes
        /// Não preparado ainda para o caso de múltiplas salas
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task RemoveSocket(string uuid)
        {
            Client client = Clients.Where(client => client.Uuid == uuid).FirstOrDefault();
            Clients.Remove(client);
                
            await client.WebSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                        statusDescription: "Closed by the Service",
                        cancellationToken: CancellationToken.None);
        }

        /// <summary>
        /// Realiza o Login do cliente, que consiste em associa-lo a uma sala e atribuir o username
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Login(string uuid, string userName)
        {
            var clientWithSameUserName = Clients.Where(client => client.UserName != null && client.UserName?.ToLower() == userName.ToLower()).FirstOrDefault();
            
            if (clientWithSameUserName != null) { return false; }

            Clients.FirstOrDefault(client => client.Uuid == uuid)?.Login(userName);
            return true;
        }
    }
}
