using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Entity.Base;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MmFramework.ActionFilter;

namespace MmFramework.Auth
{
    public class MmAttribute : TypeFilterAttribute
    {
        public MmAttribute() : base(typeof(MmFilter))
        {
            //Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class MmFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return;
            }

            var token = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(token))
            {
                context.Result = new CustomUnauthorizedResult("Can't read token");
            }

            var jwtSecurityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var claims = jwtSecurityToken?.Claims;

            var userId = claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            //  Check user is active
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.First(x => x.Id == userId);
                if (user == null)
                {
                    var result = ApplicationResult.Fail("User does not exist");
                    context.Result = new OkObjectResult(result);
                    return;
                }
            }
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return;
            }

            var token = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString().Split(" ")[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(token))
            {
                context.Result = new CustomUnauthorizedResult("Can't read token");
            }

            var jwtSecurityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var claims = jwtSecurityToken?.Claims;

            var userId = claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            //  Check user is active
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.First(x => x.Id == userId);
                if (user == null)
                {
                    var result = ApplicationResult.Fail("User does not exist");
                    context.Result = new OkObjectResult(result);
                    return;
                }
            }
        }
      
    }
}