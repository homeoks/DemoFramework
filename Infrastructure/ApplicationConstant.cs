using System;
using System.ComponentModel;
using Infrastructure.Attribute;

namespace Infrastructure
{
    public class ApplicationConstant
    {
        public const int TokenMinutes = 15;
        public const string Signature = "--GO fuck yourself--";


        
    }
    public class ConfigurationEnum
    {
        public enum Group
        {
            Group1 = 100,
            Group2 = 200,
            Group3 = 300,
        }

        public enum Key
        {
            //  Blockchain group
            [ConfigurationGroup(Group.Group1, "DefaultValue")]
            [DisplayName("Identifier")]
            Item1 = 101,
            [ConfigurationGroup(Group.Group1, "DefaultValue@")]
            [DisplayName("Password")]
            Item2 = 102,
            [ConfigurationGroup(Group.Group1, "DefaultValue")]
            [DisplayName("Api Code")]
            Item3 = 103,
            [ConfigurationGroup(Group.Group1, "DefaultValue")]
            [DisplayName("Address")]
            Item4 = 104,
            //Reward Setting

            [ConfigurationGroup(Group.Group2, "0")]
            [DisplayName("DailyReward")]
            DailyReward = 201,


            [ConfigurationGroup(Group.Group2, "0")]
            [DisplayName("WeeklyReward")]
            WeeklyReward = 202,

            [ConfigurationGroup(Group.Group2, "0")]
            [DisplayName("Weekly point")]
            WeeklyPointReward = 203,

            [ConfigurationGroup(Group.Group2, "0")]
            [DisplayName("Daily point")]
            DailyPointReward = 204,

            //Account Admin
            [ConfigurationGroup(Group.Group3, "Account-1")]
            [DisplayName("AccountAdmin1")]
            AccountAdmin1 = 301,
            [ConfigurationGroup(Group.Group3, "Account-2")]
            [DisplayName("AccountAdmin2")]
            AccountAdmin2 = 302,
        }
    }
}
