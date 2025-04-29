using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.DeleteAuthor{


    public class DeleteAuthorCommand{
        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId {get; set;}

        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(){
            var author = _dbContext.Authors.Include(x=> x.Books).SingleOrDefault(x=> x.Id == AuthorId);
            if(author == null)
                throw new InvalidCastException("Silinecek Yazar Bulunamadı");
            if(author.Books != null && author.Books.Any())
                throw new InvalidOperationException("Bu yazarın kayıtlı kitapları var, silinemez!");
            foreach(var book in author.Books){
                book.AuthorId = null;
            }    
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }

    }
}