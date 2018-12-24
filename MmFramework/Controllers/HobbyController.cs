using MFramework.Controllers.BaseApiController;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.ViewModel;

namespace MmFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbyController : BaseApiController
    {
        private readonly IHobbyService _hobbyService;

        public HobbyController(IHobbyService hobbyService)
        {
            _hobbyService = hobbyService;
        }

        [HttpGet]
        [Route("GetHobbies")]
        public IActionResult GetAllDevice()
        {
            return Result(() => _hobbyService.GetAllHobby(CurrentUser.Id));
        }


        [HttpPost]
        [Route("AddNewHobby")]
        public IActionResult AddNewHobby(HobbyViewModel model)
        {
            return Result(() => _hobbyService.AddNewHobby(model, CurrentUser.Id));
        }

    }
}