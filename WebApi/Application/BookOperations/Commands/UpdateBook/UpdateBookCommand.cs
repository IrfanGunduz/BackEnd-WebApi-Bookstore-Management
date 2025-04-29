using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook{

    public class UpdateBookCommand{

        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookModel Model {get; set;}
        public int BookId { get; set; }

        public UpdateBookCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(){

        var book = _context.Books.SingleOrDefault(x=> x.Id == BookId);

        if(book == null)
            throw new InvalidOperationException("Güncellenecek Kitap Bulunamadı!");

        _mapper.Map(Model, book);
        
        // book.GenreId  = Model.GenreId != default ? Model.GenreId : book.GenreId;
        // book.Title = Model.Title != default ? Model.Title : book.Title;

        _context.SaveChanges();
        }

    
    }
    public class UpdateBookModel{
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount {get; set;}
        public DateTime PublishDate {get; set;}
        
    }
}