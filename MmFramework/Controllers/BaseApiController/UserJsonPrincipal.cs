using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MFramework.Controllers.BaseApiController
{
    public class UserJsonPrincipal
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
