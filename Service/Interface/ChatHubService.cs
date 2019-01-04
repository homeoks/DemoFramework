using Microsoft.AspNetCore.SignalR;

namespace Service.Interface
{
    public interface IChatHubService : IHubContext<ChatHub>
{
        void SendMessageToUser(string message, string id);
    }
}