using Microsoft.EntityFrameworkCore;
using Inquiry.API.Models;
namespace Inquiry.API.Data;
public class InquiryDbContext : DbContext {
    public InquiryDbContext(DbContextOptions<InquiryDbContext> options) : base(options) { }
    public DbSet<ContactMessage> Messages { get; set; }
}
