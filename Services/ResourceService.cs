using M4Webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace M4Webapp.Services;

public interface IResourceService
{
    Task<IEnumerable<Resource>> GetAllAsync();
    Task<Resource?> GetByIdAsync(int id);
    Task CreateAsync(Resource resource);
    Task UpdateAsync(Resource resource);
    Task DeleteAsync(int id);
}

public class DbResourceService : IResourceService
{
    private readonly Data.AppDbContext _context;

    public DbResourceService(Data.AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        return await _context.Resources
            .Include(r => r.Category)
            .Include(r => r.Topics)
            .ToListAsync();
        return await _context.Resources.ToListAsync();
    }

    public async Task<Resource?> GetByIdAsync(int id)
    {
        return await _context.Resources
            .Include(r => r.Category)
            .Include(r => r.Topics)
            .FirstOrDefaultAsync(m => m.Id == id);
        return await _context.Resources.FindAsync(id);
    }

    public async Task CreateAsync(Resource resource)
    {
        _context.Resources.Add(resource);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Resource resource)
    {
        _context.Entry(resource).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var resource = await _context.Resources.FindAsync(id);
        if (resource != null)
        {
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }
    }
}
