using System;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.CreateAuthor{

    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>{

        public CreateAuthorCommandValidator(){
            RuleFor(command=> command.Model.Name).NotEmpty().MinimumLength(2);
            RuleFor(command=> command.Model.Surname).NotEmpty().MinimumLength(2);
            RuleFor(command=> command.Model.BirthDate).LessThan(DateTime.Now.Date);
            
        }
    }
}