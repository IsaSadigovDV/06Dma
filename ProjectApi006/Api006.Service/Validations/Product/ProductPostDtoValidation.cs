using Api006.Service.Dtos.Product;
using Api006.Service.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api006.Service.Validations.Product
{
    public class ProductPostDtoValidation:AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidation()
        {
            RuleFor(x=>x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(x=>x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10000);
            RuleFor(x=>x.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(x => x)
                .NotNull().WithMessage("olmadi ki")
                .NotEmpty().WithMessage("agilli ol")
                .Custom((x, context) =>
            {
                if (!x.File.IsImage())
                {
                    context.AddFailure("File", "File is not an image");
                }
                if (!x.File.IsSizeOk(5))
                {
                    context.AddFailure("File", "File size must be 5 mb");
                }
            });
                
        }
    }
}
