using Infrastructure;
using Service.ViewModel;

namespace Service.Interface
{
    public interface IHobbyService
    {
        ApplicationResult AddNewHobby(HobbyViewModel model, string currentUserId);
        ApplicationResult GetAllHobby(string currentUserId);
    }
}