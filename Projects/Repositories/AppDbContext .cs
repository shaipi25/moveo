using Dto;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProjectDto> Projects { get; set; }
    }
}
