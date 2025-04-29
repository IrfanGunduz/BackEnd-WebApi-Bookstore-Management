using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup{
    public static class Authors{
        public static void AddAuthors(this BookStoreDbContext context){
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
        }
    }
}