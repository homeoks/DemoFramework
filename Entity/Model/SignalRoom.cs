using System;
using System.Collections.Generic;
using System.Text;
using Entity.Interface;

namespace Entity
{
    public class SignalRoom : IEntity<int>
    {
        public string FromUser { get; set; }
        public bool ToUser { get; set; }
        public int Id { get; set; }
        public string RoomName { get; set; }
    }
}
