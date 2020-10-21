using DTOs.React;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Validators.User
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().MinimumLength(5);
            RuleFor(x => x.Role).NotNull();
        }
    }
}
