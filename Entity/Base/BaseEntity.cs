using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.Interface;
using Microsoft.AspNetCore.Identity;

namespace Entity
{
    public class BaseEntity : IEntity<int>,IDeleteEntity,IAuditEntity
    {
        public BaseEntity()
        {
            TimeCreatedOffset = DateTimeOffset.Now;
            TimeModifyOffset = DateTimeOffset.Now;
        }

        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CreateBy { get; set; }
        public DateTimeOffset TimeCreatedOffset { get; set; }
        public DateTimeOffset? TimeModifyOffset { get; set; }
        public string ModifyBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? TimeDeletedOffset { get; set; }
        public string DeletedBy { get; set; }

    }
}
