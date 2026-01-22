using Microsoft.EntityFrameworkCore;
using Catalog.API.Models;
namespace Catalog.API.Data;
public class CatalogDbContext : DbContext {
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
    public DbSet<Resource> Resources { get; set; }
}
