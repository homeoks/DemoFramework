using System.Collections.Generic;
using Entity;
using Entity.Model;
using Repository.BaseRepository;

namespace Repository.Interface
{
    public interface IUserRelationShipRepository : IGenericRepository<UserRelationShip>
    {
        List<UserRelationShip> GetBlackList(string currentUserId);
    }
}