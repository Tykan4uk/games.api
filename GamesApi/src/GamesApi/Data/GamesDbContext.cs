using Microsoft.EntityFrameworkCore;

namespace GamesApi.Data
{
    public class GamesDbContext : DbContext
    {
        public GamesDbContext(DbContextOptions<GamesDbContext> options)
            : base(options)
        {
        }

        public DbSet<GameEntity> Games { get; set; } = null!;
    }
}