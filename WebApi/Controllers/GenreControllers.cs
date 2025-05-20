using System;
using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentValidation;
using WebApi.Entities;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.GenreOperations.CreateGenre;
using WebApi.Applications.GenreOperations.UpdateGenre;
using WebApi.Applications.GenreOperations.DeleteGenre;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class GenreController: ControllerBase{
        public readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres(){
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }


        [HttpGet("id")]
        public ActionResult GetGenres(int id){
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }


        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre){

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = newGenre;

            CreateGenreCommandsValidator validator = new CreateGenreCommandsValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();

        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre){

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = updateGenre;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id){
            DeleteGenreCommmand command = new DeleteGenreCommmand(_context);
            command.GenreId = id;

            DeleteGenreCommmandValidator validator = new DeleteGenreCommmandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}