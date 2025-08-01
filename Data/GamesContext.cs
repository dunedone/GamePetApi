using Microsoft.EntityFrameworkCore;
using GamePetApi.Models;

namespace GamePetApi.Data
{
    public class GamesContext : DbContext
    {
        public required DbSet<VideoGame> Games { get; set; }
        public GamesContext(DbContextOptions<GamesContext> options)
            : base(options) 
        {
            try
            {
                var temp = Games.Count();
            }
            catch
            {
                try
                {
                    Database.EnsureCreated();
                    var temp = Games.Count();
                }
                catch
                {
                    Database.EnsureDeleted();
                    Database.EnsureCreated();
                }
            }
            Database.Migrate(); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<VideoGame>().HasData(
                new VideoGame
                {
                    Id = 1,
                    Title = "Minecraft",
                    YearReleased = 2011,
                    Developer = "Mojang AB, Xbox Game Studios",
                    Genre = "Sandbox",
                    Platform = "Windows, Mac, Linux, PlayStation, Xbox, Nintendo Switch, iPhone, iPad, Android"
                }
            );
        }
    }
}
