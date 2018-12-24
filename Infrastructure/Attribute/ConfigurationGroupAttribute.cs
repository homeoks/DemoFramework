using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Attribute
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ConfigurationGroupAttribute : System.Attribute
    {
        public ConfigurationEnum.Group Group { get; set; }
        public string DefaultValue { get; set; }
        public ConfigurationGroupAttribute(ConfigurationEnum.Group group, string defaulValue = null)
        {
            Group = group;
            DefaultValue = defaulValue ?? string.Empty;
        }
    }
}
