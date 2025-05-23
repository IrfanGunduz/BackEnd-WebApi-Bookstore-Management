using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail{

    public class GetAuthorDetailQuery{
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int AuthorId {get; set;}

        public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle(){
            var author = _dbContext.Authors.SingleOrDefault(author=> author.Id == AuthorId);
            if(author == null)
                throw new InvalidOperationException("Yazar var olmadığı için bulunamadı");
            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);
            _dbContext.SaveChanges();
            return vm;
        }
    }

    public class AuthorDetailViewModel{
        public string Name {get; set;}
        public string Surname { get; set;}
        public DateTime BirthDate { get; set;}
    }
}