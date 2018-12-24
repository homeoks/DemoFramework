using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Entity.Base;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace MmFramework.ActionFilter
{
    //use [ClaimRequirement(ClaimTypes.Role,"Admin")]
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            if (hasClaim)
            {
                return;
            }

            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new CustomUnauthorizedResult("Blocked");
                return;
            }

            var token = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(token))
            {
                context.Result = new CustomUnauthorizedResult("Can't read token");
                return;
            }

            var jwtSecurityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var claims = jwtSecurityToken?.Claims;

           

            var userId = claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

            using (var db = new ApplicationDbContext())
            {
                var user = db.UserRoles.Include(x=>x.Role).FirstOrDefault(x => x.UserId == userId && x.Role!=null && x.Role.Name==_claim.Value);
                if (user == null)
                {
                    var result = ApplicationResult.Fail("No no no");
                    context.Result = new OkObjectResult(result);
                    return;
                }
            }
        }
    }
}
