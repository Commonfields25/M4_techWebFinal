using Microsoft.EntityFrameworkCore;
using Taxonomy.API.Models;
namespace Taxonomy.API.Data;
public class TaxonomyDbContext : DbContext {
    public TaxonomyDbContext(DbContextOptions<TaxonomyDbContext> options) : base(options) { }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Topic> Topics { get; set; }
}
