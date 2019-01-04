using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Entity;
using Entity.Model;
using Service.ViewModel;

namespace Service.Config
{
    public static class AutoMapperConfig
    {
        public static void Build()
        {
            Mapper.Initialize(cfg =>
            {
                #region User

                cfg.CreateMap<User, UserViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
                cfg.CreateMap<UserViewModel, User>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

                cfg.CreateMap<User, UserOtherViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
                cfg.CreateMap<UserOtherViewModel, User>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));


                cfg.CreateMap<User, UserViewProfile>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

                cfg.CreateMap<User, UserEditProfile>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null && (sourceMember as string != ""))));
                cfg.CreateMap<UserEditProfile, User>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null && (sourceMember as string != ""))));

                #endregion

                #region Device

                cfg.CreateMap<DeviceViewModel, Device>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
                cfg.CreateMap<Device, DeviceViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

                #endregion

                #region Hobby

                cfg.CreateMap<HobbyViewModel, Hobby>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
                cfg.CreateMap<Hobby, HobbyViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));


                #endregion


                #region Message
                cfg.CreateMap<MessageViewModel, Message>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null && (sourceMember as string != ""))));
                cfg.CreateMap<Message, MessageViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null && (sourceMember as string != ""))));

                #endregion

            });
        }
    }
}
