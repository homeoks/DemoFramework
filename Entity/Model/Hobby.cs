using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Infrastructure;

namespace Entity.Model
{
    public class Hobby:BaseEntity
    {
        public HobbyType HobbyType{ get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User{ get; set; }
    }
}
