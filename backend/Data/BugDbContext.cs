using Microsoft.EntityFrameworkCore;
using BugManagementAPI.Models;

namespace BugManagementAPI.Data
{
    public class BugDbContext : DbContext
    {
        public BugDbContext(DbContextOptions<BugDbContext> options) : base(options)
        {
        }

        public DbSet<Bug> Bugs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bug>().Property(b => b.Status).HasConversion<string>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
