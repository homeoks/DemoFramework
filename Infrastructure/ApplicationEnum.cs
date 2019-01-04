using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Infrastructure
{
    [JsonConverter(typeof(StringEnumConverter))]
        public enum DeviceType
        {
            Phone,
            Tablet,
            Laptop,
        }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SexType
        {
            NotFound,
            Male,
            Female,
            Other,

        }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HobbyType
        {
            Music,
            Movie,
            People,
            Activity,
        }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        [Description("Online")]
        Online=1,
        [Description("Offline")]
        Offline = 2,
        [Description("Busy")]
        Busy = 3,
        [Description("Afk")]
        Afk= 4,
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RelationAction
    {
        [Description("Friend")]
        Friend = 1,
        [Description("Block")]
        Block = 2,
        [Description("Away")]
        Away = 3,
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TypeRelation
    {
        [Description("Normal")]
        Online=1,
        [Description("Family")]
        Offline = 2,
        [Description("BFF")]
        Busy = 3,
        [Description("FWB")]
        Afk= 4,
    }
}
