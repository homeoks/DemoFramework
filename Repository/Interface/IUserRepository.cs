using System;
using System.Collections.Generic;
using Entity;
using Repository.BaseRepository;

namespace Repository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> GetUsersIncludeDeviceAndHobby(Func<User, bool> func);
    }
}