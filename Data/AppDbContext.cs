using Microsoft.EntityFrameworkCore;
using GamePetApi.Models;

namespace GamePetApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }
}
