using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.UpdateAuthor{

    public class UpdateAuthorCommand{
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int AuthorId {get; set;}
        public UpdateAuthorModel Model {get; set;}

        public UpdateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(){
            var author = _context.Authors.SingleOrDefault(x=> x.Id == AuthorId);
            if(author == null)
                throw new InvalidOperationException("Güncellenecek Yazar Bulunamadı!");
            _mapper.Map(Model, author);

            _context.SaveChanges();
        }


    }

    public class UpdateAuthorModel{
        public string Name {get; set;}
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
    }
}