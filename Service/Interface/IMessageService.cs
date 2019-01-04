using Infrastructure;
using Service.ViewModel;

namespace Service.Interface
{
    public interface IMessageService
    {
        void SaveMessage(string message, string currentUserId, string id);
        ApplicationResult GetAllMessageWithUser(string currentUserId, string withUser);
    }
}