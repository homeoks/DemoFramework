using System;
using System.Collections.Generic;
using Entity.Model;
using Infrastructure;
using Service.ViewModel;

namespace Service
{
    public class MessageViewModel
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Content{ get; set; }
        public DateTimeOffset DateReceive { get; set; }
        public string FromDate => $"{DateReceive.Date}/{DateReceive.Month}/{DateReceive.Year}, {DateReceive.Hour}:{DateReceive.Minute}";

    }
}
