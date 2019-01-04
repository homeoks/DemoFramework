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
    [Route(Endpoint + "game")]
    public class GameController : BaseMvcController
    {

        public GameController()
        {
            
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("roll")]
        [AllowAnonymous]
        public IActionResult Roll()
        {
            return View();
        }
        [HttpPost("roll")]
        [AllowAnonymous]
        public IActionResult Roll(int min,int max)
        {
            var rd=new Random().Next(max - min)+min;
            return Content(rd+"");
        }

    }
}
