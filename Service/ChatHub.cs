using System;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Entity.Base;
using Microsoft.AspNetCore.SignalR;

namespace Service
{
    public class ChatHub : Hub
    {
        public async Task NewMessage(string username, string message)
        {

            using (var db = new ApplicationDbContext())
            {
                var connection = db.SignalR.FirstOrDefault(x => x.UserName == username);
                if(connection==null)
                db.SignalR.Add(new SignalR()
                {
                    ConnectionId = Context.ConnectionId,
                    UserName = username,
                    UserId = db.Users.First(x=>x.UserName==username).Id,
                });
                else
                {
                    connection.ConnectionId = Context.ConnectionId;
                    db.SignalR.Update(connection);
                }
                db.SaveChanges();
            }
            await Clients.All.SendAsync("messageReceived", username, message);
        }
        public async Task PrivateMessage(string fromUser,string toUser, string message)
        {
            using (var db = new ApplicationDbContext())
            {
                var fUser= db.SignalR.FirstOrDefault(x=>x.UserName==fromUser);
                var tUser = db.SignalR.FirstOrDefault(x => x.UserName == toUser);
                
                //add message history
                db.Messages.Add(new Message
                {
                    Content = message,
                    FromUser = fromUser,
                    ToUser = toUser,
                    TimeModifyOffset = DateTimeOffset.Now,
                    CreateBy = fUser?.UserId,
                    TimeCreatedOffset = DateTimeOffset.Now,
                    ModifyBy = fUser?.UserId,
                    DateReceive = DateTimeOffset.Now,
                });
                db.SaveChanges();

                await Clients.Client(tUser?.ConnectionId).SendAsync("privateMessageReceived", fromUser, toUser, message);
            }
          
        }

        public async Task SendMessageToAllUser(string message)
        {
            await Clients.All.SendAsync("adminSendMessage", message,true);
        }
    }
}