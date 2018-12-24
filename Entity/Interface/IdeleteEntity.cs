using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Interface
{
    public interface IDeleteEntity
    {
        bool IsDeleted { get; set; }
        [MaxLength(100)]
        string DeletedBy { get; set; }
        DateTimeOffset? TimeDeletedOffset { get; set; }
    }
}