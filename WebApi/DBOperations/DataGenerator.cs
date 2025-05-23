using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations{

    public class DataGenerator{

        public static void Initilize(IServiceProvider serviceProvider){
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if(context.Books.Any()){
                    return;
                }
                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }

                );
                context.Books.AddRange(
                    new Book{
                        // Id = 1,
                        Title="Lean Startup",
                        GenreId = 1,
                        AuthorId = 1, 
                        PageCount = 200,
                        PublishDate = new DateTime(2001,06,12)
                    },
                        new Book{
                        // Id = 2,
                        Title="Herland",
                        GenreId = 2,
                        AuthorId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2010,05,23)
                    },
                        new Book{
                        // Id = 3,
                        Title="Dune",
                        GenreId = 2,
                        AuthorId = 3, 
                        PageCount = 540,
                        PublishDate = new DateTime(2001,12,21)
                    });
                    context.Authors.AddRange(
                        new Author{
                        // Id = 1,
                        Id=1,
                        Name = "Arthur", 
                        Surname = "Morgan",
                        BirthDate = new DateTime(1863,06,12)
                    },
                        new Author{
                        // Id = 2,
                        Id=2,
                        Name = "John", 
                        Surname = "Marston",
                        BirthDate = new DateTime(1873,05,23)
                    },
                        new Author{
                        // Id = 3,
                        Id=3,
                        Name = "Dutch", 
                        Surname = "Van Der Linde",
                        BirthDate = new DateTime(1855,12,21)
                    });


                    context.SaveChanges();
                }
            }
        }
    }

