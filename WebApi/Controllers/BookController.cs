using System;
using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentValidation;

namespace WebApi.Controllers{
    
    [ApiController]
    [Route("[controller]s")]
    public class BookController: ControllerBase{
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    [HttpGet]
    public IActionResult GetBooks(){
        GetBooksQuery query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id){
        BookDetailViewModel result;
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();
            
        return Ok(result);
        
    }
    
    // [HttpGet]
    // public Book Get([FromQuery] string id){
    //     var book = BookList.Where(book=> book.Id == Convert.ToInt32(id)).SingleOrDefault();
    //     return book;
    // }
    
    //post
    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook){
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator(_context);
            validator.ValidateAndThrow(command);
            command.Handle();
            // if(!result.IsValid)
            //     foreach(var item in result.Errors){
            //         Console.WriteLine("Özellik" + item.PropertyName + "-Error Message: " + item.ErrorMessage);
            //     }
            
        return Ok();    
    }


    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook){
        
            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        
        return Ok();
    }   

    [HttpDelete("{id}")]
    public IActionResult DeleteBook (int id){
        
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            
        return Ok();
    }
    }
}