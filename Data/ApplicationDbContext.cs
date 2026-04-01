using BookAuthors.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BookAuthors.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "Stephen King" },
                new Author { AuthorId = 2, Name = "Peter Straub" },
                new Author { AuthorId = 3, Name = "J.K. Rowling" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "The Shining" },
                new Book { BookId = 2, Title = "The Talisman" },
                new Book { BookId = 3, Title = "Harry Potter and the Sorcerer's Stone" }
            );

            // Seed the many-to-many join table
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithMany(b => b.Authors)
                .UsingEntity(j => j.HasData(
                    new { AuthorsAuthorId = 1, BooksBookId = 1 },  // Stephen King wrote The Shining
                    new { AuthorsAuthorId = 1, BooksBookId = 2 },  // Stephen King co-wrote The Talisman
                    new { AuthorsAuthorId = 2, BooksBookId = 2 },  // Peter Straub co-wrote The Talisman
                    new { AuthorsAuthorId = 3, BooksBookId = 3 }   // J.K. Rowling wrote Harry Potter
                    ));

        }
    }
}
