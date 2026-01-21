using M4Webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace M4Webapp.Repositories;

public class DbMessageRepository : IMessageRepository
{
    private readonly Data.AppDbContext _context;

    public DbMessageRepository(Data.AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Message>> GetAllAsync()
    {
        return await _context.Messages.ToListAsync();
    }
}
