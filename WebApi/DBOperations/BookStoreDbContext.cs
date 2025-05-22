using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations{



    public class BookStoreDbContext : DbContext, IBookStoreDbContext{

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options): base(options)
        { }

        public DbSet<Book> Books {get; set;}
        public DbSet<Genre> Genres {get; set;}
        public DbSet<Author> Authors {get; set;}
        public DbSet<User> Users {get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)                     
                .WithMany(a => a.Books)                              
                .HasForeignKey(b => b.AuthorId)          
                .OnDelete(DeleteBehavior.SetNull);

                 modelBuilder.Entity<Book>()
                .HasOne(b => b.Genre)
                .WithMany() 
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.SetNull);




            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Personal Growth" },
                new Genre { Id = 2, Name = "Science Fiction" },
                new Genre { Id = 3, Name = "Romance" }

            );

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Arthur", Surname = "Morgan", BirthDate = new DateTime(1863, 06, 12)},
                new Author { Id = 2, Name = "John", Surname = "Marston", BirthDate = new DateTime(1873, 05, 23)},
                new Author { Id = 3, Name = "Dutch", Surname = "Van Der Linde", BirthDate = new DateTime(1855, 12, 21)}
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Lean Startup", GenreId = 1, AuthorId = 1, PageCount = 200, PublishDate = new DateTime(2001, 06, 12) },
                new Book { Id = 2, Title = "Herland", GenreId = 2, AuthorId = 2, PageCount = 250, PublishDate = new DateTime(2010, 05, 23) },
                new Book { Id = 3, Title = "Dune", GenreId = 3, AuthorId = 3, PageCount = 540, PublishDate = new DateTime(2001, 12, 21) }
            );
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}