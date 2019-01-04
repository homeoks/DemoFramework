using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Interface;
using Entity.Model;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Entity
{
    public class User :IdentityUser, IEntity<string>,IDeleteEntity,IAuditEntity
    {
        public string Avatar { get; set; }
        public string Note { get; set; }
        public SexType SexType { get; set; }
        public Status Status { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpireDate { get; set; }
  
        public string Country { get; set; }
        public virtual ICollection<Device> Devices{ get; set; }
        public virtual ICollection<Hobby>Hobbies{ get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTimeOffset? TimeDeletedOffset { get; set; }
        public string CreateBy { get; set; }
        public DateTimeOffset TimeCreatedOffset { get; set; }
        public DateTimeOffset? TimeModifyOffset { get; set; }
        public string ModifyBy { get; set; }
    }
}
