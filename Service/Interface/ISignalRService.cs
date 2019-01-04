using Infrastructure;

namespace Service.Interface
{
    public interface ISignalRService
    {
        string GetConnectionId(string id);
    }
}