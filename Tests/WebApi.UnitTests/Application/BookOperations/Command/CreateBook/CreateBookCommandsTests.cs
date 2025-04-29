using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook{

    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>{
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationsException_ShouldBeReturn(){
            //arrange (Hazırlık)
            var book = new Book(){Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationsException_ShouldBeReturn", PageCount =100, PublishDate= new System.DateTime(1990, 01, 10), GenreId = 1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel(){Title = book.Title};
            
            //act (Çalıştırma) && assert (Doğrulama)
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidCastException>().And.Message.Should().Be("Kitap zaten mevcut");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated(){
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel(){Title = "Hobbit", PageCount=1000, PublishDate=DateTime.Now.Date.AddYears(-10), GenreId = 1};
            command.Model = model;
            //act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(book=> book.Title == model.Title);
            
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}