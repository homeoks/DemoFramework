using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Repository.Interface;
using Service.Interface;

namespace Service.Service
{
    public class ChatHubService
    {
        private readonly ISignalRRepository _signalRRepository;
        public IHubClients Clients { get; }
        public IGroupManager Groups { get; }
        public ChatHubService(IHubClients clients, IGroupManager groups, ISignalRRepository signalRRepository)
        {
            Clients = clients;
            Groups = groups;
            _signalRRepository = signalRRepository;
        }

      
        public void SendMessageToUser(string message, string id)
        {
            if (id == null)
                Task.Run(() => Clients.All.SendAsync("adminSendMessage", message));
            else
            {
                id = id.Replace(" ", "").Replace("\n", "");
                var connectionId = _signalRRepository.GetById(id)?.ConnectionId;
                if (connectionId != string.Empty)
                    Task.Run(() => Clients.Clients(connectionId).SendAsync("adminSendMessage", message));
            }
        }
    }
}