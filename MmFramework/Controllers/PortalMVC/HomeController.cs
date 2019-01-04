using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MmFramework.Controllers.BackOffice;
using Service;
using Service.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MmFramework.Controllers.PortalMVC
{
    [Route(Endpoint)]
    [Route("")]
    [Route(Endpoint + "home")]
    public class HomeController : BaseMvcController
    {
        private readonly IUserService _userService;
        private readonly  ISignalRService _signalRService;
        private readonly IMessageService _messageService;
        private readonly IConfiguration _configuration;
        private IHubContext<ChatHub> HubContext{get;set;}

        public HomeController(IUserService userService, IHubContext<ChatHub> hubContext, 
            ISignalRService signalRService, IMessageService messageService, IConfiguration configuration)
        {
            HubContext = hubContext;
            _signalRService = signalRService;
            _messageService = messageService;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index([FromForm] LoginUserModel model)
        {
            if(model.UserName==null)
             return View();

            var result= _userService.GetUserByCredential(model.UserName, model.Password);

            

           if (result.IsSuccess)
           {
               SignInAsync(result.Value);
               return RedirectToAction("Test");
           }
           return View();
        }

        private void SignInAsync(UserViewModel model)
        {
            var token = GenerateJwtToken(model);
            HttpContext.Response.Cookies.Append("Authorization", token);
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

        [HttpGet("test")]
        public IActionResult Test()
        {
            var users = _userService.GetAllUser();
            return View(users.Value);
        }

        [HttpPost("send")]
        public IActionResult Send(string message,string id)
        {
            if(id==null)
            Task.Run(()=> HubContext.Clients.All.SendAsync("adminSendMessage", message));
            else
            {
                id = id.Replace(" ", "").Replace("\n", "");
              var connectionId = _signalRService.GetConnectionId(id);
              if(connectionId!=string.Empty)
                  Task.Run(() => HubContext.Clients.Clients(connectionId).SendAsync("adminSendMessage", message));
            }

            if (!string.IsNullOrEmpty(message))
            {
                var adminName = CurrentUser == null ? "Admin" : CurrentUser.Id;
                _messageService.SaveMessage(message, adminName, id);
            }
            return RedirectToAction("Test");
        }
    }
}
