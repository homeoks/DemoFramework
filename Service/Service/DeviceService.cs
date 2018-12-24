using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using AutoMapper;
using Entity;
using Entity.Model;
using Infrastructure;
using Repository.Interface;
using Service.Interface;

namespace Service.Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUserRepository _userRepository;

        public DeviceService(IDeviceRepository deviceRepository, IUserRepository userRepository)
        {
            _deviceRepository = deviceRepository;
            _userRepository = userRepository;
        }

        public ApplicationResult AddNewDevice(DeviceViewModel model, string userId)
        {
            var user = _userRepository.GetById(userId);
            if(user==null) return ApplicationResult.Fail("User does not exist");
            model.UserId = userId;
            var device=_deviceRepository.Insert(Mapper.Map<Device>(model));
            return ApplicationResult.Ok(Mapper.Map<DeviceViewModel>(device));
        }

        public ApplicationResult GetAllDevice(string id, int pageSize, int pageIndex)
        {
            return ApplicationResult.Ok(_deviceRepository.Gets(x => x.IsDeleted == false && x.UserId == id));
        }

        public ApplicationResult DeleteDevice(int id)
        {
            _deviceRepository.Delete(id);
            return ApplicationResult.Ok();
        }

        public ApplicationResult UpdateDevice(DeviceEditModel model)
        {
            var device = _deviceRepository.GetById(model.Id);
            if(device==null) return ApplicationResult.Fail("Device not found");

           Mapper.Map(model,device);

            _deviceRepository.Update(device);
            return ApplicationResult.Ok();
        }
    }
}
