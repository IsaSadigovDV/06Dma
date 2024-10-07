using Api006.Service.Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api006.Service.Validations.Auth
{
    public class RegisterDtoValidations : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidations()
        {
            RuleFor(r => r.UserName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);
            RuleFor(r => r.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(r => r)
                .NotEmpty()
                .NotNull()
                .Custom((p, context) =>
            {
                if (p.Password != p.ConfirmPassword)
                {
                    context.AddFailure(p.ConfirmPassword, "Passwords do not match!!!");
                }
            });


        }
    }
}
