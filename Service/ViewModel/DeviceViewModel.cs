using System;
using Infrastructure;

namespace Service
{
    public class DeviceViewModel
    {
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public string UserId { get; set; }
    }

    public class DeviceEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public string UserId { get; set; }
    }
    public class DeviceDeleteModel
    {
        public int Id { get; set; }
    }
}
