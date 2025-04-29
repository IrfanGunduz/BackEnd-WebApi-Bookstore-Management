using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.BookOperations.Commands.CreateBook{

    public class CreateBookCommandsValidatorTests : IClassFixture<CommonTestFixture>{

        private readonly IBookStoreDbContext _context;

        public CreateBookCommandsValidatorTests(CommonTestFixture testFixture){
            _context = testFixture.Context;
        
        }
        [Theory]
        [InlineData("Lord of the rings",0,0)]
        [InlineData("Lord of the rings",0,1)]
        [InlineData("Lord of the rings",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("lor",100,1)]
        [InlineData("lord",100,0)]
        [InlineData("lord",0,1)]
        [InlineData(" ",100,1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnedErrors(string title, int PageCount, int genreId){
            //arrange
            CreateBookCommand command = new CreateBookCommand(_context, null);
            command.Model = new CreateBookModel(){
                Title = title,
                PageCount = PageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };
           //act
           CreateBookCommandValidator validator = new CreateBookCommandValidator(_context);
           var result = validator.Validate(command);
           //assert
           result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError(){
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator(_context);
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(){
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel(){
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator(_context);
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}