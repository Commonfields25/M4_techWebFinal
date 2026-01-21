using Microsoft.EntityFrameworkCore;
using M4Webapp.Models;

namespace M4Webapp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Resource> Resources { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Configuration Topic
        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Configuration Resource
        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Url).IsRequired();

            // Relation Many-to-One : Category
            entity.HasOne(d => d.Category)
                  .WithMany(p => p.Resources)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relation Many-to-Many : Topics
            entity.HasMany(p => p.Topics)
                  .WithMany(p => p.Resources)
                  .UsingEntity(j => j.ToTable("ResourceTopics"));
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
        });
    }
}
