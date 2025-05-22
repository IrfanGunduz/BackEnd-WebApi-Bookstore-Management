using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;

namespace WebApi.Entities{

    public class Author{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public string Name {get; set;}

        public string Surname { get; set; }

        public DateTime BirthDate {get; set;}
        //public int BookId {get; set;}
        public ICollection<Book> Books {get; set;}


    }



}