using Microsoft.EntityFrameworkCore;

namespace GameLibraryAPI.Models
{
    public class GameLibraryDBContext : DbContext
    {
        public GameLibraryDBContext(DbContextOptions<GameLibraryDBContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Platform> Platforms { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
    }
}
