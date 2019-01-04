using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MmFramework.ActionFilter;
using Service;
using Service.Interface;

namespace MFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseApiController.BaseApiController
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Route("GetAllMessageWithUser")]
        public IActionResult GetAllMessageWithUser(string withUser)
        {
            return Result(() => _messageService.GetAllMessageWithUser(CurrentUser.Id,withUser));
        }
    }
}