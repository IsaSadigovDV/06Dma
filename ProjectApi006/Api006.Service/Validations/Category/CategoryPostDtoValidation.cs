using Api006.Service.Dtos;
using FluentValidation;

namespace Api006.Service.Validations.Category
{
    public class CategoryPostDtoValidation:AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Item can not be null")
                .NotEmpty().WithMessage("Item can not be empty")
                .MaximumLength(30)
                .MinimumLength(3);
        }
    }
}
