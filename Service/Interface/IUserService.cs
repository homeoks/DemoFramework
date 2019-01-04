using System.Collections.Generic;
using Entity;
using Infrastructure;

namespace Service.Interface
{
    public interface IUserService
    {
        ApplicationResult<UserViewModel> SignUp(UserViewModel model);
        ApplicationResult<List<User>> GetAllUser();
        ApplicationResult<UserViewModel> GetUserByCredential(string modelUserName, string modelPassword);
        ApplicationResult<UserViewModel> GetUserByRefreshToken(string modelRefreshToken);
        string GrantRefreshToken(string userId);
        ApplicationResult GetUserProfile(string currentUserId);
        ApplicationResult UpdateUserProfile(UserEditProfile currentUserId, string userId);
        ApplicationResult GetUserById(string s, string id);
        ApplicationResult ActionUser(RelationAction action, string userId, string currentUserId);
        ApplicationResult GetBlackList(string currentUserId);
        ApplicationResult GetOtherUserProfile(string userId, int currentUserId, int pageIndex, string id);
    }
}