using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Infrastructure;
using MFramework.Controllers.BaseApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MmFramework.Controllers.BackOffice
{
    public class BaseMvcController : Controller
    {
        protected const string Endpoint = "portal/";
        protected UserJsonPrincipal CurrentUser => GetCurrentUser();
        public BaseMvcController()
        {
        }

        private UserJsonPrincipal GetCurrentUser()
        {
            var token = HttpContext.Request.Cookies["Authorization"];
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwtSecurityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                var claims = jwtSecurityToken.Claims;
                return new UserJsonPrincipal
                {
                    Id = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub).Value,
                    UserName = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
                    Claims = claims,
                    IsAdmin = claims.Any(x => x.Type == ClaimTypes.Role && x.Value == ApplicationSetting.Get().GetSection("SeedData")["RoleAdmin"]),
                    IsSuperAdmin = claims.Any(x => x.Type == ClaimTypes.Role && x.Value == ApplicationSetting.Get().GetSection("SeedData")["RoleSuperAdmin"])
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}