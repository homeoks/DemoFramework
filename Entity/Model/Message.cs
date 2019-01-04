using System;
using System.Collections.Generic;
using System.Text;
using Entity.Interface;

namespace Entity
{
    public class Message : BaseEntity
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public DateTimeOffset DateReceive { get; set; }
        public string Content { get; set; }
    }
}
