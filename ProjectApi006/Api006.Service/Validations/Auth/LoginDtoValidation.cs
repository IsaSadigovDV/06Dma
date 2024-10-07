using Api006.Service.Dtos.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api006.Service.Validations.Auth
{
    public class LoginDtoValidation:AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(r => r.UserName)
              .NotEmpty()
              .NotNull();
            RuleFor(r => r.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
