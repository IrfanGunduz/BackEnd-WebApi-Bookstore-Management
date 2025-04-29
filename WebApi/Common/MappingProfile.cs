using AutoMapper;
using WebApi.Application.AuthorOperations.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.UpdateAuthor;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.Entities;

namespace WebApi.Common{


    public class MappingProfile : Profile{

        public MappingProfile(){
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=> src.Genre.Name)).ForMember(dest => dest.Author, opt=>opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            CreateMap<Book, BooksViewModel>().ForMember(dest=>dest.Genre, opt=>opt.MapFrom(src=> src.Genre.Name)).ForMember(dest=>dest.Author, opt=>opt.MapFrom(src=> src.Author.Name + " " + src.Author.Surname));
            CreateMap<UpdateBookModel, Book>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null && !srcMember.Equals(default)));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null && !srcMember.Equals(default)));
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>();


        }



    }
}