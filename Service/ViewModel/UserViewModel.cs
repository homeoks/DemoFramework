using System;
using System.Collections.Generic;
using Entity.Model;
using Infrastructure;
using Service.ViewModel;

namespace Service
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public SexType SexType { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class UserEditProfile
    {
        public string Email { get; set; }
        public string Avatar { get; set; }
        public SexType SexType { get; set; }
        public string Country { get; set; }
        public string Note { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserViewProfile
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public SexType SexType { get; set; }
        public string Country { get; set; }
        public string Note { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserOtherViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public SexType SexType { get; set; }
        public string Country { get; set; }
        public string Hobbies{ get; set; }
        public string Avatar{ get; set; }
    }
}
