using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Interface
{
    public interface IAuditEntity
    {
        [MaxLength(100)]
        string CreateBy { get; set; }
        DateTimeOffset TimeCreatedOffset { get; set; }
        DateTimeOffset? TimeModifyOffset { get; set; }
        [MaxLength(100)]
        string ModifyBy { get; set; }
    }
}