﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AutoMapper;
using Entity;
using Infrastructure;
using Infrastructure.Extensitions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repository.BaseRepository;
using Repository.Interface;
using Service.Config;
using Service.Interface;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRelationShipRepository _relationShipRepository;
        private readonly IConfiguration _configuration;
        private readonly IGenericRepository<User> _userGenericRepository;
        private readonly IGenericRepository<Message> _messageGenericRepository;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IUserRelationShipRepository relationShipRepository, IGenericRepository<User> genericRepository, IGenericRepository<Message> messageGenericRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _relationShipRepository = relationShipRepository;
            _userGenericRepository = genericRepository;
            _messageGenericRepository = messageGenericRepository;
        }

        public ApplicationResult<UserViewModel> SignUp(UserViewModel model)
        {
            model.UserName = model.UserName.ToLower();
            model.Email = model.Email.ToLower();
            var users = _userRepository.Gets(x => true);
            var existUser = users.FirstOrDefault(x => model.UserName==x.UserName);
            if (existUser != null)
                return ApplicationResult.Fail<UserViewModel>("The user already exists");
            var user = Mapper.Map<User>(model);
            SetPassword(user, model.PasswordHash);
            _userRepository.Insert(user);

            return ApplicationResult.Ok(Mapper.Map<UserViewModel>(user));
        }
        private void SetPassword(User user, string password)
        {
            var passwordHasher = new IdentityPasswordHasher();
            passwordHasher.HashPassword(user, password);
        }

        public ApplicationResult<List<User>> GetAllUser()
        {
            var t=_userGenericRepository.Gets(x => true);
            var m = _messageGenericRepository.Gets(x => true);
            return ApplicationResult.Ok(_userRepository.Gets(x => true));
        }

        public ApplicationResult<UserViewModel> GetUserByCredential(string username, string password)
        {
            username = username.ToLower();
            var user = _userRepository.Get(x => x.UserName == username);
            if (user == null)
                return ApplicationResult.Fail<UserViewModel>("The username does not exist");
            var isValidPassword = ValidatePassword(user, password);

            if (isValidPassword == false)
                return ApplicationResult.Fail<UserViewModel>("The password is not valid");

            if (user.IsDeleted)
                return ApplicationResult.Fail<UserViewModel>("The user is deleted");

            return ApplicationResult.Ok(Mapper.Map<UserViewModel>(user));
        }

        public ApplicationResult<UserViewModel> GetUserByRefreshToken(string refreshToken)
        {
            var user = _userRepository.Get(x => x.RefreshToken == refreshToken);
            if (user == null || user.RefreshTokenExpireDate < DateTimeOffset.Now)
                return ApplicationResult.Fail<UserViewModel>("The refresh token is not valid, or expired");

            if (user.IsDeleted)
                return ApplicationResult.Fail<UserViewModel>("The user is deleted");

            return ApplicationResult.Ok(Mapper.Map<UserViewModel>(user));
        }
        public string GrantRefreshToken(string userId)
        {
            var user = _userRepository.GetById(userId);

            if (user.IsDeleted)
                return "";

        var refreshToken = Guid.NewGuid().ToString("N").ConvertStringToBase64();
            user.RefreshToken = refreshToken;
            var expireDays = _configuration.GetSection("Token")["RefreshTokenExpireDays"];
            user.RefreshTokenExpireDate=DateTimeOffset.Now.AddDays(int.Parse(expireDays));
            _userRepository.Update(user,"Refresh Token",x=>x.RefreshToken,x=>x.RefreshTokenExpireDate);
            return user.RefreshToken;
        }

        public ApplicationResult GetUserProfile(string userId)
        {
            var user = _userRepository.GetById(userId);
           
            return ApplicationResult.Ok(Mapper.Map<UserViewProfile>(user));
        }

        public ApplicationResult UpdateUserProfile(UserEditProfile model, string userId)
        {
            var user = _userRepository.GetById(userId);
            if(user==null)
                return ApplicationResult.Fail("User does not found");
            Mapper.Map(model, user);
            _userRepository.Update(user);
            return ApplicationResult.Ok(Mapper.Map<UserViewProfile>(user));

        }

        public ApplicationResult GetOtherUserProfile(string search, int pageSize, int pageIndex, string id)
        {
            if (search == null) search = "";
            var data = _userRepository.GetUsersIncludeDeviceAndHobby(search,pageSize, pageIndex,id);

            var content=data.Select(x => new UserOtherViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                SexType = x.SexType,
                Hobbies = string.Join(',', x.Hobbies.Select(y => y.Name)),
                Country = x.Country,
                Avatar = x.Avatar,
                Status = x.Status
            }).ToList();

            var total = _userRepository.CountGetUsersIncludeDeviceAndHobby(search,id);

            var results = new PagingModel<UserOtherViewModel>(content, pageSize, pageIndex, total);

            return ApplicationResult.Ok(results);
        }

        public ApplicationResult GetUserById(string id, string currentUserId)
        {
            var relate= _relationShipRepository.Gets(x =>x.CurrentUserId == currentUserId && x.OtherUserId == id);
            var result = Mapper.Map<UserOtherViewModel>(_userRepository.GetById(id));
            result.IsFriend = relate.Any(x => x.IsFriend);
            result.IsBan = relate.Any(x => x.IsBlock);
            result.IsAway = relate.Any(x => x.Ignored);
            return ApplicationResult.Ok(result);
        }

        public ApplicationResult ActionUser(RelationAction action, string userId, string currentUserId)
        {
            var message = _userRepository.ActionUser(action, userId, currentUserId);
            return string.IsNullOrEmpty(message) ? ApplicationResult.Ok() : ApplicationResult.Fail(message);
        }

        public ApplicationResult GetBlackList(string currentUserId)
        {
            var data = _relationShipRepository.GetBlackList(currentUserId);

            var result = data.Select(x => new UserOtherViewModel()
            {
                Id = x.OtherUserId,
                UserName = x.OtherUser.UserName,
                SexType = x.OtherUser.SexType,
                Country = x.OtherUser.Country,
                Avatar = x.OtherUser.Avatar,
                Status = x.OtherUser.Status
            });

            return ApplicationResult.Ok(result);
        }

        private bool ValidatePassword(User user, string password)
        {
            
            var passwordHasher = new IdentityPasswordHasher();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
