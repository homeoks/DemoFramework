using MFramework.Controllers.BaseApiController;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Interface;

namespace MmFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly ICountryService _countryService;

        public UserController(IUserService userService, ICountryService countryService)
        {
            _userService = userService;
            _countryService = countryService;
        }

        [HttpGet]
        [Route("GetUserProfile")]
        public IActionResult GetUserProfile()
        {
            return Result(() => _userService.GetUserProfile(CurrentUser.Id));
        }

        [HttpPost]
        [Route("UpdateUserProfile")]
        public IActionResult UpdateUserProfile(UserEditProfile model)
        {
            return Result(() => _userService.UpdateUserProfile(model,CurrentUser.Id));
        }


        [HttpGet]
        [Route("GetOtherUserProfile")]
        public IActionResult GetOtherUserProfile()
        {
            return Result(() => _userService.GetOtherUserProfile(CurrentUser.Id));
        }

        [HttpGet]
        [Route("GetUserById")]
        public IActionResult GetUserById(string id)
        {
            return Result(() => _userService.GetUserById(id));
        }

        [HttpGet]
        [Route("GetCountries")]
        public IActionResult GetCountries()
        {
            return Result(() => _countryService.GetCountries());
        }

        [HttpGet]
        [Route("UploadFile")]
        public IActionResult UploadFile(FileContentResult file)
        {
            return Result(() => _countryService.GetCountries());
        }   

    }
}