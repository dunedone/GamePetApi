using Microsoft.EntityFrameworkCore;
using GamePetApi.Models;

namespace GamePetApi.Data
{
    public class BookContext : DbContext
    {
        public required DbSet<Book> Books { get; set; }

        public BookContext(DbContextOptions<BookContext> options)
            : base(options)
        {
            try
            {
                var temp = Books!.Count();
            }
            catch
            {
                try
                {
                    Database.EnsureCreated();
                    var temp = Books!.Count();
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
            builder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    PublicationYear = 1937
                }
            );
        }
    }
}