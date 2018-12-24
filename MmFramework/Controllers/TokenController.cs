using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure;
using MFramework.Controllers.BaseApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service;
using Service.Interface;
using Service.ViewModel;

namespace MmFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public TokenController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public IActionResult Auth(TokenAuthRequest model)
        {
            return Result(() =>
            {
                if (model == null)
                    return ApplicationResult.Fail("bad request");
                UserViewModel user;

                switch (model.GrantType)
                {
                    case "password":
                        var credentialResult = _userService.GetUserByCredential(model.UserName, model.Password);
                        if (credentialResult.IsSuccess == false)
                            return credentialResult;
                        user = credentialResult.Value;
                        break;
                    case "refresh_token":
                        var refreshTokenResult = _userService.GetUserByRefreshToken(model.RefreshToken);
                        if (refreshTokenResult.IsSuccess == false)
                            return refreshTokenResult;
                        user = refreshTokenResult.Value;
                        break;



                    case "signUp": case "string":
                        if (!ModelState.IsValid)
                        {
                            return ApplicationResult.Fail(GetModelErrors());
                        }

                        //return user Id
                        var signUpResult = _userService.SignUp(new UserViewModel()
                        {
                            UserName = model.UserName.ToLower(),
                            Email = model.Email,
                            PasswordHash = model.Password,
                            Avatar = model.Avatar,
                            Country = model.Country,
                            SexType = model.SexType,
                            PhoneNumber = model.Phone
                        });
                        
                        if (signUpResult.IsSuccess == false)
                            return signUpResult;

                        user = signUpResult.Value;
                        break;
                    default:
                        return ApplicationResult.Fail("The grant_type is not support");
                }

                var refreshToken = _userService.GrantRefreshToken(user.Id);
                var accessToken = GenerateJwtToken(user);
                var result = new TokenAuthResponse
                {
                    RefreshToken = refreshToken,
                    AccessToken = accessToken
                };
                return ApplicationResult.Ok(result);
            });
        }

        private string GenerateJwtToken(UserViewModel user)
        {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email,user.Email)
                };

                var audienceConfig = _configuration.GetSection("Audience");
                var secret = audienceConfig["Secret"];
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(audienceConfig["JwtExpireMinutes"]));

                var token = new JwtSecurityToken(
                    audienceConfig["Iss"],
                    audienceConfig["Aud"],
                    claims,
                    expires: expires,
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}