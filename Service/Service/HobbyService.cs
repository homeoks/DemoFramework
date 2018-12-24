using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using AutoMapper;
using Entity;
using Entity.Model;
using Infrastructure;
using Repository.Interface;
using Service.Interface;
using Service.ViewModel;

namespace Service.Service
{
    public class HobbyService : IHobbyService
    {
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IUserRepository _userRepository;

        public HobbyService(IUserRepository userRepository, IHobbyRepository hobbyRepository)
        {
            _userRepository = userRepository;
            _hobbyRepository = hobbyRepository;
        }

        public ApplicationResult AddNewHobby(HobbyViewModel model, string userId)
        {
            var user = _userRepository.GetById(userId);
            if(user==null) return ApplicationResult.Fail("User does not exist");
            model.UserId = userId;
            var device= _hobbyRepository.Insert(Mapper.Map<Hobby>(model));
            return ApplicationResult.Ok(Mapper.Map<HobbyViewModel>(device));
        }

        public ApplicationResult GetAllHobby(string id)
        {
           return ApplicationResult.Ok(_hobbyRepository.Gets(x=>x.IsDeleted==false && x.UserId==id));
        }
    }
}
