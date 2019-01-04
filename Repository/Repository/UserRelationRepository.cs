using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Entity;
using Entity.Base;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using Repository.Interface;

namespace Repository.Repository
{
    public class UserRelationShipRepository : GenericRepository<UserRelationShip>, IUserRelationShipRepository
    {
        public List<UserRelationShip> GetBlackList(string currentUserId)
        {
            using (var db = new ApplicationDbContext())
            {
                var results = db.UserRelationShips.Include(x=>x.OtherUser).Where(x => x.Ignored && x.CurrentUserId == currentUserId);
                return results.ToList();
            }
        }
    }
}
