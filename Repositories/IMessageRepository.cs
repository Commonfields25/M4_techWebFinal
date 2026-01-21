using M4Webapp.Models;

namespace M4Webapp.Repositories;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    Task<IEnumerable<Message>> GetAllAsync();
}
