using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBookDetail{

    public class GetBookDetailQuery{

        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId {get; set;}
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle(){
            var book = _dbContext.Books.Include(x=> x.Genre).Include(x=> x.Author).Where(book=> book.Id == BookId).SingleOrDefault();
            if(book == null)
                throw new InvalidOperationException("Kitap bulunamadı!");
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book); 
            //new BookDetailViewModel();
            // vm.Title = book.Title;
            // vm.PageCount = book.PageCount;
            // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            // vm.Genre = ((GenreEnum)book.GenreId).ToString();
            _dbContext.SaveChanges();
            return vm;   
        }


        
        
    }

    public class BookDetailViewModel{
            public string Title {get; set;}
            public string Genre {get; set;} 
            public string Author {get; set;}
            public int PageCount {get; set;} 
            public string PublishDate {get; set;}
        }
    
}