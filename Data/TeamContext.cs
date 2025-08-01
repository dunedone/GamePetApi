using Microsoft.EntityFrameworkCore;
using GamePetApi.Models;

namespace GamePetApi.Data
{
    public class TeamContext : DbContext
    {
        public required DbSet<TeamMember> Team { get; set; }
        public TeamContext(DbContextOptions<TeamContext> options)
            : base(options) 
        {
            try
            {
                var temp = Team.Count();
            }
            catch
            {
                try
                {
                    Database.EnsureCreated();
                    var temp = Team.Count();
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
            builder.Entity<TeamMember>().HasData(
                new TeamMember
                {
                    Id = 1,
                    FirstName = "Samuel",
                    LastName = "Bosch-Bird",
                    BirthDate = new DateOnly(2003, 11, 06),
                    Program = "IT Certificate",
                    Year = "N/A"
                }
            );
        }
    }
}
