using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.CreateAuthor;
using WebApi.Application.AuthorOperations.DeleteAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.UpdateAuthor;
using WebApi.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Controllers{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController: ControllerBase{

        public readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult GetAuthors(){
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("id")]
        public ActionResult GetAuthors(int id){
            // GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            // query.AuthorId = id;
            // GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            // validator.ValidateAndThrow(query);
            // var obj = query.Handle();
            // return Ok(obj);
            AuthorDetailViewModel result;
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);

        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor){
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = newAuthor;
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor){
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            command.AuthorId = id;
            command.Model = updateAuthor;
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor (int id){
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = id;
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

        }















    }
}