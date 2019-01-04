using Infrastructure;
using Infrastructure;

namespace Service.Interface
{
    public interface IDeviceService
    {
        ApplicationResult AddNewDevice(DeviceViewModel model, string currentUserId);
        ApplicationResult GetAllDevice(string currentUserId, int pageSize, int pageIndex);
        ApplicationResult DeleteDevice(int id);
        ApplicationResult UpdateDevice(DeviceEditModel model);
    }
}