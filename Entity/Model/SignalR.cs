using System;
using System.Collections.Generic;
using System.Text;
using Entity.Interface;

namespace Entity
{
    public class SignalR : IEntity<int>
    {
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
    }
}
