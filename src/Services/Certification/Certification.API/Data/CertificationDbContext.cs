using Microsoft.EntityFrameworkCore;
using Certification.API.Models;
namespace Certification.API.Data;
public class CertificationDbContext : DbContext {
    public CertificationDbContext(DbContextOptions<CertificationDbContext> options) : base(options) { }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<UserProgress> UserProgresses { get; set; }
}
