using Microsoft.EntityFrameworkCore;

namespace aspnetcore_rest_api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Workout> Workouts { get; set; }
    }
}
