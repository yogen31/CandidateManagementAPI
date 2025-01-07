using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Database.Context
{
    public class CandidateManagementAPIDbContext : DbContext
    {
        public CandidateManagementAPIDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Candidate> Candidates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make the Email field unique
            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
