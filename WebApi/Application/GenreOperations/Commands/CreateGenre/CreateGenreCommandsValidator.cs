using FluentValidation;
using WebApi.Applications.GenreOperations.CreateGenre;

namespace WebApi.Applications.GenreOperations.CreateGenre{


    public class CreateGenreCommandsValidator: AbstractValidator<CreateGenreCommand>{
        public CreateGenreCommandsValidator(){
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(4);
        }
    }

}