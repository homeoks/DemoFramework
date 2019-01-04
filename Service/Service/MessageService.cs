using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using AutoMapper;
using Entity;
using Entity.Model;
using Infrastructure;
using Repository.Interface;
using Service.Interface;

namespace Service.Service
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public void SaveMessage(string message, string adminId, string userId)
        {
            var admin = _userRepository.GetById(adminId) ;
            var user = _userRepository.GetById(userId) ;
            var adminName = admin == null ? "Admin" : admin.UserName;
            var userName = user == null ? "All" : user.UserName;
            _messageRepository.Insert(new Message()
            {
                Content = message,
                FromUser = adminName,
                ToUser = userName,
            });
        }

        public ApplicationResult GetAllMessageWithUser(string userId, string withUser)
        {
            var user = _userRepository.GetById(userId);
            if(user==null) return ApplicationResult.Fail("User does not found");
            var results=_messageRepository.Gets(x => (x.ToUser == user.UserName && x.FromUser == withUser) ||
                                         (x.FromUser == user.UserName && x.ToUser == withUser));
    
            return ApplicationResult.Ok(results.Select(Mapper.Map<MessageViewModel>));
        }
    }
}
