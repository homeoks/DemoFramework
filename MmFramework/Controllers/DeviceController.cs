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
    public class DeviceController : BaseApiController.BaseApiController
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        [Route("GetDevices")]
        public IActionResult GetAllDevice(int pageSize=10,int pageIndex=1)
        {
            return Result(() => _deviceService.GetAllDevice(CurrentUser.Id,pageSize,pageIndex));
        }


        [HttpPost]
        [Route("AddNewDevice")]
        public IActionResult AddNewDevice(DeviceViewModel model)
        {
            return Result(() => _deviceService.AddNewDevice(model, CurrentUser.Id));
        }
        [HttpPost]
        [Route("DeleteDevice")]
        public IActionResult DeleteDevice(int id)
        {
            return Result(() => _deviceService.DeleteDevice(id));
        }
        [HttpPost]
        [Route("UpdateDevice")]
        public IActionResult UpdatesDevice(DeviceEditModel model)
        {
            return Result(() => _deviceService.UpdateDevice(model));
        }

    }
}