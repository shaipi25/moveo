using Dto;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskDto> Tasks { get; set; }

        public DbSet<ProjectDto> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskDto>()
                    .HasOne<ProjectDto>()
                    .WithMany()
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
