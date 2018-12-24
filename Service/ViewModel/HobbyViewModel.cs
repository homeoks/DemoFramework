using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure;

namespace Service.ViewModel
{
   public class HobbyViewModel
    {
        public string UserId { get; set; }
        public HobbyType HobbyType { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
