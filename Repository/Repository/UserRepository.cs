using System;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Entity.Base;
using Entity.Model;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using Repository.Interface;

namespace Repository.Repository
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        public List<User> GetUsersIncludeDeviceAndHobby(string search, int pageSize, int pageIndex, string id)
        {
            using (var db=new ApplicationDbContext())
            {
                //user blocked current user or current user faded
                var ignoreUser = db.UserRelationShips.Where(x => (x.Ignored && x.CurrentUserId == id)).Select(x => x.OtherUserId).ToList();
                var blockedUser = db.UserRelationShips.Where(x => (x.IsBlock && x.OtherUserId == id)).Select(x => x.CurrentUserId).ToList();
                blockedUser.AddRange(ignoreUser);
                return db.Users
                    .Include(x => x.Hobbies)
                    .Include(x => x.Devices)
                    .Where(x => x.IsDeleted == false && x.Id != id && !blockedUser.Contains(x.Id) && x.UserName.Contains(search)).OrderBy(x=>x.UserName)
                    .Skip(pageSize*(pageIndex-1))
                    .Take(pageSize).ToList();
            }
        }
        public int CountGetUsersIncludeDeviceAndHobby(string search, string id)
        {
            using (var db = new ApplicationDbContext())
            {
                //user blocked current user or current user faded
                var ignoreUser = db.UserRelationShips.Where(x => (x.Ignored && x.CurrentUserId == id)).Select(x => x.OtherUserId).ToList();
                var blockedUser = db.UserRelationShips.Where(x => (x.IsBlock && x.OtherUserId == id)).Select(x => x.CurrentUserId).ToList();
                blockedUser.AddRange(ignoreUser);
                return db.Users.Count(x => x.IsDeleted == false && x.Id != id && !blockedUser.Contains(x.Id) && x.UserName.Contains(search));
            }
        }
        public string ActionUser(RelationAction action, string userId, string currentUserId)
        {
            using (var db = new ApplicationDbContext())
            {
                var currentUser= db.Users.Single(x => x.Id == currentUserId);
                if (currentUser == null) return "Current User does not exist";

                var user= db.Users.Single(x => x.Id == userId);
                if (user == null) return "User does not exist";

                var relation =
                    db.UserRelationShips.FirstOrDefault(x => x.CurrentUserId == currentUserId && x.OtherUserId == userId);

                if(relation==null)
                relation=new UserRelationShip()
                {
                    CurrentUserId = currentUserId,
                    OtherUserId = userId,
                    CreateBy = currentUserId,
                };
                switch (action)
                {
                    case RelationAction.Away:
                        relation.Ignored = !relation.Ignored;
                        break;

                    case RelationAction.Friend:
                        relation.IsFriend = !relation.IsFriend;
                        var otherRelation =
                            db.UserRelationShips.FirstOrDefault(x => x.CurrentUserId == userId && x.OtherUserId == currentUserId);

                        if (otherRelation == null)
                            otherRelation = new UserRelationShip()
                            {
                                CurrentUserId = userId ,
                                OtherUserId = currentUserId,
                                CreateBy = userId ,
                            };
                        otherRelation.IsFriend = relation.IsFriend;
                        break;

                    case RelationAction.Block:
                        relation.IsBlock = !relation.IsBlock;
                        break;
                }

                if (relation.Id == 0)
                    db.UserRelationShips.Add(relation);
                else
                    db.UserRelationShips.Update(relation);
                db.SaveChanges();
                return string.Empty;
            }
        }
    }
}
