using Microsoft.EntityFrameworkCore;
using SalesLeadManagement.Api.Models;

namespace SalesLeadManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Lead> Leads => Set<Lead>();

        public DbSet<LeadActivity> LeadActivities =>
            Set<LeadActivity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lead>()
                .HasKey(l => l.LeadId);

            modelBuilder.Entity<LeadActivity>()
                .HasKey(a => a.ActivityId);

            modelBuilder.Entity<Lead>()
                .HasMany(l => l.Activities)
                .WithOne(a => a.Lead)
                .HasForeignKey(a => a.LeadId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LeadActivity>()
                .HasIndex(a => new { a.LeadId, a.CreatedAt });
        }
    }
}