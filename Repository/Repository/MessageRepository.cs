using Entity;
using Entity.Model;
using Repository.BaseRepository;
using Repository.Interface;

namespace Repository.Repository
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
    }
}
