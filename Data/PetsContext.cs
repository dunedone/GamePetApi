using Microsoft.EntityFrameworkCore;
using GamePetApi.Models;

namespace GamePetApi.Data
{
    public class PetsContext : DbContext
    {
        public required DbSet<Pet> Pets { get; set; }

        public PetsContext(DbContextOptions<PetsContext> options)
            : base(options)
        {
            try
            {
                var temp = Pets!.Count();
            }
            catch
            {
                try
                {
                    Database.EnsureCreated();
                    var temp = Pets!.Count();
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
            builder.Entity<Pet>().HasData(
                new Pet
                {
                    Id = 1,
                    Name = "Rocky",
                    Species = "Dog",
                    Gender = "Male",
                    BirthDate = new DateTime(2020, 5, 15),
                    OwnerName = "John Doe"
                }
            );
        }
    }
}