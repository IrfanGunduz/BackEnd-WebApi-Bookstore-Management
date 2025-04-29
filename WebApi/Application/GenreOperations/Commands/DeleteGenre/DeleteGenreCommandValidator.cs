using System;
using System.Linq;
using FluentValidation;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Applications.GenreOperations.DeleteGenre{

    public class DeleteGenreCommmandValidator: AbstractValidator<DeleteGenreCommmand>{

        public DeleteGenreCommmandValidator(){
            RuleFor(command => command.GenreId).GreaterThan(0);
        }

    }

}