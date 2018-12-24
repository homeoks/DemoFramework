using Infrastructure;

namespace Service.Interface
{
    public interface IUserService
    {
        ApplicationResult<UserViewModel> SignUp(UserViewModel model);
        ApplicationResult GetAllUser();
        ApplicationResult<UserViewModel> GetUserByCredential(string modelUserName, string modelPassword);
        ApplicationResult<UserViewModel> GetUserByRefreshToken(string modelRefreshToken);
        string GrantRefreshToken(string userId);
        ApplicationResult GetUserProfile(string currentUserId);
        ApplicationResult UpdateUserProfile(UserEditProfile currentUserId, string userId);
        ApplicationResult GetOtherUserProfile(string currentUserId);
        ApplicationResult GetUserById(string id);
    }
}