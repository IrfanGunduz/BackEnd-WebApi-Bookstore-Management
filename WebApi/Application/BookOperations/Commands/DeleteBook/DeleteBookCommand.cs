using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook{

    public class DeleteBookCommand{

        private readonly IBookStoreDbContext _dbContext;
        public int BookId {get; set;}
        public DeleteBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
    
    
        public void Handle(){
        var book = _dbContext.Books.SingleOrDefault(x=>x.Id == BookId);
        if(book == null)
            throw new InvalidCastException("Silinecek kitap bulunamadı");
        
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();


        }
    
    
    
    
    }


}