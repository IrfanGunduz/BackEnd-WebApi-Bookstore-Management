using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.CreateAuthor{


    public class CreateAuthorCommand{
        public CreateAuthorModel Model {get; set;}
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle(){
            var author = _dbContext.Authors.SingleOrDefault(x=> x.Name == Model.Name);
            if(author!= null)
                throw new InvalidOperationException("Yazar Zaten Mevcut!");
            author = _mapper.Map<Author>(Model);
            _dbContext.Authors.Add(author);
            if(Model.BookId != null){
                var book = _dbContext.Books.SingleOrDefault(b=> b.Id == Model.BookId && b.AuthorId == null);

                if(book == null)
                    throw new InvalidOperationException("Bu kitap zaten bir yazara atanmış veya mevcut değil!");
                book.AuthorId = author.Id;
                _dbContext.SaveChanges();
            }
            
            _dbContext.SaveChanges();
        }

    }

     public class CreateAuthorModel{
     public string Name {get; set;}
     public string Surname {get; set;}
     public DateTime BirthDate {get; set;}
     public int? BookId {get; set;}
    }
}