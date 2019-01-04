using System;
using System.Collections.Generic;
using Entity;
using Infrastructure;
using Repository.BaseRepository;

namespace Repository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> GetUsersIncludeDeviceAndHobby(string search, int pageSize, int pageIndex, string func);
        int CountGetUsersIncludeDeviceAndHobby(string search, string func);
        string ActionUser(RelationAction action, string userId, string currentUserId);
    }
}