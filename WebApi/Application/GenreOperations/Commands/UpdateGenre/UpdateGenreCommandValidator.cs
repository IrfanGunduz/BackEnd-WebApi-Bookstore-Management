
using FluentValidation;


namespace WebApi.Applications.GenreOperations.UpdateGenre{

    public class UpdateGenreCommandValidator: AbstractValidator<UpdateGenreCommand>{
    
    public UpdateGenreCommandValidator(){
        RuleFor(command => command.Model.Name).MinimumLength(4).When(x=> x.Model.Name.Trim() != string.Empty);
        
    }
    }


}