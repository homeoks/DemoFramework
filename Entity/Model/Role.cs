using System;
using System.Collections.Generic;
using System.Text;
using Entity.Interface;

namespace Entity
{
    public class Role : IEntity<int>
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
