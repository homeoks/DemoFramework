using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Base;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using Repository.Interface;

namespace Repository.Repository
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public List<User> GetUsersIncludeDeviceAndHobby(Func<User, bool> func)
        {
            using (var db=new ApplicationDbContext())
            {
                return db.Users.Include(x => x.Hobbies).Include(x => x.Devices).Where(func).ToList();
            }
        }
    }
}
