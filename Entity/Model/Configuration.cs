using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Model
{
    public class Configuration : BaseEntity
    {
        public Infrastructure.ConfigurationEnum.Group Group { get; set; }
        public Infrastructure.ConfigurationEnum.Key Key { get; set; }
        public string Value { get; set; }
    }
}
