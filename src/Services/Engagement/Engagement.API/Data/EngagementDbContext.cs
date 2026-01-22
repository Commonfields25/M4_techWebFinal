using Microsoft.EntityFrameworkCore;
using Engagement.API.Models;
namespace Engagement.API.Data;
public class EngagementDbContext : DbContext {
    public EngagementDbContext(DbContextOptions<EngagementDbContext> options) : base(options) { }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Rating> Ratings { get; set; }
}
