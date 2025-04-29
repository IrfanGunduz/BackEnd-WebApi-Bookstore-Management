using System;
using System.IO.Compression;
using System.Linq;
using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook{

    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>{
        public CreateBookCommandValidator(IBookStoreDbContext _dbContext){
            RuleFor(command=> command.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(command=> command.Model.GenreId).GreaterThan(0).Must(genreId=> _dbContext.Genres.Any(g=> g.Id == genreId)).WithMessage("Geçerli Bir Genre Id Giriniz!");
            RuleFor(command=> command.Model.AuthorId).GreaterThan(0).Must(authorId=> authorId == null || _dbContext.Authors.Any(a=> a.Id == authorId)).WithMessage("Geçerli Bir Author Id Giriniz!");
            RuleFor(command=> command.Model.PageCount).GreaterThan(0);
            RuleFor(command=> command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            
            
        }

    }


}